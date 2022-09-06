using System;
using System.Collections.Generic;
using System.Drawing;
using System.Drawing.Imaging;
using System.Linq;
using System.Runtime.Caching;
using System.Security.Cryptography;
using System.Text;
//using System.Data.Entity;
using System.Web.Mvc;
using System.Web;
using PROMPERU.AULAVIRTUAL.BE.CMS;
using PROMPERU.AULAVIRTUAL.DA;

namespace PROMPERU.AULAVIRTUAL.WEB.Helpers
{
    public  static partial class CmsHelpers
    {
        public const string PREFIX_TRADUCCION = "i18n";
        public const string IDIOMA_DEFECTO = "es";
        

        public static void TryInitCache()
        {
            CMSDA cMSDA = new CMSDA();
            var cache = MemoryCache.Default;
            var cacheInitKey = PREFIX_TRADUCCION;

            if (!cache.Contains(cacheInitKey))
            {
                cache[cacheInitKey] = "init";

                //var context = new EvolEntities();
                var traducciones = cMSDA.ListarLabel();

                foreach (var traduccion in traducciones)
                {
                    var cacheKey = PREFIX_TRADUCCION + traduccion.Codigo;
                    cache[cacheKey] = traduccion;
                }
            }
        }

        public static CMS_Label GetEntity(String key)
        {
            TryInitCache();
            CMSDA cMSDA = new CMSDA();
            var cache = MemoryCache.Default;
            var cacheKey = PREFIX_TRADUCCION + key;

            if (!cache.Contains(cacheKey))
            {
                List<CMS_Label> cmsLabels = cMSDA.ListarLabel();
                //var context = new EvolEntities();
                var label = cmsLabels.Where(x => x.Codigo == key).ToList().FirstOrDefault(x => x.Codigo == key); //Para evitar case insensitive

                if (label == null)
                {
                    label = new CMS_Label();
                    label.Codigo = key;
                    label.Texto = key;
                    label.EsRaw = false;
                    label.FechaModificacion = DateTime.Now;
                    //context.CMS_Label.Add(label);
                    //context.SaveChanges();
                }

                cache[cacheKey] = label;
            }

            return (CMS_Label)cache[cacheKey];
        }

        public static void Remove(String key)
        {
            var cache = MemoryCache.Default;
            var cacheKey = PREFIX_TRADUCCION + key;

            if (cache.Contains(cacheKey))
                cache.Remove(cacheKey);
        }

        public static String GetText(String key)
        {
            return GetText(key, (HttpContext.Current.Session["lang"] ?? IDIOMA_DEFECTO).ToSafeString());
        }

        public static String GetText(String key, String lang)
        {
            var traduccion = GetEntity(key);

            switch (lang)
            {
                case "es": return traduccion.Texto;
                case "xx": return "XXX";
                default: return "IDIOMA INCORRECTO: " + lang;
            }
        }

        public static object Label(this HtmlHelper helper, String key, String lang, params String[] vals)
        {
            var entity = GetEntity(key);
            if (entity.EsRaw)
                return LabelRaw(helper, key, lang, vals);
            else
                return LabelText(helper, key, lang, vals);
        }

        public static object LabelText(this HtmlHelper helper, String key, String lang, params String[] vals)
        {
            var result = "";

            if (vals.Length > 0)
                result = String.Format(GetText(key, lang), vals);
            else
                result = GetText(key, lang);

            return result;
        }

        public static object LabelRaw(this HtmlHelper helper, String key, String lang, params String[] vals)
        {
            var result = "";

            if (vals.Length > 0)
                result = String.Format(GetText(key, lang), vals);
            else
                result = GetText(key, lang);

            return helper.Raw(result);
        }

        public static object Label(this HtmlHelper helper, String key)
        {
            return Label(helper, key, (HttpContext.Current.Session["lang"] ?? IDIOMA_DEFECTO).ToSafeString());
        }

        public static object LabelText(this HtmlHelper helper, String key)
        {
            return Label(helper, key, (HttpContext.Current.Session["lang"] ?? IDIOMA_DEFECTO).ToSafeString());
        }

        public static object LabelRaw(this HtmlHelper helper, String key)
        {
            return Label(helper, key, (HttpContext.Current.Session["lang"] ?? IDIOMA_DEFECTO).ToSafeString());
        }

        public static String GetIdiomaActual(this HttpSessionStateBase session)
        {
            return (session["lang"] ?? IDIOMA_DEFECTO).ToSafeString();
        }

        public static Boolean EsIdiomaActual(this HttpSessionStateBase session, String lang)
        {
            return GetIdiomaActual(session) == lang;
        }

        public static String Label(this String key)
        {
            return GetText(key);
        }
        public static String Label(this String key, String lang)
        {
            return GetText(key, lang);
        }

        public static String GetCmsResourceUrl(this UrlHelper helper, String ruta)
        {
            return helper.Content("~/Content/CMS_NEW/Recursos/" + ruta);
        }

        public static String GetCmsPageUrl(this UrlHelper helper, String pagina, object parametros = null)
        {
            var dictParametros = new System.Web.Routing.RouteValueDictionary(parametros);
            if (!dictParametros.ContainsKey("url"))
                dictParametros.Add("url", pagina);

            return helper.RouteUrl("CMS", dictParametros);
        }

        public static String GetCmsMenuItemUrl(this UrlHelper helper, CMS_MenuItem item, object parametros = null)
        {
            if (item.PageId != null)
            {
                var dictParametros = new System.Web.Routing.RouteValueDictionary(parametros);
                if (!dictParametros.ContainsKey("url"))
                    dictParametros.Add("url", item.CMS_Page.Url);

                //return helper.RouteUrl("CMS", dictParametros) + (item.CustomUrl ?? "");

                return "/" + (item.CustomUrl ?? "") + item.CMS_Page.Url;

            }

            return item.CustomUrl;
        }
    }
}