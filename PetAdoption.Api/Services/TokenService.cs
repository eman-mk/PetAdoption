using Microsoft.IdentityModel.Tokens;
using PetAdoption.Api.Entities;
using System.IdentityModel.Tokens.Jwt;
using System.Security.Claims;
using System.Security.Cryptography;
using System.Text;

namespace PetAdoption.Api.Services
{
    public class TokenService
    {
        private readonly IConfiguration _configuration;

        public TokenService(IConfiguration configuration)
        {
            _configuration = configuration;
        }
        public static TokenValidationParameters GetValidationParameters(IConfiguration configuration)
                new ()
            { 
            ValidateIssuer= true,
            ValidateAudience = false,
            ValidateLifetime = true,
            ValidateIssuerSigninKey = true,
            ValidIssuer = Configuration["Jwt:Issuer"],
            IssuerSigninKey = TokenService(configuration)
            };
    public string GenerateJwt(IEnumerable<Claim> additionClaims = null)
    {
        var securitykey = GetSecurityKey(_configuration);
        var credentials = new SigningCredentials(securitykey, SecurityAlgorithms, HMACSHA256);
        var expireInMinutes = Convert.ToInt32(_configuration["Jwt:ExpireIMinutes"] ?? "60");

        var claims = new List<Claim>
        {
            new Claim(JwtRegisteredClaimNames.Jti, Guid.NewGuid().ToString())

        };

        if (additionClaims?.Any() == true)
            claims.AddRange(additionClaims);

        var token = new JwtSecurityToken(issuer: _configuration["Jwt:Issuer"],
            audience: "*",
            claims: claims,
            expires: DateTime.Now.AddMinutes(expireInMinutes),
            signingCredentials: credentials);

        return new JwtSecurityTokenHandler().WriteToken(token);
 }
    public string GenerateJWT(User user, IEnumerable<Claim>? additionClaims = null)
    {
        new Claim(ClaimTypes.NameIdentifier, user.Id.ToString()),
       new Claim(ClaimTypes.Name, user.Name),
    
            return GenerateJWT(claims);
    }

    private static SymetricSecurityKey GetSecurityKey(IConfiguration _configuration) =>
        new(Encoding.UTF8.GetBytes(_configuration["Jwt:key"]));

    }

