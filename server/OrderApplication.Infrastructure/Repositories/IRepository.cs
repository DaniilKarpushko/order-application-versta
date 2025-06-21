namespace OrderApplication.Infrastructure.Repositories;

public interface IRepository<T>
{
    Task<T?> GetByIdAsync(Guid id, CancellationToken cancellationToken);
    
    Task<T> AddAsync(T entity, CancellationToken cancellationToken);
    
    Task<T> UpdateAsync(T entity, CancellationToken cancellationToken);
    
    IAsyncEnumerable<T> GetEntitiesAsync(int limit, int page, CancellationToken cancellationToken);
    
    Task DeleteByIdAsync(Guid id, CancellationToken cancellationToken);
}