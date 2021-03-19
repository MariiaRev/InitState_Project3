using System.ComponentModel.DataAnnotations;

namespace PMFightAcademy.Client.Contract.Dto
{
    /// <summary>
    /// History dto model.
    /// </summary>
    public class HistoryDto
    {
        /// <summary>
        /// Name of provided service.
        /// </summary>
        [Required]
        public string ServiceName { get; set; }

        /// <summary>
        /// Date of provided service.
        /// </summary>
        [Required]
        public string Date { get; set; }

        /// <summary>
        /// Time of provided service.
        /// </summary>
        [Required]
        public string Time { get; set; }

        /// <summary>
        /// First name of the coach that provided the service
        /// </summary>
        [Required]
        public string CoachFirstName { get; set; }

        /// <summary>
        /// Last name of the coach that provided the service
        /// </summary>
        [Required]
        public string CoachLastName { get; set; }
    }
}
