using System;
using System.ComponentModel.DataAnnotations;
using System.Linq;

namespace PMFightAcademy.Client.Authorization
{
    public class PhoneValidatorAttribute : ValidationAttribute
    {
        public override bool IsValid(object input)
        {
            var phone = input.ToString();

            if (string.IsNullOrEmpty(phone))
                throw new ArgumentNullException(ErrorMessage = "Phone can not be null");

            if (phone.Length == 13)
            {
                if (!phone.StartsWith("+38"))
                    return false;
                phone = String.Concat(phone.Skip(3));
            }

            if (phone.Length != 10)
                return false;

            var countryCodes = new[]
            {
                "039", "067", "068", "096", "097", "098", "050", "066", "095", "099", "063", "093", "091", "092", "094"
            };

            return countryCodes.Any(x => x.Equals(String.Concat(phone.Take(3))));
        }
    }
}
