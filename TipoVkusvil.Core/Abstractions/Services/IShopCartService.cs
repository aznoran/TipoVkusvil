using TipoVkusvil.Models;

namespace TipoVkusvil.Core.Abstractions;

public interface IShopCartService
{
    public Task<List<CartItem>> ShowShopCart(string jwtToken);

    public Task<short> AddItemToShopCart(string jwtToken, Guid productId);

    public Task<short> RemoveItemFromShopCart(string jwtToken, Guid productId);
}