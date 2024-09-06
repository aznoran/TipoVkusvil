using TipoVkusvil.Core.Enums;

namespace TipoVkusvil.Core.Abstractions;

public interface IPermissionService
{
    public Task<HashSet<Permission>> GetPermissionAsync(Guid userId);
}