using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Text.Json.Serialization;

namespace PMFightAcademy.Admin.Models
{
    /// <summary>
    /// Slot model.
    /// </summary>
    [Table("Slots")]
    public class Slot
    {
        /// <summary>
        /// Slot id.
        /// </summary>
        [DatabaseGenerated(DatabaseGeneratedOption.Identity)]
        
        public int Id { get;  set; }

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
        /// TimeEnd of slot.
        /// </summary>
        [Required]
        public TimeSpan Duration { get; set; }

        /// <summary>
        /// Coach of slot.
        /// </summary>
        [Required]                              
        public int CoachId { get; set; }
        
        /// <summary>
        /// 
        /// </summary>
        [JsonIgnore]
        public Coach Coach { get; set; }

        /// <summary>
        /// 
        /// </summary>
        [JsonIgnore]
        public ICollection<Booking> Bookings { get; set; }

        /// <summary>
        /// Expired slot
        /// </summary>
        [JsonIgnore]
        [DefaultValue(false)]
        public bool Expired { get; set; }
    }
}