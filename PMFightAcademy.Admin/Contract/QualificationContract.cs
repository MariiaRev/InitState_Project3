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
        public Service Service { get; set; }
        /// <summary>
        /// Coach
        /// </summary>
        public Coach Coach { get; set; }
    }
}