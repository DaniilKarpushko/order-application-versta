using System.ComponentModel.DataAnnotations;

namespace OrderApplication.Api.Dtos;

public class CreateOrderDto
{
    [Required]
    [MaxLength(200)]
    [MinLength(1)]
    public string SenderCity { get; set; } = null!;

    [Required]
    [MaxLength(200)]
    [MinLength(1)]
    public string SenderAddress { get; set; } = null!;

    [Required]
    [MaxLength(200)]
    [MinLength(1)]
    public string ReceiverCity { get; set; } = null!;

    [Required]
    [MaxLength(200)]
    [MinLength(1)]
    public string ReceiverAddress { get; set; } = null!;
    
    [Required]
    [Range(0.01, 100000)]
    public double Weight { get; set; }
    
    [Required]
    public DateTimeOffset PickupDate { get; set; }
}