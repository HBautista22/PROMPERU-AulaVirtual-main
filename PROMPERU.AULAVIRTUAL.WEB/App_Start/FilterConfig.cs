using System.Web.Mvc;
using PROMPERU.AULAVIRTUAL.WEB.Helpers;

namespace PROMPERU.AULAVIRTUAL.WEB
{
    public class FilterConfig
    {
        public static void RegisterGlobalFilters(GlobalFilterCollection filters)
        {
            filters.Add(new HandleErrorAttribute());
            //filters.Add(new SessionRestoreAttribute());
            bool allowHttps = System.Configuration.ConfigurationManager.AppSettings["ALLOW_ONLY_HHTPS"].ToBoolean();
            if(allowHttps)
                filters.Add(new RequireHttpsAttribute());
        }
    }
}