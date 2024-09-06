using System.IdentityModel.Tokens.Jwt;
using System.Runtime.CompilerServices;
using System.Security.Claims;
using System.Text;
using Microsoft.Extensions.Options;
using Microsoft.IdentityModel.Tokens;
using TipoVkusvil.Core.Abstractions;
using TipoVkusvil.Models;

namespace TipoVkusvil.Infrastructure;

public class JwtProvider : IJwtProvider
{
    private readonly JwtOptions _options;

    public JwtProvider(IOptions<JwtOptions> options)
    {
        _options = options.Value;
    }


    public Claim[] ReadToken(string jwtToken)
    {
        var tokenHandler = new JwtSecurityTokenHandler();
        var jsonToken = tokenHandler.ReadToken(jwtToken) as JwtSecurityToken;

        if (jsonToken == null)
        {
            throw new ArgumentException("Invalid JWT Token");
        }

        return jsonToken.Claims.ToArray();
    }

    public string GenerateToken(User user )
    {
        Claim[] claims = new Claim[]
        {
            new("userId", user.Id.ToString()),
        };
        
       
        
        var signingCredentials = new SigningCredentials(
            new SymmetricSecurityKey(Encoding.UTF8.GetBytes(_options.SecretKey)), SecurityAlgorithms.HmacSha256);

        var token = new JwtSecurityToken(
            claims: claims,
            signingCredentials: signingCredentials,
            expires: DateTime.UtcNow.AddHours(_options.ExpiresHours)
            );

        var tokenValue = new JwtSecurityTokenHandler().WriteToken(token);

        return tokenValue;
    }
}