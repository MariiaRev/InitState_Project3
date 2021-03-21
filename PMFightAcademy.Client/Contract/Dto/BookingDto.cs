using System;
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
        /// Id in db
        /// </summary>
        [Required]
        public int Id { get; set; }

        /// <summary>
        /// The date for the service to be provided.
        /// Should be in format "MM/dd/yyyy" but as a string.
        /// </summary>
        [Required]
        [RegularExpression(@"^(0[1-9])|1[0-2]\/([0-2][0-9]|3[0-1])\/[0-9]{4}$")]
        public string Date { get; set; }

        /// <summary>
        /// The time for the service to be provided.
        /// Should be in format "HH:mm" but as a string.
        /// </summary>
        [Required]
        [RegularExpression(@"^([0-1][0-9]|2[0-3]):([0-5][0-9])$")]
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
