using System.Collections;
using System.Diagnostics;
using Humanizer;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using Newtonsoft.Json;
using OptiCompare.Data;
using OptiCompare.DTOs;
using OptiCompare.Mappers;
using OptiCompare.Models;
using OptiCompare.PhoneSpecs;
using OptiCompare.Repositories;
using X.PagedList;

namespace OptiCompare.Controllers;

public class CompareController : Controller
{
    private const string ViewPath = "~/Views/Comparison/Index.cshtml";

    private PhoneComparer? _phoneComparer = new()
    {
        dtoPhones = new List<PhoneDto>()
    };
    private readonly PhoneRepository _phoneRepository;

    public CompareController(PhoneRepository phoneRepository)
    {
        _phoneRepository = phoneRepository;
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
        var phone = _phoneRepository.GetAll()
            .SingleOrDefault(phone1 => phone1.Id == id);
        if (_phoneComparer?.dtoPhones != null && phone != null)
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
                if (_phoneComparer.dtoPhones!.Count > 3)
                {
                    TempData["Message"] = "Cannot compare more than 4 phones!";
                    var samePhoneComparerJson = JsonConvert.SerializeObject(_phoneComparer);
                    TempData["PhoneComparer"] = samePhoneComparerJson;
                    return RedirectToAction(nameof(Index));
                }
                
                Debug.Assert(_phoneComparer.dtoPhones != null, "_phoneComparer.phones != null");
                if (_phoneComparer.dtoPhones.Any(p => p.Id == phone.Id))
                {
                    TempData["Message"] = "Trying to add a phone thats already in comparison";
                    var samePhoneComparerJson = JsonConvert.SerializeObject(_phoneComparer);
                    TempData["PhoneComparer"] = samePhoneComparerJson;
                    int pageNumber = 1;
                    int pageSize = 7;
                    var phonesPagedList = await _phoneRepository.
                        GetAll()
                        .Select(p=>p.ToPhoneDto())
                        .ToPagedListAsync(pageNumber, pageSize);
                    return View("~/Views/Phones/Index.cshtml",phonesPagedList);
                }
            }
            _phoneComparer?.dtoPhones?.Add(phone.ToPhoneDto());
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
        if (_phoneComparer?.dtoPhones != null)
        {
            var phoneToRemove = _phoneComparer?.dtoPhones.FirstOrDefault(phone => phone.Id == id);
            Debug.Assert(phoneToRemove != null, nameof(phoneToRemove) + " != null");
            _phoneComparer?.dtoPhones.Remove(phoneToRemove);
            var phoneComparerJson = JsonConvert.SerializeObject(_phoneComparer);
            TempData["PhoneComparer"] = phoneComparerJson;
            return RedirectToAction(nameof(Index));
        }
        const int pageNumber = 1;
        const int pageSize = 6;
        var phonesPagedList = await _phoneRepository
            .GetAll()
            .Select(p => p.ToPhoneDto())
            .ToPagedListAsync(pageNumber, pageSize);
        Console.WriteLine("Cannot remove non-existing phone from comparison!");
        return View("~/Views/Phones/Index.cshtml",phonesPagedList);
    }
    
}