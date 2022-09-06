using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using System.Web.Routing;
using System.Linq.Expressions;
using System.Web.Mvc.Html;
using System.Dynamic;

namespace PROMPERU.AULAVIRTUAL.WEB.Helpers
{
    public static class DictionaryHelpers
    {
        public static TValue GetValueOrDefault<TKey, TValue>(this Dictionary<TKey, TValue> diccionario, TKey key, TValue defaultValue)
        {
            if (diccionario.ContainsKey(key))
                return diccionario[key];
            return defaultValue;
        }

        public static ExpandoObject ToExpando(this object anonymousObject)
        {
            IDictionary<string, object> anonymousDictionary = HtmlHelper.AnonymousObjectToHtmlAttributes(anonymousObject);
            IDictionary<string, object> expando = new ExpandoObject();
            foreach (var item in anonymousDictionary)
                expando.Add(item);
            return (ExpandoObject)expando;
        }
    }
}