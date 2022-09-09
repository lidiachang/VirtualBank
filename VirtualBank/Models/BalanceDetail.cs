namespace VirtualBank.Models
{
    using System;
    using System.Collections.Generic;
    using System.ComponentModel.DataAnnotations;
    using System.ComponentModel.DataAnnotations.Schema;
    using System.Data.Entity.Spatial;

    [Table("BalanceDetail")]
    public partial class BalanceDetail
    {
        [Key]
        [Column(Order = 0)]
        public int Id { get; set; }

        [Key]
        [Column(Order = 1)]
        [DatabaseGenerated(DatabaseGeneratedOption.None)]
        public int trx_Id { get; set; }

        [Key]
        [Column(Order = 2)]
        [StringLength(1)]
        public string trx_type { get; set; }

        [Key]
        [Column(Order = 3)]
        [DatabaseGenerated(DatabaseGeneratedOption.None)]
        public int balance_Id { get; set; }

        [Key]
        [Column(Order = 4)]
        public decimal amount { get; set; }

        [StringLength(100)]
        public string desc { get; set; }

        [StringLength(100)]
        public string desc_2 { get; set; }

        [Key]
        [Column(Order = 5)]
        public DateTime created_dt { get; set; }

        [Key]
        [Column(Order = 6)]
        [StringLength(34)]
        public string created_by { get; set; }
    }
}
