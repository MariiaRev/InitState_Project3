using System.ComponentModel.DataAnnotations;
using PMFightAcademy.Client.Authorization;


namespace PMFightAcademy.Client.Contract
{
    /// <summary>
    /// Contract for log in action.
    /// </summary>
    public class LoginContract
    {
        /// <summary>
        /// Phone number (login).
        /// </summary>
        /// <remarks>
        /// Formats of phone number:
        /// +38067 111 1111
        /// 067 111 1111
        /// Available country codes:
        /// 039, 067, 068, 096, 097, 098, 050, 066, 095, 099, 063, 093, 091, 092, 094
        /// </remarks>
        [Required(ErrorMessage = "Incorrect phone number!")]
        [StringLength(13, MinimumLength = 10)]
        [PhoneValidator]
        //[RegularExpression("")]
        public string Login { get; set; }

        /// <summary>
        /// Password.
        /// </summary>
        /// <remarks>
        /// Password must have at least 8 chars
        /// At least 1 upper char
        /// and at least 1 number
        /// </remarks> 
        [Required(ErrorMessage = "Password is required")]
        [StringLength(64, MinimumLength = 8)]
        [PasswordValidator]
        public string Password { get; set; }
    }
}
