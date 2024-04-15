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

        public PhonesController(PhoneRepository phoneRepository)
        {
            _phoneRepository = phoneRepository;
        }
        [AllowAnonymous]
        [Route("Phones")]
        [HttpGet]
        public async Task<IActionResult> Index(int? page, string? searchString)
        {
            const int pageSize = 7; 
            var pageNumber = page ?? 1;
            ViewData["CurrentFilter"] = searchString;
            var phones = _phoneRepository.GetAll();
            if (!string.IsNullOrEmpty(searchString))
            {
                phones = phones.Where(ps => ps!.brandName!.Contains(searchString) || ps.modelName!.Contains(searchString));
            }
            var paginatedPhones = await phones.ToList()
                .Select(p => p!.ToPhoneDto())
                .ToPagedListAsync(pageNumber, pageSize);
            return View(paginatedPhones);
        }
        [AllowAnonymous]
        [Route("Details/{id:int}")]
        [HttpGet]
        public async Task<IActionResult> Details(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var phone = await _phoneRepository.Get(id.Value);
            if (phone == null)
            {
                return NotFound();
            }

            return View(phone.ToPhoneDto());
        }
        [Authorize(Roles="Admin")]
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
            catch (Exception ex)
            {
                switch (ex.Message)
                {
                    case "1":
                        return View("NoPhoneFoundAPI");
                    case "2":
                        return View("PhoneExists");
                    default:
                        throw;
                }
            }
        }

        [Authorize(Roles="Admin,Editor")]
        public async Task<IActionResult> Edit(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var phone = await _phoneRepository.Get(id.Value);
            return phone == null ? NotFound() : View(phone.ToPhoneDto());
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

            var phoneToUpdate = await _phoneRepository.Get(id);
            if (phoneToUpdate == null)
            {
                return NotFound();
            }

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
            var phone = await _phoneRepository.Get(id.Value);
            if(phone == null)
            {
                return NotFound();
            }

            return View(phone.ToPhoneDto());
        }
        [Authorize(Roles="Admin")]
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> DeleteConfirmed(int id)
        {
            var phone = await _phoneRepository.Get(id);
            if (phone != null)
            {
                await _phoneRepository.Remove(phone);
            }
            return RedirectToAction(nameof(Index));
        }
    }
}
