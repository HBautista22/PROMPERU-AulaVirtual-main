using Newtonsoft.Json;
using PROMPERU.AULAVIRTUAL.BE.CMS;
using PROMPERU.AULAVIRTUAL.BL;
using PROMPERU.AULAVIRTUAL.WEB.Logic;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.SessionState;

namespace PROMPERU.AULAVIRTUAL.WEB.Helpers
{
    public enum AppRol
    {
        Administrador,
        Ingeniero,
        Proveedor,
        Empresa,
        Supervisor,
        Alumno,
        Asesor,
        Profesor
    }

    public enum AppCursoMaestro
    {
        NoTieneCurso,
        TieneCurso,
    }

    public enum SessionKey
    {
        Usuario,
        UsuarioId,
        EmpresaId,
        NombreCompleto,
        Email,
        Rol,
        RolCompleto,
        RazonSocial,
        EmpresasUsuario,
        CursosNewUsuarios,
        CursosUsuasrios,
        _appEvol,
        CursoMaestro,
        PrimerIngreso,
        MenuCMS,
        MenuCMSDetalle
    }


    public static class SessionHelpers
    {
        #region SessionTypes
        public static List<Tuple<SessionKey, Type>> lstCast { get; set; }

        public static void SetSessionTypes()
        {
            lstCast = new List<Tuple<SessionKey, Type>>();
            lstCast.Add(new Tuple<SessionKey, Type>(SessionKey.EmpresasUsuario, typeof(List<Tuple<Int32, String>>)));
            lstCast.Add(new Tuple<SessionKey, Type>(SessionKey.CursosNewUsuarios, typeof(List<Tuple<Int32, String>>)));
        }
        #endregion

        #region TieneRol
        public static Boolean TieneRol(this HttpSessionState Session, AppRol Rol)
        {
            return Session.GetRol() == Rol;
        }

        public static Boolean TieneRol(this HttpSessionStateBase Session, AppRol Rol)
        {
            return Session.GetRol() == Rol;
        }

        public static Boolean TieneRol(this HttpSessionState Session, String Rol)
        {
            return Get(Session, SessionKey.RolCompleto).ToString() == Rol;
        }

        public static Boolean TieneRol(this HttpSessionStateBase Session, String Rol)
        {
            return Get(Session, SessionKey.RolCompleto).ToString() == Rol;
        }

        #endregion

        #region GetUsuarioId
        public static Int32 GetUsuarioId(this HttpSessionState Session)
        {
            return Get(Session, SessionKey.UsuarioId).ToInteger();
        }

        public static Int32 GetUsuarioId(this HttpSessionStateBase Session)
        {
            return Get(Session, SessionKey.UsuarioId).ToInteger();
        }
        #endregion

        #region GetEmpresaId
        public static Int32 GetEmpresaId(this HttpSessionState Session)
        {
            return Get(Session, SessionKey.EmpresaId).ToInteger();
        }

        public static Int32 GetEmpresaId(this HttpSessionStateBase Session)
        {
            return Get(Session, SessionKey.EmpresaId).ToInteger();
        }
        #endregion

        #region GetRazonSocial
        public static String GetRazonSocial(this HttpSessionState Session)
        {
            return (String)Get(Session, SessionKey.RazonSocial);
        }

        public static String GetRazonSocial(this HttpSessionStateBase Session)
        {
            return (String)Get(Session, SessionKey.RazonSocial);
        }
        #endregion

        #region GetEmpresasUsuario
        public static List<Tuple<int, String>> GetEmpresasUsuario(this HttpSessionState Session)
        {
            return (List<Tuple<int, String>>)Get(Session, SessionKey.EmpresasUsuario);
        }

        public static List<Tuple<int, String>> GetEmpresasUsuario(this HttpSessionStateBase Session)
        {
            return (List<Tuple<int, String>>)Get(Session, SessionKey.EmpresasUsuario);
        }
        #endregion

        #region GetNotificaionesCursosUsuario
        public static List<Tuple<int, String>> GetNotificaionesCursosUsuario(this HttpSessionState Session)
        {
            return (List<Tuple<int, String>>)Get(Session, SessionKey.CursosNewUsuarios);
        }

