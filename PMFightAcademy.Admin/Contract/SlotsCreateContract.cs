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
        public string DateStart { get; set; }

        /// <summary>
        /// Time start
        /// </summary>
        public string TimeStart { get; set; }
        /// <summary>
        /// Time End
        /// </summary>
        public string Duration { get; set; }

    }
}