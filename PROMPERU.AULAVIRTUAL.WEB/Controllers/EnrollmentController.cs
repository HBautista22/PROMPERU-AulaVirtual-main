using PROMPERU.AULAVIRTUAL.WEB.ViewModel.Enrollment;
using PROMPERU.AULAVIRTUAL.WEB.Helpers;
using PROMPERU.AULAVIRTUAL.BE.CURSOS;
using PROMPERU.AULAVIRTUAL.BL;
using System;
using System.Collections.Generic;
//using System.Data.Entity;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using System.Transactions;
//using PROMPERU.AULAVIRTUAL.WEB.Models;
using PROMPERU.AULAVIRTUAL.WEB.Filters;
using PROMPERU.AULAVIRTUAL.WEB.Logic;
using System.Data;
//using DocumentFormat.OpenXml.Packaging;
//using DocumentFormat.OpenXml.Spreadsheet;
using System.IO;
using System.Web.Configuration;


namespace PROMPERU.AULAVIRTUAL.WEB.Controllers
{
    public class EnrollmentController : BaseController
    {
        //
        // GET: /Enrollment/

        UsuarioBL usuarioBL = new UsuarioBL();
        CursosBL cursosBL = new CursosBL();
        EmpresaBL empresaBL = new EmpresaBL();
        NotificacionesBL notificacionesBL = new NotificacionesBL();

        public ActionResult Index()
        {
            return RedirectToAction("Dashboard", "Home");
        }

        [AppAuthorize(AppRol.Administrador, AppRol.Profesor)]
        public ActionResult ListDisponibilidadCursoOnline(Int32? p)
        {
            var viewModel = new ListDisponibilidadCursoOnlineViewModel();
            viewModel.CargarDatos(p, Session.GetUsuarioId());
            return View(viewModel);
        }

        [AppAuthorize(AppRol.Administrador, AppRol.Profesor)]
        public ActionResult EditDisponibilidadCursoOnline(Int32? DisponibilidadCursoOnlineId)
        {
            var viewModel = new EditDisponibilidadCursoOnlineViewModel();
            viewModel.CargarDatos(DisponibilidadCursoOnlineId);
            return View(viewModel);
        }

        [HttpPost]
        [AppAuthorize(AppRol.Administrador, AppRol.Profesor)]
        [ValidateAntiForgeryToken]
        public ActionResult EditDisponibilidadCursoOnline(EditDisponibilidadCursoOnlineViewModel model)
        {
            //TODO: Stores
            try
            {
                using (var ts = new TransactionScope())
                {
                    if (!ModelState.IsValid)
                    {
                        model.CargarDatos(model.DisponibilidadCursoOnlineId);
                        TryUpdateModel(model);
                        PostMessage(MessageType.Error);
                        return View(model);
                    }


                    var disponibilidadAnterior = new List<DisponibilidadCursoOnline>(); //context.DisponibilidadCursoOnline.FirstOrDefault(x => x.CursoOnlineId == model.CursoOnlineId && (x.FechaFin == null || x.FechaFin > DateTime.Now));
                    if (disponibilidadAnterior != null)
                    {
                        PostMessage(MessageType.Warning, "El curso seleccionado tiene disponibilidad Existente");
                        return View(model);
                    }

                    if (model.FechaFin != null && (model.FechaInicio >= model.FechaFin || model.FechaInicio < DateTime.Today))
                    {
                        PostMessage(MessageType.Error, "Las fechas ingresadas no son válidas");
                        return RedirectToAction("ListDisponibilidadCursoOnline");
                    }
                    var disponibilidadCursoOnline = new DisponibilidadCursoOnline();

                    if (model.DisponibilidadCursoOnlineId.HasValue)
                    {
                        disponibilidadCursoOnline = new DisponibilidadCursoOnline();// context.DisponibilidadCursoOnline.First(x => x.DisponibilidadCursoOnlineId == model.DisponibilidadCursoOnlineId);
                    }
                    else
                    {
                        //Establecer las variables por defecto
                        //context.DisponibilidadCursoOnline.Add(disponibilidadCursoOnline);
                    }

                    disponibilidadCursoOnline.CursoOnlineId = model.CursoOnlineId;
                    disponibilidadCursoOnline.FechaInicio = model.FechaInicio;
                    disponibilidadCursoOnline.FechaFin = model.FechaFin;

                    //context.SaveChanges();

                    ts.Complete();

                    PostMessage(MessageType.Success);
                    return RedirectToAction("ListDisponibilidadCursoOnline");
                }
            }
            catch (Exception)
            {

                return RedirectToAction("ListDisponibilidadCursoOnline");
                throw;
            }

            return null;

        }