        public static List<Tuple<int, String>> GetNotificaionesCursosUsuario(this HttpSessionStateBase Session)
        {
            return (List<Tuple<int, String>>)Get(Session, SessionKey.CursosNewUsuarios);
        }

        public static List<Tuple<int, String>> GetNotificaionesCursosUsuarioLeidos(this HttpSessionState Session)
        {
            return (List<Tuple<int, String>>)Get(Session, SessionKey.CursosUsuasrios);
        }

        public static List<Tuple<int, String>> GetNotificaionesCursosUsuarioLeidos(this HttpSessionStateBase Session)
        {
            return (List<Tuple<int, String>>)Get(Session, SessionKey.CursosUsuasrios);
        }
        #endregion

        #region GetNombreCompleto
        public static String GetNombreCompleto(this HttpSessionState Session)
        {
            return Get(Session, SessionKey.NombreCompleto).ToString();
        }

        public static String GetNombreCompleto(this HttpSessionStateBase Session)
        {
            return Get(Session, SessionKey.NombreCompleto).ToString();
        }
        #endregion

        #region IsLoggedIn
        public static Boolean IsLoggedIn(this HttpSessionState Session)
        {
            return Get(Session, SessionKey.UsuarioId) != null;
        }

        public static Boolean IsLoggedIn(this HttpSessionStateBase Session)
        {
            return Get(Session, SessionKey.UsuarioId) != null;
        }
        #endregion

        #region GetEmail
        public static String GetEmail(this HttpSessionState Session)
        {
            return Get(Session, SessionKey.Email).ToString();
        }

        public static String GetEmail(this HttpSessionStateBase Session)
        {
            return Get(Session, SessionKey.Email).ToString();
        }
        #endregion

        #region GetRol
        public static AppRol? GetRol(this HttpSessionState Session)
        {
            return (AppRol?)Get(Session, SessionKey.Rol);
        }

        public static AppRol? GetRol(this HttpSessionStateBase Session)
        {
            return (AppRol?)Get(Session, SessionKey.Rol);
        }
        #endregion

        #region GetMenu
        //public static List<CMS_MenuCMS> Get_MenuCMs() {
        //    CMSBL cmsbl = new CMSBL();

        //    List<CMS_MenuCMS> cMS_MenuCMs = cmsbl.ObtenerMenuCMS();
        //    return cMS_MenuCMs;
        //}

        public static List<Tuple<int, String>> Get_MenuCMs(this HttpSessionStateBase Session)
        {
            return (List<Tuple<int, String>>)Get(Session, SessionKey.MenuCMS);
        }
        public static List<Tuple<int, String>> Get_MenuCMsDetalle(this HttpSessionStateBase Session)
        {
            return (List<Tuple<int, String>>)Get(Session, SessionKey.MenuCMSDetalle);
        }


        #endregion


        #region GetCursoMaestro
        public static AppCursoMaestro? GetCursoMaestro(this HttpSessionState Session)
        {
            return (AppCursoMaestro?)Get(Session, SessionKey.CursoMaestro);
        }

        public static AppCursoMaestro? GetCursoMaestro(this HttpSessionStateBase Session)
        {
            return (AppCursoMaestro?)Get(Session, SessionKey.CursoMaestro);
        }
        #endregion


        #region GetRolCompleto
        public static String GetRolCompleto(this HttpSessionState Session)
        {
            return (String)Get(Session, SessionKey.RolCompleto);
        }

        public static String GetRolCompleto(this HttpSessionStateBase Session)
        {
            return (String)Get(Session, SessionKey.RolCompleto);
        }
        #endregion

        #region Private

        private static object Get(HttpSessionState Session, String Key)
        {
            return Session[Key];
        }

        private static void Set(HttpSessionState Session, String Key, object Value)
        {
            Session[Key] = Value;
        }

        private static bool Exists(HttpSessionState Session, String Key)
        {
            return Session[Key] != null;
        }

        private static object Get(HttpSessionStateBase Session, String Key)
        {
            return Session[Key];
        }

