using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace PMFightAcademy.Client.Models
{
    /// <summary>
    /// Service model.
    /// </summary>
    public class Service
    {
        /// <summary>
        /// Service id.
        /// </summary>
        [DatabaseGenerated(DatabaseGeneratedOption.Identity)]
        public int Id { get; }

        /// <summary>
        /// Service title.
        /// </summary>
        [Required]
        public string Name { get; set; }

        /// <summary>
        /// Service description.
        /// </summary>
        public string Description { get; set; }

        /// <summary>
        /// Service price per hour.
        /// </summary>
        [Required]
        public decimal Price { get; set; }
    }
}
