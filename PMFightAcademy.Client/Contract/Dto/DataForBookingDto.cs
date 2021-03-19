using PMFightAcademy.Client.Models;
using System.Collections.Generic;

namespace PMFightAcademy.Client.Contract.Dto
{
    /// <summary>
    /// Data for booking dto model.
    /// </summary>
    public class DataForBookingDto
    {
        /// <summary>
        /// Services available for a booking.
        /// </summary>
        public IEnumerable<Service> Services { get; set; }

        /// <summary>
        /// Coaches available for a booking.
        /// </summary>
        public IEnumerable<CoachDto> Coaches { get; set; }

        /// <summary>
        /// Dates available for a booking.
        /// </summary>
        public IEnumerable<string> Dates { get; set; }

        /// <summary>
        ///Time slots available for a booking.
        /// </summary>
        public IEnumerable<string> TimeSlots { get; set; }
    }
}
