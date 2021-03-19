using System.ComponentModel.DataAnnotations;

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
        [Required]
        [Phone]
        public string Login { get; set; }

        /// <summary>
        /// Password.
        /// </summary>
        [Required]
        [MinLength(8)]
        [RegularExpression("")]
        public string Password { get; set; }
    }
}