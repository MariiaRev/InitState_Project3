using System.ComponentModel.DataAnnotations;

namespace PMFightAcademy.Admin.Contract
{
    /// <summary>
    /// Dto
    /// </summary>
    public class SlotsReturnContract
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
        public string Duration { get; set; }

    }
}