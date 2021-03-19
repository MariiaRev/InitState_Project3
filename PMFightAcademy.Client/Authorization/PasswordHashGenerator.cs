using System;
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
            var hashed = Convert.ToBase64String(KeyDerivation.Pbkdf2(
                password: password,
                salt: new byte[128 / 8],
                prf: KeyDerivationPrf.HMACSHA256/*.HMACSHA1*/,
                iterationCount: 10000,
                numBytesRequested: 256 / 8));

            return hashed;
        }
    }
}
