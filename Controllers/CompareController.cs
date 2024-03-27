using System.Collections;
using Microsoft.AspNetCore.Mvc;
using Newtonsoft.Json;
using OptiCompare.Data;
using OptiCompare.Models;

namespace OptiCompare.Controllers;

public class CompareController : Controller
{
    private readonly string _viewPath = "~/Views/Comparison/Index.cshtml"; 
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
                    return View(_viewPath);
                }
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
    
    public IActionResult Remove(Phone phone)
    {
        return null;

    }

    public IActionResult Index()
    {
        if(TempData.Peek("PhoneComparer")?.ToString() is { } phoneComparerJson)
        {
            var phoneComparer = JsonConvert.DeserializeObject<PhoneComparer?>(phoneComparerJson);

            if(phoneComparer == null)
            {
                Console.WriteLine("Failed to deserialize PhoneComparer object.");
                return View(_viewPath);
            }
            //phoneComparer -> Comparer/Index
            return View(_viewPath,phoneComparer);
        }

        // handle case where TempData['PhoneComparer'] doesn't exist
        return View(_viewPath);
    }
}