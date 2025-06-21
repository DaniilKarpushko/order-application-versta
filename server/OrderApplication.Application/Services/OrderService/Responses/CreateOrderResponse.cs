namespace OrderApplication.Application.Services.OrderService.Responses;

public record CreateOrderResponse
{
    public sealed record Success(Guid OrderId) : CreateOrderResponse;
    public sealed record Failure(int Code, string Message) : CreateOrderResponse;
};