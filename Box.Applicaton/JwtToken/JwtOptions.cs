namespace Box.Applicaton.JwtToken
{
    /// <summary>
    /// Опции для настройки JWT токенов.
    /// </summary>
    public class JwtOptions
    {
        /// <summary>
        /// Ключ безопасности для подписи токенов.
        /// </summary>
        public string SecurityKey { get; set; } = String.Empty;

        /// <summary>
        /// Издатель токена.
        /// </summary>
        public string Issuer { get; set; } = String.Empty;

        /// <summary>
        /// Аудитория токена, для которой он предназначен.
        /// </summary>
        public string Audience { get; set; } = String.Empty;
    }
}
