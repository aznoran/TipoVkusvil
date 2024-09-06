namespace TipoVkusvil.Core.Abstractions;

public interface IPasswordHasher
{
    string Generate(string password);

    public bool Verify(string password, string hashedPassword);
}