using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using VirtualBank.Models;
using VirtualBank.ViewModel;
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
        public ActionResult Login(string passwords, string IBAN)
        {
            // pass verify
            if (!string.IsNullOrEmpty(IBAN))
            {
                //Verify IBAN & password
                var customer=db.Customer.Where(x => x.IBAN == IBAN).FirstOrDefault();
                if (customer != null)
                {
                    if (customer.passwords == passwords)
                    {
                        Session["user_id"] = customer.customer_Id;
                       
                        return RedirectToAction("Main", "Customer", new { ID = customer.customer_Id });
                    }
                    //password fail
                    else
                    {
                        ViewBag.ErrorMesg = "Password does not match!";
                        return View();
                    }

                }
                //account doesnt exist
                else
                {
                    ViewBag.ErrorMesg = "IBAN does not exist!";
                    return View();
                }

            }
                
            // not pass
            else
                return View();
        }
        public ActionResult Register(RegisterVM customer)
        {
            //if IBAN == null, generate a new IBAN and send it back
            if (customer == null || customer.IBAN == null)
            {

                return View(customer);
            }
            else
            {
                // save user.
                try
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

                    Session["user_id"] = newCustomer.customer_Id;
                    return RedirectToAction("Main", "Customer", new { ID = newCustomer.customer_Id });
                }
                catch
                {
                    //fail saving
                    return View();
                }
              
            }
        }
    }
}