using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Text.Json.Serialization;

namespace PMFightAcademy.Admin.Models
{
    /// <summary>
    /// Service, workout 
    /// </summary>
    [Table("Services")]
    public class Service
    {
        /// <summary>
        /// Personal id , key
        /// </summary>
        [DatabaseGenerated(DatabaseGeneratedOption.Identity)]
        public int Id { get; set; }

        /// <summary>
        /// Service Title 
        /// </summary>
        [Required(AllowEmptyStrings = false)]
        [StringLength(64, MinimumLength = 2)]
        public string Name { get; set; }

        /// <summary>
        /// Description of Workout
        /// </summary>
        public string Description { get; set; }

        /// <summary>
        /// Workout price per hour 
        /// </summary>
        [Required]
        [Range(1,100000)]
        public decimal Price { get; set; }

        /// <summary>
        /// 
        /// </summary>
        [JsonIgnore]
        public ICollection<Qualification> Qualifications { get; set; }

        /// <summary>
        /// 
        /// </summary>
        [JsonIgnore]
        public ICollection<Booking> Bookings { get; set; }
    }
}