using System.IdentityModel.Tokens.Jwt;
using System.Security.Claims;
using System.Text;
using Bookstore.Domain.Abstractions;
using Bookstore.Domain.Entites;
using Microsoft.Extensions.Configuration;
using Microsoft.IdentityModel.Tokens;


namespace Bookstore.Infrastructure;

public class JwtGenerateToken : IJwtTokenGenerator
{
    private const int EXPIRATION = 1;
    private readonly IConfiguration _config;
    private readonly SymmetricSecurityKey _key;

    public JwtGenerateToken(IConfiguration config)
    {
        _config = config;
        _key = new SymmetricSecurityKey(Encoding.UTF8.GetBytes(_config.GetSection("JwtSettings:SecretKey").Value 
                        ?? throw new InvalidOperationException("Jwt Secret is not configured")));
    }

    public string GenerateToken(Customer customer)
    {
        var claims = new List<Claim>
        {
            new Claim("customerId", customer.CustomerId),
            new Claim("firstname", customer.FirstName),
            new Claim("Role", "CUSTOMER"),
        };

        var creds = new SigningCredentials(_key, SecurityAlgorithms.HmacSha256Signature);

        var tokenDescriptor = new SecurityTokenDescriptor{
            Subject = new ClaimsIdentity(claims),
            Expires = DateTime.Now.AddMinutes(EXPIRATION),
            SigningCredentials = creds,
            Issuer = _config["JwtSettings:Issuer"],
            Audience = _config["JwtSettings:Audience"]
        };

        var tokenHandler = new JwtSecurityTokenHandler();
        var token = tokenHandler.CreateToken(tokenDescriptor);

        return tokenHandler.WriteToken(token);
    }

    
}
