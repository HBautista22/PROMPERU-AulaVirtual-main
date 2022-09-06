using System;
using System.Collections.Generic;
using System.Dynamic;
using System.IO;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using System.Web.Routing;

namespace PROMPERU.AULAVIRTUAL.WEB.Logic
{
    public class TemplateRender
    {
        ControllerBase controller;

        public String Render(String template, object model)
        {

            var controller = CreateController<PROMPERU.AULAVIRTUAL.WEB.Controllers.BaseController>();

            IDictionary<string, object> anonymousDictionary = HtmlHelper.AnonymousObjectToHtmlAttributes(model);
            IDictionary<string, object> expando = new ExpandoObject();
            foreach (var item in anonymousDictionary)
                expando.Add(item);

            controller.ViewData.Model = (ExpandoObject)expando;

            using (StringWriter sw = new StringWriter())
            {
                var viewPath = "~/Templates/" + template + ".cshtml";
                var razorView = new RazorView(controller.ControllerContext, viewPath, "", false, new List<String>());
                ViewContext viewContext = new ViewContext(controller.ControllerContext, razorView, controller.ViewData, controller.TempData, sw);
                viewContext.View.Render(viewContext, sw);
                return sw.GetStringBuilder().ToString();
            }
        }

        public static T CreateController<T>(RouteData routeData = null) where T : Controller, new()
        {
            // create a disconnected controller instance
            T controller = new T();

            // get context wrapper from HttpContext if available
            HttpContextBase wrapper;
            if (System.Web.HttpContext.Current != null)
                wrapper = new HttpContextWrapper(System.Web.HttpContext.Current);
            else
                throw new InvalidOperationException(
                    "Can't create Controller Context if no " +
                    "active HttpContext instance is available.");

            if (routeData == null)
                routeData = new RouteData();

            // add the controller routing if not existing
            if (!routeData.Values.ContainsKey("controller") &&
                !routeData.Values.ContainsKey("Controller"))
                routeData.Values.Add("controller",
                                     controller.GetType()
                                               .Name.ToLower().Replace("controller", ""));

            controller.ControllerContext = new ControllerContext(wrapper, routeData, controller);
            return controller;
        }
    }
}