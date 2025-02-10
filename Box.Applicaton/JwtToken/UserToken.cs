using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Box.Applicaton.JwtToken
{
    /// <summary>
    /// Информация с токена о пользователе
    /// </summary>
    public class UserToken
    {
        /// <summary>
        /// Идентификатор пользователя
        /// </summary>
        public string IdUser {  get; set; }
        /// <summary>
        /// Роль
        /// </summary>
        public string Role { get; set; }
    }
}
