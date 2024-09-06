using System.Text;
using ClassLibrary1.Services;
using Microsoft.AspNetCore.Authentication.JwtBearer;
using Microsoft.AspNetCore.Authorization;
using Microsoft.Extensions.Options;
using Microsoft.IdentityModel.Tokens;
using TipoVkusvil.API.Endpoints;
using TipoVkusvil.Core.Abstractions;
using TipoVkusvil.Core.Abstractions.ConstVars;
using TipoVkusvil.Core.Enums;
using TipoVkusvil.Infrastructure;

namespace TipoVkusvil.Extensions;

public static class ApiExtensions
{
    public static void AddMappedEndpoints(this IEndpointRouteBuilder app)
    {
        app.MapProductsEndpoints();
        app.MapProductsAdminEndpoints();
        app.MapCategoriesEndpoints();
        app.MapCategoriesAdminEndpoints();
        app.MapUsersEndpoints();
        app.MapUsersAdminEndpoints();
        app.MapShopCartEndpoints();
        app.MapOrdersEndpoints();
    }

    public static void AddApiAuthentication(this IServiceCollection services, IConfiguration configuration)
    {
        var jwtOptions = configuration.GetSection(nameof(JwtOptions)).Get<JwtOptions>();
        
        services.AddAuthentication(JwtBearerDefaults.AuthenticationScheme)
            .AddJwtBearer(
            JwtBearerDefaults.AuthenticationScheme, options =>
            {
                options.TokenValidationParameters = new()
                {
                    ValidateIssuer = false,
                    ValidateAudience = false,
                    ValidateLifetime = true,
                    ValidateIssuerSigningKey = true,
                    IssuerSigningKey = new SymmetricSecurityKey(Encoding.UTF8.GetBytes(jwtOptions.SecretKey))
                };

                options.Events = new JwtBearerEvents()
                {
                    OnMessageReceived = context =>
                    {
                        context.Token = context.Request.Cookies[ConstVars.JWT_COOKIE_NAME];

                        return Task.CompletedTask;
                    }
                };
            })
            ;
        services.AddScoped<IPermissionService, PermissionService>();

        services.AddSingleton<IAuthorizationHandler, PermissionAuthorizationHandler>();  
            
        services.AddAuthorization();
    }

    public static IEndpointConventionBuilder RequirePermissions<TBuilder>(
        this TBuilder builder, params Permission[] permissions)
        where TBuilder : IEndpointConventionBuilder
    {
        return builder.RequireAuthorization(policy => policy.AddRequirements(new PermissionRequirement(permissions)));
    }
}