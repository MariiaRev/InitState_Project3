using System.ComponentModel.DataAnnotations;
using PMFightAcademy.Dal;

namespace PMFightAcademy.Admin.Contract
{
    /// <summary>
    /// CoachContract
    /// </summary>
    public class CoachContract
    {
        /// <summary>
        /// Personal Id , key
        /// </summary>
        [Range(0, int.MaxValue)]
        public int Id { get; set; }
        /// <summary>
        /// Coach first name
        /// </summary>
        [Required(AllowEmptyStrings = false)]
        [StringLength(64, MinimumLength = 2)]
        public string FirstName { get; set; }
        /// <summary>
        /// Coach Last Name 
        /// </summary>
        [Required(AllowEmptyStrings = false)]
        [StringLength(64, MinimumLength = 2)]
        public string LastName { get; set; }

        /// <summary>
        /// Date of birth 
        /// </summary>
        [RegularExpression(Settings.DateRegularExpr)]
        public string DateBirth { get; set; }
        /// <summary>
        /// Description about coach
        /// </summary>

        public string Description { get; set; }
        /// <summary>
        /// Coach phone
        /// </summary>
        [RegularExpression(Settings.PhoneRegularExpr)]
        public string PhoneNumber { get; set; }
    }
}