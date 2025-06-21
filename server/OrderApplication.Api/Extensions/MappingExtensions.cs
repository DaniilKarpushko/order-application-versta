using OrderApplication.Api.Dtos;
using OrderApplication.Application.Services.OrderService.Requests;
using OrderApplication.Infrastructure.Models;

namespace OrderApplication.Api.Extensions;

public static class MappingExtensions
{
    public static CreateOrderRequest ToModel(this CreateOrderDto dto) =>
        new(
            dto.SenderCity,
            dto.SenderAddress,
            dto.ReceiverCity,
            dto.ReceiverAddress,
            dto.Weight,
            dto.PickupDate);

    public static OrderDto ToDto(this Order model) =>
        new()
        {
            OrderId = model.Id,
            PickupDate = model.PickupDate,
            ReceiverAddress = model.ReceiverAddress,
            ReceiverCity = model.ReceiverCity,
            SenderAddress = model.SenderAddress,
            SenderCity = model.SenderCity,
            Weight = model.Weight,
            CreatedAt = model.CreatedAt,
        };
}