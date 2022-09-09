namespace VirtualBank.Models
{
    using System;
    using System.Collections.Generic;
    using System.ComponentModel.DataAnnotations;
    using System.ComponentModel.DataAnnotations.Schema;
    using System.Data.Entity.Spatial;

    [Table("Balance")]
    public partial class Balance
    {
        [Key]
        [Column(Order = 0)]
        public int Id { get; set; }

        [Key]
        [Column(Order = 1)]
        [DatabaseGenerated(DatabaseGeneratedOption.None)]
        public int customer_Id { get; set; }

        [Key]
        [Column("balance", Order = 2)]
        public decimal balance { get; set; }

        [Key]
        [Column(Order = 3)]
        public DateTime created_dt { get; set; }

        [Key]
        [Column(Order = 4)]
        [StringLength(34)]
        public string created_by { get; set; }

        public DateTime? updated_dt { get; set; }

        [StringLength(34)]
        public string updated_by { get; set; }

        public virtual List<BalanceDetail> BalanceDetails { get; set; }
    }
}