        private static void Set(HttpSessionStateBase Session, String Key, object Value)
        {
            Session[Key] = Value;
        }

        private static bool Exists(HttpSessionStateBase Session, String Key)
        {
            return Session[Key] != null;
        }

        #endregion

        #region Getters setters GlobalKey
        //HttpSessionState
        public static object Get(this HttpSessionState Session, SessionKey Key)
        {
            return Get(Session, Key.ToString());
        }

        public static void Set(this HttpSessionState Session, SessionKey Key, object Value)
        {
            Set(Session, Key.ToString(), Value);
        }

        public static bool Exists(this HttpSessionState Session, SessionKey Key)
        {
            return Exists(Session, Key.ToString());
        }

        //HttpSessionStateBase
        public static object Get(this HttpSessionStateBase Session, SessionKey Key)
        {
            return Get(Session, Key.ToString());
        }

        public static void Set(this HttpSessionStateBase Session, SessionKey Key, object Value)
        {
            Set(Session, Key.ToString(), Value);
        }

        public static bool Exists(this HttpSessionStateBase Session, SessionKey Key)
        {
            return Exists(Session, Key.ToString());
        }

        #endregion
        /// <summary>
        /// Serializa Coockies Dependiendo de los 
        /// </summary>
        /// <param name="Session"></param>
        public static void SetCookie(this HttpSessionStateBase Session)
        {
            try
            {
                var lstSessionKey = Session.Keys;
                var sessionKeyNames = Enum.GetNames(typeof(SessionKey)).ToList();
                var dictSessionObject = sessionKeyNames.ToDictionary(x => x, v => new object());

                foreach (var key in dictSessionObject.Keys.ToList())
                    dictSessionObject[key] = null;

                for (int i = 0; i <= Session.Count - 1; i++)
                {
                    var key = Session.Keys[i];
                    if (sessionKeyNames.Any(x => x.Equals(key)))
                    {
                        if (key.Equals(SessionKey.Rol.ToString()))
                            dictSessionObject[key] = Session[key].ToString();
                        else
                            dictSessionObject[key] = Session[key];
                    }
                }

                var result = JsonConvert.SerializeObject(dictSessionObject, new JsonSerializerSettings() { PreserveReferencesHandling = PreserveReferencesHandling.Objects, ReferenceLoopHandling = ReferenceLoopHandling.Ignore, ObjectCreationHandling = ObjectCreationHandling.Reuse });

                var encriptacion = new Encriptacion();
                var resultEncriptado = encriptacion.Encriptar(result);
                CookieHelpers.Set(SessionKey._appEvol, resultEncriptado);
                //CookieHelpers.GetCookie(SessionKey._appEvol).Secure = true;
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }

        public static void RestoreSessionFromCookie(this HttpSessionState Session)
        {
            try
            {
                SetSessionTypes();

                var encriptacion = new Encriptacion();
                var coockieValue = encriptacion.Desencriptar(CookieHelpers.GetValue(SessionKey._appEvol));

                var dictSessionObject = new Dictionary<string, object>();
                dictSessionObject = JsonConvert.DeserializeObject<Dictionary<String, object>>(coockieValue);

                foreach (var item in dictSessionObject)
                {
                    if (item.Value != null)
                    {
                        var sessionKey = item.Key.ToEnum<SessionKey>();
                        if (sessionKey.Equals(SessionKey._appEvol))
                            continue;
                        else if (sessionKey.Equals(SessionKey.Rol))
                            Set(Session, sessionKey, item.Value.ToString().ToEnum<AppRol>());
                        else if (lstCast.Any(x => x.Item1 == sessionKey))
                        {
                            var cast = lstCast.FirstOrDefault(x => x.Item1 == sessionKey);
                            var value = JsonConvert.DeserializeObject(item.Value.ToString(), cast.Item2);
                            Set(Session, sessionKey, value);
                        }
                        else
                            Set(Session, sessionKey, item.Value);
                    }
                }
            }
            catch (Exception)
            {
                Session.Clear();
                CookieHelpers.DeleteAll();
            }
        }
    }
}