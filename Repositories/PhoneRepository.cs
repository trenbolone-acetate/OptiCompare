using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using OptiCompare.Data;
using OptiCompare.Models;
using OptiCompare.PhoneSpecs;

namespace OptiCompare.Repositories;

public class PhoneRepository :  IRepository<Phone>
{
    private readonly OptiCompareDbContext _context;
    private readonly DbSet<Phone> _entities;
    private readonly Func<string, string> _normalizeSearchString = searchString => searchString.Replace(" ", "%20");
    public PhoneRepository(OptiCompareDbContext context)
    {
        _context = context;
        _entities = context.Set<Phone>();
    }
    public async Task CreateFromSearch(string searchString)
    {
        string normalizedSearchString = _normalizeSearchString(searchString);
        Phone? newPhone = await PhoneDetailsFetcher.CreateFromSearch(normalizedSearchString);

        if (newPhone is null)
        {
            Console.WriteLine($"No phone found with your search: {searchString}");
            throw new InvalidOperationException("1");
        }

        bool phoneExists = GetEntities().Any(p => p.modelName == newPhone.modelName);
        if(phoneExists)
        {
            Console.WriteLine($"Phone model {newPhone.modelName} already exists!");
            throw new InvalidOperationException("2");
        }
        await Add(newPhone);
    }
    public IEnumerable<Phone?> GetAll()
    {
        return GetEntities()
            .AsNoTracking();
    }
    public async Task<Phone?> Get(int? id)
    {
        return await GetEntities()
            .FirstOrDefaultAsync(e => e.Id == id);
    }
    //Eager-Loading
    public IQueryable<Phone> GetEntities()
    {
        return _entities
            .Include(phone => phone.batteryDetails)
            .Include(phone => phone.bodyDimensions)
            .Include(phone => phone.cameraDetails)
            .Include(phone => phone.displayDetails)
            .Include(phone => phone.platformDetails);
    }
    public async Task<bool> Add(Phone? entity)
    {
        await _entities.AddAsync(entity);
        var saveResult = await _context.SaveChangesAsync();
        return saveResult > 0; 
    }

    public async Task<bool> Update(Phone? entity)
    {
        _entities.Update(entity);
        var saveResult = await _context.SaveChangesAsync();
        return saveResult > 0; 
    }

    public async Task<bool> Remove(Phone obj)
    {
        _entities.Remove(obj);
        var saveResult = await _context.SaveChangesAsync();
        return saveResult > 0; 
    }
}