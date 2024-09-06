using TipoVkusvil.Core.Enums;
using TipoVkusvil.Models;

namespace TipoVkusvil.Core.Abstractions;

public interface IUsersRepository
{
    public Task Add(User user);

    public Task<User> GetByEmail(string email);

    //public Task Create(User user);

    public Task<Guid> Delete(Guid id);

    public Task<List<User>> GetAll(string filter = "");

    public Task<HashSet<Permission>> GetUserPermissions(Guid userId);
}