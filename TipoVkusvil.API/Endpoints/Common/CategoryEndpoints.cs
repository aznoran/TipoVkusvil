using Microsoft.AspNetCore.Mvc;
using TipoVkusvil.Core.Abstractions;
using TipoVkusvil.Models;

namespace TipoVkusvil.API.Endpoints;

public static class CategoryEndpoints
{
    public static IEndpointRouteBuilder MapCategoriesEndpoints(this IEndpointRouteBuilder app)
    {
        var endpoints = app.MapGroup("categories");

        endpoints.MapGet(string.Empty, GetCategories);
        endpoints.MapGet("{id:guid}", GetCategoryById);

        return endpoints;
    }

    private static async Task<IList<Category>> GetCategories(
        HttpContext context,
        [FromServices] IBaseAdminService<Category> categoriesAdminService)
    {
        return await categoriesAdminService.GetAll("");
    }

    private static async Task<Category> GetCategoryById(
        HttpContext context,
        [FromRoute] Guid id,
        [FromServices] IBaseAdminService<Category> categoriesAdminService)
    {
        return await categoriesAdminService.GetById(id);
    }
}