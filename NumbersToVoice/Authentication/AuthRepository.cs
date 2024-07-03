using System.IdentityModel.Tokens.Jwt;
using System.Security.Claims;
using System.Security.Cryptography;
using System.Text;
using Microsoft.Extensions.Options;
using Microsoft.IdentityModel.Tokens;
using NumbersToVoice.Entities;

namespace NumbersToVoice.Authentication;

public class AuthRepository : IAuthRepository
{
    private readonly JwtOptions _jwtOptions;

    public AuthRepository(IOptions<JwtOptions> jwtOptions)
    {
        _jwtOptions = jwtOptions.Value;
    }

    public string HashPassword(string password)
    {
        using (var sha256 = SHA256.Create())
        {
            byte[] bytes = sha256.ComputeHash(Encoding.UTF8.GetBytes(password));
            return Convert.ToBase64String(bytes);
        }
    }

    public bool VerifyPassword(string hashedPassword, string password)
    {
        string hashOfInput = HashPassword(password);
        return StringComparer.OrdinalIgnoreCase.Compare(hashedPassword, hashOfInput) == 0;
    }

    public string GenerateJwtToken(User user)
    {
        
        var claims = new Claim[]
        {
            new Claim(JwtRegisteredClaimNames.Name, user.nameUser),
            new Claim(JwtRegisteredClaimNames.Email, user.emailUser),
        };

        var signingKey = new SymmetricSecurityKey(Encoding.UTF8.GetBytes(_jwtOptions.SecretKey));

        var sigingCredentials = new SigningCredentials(signingKey, SecurityAlgorithms.HmacSha256);

        var token = new JwtSecurityToken(
            _jwtOptions.Issuer,
            _jwtOptions.Audience,
            claims: claims,
            expires: DateTime.Now.AddHours(5),
            signingCredentials: sigingCredentials
        );
        try
        {
            var tk = new JwtSecurityTokenHandler().WriteToken(token);
            return tk;
        }catch(Exception e)
        {
            return e.Message;
        }
        
    }
}