using TipoVkusvil.Core.Enums;

namespace TipoVkusvil.Models;

public class Order
{
    public Guid OrderId { get; }
    
    public Guid UserId { get; }
    
    public decimal TotalPrice { get; }
    
    public DateTime CreatedAt { get; }
    
    public OrderStatus Status { get; }
    
    public string ClientsComment { get; }
    
    public string Address { get; }
    
    public PaymentMethod PaymentMethod { get; }
}