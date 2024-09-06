namespace TipoVkusvil.Models;

public class Product
{
    private Product(Guid productId,
        string productName,
        int quantity,
        double price,
        int discount,
        string description,
        string shortDescription,
        double mass,
        double kkal,
        double belki,
        double jiri,
        double uglevodi,
        int shelfLife,
        string condtionsLife,
        string companyName,
        string imgURL)
    {
        ProductId = productId;
        ProductName = productName;
        Quantity = quantity;
        Price = price;
        Discount = discount;
        Description = description;
        ShortDescription = shortDescription;
        Mass = mass;
        Kkal = kkal;
        Belki = belki;
        Jiri = jiri;
        Uglevodi = uglevodi;
        ShelfLife = shelfLife;
        CondtionsLife = condtionsLife;
        CompanyName = companyName;
        ImgURL = imgURL;
    }
    

    public Guid ProductId { get; }

    public string ProductName{ get; }

    public int Quantity { get; } = default;

    public double Price{ get; }

    public int Discount{ get; } = default;

    public string Description{ get; } 

    public string ShortDescription{ get; }

    public double Mass{ get; }= default;

    public double Kkal{ get; }= default;

    public double Belki{ get; }= default;

    public double Jiri{ get; }= default;

    public double Uglevodi{ get; }= default;

    public int ShelfLife{ get; }

    public string CondtionsLife{ get; }

    public string CompanyName{ get; }= default;
    
    public string ImgURL { get;}

    public static (Product product, string Error) Create(Guid productId,
        string productName = "",
        int quantity = 0,
        double price = 0,
        int discount = 0,
        string description = "",
        string shortDescription = "",
        double mass = 0,
        double kkal = 0,
        double belki = 0,
        double jiri = 0,
        double uglevodi = 0,
        int shelfLife = 0,
        string condtionsLife = "",
        string companyName = "",
        string imgURL = "")

    {
        var error = string.Empty;

        //Proverka

        var product = new Product(productId, productName, quantity, price, discount, description,
            shortDescription, mass, kkal, belki, jiri, uglevodi, shelfLife, condtionsLife, companyName, imgURL);

        return (product, error);
    }
}