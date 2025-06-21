using Microsoft.Extensions.DependencyInjection;
using OrderApplication.Application.Services.OrderService;

namespace OrderApplication.Application.Extensions;

public static class ServiceCollectionExtensions
{
    public static IServiceCollection AddServices(this IServiceCollection sc)
    {
        sc.AddScoped<IOrderService, OrderService>();

        return sc;
    }
}