using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Options;
using TipoVkusvil.Core.Abstractions;
using TipoVkusvil.Core.Enums;
using TipoVkusvil.DataAccess.Entities;
using TipoVkusvil.DataAccess.Options;
using TipoVkusvil.Models;

namespace TipoVkusvil.DataAccess.Repositories;

public class OrderRepository : IOrderRepository
{
    private readonly ShopDbContext _shopDbContext;
    private readonly OrderOptions _options;
    
    public OrderRepository(ShopDbContext shopDbContext,IOptions<OrderOptions> options)
    {
        _shopDbContext = shopDbContext;
        _options = options.Value;
    }

    public async Task<Dictionary<Guid, List<CartItemFinished>>> ShowOrders(Guid userId)
    {
        Dictionary<Guid, List<CartItemFinished>> res = new Dictionary<Guid, List<CartItemFinished>>();
        
        List<Guid> orderIds = await _shopDbContext.Orders.Where(o => o.UserId == userId).Select(o => o.OrderId).ToListAsync();

        foreach (var orderId in orderIds)
        {
            List<Guid> cartId = await _shopDbContext.OrderCarts.Where(oc => oc.OrderId == orderId).Select(oc => oc.CartItemFinishedId).ToListAsync();

            List<CartItemFinishedEntity> cartItemsEntity =
                await _shopDbContext.CartItemsForOrders.Where(ci => cartId.Contains(ci.CartItemId)).AsNoTracking().ToListAsync();
        
            List<CartItemFinished> CartItems = (from cartItem in cartItemsEntity
                    join product in _shopDbContext.Products
                        on cartItem.ProductId equals product.ProductId
                    select CartItemFinished.Create(
                        cartItem.CartItemId,
                        cartItem.ProductId,
                        cartItem.Quantity,
                        product.ProductName,
                        product.Price,
                        product.Discount,
                        product.Mass,
                        product.ImgURL)
                ).Select(c => c.cartItem).ToList();

            res.Add(orderId, CartItems);
        }

        return res;
    }
    
    public async Task<short> CreateOrder(Guid userId)
    {

        if (!_options.IsAvailableToOrder)
        {
            return 2004;
        }
        
        short statusCode = 2000;
        bool IsCartEmpty = true;
        
        //Перевести в localtime
        OrderEntity orderEntity = new OrderEntity()
        {
            OrderId = Guid.NewGuid(),
            CreatedAt = DateTime.UtcNow.AddHours(3),
            Status = OrderStatus.Created,
            UserId = userId
        };

        Guid shopCartId = await _shopDbContext.ShopCarts.AsNoTracking()
            .Where(sc => sc.UserId == userId)
            .Select(sc => sc.Id).FirstOrDefaultAsync();

        List<CartItemEntity> cartItemEntities =
            await _shopDbContext.CartItems.Where(ci => ci.CartId == shopCartId).ToListAsync();
        
        foreach (var cartItemEntity in cartItemEntities)
        {
            var product = await _shopDbContext.Products.Where(p => p.ProductId == cartItemEntity.ProductId)
                .FirstOrDefaultAsync();

            int productQuantity = product.Quantity;

            Guid productid = product.ProductId;
            
            if (productQuantity == 0)
            {
                statusCode = 2001;
                _shopDbContext.CartItems.Remove(cartItemEntity);
                continue;
            }

            IsCartEmpty = false;
            
            if (cartItemEntity.Quantity > productQuantity )
            {
                /*await _shopDbContext.CartItems.Where(ci => ci.CartItemId == cartItemEntity.CartItemId)
                    .ExecuteUpdateAsync(s =>
                        s.SetProperty(ci => ci.Quantity, productQuantity));*/
                cartItemEntity.Quantity = productQuantity;
                statusCode = 2001;
            }
            
            CartItemFinishedEntity cartItemFinishedEntity = new CartItemFinishedEntity()
            {
                CartItemId = cartItemEntity.CartItemId,
                Product = cartItemEntity.Product,
                ProductId = cartItemEntity.ProductId,
                Quantity = cartItemEntity.Quantity
            };
            
            OrderCartEntity temp = new OrderCartEntity()
            {
                CartItem = cartItemFinishedEntity,
                CartItemFinishedId = cartItemFinishedEntity.CartItemId,
                Order = orderEntity,
                OrderId = orderEntity.OrderId
            };

            await _shopDbContext.OrderCarts.AddAsync(temp);

            
            
            await _shopDbContext.CartItemsForOrders.AddAsync(cartItemFinishedEntity);
            //await _shopDbContext.CartItems.Where(ci => ci.CartId == shopCartId).ExecuteUpdateAsync(s => s
             //   .SetProperty(c => c.CartId, c => Guid.Empty));
             
            //Замена для избежания конфликтов//
            
            /*await _shopDbContext.Products.Where(p => p.ProductId == productid).ExecuteUpdateAsync(s => s
                .SetProperty(p => p.Quantity, p => p.Quantity - cartItemEntity.Quantity));*/
            product.Quantity -= cartItemEntity.Quantity;
            
            
            /*await _shopDbContext.CartItems.Where(ci => ci.CartItemId == cartItemEntity.CartItemId)
                .ExecuteDeleteAsync();*/
            _shopDbContext.CartItems.Remove(cartItemEntity);
        }

        if (IsCartEmpty)
        {
            statusCode = 2002;
            return statusCode;
        }
        
        await _shopDbContext.Orders.AddAsync(orderEntity);
        
        await _shopDbContext.SaveChangesAsync();
        
        return statusCode;
    }
}