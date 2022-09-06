using System;
using System.Collections.Generic;
using System.Linq;
using System.Web.Configuration;
using System.Web.Mvc;
using PROMPERU.AULAVIRTUAL.WEB.Helpers;
using PROMPERU.AULAVIRTUAL.WEB.Logic;
using PROMPERU.AULAVIRTUAL.BE.USUARIO;
using PROMPERU.AULAVIRTUAL.WEB.Models.PromPeruApiMapping;
using PROMPERU.AULAVIRTUAL.WEB.ViewModel.User;
using PROMPERU.AULAVIRTUAL.Helpers;
using PROMPERU.AULAVIRTUAL.DA;
using PROMPERU.AULAVIRTUAL.BL;
using PROMPERU.AULAVIRTUAL.BE.CURSOS;
using System.Threading;
using System.Threading.Tasks;
using Recaptcha.Web.Mvc;
using Recaptcha.Web;

namespace PROMPERU.AULAVIRTUAL.WEB.Controllers
{
    public class FrontController : BaseController
    {
        EmpresaBL empresaBL = new EmpresaBL();
        NotificacionesBL notificacionesBL = new NotificacionesBL();
        UsuarioBL usuarioBL = new UsuarioBL();
        ParametroBL parametro = new ParametroBL();


        public ActionResult Index()
        {
            return View();
        }

        public ActionResult Register()
        {
            RegisterViewModel model = new RegisterViewModel();
            ViewBag.URLPortal = WebConfigurationManager.AppSettings["PORTAL.Url"].ToString();
            return View(model);
        }
        [HttpPost]
        public int Consultar_Cuenta(String correo, string rol="ALU")
        {
            int retVal = 0;

            //Usuario usuario = context.Usuario.FirstOrDefault(x => x.Email == correo && x.Rol == ConstantHelpers.ROL.ALUMNO);

            UsuarioDA usuarioDA = new UsuarioDA();
            retVal = usuarioDA.ConsultarCuentaPorEmailRol(correo, rol);

            return retVal;

        }

