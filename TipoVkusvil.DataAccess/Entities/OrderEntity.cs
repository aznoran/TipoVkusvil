using TipoVkusvil.Core.Enums;

namespace TipoVkusvil.DataAccess.Entities;

public class OrderEntity
{
    public Guid OrderId { get; set; }
    
    public Guid UserId { get; set; }
    
    public DateTime CreatedAt { get; set; }
    
    public OrderStatus Status { get; set; }
    
    public ICollection<OrderCartEntity> OrderCarts { get; set; }
}