namespace OrderApplication.Api.Dtos;

public class OrderDto
{
    public Guid OrderId { get; set; }
    
    public string SenderCity { get; set; } = null!;

    public string SenderAddress { get; set; } = null!;

    public string ReceiverCity { get; set; } = null!;

    public string ReceiverAddress { get; set; } = null!;
    
    public double Weight { get; set; }
 
    public DateTimeOffset PickupDate { get; set; }
    
    public DateTimeOffset CreatedAt { get; set; }
}