using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace OrderApplication.Infrastructure.Models;

[Table("orders")]
public class Order
{
    [Key]
    [Column("id")]
    public Guid Id { get; set; }

    [Required]
    [Column("created_at")]
    public DateTimeOffset CreatedAt { get; set; }
    
    [Required]
    [Column("pickup_date")]
    public DateTimeOffset PickupDate { get; set; }
    
    [Required]
    [Range(0.001, 10000)]
    [Column("weight")]
    public double Weight { get; set; }
    
    [Required]
    [MaxLength(200)]
    [MinLength(1)]
    [Column("sender_city")]
    public string SenderCity { get; set; } = null!;
    
    [Required]
    [MaxLength(200)]
    [MinLength(1)]
    [Column("sender_address")]
    public string SenderAddress { get; set; } = null!;
    
    [Required]
    [MaxLength(200)]
    [MinLength(1)]
    [Column("receiver_city")]
    public string ReceiverCity { get; set; } = null!;
    
    [Required]
    [MaxLength(200)]
    [MinLength(1)]
    [Column("receiver_address")]
    public string ReceiverAddress { get; set; } = null!;

    [Column("is_delivered")]
    public bool IsDelivered { get; set; }
}
