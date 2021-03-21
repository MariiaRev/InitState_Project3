using System.ComponentModel.DataAnnotations;

namespace PMFightAcademy.Admin.Contract
{
    public class CoachContract
    {
        /// <summary>
        /// Personal Id , key
        /// </summary>
        
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
        [StringLength(64, MinimumLength = 6)]
        public string LastName { get; set; }

        /// <summary>
        /// Date of birth 
        /// </summary>
        public string DateBirth { get; set; }
        /// <summary>
        /// Description about coach
        /// </summary>

        public string Description { get; set; }
        /// <summary>
        /// Coach phone
        /// </summary>
        [Phone]
        public string PhoneNumber { get; set; }
    }
}