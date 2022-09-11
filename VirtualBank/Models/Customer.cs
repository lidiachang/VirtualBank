namespace VirtualBank.Models
{
    using System;
    using System.Collections.Generic;
    using System.ComponentModel.DataAnnotations;
    using System.ComponentModel.DataAnnotations.Schema;
    using System.Data.Entity.Spatial;
    using VirtualBank.ViewModel;
    using System.Linq;

    [Table("Customer")]
    public partial class Customer
    {
        public Customer()
        {
            Transactions = new List<Transaction>();
        }
        [Key]
        [Column(Order = 0)]
        [DatabaseGeneratedAttribute(DatabaseGeneratedOption.Identity)]
        public int customer_Id { get; set; }

        [Column(Order = 1)]
        [StringLength(34)]
        [Required]
        public string IBAN { get; set; }

        [Column(Order = 2)]
        [StringLength(50)]
        [Required]
        public string last_name { get; set; }

        [Column(Order = 3)]
        [StringLength(50)]
        [Required]
        public string first_name { get; set; }

        [DataType(DataType.PhoneNumber)]
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
        [Required]
        public string passwords { get; set; }

        public virtual List<Transaction> Transactions { get; set; }

        public virtual decimal rate { get; } = (decimal)0.001;

        #region  Method
        public void UpdateBalance(string updatedBy)
        {
            if (Transactions != null)
            {
                balance = Transactions.Select(x => x.amount).Sum();
                updated_by = updatedBy;
                updated_dt = DateTime.Now;

            }
        }
        public void Deposit(DepositVM ts, int trxId)
        {
            Transaction dep = new Transaction()
            {
                amount = ts.amount,
                created_by = this.IBAN,
                created_dt = DateTime.Now,
                customer_id = this.customer_Id,
                desc = ts.desc,
                desc_2 = ts.desc_2,
                trx_Id = trxId,
                trx_type = "D"
            };
            Transactions.Add(dep);
            //add transaction fee
            Transaction trx_fee = new Transaction()
            {
                amount = -(ts.amount) * rate,
                created_by = "system",
                created_dt = DateTime.Now,
                customer_id = this.customer_Id,
                desc = "transaction fee",
                desc_2 = ts.desc_2,
                trx_Id = trxId,
                trx_type = "D"
            };
            Transactions.Add(trx_fee);
        }
        public void TransferIn(TransferVM ts, int trxId, string fromCustomer)
        {
            Transaction trx = new Transaction()
            {
                amount = ts.amount,
                created_by = fromCustomer,
                created_dt = DateTime.Now,
                customer_id = this.customer_Id,
                desc = ts.desc,
                desc_2=ts.desc_2,
                trx_Id = trxId,
                trx_type = "T"
            };
            Transactions.Add(trx);
        }
        public void TransferOut(TransferVM ts, int trxId)
        {
            Transaction trx = new Transaction()
            {
                amount = -ts.amount,
                created_by = this.IBAN,
                created_dt = DateTime.Now,
                customer_id = this.customer_Id,
                desc = ts.desc,
                desc_2 = ts.desc_2,
                trx_Id = trxId,
                trx_type = "T"
            };
            Transactions.Add(trx);
        }

        #endregion
    }
}
