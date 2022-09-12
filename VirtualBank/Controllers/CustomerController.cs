using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using VirtualBank.Models;
using VirtualBank.ViewModel;
using VirtualBank.DAL;
using System.Data.Entity;

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
                Customer customer = db.Customer.Where(x => x.customer_Id == customerID).Include(x => x.Transactions).FirstOrDefault();
                customer.Transactions = customer.Transactions.OrderByDescending(z => z.Id).ToList();
                customer.UpdateBalance(customer.IBAN);

                return View(customer);
            }
           
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