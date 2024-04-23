using OptiCompare.Models;

namespace OptiCompare.Repositories;

public interface IRepository<T> where T : class
{
    Task CreateFromSearch(string searchString);
    Task<T?> Get(int? id);
    IEnumerable<T?> GetAll();
    Task<bool> Add(T? entity);
    Task<bool> Update(T? entity);
    Task<bool> Remove(T obj);
    IQueryable<T> GetEntities();

}