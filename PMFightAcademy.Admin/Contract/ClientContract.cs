using PMFightAcademy.Dal;
using System.ComponentModel.DataAnnotations;

namespace PMFightAcademy.Admin.Contract
{
    /// <summary>
    /// Client contract model.
    /// </summary>
    public class ClientContract
    {
        /// <summary>
        /// Client id.
        /// </summary>
        public int Id { get; set; }

        /// <summary>
        /// User login represented by his/her phone number.
        /// </summary>
        /// <remarks>
        /// Formats of phone number:
        /// +38067 111 1111
        /// 38067 111 1111
        /// 067 111 1111
        /// Available country codes:
        /// 039, 067, 068, 096, 097, 098, 050, 066, 095, 099, 063, 093, 091, 092, 094.
        /// </remarks>
        [Required()]
        [RegularExpression(Settings.PhoneRegularExpr)]
        public string Login { get; set; }

        /// <summary>
        /// User First Name
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
