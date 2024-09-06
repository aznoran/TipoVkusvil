using Microsoft.Win32.SafeHandles;
using TipoVkusvil.Core.Abstractions;

namespace TipoVkusvil.Infrastructure;

public class PasswordHasher : IPasswordHasher
{
    public string Generate(string password)
        => BCrypt.Net.BCrypt.EnhancedHashPassword(password);

    public bool Verify(string password, string hashedPassword)
        => BCrypt.Net.BCrypt.EnhancedVerify(password, hashedPassword);
}