using System.Collections;
using System.Diagnostics;
using Humanizer;
using Microsoft.AspNetCore.Mvc;
using Newtonsoft.Json;
using OptiCompare.Data;
using OptiCompare.Models;
using X.PagedList;

namespace OptiCompare.Controllers;

public class CompareController : Controller
{
    private const string ViewPath = "~/Views/Comparison/Index.cshtml";

    private PhoneComparer? _phoneComparer = new()
    {
        phones = new List<Phone>()
    };
    private readonly OptiCompareDbContext _context;
    public CompareController(OptiCompareDbContext context)
    {
        _context = context;
    }
    public IActionResult Index()
    {
        if(TempData.Peek("PhoneComparer")?.ToString() is { } phoneComparerJson)
        {
            var phoneComparer = JsonConvert.DeserializeObject<PhoneComparer?>(phoneComparerJson);

            if(phoneComparer == null)
            {
                Console.WriteLine("Failed to deserialize PhoneComparer object.");
                return View(ViewPath);
            }
            return View(ViewPath,phoneComparer);
        }

        return View(ViewPath);
    }
    public async Task<IActionResult?> Add(int? id)
    {
        var phone = _context.phones.SingleOrDefault(phone1 => phone1.Id == id);
        if (_phoneComparer?.phones != null && phone != null)
        {
            //check tempData content
            if(TempData["PhoneComparer"] != null)
            {
                _phoneComparer = JsonConvert.DeserializeObject<PhoneComparer>(TempData["PhoneComparer"]?.ToString() ?? string.Empty);
                if(_phoneComparer == null)
                {
                    Console.WriteLine("Deserialization of PhoneComparer failed.");
                    return View(ViewPath);
                }
                //limit to 4 phones on comparison page
                if (_phoneComparer.phones!.Count > 3)
                {
                    TempData["Message"] = "Cannot compare more than 4 phones!";
                    var samePhoneComparerJson = JsonConvert.SerializeObject(_phoneComparer);
                    TempData["PhoneComparer"] = samePhoneComparerJson;
                    return RedirectToAction(nameof(Index));
                }
                
                Debug.Assert(_phoneComparer.phones != null, "_phoneComparer.phones != null");
                if (_phoneComparer.phones.Any(p => p.Id == phone.Id))
                {
                    TempData["Message"] = "Trying to add a phone thats already in comparison";
                    var samePhoneComparerJson = JsonConvert.SerializeObject(_phoneComparer);
                    TempData["PhoneComparer"] = samePhoneComparerJson;
                    int pageNumber = 1;
                    int pageSize = 7;
                    var phonesPagedList = await _context.phones.ToPagedListAsync(pageNumber, pageSize);
                    return View("~/Views/Phones/Index.cshtml",phonesPagedList);
                }
            }
            _phoneComparer?.phones?.Add(phone);
            var phoneComparerJson = JsonConvert.SerializeObject(_phoneComparer);
            TempData["PhoneComparer"] = phoneComparerJson;
            return RedirectToAction(nameof(Index));
        }
        Console.WriteLine("Trying to add null phone to comparison!");
        return null;
    }
    
    public async Task<IActionResult> Remove(int? id)
    {
        _phoneComparer = JsonConvert.DeserializeObject<PhoneComparer>(TempData.Peek("PhoneComparer")?.ToString() ?? string.Empty);
        if (_phoneComparer?.phones != null)
        {
            var phoneToRemove = _phoneComparer?.phones.FirstOrDefault(phone => phone.Id == id);
            Debug.Assert(phoneToRemove != null, nameof(phoneToRemove) + " != null");
            _phoneComparer?.phones.Remove(phoneToRemove);
            var phoneComparerJson = JsonConvert.SerializeObject(_phoneComparer);
            TempData["PhoneComparer"] = phoneComparerJson;
            return RedirectToAction(nameof(Index));
        }
        int pageNumber = 1;
        int pageSize = 10; // or whatever size you want
        var phonesPagedList = await _context.phones.ToPagedListAsync(pageNumber, pageSize);
        Console.WriteLine("Cannot remove non-existing phone from comparison!");
        return View("~/Views/Phones/Index.cshtml",phonesPagedList);
    }
    
}