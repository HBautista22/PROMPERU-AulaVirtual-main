using System;
using System.Collections.Generic;
//using System.Data.Entity;
using System.Linq;
using System.Text;
using System.Transactions;
using System.Web;
using System.Web.Configuration;
using System.Web.Mvc;
using PROMPERU.AULAVIRTUAL.WEB.Helpers;
using PROMPERU.AULAVIRTUAL.WEB.Logic;
//using PROMPERU.AULAVIRTUAL.WEB.Models;
using PROMPERU.AULAVIRTUAL.BE.USUARIO;
using PROMPERU.AULAVIRTUAL.BE.CURSOS;
using PROMPERU.AULAVIRTUAL.WEB.ViewModel.Home;
using PROMPERU.AULAVIRTUAL.BL;
using PROMPERU.AULAVIRTUAL.BE.CMS;

namespace PROMPERU.AULAVIRTUAL.WEB.Controllers
{
    public class LoginController : BaseController
    {
        private CMSBL CMSBL = new CMSBL();
        private UsuarioBL usuarioBL = new UsuarioBL();
        private CursosBL cursosBL = new CursosBL();
        private EmpresaBL empresaBL = new EmpresaBL();
        private UsuarioMultiEmpresaBL usuarioMultiEmpresaBL = new UsuarioMultiEmpresaBL();

