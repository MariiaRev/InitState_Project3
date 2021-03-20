using PMFightAcademy.Admin.Models;

namespace PMFightAcademy.Admin.Contract
{
    /// <summary>
    /// Qualification contract
    /// </summary>
    public class QualificationContract
    {
        /// <summary>
        /// Service
        /// </summary>
        public int ServiceId { get; set; }
        /// <summary>
        /// Coach
        /// </summary>
        public int CoachId { get; set; }
    }
}