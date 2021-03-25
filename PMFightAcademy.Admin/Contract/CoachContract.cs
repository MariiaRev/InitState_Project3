using System.ComponentModel.DataAnnotations;

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
        [RegularExpression("^(0[1-9]|1[0-2]).([0-2][0-9]|3[0-1]).[0-9]{4}$")]
        public string DateBirth { get; set; }
        /// <summary>
        /// Description about coach
        /// </summary>

        public string Description { get; set; }
        /// <summary>
        /// Coach phone
        /// </summary>
        [RegularExpression(@"^(\+38|38)?0(39|50|63|66|67|68|91|92|93|94|95|96|97|98|99)\d{7}$")]
        public string PhoneNumber { get; set; }
    }
}