using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using PROMPERU.AULAVIRTUAL.WEB.Helpers;
using System.Web.Mvc;
using System.Web.Routing;

namespace PROMPERU.AULAVIRTUAL.WEB.Filters
{
    public class AppAuthorizeCursosMaestros : FilterAttribute, IAuthorizationFilter
    {

        private readonly AppCursoMaestro[] _acceptedPermisos;

        public AppAuthorizeCursosMaestros(params AppCursoMaestro[] acceptedPermisos)
        {
            _acceptedPermisos = acceptedPermisos;
        }

        public void OnAuthorization(AuthorizationContext filterContext)
        {
            var unauthorized = false;

            try
            {
                var user = filterContext.HttpContext.Session.Get(SessionKey.UsuarioId).ToString();
                var cursoMaestro = (AppCursoMaestro)filterContext.HttpContext.Session.Get(SessionKey.CursoMaestro);

                if (_acceptedPermisos.Length > 0)
                {
                    if (!_acceptedPermisos.Contains(cursoMaestro))
                    {
                        unauthorized = true;
                    }
                }
            }
            catch (Exception ex)
            {
                unauthorized = true;
            }

            if (unauthorized)
            {
                if (filterContext.HttpContext.Session.Get(SessionKey.UsuarioId) == null)
                {
                    filterContext.Result = new RedirectToRouteResult(new RouteValueDictionary(new { controller = "Login", action = "Index" }));
                    CookieHelpers.DeleteAll();
                }
                else
                    filterContext.Result = new ViewResult() { ViewName = filterContext.HttpContext.Request.IsAjaxRequest() ? "_PermisoInsuficienteModal" : "PermisoInsuficiente" };
            }
        }

    }
}