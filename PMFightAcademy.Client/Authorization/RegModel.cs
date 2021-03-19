using System.ComponentModel.DataAnnotations;

namespace PMFightAcademy.Client.Authorization
{
    public class RegModel
    {
        [Required(ErrorMessage = "Incorrect phone number!")]
        [StringLength(13, MinimumLength = 10)]
        [PhoneValidator]
        public string PhoneNumber { get; set; }

        [Required(ErrorMessage = "Password is required")]
        [StringLength(64, MinimumLength = 6)]
        [PasswordValidator]
        public string Password { get; set; }
    }
}
