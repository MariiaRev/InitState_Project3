using System.ComponentModel.DataAnnotations;

namespace PMFightAcademy.Client.Contract.Dto
{
    /// <summary>
    /// Booking a service dto model.
    /// </summary>
    public class BookingDto
    {
        /// <summary>
        /// The date for the service to be provided.
        /// </summary>
        [Required]
        public string Date { get; set; }

        /// <summary>
        /// The time for the service to be provided.
        /// </summary>
        [Required]
        public string Time { get; set; }

        /// <summary>
        /// Service id to be provided.
        /// </summary>
        [Required]
        public int ServiceId { get; set; }

        /// <summary>
        /// Coach id to provide the service.
        /// </summary>
        [Required]
        public int CoachId { get; set; }
    }
}
