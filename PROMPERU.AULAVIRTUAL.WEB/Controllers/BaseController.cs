using PROMPERU.AULAVIRTUAL.WEB.Helpers;
using System;
using System.Collections.Generic;
using System.Web;
using System.Web.Mvc;

namespace PROMPERU.AULAVIRTUAL.WEB.Controllers
{
    public class BaseController : Controller
    {
        private CargarDatosContext _cargarDatosContext;

        //protected EvolEntities context;
        protected CargarDatosContext DatosContext => _cargarDatosContext ??
                                                     (_cargarDatosContext = new CargarDatosContext
                                                         { Session = Session });

        /*protected override void Initialize(System.Web.Routing.RequestContext requestContext)
        {
            requestContext.RouteData.Values["action"].ToString().Replace("p", "");
        }*/

        public BaseController()
        {
            //context = new EvolEntities();
        }

        //public void InvalidarContext()
        //{
        //    context.Dispose();
        //    context = new EvolEntities();
        //}

        public void PostMessage(FlashMessage message)
        {
            if (TempData["FlashMessages"] == null)
                TempData["FlashMessages"] = new List<FlashMessage>();

            ((List<FlashMessage>)TempData["FlashMessages"]).Add(message);
        }

        public void PostMessage(MessageType type)
        {
            string body;

            switch (type)
            {
                case MessageType.Error:
                    body = "Ha ocurrido un error al procesar la solicitud.";
                    break;
                case MessageType.Info:
                    body = string.Empty;
                    break;
                case MessageType.Success:
                    body = "Los datos se guardaron exitosamente.";
                    break;
                case MessageType.Warning:
                    body = ".";
                    break;
                default:
                    throw new ArgumentOutOfRangeException(nameof(type), type, null);
            }

            PostMessage(type, body);
        }

        public void PostMessage(MessageType type, string title, string body)
        {
            PostMessage(new FlashMessage { Title = title, Body = body, Type = type });
        }

        public void PostMessage(MessageType type, string body)
        {
            string title;

            switch (type)
            {
                case MessageType.Error:
                    title = "¡Error!";
                    break;
                case MessageType.Info:
                    title = "Ojo.";
                    break;
                case MessageType.Success:
                    title = "¡Éxito!";
                    break;
                case MessageType.Warning:
                    title = "¡Atención!";
                    break;
                default:
                    throw new ArgumentOutOfRangeException(nameof(type), type, null);
            }

            PostMessage(new FlashMessage { Title = title, Body = body, Type = type });
        }

        public ActionResult Error(Exception ex)
        {
            return View("Error", ex);
        }

        public ActionResult RedirectToActionPartialView(string actionName)
        {
            return RedirectToActionPartialView(actionName, null, null);
        }

        public ActionResult RedirectToActionPartialView(string actionName, object routeValues)
        {
            return RedirectToActionPartialView(actionName, null, routeValues);
        }

        public ActionResult RedirectToActionPartialView(string actionName, string controllerName)
        {
            return RedirectToActionPartialView(actionName, controllerName, null);
        }

        public ActionResult RedirectToActionPartialView(string actionName, string controllerName, object routeValues)
        {
            string url = Url.Action(actionName, controllerName, routeValues);
            return Content("<script> window.location = '" + url + "'</script>");
        }

        public string GetIpAddress()
        {
            return Request != null ? Request.UserHostAddress : string.Empty;
        }
    }

    public class CargarDatosContext
    {
        //public EvolEntities context { get; set; }
        public HttpSessionStateBase Session { get; set; }
    }
}