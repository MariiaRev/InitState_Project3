using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace PMFightAcademy.Client.Models
{
    /// <summary>
    /// Coach qualification. 
    /// </summary>
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
        /// Service id.
        /// </summary>
        [Required]
        public int ServiceId { get; set; }
    }
}
