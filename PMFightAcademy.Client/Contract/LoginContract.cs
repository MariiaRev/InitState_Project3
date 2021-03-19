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
        [Required(ErrorMessage = "Incorrect phone number!")]
        [StringLength(13, MinimumLength = 10)]
        [PhoneValidator]
        [RegularExpression("")]
        public string Login { get; set; }

        /// <summary>
        /// Password.
        /// </summary>
        [Required(ErrorMessage = "Password is required")]
        [StringLength(64, MinimumLength = 6)]
        [PasswordValidator]
        public string Password { get; set; }
    }
}
