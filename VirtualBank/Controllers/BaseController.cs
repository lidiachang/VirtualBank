using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace VirtualBank.Controllers
{
    public class BaseController : Controller
    {
        // GET: Base

        #region Notification Toastr

        public void ToastrInfo(string message)
        {
            AddToastrToView(message, null, "info");
        }

        public void ToastrInfo(string message, string title)
        {
            AddToastrToView(message, title, "info");
        }

        public void ToastrError(string message)
        {
            AddToastrToView(message, null, "error");
        }

        public void ToastrError(string message, string title)
        {
            AddToastrToView(message, title, "error");
        }

        public void ToastrSuccess(string message)
        {
            AddToastrToView(message, null, "success");
        }

        public void ToastrSuccess(string message, string title)
        {
            AddToastrToView(message, title, "success");
        }

        public void ToastrWarning(string message)
        {
            AddToastrToView(message, null, "warning");
        }

        public void ToastrWarning(string message, string title)
        {
            AddToastrToView(message, title, "warning");
        }

        private void AddToastrToView(string message, string title, string type)
        {
            message = message.Replace("'", "&#39;");
            var toastrs = TempData.ContainsKey(Toastr.key) ?
                (List<Toastr>)TempData[Toastr.key] :
                new List<Toastr>();

            toastrs.Add(new Toastr
            {
                message = message,
                title = title,
                type = type
            });

            TempData["TempDataToastr"] = toastrs;
        }

        #endregion Notification Toastr


    }
}