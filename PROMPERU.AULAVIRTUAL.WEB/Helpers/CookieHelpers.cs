using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace PROMPERU.AULAVIRTUAL.WEB.Helpers
{
    public class CookieHelpers
    {
        private static HttpContext _context { get { return HttpContext.Current; } }

        #region Get & Set
        public static void Set(SessionKey key, object value)
        {
            if (value == null)
            {
                value = String.Empty;
            }
            var cookie = new HttpCookie(key.ToString())
            {
                Value = value.ToString(),
                Expires = DateTime.Now.AddHours(16),
                HttpOnly = true
            };
            _context.Response.Cookies.Remove(key.ToString());
            _context.Response.Cookies.Add(cookie);
            //_context.Response.SetCookie(cookie);
        }

        public static HttpCookie GetCookie(SessionKey key)
        {
            if (Exists(key))
            {
                return _context.Request.Cookies.Get(key.ToString());
            }
            return null;
        }

        public static String GetValue(SessionKey key)
        {
            try
            {
                var cookie = GetCookie(key);
                return cookie.Value;
                //return cookie != null ? _context.Server.HtmlEncode(cookie.Value).Trim() : String.Empty;
            }
            catch (Exception)
            {
                return String.Empty;
            }

        }

        public static Boolean Exists(SessionKey key)
        {
            return _context.Request.Cookies[key.ToString()] != null;
        }

        private static Boolean IsExpired(SessionKey key)
        {
            if (Exists(key))
            {
                var cookie = GetCookie(key);
                return cookie.Expires < DateTime.Now;
            }
            return true;
        }
        #endregion

        #region Delete
        public static void Delete(SessionKey key)
        {
            if (Exists(key))
            {
                var cookie = new HttpCookie(key.ToString()) { Expires = DateTime.Now.AddDays(-1) };
                _context.Response.Cookies.Remove(key.ToString());
                _context.Response.Cookies.Add(cookie);
            }
        }

        public static void DeleteAll()
        {
            var sessionKeyNames = Enum.GetNames(typeof(SessionKey)).ToList();

            for (int i = 0; i <= _context.Request.Cookies.Count - 1; i++)
            {
                var name = _context.Request.Cookies[i].Name;
                if (_context.Request.Cookies[i] != null && sessionKeyNames.Any(x => x.Equals(name)))
                    Delete(name.ToEnum<SessionKey>());

            }
        }

        #endregion



    }
}