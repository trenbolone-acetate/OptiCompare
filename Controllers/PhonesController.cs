using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;
using Microsoft.EntityFrameworkCore;
using Mysqlx;
using OptiCompare.Data;
using OptiCompare.DTOs;
using OptiCompare.Extensions;
using OptiCompare.Mappers;
using OptiCompare.Models;
using OptiCompare.PhoneSpecs;
using OptiCompare.Repositories;
using OptiCompare.Services;
using X.PagedList;

namespace OptiCompare.Controllers
{
    public class PhonesController : Controller
    {
        private IPhoneService _phoneService;
        private IActionResult NoPhoneFound() => View("NoPhoneFound");

        public PhonesController(IPhoneService phoneService)
        {
            _phoneService = phoneService;
        }
        [AllowAnonymous]
        public async Task<IActionResult> Index(int? page, string? searchString)
        {
            var paginatedPhones = await _phoneService.GetPaginatedPhones(page, searchString);
            return View(paginatedPhones);
        }
        [AllowAnonymous]
        public async Task<IActionResult> Details(int? id)
        {
            if (!id.HasValue)
            {
                return NoPhoneFound();
            }
            var phoneDto = await _phoneService.GetPhoneById(id);

            return View(phoneDto);
        }
        //GET Create
        [Authorize(Roles="Admin")]
        public IActionResult Create()
        {
            var newPhoneDto = new PhoneDto();
            return View(newPhoneDto);
        }
        //POST Create
        [Authorize(Roles="Admin")]
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Create([FromForm][Bind("brandName,modelName,hasNetwork5GBands," +
                                                                "bodyDimensions,displayDetails,platformDetails,storage," +
                                                                "cameraDetails,batteryDetails,price,image")] PhoneDto phoneDto)
        {
            bool result = await _phoneService.AddPhone(phoneDto);
            if(!result)
            {
                return View();
            }
            return RedirectToAction(nameof(Details), new{id = phoneDto.Id});
        }
        [Authorize(Roles="Admin")]
        [HttpPost]
        public async Task<IActionResult> CreateFromSearch(string searchString)
        {
            var returnCode = await _phoneService.CreateFromSearch(searchString);
            return returnCode switch
            {
                "0" => RedirectToAction(nameof(Index)),
                "1" => View("NoPhoneFound", searchString),
                "2" => View("PhoneExists", searchString),
                _ => RedirectToAction(nameof(Index))
            };
        }

        [Authorize(Roles="Admin,Editor")]
        public async Task<IActionResult> Edit(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var phoneDto = await _phoneService.GetPhoneById(id);
            return View(phoneDto);
        }
        [Authorize(Roles="Admin,Editor")]
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Edit(int id, [FromForm][Bind("Id,brandName,modelName,hasNetwork5GBands," +
                                                                      "bodyDimensions,displayDetails,platformDetails,storage," +
                                                                      "cameraDetails,batteryDetails,price,image")] PhoneDto phoneDto)
        {
            bool result = await _phoneService.EditPhone(id, phoneDto);
            if(!result)
            {
                return View();
            }
            return RedirectToAction(nameof(Index));
        }
        //GET Delete
        [Authorize(Roles="Admin")]
        [HttpGet]
        public async Task<IActionResult> Delete(int? id)
        {
            if(id == null)
            {
                return NotFound();
            }
            var phoneDto = await _phoneService.GetPhoneById(id);

            return View(phoneDto);
        }
        //POST Delete
        [Authorize(Roles="Admin")]
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> DeleteConfirmed(int id)
        {
            bool result = await _phoneService.DeletePhone(id);
            if(!result)
            {
                return View();
            }
            return RedirectToAction(nameof(Index));
        }

        [Route("GetPhoneById/{id:int}")]
        [Authorize]
        [HttpGet]
        public async Task<Phone> GetPhoneById(int id)
        {
            var phone = await _phoneService.GetPhoneById(id);
            return phone.ToPhone();
        }
    }
}