        [HttpPost]
        public ActionResult Register(RegisterViewModel model)
        {


                        
            RecaptchaVerificationHelper recaptchaHelper = this.GetRecaptchaVerificationHelper();
            if (String.IsNullOrEmpty(recaptchaHelper.Response))
            {
                ModelState.AddModelError("", "Captcha answer cannot be empty.");
                return View(model);
            }
            
            RecaptchaVerificationResult recaptchaResult = recaptchaHelper.VerifyRecaptchaResponse();
            
            if (!recaptchaResult.Success)
            {
                ModelState.AddModelError("", "Incorrect captcha answer.");
            }


            if (!ModelState.IsValid)
            {
                PostMessage(MessageType.Error, "Los campos no son correctos, " + ModelState.AllErrors());
                return View(model);
            }

            UsuarioDA usuarioDA = new UsuarioDA();

            Usuario usuario = usuarioDA.ObtenerUsuarioPorEmail(model.Email);

            if (usuario.Codigo != null && model.Conversion == 0)
            {
                PostMessage(MessageType.Warning, "Su cuenta de correo ya se encuentra registrada. Si no recuerda su contraseña pulse en Olvidé mi contraseña");
                return RedirectToAction("Index", "Login");
            }
            //Empresa

            var rolUsuario = model.TipoRegistro == "0" ? "EMP" : "ALU";

            //Empresa empresa = context.Empresa.FirstOrDefault(x => x.RUC == model.RUC);
            Empresa empresa = null;

            if (model.TipoRegistro == "0")//empresa
            {
                empresa = empresaBL.ObtenerEmpresaPorRUC(model.RUC);

                if (empresa.RUC == null)//aseguramos que solo sea 1 empresa registrada
                {
                    empresa = new Empresa
                    {
                        RUC = model.RUC,
                        RazonSocial = model.RazonSocial,
                        AutoMatriculaHabilitada = true,
                        CIIU = model.CIIU,
                        Productos = string.Join(",", model.Productos),
                        Email = model.EmailEmpresa
                    };
                    //context.Empresa.Add(empresa);
                    
                    int resultGrabarEmpresa = empresaBL.InsertarEmpresa(empresa);
                    
                }

                if (usuario.Codigo == null && model.Conversion == 0)//si no encontramos al usuario
                {
                    usuario = new Usuario
                    {
                        Apellido = model.Apellidos,
                        Cargo = "alumno",
                        Codigo = model.Email,
                        Email = model.Email,
                        EmpresaId = empresa.EmpresaId,
                        Estado = "ACT",//estado pendiente
                        Grupo = "alumno",
                        Nombre = model.Nombres,
                        Password = model.Password,
                        Rol = "EMP",
                        DNI = model.DNI,
                        NacionalidadId = model.NacionalidadId2,
                        TipoDocumento = (model.Nacionalidad == "DNI") ? 1 : 2,
                        Conversion = (model.Conversion == 1) ? true : false,
                        Telefono = model.Telefono,
                        Sector = String.Join("|", model.Sector.Select(x => x.ToString()).ToArray()),
                        SexoId = model.Sexo
                    };
                    //context.Usuario.Add(usuario);
                }
                else
                {
                    usuario.EmpresaId = empresa.EmpresaId;
                    usuario.Rol = "EMP";
                    usuario.Conversion = (model.Conversion == 1) ? true : false;
                }
            }
            else//registro emprendedor
            {
                usuario = new Usuario
                {
                    Apellido = model.Apellidos,
                    Cargo = "alumno",
                    Codigo = model.Email,
                    Email = model.Email,
                    Estado = "ACT",//estado pendiente
                    Grupo = "alumno",
                    Nombre = model.Nombres,
                    //Password = RandomHelper.GenerateRandomPassword(10),
                    Password = model.Password,
                    Rol = "ALU",
                    DNI = model.DNI,
                    //TestExportador = model.TieneTest,
                    TipoDocumento = (model.Nacionalidad == "DNI") ? 1 : 2,
                    NacionalidadId = model.NacionalidadId2,
                    Telefono = model.Telefono,
                    Sector = String.Join("|", model.Sector.Select(x => x.ToString()).ToArray()),
                    SexoId = model.Sexo
                };
                //context.Usuario.Add(usuario);
            }

            try
            {
                //context.SaveChanges();
                
                int resultGrabarUsuario = usuarioBL.InsertarUsuario(usuario);
                usuario = usuarioDA.ObtenerUsuarioPorEmail(model.Email);

            }
            catch(Exception ex)
            {
                Util.SaveErrorLog(ex);
                throw new Exception(ex.Message);
            }
            //catch (DbEntityValidationException e)
            //{
            //    Util.SaveErrorLog(e);

            //    string error = string.Empty;

            //    foreach (var eve in e.EntityValidationErrors)
            //    {
            //        error += string.Format("Entity of type \"{0}\" in state \"{1}\" has the following validation errors:",
            //            eve.Entry.Entity.GetType().Name, eve.Entry.State);
            //        foreach (var ve in eve.ValidationErrors)
            //        {
            //            error += string.Format("- Property: \"{0}\", Error: \"{1}\"",
            //                ve.PropertyName, ve.ErrorMessage);
            //        }
            //    }
            //    throw new Exception(error);
            //}

            int IdUsuario = usuario.UsuarioId;

            //REGISTRO EN GRUPO

            var grupoRegistro = new List<Grupo>();
            //grupoRegistro = context.Grupo.ToList();
            grupoRegistro = usuarioBL.ListarGrupo();

            foreach (var item in grupoRegistro)
            {
                if (item.Aforo == 1)
                {
                    if (rolUsuario == item.TipoRegistro || item.TipoRegistro == "GNR")
                    {
                        UsuarioGrupo usuarioLista = new UsuarioGrupo();
                        usuarioLista.GrupoId = item.GrupoId;
                        usuarioLista.UsuarioId = IdUsuario;
                        usuarioLista.Estado = ConstantHelpers.ESTADO.ACTIVO;

                        int resultGrabarUsuarioGrupo = usuarioBL.InsertarUsuarioGrupo(usuarioLista);
                        //context.UsuarioGrupo.Add(usuarioLista);
                        //context.SaveChanges();
                    }

                }
            }

            var baseUrl = Request.RequestContext.HttpContext.Request.Url;
            var hostUrl = baseUrl.AbsoluteUri.Replace(baseUrl.AbsolutePath, "");
            MailSMTP mail = new MailSMTP();
            var direccionTemplate = Server.MapPath("~/Views/Shared/EditorTemplates/_TemplateCorreoConfirmacion.cshtml");
            
            //var configuracion = context.Notificaciones.Where(a => a.Estado == Helpers.ConstantHelpers.ESTADO.ACTIVO && a.TipoNotificaciones.Codigo == "CNF").FirstOrDefault();

            List<Notificaciones> notificaciones = notificacionesBL.ListarNotificaciones(); 

            var configuracion = notificaciones.Where(a => a.Estado == Helpers.ConstantHelpers.ESTADO.ACTIVO && a.TipoNotificaciones.Codigo == "CNF").FirstOrDefault();

            if (configuracion != null && model.Conversion == 0)
            {
                var token = RandomHelper.Cifrar("PEN", usuario.Email);//se cifra el email con el codigo pendiente de activacion
                var enlace2 = "<a href='" + hostUrl + "/Login/VerifyEmail?usermailtoken=" + RandomHelper.Base64Encode(token) + "'>Enlace de confirmación</a>";
                var contenido = configuracion.Contenido.Replace("@servidor", hostUrl).Replace("@enlace", enlace2);

                //var titulo = "Correo de confirmación:";
                var titulo = configuracion.Titulo;
                var body = mail.PopulateEmailBody(direccionTemplate, contenido, usuario.Nombre, titulo);
                mail.SendSingleEmail(usuario.Email, titulo, body);

                CambioContrasena cambio = new CambioContrasena
                {
                    Fecha = DateTime.Now,
                    Token = token,
                    UsuarioId = usuario.UsuarioId,
                };

                //context.CambioContrasena.Add(cambio);
                //context.SaveChanges();

                int resultInsertarCambioContrasena = usuarioBL.InsertarCambioContrasena(cambio);

            }

            PostMessage(MessageType.Success);
            return RedirectToAction("Index", "Login");
        }


