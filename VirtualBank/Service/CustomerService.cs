using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using VirtualBank.Models;
using VirtualBank.ViewModel;
using VirtualBank.DAL;
using System.Data.Entity;
using VirtualBank.Helpers;

namespace VirtualBank.Service
{
    public class CustomerService
    {
        private DBcontext db;
        public CustomerService()
        {
            db = new DBcontext("DefaultConnection");
        }
        #region Login Register
        public int Register(RegisterVM customer)
        {
            Customer newCustomer = new Customer()
            {
                IBAN = customer.IBAN,
                first_name = customer.first_name,
                last_name = customer.last_name,
                phone = customer.phone,
                passwords = customer.passwords,
                balance = 0,
                created_by = customer.last_name,
                created_dt = DateTime.Now
            };
            db.Customer.Add(newCustomer);
            db.SaveChanges();
            return newCustomer.customer_Id;
        }

        #endregion
        public MainDisplayVM GetMainDisplay(int customerID, int page, int rowPresent)
        {
            Customer customer = db.Customer.Where(x => x.customer_Id == customerID).Include(x => x.Transactions).FirstOrDefault();
            customer.UpdateBalance(customer.IBAN);

            int total = customer.Transactions.Count();
            List<PagerTransactionDetail> listTrx = (from Trx in customer.Transactions
                                                    select new PagerTransactionDetail
                                                    {
                                                        trx_Id = Trx.trx_Id,
                                                        trx_type = Trx.trx_type,
                                                        amount = Trx.amount,
                                                        created_by = Trx.created_by,
                                                        created_dt = Trx.created_dt,
                                                        customer_id = Trx.customer_id,
                                                        desc = Trx.desc,
                                                        desc_2 = Trx.desc_2,
                                                        Id = Trx.Id,
                                                        total_count = total
                                                    }).OrderByDescending(x => x.trx_Id).Skip((page - 1) * rowPresent).Take(rowPresent).ToList();

            MainDisplayVM rtn = new MainDisplayVM()
            {
                customer_Id = customer.customer_Id,
                balance = customer.balance,
                first_name = customer.first_name,
                last_name = customer.last_name,
                last_updated = customer.updated_dt == null ? customer.created_dt : customer.updated_dt,
                last_updated_by = customer.updated_by == null ? customer.created_by : customer.updated_by,
                IBAN = customer.IBAN,
                phone = customer.phone,
                TrxDetail = new Pager<PagerTransactionDetail>(listTrx, page, rowPresent, total)
            };
            return rtn;
        }

        public int doDeposit(DepositVM ts)
        {
            //not the best solution. to be modified.
            int next_trx = db.Transaction.Any() ? db.Transaction.Select(x => x.trx_Id).Max() + 1 : 0;
            //
            Customer customer = db.Customer.Where(x => x.customer_Id == ts.customer_id).FirstOrDefault();

            //deposit action
            customer.Deposit(ts, next_trx);
            customer.UpdateBalance(customer.IBAN);
            db.SaveChanges();
            // deposit action end
            return next_trx;
        }
        public int doTransfer(TransferVM ts)
        {
            int next_trx = db.Transaction.Any() ? db.Transaction.Select(x => x.trx_Id).Max() + 1 : 0;
            Customer receivingCustomer = db.Customer.Where(x => x.IBAN == ts.transfer_to).FirstOrDefault();
            Customer customer = db.Customer.Where(x => x.customer_Id == ts.customer_id).FirstOrDefault();
            if (receivingCustomer == null)
            {
                throw new Exception("IBAN doesn't exist.");
            }
            if(customer.balance<ts.amount)
            { 
                throw new Exception(string.Format("Not enough money for transfering!  Balance:{0}, Transfering :{1}", customer.balance, ts.amount)); 
            }
            else
            { 
            //Transfer Action
            customer.TransferOut(ts, next_trx);
            receivingCustomer.TransferIn(ts, next_trx, customer.IBAN);
            customer.UpdateBalance(customer.IBAN);
            receivingCustomer.UpdateBalance(customer.IBAN);
            db.SaveChanges();
            }
            //Transfer Action end
            return next_trx;
        }
        public Customer CheckIBANExist(string iban)
        {
            Customer customer = db.Customer.Where(x => x.IBAN == iban).FirstOrDefault();
            return customer;
        }
        public bool CheckCustomerIDExist(int customerID)
        {
            Customer customer = db.Customer.Where(x => x.customer_Id == customerID).FirstOrDefault();
            return customer == null ? false : true;
        }
    }
}