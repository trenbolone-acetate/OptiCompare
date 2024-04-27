using System.Text.RegularExpressions;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using NuGet.Protocol;
using OptiCompare.DTOs;
using OptiCompare.Extensions;
using OptiCompare.Mappers;
using OptiCompare.Models;
using OptiCompare.Repositories;
using Serilog;
using Serilog.Core;
using Serilog.Formatting.Compact;
using Serilog.Formatting.Json;
using X.PagedList;
using ILogger = Microsoft.Extensions.Logging.ILogger;

namespace OptiCompare.Services;

public class PhoneService : IPhoneService
{
    private readonly IRepository<Phone> _phoneRepository;
    private readonly CustomLogger _customLogger = new("Logs/PhoneServiceLogs/log.json");
    public PhoneService(IRepository<Phone> phoneRepository)
    {
        _phoneRepository = phoneRepository;
    }

    #region IRepository members
    public async Task<IPagedList<PhoneDto>> GetPaginatedPhones(int? page, string? searchString)
    {   
        _customLogger.Log($"GetPaginatedPhones(int? {page}, string? {searchString}); was called.");
        const int pageSize = 7;
        var pageNumber = page ?? 1;

        var filteredPhones = GetFilteredPhones(searchString);

        return await filteredPhones
            .ToList()
            .Select(p => p!.ToPhoneDto())
            .ToPagedListAsync(pageNumber, pageSize);
    }

    public async Task<string> CreateFromSearch(string searchString)
    {
        try
        {
            await _phoneRepository.CreateFromSearch(searchString);
            return "0";
        }
        catch (InvalidOperationException ex)
        {
            return ex.Message;
        }
    }
    public async Task<PhoneDto?> GetPhoneById(int? id)
    {
        var phone = await _phoneRepository.Get(id);
        return phone?.ToPhoneDto();
    }

    public async Task<bool> AddPhone(PhoneDto phoneDto)
    {
        _customLogger.Log($"AddPhone(PhoneDto {phoneDto.modelName}); was called.");
        bool result = await _phoneRepository.Add(phoneDto.ToPhone());
        return result;
    }

    public async Task<bool> EditPhone(int? id, PhoneDto phoneDto)
    {
        _customLogger.Log($"EditPhone(int? {id}, PhoneDto? {phoneDto.modelName}); was called.");
        if (id == null)
        {
            throw new Exception("Redundant phone id!");
        }
        if (id != phoneDto.Id)
        {
            throw new Exception("No phone with such id found!");
        }
        var phoneToUpdate = await _phoneRepository.Get(id);
        phoneToUpdate.CopyDtoToPhone(phoneDto);
        bool result = await _phoneRepository.Update(phoneToUpdate);
        return result;
    }

    public async Task<bool> DeletePhone(int id)
    {
        _customLogger.Log($"DeletePhone(int? {id}); was called.");
        var phone = await _phoneRepository.Get(id);
        bool result = await _phoneRepository.Remove(phone);
        return result;
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
    #endregion
    // TODO
    // private bool ValidateProduct(IValidatable productToValidate)
    // {
    //     if (productToValidate.price.GetNumbers().Any(x => x < 0))
    //         _validationDictionary.AddError("Price", "Price cannot be less than zero.");
    //     if (productToValidate.storage.GetNumbers().Any(x => x < 0))
    //         _validationDictionary.AddError("Storage", "Storage cannot be less than zero.");
    //     return _validationDictionary.IsValid;
    // }
}
public interface IPhoneService
{
    Task<IPagedList<PhoneDto>> GetPaginatedPhones(int? page, string? searchString);
    Task<string> CreateFromSearch(string searchString);
    Task<PhoneDto?> GetPhoneById(int? id);
    Task<bool> AddPhone(PhoneDto phoneDto);
    Task<bool> EditPhone(int? id, PhoneDto phoneDto);
    Task<bool> DeletePhone(int id);
}