using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;
using Microsoft.EntityFrameworkCore;
using OptiCompare.Data;
using OptiCompare.Models;

namespace OptiCompare.Controllers
{
    public class PhonesController : Controller
    {
        private readonly OptiCompareDbContext _context;

        public PhonesController(OptiCompareDbContext context)
        {
            _context = context;
        }

        // GET: Phones
        public async Task<IActionResult> Index()
        {
              return _context.Phone != null ? 
                          View(await _context.Phone.ToListAsync()) :
                          Problem("Entity set 'OptiCompareDbContext.Phone'  is null.");
        }

        // GET: Phones/Details/5
        public async Task<IActionResult> Details(int? id)
        {
            if (id == null || _context.Phone == null)
            {
                return NotFound();
            }

            var phone = await _context.Phone
                .FirstOrDefaultAsync(m => m.Id == id);
            if (phone == null)
            {
                return NotFound();
            }

            return View(phone);
        }

        // GET: Phones/Create
        public IActionResult Create()
        {
            return View();
        }

        [HttpPost]
        public IActionResult CreateFromSearch(string searchString)
        {
            // Use the PhoneDetailsFetcher to create a new phone based on the search string
            searchString = searchString.Replace(" ", "%20");
            var newPhone = PhoneDetailsFetcher.CreateFromSearch(searchString);
            // Check if the phone already exists in the database
            if (_context.Phone.Any(p => p.modelName == newPhone.Result.modelName))
            {
                // If the phone already exists, return a view indicating this
                return View("PhoneExists");
            }
            // If the phone does not exist, add it to the database and save changes
            _context.Phone.Add(newPhone.Result);
            _context.SaveChanges();
            // Redirect to the index view
            return RedirectToAction(nameof(Index));
        }

        // POST: Phones/Create
        // To protect from overposting attacks, enable the specific properties you want to bind to.
        // For more details, see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Create([Bind("Id,brandName,modelName,hasNetwork5GBands,bodyWidth,bodyHeight,bodyThickness,bodyWeight,displayType,displaySize,displayResolution,Cpu,Gpu,Os,RAM,storage,mainCameraDetails,frontCameraDetails,batteryCapacity,chargingSpeed,batteryLifeTest,price")] Phone phone)
        {
            if (ModelState.IsValid)
            {
                _context.Add(phone);
                await _context.SaveChangesAsync();
                return RedirectToAction(nameof(Index));
            }
            return View(phone);
        }

        // GET: Phones/Edit/5
        public async Task<IActionResult> Edit(int? id)
        {
            if (id == null || _context.Phone == null)
            {
                return NotFound();
            }

            var phone = await _context.Phone.FindAsync(id);
            if (phone == null)
            {
                return NotFound();
            }
            return View(phone);
        }

        // POST: Phones/Edit/5
        // To protect from overposting attacks, enable the specific properties you want to bind to.
        // For more details, see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Edit(int id, [Bind("Id,brandName,modelName,hasNetwork5GBands,bodyWidth,bodyHeight,bodyThickness,bodyWeight,displayType,displaySize,displayResolution,Cpu,Gpu,Os,RAM,storage,mainCameraDetails,frontCameraDetails,batteryCapacity,chargingSpeed,batteryLifeTest,price")] Phone phone)
        {
            if (id != phone.Id)
            {
                return NotFound();
            }

            if (ModelState.IsValid)
            {
                try
                {
                    _context.Update(phone);
                    await _context.SaveChangesAsync();
                }
                catch (DbUpdateConcurrencyException)
                {
                    if (!PhoneExists(phone.Id))
                    {
                        return NotFound();
                    }
                    else
                    {
                        throw;
                    }
                }
                return RedirectToAction(nameof(Index));
            }
            return View(phone);
        }

        // GET: Phones/Delete/5
        public async Task<IActionResult> Delete(int? id)
        {
            if (id == null || _context.Phone == null)
            {
                return NotFound();
            }

            var phone = await _context.Phone
                .FirstOrDefaultAsync(m => m.Id == id);
            if (phone == null)
            {
                return NotFound();
            }

            return View(phone);
        }

        // POST: Phones/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> DeleteConfirmed(int id)
        {
            if (_context.Phone == null)
            {
                return Problem("Entity set 'OptiCompareDbContext.Phone'  is null.");
            }
            var phone = await _context.Phone.FindAsync(id);
            if (phone != null)
            {
                _context.Phone.Remove(phone);
            }
            
            await _context.SaveChangesAsync();
            return RedirectToAction(nameof(Index));
        }

        private bool PhoneExists(int id)
        {
          return (_context.Phone?.Any(e => e.Id == id)).GetValueOrDefault();
        }

    }
}
