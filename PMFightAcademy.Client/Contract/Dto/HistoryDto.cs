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
        [RegularExpression(@"^(0[1-9])|1[0-2]\/([0-2][0-9]|3[0-1])\/[0-9]{4}$")]
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
    }
}
