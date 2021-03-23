using System.ComponentModel.DataAnnotations;

namespace PMFightAcademy.Admin.Contract
{
    /// <summary>
    /// Dto
    /// </summary>
    public class SlotsCreateContract
    {
        public int Id { get; set; }
        /// <summary>
        /// Coach Id
        /// </summary>
        public int CoachId { get; set; }

        /// <summary>
        /// Date start
        /// </summary>
        [RegularExpression("^(0[1-9]|1[0-2]).([0-2][0-9]|3[0-1]).[0-9]{4}$")]
        public string DateStart { get; set; }

        /// <summary>
        /// Time start
        /// </summary>
        [RegularExpression("^([0-1][0-9]|2[0-3]):([0-5][0-9])$")]
        public string TimeStart { get; set; }
        /// <summary>
        /// Time End
        /// </summary>
        [RegularExpression("^([0-1][0-9]|2[0-3]):([0-5][0-9])$")]
        public string TimeEnd { get; set; }

    }
}