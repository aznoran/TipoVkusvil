using TipoVkusvil.Core.Abstractions;
using TipoVkusvil.Models;

namespace ClassLibrary1.Services;

public class OrderService : IOrderService
{
    private readonly IOrderRepository _repository;
    private readonly IJwtProvider _jwtProvider;
    
    public OrderService(IOrderRepository repository, IJwtProvider jwtProvider)
    {
        _repository = repository;
        _jwtProvider = jwtProvider;
    }

    public async Task<Dictionary<Guid, List<CartItemFinished>>> ShowOrders(string jwtToken)
    {
        return await _repository.ShowOrders(GetUserIdFromClaim(jwtToken));
    }

    public async Task<short> CreateOrder(string jwtToken)
    {
        return await _repository.CreateOrder(GetUserIdFromClaim(jwtToken));
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