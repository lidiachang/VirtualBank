using System.Text;
using System.Web.Mvc;

namespace TangoWeb
{
    public static class ToastrHelper
    {
        const string quote = "\"";
        private static int _showDurationDefault = 10000;

        private static StringBuilder ArmartoastrOption(string cssPosicion, bool botonCierre, bool progressBar, int showDuration)
        {
            //tiempos default
            string Var_showDuration = "1000"; //lo que tarda en mostrar el recuadro
            string Var_hideDuration = "15000";//mostrando recuadro
            string Var_timeOut = "5000";
            string Var_extendedTimeOut = "1000";
            string Var_progressBar = "false";

            //Si posicion viene vacia asigna una por default
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
        private static StringBuilder doArmartoastrOption(string cssPosicion, bool botonCierre, bool progressBar, int showDuration,int timeOut)
        {
            //tiempos default
            string Var_showDuration = showDuration > 0 ? showDuration.ToString():"1000"; //lo que tarda en mostrar el recuadro
            string Var_hideDuration = "3000";//mostrando recuadro
            string Var_timeOut = timeOut > 0 ? timeOut.ToString() : "5000";
            string Var_extendedTimeOut = "1000";
            string Var_progressBar = "false";

            //Si posicion viene vacia asigna una por default
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
        private static string CuerpoMensajeToastr(string mensaje, string titulo)
        {
            string cuerpoMensaje = "";
            if (!string.IsNullOrEmpty(mensaje) && !string.IsNullOrEmpty(titulo))
                cuerpoMensaje = "'" + mensaje + "','" + titulo + "'";
            else
                cuerpoMensaje = "'" + mensaje + "'";

            return cuerpoMensaje;
        }


        /// <summary>
        /// Muestra una notificacion toastr "info"
        /// </summary>
        /// <param name="helper"></param>
        /// <param name="mensaje">Mensaje mostrar</param>
        /// <returns></returns>
        public static MvcHtmlString ToastrInfo(this HtmlHelper helper, string mensaje = null)
        {
            string toastr = "toastr.info(" + CuerpoMensajeToastr(mensaje, null) + ");";
            return MvcHtmlString.Create(toastr);
        }
        /// <summary>
        /// Muestra una notificacion toastr "info"
        /// </summary>
        /// <param name="helper"></param>
        /// <param name="mensaje">Mensaje mostrar</param>
        /// <param name="titulo">Titulo de notificación</param>
        /// <returns></returns>
        public static MvcHtmlString ToastrInfo(this HtmlHelper helper, string mensaje, string titulo)
        {
            string toastr = "toastr.info(" + CuerpoMensajeToastr(mensaje, titulo) + ");";
            return MvcHtmlString.Create(toastr);
        }

        /// <summary>
        /// Muestra una notificacion toastr "error"
        /// </summary>
        /// <param name="helper"></param>
        /// <param name="mensaje">Mensaje mostrar</param>
        /// <returns></returns>
        public static MvcHtmlString doToastrError(this HtmlHelper helper, string mensaje = null)
        {
            StringBuilder toastr = doArmartoastrOption("", true, true, 1000,5000);
            toastr.Append("toastr.error(" + CuerpoMensajeToastr(mensaje, null) + ");");
            return MvcHtmlString.Create(toastr.ToString());
        }
        /// <summary>
        /// Muestra una notificacion toastr "error"
        /// </summary>
        /// <param name="helper"></param>
        /// <param name="mensaje">Mensaje mostrar</param>
        /// <returns></returns>
        public static MvcHtmlString ToastrError(this HtmlHelper helper, string mensaje = null)
        {
            string toastr = "toastr.error(" + CuerpoMensajeToastr(mensaje, null) + ");";
            return MvcHtmlString.Create(toastr);
        }

        /// <summary>
        /// Muestra una notificacion toastr "error"
        /// </summary>
        /// <param name="helper"></param>
        /// <param name="mensaje">Mensaje mostrar</param>
        /// <param name="titulo">Titulo de notificación</param>
        /// <returns></returns>
        public static MvcHtmlString ToastrError(this HtmlHelper helper, string mensaje, string titulo)
        {
            string toastr = "toastr.error(" + CuerpoMensajeToastr(mensaje, titulo) + ");";
            return MvcHtmlString.Create(toastr);
        }

        /// <summary>
        /// Configuracion basica de alertas toastr
        /// </summary>
        /// <param name="helper"></param>
        /// <returns>Codigo javascrip</returns>
        public static MvcHtmlString ToastrOptions(this HtmlHelper helper)
        {
            StringBuilder sb = ArmartoastrOption("", false, false, _showDurationDefault);

            return MvcHtmlString.Create(sb.ToString());
        }

        /// <summary>
        /// Configurar alertas toastr con posición 
        /// </summary>
        /// <param name="helper"></param>
        /// <param name="cssPosicion">
        /// <para>Arriba/Derecha : toast-top-right</para>
        /// <para>Abajo/Derecha : toast-bottom-right</para>
        /// <para>Abajo/Izquierda : toast-bottom-left</para>
        /// <para>Arriba/Izquierda : toast-top-left</para>
        /// <para>Arriba/Completo : toast-top-full-width</para>
        /// <para>Abajo/Completo : toast-bottom-full-width</para>
        /// <para>Arriba/Centro : toast-top-center</para>
        /// <para>Abajo/Centro : toast-bottom-center</para>
        /// </param>
        /// <param name="botonCierre">
        /// <para>True: Se muestra un boton que permite cerrar el mensaje</para>
        /// </param>
        /// <param name="progressBar">
        /// <para>True muestra dentro de la notificacion un progress bar</para>
        /// </param>
        /// <returns>Codigo javascrip</returns>
        public static MvcHtmlString ToastrOptions(this HtmlHelper helper,
            string cssPosicion = null, bool botonCierre = true, bool progressBar = false)
        {
            StringBuilder sb;
            sb = ArmartoastrOption(cssPosicion, botonCierre, progressBar, _showDurationDefault);

            return MvcHtmlString.Create(sb.ToString());
        }

        /// <summary>
        /// Configurar alertas toastr con posición 
        /// </summary>
        /// <param name="helper"></param>
        /// <param name="cssPosicion">
        /// <para>Arriba/Derecha : toast-top-right</para>
        /// <para>Abajo/Derecha : toast-bottom-right</para>
        /// <para>Abajo/Izquierda : toast-bottom-left</para>
        /// <para>Arriba/Izquierda : toast-top-left</para>
        /// <para>Arriba/Completo : toast-top-full-width</para>
        /// <para>Abajo/Completo : toast-bottom-full-width</para>
        /// <para>Arriba/Centro : toast-top-center</para>
        /// <para>Abajo/Centro : toast-bottom-center</para>
        /// </param>
        /// <param name="botonCierre">
        /// <para>True: Se muestra un boton que permite cerrar el mensaje</para>
        /// </param>
        /// <param name="progressBar">
        /// <para>True muestra dentro de la notificacion un progress bar</para>
        /// </param>
        /// <param name="showDuration">Tiempo en milisegundos que dura el mensaje.
        /// El dafault es 10000
        /// </param>
        /// <returns>Codigo javascrip</returns>
        public static MvcHtmlString ToastrOptions(this HtmlHelper helper,
            string cssPosicion = null, bool botonCierre = true, bool progressBar = false, int showDuration = 10000)
        {
            StringBuilder sb;
            sb = ArmartoastrOption(cssPosicion, botonCierre, progressBar, showDuration);

            return MvcHtmlString.Create(sb.ToString());
        }

        /// <summary>
        /// Muestra una notificacion toastr "success"
        /// </summary>
        /// <param name="helper"></param>
        /// <param name="mensaje">Mensaje mostrar</param>
        /// <returns></returns>
        public static MvcHtmlString doToastrSuccess(this HtmlHelper helper, string mensaje = null)
        {
            StringBuilder toastr = doArmartoastrOption("", true, true, 1000, 2000);
            toastr.Append("toastr.success(" + CuerpoMensajeToastr(mensaje, null) + ");");
            return MvcHtmlString.Create(toastr.ToString());
        }
        /// <summary>
        /// Muestra una notificacion toastr "success"
        /// </summary>
        /// <param name="helper"></param>
        /// <param name="mensaje">Mensaje mostrar</param>
        /// <returns></returns>
        public static MvcHtmlString ToastrSuccess(this HtmlHelper helper, string mensaje = null)
        {
            string toastr = "toastr.success(" + CuerpoMensajeToastr(mensaje, null) + ");";
            return MvcHtmlString.Create(toastr);
        }

        /// <summary>
        /// Muestra una notificacion toastr "success"
        /// </summary>
        /// <param name="helper"></param>
        /// <param name="mensaje">Mensaje mostrar</param>
        /// <param name="titulo">Titulo de notificación</param>
        /// <returns></returns>
        public static MvcHtmlString ToastrSuccess(this HtmlHelper helper, string mensaje, string titulo)
        {
            string toastr = "toastr.success(" + CuerpoMensajeToastr(mensaje, titulo) + ");";
            return MvcHtmlString.Create(toastr);
        }

        /// <summary>
        /// Muestra una notificacion toastr "warning"
        /// </summary>
        /// <param name="helper"></param>
        /// <param name="mensaje">Mensaje mostrar</param>
        /// <returns></returns>
        public static MvcHtmlString ToastrWarning(this HtmlHelper helper, string mensaje = null)
        {
            string toastr = "toastr.warning(" + CuerpoMensajeToastr(mensaje, null) + ");";
            return MvcHtmlString.Create(toastr);
        }

        /// <summary>
        /// Muestra una notificacion toastr "warning"
        /// </summary>
        /// <param name="helper"></param>
        /// <param name="mensaje">Mensaje mostrar</param>
        /// <param name="titulo">Titulo de notificación</param>
        /// <returns></returns>
        public static MvcHtmlString ToastrWarning(this HtmlHelper helper, string mensaje, string titulo)
        {
            string toastr = "toastr.warning(" + CuerpoMensajeToastr(mensaje, titulo) + ");";
            return MvcHtmlString.Create(toastr);
        }

    }

    public class Toastr
    {
        public const string key = "TempDataToastr";
        public string Mensaje { get; set; }
        public string Titulo { get; set; }
        public string Tipo { get; set; }
    }
}