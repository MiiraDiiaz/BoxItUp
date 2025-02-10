using Auth.Applicaton.Interfaces;
using Microsoft.Extensions.Options;
using Microsoft.IdentityModel.Tokens;
using System.IdentityModel.Tokens.Jwt;
using System.Security.Claims;
using System.Text;

namespace Auth.Applicaton.Authentication.JwtToken
{
    using Auth.Domain.Models;
    /// <inheritdoc cref="IJwtProvider"/>
    public class JwtProvider(IOptions<JwtOptions> options) : IJwtProvider
    {
        private readonly JwtOptions _options = options.Value;

        /// <inheritdoc/>
        public string GenerateToken(User user)
        {
            var claims = new List<Claim>()
            {
                new(ClaimTypes.NameIdentifier, user.Id.ToString()!),
                new(ClaimTypes.Role, user.Role)
            };

            var signingCredentials = new SigningCredentials(
                new SymmetricSecurityKey(Encoding.UTF8.GetBytes(_options.SecurityKey)),
                SecurityAlgorithms.HmacSha256);

            var token = new JwtSecurityToken(
                claims: claims,
                issuer: _options.Issuer,
                audience: _options.Audience,
                expires: DateTime.UtcNow.AddHours(6),
                notBefore: DateTime.UtcNow,
                signingCredentials: signingCredentials
               );

            var tokenValue = new JwtSecurityTokenHandler().WriteToken(token);

            return tokenValue;
        }
    }
}
