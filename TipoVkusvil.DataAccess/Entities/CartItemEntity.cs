namespace TipoVkusvil.DataAccess.Entities;

public class CartItemEntity
{
    public Guid CartItemId { get; set; }
    
    public Guid CartId { get; set; }
    
    public ShopCartEntity Cart { get; set; }

    public Guid ProductId { get; set; }
    
    public ProductEntity Product { get; set; }

    public int Quantity { get; set; }
}
