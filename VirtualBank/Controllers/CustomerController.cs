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
    public class CustomerController : Controller
    {
        DBcontext db = new DBcontext("DefaultConnection");
        // GET: Customer
        public ActionResult Main(int ID)
        {
            if (Session["user_id"] == null || int.Parse(Session["user_id"].ToString()) != ID)
            {
                return RedirectToAction("Login", "Login");
            }
            else
            {
                // retrieve customer's data
                Customer customer = db.Customer.Where(x => x.customer_Id == ID).Include(x => x.Transactions).FirstOrDefault();
                customer.UpdateBalance(customer.IBAN);

                return View(customer);
            }
           
        }
        public ActionResult Deposit(DepositVM ts)
        {
            if (Session["user_id"] == null || int.Parse(Session["user_id"].ToString()) != ts.customer_id)
            {
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

                customer.Deposit(ts,next_trx);
                customer.UpdateBalance(customer.IBAN);

                db.SaveChanges();

                return RedirectToAction("Main", new { ID = ts.customer_id });
            }
        }
        public ActionResult Transfer(TransferVM ts)
        {
            try
            {
                if (Session["user_id"] == null || int.Parse(Session["user_id"].ToString()) != ts.customer_id)
                {
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
                        return RedirectToAction("Transfer", new { customer_id = ts.customer_id });
                    }
                    else if (customer.balance < ts.amount)
                    {
                        // not enough money to transfer :(
                        return View(ts);
                    }
                    else
                    {
                        customer.TransferOut(ts, next_trx);
                        receivingCustomer.TransferIn(ts, next_trx, customer.IBAN);
                        customer.UpdateBalance(customer.IBAN);
                        receivingCustomer.UpdateBalance(customer.IBAN);
                        db.SaveChanges();

                        return RedirectToAction("Main", new { ID = ts.customer_id });
                    }
                }
                else
                {
                    return View(ts);
                }
            }
            catch
            {
                return View(ts);

            }
        }

    }
}