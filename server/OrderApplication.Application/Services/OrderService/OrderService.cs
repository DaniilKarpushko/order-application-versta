using System.Runtime.CompilerServices;
using Microsoft.Extensions.Logging;
using OrderApplication.Application.Services.OrderService.Requests;
using OrderApplication.Application.Services.OrderService.Responses;
using OrderApplication.Infrastructure.Models;
using OrderApplication.Infrastructure.Repositories;

namespace OrderApplication.Application.Services.OrderService;

public class OrderService : IOrderService
{
    private readonly IRepository<Order> _orderRepository;

    private readonly ILogger<OrderService> _logger;

    public OrderService(IRepository<Order> orderRepository, ILogger<OrderService> logger)
    {
        _orderRepository = orderRepository;
        _logger = logger;
    }

    public async Task<CreateOrderResponse> CreateOrderAsync(
        CreateOrderRequest request,
        CancellationToken cancellationToken)
    {
        try
        {
            var newOrder = new Order
            {
                Id = GenerateOrderId(),
                SenderCity = request.SenderCity,
                SenderAddress = request.SenderAddress,
                ReceiverCity = request.RecieverCity,
                ReceiverAddress = request.RecieverAddress,
                Weight = request.Weight,
                PickupDate = request.PickupDate,
                CreatedAt = DateTimeOffset.UtcNow
            };

            await _orderRepository.AddAsync(newOrder, cancellationToken);

            return new CreateOrderResponse.Success(newOrder.Id);
        }
        catch (Exception e)
        {
            _logger.Log(LogLevel.Error, e.Message);
            
            return new CreateOrderResponse.Failure(500, e.Message);
        }
    }

    public async Task<GetOrderResponse> GetOrderAsync(Guid orderId, CancellationToken cancellationToken)
    {
        try
        {
            var order = await _orderRepository.GetByIdAsync(orderId, cancellationToken);

            if (order is not null)
                return new GetOrderResponse.Success(order);
            
            _logger.Log(LogLevel.Warning, $"Order does not exists: {orderId.ToString()}");
                
            return new GetOrderResponse.Failure(404, "Order does not exists");

        }
        catch (Exception e)
        {
            _logger.Log(LogLevel.Error, e, "Failed to get order");
            
            return new GetOrderResponse.Failure(500, e.Message);
        }
    }

    public async IAsyncEnumerable<Order> GetOrdersAsync(
        int limit,
        int page,
        [EnumeratorCancellation] CancellationToken cancellationToken)
    {
        IAsyncEnumerable<Order> source;

        try
        {
            source = _orderRepository.GetEntitiesAsync(limit, page, cancellationToken);
        }
        catch (Exception e)
        {
            _logger.Log(LogLevel.Error, e, "Failed to get orders");
            
            yield break;
        }

        await foreach (var order in source.WithCancellation(cancellationToken))
        {
            yield return order;
        }
    }

    private Guid GenerateOrderId() => Guid.NewGuid();
}