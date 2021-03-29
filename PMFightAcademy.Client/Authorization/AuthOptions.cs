using System.Text;
using Microsoft.IdentityModel.Tokens;

namespace PMFightAcademy.Client.Authorization
{
    /// <summary>
    /// Jwt Options for server settings 
    /// </summary>
    public class AuthOptions
    {
        /// <summary>
        /// Token issuer (producer).
        /// </summary>
        public const string Issuer = "MyAuthServer";

        /// <summary>
        /// Token audience (consumer).
        /// </summary>
        public const string Audience = "MyAuthClient";

        /// <summary>
        /// Token secret part.
        /// </summary>
        const string Key = "mysupersecret_secretkey!123";

        /// <summary>
        /// Token life time.
        /// </summary>
        public const int Lifetime = 7; 

#pragma warning disable 1591
        public static SymmetricSecurityKey GetSymmetricSecurityKey()
        {
            return new SymmetricSecurityKey(Encoding.ASCII.GetBytes(Key));
        }
#pragma warning restore 1591
    }
}
