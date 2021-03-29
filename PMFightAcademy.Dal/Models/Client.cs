using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace PMFightAcademy.Dal.Models
{
    /// <summary>
    /// Client model.
    /// </summary>
    [Table("Clients")]
    public class Client
    {
        /// <summary>
        /// User id  
        /// </summary>
        [DatabaseGenerated(DatabaseGeneratedOption.Identity)]
        public int Id { get; set; }

        /// <summary>
        /// User login represented by his/her phone number.
        /// </summary>
        /// <remarks>
        /// Formats of phone number:
        /// +38067 111 1111
        /// 067 111 1111
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

        /// <summary>
        /// Description for admin
        /// </summary>
        public string Description { get; set; }
    }
}
