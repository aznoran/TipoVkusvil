using TipoVkusvil.Models;

namespace TipoVkusvil.Core.Abstractions;

public interface IShopCartRepository
{
    public Task<short> AddItemToShopCart(Guid userId, Guid productId);
    public Task<short> RemoveItemFromShopCart(Guid userId, Guid productId);
    public Task<List<CartItem>> ShowShopCart(Guid userId);
}