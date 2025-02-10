using BCrypt.Net;

namespace Auth.Applicaton.Additionally
{
    /// <summary>
    /// Класс для шифрования паролей пользователей.
    /// </summary>
    public static class EncriptPasswordUser
    {
        /// <summary>
        /// Шифрует пароль с использованием алгоритма BCrypt.
        /// </summary>
        /// <param name="password">Пароль, который необходимо зашифровать.</param>
        /// <returns>Зашифрованный пароль.</returns>
        public static string EncryptPassword(string password) =>
            BCrypt.Net.BCrypt.HashPassword(password);

        /// <summary>
        /// Проверяет, соответствует ли введенный пароль зашифрованному паролю.
        /// </summary>
        /// <param name="password">Пароль, который необходимо проверить.</param>
        /// <param name="hashedPassword">Зашифрованный пароль для сравнения.</param>
        /// <returns>True, если пароль соответствует, иначе False.</returns>
        public static bool Verify(string password, string hashedPassword) =>
            BCrypt.Net.BCrypt.EnhancedVerify(password, hashedPassword);
    }

}
