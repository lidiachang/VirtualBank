using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using VirtualBank.Models;
using VirtualBank.DAL;

using System.Net.Http;
using HtmlAgilityPack;
using System.Net;

namespace VirtualBank.Controllers
{
    public class LoginController : Controller
    {
        DBcontext db = new DBcontext("DefaultConnection");
        // GET: Login
        public ActionResult Login(Customer customer)
        {
            // pass verify
            if (customer.IBAN != null)
                return RedirectToAction("Main", "Customer", new { ID = customer.IBAN});
            // not pass
            else
                return View();
        }
        public ActionResult Register(Customer customer)
        {
            //if IBAN == null, generate a new IBAN and send it back
            if (customer == null || customer.IBAN == null)
            {
                ViewBag.IBAN = "TEST12345543";

                return View();
            }
            else
            {
                // save user.
                Customer newCustomer = new Customer()
                {
                    IBAN = customer.IBAN,
                    first_name = customer.first_name,
                    last_name = customer.last_name,
                    phone = customer.phone,
                    balance = 0,
                    created_by = customer.last_name,
                    created_dt = DateTime.Now
                };
              
                db.Customer.Add(newCustomer);
                db.SaveChanges();
              
                return RedirectToAction("Main", "Customer", new { ID = newCustomer.customer_Id });
            }
        }
    }
}