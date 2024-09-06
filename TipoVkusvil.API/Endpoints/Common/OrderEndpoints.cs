using Microsoft.AspNetCore.Mvc;
using TipoVkusvil.Core.Abstractions;
using TipoVkusvil.Core.Abstractions.ConstVars;
using TipoVkusvil.Core.Enums;
using TipoVkusvil.DataAccess.Entities;
using TipoVkusvil.Infrastructure;
using TipoVkusvil.Models;

namespace TipoVkusvil.API.Endpoints;

public static class OrderEndpoints
{
    public static IEndpointRouteBuilder MapOrdersEndpoints(this IEndpointRouteBuilder app)
    {
        var endpoints = app.MapGroup("order");

        endpoints.MapGet(string.Empty, ShowOrders).RequireAuthorization(policy => policy.AddRequirements(new PermissionRequirement(new []{Permission.OrderUser})));
        endpoints.MapPost(string.Empty, CreateOrder).RequireAuthorization(policy => policy.AddRequirements(new PermissionRequirement(new []{Permission.OrderUser})));
        
        return endpoints;
    }

    private static async Task<Dictionary<Guid, List<CartItemFinished>>> ShowOrders(
        HttpContext context,
        [FromServices] IOrderService productsService)
    {
        return await productsService.ShowOrders(GetTokenFromContext(context));
    }
    
    //TODO: добавить orderRequest в который будет входить 1)комментарий к заказу 2)адрес доставки  3)способ оплаты
    private static async Task<short> CreateOrder(
        HttpContext context,
        [FromServices] IOrderService productsService)
    {
        return await productsService.CreateOrder(GetTokenFromContext(context));
    }
    
    private static string GetTokenFromContext(HttpContext httpContext)
    {
        httpContext.Request.Cookies.TryGetValue(ConstVars.JWT_COOKIE_NAME, out string JWT_token);

        return JWT_token;
    }
}