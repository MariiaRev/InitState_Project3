﻿using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Text.Json.Serialization;
using PMFightAcademy.Admin.Validation;

namespace PMFightAcademy.Admin.Models
{
    /// <summary>
    /// Client Model
    /// </summary>
    [Table("Clients")]
    public class Client
    {
        /// <summary>
        /// Id , Key
        /// </summary>
        [DatabaseGenerated(DatabaseGeneratedOption.Identity)]
        [JsonIgnore]
        public int Id { get; set; }

        /// <summary>
        /// User login represented by his/her phone number.
        /// </summary>
        /// <remarks>
        /// Formats of phone number:
        /// +38067 111 1111
        /// 067 111 1111
        /// Available country codes:
        /// 039, 067, 068, 096, 097, 098, 050, 066, 095, 099, 063, 093, 091, 092, 094
        /// </remarks>
        [Required(ErrorMessage = "Incorrect phone number!")]
        [StringLength(13, MinimumLength = 10)]
        [PhoneValidator]
        public string Login { get; set; }

        /// <summary>
        /// User password.
        /// </summary>
        /// <remarks>
        /// Password must have at least 8 chars
        /// At least 1 upper char
        /// and at least 1 number
        /// </remarks> 
        [JsonIgnore]
        [Required(AllowEmptyStrings = false)]
        [StringLength(64, MinimumLength = 8)]
        public string Password { get; set; }


        /// <summary>
        /// User First Name
        /// </summary>
        [Required]
        [StringLength(64, MinimumLength = 2)]
        public string Name { get; set; }
    }
}