using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using VirtualBank.Models;

namespace VirtualBank.Controllers
{
    public class CustomerController : Controller
    {
        // GET: Customer
        public ActionResult Main(int ID)
        {
            // retrieve customer's data
            Customer rtn = new Customer();
            rtn.IBAN = "test";
            rtn.first_name = "name";
            rtn.last_name = "Honor";
            rtn.Id = 1;
            rtn.Balance = new Balance()
            {
                balance = 1000,
                created_by = "fake",
                created_dt = DateTime.Now,
                customer_Id = 1
            };
            rtn.Balance.BalanceDetails = new List<BalanceDetail>();
            BalanceDetail item = new BalanceDetail()
            {
                Id=1,
                balance_Id = 0,
                amount = 5,
                trx_type="T",
                trx_Id=1,
                desc="test desc",
                created_dt=DateTime.Now,
                created_by="me"
            };
            rtn.Balance.BalanceDetails.Add(item);

            BalanceDetail item2 = new BalanceDetail()
            {
                Id=2,
                balance_Id = 0,
                amount = 5,
                trx_type = "T",
                trx_Id = 1,
                desc = "test desc",
                created_dt = DateTime.Now,
                created_by = "me"
            };
            rtn.Balance.BalanceDetails.Add(item2);
            return View(rtn);
        }
        public ActionResult Deposite(int ID)
        {
            return View();
        }
        public ActionResult Transfer(int ID)
        {
            return View();
        }
    }
}