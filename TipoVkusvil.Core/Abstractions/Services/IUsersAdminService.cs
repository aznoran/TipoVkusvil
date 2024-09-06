using TipoVkusvil.Models;

namespace TipoVkusvil.Core.Abstractions;

public interface IUsersAdminService
{
    //public Task Create(User user);

    //public Task<Guid> Update(User category);

    public Task<Guid> Delete(Guid id);
    
    public Task<List<User>> GetAll(string filter = "");
}