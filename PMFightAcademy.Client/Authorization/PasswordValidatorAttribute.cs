using System;
using System.ComponentModel.DataAnnotations;
using System.Text.RegularExpressions;

namespace PMFightAcademy.Client.Authorization
{
    /// <summary>
    /// Validator for password
    /// </summary>
    /// <remarks>
    /// Password must have at least 8 chars
    /// At least 1 upper char
    /// and at least 1 number
    /// </remarks> 
    public class PasswordValidatorAttribute : ValidationAttribute
    {
#pragma warning disable CS1591 
        public override bool IsValid(object input)
        {
            var password = input.ToString();

            if (string.IsNullOrEmpty(password))
                throw new ArgumentNullException(ErrorMessage = "Password can not be null");

            var hasNumber = new Regex(@"[0-9]+");
            var hasUpperChar = new Regex(@"[A-Z]+");
            var hasMinimum8Chars = new Regex(@".{8,64}");

            return hasNumber.IsMatch(password) && hasUpperChar.IsMatch(password) && hasMinimum8Chars.IsMatch(password);
        }
#pragma warning restore CS1591 
    }
}
