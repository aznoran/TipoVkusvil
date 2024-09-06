using TipoVkusvil.Models;

namespace TipoVkusvil.Core.Abstractions;

public interface IOrderRepository
{
    public Task<Dictionary<Guid, List<CartItemFinished>>> ShowOrders(Guid userId);

    public Task<short> CreateOrder(Guid userId);
}