using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using PMFightAcademy.Client.Models;
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
        [DatabaseGenerated(DatabaseGeneratedOption.Identity)]
        public int Id { get; }

        /// <summary>
        /// Coach first name.
        /// </summary>
        [Required]
        public string FirstName { get; set; }

        /// <summary>
        /// Coach last name.
        /// </summary>
        [Required]
        public string LastName { get; set; }

        /// <summary>
        /// Coach age.
        /// </summary>
        public int Age { get; set; }

        /// <summary>
        /// Description about coach.
        /// </summary>
        public string Description { get; set; }

        /// <summary>
        /// Coach phone number.
        /// </summary>
        [Phone]
        public string PhoneNumber { get; set; }

        /// <summary>
        /// Services which can be provided by the coach.
        /// </summary>
        public List<string> Services { get; set; }
    }
}