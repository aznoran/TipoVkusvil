using Microsoft.AspNetCore.Mvc;
using TipoVkusvil.Core.Abstractions;
using TipoVkusvil.Core.Enums;
using TipoVkusvil.Infrastructure;
using TipoVkusvil.Models;

namespace TipoVkusvil.API.Endpoints;

public static class UsersAdminEndpoints
{
    public static IEndpointRouteBuilder MapUsersAdminEndpoints(this IEndpointRouteBuilder app)
    {
        var endpoints = app.MapGroup("admin/users");
        
        endpoints.MapGet(string.Empty, GetAll).RequireAuthorization(policy => policy.AddRequirements(new PermissionRequirement(new []{Permission.ReadAdmin})));
        endpoints.MapDelete("/{id:guid}", Delete).RequireAuthorization(policy => policy.AddRequirements(new PermissionRequirement(new []{Permission.Delete})));

        return endpoints;
    }

    public static async Task<Guid> Delete(
        [FromRoute] Guid id, 
        [FromServices] IUsersAdminService usersAdminService, 
        HttpContext httpContext)
    {
        return await usersAdminService.Delete(id);
    }

    public static async Task<List<User>> GetAll(
        [FromServices] IUsersAdminService usersAdminService, 
        HttpContext httpContext)
    {
        return await usersAdminService.GetAll();
    }
}