using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Logging;
using Moq;
using OrderApplication.Application.Services.OrderService;
using OrderApplication.Application.Services.OrderService.Requests;
using OrderApplication.Application.Services.OrderService.Responses;
using OrderApplication.Infrastructure.Models;
using OrderApplication.Infrastructure.Repositories;

namespace OrderApplication.Tests;

public class OrderServiceTests
{
    private readonly IOrderService _orderService;

    public OrderServiceTests()
    {
        var options = new DbContextOptionsBuilder<OrderContext>()
            .UseInMemoryDatabase(databaseName: "TestDatabase")
            .Options;
        
        var context = new OrderContext(options);

        var loggerMock = new Mock<ILogger<OrderService>>();

        IRepository<Order> orderRepository = new OrderRepository(context);
        _orderService = new OrderService(orderRepository, loggerMock.Object);
    }
    
    [Fact]
    async Task OrderService_CreateOrderAsync_ShouldReturnSuccess()
    {
        var CreateOrderRequest = new CreateOrderRequest(
            "sCity",
            "sAddr",
            "rCity",
            "rAddr",
            1,
            DateTimeOffset.UtcNow);
        
        var response = await _orderService.CreateOrderAsync(CreateOrderRequest, CancellationToken.None);
        
        Assert.IsType<CreateOrderResponse.Success>(response);
    }
    
    [Fact]
    async Task OrderService_GetOrderAsync_ShouldReturnFailure()
    {
        var orderId = Guid.NewGuid();
        
        var response = await _orderService.GetOrderAsync(orderId, CancellationToken.None);

        const int expectedCode = 404;
        
        Assert.IsType<GetOrderResponse.Failure>(response);
        Assert.Equal(expectedCode, ((GetOrderResponse.Failure)response).Code);
    }
}