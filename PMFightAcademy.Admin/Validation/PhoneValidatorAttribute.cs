using System;
using System.ComponentModel.DataAnnotations;
using System.Linq;

namespace PMFightAcademy.Admin.Validation
{
    /// <summary>
    /// Validator for phone number
    /// </summary>
    /// <remarks>
    /// Formats of phone number:
    /// +38067 111 1111
    /// 067 111 1111
    /// Available country codes:
    /// 039, 067, 068, 096, 097, 098, 050, 066, 095, 099, 063, 093, 091, 092, 094
    /// </remarks>
    public class PhoneValidatorAttribute : ValidationAttribute
    {
#pragma warning disable CS1591 
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
#pragma warning restore CS1591
    }
}
