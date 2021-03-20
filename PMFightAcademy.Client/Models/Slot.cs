﻿using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Text.Json.Serialization;

namespace PMFightAcademy.Client.Models
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
    }
}