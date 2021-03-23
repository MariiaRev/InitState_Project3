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
        /// 38067 111 1111
        /// 067 111 1111
        /// Available country codes:
        /// 039, 067, 068, 096, 097, 098, 050, 066, 095, 099, 063, 093, 091, 092, 094
        /// </remarks>
        [Required(ErrorMessage = "Incorrect phone number!")]
        [RegularExpression(@"^(\+38|38)?0(39|50|63|66|67|68|91|92|93|94|95|96|97|98|99)\d{7}$")]
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
        [RegularExpression("(?=.*[A-Z])(?=.*[0-9])(?=.*[a-z]).{8,}")]
        public string Password { get; set; }
    }
}
