using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace PMFightAcademy.Client.Models
{
    /// <summary>
    /// Client model.
    /// </summary>
    public class Client
    {
        /// <summary>
        /// User id.
        /// </summary>
        [DatabaseGenerated(DatabaseGeneratedOption.Identity)]
        public int Id { get; }

        /// <summary>
        /// User login represented by his/her phone number.
        /// </summary>
        [Phone]
        [Required]
        public string Login { get; set; }

        /// <summary>
        /// User password.
        /// </summary>
        [Required]
        [MinLength(8)]
        [RegularExpression("")]
        public string Password { get; set; }
        /// <summary>
        /// User name.
        /// </summary>
        [Required]
        [MinLength(2)]
        public string Name { get; set; }
    }
}
