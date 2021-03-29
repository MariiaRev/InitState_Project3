using PMFightAcademy.Dal;
using System.ComponentModel.DataAnnotations;

namespace PMFightAcademy.Client.Contract.Dto
{
    /// <summary>
    /// Client dto model.
    /// </summary>
    public class ClientDto
    {
        /// <summary>
        /// User login represented by his/her phone number.
        /// </summary>
        /// <remarks>
        /// Formats of phone number: +380671234567
        /// Available country codes:
        /// 039, 067, 068, 096, 097, 098, 050, 066, 095, 099, 063, 093, 091, 092, 094
        /// </remarks>
        [Required(ErrorMessage = "Login (phone) is required.")]
        [RegularExpression(Settings.PhoneRegularExpr)]
        public string Login { get; set; }

        /// <summary>
        /// User password.
        /// </summary>
        /// <remarks>
        /// Password must have at least 8 chars
        /// At least 1 upper char
        /// and at least 1 number
        /// </remarks> 
        [Required(ErrorMessage = "Password is required.")]
        [RegularExpression(Settings.PasswordRegularExpr)]
        public string Password { get; set; }

        /// <summary>
        /// User name.
        /// </summary>
        [Required]
        [StringLength(64, MinimumLength = 2)]
        public string Name { get; set; }
    }
}
