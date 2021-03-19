using System;
using Microsoft.AspNetCore.Cryptography.KeyDerivation;

namespace PMFightAcademy.Client.Authorization
{
    public static class PasswordHashGenerator
    {
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
