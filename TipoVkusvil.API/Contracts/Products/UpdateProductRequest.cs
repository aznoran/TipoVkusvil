namespace TipoVkusvil.Contracts;

public record UpdateProductRequest(
    string ProductName,
    int Quantity,
    double Price,
    int Discount,
    string Description,
    string ShortDescription,
    double Mass,
    double Kkal,
    double Belki,
    double Jiri,
    double Uglevodi,
    int ShelfLife,
    string CondtionsLife,
    string CompanyName,
    string ImgURL);