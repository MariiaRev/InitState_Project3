using System;

namespace PMFightAcademy.Admin.Contract
{
    /// <summary>
    /// Dto for create Workout
    /// </summary>
    public class BookingContract
    {
        public int Id { get; set; }
        /// <summary>
        /// Coach id
        /// </summary>
        public int SlotId { get; set; }
        /// <summary>
        /// Service id
        /// </summary>
        public int ServiceId { get; set; }

        /// <summary>
        /// Client 
        /// </summary>
        public int ClientId { get; set; }
        

    }
}