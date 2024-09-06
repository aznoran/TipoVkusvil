namespace TipoVkusvil.Models;

public class Category
{
    private Category(Guid categoryId, string categoryName,string upperCategoryName, string imgURL)
    {
        CategoryId = categoryId;
        CategoryName = categoryName;
        UpperCategoryName = upperCategoryName;
        ImgURL = imgURL;
    }
    
    public Guid CategoryId { get; }
    public string CategoryName { get; }
    
    public string UpperCategoryName { get; }
    
    public string ImgURL { get; }

    // Navigation property
    public ICollection<ProductCategory> ProductCategories { get; set; }

    public static (Category category, string Error) Create(Guid categoryId, string categoryName, string upperCategoryName, string imgURL)
    {
        var error = string.Empty;
        
        //proverka

        var category = new Category(categoryId, categoryName, upperCategoryName, imgURL);

        return (category, error);
    }
}