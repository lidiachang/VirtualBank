using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using VirtualBank.Models;

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
           var rtn = db.Customer.Where(x => x.customer_Id == ID).Include(x=>x.Transactions).FirstOrDefault();
            //update balance
            if (rtn.Transactions != null)
            {
                rtn.balance = rtn.Transactions.Select(x => x.amount).Sum();
                db.SaveChanges();
            }
            // retrieve customer's data
  
            return View(rtn);
        }
        public ActionResult Deposite(int ID)
        {
            var rtn = db.Customer.Where(x => x.customer_Id == ID).FirstOrDefault();
            ViewBag.ID = ID;
            return View();
        }

        public ActionResult DepositePost(Transaction ts)
        {
            int next_trx = db.Transaction.Select(x => x.trx_Id).Max() + 1;
           
            ts.created_by = "test";
            ts.created_dt = DateTime.Now;
            ts.trx_Id = next_trx;
            db.Transaction.Add(ts);
            db.SaveChanges();
            return RedirectToAction("Main", new { ID = ts.customer_id });
        }
        public ActionResult Transfer(int ID)
        {
            ViewBag.ID = ID;
            return View();
        }
        public ActionResult TransferPost(Transaction ts, string transfer_to)
        {
            int next_trx = db.Transaction.Select(x => x.trx_Id).Max() + 1;
            Customer receivingCustomer= db.Customer.Where(x => x.IBAN == transfer_to).FirstOrDefault();
            // error input
            if (receivingCustomer == null)
                return RedirectToAction("Transfer", new { ID = ts.customer_id });
            else
            {
                // from
                ts.created_by = "test";
                ts.created_dt = DateTime.Now;
                ts.trx_Id = next_trx;
                ts.amount = -ts.amount;
                db.Transaction.Add(ts);
                //to

                Transaction to = new Transaction()
                {
                    customer_id = receivingCustomer.customer_Id,
                    amount = -ts.amount,
                    desc = ts.desc,
                    desc_2 = ts.desc_2,
                    created_by = ts.created_by,
                    created_dt = ts.created_dt,
                    trx_Id = ts.trx_Id,
                    trx_type = ts.trx_type
                };
                db.Transaction.Add(to);

                db.SaveChanges();
                return RedirectToAction("Main", new { ID = ts.customer_id });
            }
        }
    }
}