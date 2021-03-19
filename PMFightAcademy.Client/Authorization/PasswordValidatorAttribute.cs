using System;
using System.ComponentModel.DataAnnotations;
using System.Text.RegularExpressions;

namespace PMFightAcademy.Client.Authorization
{
    public class PasswordValidatorAttribute : ValidationAttribute
    {
        public override bool IsValid(object input)
        {
            var password = input.ToString();

            if (string.IsNullOrEmpty(password))
                throw new ArgumentNullException(ErrorMessage = "Password can not be null");

            var hasNumber = new Regex(@"[0-9]+");
            var hasUpperChar = new Regex(@"[A-Z]+");
            var hasMinimum8Chars = new Regex(@".{6,64}");

            return hasNumber.IsMatch(password) && hasUpperChar.IsMatch(password) && hasMinimum8Chars.IsMatch(password);
        }
    }
}
