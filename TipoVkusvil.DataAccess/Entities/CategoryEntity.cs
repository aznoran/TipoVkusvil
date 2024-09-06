using TipoVkusvil.Models;

namespace TipoVkusvil.DataAccess.Entities;

public class CategoryEntity
{
    public Guid CategoryId { get; set; }
    public string CategoryName { get; set; }
    
    public string UpperCategoryName { get; set; }
    
    public string ImgURL { get; set; }
    
    public List<ProductEntity> Products { get; set; } = new List<ProductEntity>();

    public List<ProductCategoryEntity> ProductCategories { get; set; } = new List<ProductCategoryEntity>();
}