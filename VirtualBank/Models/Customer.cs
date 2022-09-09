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
        public int Id { get; set; }

        [Key]
        [Column(Order = 1)]
        [StringLength(34)]
        public string IBAN { get; set; }

        [Key]
        [Column(Order = 2)]
        [StringLength(50)]
        public string last_name { get; set; }

        [Key]
        [Column(Order = 3)]
        [StringLength(50)]
        public string first_name { get; set; }

        [StringLength(50)]
        public string phone { get; set; }

        public virtual Balance Balance { get; set; }
    }
}
