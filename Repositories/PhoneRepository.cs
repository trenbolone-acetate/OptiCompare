using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using OptiCompare.Data;
using OptiCompare.Models;
using OptiCompare.PhoneSpecs;

namespace OptiCompare.Repositories;

public class PhoneRepository : Repository<Phone>
{
    private readonly Func<string, string> _normalizeSearchString = searchString => searchString.Replace(" ", "%20");
    public PhoneRepository(OptiCompareDbContext context) : base(context)
    {
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
    public override IEnumerable<Phone?> GetAll()
    {
        return GetEntities()
            .AsNoTracking();
    }
    public override async Task<Phone?> Get(int id)
    {
        return await GetEntities()
            .FirstOrDefaultAsync(e => e.Id == id);
    }
    //Eager-Loading
    private IQueryable<Phone> GetEntities()
    {
        return _entities
            .Include(phone => phone.batteryDetails)
            .Include(phone => phone.bodyDimensions)
            .Include(phone => phone.cameraDetails)
            .Include(phone => phone.displayDetails)
            .Include(phone => phone.platformDetails);
    }
}