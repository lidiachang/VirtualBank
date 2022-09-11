namespace VirtualBank.Models
{
    using System;
    using System.Collections.Generic;
    using System.ComponentModel.DataAnnotations;
    using System.ComponentModel.DataAnnotations.Schema;
    using System.Data.Entity.Spatial;

    [Table("Transaction")]
    public partial class Transaction
    {
        [Key]
        [Column(Order = 0)]
        [DatabaseGeneratedAttribute(DatabaseGeneratedOption.Identity)]
        public int Id { get; set; }

        [Column(Order = 1)]
        [DatabaseGenerated(DatabaseGeneratedOption.None)]
        public int customer_id { get; set; }

        [Column(Order = 3)]
        [DatabaseGenerated(DatabaseGeneratedOption.None)]
        [Display(Name = "Trx. ID")]
        public int trx_Id { get; set; }

        [Column(Order = 2)]
        [StringLength(1)]
        [Display(Name = "Type")]
        public string trx_type { get; set; }

        [Required]
        [Display(Name = "Amount")]
        [Column(Order = 4)]
        public decimal amount { get; set; }

        [StringLength(100)]
        [Display(Name = "Description")]
        public string desc { get; set; }

        [StringLength(100)]
        [Display(Name = "Description 2")]
        public string desc_2 { get; set; }

        [Column(Order = 5)]
        [Display(Name = "Created Date")]
        public DateTime created_dt { get; set; }

        [Column(Order = 6)]
        [StringLength(34)]
        [Display(Name = "Created By")]
        public string created_by { get; set; }
    }
}
