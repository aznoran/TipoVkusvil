namespace TipoVkusvil.Models;

public class ShopCart
{
    public Guid ShopCartId { get; }
    
    public Guid UserId { get; set; }
    
    public DateTime CreatedDate { get; set; }
}
