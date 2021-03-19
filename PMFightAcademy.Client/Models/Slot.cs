using System;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace PMFightAcademy.Client.Models
{
    /// <summary>
    /// Slot model.
    /// </summary>
    public class Slot
    {
        /// <summary>
        /// Slot id.
        /// </summary>
        [DatabaseGenerated(DatabaseGeneratedOption.Identity)]
        public int Id { get; }

        /// <summary>
        /// Date of slot.
        /// </summary>
        [Required]
        public DateTime Date { get; set; }

        /// <summary>
        /// Slot start time.
        /// </summary>
        [Required]
        public TimeSpan StartTime { get; set; }

        /// <summary>
        /// Duration of slot.
        /// </summary>
        [Required]
        public TimeSpan Duration { get; set; }

        /// <summary>
        /// Coach of slot.
        /// </summary>
        [Required]                              //coach is required for service and slot?
        public int CoachId { get; set; }
    }
}
