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

namespace VirtualBank.Controllers
{
    public class CustomerController : BaseController
    {
        DBcontext db = new DBcontext("DefaultConnection");
        // GET: Customer
        public ActionResult Main()
        {
            if (Session["user_id"] == null)
            {
                return RedirectToAction("Login", "Login");
            }
            else
            {
                int.TryParse(Session["user_id"].ToString(), out int customerID);

                var customer= GetMainDisplay(customerID, 1, 20);
                return View(customer);
            }
           
        }
        public ActionResult _TransactionDetail(int? page, int customerId)
        {
            int pageselected = page ?? 1;
            var ResultList = GetMainDisplay(customerId, pageselected, 20);
            var objPagination = ResultList.TrxDetail;

            return PartialView("_TransactionDetail", objPagination);
        }
        public MainDisplayVM GetMainDisplay(int customerID, int page, int rowPresent)
        {
            Customer customer = db.Customer.Where(x => x.customer_Id == customerID).Include(x => x.Transactions).FirstOrDefault();
            customer.UpdateBalance(customer.IBAN);

            int total = customer.Transactions.Count();
            List<PagerTransactionDetail> listTrx= (from Trx in customer.Transactions
                select new PagerTransactionDetail
                {
                   trx_Id=Trx.trx_Id,
                   trx_type=Trx.trx_type,
                   amount=Trx.amount,
                   created_by=Trx.created_by,
                   created_dt=Trx.created_dt,
                   customer_id=Trx.customer_id,
                   desc=Trx.desc,
                   desc_2=Trx.desc_2,
                   Id=Trx.Id,
                   total_count= total
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
                TrxDetail = new Pager<PagerTransactionDetail>(listTrx, page, rowPresent,total)
            };
            return rtn;
        }
        public ActionResult Deposit(DepositVM ts)
        {
            try
            {
                if (Session["user_id"] == null || int.Parse(Session["user_id"].ToString()) != ts.customer_id)
                {
                    ToastrError("Please login.", "Warning");
                    return RedirectToAction("Login", "Login");
                }
                if (ts.amount == 0)
                {
                    return View(ts);
                }
                else
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

                    ToastrSuccess("Deposit complete. transaction ID:" + next_trx, "Complete");
                    return RedirectToAction("Main");
                }
            }
            catch (Exception ex)
            {
                ToastrError("Exception." + ex.Message, "Fail");
                return View(ts);
            }
        }
        public ActionResult Transfer(TransferVM ts)
        {
            try
            {
                if (Session["user_id"] == null || int.Parse(Session["user_id"].ToString()) != ts.customer_id)
                {
                    ToastrError("Please login.", "Warning");
                    return RedirectToAction("Login", "Login");
                }
                if (ts.amount > 0)
                {
                    int next_trx = db.Transaction.Any() ? db.Transaction.Select(x => x.trx_Id).Max() + 1 : 0;
                    Customer receivingCustomer = db.Customer.Where(x => x.IBAN == ts.transfer_to).FirstOrDefault();
                    Customer customer = db.Customer.Where(x => x.customer_Id == ts.customer_id).FirstOrDefault();

                    // error input
                    if (receivingCustomer == null || customer == null)
                    {
                        ToastrError("IBAN doesn't exist.","Fail");
                        return RedirectToAction("Transfer", new { customer_id = ts.customer_id });
                    }
                    else if (customer.balance < ts.amount)
                    {
                        //  :(
                        ToastrError(string.Format("Not enough money for transfering!  Balance:{0}, Transfering :{1}", customer.balance, ts.amount), "Fail");
                        return View(ts);
                    }
                    else
                    {
                        //Transfer Action
                        customer.TransferOut(ts, next_trx);
                        receivingCustomer.TransferIn(ts, next_trx, customer.IBAN);
                        customer.UpdateBalance(customer.IBAN);
                        receivingCustomer.UpdateBalance(customer.IBAN);
                        db.SaveChanges();
                        //Transfer Action end

                        ToastrSuccess("Transfer complete. transaction ID:" + next_trx, "Complete");
                        return RedirectToAction("Main");
                    }
                }
                else
                {
                    return View(ts);
                }
            }
            catch(Exception ex)
            {
                ToastrError("Exception." + ex.Message, "Fail");
                return View(ts);

            }
        }

    }
}