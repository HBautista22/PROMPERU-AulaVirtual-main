using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using PROMPERU.AULAVIRTUAL.WEB.Helpers;
using System.Web.Mvc;
using System.Web.Routing;
using PROMPERU.AULAVIRTUAL.WEB.Logic;

namespace PROMPERU.AULAVIRTUAL.WEB.Filters
{
    public class SessionRestoreAttribute : FilterAttribute, IAuthorizationFilter
    {
        public SessionRestoreAttribute()
        {
        }

        public void OnAuthorization(AuthorizationContext filterContext)
        {
            if (!filterContext.HttpContext.Session.IsLoggedIn())
            {
                if (filterContext.Controller.ControllerContext.RouteData.Values["action"].Equals("ExternLogin"))
                {
                    HttpContext.Current.Session.Clear();
                    CookieHelpers.DeleteAll();
                    return;
                }

                if (CookieHelpers.Exists(SessionKey._appEvol))
                {
                    try
                    {
                        HttpContext.Current.Session.RestoreSessionFromCookie();
                    }
                    catch (Exception)
                    {
                        CookieHelpers.DeleteAll();
                        HttpContext.Current.Session.Clear();
                    }
                }
                else
                {
                    HttpContext.Current.Session.Clear();
                    CookieHelpers.DeleteAll();
                }
            }
        }

    }
}