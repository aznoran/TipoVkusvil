using Microsoft.AspNetCore.Mvc;
using TipoVkusvil.Core.Abstractions;
using TipoVkusvil.Models;
using TipoVkusvil.Core.Abstractions.ConstVars;
using TipoVkusvil.Core.Enums;
using TipoVkusvil.Infrastructure;

namespace TipoVkusvil.API.Endpoints;

public static class ShopCartEndpoints
{
    public static IEndpointRouteBuilder MapShopCartEndpoints(this IEndpointRouteBuilder app)
    {
        var endpoints = app.MapGroup("shopcart");
        
        endpoints.MapGet(string.Empty, GetShopCart).RequireAuthorization(policy => policy.AddRequirements(new PermissionRequirement(new []{Permission.ShopCartUser})));
        endpoints.MapPost("/{id:guid}", AddItemToShopCart).RequireAuthorization(policy => policy.AddRequirements(new PermissionRequirement(new []{Permission.ShopCartUser})));
        endpoints.MapDelete("/{id:guid}", RemoveItemFromShopCart).RequireAuthorization(policy => policy.AddRequirements(new PermissionRequirement(new []{Permission.ShopCartUser})));
        
        return endpoints;
    }
    
    public static async Task<short> AddItemToShopCart(
        HttpContext context,
        [FromRoute] Guid id,
        [FromServices] IShopCartService shopCartService)
    {
        return await shopCartService.AddItemToShopCart(GetTokenFromContext(context), id);
    }

    public static async Task<short> RemoveItemFromShopCart(
        HttpContext context,
        [FromRoute] Guid id,
        [FromServices] IShopCartService shopCartService)
    {
        return await shopCartService.RemoveItemFromShopCart(GetTokenFromContext(context), id);
    }
    
    private static async Task<List<CartItem>> GetShopCart(
        HttpContext context,
        [FromServices] IShopCartService shopCartService)
    {
        
        return await shopCartService.ShowShopCart(GetTokenFromContext(context));
    }

    private static string GetTokenFromContext(HttpContext httpContext)
    {
        httpContext.Request.Cookies.TryGetValue(ConstVars.JWT_COOKIE_NAME, out string JWT_token);

        return JWT_token;
    }
}