using TCBApp.Models;

namespace TCBApp.Interface;

public interface IDataServiceBase<T> where T : ModelBase
{
    IQueryable<T> GetAll();
    Task<T?> GetByIdAsync(long id);

    Task<T> AddAsync(T entity);
    Task<T> UpdateAsync(T entity);
    Task<T> RemoveAsync(T entity);
    Task<T> RemoveAsync(long id);
}