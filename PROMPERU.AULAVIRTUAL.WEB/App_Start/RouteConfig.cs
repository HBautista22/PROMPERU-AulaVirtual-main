using System.Web.Mvc;
using System.Web.Routing;

namespace PROMPERU.AULAVIRTUAL.WEB
{
    public class RouteConfig
    {
        public static void RegisterRoutes(RouteCollection routes)
        {
            routes.IgnoreRoute("{resource}.axd/{*pathInfo}");
            //routes.MapMvcAttributeRoutes();

            //routes.IgnoreRoute("p/{*url}");

            routes.MapRoute(
                name: "CMS",
                url: "",
                defaults: new { controller = "Cms", action = "ViewPagina" }
            );

            routes.MapRoute(
                name: "CMS 2",
                url: "agenda",
                defaults: new { controller = "Cms", action = "ViewPagina", url="agenda" }
            );

            routes.MapRoute(
                name: "CMS 3",
                url: "preguntas-frecuentes",
                defaults: new { controller = "Cms", action = "ViewPagina", url = "preguntas-frecuentes" }
            );

            routes.MapRoute(
                name: "CMS 4",
                url: "certificate",
                defaults: new { controller = "Cms", action = "ViewPagina", url= "certificate" }
            );

            routes.MapRoute(
                name: "Default",
                url: "{controller}/{action}/{id}",
                defaults: new { controller = "Home", action = "Index", id = UrlParameter.Optional }
            );



        }
    }
}