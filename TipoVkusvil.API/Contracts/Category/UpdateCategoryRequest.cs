using System.ComponentModel.DataAnnotations;

namespace TipoVkusvil.Contracts;

public record UpdateCategoryRequest(
    string CategoryName,
    string UpperCategoryName,
    string ImgURL);