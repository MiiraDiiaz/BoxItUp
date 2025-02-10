using Box.Applicaton.BackgroundServices.Data;
using Box.Applicaton.Interface;
using Microsoft.Extensions.Logging;
using Microsoft.Extensions.Options;
using Microsoft.IdentityModel.Tokens;
using System.IdentityModel.Tokens.Jwt;
using System.Security.Claims;
using System.Text;

namespace Box.Applicaton.JwtToken
{
    /// <inheritdoc cref="IJwtProvider"/>
    public class JwtProvider : IJwtProvider
    {
        private readonly JwtOptions _options;
        private readonly ILogger<JwtProvider> _logger;

        /// <summary>
        /// Конструктор для инициализации JwtProvider.
        /// </summary>
        /// <param name="options">Настройки JWT.</param>
        /// <param name="logger">Логгер для записи ошибок.</param>
        public JwtProvider(IOptions<JwtOptions> options,
                    ILogger<JwtProvider> logger)
        {
            _options = options.Value;
            _logger = logger;
        }

        /// <inheritdoc/>
        public void DecodeToken(string token)
        {
            var tokenHandler = new JwtSecurityTokenHandler();
            var key = Encoding.UTF8.GetBytes(_options.SecurityKey);

            try
            {
                var validationParameters = new TokenValidationParameters
                {
                    ValidateIssuerSigningKey = true,
                    IssuerSigningKey = new SymmetricSecurityKey(key),
                    ValidIssuer = _options.Issuer,
                    ValidateIssuer = true,
                    ValidAudience = _options.Audience,
                    ValidateAudience = true,
                    ClockSkew = TimeSpan.Zero
                };

                var principal = tokenHandler.ValidateToken(token, validationParameters, out var validatedToken);
                var jwtToken = (JwtSecurityToken)validatedToken;

                UserContext.UserId = jwtToken.Claims.FirstOrDefault(c => c.Type == ClaimTypes.NameIdentifier)?.Value ?? "defaultId";
                UserContext.Role = jwtToken.Claims.FirstOrDefault(static c => c.Type == ClaimTypes.Role)?.Value ?? "defaultRole";

            }
            catch (SecurityTokenException ex)
            {
                _logger.LogError($"Ошибка декодирования токена: {ex.Message}");
                throw;
            }
        }
    }
}
