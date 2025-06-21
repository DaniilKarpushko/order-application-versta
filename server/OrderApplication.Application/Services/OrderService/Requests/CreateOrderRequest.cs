namespace OrderApplication.Application.Services.OrderService.Requests;

public record CreateOrderRequest(
    string SenderCity,
    string SenderAddress,
    string RecieverCity,
    string RecieverAddress,
    double Weight,
    DateTimeOffset PickupDate);