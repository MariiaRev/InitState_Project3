using System;

namespace PMFightAcademy.Admin.Contract
{
    /// <summary>
    /// Dto for create Workout
    /// </summary>
    public class BookingContract
    {
        public int Id { get; }
        /// <summary>
        /// Coach id
        /// </summary>
        public int CoachId { get; set; }
        /// <summary>
        /// Service id
        /// </summary>
        public int ServiceId { get; set; }
        /// <summary>
        /// Date 
        /// </summary>
        public string Date { get; set; }
        /// <summary>
        /// Time to start
        /// </summary>
        public string TimeToStart { get; set; }

    }
}