        //GET: lOGIN
        public ActionResult Index(string returnUrl = "", bool confirmacion = false)
        {
            if (Session.IsLoggedIn())
            {
                Usuario usuario = usuarioBL.ObtenerUsuarioPorId(Session.GetUsuarioId());
                usuario.UltimoAcceso = DateTime.Now;
                usuarioBL.InsertarUsuarioUltimoAcceso(usuario);

                return Dashboard(returnUrl);
            }

            LoginViewModel model = new LoginViewModel
            {
                ReturnUrl = returnUrl
            };

            ViewBag.URLPortal = WebConfigurationManager.AppSettings["PORTAL.Url"].ToString();

            if (confirmacion)
            {
                PostMessage(MessageType.Success, "Confirmación satisfactoria");
            }
            return View(model);
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Index(LoginViewModel model)
        {
            if (!ModelState.IsValid)
            {
                return View(model);
            }

            Session.Clear();
            Usuario usuario = new Usuario(); 

            if (int.TryParse(model.Codigo.ToLower(), out _))
            {
                usuario = usuarioBL.ObtenerUsuarioPorId(Convert.ToInt32(model.Codigo.ToLower()));
            }

            if(usuario.UsuarioId==0)
            {
                usuario = usuarioBL.ObtenerUsuarioPorEmail(model.Codigo.ToLower());
            }

            if (usuario.UsuarioId == 0 || String.IsNullOrEmpty(usuario.Password) || model.Contrasena != usuario.Password)
            {
                PostMessage(MessageType.Error, "Usuario y/o Contraseña Incorrectos");
            }
            else if (usuario.Estado == ConstantHelpers.ESTADO.ACTIVO)
            {

                usuario.UltimoAcceso = DateTime.Now;
                usuarioBL.InsertarUsuarioUltimoAcceso(usuario);

                AppRol rol = AppRol.Alumno;
                switch (usuario.Rol)
                {
                    case ConstantHelpers.ROL.ADMINISTRADOR:
                        {
                            rol = AppRol.Administrador; break;
                        }
                    case ConstantHelpers.ROL.SUPERVISOR:
                        {
                            rol = AppRol.Supervisor; break;
                        }
                    case ConstantHelpers.ROL.ALUMNO:
                        {
                            rol = AppRol.Alumno; break;
                        }
                    case ConstantHelpers.ROL.EMPRESA:
                        {
                            rol = AppRol.Empresa; break;
                        }
                    case ConstantHelpers.ROL.ASESOR:
                        {
                            rol = AppRol.Asesor; break;
                        }
                    case ConstantHelpers.ROL.PROFESOR:
                        {
                            rol = AppRol.Profesor; break;
                        }
                }

                //context.Entry(usuario).State = EntityState.Detached;

                Session.Set(SessionKey.PrimerIngreso,false);
                int cantidadLogUsuario = usuarioBL.ObtenerLogAccesosUsuario(usuario.UsuarioId).Count;
                if (cantidadLogUsuario < 1)
                {
                    Session.Set(SessionKey.PrimerIngreso, true);
                }

                Session.Set(SessionKey.Usuario, usuario);

                Session.Set(SessionKey.UsuarioId, usuario.UsuarioId);
                Session.Set(SessionKey.Email, usuario.Email);
                Session.Set(SessionKey.NombreCompleto, usuario.Nombre + " " + usuario.Apellido);
                Session.Set(SessionKey.Rol, rol);
                Session.Set(SessionKey.RolCompleto, usuario.Rol);

                //aqui se guarda el estado que verificar si un alumno cuenta con al menos un curso maestro asignado a un grupo

                AppCursoMaestro CursoMaestro = AppCursoMaestro.NoTieneCurso;

                if (usuarioBL.ConsultarGrupo(usuario.UsuarioId) > 0)
                {
                    CursoMaestro = AppCursoMaestro.TieneCurso;
                }


                Session.Set(SessionKey.CursoMaestro, CursoMaestro);

                var menuAccesos = new List<CMS_MenuCMS>();
                menuAccesos = CMSBL.ObtenerMenuCMS();

                if (menuAccesos.Count() > 0) {
                    List<Tuple<int, String>> MenuCMS = new List<Tuple<int, String>>();
                    foreach (var item in menuAccesos.Where(x => x.Perfil == usuario.Rol && x.Activo == true && x.MenuIDPadre == null).ToList()) {
                        var tupla = new Tuple<int, String>(item.MenuID, string.Concat(item.URL, "@", item.Nombre,"@",item.Seccion,"@",item.Class));
                        MenuCMS.Add(tupla);
                    }
                    Session.Set(SessionKey.MenuCMS, MenuCMS);


                    List<Tuple<int, String>> MenuCMSDetalle = new List<Tuple<int, String>>();
                    foreach (var item in menuAccesos.Where(x => x.Perfil == usuario.Rol && x.Activo == true && x.MenuIDPadre != null).ToList())
                    {
                        var tupla = new Tuple<int, String>((int)item.MenuIDPadre, string.Concat(item.URL, "@", item.Nombre, "@", item.Seccion, "@", item.Class));
                        MenuCMSDetalle.Add(tupla);
                    }
                    Session.Set(SessionKey.MenuCMSDetalle, MenuCMSDetalle);

                }




                var fecha = DateTime.Now.AddDays(-15);

                var CursosNuevos = new List<CursoOnline>();

                CursosNuevos = cursosBL.ListarCursosUsuarioDisponible(usuario.UsuarioId)
                    //.Where(a => a.FechaCreacion >= fecha)
                    .OrderByDescending(a => a.FechaCreacion)
                    //.Take(3)
                    .ToList();


                //var CursosNuevos = context.CursoOnline.Where(a => a.FechaCreacion >= fecha && a.MatriculaCursoOnline.All(x => x.UsuarioId != usuario.UsuarioId))
                //.OrderByDescending(a => a.FechaCreacion)
                //.Take(3)
                //.ToList();



                //CursosNuevos[0].FechaCreacion 


                NotificacionesBL notificacionesBL = new NotificacionesBL();

                List<Notificaciones> notificaciones = new List<Notificaciones>();

                notificaciones = notificacionesBL.ListarNotificaciones().Where(a => a.Estado == Helpers.ConstantHelpers.ESTADO.ACTIVO && a.TipoNotificaciones.Codigo == "GNR").ToList();

                //foreach (var item in notificaciones)
                //{
                //var tupla = new Tuple<int, String>(item.NotificacionId, string.Concat(item.FechaRegistro.ToString("d/MMM"), "@", item.Nombre, "@", "NO"));
                //}





                if (CursosNuevos.Count() > 0)
                {
                    int contador = 0;
                    List<Tuple<int, String>> NotificacionesNuevas = new List<Tuple<int, String>>();
                    List<Tuple<int, String>> NotificacionesAntiguas = new List<Tuple<int, String>>();
                    foreach (var item in CursosNuevos)
                    {
                        Notificaciones nuevanotifacion = new Notificaciones();

                        nuevanotifacion.NotificacionId = item.CursoOnlineId;
                        nuevanotifacion.FechaRegistro = (DateTime)item.FechaCreacion;
                        nuevanotifacion.Nombre = item.Nombre;
                        nuevanotifacion.Estado = "CU";
                        notificaciones.Add(nuevanotifacion);


                    }
                    foreach (var item in notificaciones.OrderByDescending(x => x.FechaRegistro))
                    {
                        //var tupla = new Tuple<int, String>(item.NotificacionId, string.Concat(item.FechaRegistro.ToString("d/MMM"), "@", item.Nombre, "@", "NO"));

                        contador++;

                        var tupla = new Tuple<int, String>(item.NotificacionId, string.Concat(item.FechaRegistro.ToString("d/MMM"), "@", item.Nombre, "@", item.Estado));
                        if (item.FechaRegistro >= fecha && contador <= 3)
                        {
                            NotificacionesNuevas.Add(tupla);
                            NotificacionesAntiguas.Add(tupla);
                        }
                        else
                        {
                            NotificacionesAntiguas.Add(tupla);

                        }
                       

                    }


                    Session.Set(SessionKey.CursosNewUsuarios, NotificacionesNuevas);
                    Session.Set(SessionKey.CursosUsuasrios, NotificacionesAntiguas);
                }





                //if (CursosNuevos.Count() > 0)
                //{
                //    int contador = 0;
                //    List<Tuple<int, String>> NotificacionesNuevas = new List<Tuple<int, String>>();
                //    List<Tuple<int, String>> NotificacionesAntiguas = new List<Tuple<int, String>>();
                //    foreach (var item in CursosNuevos)
                //    {
                //        Notificaciones nuevanotifacion = new Notificaciones();

                //        nuevanotifacion.NotificacionId = item.CursoOnlineId;
                //        nuevanotifacion.FechaRegistro =(DateTime)item.FechaCreacion;
                //        nuevanotifacion.Nombre = item.Nombre;
                //        nuevanotifacion.Estado = "CU";
                //        notificaciones.Add(nuevanotifacion);
                //        //contador++;

                //       // var tupla = new Tuple<int, String>(item.CursoOnlineId, string.Concat(item.FechaCreacion.Value.ToString("d/MMM"), "@", item.Nombre,"@","CU"));

                //    }



                //    foreach (var item in notificaciones.OrderByDescending(x => x.FechaRegistro))
                //    {
                //        //var tupla = new Tuple<int, String>(item.NotificacionId, string.Concat(item.FechaRegistro.ToString("d/MMM"), "@", item.Nombre, "@", "NO"));

                //        contador++;

                //        var tupla = new Tuple<int, String>(item.NotificacionId, string.Concat(item.FechaRegistro.ToString("d/MMM"), "@", item.Nombre, "@", item.Estado));


                //        if (item.FechaRegistro >= fecha && contador <= 3)
                //        {
                //            NotificacionesNuevas.Add(tupla);
                //            NotificacionesAntiguas.Add(tupla);
                //        }
                //        else
                //        {
                //            NotificacionesAntiguas.Add(tupla);

                //        }

                //    }

                //    Session.Set(SessionKey.CursosNewUsuarios, NotificacionesNuevas);
                //    Session.Set(SessionKey.CursosUsuasrios, NotificacionesAntiguas);
                //}

                //Para multi Empresa
                if (usuario.EmpresaId!=0)
                {
                    
                    var empresa = new Empresa();
                    //var empresa  = context.Empresa.FirstOrDefault(x => x.EmpresaId == usuario.EmpresaId);
                    empresa = empresaBL.ListarEmpresa().FirstOrDefault(x => x.EmpresaId == usuario.EmpresaId);
             
                    var empresasUsuario = new List<UsuarioMultiEmpresa>();

                    //var empresasUsuario context.UsuarioMultiEmpresa.Include(x => x.Empresa)
                    //.Where(x => x.UsuarioId == usuario.UsuarioId)
                    //.Select(x => x.Empresa)
                    //.Distinct()
                    //.OrderBy(x => x.RazonSocial)
                    //.ToList();

                    
                    empresasUsuario = usuarioMultiEmpresaBL.ListarUsuarioMultiEmpresa()
                    .Where(x => x.UsuarioId == usuario.UsuarioId)
                    .Distinct()
                    .ToList();


                    if (empresasUsuario.Count() > 0)
                    {
                        var lstEmpresa = new List<UsuarioMultiEmpresa>();

                        //var lstEmpresa = empresasUsuario.Select(x => new Tuple<int, string>(x.EmpresaId, x.RazonSocial)).ToList();
                        lstEmpresa = usuarioMultiEmpresaBL.ListarUsuarioMultiEmpresa();
                        List<Empresa> allEmpresas = empresaBL.ListarEmpresa();

                        List<Tuple<int, string>> tuplas = new List<Tuple<int, string>>();
                        foreach (UsuarioMultiEmpresa ume in lstEmpresa)
                        {
                            Empresa foundEmp = allEmpresas.First(x => x.EmpresaId == ume.EmpresaId);
                            Tuple<int, string> tupla = new Tuple<int, string>(ume.EmpresaId, foundEmp.RazonSocial);
                            tuplas.Add(tupla);
                        }


                        Session.Set(SessionKey.EmpresasUsuario, tuplas.OrderBy((x => x.Item2)));
                    }

                    Session.Set(SessionKey.EmpresaId, usuario.EmpresaId);
                    Session.Set(SessionKey.RazonSocial, empresa.RazonSocial);
                }



                Session.SetCookie();

                //LogUsuario
                using (var ts = new TransactionScope())
                {
                    LogAccesosUsuario log = new LogAccesosUsuario();
                    log.FechaAcceso = DateTime.Now;
                    var browser = Request.Browser;
                    log.DatosNavegador = $"Type: {browser.Type}; {Environment.NewLine} Browser: {browser.Browser}; {Environment.NewLine} Version: {browser.Version}; {Environment.NewLine} MinorVersion : {browser.MinorVersion}; {Environment.NewLine} MajorVersion : {browser.MajorVersion}; {Environment.NewLine} Platform { browser.Platform }";
                    log.UserAgent = Request.ServerVariables["HTTP_USER_AGENT"];
                    log.UsuarioId = usuario.UsuarioId;
                    log.IPAcceso = Request.UserHostAddress;
                    log.TipoAcceso = ConstantHelpers.TIPO_ACCESO.LOGIN;

                    usuarioBL.GrabarLogAccesoUsuario(log);

                    //context.LogAccesosUsuario.Add(log);
                    //context.SaveChanges();
                    ts.Complete();
                }


                //ENVIO DE CORREO DE BIENVENIDAS
                int usuarioId = Session.GetUsuarioId();
                
                int cantidadLog = usuarioBL.ObtenerLogAccesosUsuario(usuarioId).Count;
                if (cantidadLog == 1)
                {
                   // NotificacionesBL notificacionesBL = new NotificacionesBL();

                    Notificaciones configuracion = new Notificaciones();
                    
                    //var configuracion =  context.Notificaciones.Where(a => a.Estado == Helpers.ConstantHelpers.ESTADO.ACTIVO && a.TipoNotificaciones.Codigo == "BND").FirstOrDefault();
                    configuracion = notificacionesBL.ListarNotificaciones().Where(a => a.Estado == Helpers.ConstantHelpers.ESTADO.ACTIVO && a.TipoNotificaciones.Codigo == "BND").FirstOrDefault();

                    if (configuracion != null)
                    {
                        MailSMTP mail = new MailSMTP();
                        var direccionTemplate = Server.MapPath("~/Views/Shared/EditorTemplates/_TemplateCorreoBienvenida.cshtml");
                        var contenido = configuracion.Contenido;
                        var titulo = configuracion.Titulo;
                        var body = mail.PopulateEmailBody(direccionTemplate, contenido, usuario.Nombre, titulo);
                        mail.SendSingleEmail(usuario.Email, titulo, body);
                    }

                    
                }




                return Dashboard(model.ReturnUrl);
            }
            else if (usuario.Estado == ConstantHelpers.ESTADO.PENDIENTE)
            {
                PostMessage(MessageType.Error, "¡Falta Verificar Cuenta de Correo!");
            }
            else
            {
                PostMessage(MessageType.Error, "Usuario Inactivo");
            }

            return View(model);
        }

        //[HttpPost]
        //[ValidateAntiForgeryToken]
        public ActionResult IndexExterno(LoginViewModel model)
        {
            if (!ModelState.IsValid)
                return RedirectToAction("Index");

            Session.Clear();

            var usuario = usuarioBL.ObtenerUsuarioPorId(Convert.ToInt32(model.Codigo.ToLower()));

            if (usuario == null || String.IsNullOrEmpty(usuario.Password) || model.Contrasena != usuario.Password)
            {
                PostMessage(MessageType.Error, "Usuario y/o Contraseña Incorrectos");
            }
            else if (usuario.Estado == ConstantHelpers.ESTADO.ACTIVO)
            {
                AppRol rol = AppRol.Alumno;
                switch (usuario.Rol)
                {
                    case ConstantHelpers.ROL.ADMINISTRADOR: rol = AppRol.Administrador; break;
                    case ConstantHelpers.ROL.SUPERVISOR: rol = AppRol.Supervisor; break;
                    case ConstantHelpers.ROL.ALUMNO: rol = AppRol.Alumno; break;
                    case ConstantHelpers.ROL.PROFESOR: rol = AppRol.Profesor; break;
                }

                //context.Entry(usuario).State = EntityState.Detached;

                Session.Set(SessionKey.Usuario, usuario);

                Session.Set(SessionKey.UsuarioId, usuario.UsuarioId);
                Session.Set(SessionKey.Email, usuario.Email);
                Session.Set(SessionKey.NombreCompleto, usuario.Nombre + " " + usuario.Apellido);
                Session.Set(SessionKey.Rol, rol);
                Session.Set(SessionKey.RolCompleto, usuario.Rol);

                //Para multi Empresa
                if (usuario.EmpresaId.HasValue)
                {
                    var empresa = new Empresa();

                    //  context.Empresa.FirstOrDefault(x => x.EmpresaId == usuario.EmpresaId);
                    

                    empresa = empresaBL.ListarEmpresa().FirstOrDefault(x => x.EmpresaId == usuario.EmpresaId);

                    var empresasUsuario = new List<UsuarioMultiEmpresa>();
                    //var empresasUsuario = context.UsuarioMultiEmpresa.Include(x => x.Empresa)
                    empresasUsuario = usuarioMultiEmpresaBL.ListarUsuarioMultiEmpresa()
                    .Where(x => x.UsuarioId == usuario.UsuarioId)
                    //.Select(x => x.Empresa)
                    .Distinct()
                    //.OrderBy(x => x.RazonSocial)
                    .ToList();





                    if (empresasUsuario.Count() > 0)
                    {
                        var lstEmpresa = new List<UsuarioMultiEmpresa>();
                        
                        //var lstEmpresa = empresasUsuario.Select(x => new Tuple<int, string>(x.EmpresaId, x.RazonSocial)).ToList();
                        lstEmpresa = usuarioMultiEmpresaBL.ListarUsuarioMultiEmpresa();
                        List<Empresa> allEmpresas = empresaBL.ListarEmpresa();

                        List<Tuple<int, string>> tuplas = new List<Tuple<int, string>>();
                        foreach(UsuarioMultiEmpresa ume in lstEmpresa)
                        {
                            Empresa foundEmp = allEmpresas.First(x => x.EmpresaId == ume.EmpresaId);
                            Tuple<int, string> tupla = new Tuple<int, string>(ume.EmpresaId, foundEmp.RazonSocial);
                            tuplas.Add(tupla);
                        }
                        

                        Session.Set(SessionKey.EmpresasUsuario, tuplas.OrderBy((x=>x.Item2)));
                    }

                    Session.Set(SessionKey.EmpresaId, usuario.EmpresaId);
                    Session.Set(SessionKey.RazonSocial, empresa.RazonSocial);
                }

                Session.SetCookie();

                //LogUsuario
                using (var ts = new TransactionScope())
                {
                    var log = new LogAccesosUsuario();
                    log.FechaAcceso = DateTime.Now;
                    var browser = Request.Browser;
                    log.DatosNavegador = $"Type: {browser.Type}; {Environment.NewLine} Browser: {browser.Browser}; {Environment.NewLine} Version: {browser.Version}; {Environment.NewLine} MinorVersion : {browser.MinorVersion}; {Environment.NewLine} MajorVersion : {browser.MajorVersion}; {Environment.NewLine} Platform { browser.Platform }";
                    log.UserAgent = Request.ServerVariables["HTTP_USER_AGENT"];
                    log.UsuarioId = usuario.UsuarioId;
                    log.IPAcceso = Request.UserHostAddress;
                    log.TipoAcceso = ConstantHelpers.TIPO_ACCESO.LOGIN;


                    //context.LogAccesosUsuario.Add(log);
                    //context.SaveChanges();
                    usuarioBL.GrabarLogAccesoUsuario(log);

                    ts.Complete();
                }

                return Dashboard(model.ReturnUrl);
            }
            else
            {
                PostMessage(MessageType.Error, "Usuario Inactivo");
            }

            return RedirectToAction("Index");
        }

        /// <summary>
        /// 
        /// </summary>
        /// <param name="e">EmpresaId</param>
        /// <param name="r">Ruc</param>
        /// <param name="s">Nombre de empresa</param>
        /// <param name="n">Nombre de usuario</param>
        /// <param name="a">Apellido de usuario</param>
        /// <param name="u">UsuarioId</param>
        /// <param name="g">Grupo rol</param>
        /// <param name="d">DateTime.Utc.Ticks de creación</param>
        /// <param name="k">Key</param>
        /// <returns></returns>
        public ActionResult ExternLogin(String m, String e, String r, String s, String n, String a, String u, String g, String c, String d, String k)
        {
            //Si funciona, no se toca holi :v
            var es_valido = true;

            var encriptacion_logic = new Encriptacion();
            var fechaCreacionTicket = DateTime.UtcNow;

            try
            {
                m = encriptacion_logic.Desencriptar(m, k, true);
                r = encriptacion_logic.Desencriptar(r, k, true);
                s = encriptacion_logic.Desencriptar(s, k, true);
                e = encriptacion_logic.Desencriptar(e, k, true);
                n = encriptacion_logic.Desencriptar(n, k, true);
                a = encriptacion_logic.Desencriptar(a, k, true);
                u = encriptacion_logic.Desencriptar(u, k, true);
                g = encriptacion_logic.Desencriptar(g, k, true);
                d = encriptacion_logic.Desencriptar(d, k, true);
                c = encriptacion_logic.Desencriptar(c, k, true);
                fechaCreacionTicket = new DateTime(Convert.ToInt64(d), DateTimeKind.Utc);
            }
            catch (Exception ex)
            {
                es_valido = false;
            }

            CookieHelpers.DeleteAll();
            Session.Clear();

            if (DateTime.UtcNow.Subtract(fechaCreacionTicket).TotalSeconds > 60 * 5)
            {
                PostMessage(MessageType.Info, "El link ha expirado. Por favor ingrese nuevamente.");
                es_valido = false;
            }

            var es_empresa = !String.IsNullOrEmpty(e);

            if (String.IsNullOrEmpty(k) || String.IsNullOrEmpty(m))
                es_valido = false;

            if (es_valido && es_empresa)
            {
                if (String.IsNullOrEmpty(r) || String.IsNullOrEmpty(s))
                    es_valido = false;
            }

            if (es_valido && !es_empresa)
            {
                if (String.IsNullOrEmpty(n) || String.IsNullOrEmpty(a) || String.IsNullOrEmpty(u))
                    es_valido = false;
            }

            if (!es_valido)
            {
                return View("PermisoInsuficiente");
            }

            var acceso = false;

            // context.Usuario.FirstOrDefault(x => x.Codigo.ToLower().Equals(m.ToLower()));
            Usuario usuario = usuarioBL.ObtenerUsuarioPorEmail(m);

            var rolPortal = String.Empty;

            switch (g.Trim().ToLower())
            {
                case "usuario empresa": rolPortal = ConstantHelpers.ROL.ALUMNO; break;
                case "administrador empresa": rolPortal = ConstantHelpers.ROL.SUPERVISOR; break;
                case "administrador": rolPortal = ConstantHelpers.ROL.ADMINISTRADOR; break;
                default: rolPortal = ConstantHelpers.ROL.ALUMNO; break;
            }

            if (usuario == null)
            {
                usuario = new Usuario();
                usuario.Codigo = m.ToLower();
                usuario.Nombre = n;
                usuario.Apellido = a;
                usuario.Cargo = c;
                usuario.Email = m.ToLower();
                usuario.Estado = ConstantHelpers.ESTADO.ACTIVO;
                usuario.Rol = rolPortal;

                usuario.Password = new Guid().ToString().Substring(0, 6);

                var empresa = new Empresa();

                //var empresa = context.Empresa.FirstOrDefault(x => x.RUC == r);
                empresa = empresaBL.ListarEmpresa().FirstOrDefault(x => x.RUC == r);

                if (empresa == null)
                {
                    empresa = new Empresa();

                    //empresa = context.Empresa.Add(empresa);
                    empresaBL.InsertarEmpresa(empresa);
                }

                empresa.RazonSocial = s;
                empresa.RUC = r;
                usuario.Empresa = empresa;

                //context.Usuario.Add(usuario);
                usuarioBL.InsertarUsuario(usuario);
                //context.SaveChanges();
                acceso = true;
            }
            else
            {
                usuario.Nombre = n;
                usuario.Apellido = a;
                usuario.Cargo = c;
                usuario.Estado = ConstantHelpers.ESTADO.ACTIVO;

                var empresa = new Empresa();
                
                //var empresa = context.Empresa.FirstOrDefault(x => x.RUC == r);
                empresa = empresaBL.ListarEmpresa().FirstOrDefault(x => x.RUC == r);

                if (empresa == null)
                {
                    empresa = new Empresa();

                    //empresa = context.Empresa.Add(empresa);
                    empresaBL.InsertarEmpresa(empresa);
                }

                empresa.RazonSocial = s;
                empresa.RUC = r;

                usuario.Rol = rolPortal;
                usuario.Empresa = empresa;

                
                //context.SaveChanges();
                empresaBL.ActualizarEmpresa(empresa);
                acceso = true;
            }

            if (acceso)
            {
                Session.Clear();
                AppRol rol = AppRol.Alumno;

                switch (usuario.Rol)
                {
                    case ConstantHelpers.ROL.ADMINISTRADOR: rol = AppRol.Administrador; break;
                    case ConstantHelpers.ROL.SUPERVISOR: rol = AppRol.Supervisor; break;
                    case ConstantHelpers.ROL.ALUMNO: rol = AppRol.Alumno; break;
                    case ConstantHelpers.ROL.ASESOR: rol = AppRol.Asesor; break;
                    case ConstantHelpers.ROL.PROFESOR: rol = AppRol.Profesor; break;
                }

                Session.Set(SessionKey.Usuario, usuario);
                Session.Set(SessionKey.UsuarioId, usuario.UsuarioId);
                Session.Set(SessionKey.Email, usuario.Email);
                Session.Set(SessionKey.NombreCompleto, usuario.Nombre + " " + usuario.Apellido);
                Session.Set(SessionKey.Rol, rol);
                Session.Set(SessionKey.RolCompleto, usuario.Rol);

                //Para multi Empresa
                if (usuario.EmpresaId.HasValue)
                {
                    var empresa = new Empresa();
                    
                    //var empresa = context.Empresa.FirstOrDefault(x => x.EmpresaId == usuario.EmpresaId);
                    empresa = empresaBL.ListarEmpresa().FirstOrDefault(x => x.EmpresaId == usuario.EmpresaId);

                    var empresasUsuario = new List<UsuarioMultiEmpresa>();
                  
                    //var empresasUsuario = context.UsuarioMultiEmpresa.Include(x => x.Empresa)
                    //.Where(x => x.UsuarioId == usuario.UsuarioId)
                    //.Select(x => x.Empresa)
                    //.Distinct()
                    //.OrderBy(x => x.RazonSocial)
                    //.ToList();

                    empresasUsuario = new List<UsuarioMultiEmpresa>();
                    empresasUsuario = usuarioMultiEmpresaBL.ListarUsuarioMultiEmpresa()
                    .Where(x => x.UsuarioId == usuario.UsuarioId)
                    .Distinct()
                    .ToList();


                    if (empresasUsuario.Count() > 0)
                    {
                        var lstEmpresa = new List<UsuarioMultiEmpresa>();
                        
                        //var lstEmpresa = empresasUsuario.Select(x => new Tuple<int, string>(x.EmpresaId, x.RazonSocial)).ToList();

                        //Session.Set(SessionKey.EmpresasUsuario, lstEmpresa);
                        lstEmpresa = usuarioMultiEmpresaBL.ListarUsuarioMultiEmpresa();
                        List<Empresa> allEmpresas = empresaBL.ListarEmpresa();

                        List<Tuple<int, string>> tuplas = new List<Tuple<int, string>>();
                        foreach (UsuarioMultiEmpresa ume in lstEmpresa)
                        {
                            Empresa foundEmp = allEmpresas.First(x => x.EmpresaId == ume.EmpresaId);
                            Tuple<int, string> tupla = new Tuple<int, string>(ume.EmpresaId, foundEmp.RazonSocial);
                            tuplas.Add(tupla);
                        }


                        Session.Set(SessionKey.EmpresasUsuario, tuplas.OrderBy((x => x.Item2)));
                    }

                    Session.Set(SessionKey.EmpresaId, usuario.EmpresaId);
                    Session.Set(SessionKey.RazonSocial, empresa.RazonSocial);
                }

                Session.SetCookie();

                System.Web.Security.FormsAuthentication.SetAuthCookie(usuario.UsuarioId.ToString(), false);

                var log = new LogAccesosUsuario();
                log.FechaAcceso = DateTime.Now;
                var browser = Request.Browser;
                log.DatosNavegador = $" Type: {browser.Type}; {Environment.NewLine} Browser: {browser.Browser}; {Environment.NewLine} Version: {browser.Version}; {Environment.NewLine} MinorVersion : {browser.MinorVersion}; {Environment.NewLine} MajorVersion : {browser.MajorVersion}; {Environment.NewLine} Platform { browser.Platform }";
                log.UserAgent = Request.ServerVariables["HTTP_USER_AGENT"];
                log.Usuario = usuario;
                log.IPAcceso = Request.UserHostAddress;
                log.TipoAcceso = ConstantHelpers.TIPO_ACCESO.PORTAL;

                
                //context.LogAccesosUsuario.Add(log);
                //context.SaveChanges();
                usuarioBL.GrabarLogAccesoUsuario(log);

                return RedirectToAction("Dashboard", "Home");
            }
            else
            {
                Session.Clear();
                PostMessage(MessageType.Error, "¡Error!", "Ha ocurrido un error. Vuelva a intentarlo.");
                return View("PermisoInsuficiente");
            }
        }


        public ActionResult Dashboard(string returnUrl = "")
        {

            if (string.IsNullOrEmpty(returnUrl))
                switch (Session.GetRol())
                {
                    case AppRol.Administrador:
                        {
                            return RedirectToAction("AdministradorIndex", "Home");
                        }
                    case AppRol.Supervisor:
                        {
                            return RedirectToAction("SupervisorIndex", "Home");
                        }
                    case AppRol.Alumno:
                        {
                            return RedirectToAction("AlumnoIndex", "Home");
                        }
                    case AppRol.Empresa:
                        {
                            return RedirectToAction("AlumnoIndex", "Home");
                        }
                    case AppRol.Asesor:
                        {
                            return RedirectToAction("Index", "AsesoriaAsesor");
                        }
                    case AppRol.Profesor:
                        {
                            return RedirectToAction("AdministradorIndex", "Home");
                        }
                }

            else
                return Redirect(returnUrl);

            return RedirectToAction("Index");
        }

        public ActionResult Logout()
        {
            CookieHelpers.DeleteAll();
            Session.Clear();
            TempData["FlashMessages"] = null;
            return RedirectToAction("Index");
        }

        public ActionResult ForgotPassword()
        {
            TempData["FlashMessages"] = null;
            return View();
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult ForgotPassword(ForgotPasswordViewModel model)
        {
            TempData["FlashMessages"] = null;
            if (!ModelState.IsValid)
            {
                PostMessage(MessageType.Error, "Los campos no son correctos, " + ModelState.AllErrors());
                return View(model);
            }


            MailSMTP mail = new MailSMTP();
            var usuario = new Usuario();
            //  context.Usuario.FirstOrDefault(x => x.Email == model.Codigo);
            usuario = usuarioBL.ObtenerUsuarioPorEmail(model.Codigo);

            var baseUrl = Request.RequestContext.HttpContext.Request.Url;
            var hostUrl = baseUrl.AbsoluteUri.Replace(baseUrl.AbsolutePath, "");
            var token = RandomHelper.GenerateRandomToken();

            var direccionTemplate = Server.MapPath("~/Views/Shared/EditorTemplates/_TemplateCorreoSolicitudContrasenia.cshtml");
            var contenido = $@"Al parecer tuviste problemas con la contraseña de tu cuenta “correo” por lo que nos solicitaste reestablecerla.
                                    Para confirmar esta solicitud, y configurar una nueva contraseña para tu cuenta, por favor, vaya a la siguiente dirección web:
                                    {MailSMTP.NewLine}{MailSMTP.NewLine}<a href='{hostUrl}/Login/RestorePassword?usermailtoken={token}'> Restaurar</a>{MailSMTP.NewLine}{MailSMTP.NewLine}
                                    (Este enlace es válido por 60 minutos a partir de que se solicitó por vez primera el reinicio)
                                    {MailSMTP.NewLine}{MailSMTP.NewLine}NOTA: El enlace proporcionado a través de este correo electrónico debería aparecer en azul. Si no funciona, córtelo y péguelo en la ventana de direcciones de su navegador.
                                    Si necesita ayuda o presentas inconvenientes en el registro, por favor contáctate con nosotros a través del correo: aulavirtual@promperu.gob.pe";
            var titulo = "Correo de solicitud de contraseña";
            var body = mail.PopulateEmailBody(direccionTemplate, contenido, usuario.Nombre, titulo);
            mail.SendSingleEmail(usuario.Email, titulo, body);

            CambioContrasena cambio = new CambioContrasena
            {
                Fecha = DateTime.Now,
                Token = token,
                UsuarioId = usuario.UsuarioId,
            };

            
            //context.CambioContrasena.Add(cambio);
            usuarioBL.InsertarCambioContrasena(cambio);

            //context.SaveChanges();

            PostMessage(MessageType.Success, "Se ha enviado un link a su cuenta de correo por favor verifique su bandeja.");
            return RedirectToAction("Index");
        }


        public ActionResult RestorePassword(string usermailtoken)
        {
            CambioContrasena cambio = new CambioContrasena();

            //context.CambioContrasena.FirstOrDefault(x => x.Token == usermailtoken);

            usermailtoken = usermailtoken.Replace("\"", "");

            cambio = usuarioBL.ObtenerCambioContrasena(usermailtoken);
            cambio.Usuario = usuarioBL.ObtenerUsuarioPorId(cambio.UsuarioId);


            if (cambio == null)
            {
                PostMessage(MessageType.Error, "No se ha encontrado una solicitud de cambio de contraseña con los datos brindados");
                return RedirectToAction("Index");
            }

            RestorePasswordViewModel model = new RestorePasswordViewModel
            {
                Codigo = cambio.Usuario.Email,
                CambioId = cambio.CambioContrasenaId
            };

            return View(model);
        }

        [HttpPost]
        public ActionResult RestorePassword(RestorePasswordViewModel model)
        {

            Usuario usuario = new Usuario();
            
            //context.Usuario.FirstOrDefault(x => x.Email == model.Codigo);
            usuario = usuarioBL.ObtenerUsuarioPorEmail(model.Codigo);

            CambioContrasena cambio = new CambioContrasena();
            
            //context.CambioContrasena.FirstOrDefault(x => x.CambioContrasenaId == model.CambioId);
            cambio = usuarioBL.ObtenerCambioContrasena(model.CambioId);
            


            if (usuario == null || cambio.Fecha == null)
            {
                PostMessage(MessageType.Error, "No se ha encontrado un usuario con los datos brindados");
                return RedirectToAction("Index");
            }
            usuario.Password = model.Contrasena;
            usuarioBL.ActualizarContraseña(usuario.UsuarioId, usuario.Password);

            cambio.FechaCambio = DateTime.Now;
            usuarioBL.ActualizarCambioContrasena(model.CambioId, cambio.FechaCambio);

            //context.SaveChanges();
            

            PostMessage(MessageType.Success, "Contraseña actualizada correctamente");
            return RedirectToAction("Index");
        }

        public ActionResult VerifyEmail(string usermailtoken)
        {
            //try
            //{
            //aqui modificamos 1 campo de estado que activara a nuestro usuario
            //var token = HttpUtility.UrlEncode(usermailtoken, Encoding.UTF8);

            var token = RandomHelper.Base64Decode(usermailtoken);

            var email = RandomHelper.Decifrar("PEN", token);
            Usuario usuario = new Usuario();

            //Usuario usuario = context.Usuario.FirstOrDefault(x => x.Email == email && x.Estado == "PEN");
            usuario = usuarioBL.ObtenerUsuarioPorEmail(email);

            if (usuario.Estado == "PEN")
            {

                usuario.Estado = "ACT";

                usuarioBL.ActualizarUsuarioEstado(usuario.UsuarioId, usuario.Estado);

                //context.SaveChanges();
                ViewBag.Mensaje = "Se ha validado el correo correctamente";
            }
            else
            {
                //ViewBag.Mensaje = "Enlace caducado";
                ViewBag.Mensaje = "Cuenta activada";
            }

            return View();
            /*}
            catch (Exception e) {

            }*/


        }


        public ActionResult _ViewTermsConditions()
        {
            return View();
        }


    }
}