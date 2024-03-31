using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;
using Microsoft.EntityFrameworkCore;
using OptiCompare.Data;
using OptiCompare.Models;
using X.PagedList;

namespace OptiCompare.Controllers
{
    public class PhonesController : Controller
    {
        private readonly OptiCompareDbContext _context;

        public PhonesController(OptiCompareDbContext context)
        {
            _context = context;
        }

        public async Task<IActionResult> Index(int? page, string searchString)
        {
            const int pageSize = 7; 
            var pageNumber = page ?? 1;
            ViewData["CurrentFilter"] = searchString;
            var phones = from ps in _context.phones select ps;
            if (!string.IsNullOrEmpty(searchString))
            {
                phones = phones.Where(ps => ps.brandName!.Contains(searchString) || ps.modelName!.Contains(searchString));
            }
            var paginatedPhones = await phones.ToPagedListAsync(pageNumber, pageSize);
            return View(paginatedPhones);
        }

        public async Task<IActionResult> Details(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var phone = await _context.phones
                .FirstOrDefaultAsync(m => m.id == id);
            if (phone == null)
            {
                return NotFound();
            }

            return View(phone);
        }

        public IActionResult Create()
        {
            var newPhone = new Phone();
            return View(newPhone);
        }

        [HttpPost]
        public IActionResult CreateFromSearch(string searchString)
        {
            searchString = searchString.Replace(" ", "%20");
            var newPhone = PhoneDetailsFetcher.CreateFromSearch(searchString);
            if (newPhone.Result == null)
            {
                return View("NoPhoneFoundAPI");
            }
            if (_context.phones.Any(p => p.modelName == newPhone.Result.modelName))
            {
                return View("PhoneExists");
            }
            _context.phones.Add(newPhone.Result);
            _context.SaveChanges();
            return RedirectToAction(nameof(Index));
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Create([Bind("Id,brandName,modelName,hasNetwork5GBands," +
                                                      "BodyDimensions,DisplayDetails,PlatformDetails,storage," +
                                                      "CameraDetails,BatteryDetails,price,image")] Phone phone)
        {
            if (ModelState.IsValid)
            {
                _context.Add(phone);
                await _context.SaveChangesAsync();
                return RedirectToAction(nameof(Index));
            }

            Console.WriteLine("Model is not valid!");
            return View(phone);
        }

        public async Task<IActionResult> Edit(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var phone = await _context.phones.FindAsync(id);
            if (phone == null)
            {
                return NotFound();
            }
            return View(phone);
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Edit(int id, [Bind("Id,brandName,modelName,hasNetwork5GBands," +
                                                             "BodyDimensions,DisplayDetails,PlatformDetails,storage," +
                                                             "CameraDetails,BatteryDetails,price,image")] Phone phone)
        {
            Console.WriteLine("Entered Edit");
            var oldPhone = await _context.phones.FindAsync(id);
            
            if (oldPhone == null) return RedirectToAction(nameof(Index));
            
            _context.Entry(oldPhone).CurrentValues.SetValues(phone);
            await _context.SaveChangesAsync();
            return RedirectToAction(nameof(Index));
        }

        public async Task<IActionResult> Delete(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var phone = await _context.phones
                .FirstOrDefaultAsync(m => m.id == id);
            if (phone == null)
            {
                return NotFound();
            }

            return View(phone);
        }

        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> DeleteConfirmed(int id)
        {
            var phone = await _context.phones.FindAsync(id);
            if (phone != null)
            {
                _context.phones.Remove(phone);
            }
            
            await _context.SaveChangesAsync();
            return RedirectToAction(nameof(Index));
        }
    }
}
