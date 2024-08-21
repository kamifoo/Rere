namespace Rere.Core.Repositories;

public interface IRepository<T>
{
    Task<IEnumerable<T>> ListAsync();
    Task<T?> GetByIdOrNullAsync(int id);
    Task<IEnumerable<T>> SearchAsync(SearchQuery<T> query);
    Task<int> AddAsync(T entity);
    Task UpdateAsync(T entity);
    Task DeleteAsync(T entity);
}