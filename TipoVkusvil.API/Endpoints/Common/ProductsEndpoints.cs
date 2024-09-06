using ClassLibrary1.Services;
using Microsoft.AspNetCore.Http.HttpResults;
using Microsoft.AspNetCore.Mvc;
using TipoVkusvil.Contracts;
using TipoVkusvil.Core.Abstractions;
using TipoVkusvil.Models;

//using Permission = LearningPlatform.Core.Enums.Permission;

namespace TipoVkusvil.API.Endpoints;

public static class ProductsEndpoints
{
    public static IEndpointRouteBuilder MapProductsEndpoints(this IEndpointRouteBuilder app)
    {
        var endpoints = app.MapGroup("products");

        endpoints.MapGet(string.Empty, GetProducts);
        endpoints.MapGet("/{id:guid}", GetProductById);
        endpoints.MapGet("/category/{id:guid}", GetByCategoryId);

        return endpoints;
    }

    private static async Task<IList<Product>> GetProducts(
        HttpContext context,
        [FromServices] IProductService productsService)
    {
        return await productsService.GetAll("");
    }

    public static async Task<IList<Product>> GetByCategoryId(
        HttpContext context,
        [FromServices] IProductService productsService,
        [FromRoute] Guid id)
    {
        return await productsService.GetByCategoryId(id);
    }

    private static async Task<Product> GetProductById(
        HttpContext context,
        [FromRoute] Guid id,
        [FromServices] IProductService productsService)
    {
        return await productsService.GetById(id);
    }
}