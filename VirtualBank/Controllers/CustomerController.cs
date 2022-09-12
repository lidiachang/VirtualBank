using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using VirtualBank.ViewModel;
using VirtualBank.Service;

namespace VirtualBank.Controllers
{
    public class CustomerController : BaseController
    {
        CustomerService _service = new CustomerService();
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

                var customer= _service.GetMainDisplay(customerID, 1, 20);
                return View(customer);
            }
           
        }
        public ActionResult _TransactionDetail(int? page, int customerId)
        {
            int pageselected = page ?? 1;
            var ResultList = _service.GetMainDisplay(customerId, pageselected, 20);
            var objPagination = ResultList.TrxDetail;

            return PartialView("_TransactionDetail", objPagination);
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
                    int TrxID = _service.doDeposit(ts);
                    ToastrSuccess("Deposit complete. transaction ID:" + TrxID, "Complete");
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
                    int TrxID = _service.doTransfer(ts);
                    ToastrSuccess("Transfer complete. transaction ID:" + TrxID, "Complete");
                    return RedirectToAction("Main");

                }
                else
                {
                    return View(ts);
                }
            }
            catch(Exception ex)
            {
                ToastrError(ex.Message, "Fail");
                return View(ts);

            }
        }

    }
}