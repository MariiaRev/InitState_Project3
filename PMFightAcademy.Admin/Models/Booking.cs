using System;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Text.Json.Serialization;

namespace PMFightAcademy.Admin.Models
{
    /// <summary>
    /// Booking WorkOut
    /// </summary>
    [Table("Bookings")]
    public class Booking
    {
        /// <summary>
        /// Personal key , id
        /// </summary>
        [DatabaseGenerated(DatabaseGeneratedOption.Identity)]
        public int Id { get; set; }

        /// <summary>
        /// Slot of workout
        /// </summary>
        ///////////////////////////[ForeignKey("Slot")]
        [Required]
        public int SlotId { get; set; }

        /// <summary>
        /// 
        /// </summary>
        [JsonIgnore]
        public Slot Slot { get; set; }

        /// <summary>
        /// Type of Workout(Service)
        /// </summary>
        [Required]
        public int ServiceId { get; set; }

        /// <summary>
        /// 
        /// </summary>
        [JsonIgnore]
        public Service Service { get; set; }

        /// <summary>
        /// Client 
        /// </summary>
        [Required]
        public int ClientId { get; set; }

        /// <summary>
        /// 
        /// </summary>
        [JsonIgnore]
        public Client Client { get; set; }

        /// <summary>
        /// result price add of coach and service 
        /// </summary>
        [Required]
        [Range(1,100000)]
        public decimal ResultPrice { get; set; }
    }
}