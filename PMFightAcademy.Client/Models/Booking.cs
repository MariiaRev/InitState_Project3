using System.ComponentModel.DataAnnotations.Schema;
using System.ComponentModel.DataAnnotations;
using System.Text.Json.Serialization;

namespace PMFightAcademy.Client.Models
{
    /// <summary>
    /// Booking a service model.
    /// </summary>
    [Table("Bookings")]
    public class Booking
    {
        /// <summary>
        /// Booking id.
        /// </summary>
        [DatabaseGenerated(DatabaseGeneratedOption.Identity)]
        public int Id { get; set; }

        /// <summary>
        /// Slot id for the booking.
        /// </summary>
        [Required]
        public int SlotId { get; set; }
        
        /// <summary>
        /// 
        /// </summary>
        [JsonIgnore]
        public Slot Slot { get; set; }

        /// <summary>
        /// Service id for the booking.
        /// </summary>
        [Required]        
        public int ServiceId { get; set; }
        
        /// <summary>
        /// 
        /// </summary>
        [JsonIgnore]
        public Service Service { get; set; }

        /// <summary>
        /// Client id  for the booking.
        /// </summary>
        [Required]
        public int ClientId { get; set; }
        
        /// <summary>
        /// 
        /// </summary>
        [JsonIgnore]
        public Dal.Models.Client Client { get; set; }

        /// <summary>
        /// Result price of the booking.
        /// </summary>
        [Required]
        [Range(0, 100000)]
        public decimal ResultPrice { get; set; }
    }
}