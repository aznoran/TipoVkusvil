namespace TipoVkusvil.DataAccess.Entities;

public class ProductCategoryEntity
{
    public Guid ProductId { get; set; }
    
    public ProductEntity Product { get; set; }

    public Guid CategoryId { get; set; }
    
    public CategoryEntity Category { get; set; }
}