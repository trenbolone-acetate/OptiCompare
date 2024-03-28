using System.Collections;
using System.Diagnostics;
using Microsoft.AspNetCore.Mvc;
using Newtonsoft.Json;
using OptiCompare.Data;
using OptiCompare.Models;

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
    public IActionResult? Add(int? id)
    {
        var phone = _context.Phones.SingleOrDefault(phone1 => phone1.Id == id);
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

                Debug.Assert(_phoneComparer.phones != null, "_phoneComparer.phones != null");
                if (_phoneComparer.phones.Any(p => p.Id == phone.Id))
                {
                    TempData["Message"] = "Trying to add a phone thats already in comparison";
                    var samePhoneComparerJson = JsonConvert.SerializeObject(_phoneComparer);
                    TempData["PhoneComparer"] = samePhoneComparerJson;
                    return View("~/Views/Phones/Index.cshtml",_context.Phones);
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
    
    public IActionResult Remove(int? id)
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

        Console.WriteLine("Cannot remove non-existing phone from comparison!");
        return View("~/Views/Phones/Index.cshtml",_context.Phones);
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
}