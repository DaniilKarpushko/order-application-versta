using System.Runtime.CompilerServices;
using Microsoft.AspNetCore.Mvc;
using OrderApplication.Api.Dtos;
using OrderApplication.Api.Extensions;
using OrderApplication.Application.Services.OrderService;
using OrderApplication.Application.Services.OrderService.Responses;

namespace OrderApplication.Api.Controllers;

[ApiController]
[Route("api/orders")]
public class OrdersController : ControllerBase
{
    private readonly IOrderService _orderService;

    public OrdersController(IOrderService orderService)
    {
        _orderService = orderService;
    }

    [HttpGet]
    [Route("{orderId}")]
    public async Task<IActionResult> GetOrderAsync([FromRoute]Guid orderId, CancellationToken cancellationToken)
    {
        var response = await _orderService.GetOrderAsync(orderId, cancellationToken);

        return response switch
        {
            GetOrderResponse.Failure failure => StatusCode(failure.Code, failure.Message),
            GetOrderResponse.Success success => Ok(success.Order.ToDto()),
            _ => throw new ArgumentOutOfRangeException(nameof(response))
        };
    }

    [HttpGet]
    public async IAsyncEnumerable<OrderDto> GetOrdersAsync(
        [FromQuery] int limit,
        [FromQuery] int page,
        [EnumeratorCancellation] CancellationToken cancellationToken)
    {
        await foreach (var order in _orderService.GetOrdersAsync(limit, page, cancellationToken))
            yield return order.ToDto();
    }
    
    [HttpPost]
    public async Task<IActionResult> CreateOrderAsync(
        [FromBody] CreateOrderDto request,
        CancellationToken cancellationToken)
    {
        var createOrderRequest = request.ToModel();
        var response = await _orderService.CreateOrderAsync(createOrderRequest, cancellationToken);

        return response switch
        {
            CreateOrderResponse.Failure failure => StatusCode(failure.Code, failure.Message),
            CreateOrderResponse.Success success => Ok(success),
            _ => throw new ArgumentOutOfRangeException(nameof(response))
        };
    }
}