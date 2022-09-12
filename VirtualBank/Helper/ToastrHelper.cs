using System.Text;
using System.Web.Mvc;

namespace VirtualBank
{
    public static class ToastrHelper
    {
        const string quote = "\"";
        private static int _showDurationDefault = 10000;

        private static StringBuilder ArmartoastrOption(string cssPosicion, bool botonCierre, bool progressBar, int showDuration)
        {
            string Var_showDuration = "1000"; 
            string Var_hideDuration = "15000";
            string Var_timeOut = "5000";
            string Var_extendedTimeOut = "1000";
            string Var_progressBar = "false";

            if (string.IsNullOrEmpty(cssPosicion))
                cssPosicion = "toast-top-center";

            StringBuilder sb = new StringBuilder();
            sb.Append("toastr.options = {");
            sb.Append(quote + "debug" + quote + ": false,");
            sb.Append(quote + "newestOnTop" + quote + ": false,");
            sb.Append(
                quote + "positionClass" + quote + ": " + quote + cssPosicion + quote + ",");
            sb.Append(quote + "closeButton" + quote + ": " + botonCierre.ToString().ToLower() + ",");
            sb.Append(
               quote + "toastClass" + quote + ": " + quote + " animated fadeInDown" + quote + ",");
            sb.Append(quote + "progressBar" + quote + ": " + Var_progressBar + ",");
            sb.Append(quote + "showDuration" + quote + ": " + Var_showDuration + ",");
            sb.Append(quote + "hideDuration" + quote + ":" + Var_hideDuration + ",");
            sb.Append(quote + "timeOut" + quote + ":" + Var_timeOut + ",");
            sb.Append(quote + "extendedTimeOut" + quote + ":" + Var_extendedTimeOut + "");

            sb.Append("};");
            return sb;
        }
        private static string CuerpomessageToastr(string message, string title)
        {
            string cuerpomessage = "";
            if (!string.IsNullOrEmpty(message) && !string.IsNullOrEmpty(title))
                cuerpomessage = "'" + message + "','" + title + "'";
            else
                cuerpomessage = "'" + message + "'";

            return cuerpomessage;
        }

        public static MvcHtmlString ToastrOptions(this HtmlHelper helper)
        {
            StringBuilder sb = ArmartoastrOption("", false, false, _showDurationDefault);

            return MvcHtmlString.Create(sb.ToString());
        }
        public static MvcHtmlString ToastrOptions(this HtmlHelper helper,
            string cssPosicion = null, bool botonCierre = true, bool progressBar = false)
        {
            StringBuilder sb;
            sb = ArmartoastrOption(cssPosicion, botonCierre, progressBar, _showDurationDefault);

            return MvcHtmlString.Create(sb.ToString());
        }
        public static MvcHtmlString ToastrOptions(this HtmlHelper helper,
            string cssPosicion = null, bool botonCierre = true, bool progressBar = false, int showDuration = 10000)
        {
            StringBuilder sb;
            sb = ArmartoastrOption(cssPosicion, botonCierre, progressBar, showDuration);

            return MvcHtmlString.Create(sb.ToString());
        }

        public static MvcHtmlString ToastrInfo(this HtmlHelper helper, string message = null, string title = null)
        {
            string toastr = "toastr.info(" + CuerpomessageToastr(message, title) + ");";
            return MvcHtmlString.Create(toastr);
        }

        public static MvcHtmlString ToastrError(this HtmlHelper helper, string message = null, string title = null)
        {
            string toastr = "toastr.error(" + CuerpomessageToastr(message, title) + ");";
            return MvcHtmlString.Create(toastr);
        }
        public static MvcHtmlString ToastrSuccess(this HtmlHelper helper, string message = null, string title = null)
        {
            string toastr = "toastr.success(" + CuerpomessageToastr(message, title) + ");";
            return MvcHtmlString.Create(toastr);
        }
        public static MvcHtmlString ToastrWarning(this HtmlHelper helper, string message = null, string title = null)
        {
            string toastr = "toastr.warning(" + CuerpomessageToastr(message, title) + ");";
            return MvcHtmlString.Create(toastr);
        }
    }
   

    public class Toastr
    {
        public const string key = "TempDataToastr";
        public string message { get; set; }
        public string title { get; set; }
        public string type { get; set; }
    }
}