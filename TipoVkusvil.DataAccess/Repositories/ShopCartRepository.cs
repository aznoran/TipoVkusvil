using Microsoft.EntityFrameworkCore;
using TipoVkusvil.Core.Abstractions;
using TipoVkusvil.DataAccess.Entities;
using TipoVkusvil.Models;

namespace TipoVkusvil.DataAccess.Repositories;

public class ShopCartRepository : IShopCartRepository
{
    private readonly ShopDbContext _shopDbContext;

    public ShopCartRepository(ShopDbContext shopDbContext)
    {
        _shopDbContext = shopDbContext;
    }


    public async Task<short> AddItemToShopCart(Guid userId, Guid productId)
    {
        short statusCode = 1000;
        
        var tempin = await _shopDbContext.ShopCarts.Where(sc => sc.UserId == userId).FirstOrDefaultAsync();

        Guid cartId = tempin.Id;

        var product = await _shopDbContext.Products.FirstOrDefaultAsync(p => p.ProductId == productId);

        if (product is null)
        {
            throw new Exception(
                "Product has null reference (Не знаю возможно ли это вообще?)");
        }
        
        if (product.Quantity == 0)
        {
            statusCode = 1004;
            return statusCode;
        }
            //sdelano !
            //1) Проверить когда последний товар
            //2) Проверить когда товара больше нет
            //3) Проверить когда кол-во товара изменилось и в корзине больше чем есть на самом деле(изменить кол-во)
        if (_shopDbContext.CartItems.Where(ci => ci.CartId == cartId).Where(ci => ci.ProductId == productId).Any())
        {
            var quantityOfProductInCart = await _shopDbContext.CartItems.Where(ci => ci.CartId == cartId).Where(ci => ci.ProductId == productId).FirstOrDefaultAsync();

            
            if ((quantityOfProductInCart.Quantity+1) == product.Quantity)
            {
                statusCode = 1001;
                await _shopDbContext.CartItems.Where(ci => ci.CartId == cartId).Where(ci => ci.ProductId == productId).ExecuteUpdateAsync(s =>
                    s.SetProperty(ci => ci.Quantity, ci => ci.Quantity+1));
            }
            else if (quantityOfProductInCart.Quantity >= product.Quantity)
            {
                statusCode = 1002;
                await _shopDbContext.CartItems.Where(ci => ci.CartId == cartId).Where(ci => ci.ProductId == productId).ExecuteUpdateAsync(s =>
                    s.SetProperty(ci => ci.Quantity, product.Quantity));
            }
            else
            {
                await _shopDbContext.CartItems.Where(ci => ci.CartId == cartId).Where(ci => ci.ProductId == productId).ExecuteUpdateAsync(s =>
                    s.SetProperty(ci => ci.Quantity, ci => ci.Quantity+1));
            }
            
        }
        else
        {
            CartItemEntity cartItemEntity = new CartItemEntity()
            {
                CartItemId = Guid.NewGuid(),
                CartId = cartId,
                Cart = tempin,
                ProductId = productId,
                Product = product,
                Quantity = 1
            };

            await _shopDbContext.CartItems.AddAsync(cartItemEntity);
            
        }
        
        await _shopDbContext.SaveChangesAsync();
        
        return statusCode;
    }

    public async Task<short> RemoveItemFromShopCart(Guid userId, Guid productId)
    {
        short statusCode = 1100;
        
        
        var tempin = await _shopDbContext.ShopCarts.Where(sc => sc.UserId == userId).FirstOrDefaultAsync();

        Guid cartId = tempin.Id;
        
        var product = await _shopDbContext.Products.FirstOrDefaultAsync(p => p.ProductId == productId);
        
        if (product is null)
        {
            throw new Exception(
                "Product has null reference (Не знаю возможно ли это вообще?)");
        }

        if (product.Quantity == 0)
        {
            statusCode = 1104;
            return statusCode;  
        }
        
        if (_shopDbContext.CartItems.Where(ci => ci.CartId == cartId).Where(ci => ci.ProductId == productId).Any())
        {
            var quantityOfProductInCart = await _shopDbContext.CartItems.Where(ci => ci.CartId == cartId).Where(ci => ci.ProductId == productId).FirstOrDefaultAsync();

            if (quantityOfProductInCart is null)
            {
                throw new Exception(
                    "Quantity of products in carts have null reference (Не знаю возможно ли это вообще?)");
            }

            if ((quantityOfProductInCart.Quantity - 1) > product.Quantity)
            {
                statusCode = 1102;
                await _shopDbContext.CartItems.Where(ci => ci.CartId == cartId).Where(ci => ci.ProductId == productId)
                    .ExecuteUpdateAsync(s=>s
                        .SetProperty(b => b.Quantity, product.Quantity));
            }
            else if (quantityOfProductInCart.Quantity == 1)
            {
                statusCode = 1101;
                await _shopDbContext.CartItems.Where(ci => ci.CartId == cartId).Where(ci => ci.ProductId == productId)
                    .ExecuteDeleteAsync();
            }
            else
            {
                await _shopDbContext.CartItems.Where(ci => ci.CartId == cartId).Where(ci => ci.ProductId == productId)
                    .ExecuteUpdateAsync(s=>s
                        .SetProperty(b => b.Quantity, b => b.Quantity-1));
            }
            await _shopDbContext.SaveChangesAsync();
        }
        else
        {
            statusCode = 1103;
        }

        return statusCode;
    }
    public async Task<List<CartItem>> ShowShopCart(Guid userId)
    {
        var tempin = await _shopDbContext.ShopCarts.Where(sc => sc.UserId == userId).FirstOrDefaultAsync();

        Guid cartId = tempin.Id;
        
        var cartItemsEntity= await _shopDbContext.CartItems.Where(ci => ci.CartId==cartId).AsNoTracking().ToListAsync();

        List<CartItem> cartItems = new List<CartItem>();
        
        foreach (var cartItemEntity in cartItemsEntity)
        {
            var product = await _shopDbContext.Products.Where(p => p.ProductId == cartItemEntity.ProductId)
                .FirstOrDefaultAsync();

            if (product.Quantity == 0)
            {
                continue;
            }

            if (cartItemEntity.Quantity > product.Quantity)
            {
                await _shopDbContext.CartItems.Where(ci => ci.CartItemId == cartItemEntity.CartItemId)
                    .ExecuteUpdateAsync(s =>
                        s.SetProperty(ci => ci.Quantity, product.Quantity));
                cartItemEntity.Quantity = product.Quantity;
                await _shopDbContext.SaveChangesAsync();
            }
            
            CartItem cartItem = CartItem.Create(cartItemEntity.CartItemId,
                cartItemEntity.CartId,
                cartItemEntity.ProductId,
                cartItemEntity.Quantity,
                product.ProductName,
                product.Price,
                product.Discount,
                product.Mass,
                product.ImgURL).cartItem;
            
            cartItems.Add(cartItem);
        }

        return cartItems;

        /*return (from cartItem in cartItemsEntity
                join product in _shopDbContext.Products
                    on cartItem.ProductId equals product.ProductId
                select CartItem.Create(
                    cartItem.CartItemId,
                    cartItem.CartId,
                    cartItem.ProductId,
                    cartItem.Quantity,
                    product.ProductName,
                    product.Price,
                    product.Discount,
                    product.Mass,
                    product.ImgURL)
            ).Select(c => c.cartItem).ToList();*/

        //return cartItemsEntity.Select(s => CartItem.Create(s.CartItemId,s.CartId,s.ProductId,s.Quantity).cartItem).ToList();

    }
    
    //public async Task AddItemToShopCart()
}
