using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace PMFightAcademy.Admin.Models
{
    /// <summary>
    /// Service, workout 
    /// </summary>
    public class Service
    {
        /// <summary>
        /// Personal id , key
        /// </summary>
        [DatabaseGenerated(DatabaseGeneratedOption.Identity)]
        public int Id { get;  }
        /// <summary>
        /// Service Title 
        /// </summary>
        [Required(AllowEmptyStrings = false)]
        [StringLength(64, MinimumLength = 2)]
        public string Name { get; set; }
        /// <summary>
        /// Description of Workout
        /// </summary>
        public string Description { get; set; }
        /// <summary>
        /// Workout price per hour 
        /// </summary>
        public decimal Price { get; set; }
    }
}