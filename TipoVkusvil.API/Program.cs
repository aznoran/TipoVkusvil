using ClassLibrary1.Exceptions;
using ClassLibrary1.Services;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Options;
using TipoVkusvil.API.Endpoints;
using TipoVkusvil.Core.Abstractions;
using TipoVkusvil.Core.Enums;
using TipoVkusvil.DataAccess;
using TipoVkusvil.DataAccess.Entities;
using TipoVkusvil.DataAccess.Options;
using TipoVkusvil.DataAccess.Repositories;
using TipoVkusvil.Extensions;
using TipoVkusvil.Infrastructure;
using TipoVkusvil.Models;
using AuthorizationOptions = TipoVkusvil.DataAccess.AuthorizationOptions;

var builder = WebApplication.CreateBuilder(args);

var configuration = builder.Configuration;

builder.Services.AddApiAuthentication(configuration);

builder.Services.Configure<JwtOptions>(builder.Configuration.GetSection(nameof(JwtOptions)));
builder.Services.Configure<OrderOptions>(builder.Configuration.GetSection(nameof(OrderOptions)));
builder.Services.Configure<AuthorizationOptions>(builder.Configuration.GetSection(nameof(AuthorizationOptions)));
builder.Services.AddSingleton(resolver => resolver.GetRequiredService<IOptions<AuthorizationOptions>>().Value);
builder.Services.AddSingleton<IAuthorizationHandler, PermissionAuthorizationHandler>();


builder.Services.AddSwaggerGen();
builder.Services.AddEndpointsApiExplorer();
builder.Services.AddDbContext<ShopDbContext>();


#region Добавление сервисов

builder.Services.AddScoped<IProductRepository, ProductRepository>();
builder.Services.AddScoped<IBaseRepository<Category>, CategoryRepository>();
builder.Services.AddScoped<IUsersRepository, UsersRepository>();
builder.Services.AddScoped<IProductCategoryRepository, ProductCategoryRepository>();
builder.Services.AddScoped<IShopCartRepository, ShopCartRepository>();
builder.Services.AddScoped<IOrderRepository, OrderRepository>();

builder.Services.AddScoped<IBaseService<Category>, CategoryService>();
builder.Services.AddScoped<IProductService, ProductService>();

builder.Services.AddScoped<IBaseAdminService<Product>, ProductAdminService>();
builder.Services.AddScoped<IBaseAdminService<Category>, CategoryAdminService>();


builder.Services.AddScoped<IUsersService, UsersService>();
builder.Services.AddScoped<IUsersAdminService, UsersAdminService>();
builder.Services.AddScoped<IShopCartService, ShopCartService>();
builder.Services.AddScoped<IOrderService, OrderService>();

builder.Services.AddScoped<IPasswordHasher, PasswordHasher>();
builder.Services.AddScoped<IJwtProvider, JwtProvider>();

#endregion

var app = builder.Build();


if (app.Environment.IsDevelopment())
{
    app.UseSwagger();
    app.UseSwaggerUI(c =>
    {
        c.SwaggerEndpoint("/swagger/v1/swagger.json", "My API V1");
        c.RoutePrefix = string.Empty;
    });
}
else 
{
    app.UseSwagger();
    app.UseSwaggerUI();
}

//app.UseExceptionHandler("/Error");

app.UseHttpsRedirection();

app.UseAuthentication();

app.UseAuthorization();

app.AddMappedEndpoints();

app.Run();
