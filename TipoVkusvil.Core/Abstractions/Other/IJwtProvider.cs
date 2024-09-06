using System.Security.Claims;
using TipoVkusvil.Models;

namespace TipoVkusvil.Core.Abstractions;

public interface IJwtProvider
{
    public Claim[] ReadToken(string jwtToken);
    public string GenerateToken(User user);
}