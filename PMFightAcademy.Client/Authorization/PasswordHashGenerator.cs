using System;
using System.Text;
using Microsoft.AspNetCore.Cryptography.KeyDerivation;

namespace PMFightAcademy.Client.Authorization
{
    /// <summary>
    /// Static class with extension method
    /// </summary>
    public static class PasswordHashGenerator
    {
        /// <summary>
        /// Method for generate hash for password (HMACSHA256)
        /// </summary>
        /// <param name="password"></param>
        /// <returns></returns>
        public static string GenerateHash(this string password)
        {
            var salt = Encoding.ASCII.GetBytes("AJSD21i1AJFafka");
            
            var hashed = Convert.ToBase64String(KeyDerivation.Pbkdf2(
                password: password,
                salt: salt,
                prf: KeyDerivationPrf.HMACSHA256/*.HMACSHA1*/,
                iterationCount: 10000,
                numBytesRequested: 256 / 8));

            return hashed;
        }
    }
}
