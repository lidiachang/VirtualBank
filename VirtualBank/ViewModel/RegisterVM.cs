using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Data.Entity.Spatial;

namespace VirtualBank.ViewModel
{
    public class RegisterVM
    {
        [Required]
        [StringLength(34)]
        public string IBAN { get; set; }

        [Required]
        [StringLength(50)]
        public string last_name { get; set; }

        [Required]
        [Column(Order = 3)]
        [StringLength(50)]
        public string first_name { get; set; }

        [DataType(DataType.PhoneNumber)]
        [StringLength(50)]
        public string phone { get; set; }

        [Required]
        [DataType(DataType.Password)]
        [StringLength(50)]
        public string passwords { get; set; }

        [Required]
        [DataType(DataType.Password)]
        [StringLength(50)]
        [Compare("passwords",ErrorMessage ="Inconsistant passwords")]
        public string dp_password { get; set; }

    }
}