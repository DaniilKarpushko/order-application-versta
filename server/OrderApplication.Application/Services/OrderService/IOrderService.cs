using OrderApplication.Application.Services.OrderService.Requests;
using OrderApplication.Application.Services.OrderService.Responses;
using OrderApplication.Infrastructure.Models;

namespace OrderApplication.Application.Services.OrderService;

public interface IOrderService
{
    Task<CreateOrderResponse> CreateOrderAsync(CreateOrderRequest request, CancellationToken cancellationToken);
    
    Task<GetOrderResponse> GetOrderAsync(Guid orderId, CancellationToken cancellationToken);

    IAsyncEnumerable<Order> GetOrdersAsync(int limit, int page, CancellationToken cancellationToken);
}