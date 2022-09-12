using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Data.Entity.Spatial;

namespace VirtualBank.ViewModel
{
    public class PagerTransactionDetail
    {
        public int Id { get; set; }

        public int customer_id { get; set; }

        [Display(Name = "Trx. ID")]
        public int trx_Id { get; set; }

        [Display(Name = "Type")]
        public string trx_type { get; set; }

        [Display(Name = "Amount")]
        [DisplayFormat(DataFormatString = "{0:N3}")]
        public decimal amount { get; set; }

        [Display(Name = "Description")]
        public string desc { get; set; }

        [Display(Name = "Description 2")]
        public string desc_2 { get; set; }

        [Display(Name = "Created Date")]
        public DateTime created_dt { get; set; }

        [Display(Name = "Created By")]
        public string created_by { get; set; }

        public int total_count { get; set; }
    }
}