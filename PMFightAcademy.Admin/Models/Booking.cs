using System.ComponentModel.DataAnnotations.Schema;

namespace PMFightAcademy.Admin.Models
{
    /// <summary>
    /// Booking WorkOut
    /// </summary>
    public class Booking
    {
        /// <summary>
        /// Personal key , id
        /// </summary>
        [DatabaseGenerated(DatabaseGeneratedOption.Identity)]
        public int Id { get; }
        /// <summary>
        /// Slot of workout
        /// </summary>
        public int SlotId { get; set; }
        /// <summary>
        /// Type of Workout(Service)
        /// </summary>
        public int ServiceId { get; set; }
        /// <summary>
        /// Client 
        /// </summary>
        public int ClientId { get; set; }
        /// <summary>
        /// result price add of coach and service 
        /// </summary>
        public decimal ResultPrice { get; set; }
    }
}