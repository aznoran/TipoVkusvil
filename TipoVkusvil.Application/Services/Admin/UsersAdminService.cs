using TipoVkusvil.Core.Abstractions;
using TipoVkusvil.Models;

namespace ClassLibrary1.Services;


public class UsersAdminService : IUsersAdminService
{
    private readonly IUsersRepository _usersRepository;
    private readonly IPasswordHasher _passwordHasher;
    private readonly IJwtProvider _jwtProvider;
    
    public UsersAdminService(IPasswordHasher passwordHasher, IUsersRepository usersRepository, IJwtProvider jwtProvider)
    {
        _usersRepository = usersRepository;
        _passwordHasher = passwordHasher;
        _jwtProvider = jwtProvider;
    }


    /*public async Task Create(User user)
    {
        await _usersRepository.Create(user);
    }*/

    public async Task<Guid> Delete(Guid id)
    {
        return await _usersRepository.Delete(id);
    }

    public async Task<List<User>> GetAll(string filter = "")
    {
        return await _usersRepository.GetAll();
    }

}