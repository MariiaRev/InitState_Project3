using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Text.Json.Serialization;

namespace PMFightAcademy.Admin.Models
{
    /// <summary>
    /// Coach Qualification 
    /// </summary>
    [Table("Qualifications")]
    public class Qualification
    {
        /// <summary>
        /// Personal Id , key
        /// </summary>
        [DatabaseGenerated(DatabaseGeneratedOption.Identity)]
        [JsonIgnore]
        public int Id { get; set; }

        /// <summary>
        /// Coach 
        /// </summary>
        [Required]
        public int CoachId { get; set; }
        
        /// <summary>
        /// 
        /// </summary>
        [JsonIgnore]
        public Coach Coach { get; set; }

        /// <summary>
        /// Service
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