        [AppAuthorize(AppRol.Supervisor, AppRol.Alumno, AppRol.Empresa, AppRol.Administrador, AppRol.Profesor)]
        public ActionResult RegisterUsuarioMatriculaCursoOlnile(Int32 CursoOnlineId)
        {
            //TODO: Stores
            try
            {

                using (var ts = new TransactionScope())
                {
                    Int32 usuarioId = Session.GetUsuarioId();
                    var usuario = usuarioBL.ObtenerUsuarioPorId(usuarioId);  //   ursosBL.obten    context.Usuario.Find(usuarioId);

                    if (usuario.Estado == ConstantHelpers.ESTADO.INACTIVO)
                    {
                        PostMessage(MessageType.Warning, "El usuario no está activo.");
                        return RedirectToAction("ViewCursoOnline", "Course", new { CursoOnlineId = CursoOnlineId });
                    }

                    var fechaInicioMatriculaVigente = ConstantHelpers.GET_FECHA_INICIO_MATRICULA_VIGENTE();
                    List<MatriculaCursoOnline> lstMatriculaCursoOnline = cursosBL.ListarMatriculaCursoOnline(usuarioId);
                    //var matriculaAnterior = context.MatriculaCursoOnline.OrderByDescending(x => x.MatriculaCursoOnlineId).Where(x => x.UsuarioId == usuarioId && x.CursoOnlineId == CursoOnlineId && !x.Estado.Equals(ConstantHelpers.ESTADO_MATRICULA.INACTIVO) && (x.FechaMatricula >= fechaInicioMatriculaVigente /*|| x.Estado.Equals(ConstantHelpers.ESTADO_MATRICULA.APROBADO)*/)).FirstOrDefault();
                    var matriculaAnterior = lstMatriculaCursoOnline.OrderByDescending(x => x.MatriculaCursoOnlineId).Where(x => x.UsuarioId == usuarioId && x.CursoOnlineId == CursoOnlineId && !x.Estado.Equals(ConstantHelpers.ESTADO_MATRICULA.INACTIVO) && (x.FechaMatricula >= fechaInicioMatriculaVigente /*|| x.Estado.Equals(ConstantHelpers.ESTADO_MATRICULA.APROBADO)*/)).FirstOrDefault();

                    if (matriculaAnterior != null && (matriculaAnterior.Estado == ConstantHelpers.ESTADO_MATRICULA.COMPLETADO || matriculaAnterior.Estado == ConstantHelpers.ESTADO_MATRICULA.PENDIENTE || matriculaAnterior.Estado == ConstantHelpers.ESTADO_MATRICULA.APROBADO))
                    {
                        PostMessage(MessageType.Warning, "Ya se encuentra matriculado en el curso.");
                        return RedirectToAction("ViewCursoOnline", "Course", new { CursoOnlineId = CursoOnlineId });
                    }

                    List<DisponibilidadCursoOnline> lstDisponibilidadCursoOnline = cursosBL.ListarDisponibilidadCursoOnline();
                    //var disponibilidadCursoOnline = context.DisponibilidadCursoOnline.Where(x => x.CursoOnlineId == CursoOnlineId && (!x.FechaFin.HasValue || x.FechaFin > DateTime.Now)).FirstOrDefault();
                    var disponibilidadCursoOnline = lstDisponibilidadCursoOnline.Where(x => x.CursoOnlineId == CursoOnlineId && (!x.FechaFin.HasValue || x.FechaFin > DateTime.Now)).FirstOrDefault();
                    if (disponibilidadCursoOnline == null)
                    {
                        PostMessage(MessageType.Warning, "El curso no está disponible.");
                        return RedirectToAction("ViewCursoOnline", "Course", new { CursoOnlineId = CursoOnlineId });
                    }

                    List<UnidadCursoOnline> lstUnidadCursoOnline = cursosBL.ListarUnidadCursoOnlinePorCursoOnlineId(CursoOnlineId);

                    var cantidadUnidadesCursoOnline = lstUnidadCursoOnline.Where(x => x.Estado == ConstantHelpers.ESTADO.ACTIVO && x.CursoOnlineId == CursoOnlineId).Count();  //context.UnidadCursoOnline.Where(x => x.Estado == ConstantHelpers.ESTADO.ACTIVO && x.CursoOnlineId == CursoOnlineId).Count();
                    var matricula = new MatriculaCursoOnline();

                    matricula.UsuarioId = usuario.UsuarioId;
                    matricula.CursoOnlineId = CursoOnlineId;
                    matricula.CursoOnline = cursosBL.ObtenerCursoOnlinePorId(CursoOnlineId);
                    matricula.FechaMatricula = DateTime.Now;
                    matricula.PorcentajeAvance = 0;
                    matricula.Estado = ConstantHelpers.ESTADO_MATRICULA.PENDIENTE;
                    matricula.TotalUnidadesCursoOnline = cantidadUnidadesCursoOnline;


                    cursosBL.InsertarMatricualCursoOnline(matricula);
                    SendNotificationOnEnrollment(matricula);


                    var configuracion = notificacionesBL.ListarNotificaciones().
                            Where(a => a.Estado == Helpers.ConstantHelpers.ESTADO.ACTIVO &&
                            a.TipoNotificaciones.Codigo == "BNM").FirstOrDefault(); // int(; // = null; // context.Notificaciones.Where(a => a.Estado == Helpers.ConstantHelpers.ESTADO.ACTIVO && a.TipoNotificaciones.Codigo == "BNM").FirstOrDefault();

                    if (configuracion != null)
                    {

                        MailSMTP mail = new MailSMTP();
                        var direccionTemplate = Server.MapPath("~/Views/Shared/EditorTemplates/_TemplateCorreoBienvenidaModulo.cshtml");
                        //var contenido = $@"Bienvenido al módulo de "+ matricula.CursoOnline.Nombre +" en donde podrás encontrar información muy interesante y sobre todo te ayudará a fortalecer tus conocimientos para convertirte en un exportador de éxito.";
                        //var titulo = "Correo de bienvenida a módulo";
                        var contenido = configuracion.Contenido.Replace("@curso", matricula.CursoOnline.Nombre);
                        var titulo = configuracion.Titulo;
                        var body = mail.PopulateEmailBody(direccionTemplate, contenido, usuario.Nombre, titulo);
                        mail.SendSingleEmail(usuario.Email, titulo, body);
                    }

                    PostMessage(MessageType.Success, "La matrícula se registró satisfactoriamente");
                    ts.Complete();
                }
            }
            catch (Exception ex)
            {
                PostMessage(MessageType.Error);
                throw;
            }
            return RedirectToAction("ViewCursoOnline", "Course", new { CursoOnlineId = CursoOnlineId });
        }

