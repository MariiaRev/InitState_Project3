using System.ComponentModel.DataAnnotations;
using PMFightAcademy.Dal;

namespace PMFightAcademy.Admin.Contract
{
    /// <summary>
    /// Dto
    /// </summary>
    public class SlotsCreateContract
    {
        [Range(0, int.MaxValue)]
        public int Id { get; set; }
        /// <summary>
        /// Coach Id
        /// </summary>
        public int CoachId { get; set; }

        /// <summary>
        /// Date start
        /// </summary>
        [RegularExpression(Settings.DateRegularExpr)]
        public string DateStart { get; set; }

        /// <summary>
        /// Time start
        /// </summary>
        [RegularExpression(Settings.TimeRegularExpr)]
        public string TimeStart { get; set; }
        /// <summary>
        /// Time End
        /// </summary>
        [RegularExpression(Settings.TimeRegularExpr)]
        public string TimeEnd { get; set; }

    }
}