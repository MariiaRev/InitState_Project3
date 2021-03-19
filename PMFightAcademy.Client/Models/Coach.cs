using System;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace PMFightAcademy.Client.Models
{
    /// <summary>
    /// Coach model.
    /// </summary>
    public class Coach
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
        /// Coach date of birth.
        /// </summary>
        public DateTime BirthDate { get; set; }

        /// <summary>
        /// Description about coach.
        /// </summary>
        public string Description { get; set; }

        /// <summary>
        /// Coach phone number.
        /// </summary>
        [Phone]
        public string PhoneNumber { get; set; }
    }
}