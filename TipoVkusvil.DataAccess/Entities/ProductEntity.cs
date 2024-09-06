using TipoVkusvil.Models;

namespace TipoVkusvil.DataAccess.Entities;

public class ProductEntity
{
    public Guid ProductId { get; set;  }

    public string ProductName{ get; set;  }

    public int Quantity { get; set;  }

    public double Price{ get; set;  }

    public int Discount{ get; set;  }

    public string Description{ get; set;  }

    public string ShortDescription{ get; set;  }

    public double Mass{ get; set;  }

    public double Kkal{ get; set;  }

    public double Belki{ get; set;  }

    public double Jiri{ get; set;  }

    public double Uglevodi{ get; set;  }

    public int ShelfLife{ get; set;  }

    public string CondtionsLife{ get; set;  }

    public string CompanyName{ get; set;  }
    
    public string ImgURL { get; set; }

    public List<CategoryEntity> Categories { get; set; } = new List<CategoryEntity>();
    public List<ProductCategoryEntity> ProductCategories { get; set; } = new List<ProductCategoryEntity>();
    public ICollection<CartItemFinishedEntity> CartItems { get; set; }
    
}