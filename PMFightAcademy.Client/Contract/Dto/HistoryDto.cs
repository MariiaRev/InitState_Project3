using System;
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

        /// <summary>
        /// Parameterless constructor.
        /// </summary>
        public HistoryDto() { }

        /// <summary>
        /// Constructor with parameters.
        /// </summary>
        /// <param name="serviceName">name of the provided service.</param>
        /// <param name="date">Date when the service is/was provided.</param>
        /// <param name="time">Time when the service is/was provided.</param>
        /// <param name="coachFirstName">First name of the coach who provided the service.</param>
        /// <param name="coachLastName">Last name of the coach who provided the service.</param>
        public HistoryDto(string serviceName, DateTime date, DateTime time, string coachFirstName, string coachLastName)
        {
            ServiceName = serviceName;
            Date = date.ToString("MM/dd/yyyy");
            Time = time.ToString("HH:mm");
            CoachFirstName = coachFirstName;
            CoachLastName = coachLastName;
        }
    }
}