        public async Task<JsonResult> FindPersonByDNI(string dni, CancellationToken cancellationToken)
        {
            PromPeruApiClient client = new PromPeruApiClient();
            var response = await client.PostAsync<DNISearchResponse>("api/entidad/reniec/request", new { dni }, cancellationToken);
            return Json(response);
        }

        public async Task<JsonResult> FindCompanyByRUC(string ruc, CancellationToken cancellationToken)
        {
            PromPeruApiClient client = new PromPeruApiClient();
            var response = await client.PostAsync<RUCSearchResponse>("api/entidad/sunat/request", new { ruc }, cancellationToken);
            return Json(response);
        }

        public ActionResult FAQ()
        {
            return View();
        }
        [HttpPost]
        public string LlenarProvincia(int dep)
        {
          
            var Provincias = parametro.ListarProvincia().Where(x => x.DepartamentoId == dep).Select(x => new { ProvinciaId = x.ProvinciaId, Nombre = x.Nombre }).ToJson();
            return Provincias;
        }

        [HttpPost]
        public string LlenarDistrito(int dep, int prov)
        {
            
            var Distritos = parametro.ListarDistrito().Where(x => x.DepartamentoId == dep && x.ProvinciaId == prov).Select(x => new { DistritoId = x.DistritoId, Nombre = x.Nombre }).ToJson();
            return Distritos;
        }
        [HttpPost]
        public JsonResult Contact(string mail, string name, string message)
        {
            EmailLogic emailLogic = new EmailLogic();
            emailLogic.SendMailContact(message, mail, name);
            return Json(true);
        }
    }
}