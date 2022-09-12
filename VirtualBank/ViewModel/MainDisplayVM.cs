using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Data.Entity.Spatial;
using VirtualBank.Helpers;

namespace VirtualBank.ViewModel
{
    public class MainDisplayVM
    {
        public int customer_Id { get; set; }

        public string IBAN { get; set; }

        public string last_name { get; set; }

        public string first_name { get; set; }

        public string phone { get; set; }

        [DisplayFormat(DataFormatString = "{0:N3}")]
        public decimal balance { get; set; }

        public DateTime? last_updated { get; set; }

        public string last_updated_by { get; set; }

        public Pager<PagerTransactionDetail> TrxDetail { get; set; }
    }
}