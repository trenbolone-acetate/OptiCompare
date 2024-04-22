using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;
using Microsoft.EntityFrameworkCore;
using OptiCompare.Data;
using OptiCompare.DTOs;
using OptiCompare.Extensions;
using OptiCompare.Mappers;
using OptiCompare.Models;
using OptiCompare.PhoneSpecs;
using OptiCompare.Repositories;
using X.PagedList;

namespace OptiCompare.Controllers
{
    public class PhonesController : Controller
    {
        private readonly PhoneRepository _phoneRepository;
        private IActionResult NoPhoneFound() => View("NoPhoneFound");

        public PhonesController(PhoneRepository phoneRepository)
        {
            _phoneRepository = phoneRepository;
        }
        [AllowAnonymous]
        public async Task<IActionResult> Index(int? page, string? searchString)
        {
            const int pageSize = 7; 
            var pageNumber = page ?? 1;
            ViewData["CurrentFilter"] = searchString;
    
            var filteredPhones = GetFilteredPhones(searchString);
    
            var paginatedPhones = await filteredPhones.ToList().Select(p => p!.ToPhoneDto()).ToPagedListAsync(pageNumber, pageSize);
            return View(paginatedPhones);
        }
        [AllowAnonymous]
        public Task<IActionResult> Details(int? id)
        {
            if (!id.HasValue)
            {
                return Task.FromResult(NoPhoneFound());
            }

            var phone = GetPhoneById(id.Value).Result;

            return Task.FromResult<IActionResult>(View(phone.ToPhoneDto()));
        }
        [Authorize(Roles="Admin")]
        [HttpPost]
        public IActionResult Create()
        {
            var newPhoneDto = new PhoneDto();
            return View(newPhoneDto);
        }
        [Authorize(Roles="Admin")]
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Create([FromForm][Bind("brandName,modelName,hasNetwork5GBands," +
                                                                "bodyDimensions,displayDetails,platformDetails,storage," +
                                                                "cameraDetails,batteryDetails,price,image")] PhoneDto phoneDto)
        {
            if (ModelState.IsValid)
            {
                await _phoneRepository.Add(phoneDto.ToPhone());
                return RedirectToAction(nameof(Index));
            }
  
            Console.WriteLine("Model is not valid!");
            return View(phoneDto);
        }
        [Authorize(Roles="Admin")]
        [HttpPost]
        public async Task<IActionResult> CreateFromSearch(string searchString)
        {
            try
            {
                await _phoneRepository.CreateFromSearch(searchString);
                return RedirectToAction(nameof(Index));
            }
            catch (InvalidOperationException ex)
            {
                switch (ex.Message)
                {
                    case "1":
                        return View("NoPhoneFound",searchString);
                    case "2":
                        return View("PhoneExists",searchString);
                    default:
                        throw;
                }
            }
        }

        [Authorize(Roles="Admin,Editor")]
        public Task<IActionResult> Edit(int? id)
        {
            if (id == null)
            {
                return Task.FromResult<IActionResult>(NotFound());
            }

            var phone = GetPhoneById(id.Value).Result;
            return Task.FromResult<IActionResult>(View(phone.ToPhoneDto()));
        }
        [Authorize(Roles="Admin,Editor")]
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Edit(int id, [FromForm][Bind("Id,brandName,modelName,hasNetwork5GBands," +
                                                                      "bodyDimensions,displayDetails,platformDetails,storage," +
                                                                      "cameraDetails,batteryDetails,price,image")] PhoneDto phoneDto)
        {
            if (id != phoneDto.Id)
            {
                return NotFound();
            }

            var phoneToUpdate = GetPhoneById(id).Result;

            phoneToUpdate.CopyDtoToPhone(phoneDto);
            try
            {
                await _phoneRepository.Update(phoneToUpdate);
                return RedirectToAction(nameof(Index));
            }
            catch (DbUpdateConcurrencyException)
            {
            }
            return View(phoneDto);
        }
        [Authorize(Roles="Admin")]
        [HttpGet]
        public async Task<IActionResult> Delete(int? id)
        {
            if(id == null)
            {
                return NotFound();
            }
            var phone = GetPhoneById(id.Value).Result;

            return View(phone.ToPhoneDto());
        }
        [Authorize(Roles="Admin")]
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> DeleteConfirmed(int id)
        {
            var phone = GetPhoneById(id).Result;
            await _phoneRepository.Remove(phone);
            return RedirectToAction(nameof(Index));
        }
        private IEnumerable<Phone?> GetFilteredPhones(string? searchString)
        {
            var phones = _phoneRepository.GetAll();
            if (!string.IsNullOrEmpty(searchString))
            {
                phones = phones.Where(ps => ps!.brandName!.Contains(searchString) || ps.modelName!.Contains(searchString));
            }
            return phones;
        }
        [Route("GetPhoneById/{id:int}")]
        [Authorize]
        [HttpGet]
        public async Task<Phone> GetPhoneById(int id)
        {
            return await _phoneRepository.Get(id) ?? new Phone();
        }
    }
}
