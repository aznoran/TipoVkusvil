using TipoVkusvil.Core.Abstractions;
using TipoVkusvil.Models;

namespace ClassLibrary1.Services;

public class ShopCartService : IShopCartService
{
    private readonly IShopCartRepository _repository;
    private readonly IJwtProvider _jwtProvider;
    
    public ShopCartService(IShopCartRepository repository, IJwtProvider jwtProvider)
    {
        _repository = repository;
        _jwtProvider = jwtProvider;
    }


    public async Task<short> AddItemToShopCart(string jwtToken, Guid productId)
    {
        return await _repository.AddItemToShopCart(GetUserIdFromClaim(jwtToken), productId);
    }

    public async Task<short> RemoveItemFromShopCart(string jwtToken, Guid productId)
    {
        return await _repository.RemoveItemFromShopCart(GetUserIdFromClaim(jwtToken), productId);
    }
    public async Task<List<CartItem>> ShowShopCart(string jwtToken)
    {
        return await _repository.ShowShopCart(GetUserIdFromClaim(jwtToken));
    }

    private Guid GetUserIdFromClaim(string jwtToken)
    {
        var claims = _jwtProvider.ReadToken(jwtToken);

        var userId = claims.FirstOrDefault(c => c.Type == "userId").Value;

        if (!Guid.TryParse(userId, out var result))
        {
            throw new Exception();
        }

        return result;
    }
}