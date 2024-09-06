using Microsoft.AspNetCore.Mvc;
using TipoVkusvil.Contracts;
using TipoVkusvil.Core.Abstractions;
using TipoVkusvil.Core.Enums;
using TipoVkusvil.Infrastructure;
using TipoVkusvil.Models;



namespace TipoVkusvil.API.Endpoints;

public static class ProductsAdminEndpoints
{
    public static IEndpointRouteBuilder MapProductsAdminEndpoints(this IEndpointRouteBuilder app)
    {
        var endpoints = app.MapGroup("admin/products");

        endpoints.MapGet(string.Empty, GetProducts).RequireAuthorization(policy => policy.AddRequirements(new PermissionRequirement(new []{Permission.ReadAdmin})));
        endpoints.MapGet("/{id:guid}", GetProductById).RequireAuthorization(policy => policy.AddRequirements(new PermissionRequirement(new []{Permission.ReadAdmin})));
        endpoints.MapPost(string.Empty, CreateProduct).RequireAuthorization(policy => policy.AddRequirements(new PermissionRequirement(new []{Permission.Create})));
        endpoints.MapPut("/{id:guid}", UpdateProduct).RequireAuthorization(policy => policy.AddRequirements(new PermissionRequirement(new []{Permission.Update})));
        endpoints.MapDelete("/{id:guid}", DeleteProduct).RequireAuthorization(policy => policy.AddRequirements(new PermissionRequirement(new []{Permission.Delete})));
        
        return endpoints;
    }

    private static async Task<IList<Product>> GetProducts(
        HttpContext context,
        [FromServices] IBaseAdminService<Product> productsAdminService)
    {
        return await productsAdminService.GetAll("");
    }
    
    private static async Task<Product> GetProductById(
        HttpContext context,
        [FromRoute] Guid id,
        [FromServices] IBaseAdminService<Product> productsAdminService)
    {
        Console.WriteLine(context.User.Claims);
        
        return await productsAdminService.GetById(id);
    }
    
    private static async Task<IResult> CreateProduct(
        HttpContext context,
        [FromBody] CreateProductRequest createProductRequest,
        [FromServices] IBaseAdminService<Product> productsAdminService)
    {

        var productTemp = Product.Create(
            Guid.NewGuid(),
            createProductRequest.ProductName,
            createProductRequest.Quantity,
            createProductRequest.Price,
            createProductRequest.Discount,
            createProductRequest.Description,
            createProductRequest.ShortDescription,
            createProductRequest.Mass,
            createProductRequest.Kkal,
            createProductRequest.Belki,
            createProductRequest.Jiri,
            createProductRequest.Uglevodi,
            createProductRequest.ShelfLife,
            createProductRequest.CondtionsLife,
            createProductRequest.CompanyName,
            createProductRequest.ImgURL
            );

        if (productTemp.Error != string.Empty)
        {
            return Results.Conflict(error: productTemp.Error);
        }
        
        await productsAdminService.Create(productTemp.product);

        return Results.Ok();
    }
    
    private static async Task<IResult> UpdateProduct(
        HttpContext context,
        [FromRoute] Guid id,
        [FromBody] UpdateProductRequest updateProductRequest,
        [FromServices] IBaseAdminService<Product> productsAdminService)
    {

        var productTemp = Product.Create(
            id,
            updateProductRequest.ProductName,
            updateProductRequest.Quantity,
            updateProductRequest.Price,
            updateProductRequest.Discount,
            updateProductRequest.Description,
            updateProductRequest.ShortDescription,
            updateProductRequest.Mass,
            updateProductRequest.Kkal,
            updateProductRequest.Belki,
            updateProductRequest.Jiri,
            updateProductRequest.Uglevodi,
            updateProductRequest.ShelfLife,
            updateProductRequest.CondtionsLife,
            updateProductRequest.CompanyName,
            updateProductRequest.ImgURL
        );

        if (productTemp.Error != string.Empty)
        {
            return Results.Conflict(error: productTemp.Error);
        }
        
        await productsAdminService.Update(productTemp.product);

        return Results.Ok();
    }
    
    private static async Task<IResult> DeleteProduct(
        HttpContext context,
        [FromRoute] Guid Id,
        [FromServices] IBaseAdminService<Product> productsAdminService)
    {
        await productsAdminService.Delete(Id);

        return Results.Ok();
    }

}