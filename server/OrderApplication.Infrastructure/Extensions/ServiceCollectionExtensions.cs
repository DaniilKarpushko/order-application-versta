using FluentMigrator.Runner;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using OrderApplication.Infrastructure.Migrations;
using OrderApplication.Infrastructure.Models;
using OrderApplication.Infrastructure.Repositories;

namespace OrderApplication.Infrastructure.Extensions;

public static class ServiceCollectionExtensions
{
    public static IServiceCollection AddRepositories(this IServiceCollection sc, IConfiguration configuration)
    {
        sc.AddDbContextPool<OrderContext>(opt =>
            opt.UseNpgsql(
                configuration.GetConnectionString("OrderContext")));

        sc.AddScoped<IRepository<Order>, OrderRepository>();

        return sc;
    }

    public static IServiceCollection AddMigrations(this IServiceCollection sc, IConfiguration configuration)
    {
        sc
            .AddFluentMigratorCore()
            .ConfigureRunner(rb => rb
                .AddPostgres()
                .WithGlobalConnectionString(configuration.GetConnectionString("OrderContext"))
                .WithMigrationsIn(typeof(InitialMigration).Assembly));

        return sc;
    }
}