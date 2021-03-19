using System.ComponentModel.DataAnnotations.Schema;

namespace PMFightAcademy.Admin.Models
{
    /// <summary>
    /// Coach Qualification 
    /// </summary>
    public class Qualification
    {
        /// <summary>
        /// Personal Id , key
        /// </summary>
        [DatabaseGenerated(DatabaseGeneratedOption.Identity)]
        public int Id { get; }
        /// <summary>
        /// Coach 
        /// </summary>
        public int CoachId { get; set; }
        /// <summary>
        /// Service
        /// </summary>
        public int ServiceId { get; set; }

    }
}