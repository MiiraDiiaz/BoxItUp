namespace Auth.Applicaton.Interfaces
{
    using Auth.Domain.Models;
    /// <summary>
    /// JwtProvider для генерации токена
    /// </summary>
    public interface IJwtProvider
    {
        /// <summary>
        /// Генерация токена
        /// </summary>
        /// <param name="user"></param>
        /// <returns>токен</returns>
        string GenerateToken(User user);
    }
}