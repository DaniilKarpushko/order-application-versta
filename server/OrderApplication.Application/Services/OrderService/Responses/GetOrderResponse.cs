using OrderApplication.Infrastructure.Models;

namespace OrderApplication.Application.Services.OrderService.Responses;

public record GetOrderResponse
{
    public sealed record Success(Order Order) : GetOrderResponse;
    
    public sealed record Failure(int Code, string Message) : GetOrderResponse;
}