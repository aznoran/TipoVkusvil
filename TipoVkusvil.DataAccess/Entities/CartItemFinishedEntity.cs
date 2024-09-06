namespace TipoVkusvil.DataAccess.Entities;

public class CartItemFinishedEntity
{
    public Guid CartItemId { get; set; }

    public Guid ProductId { get; set; }
    
    public ProductEntity Product { get; set; }

    public int Quantity { get; set; }
    
    public ICollection<OrderCartEntity> OrderCarts { get; set; }
}