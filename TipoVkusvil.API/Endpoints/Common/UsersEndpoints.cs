using ClassLibrary1.Services;
using Microsoft.AspNetCore.Mvc;
using TipoVkusvil.Contracts.Users;
using TipoVkusvil.Core.Abstractions;
using TipoVkusvil.Core.Abstractions.ConstVars;

namespace TipoVkusvil.API.Endpoints;

public static class UsersEndpoints
{
    public static IEndpointRouteBuilder MapUsersEndpoints(this IEndpointRouteBuilder app)
    {
        var endpoints = app.MapGroup("users");
        
        
        endpoints.MapPost("/sign-up", Register);
        endpoints.MapPost("/sign-in", Login);

        return endpoints;
    }

    private static async Task<IResult> Register([FromBody] RegisterUserRequest registerUserRequest, IUsersService usersService)
    {
        await usersService.Register(
            registerUserRequest.UserName,
            registerUserRequest.Email,
            registerUserRequest.Password);
        
        return Results.Ok();
    }
    
    private static async Task<IResult> Login([FromBody] LoginUserRequest loginUserRequest, IUsersService usersService, HttpContext httpContext)
    {
        var token = await usersService.Login(loginUserRequest.Email, loginUserRequest.Password);

        httpContext.Response.Cookies.Append(ConstVars.JWT_COOKIE_NAME, token);
        
        return Results.Ok(token);
    }
}