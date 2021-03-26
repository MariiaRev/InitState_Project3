using System.ComponentModel.DataAnnotations;

namespace PMFightAcademy.Admin.Contract
{
    /// <summary>
    /// 
    /// </summary>
    public class BookingReturnContract
    {
        /// <summary>
        /// Id in db
        /// </summary>
        [Range(0, int.MaxValue)]
        public int Id { get; set; }

        /// <summary>
        /// Slot id
        /// </summary>
        public SlotsReturnContract Slot { get; set; }

        /// <summary>
        /// Service id
        /// </summary>
        public int ServiceId { get; set; }

        /// <summary>
        /// Client 
        /// </summary>
        public int ClientId { get; set; }

        /// <summary>
        /// ResultPrice
        /// </summary>
        [Range(0, 100000)]
        public decimal ResultPrice { get; set; }
    }
}