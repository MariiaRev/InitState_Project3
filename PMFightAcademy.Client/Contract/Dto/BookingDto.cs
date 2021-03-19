using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace PMFightAcademy.Client.Contract.Dto
{
    /// <summary>
    /// Booking a service dto model.
    /// </summary>
    public class BookingDto
    {
        /// <summary>
        /// Booking id.
        /// </summary>
        [DatabaseGenerated(DatabaseGeneratedOption.Identity)]
        public int Id { get; }

        /// <summary>
        /// The date for the service to be provided.
        /// </summary>
        
        public  string Date { get; set; }

        /// <summary>
        /// The time for the service to be provided.
        /// </summary>
        public string Time { get; set; }

        /// <summary>
        /// Service id to be provided.
        /// </summary>
        [Required]
        public int? ServiceId { get; set; }

        /// <summary>
        /// Coach id to provide the service.
        /// </summary>
        [Required]
        public int? CoachId { get; set; }
    }
}
