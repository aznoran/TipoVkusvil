using System.ComponentModel.DataAnnotations;

namespace TipoVkusvil.Contracts;

public record CreateCategoryRequest([Required] string CategoryName, [Required] string UpperCategoryName, [Required] string ImgURL);