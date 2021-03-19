using System.ComponentModel.DataAnnotations.Schema;
using System.ComponentModel.DataAnnotations;

namespace PMFightAcademy.Client.Models
{
    /// <summary>
    /// Booking a service model.
    /// </summary>
    public class Booking
    {
        /// <summary>
        /// Booking id.
        /// </summary>
        [DatabaseGenerated(DatabaseGeneratedOption.Identity)]
        public int Id { get; }

        /// <summary>
        /// Slot id for the booking.
        /// </summary>
        [Required]
        public int SlotId { get; set; }

        /// <summary>
        /// Service id for the booking.
        /// </summary>
        [Required]        
        public int ServiceId { get; set; }

        /// <summary>
        /// Client id  for the booking.
        /// </summary>
        [Required]
        public int ClientId { get; set; }

        /// <summary>
        /// Result price of the booking.
        /// </summary>
        [Required]
        public decimal ResultPrice { get; set; }
    }
}
