using Microsoft.AspNetCore.Mvc;
using TipoVkusvil.Contracts;
using TipoVkusvil.Core.Abstractions;
using TipoVkusvil.Core.Enums;
using TipoVkusvil.Infrastructure;
using TipoVkusvil.Models;

namespace TipoVkusvil.API.Endpoints;

public static class CategoryAdminEndpoints
{
    public static IEndpointRouteBuilder MapCategoriesAdminEndpoints(this IEndpointRouteBuilder app)
    {
        var endpoints = app.MapGroup("admin/categories");

        endpoints.MapGet(string.Empty, GetCategories).RequireAuthorization(policy => policy.AddRequirements(new PermissionRequirement(new []{Permission.ReadAdmin})));
        endpoints.MapGet("/{id:guid}", GetCategoryById).RequireAuthorization(policy => policy.AddRequirements(new PermissionRequirement(new []{Permission.ReadAdmin})));
        endpoints.MapPost(string.Empty, CreateCategory).RequireAuthorization(policy => policy.AddRequirements(new PermissionRequirement(new []{Permission.Create})));
        endpoints.MapPut("{id:guid}", UpdateCategory).RequireAuthorization(policy => policy.AddRequirements(new PermissionRequirement(new []{Permission.Update})));
        endpoints.MapDelete("/{id:guid}", DeleteCategory).RequireAuthorization(policy => policy.AddRequirements(new PermissionRequirement(new []{Permission.Delete})));
        
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
    private static async Task<IResult> CreateCategory(
        HttpContext context,
        [FromBody] CreateCategoryRequest createCategoryRequest,
        [FromServices] IBaseAdminService<Category> categoriesAdminService)
    {

        var categoryTemp = Category.Create(
            Guid.NewGuid(),
            createCategoryRequest.CategoryName,
            createCategoryRequest.UpperCategoryName,
            createCategoryRequest.ImgURL
            );

        if (categoryTemp.Error != string.Empty)
        {
            return Results.Conflict(error: categoryTemp.Error);
        }
        
        await categoriesAdminService.Create(categoryTemp.category);

        return Results.Ok();
    }
    
    private static async Task<IResult> UpdateCategory(
        HttpContext context,
        [FromRoute] Guid id,
        [FromBody] UpdateCategoryRequest updateCategoryRequest,
        [FromServices] IBaseAdminService<Category> categoriesAdminService)
    {

        var categoryTemp = Category.Create(
            id,
            updateCategoryRequest.CategoryName,
            updateCategoryRequest.UpperCategoryName,
            updateCategoryRequest.ImgURL
        );

        if (categoryTemp.Error != string.Empty)
        {
            return Results.Conflict(error: categoryTemp.Error);
        }
        
        await categoriesAdminService.Update(categoryTemp.category);

        return Results.Ok();
    }
    
    private static async Task<IResult> DeleteCategory(
        HttpContext context,
        [FromRoute] Guid Id,
        [FromServices] IBaseAdminService<Category> categoriesAdminService)
    {
        await categoriesAdminService.Delete(Id);

        return Results.Ok();
    }
}