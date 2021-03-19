using System;
using System.ComponentModel.DataAnnotations.Schema;
using System.Runtime.InteropServices.ComTypes;

namespace PMFightAcademy.Admin.Models
{
    /// <summary>
    /// Slot 
    /// </summary>
    public class Slot
    {
        /// <summary>
        /// Personal key , id
        /// </summary>
        [DatabaseGenerated(DatabaseGeneratedOption.Identity)]
        public int Id { get; }
        /// <summary>
        /// Date of slot 
        /// </summary>
        public DateTime Date { get; set; }
        /// <summary>
        /// Start time 
        /// </summary>
        public TimeSpan StartTime { get; set; }
        /// <summary>
        /// Duration of slot 
        /// </summary>
        public TimeSpan Duration { get; set; }
        /// <summary>
        /// Coach of slot
        /// </summary>
        public int CoachId { get; set; }
    }
}