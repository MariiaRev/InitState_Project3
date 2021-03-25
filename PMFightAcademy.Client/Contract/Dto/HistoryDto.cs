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
        /// Date is in format "MM.dd.yyyy".
        /// </summary>
        [Required]
        [RegularExpression(Settings.DateRegularExpr)]
        public string Date { get; set; }

        /// <summary>
        /// Time of provided service.
        /// Time is in format "HH:mm".
        /// </summary>
        [Required]
        [RegularExpression(Settings.TimeRegularExpr)]
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
        /// Result booking price.
        /// </summary>
        public decimal Price { get; set; }

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
        /// <param name="price">Booking price.</param>
        public HistoryDto(string serviceName, DateTime date, TimeSpan time, string coachFirstName, string coachLastName, decimal price)
        {
            ServiceName = serviceName;
            Date = date.ToString(Settings.DateFormat);
            Time = (new DateTime(1, 1, 1) + time).ToString(Settings.TimeFormat);
            CoachFirstName = coachFirstName;
            CoachLastName = coachLastName;
            Price = price;
        }
    }
}
