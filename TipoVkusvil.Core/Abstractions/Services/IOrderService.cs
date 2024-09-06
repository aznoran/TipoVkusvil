using TipoVkusvil.Models;

namespace TipoVkusvil.Core.Abstractions;

public interface IOrderService
{
    public Task<Dictionary<Guid, List<CartItemFinished>>> ShowOrders(string jwtToken);

    public Task<short> CreateOrder(string jwtToken);
}