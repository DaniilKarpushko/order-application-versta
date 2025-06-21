using Microsoft.EntityFrameworkCore;
using OrderApplication.Infrastructure.Models;

namespace OrderApplication.Infrastructure.Repositories;

public class OrderContext : DbContext
{
    public OrderContext(DbContextOptions<OrderContext> options)
        : base(options)
    {
    }

    public DbSet<Order> Orders { get; set; }
}