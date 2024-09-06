namespace TipoVkusvil.Core.Abstractions;

public interface IUsersService
{
    public Task Register(string userName, string email, string password);

    public Task<string> Login(string email, string password);
}