namespace TipoVkusvil.Models;

public class CartItem
{
    private CartItem(
        Guid cartItemId, 
        Guid cartId, 
        Guid productId, 
        int quantity,
        string productName,
        double price,
        int discount,
        double mass,
        string imgURL)
    {
        CartItemId = cartItemId;
        CartId = cartId;
        ProductId = productId;
        Quantity = quantity;
        ProductName = productName;
        Price = price;
        Discount = discount;
        Mass = mass;
        ImgURL = imgURL;
    }
    
    public Guid CartItemId { get; set; }
    
    public Guid CartId { get; set; }

    public Guid ProductId { get; set; }

    public int Quantity { get; set; }

    public string ProductName{ get; }

    public double Price{ get; }

    public int Discount{ get; } = default;

    public double Mass{ get; }= default;
    
    public string ImgURL { get;}
    
    public static (CartItem cartItem, string Error) Create(
        Guid cartItemId, 
        Guid cartId, 
        Guid productId, 
        int quantity,
        string productName,
        double price,
        int discount,
        double mass,
        string imgURL)
    {
        var error = string.Empty;

        var cartItem = new CartItem(cartItemId, cartId, productId, quantity, productName, price, discount, mass, imgURL);

        return (cartItem, error);
    }
}
