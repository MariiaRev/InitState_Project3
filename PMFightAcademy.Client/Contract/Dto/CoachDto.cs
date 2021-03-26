using System.ComponentModel.DataAnnotations;
using System.Collections.Generic;

namespace PMFightAcademy.Client.Contract.Dto
{
    /// <summary>
    /// Coach dto model.
    /// </summary>
    public class CoachDto
    {
        /// <summary>
        /// Coach id.
        /// </summary>
        [Range(1, int.MaxValue)]
        public int Id { get; set; }

        /// <summary>
        /// Coach first name.
        /// </summary>
        [Required]
        [StringLength(64, MinimumLength = 2)]
        public string FirstName { get; set; }

        /// <summary>
        /// Coach last name.
        /// </summary>
        [Required]
        [StringLength(64, MinimumLength = 2)]
        public string LastName { get; set; }

        /// <summary>
        /// Coach age.
        /// </summary>
        [Range(18, 90)]
        public int Age { get; set; }

        /// <summary>
        /// Description about coach.
        /// </summary>
        public string Description { get; set; }

        /// <summary>
        /// Coach phone number.
        /// </summary>
        [RegularExpression(Settings.PhoneRegularExpr)]
        public string PhoneNumber { get; set; }

        /// <summary>
        /// Services which can be provided by the coach.
        /// Could be empty if no service is assigned to this coach.
        /// </summary>
        public IEnumerable<string> Services { get; set; }
    }
}