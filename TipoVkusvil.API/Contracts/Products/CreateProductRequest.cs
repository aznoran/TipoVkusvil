using System.ComponentModel.DataAnnotations;

namespace TipoVkusvil.Contracts;

public record CreateProductRequest(
         [Required] string ProductName,
         [Required] int Quantity,
         [Required] double Price,
         int Discount,
         [Required] string Description,
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