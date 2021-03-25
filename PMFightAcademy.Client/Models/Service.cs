using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Text.Json.Serialization;

namespace PMFightAcademy.Client.Models
{
    /// <summary>
    /// Service model.
    /// </summary>
    [Table("Services")]
    public class Service
    {
        /// <summary>
        /// Service id.
        /// </summary>
        [DatabaseGenerated(DatabaseGeneratedOption.Identity)]
        public int Id { get; set; }

        /// <summary>
        /// Service title.
        /// </summary>
        [Required(AllowEmptyStrings = false)]
        [StringLength(64, MinimumLength = 2)]
        public string Name { get; set; }

        /// <summary>
        /// Service description.
        /// </summary>
        public string Description { get; set; }

        /// <summary>
        /// Service price per hour.
        /// </summary>
        [Required]
        [Range(0, 100000)]
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
