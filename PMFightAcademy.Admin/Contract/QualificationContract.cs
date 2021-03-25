using System.ComponentModel.DataAnnotations;

namespace PMFightAcademy.Admin.Contract
{
    /// <summary>
    /// Qualification contract
    /// </summary>
    public class QualificationContract
    {
        [Range(0, int.MaxValue)] 
        public int Id { get; set; }
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