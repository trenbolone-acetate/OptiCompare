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
        if (TempData.Peek("PhoneComparer")?.ToString() is { } phoneComparerJson)
        {
            var phoneComparer = JsonConvert.DeserializeObject<PhoneComparer?>(phoneComparerJson);

            if (phoneComparer == null)
            {
                Console.WriteLine("Failed to deserialize PhoneComparer object.");
                return View(ViewPath);
            }

            return View(ViewPath, phoneComparer);
        }

        return View(ViewPath);
    }

    public async Task<IActionResult?> Add(int? id)
{
    var phone = _phoneRepository.GetAll().SingleOrDefault(p => p?.Id == id);
    if (_phoneComparer?.dtoPhones != null && phone != null)
    {
        if (TempData.TryGetValue("PhoneComparer", out var phoneComparerData))
        {
            _phoneComparer = JsonConvert.DeserializeObject<PhoneComparer>(phoneComparerData?.ToString() ?? string.Empty);
            if (_phoneComparer == null)
            {
                Console.WriteLine("Deserialization of PhoneComparer failed.");
                return View(ViewPath);
            }
            if (_phoneComparer.dtoPhones!.Count > 3)
            {
                TempData["Message"] = "Cannot compare more than 4 phones!";
                TempData["PhoneComparer"] = JsonConvert.SerializeObject(_phoneComparer);
                return RedirectToAction(nameof(Index));
            }

            if (_phoneComparer.dtoPhones.Any(p => p.Id == phone.Id))
            {
                TempData["Message"] = "Trying to add a phone that's already in comparison";
                TempData["PhoneComparer"] = JsonConvert.SerializeObject(_phoneComparer);
                int pageNumber = 1;
                int pageSize = 7;
                var phonesPagedList = await _phoneRepository.GetAll()
                    .Select(p => p.ToPhoneDto())
                    .ToPagedListAsync(pageNumber, pageSize);
                return View("~/Views/Phones/Index.cshtml", phonesPagedList);
            }
        }

        // Add the phone to comparison
        _phoneComparer?.dtoPhones?.Add(phone.ToPhoneDto());
        TempData["PhoneComparer"] = JsonConvert.SerializeObject(_phoneComparer);
        return RedirectToAction(nameof(Index));
    }

    // Log if trying to add a null phone to comparison
    Console.WriteLine("Trying to add null phone to comparison!");
    return null;
}


    public async Task<IActionResult> Remove(int? id)
    {
        RetrievePhoneComparer();
    
        if (_phoneComparer?.dtoPhones != null)
        {
            var phoneToRemove = FindPhoneToRemove(id);
            Debug.Assert(phoneToRemove != null, nameof(phoneToRemove) + " != null");
            _phoneComparer?.dtoPhones.Remove(phoneToRemove);
            UpdatePhoneComparer();
   
            return RedirectToAction(nameof(Index));
        }

        return await ReturnViewWithPagedListAndErrorMessage();
    }

    private void RetrievePhoneComparer()
    {
        string json = TempData.Peek("PhoneComparer")?.ToString() ?? string.Empty;
        _phoneComparer = JsonConvert.DeserializeObject<PhoneComparer>(json);
    }

    private PhoneDto FindPhoneToRemove(int? id)
    {
        return _phoneComparer?.dtoPhones.FirstOrDefault(phone => phone.Id == id);
    }

    private void UpdatePhoneComparer()
    {
        string updatedComparerJson = JsonConvert.SerializeObject(_phoneComparer);
        TempData["PhoneComparer"] = updatedComparerJson;
    }

    private async Task<IActionResult> ReturnViewWithPagedListAndErrorMessage()
    {
        const string ErrorMessage = "Cannot remove non-existing phone from comparison!";
        const int PageNumber = 1;
        const int PageSize = 6;

        var phonesPagedList = await _phoneRepository
            .GetAll()
            .Select(p => p.ToPhoneDto())
            .ToPagedListAsync(PageNumber, PageSize);

        Console.WriteLine(ErrorMessage);
        return View("~/Views/Phones/Index.cshtml", phonesPagedList);
    }
}