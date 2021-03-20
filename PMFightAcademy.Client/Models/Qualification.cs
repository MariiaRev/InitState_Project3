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
        public int Id { get; }

        /// <summary>
        /// Coach id.
        /// </summary>
        [Required]
        public int CoachId { get; set; }

        /// <summary>
        /// 
        /// </summary>
        [JsonIgnore]
        public Coach Coach { get; set; }

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
