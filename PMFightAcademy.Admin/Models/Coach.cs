using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Text.Json.Serialization;

namespace PMFightAcademy.Admin.Models
{
    /// <summary>
    /// Coach 
    /// </summary>
    [Table("Coaches")]
    public class Coach
    {
        /// <summary>
        /// Personal Id , key
        /// </summary>
        [DatabaseGenerated(DatabaseGeneratedOption.Identity)]
       
        public int Id { get;  set; }

        /// <summary>
        /// Coach first name
        /// </summary>
        [Required(AllowEmptyStrings = false)]
        [StringLength(64, MinimumLength = 2)]
        public string FirstName { get; set; }

        /// <summary>
        /// Coach Last Name 
        /// </summary>
        [Required(AllowEmptyStrings = false)]
        [StringLength(64, MinimumLength = 2)]
        public string LastName { get; set; }
        
        /// <summary>
        /// Date of birth 
        /// </summary>
        public DateTime BirthDate { get; set; }
        /// <summary>
        /// Description about coach
        /// </summary>
       
        public string Description { get; set; }

        /// <summary>
        /// Coach phone
        /// </summary>
        [Phone]
        public string PhoneNumber { get; set; }

        /// <summary>
        /// 
        /// </summary>
        [JsonIgnore]
        public ICollection<Slot> Slots { get; set; }
        
        /// <summary>
        /// 
        /// </summary>
        [JsonIgnore]
        public ICollection<Qualification> Qualifications { get; set; }
    }
}