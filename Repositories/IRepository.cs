namespace OptiCompare.Repositories;

public interface IRepository<T> where T : class
{
    Task<T?> Get(int id);
    IEnumerable<T?> GetAll();
    Task Add(T? entity);
    Task Update(T? entity);
    Task Remove(T obj);
}