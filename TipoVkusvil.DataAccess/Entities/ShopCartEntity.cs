namespace TipoVkusvil.DataAccess.Entities;

public class ShopCartEntity
{
    public Guid Id { get; set; }

    public Guid UserId { get; set; }
    
    public DateTime CreatedDate { get; set; }
    
    public ICollection<CartItemEntity> CartItems { get; set; } = new List<CartItemEntity>();
    
}
