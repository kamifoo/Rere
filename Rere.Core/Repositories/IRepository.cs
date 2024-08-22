namespace Rere.Core.Repositories;

public interface IRepository<T>
{
    /// <summary>
    /// List all entities
    /// </summary>
    /// <returns>Entities requested</returns>
    Task<IEnumerable<T>> ListAsync();

    /// <summary>
    /// Check if an entity existed
    /// </summary>
    /// <param name="id">ID of that entity</param>
    /// <returns>true: existed; false: NOT existed</returns>
    Task<bool> ExistsAsync(int id);

    /// <summary>
    /// Get an entity by its ID or return null
    /// </summary>
    /// <param name="id">ID of that entity</param>
    /// <returns>Entity requested</returns>
    Task<T?> GetByIdOrNullAsync(int id);

    /// <summary>
    /// Search entities by search query
    /// </summary>
    /// <param name="query">Search query of that entity</param>
    /// <returns>Matched entities by the search query</returns>
    Task<IEnumerable<T>> SearchAsync(SearchQuery<T> query);

    /// <summary>
    /// Add entity to repo
    /// </summary>
    /// <param name="entity">The entity to be added</param>
    /// <returns>ID of that newly added entity</returns>
    Task<int> AddAsync(T entity);

    /// <summary>
    /// Update entity in repo
    /// </summary>
    /// <param name="entity">The entity to be updated</param>
    /// <returns></returns>
    Task UpdateAsync(T entity);

    /// <summary>
    /// Delete entity in repo
    /// </summary>
    /// <param name="id">ID of the entity to be deleted</param>
    /// <returns></returns>
    Task DeleteAsync(int id);
}