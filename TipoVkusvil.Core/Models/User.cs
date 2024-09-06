namespace TipoVkusvil.Models;

public class User
{
    private User(Guid id, string userName, string passwordHash, string email)
    {
        Id = id;
        UserName = userName;
        PasswordHash = passwordHash;
        Email = email;
    }

    public Guid Id { get; }

    public string UserName { get; }

    public string PasswordHash { get; }

    public string Email { get; }

    public static (User user, string Error) Create(Guid id, string userName, string passwordHash, string email)
    {
        var user = new User(id, userName, passwordHash, email);

        var error = "";

        return (user, error);
    }
}