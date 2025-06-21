using System.Runtime.CompilerServices;
using Microsoft.EntityFrameworkCore;
using OrderApplication.Infrastructure.Models;

namespace OrderApplication.Infrastructure.Repositories;

public class OrderRepository : IRepository<Order>
{
    private readonly OrderContext _context;

    public OrderRepository(OrderContext context)
    {
        _context = context;
    }

    public async Task<Order?> GetByIdAsync(Guid id, CancellationToken cancellationToken)
    {
        var order = await _context.Orders.FindAsync([id], cancellationToken: cancellationToken);

        return order;
    }

    public async Task<Order> AddAsync(Order entity, CancellationToken cancellationToken)
    {
        _context.Orders.Add(entity);
        await _context.SaveChangesAsync(cancellationToken);
        
        return entity;
    }

    public async Task<Order> UpdateAsync(Order entity, CancellationToken cancellationToken)
    {
        _context.Orders.Update(entity);
        await _context.SaveChangesAsync(cancellationToken);
        
        return entity;
    }

    public async IAsyncEnumerable<Order> GetEntitiesAsync(
        int limit,
        int page,
        [EnumeratorCancellation] CancellationToken cancellationToken)
    {
        var query = _context.Orders
            .OrderByDescending(o => o.CreatedAt)
            .AsNoTracking()
            .Skip(page * limit)
            .Take(limit);

        await foreach (var order in query.AsAsyncEnumerable().WithCancellation(cancellationToken))
            yield return order;
    }

    public async Task DeleteByIdAsync(Guid id, CancellationToken cancellationToken)
    {
        var order = await _context.Orders.FindAsync([id],  cancellationToken: cancellationToken);
        if (order != null)
        {
            _context.Orders.Remove(order);
            await _context.SaveChangesAsync(cancellationToken);
        }
    }
}