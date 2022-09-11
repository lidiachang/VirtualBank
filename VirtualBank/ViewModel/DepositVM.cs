using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.ComponentModel.DataAnnotations;

namespace VirtualBank.ViewModel
{
    public class DepositVM
    {
        [Required]
        public int customer_id { get; set; }

        [Required]
        [Display(Name = "Amount")]
        [Range(0, int.MaxValue, ErrorMessage = "Please enter a value bigger than {1}")]
        public decimal amount { get; set; }

        [StringLength(100)]
        [Display(Name = "Description")]
        public string desc { get; set; }

        [StringLength(100)]
        [Display(Name = "Description 2")]
        public string desc_2 { get; set; }
    }
}