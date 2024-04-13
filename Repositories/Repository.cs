using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using OptiCompare.Data;
using OptiCompare.Models;

namespace OptiCompare.Repositories;

public class Repository<T> : IRepository<T> where T : class
{
    protected readonly OptiCompareDbContext _context;
    protected DbSet<T> _entities;

    protected Repository(OptiCompareDbContext context)
    {
        _context = context;
        _entities = context.Set<T>();
    }
    public virtual async Task<T?> Get(int id)
    {
        return await _entities.FindAsync(id);
    }
    public virtual IEnumerable<T?> GetAll()
    {
        return _entities.AsNoTracking();
    }

    public async Task Add(T? entity)
    {
        await _entities.AddAsync(entity);
        await _context.SaveChangesAsync();
    }

    public async Task Update(T? entity)
    {
        _entities.Update(entity);
        await _context.SaveChangesAsync();
    }

    public async Task Remove(T obj)
    {
        _entities.Remove(obj);
        await _context.SaveChangesAsync();
    }
}