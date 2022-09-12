using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using VirtualBank.Models;
using VirtualBank.ViewModel;
using VirtualBank.DAL;
using VirtualBank.Service;

namespace VirtualBank.Controllers
{
    public class LoginController : BaseController
    {
        CustomerService _service = new CustomerService();
        // GET: Login
        public ActionResult Login(string passwords, string IBAN)
        {
            Session["user_id"] = null;

            if (!string.IsNullOrEmpty(IBAN))
            {
                var customer = _service.CheckIBANExist(IBAN);
                if (customer != null)
                {
                //Verify IBAN & password
                    if (customer.passwords == passwords)
                    {
                        Session["user_id"] = customer.customer_Id;
                       
                        return RedirectToAction("Main", "Customer");
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
                try
                {
                    int customerID = _service.Register(customer);
                    ToastrInfo("Account created. Welcome to Virtual Bank!", "你好!");
                    Session["user_id"] = customerID;
                    return RedirectToAction("Main", "Customer");
                }
                catch (Exception ex)
                {
                    //fail saving
                    ToastrError("Exception.", "Error");
                    return View();
                }
              
            }
        }
    }
}