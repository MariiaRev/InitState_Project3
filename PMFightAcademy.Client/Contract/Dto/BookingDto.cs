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
        /// Should be in format "MM.dd.yyyy" but as a string.
        /// </summary>
        [Required]
        [RegularExpression(Settings.DateRegularExpr)]
        public string Date { get; set; }

        /// <summary>
        /// The time for the service to be provided.
        /// Should be in format "HH:mm" but as a string.
        /// </summary>
        [Required]
        [RegularExpression(Settings.TimeRegularExpr)]
        public string Time { get; set; }

        /// <summary>
        /// Service id to be provided.
        /// </summary>
        [Required]
        [Range(1, int.MaxValue)]
        public int ServiceId { get; set; }

        /// <summary>
        /// Coach id to provide the service.
        /// </summary>
        [Required]
        [Range(1, int.MaxValue)]
        public int CoachId { get; set; }
    }
}
