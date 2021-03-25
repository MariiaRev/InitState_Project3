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
        /// Date is in format "MM/dd/yyyy".
        /// </summary>
        [Required]
        [RegularExpression(@"^(0[1-9])|1[0-2].([0-2][0-9]|3[0-1]).[0-9]{4}$")]
        [RegularExpression(@"^(0[1-9]|1[0-2]).([0-2][0-9]|3[0-1]).[0-9]{4}$")]
        public string Date { get; set; }

        /// <summary>
        /// Time of provided service.
        /// Time is in format "HH:mm".
        /// </summary>
        [Required]
        [RegularExpression(@"^([0-1][0-9]|2[0-3]):([0-5][0-9])$")]
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
            Date = date.ToString("MM.dd.yyyy");
            Time = (new DateTime(1, 1, 1) + time).ToString("HH:mm");
            CoachFirstName = coachFirstName;
            CoachLastName = coachLastName;
            Price = price;
        }
    }
}
