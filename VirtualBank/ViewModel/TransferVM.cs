using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Data.Entity.Spatial;


namespace VirtualBank.ViewModel
{
    public class TransferVM
    {
        [Required]
        [StringLength(34)]
        public string transfer_to { get; set; }

        [Required]
        public int customer_id { get; set; }

        [Required]
        [Display(Name = "Amount")]
        [Range(1, int.MaxValue, ErrorMessage = "Please enter a value bigger than {1}")]
        public decimal amount { get; set; }

        [StringLength(100)]
        [Display(Name = "Description")]
        public string desc { get; set; }

        [StringLength(100)]
        [Display(Name = "Description 2")]
        public string desc_2 { get; set; }

    }
}