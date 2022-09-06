using PROMPERU.AULAVIRTUAL.WEB.Helpers;
using System;
using System.Globalization;
using System.Threading;
using System.Web;
using System.Web.Http;
using System.Web.Mvc;
using System.Web.Routing;
using Newtonsoft.Json;

namespace PROMPERU.AULAVIRTUAL.WEB
{
    // Note: For instructions on enabling IIS6 or IIS7 classic mode, 
    // visit http://go.microsoft.com/?LinkId=9394801
    public class MvcApplication : HttpApplication
    {
        protected void Application_Start()
        {
            AreaRegistration.RegisterAllAreas();

            WebApiConfig.Register(GlobalConfiguration.Configuration);
            FilterConfig.RegisterGlobalFilters(GlobalFilters.Filters);
            RouteConfig.RegisterRoutes(RouteTable.Routes);

            ModelBinders.Binders.Add(typeof(decimal), new DecimalModelBinder());
            ModelBinders.Binders.Add(typeof(decimal?), new DecimalModelBinder());
            //ModelBinders.Binders.Add(typeof(DateTime), new PeruDateTimeModelBinder());
            //ModelBinders.Binders.Add(typeof(DateTime?), new PeruDateTimeModelBinder());
            GlobalConfiguration.Configuration.Formatters.JsonFormatter.SerializerSettings.ReferenceLoopHandling = ReferenceLoopHandling.Ignore;
            GlobalConfiguration.Configuration.Formatters.Remove(GlobalConfiguration.Configuration.Formatters.XmlFormatter);
        }

        protected void Session_Start(object sender, EventArgs e)
        {
            Thread.CurrentThread.CurrentCulture = Thread.CurrentThread.CurrentUICulture = new CultureInfo("ES-pe");
        }

        private void Application_Error(object sender, EventArgs e)
        {
            Exception exception = Server.GetLastError();


            //if (!HttpContext.Current.IsDebuggingEnabled)
            //{
               // Response.Redirect("/Home/NotFound");
            //}
            //else
            //{
            //    System.Diagnostics.Debug.WriteLine(exception);
            //}
        }
    }
}