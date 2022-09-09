using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using VirtualBank.Models;

namespace VirtualBank.Controllers
{
    public class LoginController : Controller
    {
        // GET: Login
        public ActionResult Login(Customer customer)
        {
            // pass verify
            if (customer.IBAN != null)
                return RedirectToAction("Main", "Customer", new { ID = customer.Id });
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
                return RedirectToAction("Main", "Customer", new { ID = customer.Id });
            }
        }
    }
}