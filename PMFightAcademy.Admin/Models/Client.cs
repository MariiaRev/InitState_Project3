using System;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace PMFightAcademy.Admin.Models
{
    /// <summary>
    /// Client Model
    /// </summary>
    public class Client
    {
        /// <summary>
        /// Id , Key
        /// </summary>
        [DatabaseGenerated(DatabaseGeneratedOption.Identity)]
        public int Id { get;  }
        /// <summary>
        /// User Login , phone number
        /// </summary>
        [Phone]
        [Required(AllowEmptyStrings = false)]
        [StringLength(64, MinimumLength = 6)]
        public string Login { get; }
        /// <summary>
        /// Password
        /// </summary>
        [Required(AllowEmptyStrings = false)]
        [StringLength(64, MinimumLength = 6)]
        public string Password { get; }
        /// <summary>
        /// User First Name
        /// </summary>
        [Required(AllowEmptyStrings = false)]
        [StringLength(64, MinimumLength = 2)]
        public string Name { get; set; }
    }
}