using System.ComponentModel.DataAnnotations;

namespace PMFightAcademy.Dal.Models
{
    /// <summary>
    /// Paggination model.
    /// </summary>
    public class Paggination
    {
        /// <summary>
        /// Current page number.
        /// </summary>
        [Required]
        [Range(1, int.MaxValue)]
        public int Page { get; set; }

        /// <summary>
        /// Total pages count.
        /// </summary>
        [Required]
        [Range(1, int.MaxValue)]
        public int TotalPages { get; set; }

        /// <summary>
        /// Indicates if the current page has a previous one.
        /// </summary>
        [Required]
        public bool HasPreviousPage
        {
            get
            {
                return (Page > 1);
            }
        }

        /// <summary>
        /// Indicates if the current page has a next one.
        /// </summary>
        [Required]
        public bool HasNextPage
        {
            get
            {
                return (Page < TotalPages);
            }
        }
    }
}
