using System.Runtime.InteropServices.ComTypes;
using System.Text.Json.Serialization;
using PMFightAcademy.Admin.Models;

namespace PMFightAcademy.Admin.Contract
{
    /// <summary>
    /// Qualification contract
    /// </summary>
    public class QualificationContract
    {
        
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