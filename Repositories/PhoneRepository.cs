using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using OptiCompare.Data;
using OptiCompare.Models;
using OptiCompare.PhoneSpecs;

namespace OptiCompare.Repositories;

public class PhoneRepository : Repository<Phone>
{
    public PhoneRepository(OptiCompareDbContext context) : base(context)
    {
        
    }
    public async Task<Phone?> CreateFromSearch(string searchString)
    {
        searchString = searchString.Replace(" ", "%20");
        Phone? newPhone = await PhoneDetailsFetcher.CreateFromSearch(searchString);
    
        if (newPhone == null)
        {
            Console.WriteLine("No phone found with your search");
            throw new Exception("1");
        }
    
        bool phoneExists = GetEntities().Any(p => p.modelName == newPhone.modelName);
        if(phoneExists)
        {
            Console.WriteLine("Such phone already exists!");
            throw new Exception("2");
        }
    
        await Add(newPhone);
    
        return newPhone;
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