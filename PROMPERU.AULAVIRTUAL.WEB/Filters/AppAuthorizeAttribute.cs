using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using PROMPERU.AULAVIRTUAL.WEB.Helpers;
using System.Web.Mvc;
using System.Web.Routing;

namespace PROMPERU.AULAVIRTUAL.WEB.Filters
{
    public class AppAuthorizeAttribute : FilterAttribute, IAuthorizationFilter
    {
        private readonly AppRol[] _acceptedRoles;

        public AppAuthorizeAttribute(params AppRol[] acceptedRoles)
        {
            _acceptedRoles = acceptedRoles;
        }

        public void OnAuthorization(AuthorizationContext filterContext)
        {
            var unauthorized = false;

            try
            {
                var user = filterContext.HttpContext.Session.Get(SessionKey.UsuarioId).ToString();
                var rol = (AppRol)filterContext.HttpContext.Session.Get(SessionKey.Rol);

                if (_acceptedRoles.Length > 0)
                {
                    if (!_acceptedRoles.Contains(rol))
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