        public void SendNotificationOnEnrollment(MatriculaCursoOnline matricula)
        {
            try
            {
                EmailLogic emailLogic = new EmailLogic();
                emailLogic.SendMailOnEnrollment("Matricula: ", "Usted se ha matriculado satisfactoriamente en el curso ", matricula.UsuarioId, matricula.CursoOnlineId);
            }
            catch (Exception)
            {
                throw;
            }

        }

        [AppAuthorize(AppRol.Supervisor, AppRol.Administrador, AppRol.Profesor)]
        public ActionResult _ResgisterUsuariosMatriculaCursoOnline(Int32 CursoOnlineId)
        {
            //TODO: Stores
            try
            {
                var viewModel = new _ResgisterUsuariosMatriculaCursoOnlineViewModel();
                viewModel.CargarDatos(DatosContext, CursoOnlineId);
                return View(viewModel);
            }
            catch (Exception)
            {
                throw;
            }
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        [AppAuthorize(AppRol.Supervisor, AppRol.Administrador, AppRol.Profesor)]
        public ActionResult _ResgisterUsuariosMatriculaCursoOnline(FormCollection form)
        {
            //TODO: Stores
            //var cursoOnlineId = form[1].ToInteger();
            //try
            //{
            //    using (var ts = new TransactionScope())
            //    {

            //        var emailLogic = new EmailLogic(context);
            //        var cursoOnline = context.CursoOnline.Find(cursoOnlineId);
            //        var disponibilidadCursoOnline = context.DisponibilidadCursoOnline.Where(x => x.CursoOnlineId == cursoOnline.CursoOnlineId && (!x.FechaFin.HasValue || x.FechaFin > DateTime.Now)).FirstOrDefault();

            //        if (disponibilidadCursoOnline == null)
            //        {
            //            PostMessage(MessageType.Warning, "El curso no está disponible");
            //            return RedirectToAction("ViewCursoOnline", "Course", new { CursoOnlineId = cursoOnlineId });
            //        }

            //        var usuariosKey = form.AllKeys.Where(x => x.StartsWith("USU-"));
            //        var lstUnidadCursoOnlineActiva = context.UnidadCursoOnline.Where(x => x.Estado == ConstantHelpers.ESTADO.ACTIVO && x.CursoOnlineId == cursoOnlineId).ToList();

            //        var lstUsuariosMatriculadosAnteriormente = new List<Int32>();
            //        var lstUsuarioNuevos = new List<Int32>();

            //        foreach (var usuarioKey in usuariosKey)
            //        {
            //            var usuarioId = usuarioKey.Split('-')[1].ToInteger();

            //            var fechaInicioMatriculaVigente = ConstantHelpers.GET_FECHA_INICIO_MATRICULA_VIGENTE();
            //            var matriculaAnterior = context.MatriculaCursoOnline.Where(x => x.UsuarioId == usuarioId && x.CursoOnlineId == cursoOnline.CursoOnlineId && !x.Estado.Equals(ConstantHelpers.ESTADO_MATRICULA.INACTIVO) && (x.FechaMatricula >= fechaInicioMatriculaVigente /*|| x.Estado.Equals(ConstantHelpers.ESTADO_MATRICULA.APROBADO)*/)).FirstOrDefault();
            //            if (matriculaAnterior != null)
            //            {
            //                lstUsuariosMatriculadosAnteriormente.Add(matriculaAnterior.UsuarioId);
            //            }
            //            else
            //            {
            //                var cantidadUnidadesCursoOnline = lstUnidadCursoOnlineActiva.Count();
            //                MatriculaCursoOnline matricula = new MatriculaCursoOnline();
            //                matricula.UsuarioId = usuarioId;
            //                matricula.CursoOnlineId = cursoOnline.CursoOnlineId;
            //                matricula.FechaMatricula = DateTime.Now;
            //                matricula.PorcentajeAvance = 0;
            //                matricula.Estado = ConstantHelpers.ESTADO_MATRICULA.PENDIENTE;
            //                //matricula.TotalUnidadesCursoOnline = cantidadUnidadesCursoOnline;

            //                context.MatriculaCursoOnline.Add(matricula);
            //                lstUsuarioNuevos.Add(usuarioId);
            //            }
            //        }
            //        context.SaveChanges();
            //        Int32 CantidadUsuariosMatriculadosAnterior = lstUsuariosMatriculadosAnteriormente.Count();
            //        Int32 CantidadUsuarios = usuariosKey.Count();
            //        Int32 CantidadUsuariosMatriculados = CantidadUsuarios - CantidadUsuariosMatriculadosAnterior;
            //        if (CantidadUsuariosMatriculadosAnterior > 0)
            //        {
            //            PostMessage(MessageType.Info, "Hay " + CantidadUsuariosMatriculados + " usuarios que cuentan con una matrícula vigente");
            //        }
            //        if (CantidadUsuariosMatriculados > 0)
            //        {
            //            PostMessage(MessageType.Success, "Se registró la matrícula de " + CantidadUsuariosMatriculados + " usuarios al curso " + cursoOnline.Nombre);
            //        }

            //        foreach (var usuarioId in lstUsuarioNuevos)
            //        {
            //            emailLogic.SendMailOnEnrollment("Matricula: ", "Usted se ha matriculado satisfactoriamente en el curso ", usuarioId, cursoOnlineId);
            //        }

            //        ts.Complete();
            //    }
            //}
            //catch (Exception)
            //{
            //    PostMessage(MessageType.Error);
            //}
            //return RedirectToAction("ViewCursoOnline", "Course", new { CursoOnlineId = cursoOnlineId });
            return null;
        }

        [AppAuthorize(AppRol.Administrador, AppRol.Profesor)]
        public ActionResult _SetDisponibilidadCursoOnline(Int32 CursoOnlineId)
        {
        
            var viewModel = new _SetDisponibilidadCursoOnlineViewModel();
            

            List<DisponibilidadCursoOnline> lstDisponibilidadCursoOnline = cursosBL.ListarDisponibilidadCursoOnline();

            List<int> cursosDisponibles = cursosBL.ListarDisponibilidadCursoOnlineId();

            bool cursoDisponible = cursosDisponibles.Any(x => x == CursoOnlineId);

            DisponibilidadCursoOnline disponibilidadCursoOnline = new DisponibilidadCursoOnline();

            if (cursoDisponible)
            {
                disponibilidadCursoOnline = lstDisponibilidadCursoOnline.First(x => x.CursoOnlineId == CursoOnlineId);
            }
            else
            {
                disponibilidadCursoOnline = null;
            }

            if (disponibilidadCursoOnline != null)  //(disponibilidadCursoOnline != null)
            {
                viewModel.DisponibilidadCursoOnlineId = disponibilidadCursoOnline.DisponibilidadCursoOnlineId;
                viewModel.FechaFin = disponibilidadCursoOnline.FechaFin;
                viewModel.FechaInicio = disponibilidadCursoOnline.FechaInicio;
            }
            viewModel.CursoOnline = cursosBL.ObtenerCursoOnlinePorId(CursoOnlineId);
            viewModel.FechaInicio = DateTime.Now;

            return View(viewModel);

        }

        [AppAuthorize(AppRol.Administrador, AppRol.Profesor)]
        public ActionResult DisponibilidadCursoOnline(Int32 CursoOnlineId)
        {

            var viewModel = new _SetDisponibilidadCursoOnlineViewModel();


            List<DisponibilidadCursoOnline> lstDisponibilidadCursoOnline = cursosBL.ListarDisponibilidadCursoOnline();

            List<int> cursosDisponibles = cursosBL.ListarDisponibilidadCursoOnlineId();

            bool cursoDisponible = cursosDisponibles.Any(x => x == CursoOnlineId);

            DisponibilidadCursoOnline disponibilidadCursoOnline = new DisponibilidadCursoOnline();

            if (cursoDisponible)
            {
                disponibilidadCursoOnline = lstDisponibilidadCursoOnline.First(x => x.CursoOnlineId == CursoOnlineId);
            }
            else
            {
                disponibilidadCursoOnline = null;
            }

            if (disponibilidadCursoOnline != null)  //(disponibilidadCursoOnline != null)
            {
                viewModel.DisponibilidadCursoOnlineId = disponibilidadCursoOnline.DisponibilidadCursoOnlineId;
                viewModel.FechaFin = disponibilidadCursoOnline.FechaFin;
                viewModel.FechaInicio = disponibilidadCursoOnline.FechaInicio;
            }
            viewModel.CursoOnline = cursosBL.ObtenerCursoOnlinePorId(CursoOnlineId);
            viewModel.FechaInicio = DateTime.Now;

            return View(viewModel);

        }



        #region TODO: todo lo de adelante hay que revisar store y context

        [HttpPost]
        [ValidateAntiForgeryToken]
        [AppAuthorize(AppRol.Administrador, AppRol.Profesor)]
        public ActionResult DisponibilidadCursoOnline(_SetDisponibilidadCursoOnlineViewModel model)
        {
            //try
            //{
                using (var ts = new TransactionScope())
                {

                    List<DisponibilidadCursoOnline> lstDisponibilidadCursoOnline = cursosBL.ListarDisponibilidadCursoOnline();

                    if (!ModelState.IsValid)
                    {
                        //model.CargarDatos(_DatosContext, model.CursoOnlineId);
                        //TryUpdateModel(model);
                        PostMessage(MessageType.Error);
                        return RedirectToAction("ViewCursoOnlineAdmin", "Course", new { CursoOnlineId = model.CursoOnlineId });
                    }

                    if (model.FechaFin != null && (model.FechaInicio >= model.FechaFin || model.FechaInicio < DateTime.Today))
                    {
                        PostMessage(MessageType.Error, "La fecha ingresada no es válida");
                        return RedirectToAction("ViewCursoOnlineAdmin", "Course", new { CursoOnlineId = model.CursoOnlineId });
                    }

                    var disponibilidadCursoOnline = new DisponibilidadCursoOnline();

                    if (model.DisponibilidadCursoOnlineId.HasValue)
                    {
                        disponibilidadCursoOnline =lstDisponibilidadCursoOnline.First(x => x.DisponibilidadCursoOnlineId == model.DisponibilidadCursoOnlineId);  
                    }
                    else
                    {
                        //Establecer las variables por defecto
                        disponibilidadCursoOnline = new DisponibilidadCursoOnline();
                    }

                    disponibilidadCursoOnline.CursoOnlineId = model.CursoOnlineId;
                    disponibilidadCursoOnline.FechaInicio = model.FechaInicio;
                    disponibilidadCursoOnline.FechaFin = model.FechaFin;


                    if (model.DisponibilidadCursoOnlineId.HasValue)
                    {
                        
                    }
                    else
                    {
                        cursosBL.InsertarDisponibilidadCursoOnline(disponibilidadCursoOnline);
                    }



                    //context.SaveChanges();
                    ts.Complete();

                    PostMessage(MessageType.Success);
                    return RedirectToAction("ViewCursoOnlineAdmin", "Course", new { CursoOnlineId = model.CursoOnlineId });
                }
            //}
            //catch (Exception)
            //{
            //    PostMessage(MessageType.Error);
            //    return RedirectToAction("ViewCursoOnlineAdmin", "Course", new { CursoOnlineId = model.CursoOnlineId });
            //    throw;
            //}

        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        [AppAuthorize(AppRol.Administrador, AppRol.Profesor)]
        public ActionResult _SetDisponibilidadCursoOnline(_SetDisponibilidadCursoOnlineViewModel model)
        {
            //try
            //{
            using (var ts = new TransactionScope())
            {

                List<DisponibilidadCursoOnline> lstDisponibilidadCursoOnline = cursosBL.ListarDisponibilidadCursoOnline();

                if (!ModelState.IsValid)
                {
                    //model.CargarDatos(_DatosContext, model.CursoOnlineId);
                    //TryUpdateModel(model);
                    PostMessage(MessageType.Error);
                    return RedirectToAction("ViewCursoOnlineAdmin", "Course", new { CursoOnlineId = model.CursoOnlineId });
                }

                if (model.FechaFin != null && (model.FechaInicio >= model.FechaFin || model.FechaInicio < DateTime.Today))
                {
                    PostMessage(MessageType.Error, "La fecha ingresada no es válida");
                    return RedirectToAction("ViewCursoOnlineAdmin", "Course", new { CursoOnlineId = model.CursoOnlineId });
                }

                var disponibilidadCursoOnline = new DisponibilidadCursoOnline();

                if (model.DisponibilidadCursoOnlineId.HasValue)
                {
                    disponibilidadCursoOnline = lstDisponibilidadCursoOnline.First(x => x.DisponibilidadCursoOnlineId == model.DisponibilidadCursoOnlineId);
                }
                else
                {
                    //Establecer las variables por defecto
                    disponibilidadCursoOnline = new DisponibilidadCursoOnline();
                }

                disponibilidadCursoOnline.CursoOnlineId = model.CursoOnlineId;
                disponibilidadCursoOnline.FechaInicio = model.FechaInicio;
                disponibilidadCursoOnline.FechaFin = model.FechaFin;


                if (model.DisponibilidadCursoOnlineId.HasValue)
                {

                }
                else
                {
                    cursosBL.InsertarDisponibilidadCursoOnline(disponibilidadCursoOnline);
                }



                //context.SaveChanges();
                ts.Complete();

                PostMessage(MessageType.Success);
                return RedirectToAction("ViewCursoOnlineAdmin", "Course", new { CursoOnlineId = model.CursoOnlineId });
            }
            //}
            //catch (Exception)
            //{
            //    PostMessage(MessageType.Error);
            //    return RedirectToAction("ViewCursoOnlineAdmin", "Course", new { CursoOnlineId = model.CursoOnlineId });
            //    throw;
            //}

        }

        //[AppAuthorize(AppRol.Supervisor, AppRol.Administrador)]
        //public ActionResult _RegisterUsuarioCargaMasiva(Int32 CursoOnlineId)
        //{
        //    var viewModel = new _RegisterUsuarioCargaMasivaViewModel();
        //    viewModel.CargarDatos(_DatosContext, CursoOnlineId);
        //    return PartialView(viewModel);
        //}

        //[AppAuthorize(AppRol.Supervisor, AppRol.Administrador)]
        //public ActionResult _DownloadFormatoCargaMasiva()
        //{
        //    var virtualFilePath = Server.MapPath("~/Files/formato-carga-masiva-usuarios-evol.xlsx");
        //    return File(virtualFilePath, System.Net.Mime.MediaTypeNames.Application.Octet, Path.GetFileName(virtualFilePath));
        //}

        //[HttpPost]
        //[ValidateAntiForgeryToken]
        //[AppAuthorize(AppRol.Supervisor, AppRol.Administrador)]
        //public ActionResult _RegisterUsuarioCargaMasiva(_RegisterUsuarioCargaMasivaViewModel model)
        //{
        //    if (!ModelState.IsValid)
        //    {
        //        PostMessage(MessageType.Error);
        //        return RedirectToAction("ViewCursoOnline", "Course", new { CursoOnlineId = model.CursoOnlineId });
        //    }

        //    try
        //    {
        //        using (var ts = new TransactionScope())
        //        {
        //            var dataTable = ExcelLogic.ExtractExcelToDataTable(model.FileCargaMasiva);
        //            var lstUsuario = new List<Usuario>();
        //            var lstUsuarioNuevos = new List<Usuario>();
        //            var lstUsuarioExistentes = new List<Usuario>();
        //            var lstUsuarioNoMatriculados = new List<Usuario>();
        //            var lstUsuarioMatriculados = new List<Usuario>();
        //            var lstUsuarioNoRegistrados = new List<Usuario>();
        //            var dictGrupoUsuario = new Dictionary<String, String>();


        //            var colNombre = 0;
        //            var colApellido = 1;
        //            var colEmail = 2;
        //            var colGrupo = 3;
        //            var filaInicial = 7;
        //            var empresaId = Session.GetEmpresaId();

        //            for (int i = filaInicial; i < dataTable.Rows.Count; i++)
        //            {
        //                var email = dataTable.Rows[i][colEmail].ToString().SafeTrim();
        //                var nombre = dataTable.Rows[i][colNombre].ToString().SafeTrim();
        //                var apellido = dataTable.Rows[i][colApellido].ToString().SafeTrim();
        //                var grupo = dataTable.Rows[i][colGrupo].ToString().SafeTrim();

        //                if (String.IsNullOrEmpty(nombre) && String.IsNullOrEmpty(apellido) && String.IsNullOrEmpty(email))
        //                {
        //                    continue;
        //                }

        //                if (!ValidationHelpers.IsValidEmail(email) && !String.IsNullOrEmpty(email))
        //                {
        //                    continue;
        //                }

        //                var usuario = context.Usuario.FirstOrDefault(x => x.Email.Equals(email));
        //                if (usuario == null)
        //                {
        //                    usuario = new Usuario();
        //                    usuario.Codigo = email;
        //                    usuario.Nombre = nombre;
        //                    usuario.Apellido = apellido;
        //                    usuario.Email = email;
        //                    usuario.Password = Guid.NewGuid().ToString().Replace("-", String.Empty).Substring(0, 8);
        //                    usuario.EmpresaId = empresaId;
        //                    usuario.Rol = ConstantHelpers.ROL.ALUMNO;
        //                    usuario.Estado = ConstantHelpers.ESTADO.ACTIVO;

        //                    lstUsuarioNoRegistrados.Add(usuario);

        //                    /*if (String.IsNullOrEmpty(nombre) || String.IsNullOrEmpty(apellido) || String.IsNullOrEmpty(email) || !(ValidationHelpers.IsValidEmail(email)))
        //                    {
        //                        lstUsuarioNoRegistrados.Add(usuario);
        //                        continue;
        //                    }
        //                    else
        //                    {
        //                        lstUsuarioNuevos.Add(usuario);
        //                        context.Usuario.Add(usuario);
        //                    }*/
        //                }
        //                else
        //                {
        //                    if (usuario.EmpresaId != empresaId)
        //                        continue;

        //                    //if (usuario.Estado == ConstantHelpers.ESTADO.INACTIVO)
        //                    //    usuario.Estado = ConstantHelpers.ESTADO.ACTIVO;

        //                    if (!dictGrupoUsuario.ContainsKey(email))
        //                    {
        //                        dictGrupoUsuario.Add(email, grupo);
        //                    }

        //                    lstUsuarioExistentes.Add(usuario);
        //                }

        //                lstUsuario.Add(usuario);
        //            }

        //            context.SaveChanges();

        //            var fechaInicioMatriculaVigente = ConstantHelpers.GET_FECHA_INICIO_MATRICULA_VIGENTE();

        //            var fechaActual = DateTime.Now;
        //            foreach (var usuario in lstUsuarioExistentes) //Era lstUsuario
        //            {
        //                var matriculaCrusoOnline = context.MatriculaCursoOnline.FirstOrDefault(x => x.UsuarioId == usuario.UsuarioId && x.CursoOnlineId == model.CursoOnlineId && !x.Estado.Equals(ConstantHelpers.ESTADO_MATRICULA.INACTIVO) && (x.FechaMatricula >= fechaInicioMatriculaVigente/* || x.Estado.Equals(ConstantHelpers.ESTADO_MATRICULA.APROBADO)*/));
        //                if (matriculaCrusoOnline == null)
        //                {
        //                    matriculaCrusoOnline = new MatriculaCursoOnline();
        //                    matriculaCrusoOnline.UsuarioId = usuario.UsuarioId;
        //                    matriculaCrusoOnline.CursoOnlineId = model.CursoOnlineId;
        //                    matriculaCrusoOnline.FechaMatricula = fechaActual;
        //                    matriculaCrusoOnline.Estado = ConstantHelpers.ESTADO_MATRICULA.PENDIENTE;

        //                    if(dictGrupoUsuario.ContainsKey(usuario.Email))
        //                        matriculaCrusoOnline.Grupo = dictGrupoUsuario[usuario.Email];

        //                    context.MatriculaCursoOnline.Add(matriculaCrusoOnline);
        //                    lstUsuarioMatriculados.Add(usuario);
        //                }
        //                else
        //                {
        //                    lstUsuarioNoMatriculados.Add(usuario);
        //                }
        //            }

        //            context.SaveChanges();
        //            //emailLogic.SendMailOnEnrollment("Matricula: ", "Usted se ha matriculado satisfactoriamente en el curso ", matricula.UsuarioId, matricula.CursoOnlineId);

        //            var emailLogic = new EmailLogic(context);
        //            foreach (var usuario in lstUsuarioNuevos)
        //            {
        //                emailLogic.SendMailOnEnrollmentCargaUsuario("Creación de usuario", "Bienvenido al " + WebConfigurationManager.AppSettings["EVOL.NombreSistema"], usuario.UsuarioId, model.CursoOnlineId);
        //            }

        //            foreach (var usuario in lstUsuarioMatriculados)
        //            {
        //                emailLogic.SendMailOnEnrollment("Matricula: ", "Usted se ha matriculado satisfactoriamente en el curso ", usuario.UsuarioId, model.CursoOnlineId);
        //            }

        //            var resultCargaDatosUsuarioViewModel = new ResultCargaDatosUsuarioViewModel();
        //            resultCargaDatosUsuarioViewModel.LstUsuarioId = lstUsuario.Select(x => x.UsuarioId).ToList();
        //            resultCargaDatosUsuarioViewModel.LstUsuarioIdExistente = lstUsuarioExistentes.Select(x => x.UsuarioId).ToList();
        //            resultCargaDatosUsuarioViewModel.LstUsuarioNoMatriculados = lstUsuarioNoMatriculados.Select(x => x.UsuarioId).ToList();
        //            resultCargaDatosUsuarioViewModel.CargarDatos(_DatosContext, model.CursoOnlineId);
        //            resultCargaDatosUsuarioViewModel.LstUsuarioNoRegistrados = lstUsuarioNoRegistrados;
        //            if (lstUsuarioNoRegistrados.Count() > 0)
        //                PostMessage(MessageType.Warning, "No se registraron todos los usuarios.", "Revise los campos inválidos en la pestaña de \"Usuarios no registrados\".");
        //            else
        //                PostMessage(MessageType.Success);

        //            ts.Complete();

        //            return View("ResultCargaDatosUsuario", resultCargaDatosUsuarioViewModel);
        //        }
        //    }
        //    catch (Exception ex)
        //    {

        //        PostMessage(MessageType.Error);
        //        return RedirectToAction("ViewCursoOnline", "Course", new { CursoOnlineId = model.CursoOnlineId });
        //    }
        //}

        //[AppAuthorize(AppRol.Supervisor, AppRol.Administrador)]
        //public ActionResult ListUsuarioMatriculado(Int32 CursoOnlineId, Int32? np)
        //{
        //    var viewModel = new ListUsuarioMatriculadoViewModel();
        //    viewModel.CargarDatos(_DatosContext, CursoOnlineId, np);

        //    return View(viewModel);
        //}
        //[HttpPost]
        //[AppAuthorize(AppRol.Supervisor, AppRol.Administrador)]
        //public ActionResult ListUsuarioMatriculado(ListUsuarioMatriculadoViewModel model)
        //{
        //    model.CargarDatos(_DatosContext, model.CursoOnlineId, null);

        //    return View(model);
        //}

        //[AppAuthorize(AppRol.Supervisor, AppRol.Administrador)]
        //public ActionResult _DeleteMatricula(Int32 MatriculaCursoOnlineId)
        //{
        //    var viewModel = new DeleteMatriculaViewModel();

        //    viewModel.CargarDatos(_DatosContext, MatriculaCursoOnlineId);
        //    viewModel.UrlRefferer = Request.UrlReferrer.AbsoluteUri;
        //    return PartialView(viewModel);
        //}



        //[HttpPost]
        //[ValidateAntiForgeryToken]
        //[AppAuthorize(AppRol.Supervisor, AppRol.Administrador)]
        //public ActionResult _DeleteMatricula(DeleteMatriculaViewModel model)
        //{
        //    try
        //    {

        //        using (var transactionScope = new TransactionScope())
        //        {
        //            var matriculaCursoOnline = context.MatriculaCursoOnline.
        //                Include(x => x.AvanceMatriculaCursoOnline).
        //                Include(x => x.EvaluacionCursoOnline).
        //                Include(x => x.Usuario).
        //                Include(x => x.Usuario.ParametroScorm).
        //                FirstOrDefault(x => x.MatriculaCursoOnlineId == model.MatriculaCursoOnlineId);

        //            var lstAvanceMatriculaCursoOnline = matriculaCursoOnline.AvanceMatriculaCursoOnline.ToList();
        //            var lstEvaluacionCursoOnline = matriculaCursoOnline.EvaluacionCursoOnline.ToList();
        //            var lstParametrosScorm = matriculaCursoOnline.Usuario.ParametroScorm.ToList();

        //            foreach (var avanceMatriculaCursoOnline in lstAvanceMatriculaCursoOnline)
        //            {
        //                context.AvanceMatriculaCursoOnline.Remove(avanceMatriculaCursoOnline);
        //            }
        //            foreach (var parametrosScorm in lstParametrosScorm)
        //            {
        //                context.ParametroScorm.Remove(parametrosScorm);
        //            }

        //            foreach (var evaluacionCursoOnline in lstEvaluacionCursoOnline)
        //            {
        //                var lstPreguntaEvaluacionCursoOnline = evaluacionCursoOnline.PreguntaEvaluacionCursoOnline.ToList();
        //                foreach (var preguntaEvaluacionCursoOnline in lstPreguntaEvaluacionCursoOnline)
        //                {
        //                    var lstRespuestaEvaluacionCursoOnline = preguntaEvaluacionCursoOnline.RespuestaEvaluacionCursoOnline.ToList();
        //                    var lstRespuestaSeleccionadaEvaluacionCursoOnline = preguntaEvaluacionCursoOnline.RespuestaSeleccionadaEvaluacionCursoOnline.ToList();

        //                    foreach (var respuestaEvaluacionCursoOnline in lstRespuestaEvaluacionCursoOnline)
        //                    {
        //                        context.RespuestaEvaluacionCursoOnline.Remove(respuestaEvaluacionCursoOnline);
        //                    }
        //                    foreach (var respuestaSeleccionadaEValuacionCursoOnline in lstRespuestaSeleccionadaEvaluacionCursoOnline)
        //                    {
        //                        context.RespuestaSeleccionadaEvaluacionCursoOnline.Remove(respuestaSeleccionadaEValuacionCursoOnline);
        //                    }

        //                    context.PreguntaEvaluacionCursoOnline.Remove(preguntaEvaluacionCursoOnline);

        //                }
        //                context.EvaluacionCursoOnline.Remove(evaluacionCursoOnline);
        //            }

        //            context.MatriculaCursoOnline.Remove(matriculaCursoOnline);
        //            context.SaveChanges();

        //            transactionScope.Complete();
        //        }

        //    }
        //    catch (Exception ex)
        //    {
        //        PostMessage(MessageType.Error);
        //        return Redirect(model.UrlRefferer);
        //    }

        //    return Redirect(model.UrlRefferer);
        //}

        //public ActionResult EnableEvaluacion(Int32 MatriculaCursoOnlineId)
        //{

        //    try
        //    {
        //        using (var ts = new TransactionScope())
        //        {

        //            var matricula = context.MatriculaCursoOnline.Find(MatriculaCursoOnlineId);

        //            if (matricula.FechaMatricula >= ConstantHelpers.GET_FECHA_INICIO_MATRICULA_VIGENTE() && matricula.Estado == ConstantHelpers.ESTADO_MATRICULA.PENDIENTE && matricula.CursoOnline.TieneExamen == ConstantHelpers.ESTADO.ACTIVO)
        //            {
        //                matricula.Estado = ConstantHelpers.ESTADO_MATRICULA.COMPLETADO;
        //                matricula.FechaCompletado = DateTime.Now;
        //                matricula.PorcentajeAvance = 100;

        //                context.SaveChanges();
        //            }
        //            else
        //            {
        //                throw new Exception("Ne se puede Realizar la accion Solicitada por las condiciones del curso.");
        //            }
        //            ts.Complete();
        //            PostMessage(MessageType.Success);
        //        }
        //    }
        //    catch (Exception)
        //    {
        //        PostMessage(MessageType.Error);
        //    }
        //    return Redirect(Request.UrlReferrer.ToString());
        //}

        #endregion





    }
}
