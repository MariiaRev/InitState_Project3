using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Text.Json.Serialization;

namespace PMFightAcademy.Client.Models
{
    /// <summary>
    /// Coach qualification. 
    /// </summary>
    [Table("Qualifications")]
    public class Qualification
    {
        /// <summary>
        /// Qualification id.
        /// </summary>
        [DatabaseGenerated(DatabaseGeneratedOption.Identity)]
        [JsonIgnore]
        public int Id { get; set; }

        /// <summary>
        /// Coach id.
        /// </summary>
        [Required]
        public int CoachId { get; set; }

        /// <summary>
        /// 
        /// </summary>
        [JsonIgnore]
        public Dal.Models.Coach Coach { get; set; }

        /// <summary>
        /// Service id.
        /// </summary>
        [Required]
        public int ServiceId { get; set; }

        /// <summary>
        /// 
        /// </summary>
        [JsonIgnore]
        public Service Service { get; set; }
    }
}
