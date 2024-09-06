namespace TipoVkusvil.DataAccess.Entities;

public class OrderCartEntity
{
    public Guid OrderId { get; set; }

    public OrderEntity Order { get; set; }
    
    public Guid CartItemFinishedId { get; set; }
    
    public CartItemFinishedEntity CartItem { get; set; }
}