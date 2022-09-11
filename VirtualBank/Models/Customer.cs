namespace VirtualBank.Models
{
    using System;
    using System.Collections.Generic;
    using System.ComponentModel.DataAnnotations;
    using System.ComponentModel.DataAnnotations.Schema;
    using System.Data.Entity.Spatial;

    [Table("Customer")]
    public partial class Customer
    {
        [Key]
        [Column(Order = 0)]
        [DatabaseGeneratedAttribute(DatabaseGeneratedOption.Identity)]
        public int customer_Id { get; set; }

        [Column(Order = 1)]
        [StringLength(34)]
        public string IBAN { get; set; }

        [Column(Order = 2)]
        [StringLength(50)]
        public string last_name { get; set; }

        [Column(Order = 3)]
        [StringLength(50)]
        public string first_name { get; set; }

        [StringLength(50)]
        public string phone { get; set; }

        public decimal balance { get; set; }

        public DateTime created_dt { get; set; }

        [StringLength(34)]
        public string created_by { get; set; }

        public DateTime? updated_dt { get; set; }

        [StringLength(34)]
        public string updated_by { get; set; }

        [DataType(DataType.Password)]
        [StringLength(50)]
        public string passwords { get; set; }

        public virtual List<Transaction> Transactions { get; set; }
    }
}
