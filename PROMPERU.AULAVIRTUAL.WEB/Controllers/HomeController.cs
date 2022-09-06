using System;
using System.Collections.Generic;
using System.Data;
using System.IO;
using System.Linq;
using System.Web.Mvc;
using ClosedXML.Excel;
using PROMPERU.AULAVIRTUAL.WEB.Helpers;
using PROMPERU.AULAVIRTUAL.WEB.ViewModel.Home;
using PROMPERU.AULAVIRTUAL.BE.CURSOS;
using PROMPERU.AULAVIRTUAL.BE.USUARIO;
using PROMPERU.AULAVIRTUAL.WEB.Filters;
using PROMPERU.AULAVIRTUAL.BL;
using PROMPERU.AULAVIRTUAL.Helpers;

//using System.Data.Entity;

namespace PROMPERU.AULAVIRTUAL.WEB.Controllers
{
    public class HomeController : BaseController
    {
        private readonly UsuarioBL _usuarioBl = new UsuarioBL();
        private readonly CursosBL _cursosBl = new CursosBL();

        private readonly EnlacesInteresBL _enlacesInteresBl = new EnlacesInteresBL();
        //
        // GET: /Home/


        public ActionResult NotFound()
        {
            return View();
        }

        public ActionResult Index()
        {
            return RedirectToRoute("CMS", new { url = "" });
        }

        [ValidateAntiForgeryToken]
        public ActionResult Login()
        {
            return View();
        }

        public ContentResult KeepAlive()
        {
            Session.Set(SessionKey.UsuarioId, Session.Get(SessionKey.UsuarioId));
            return Content(string.Empty);
        }

        [AppAuthorize(AppRol.Administrador, AppRol.Profesor)]
        public ActionResult AdministradorIndex()
        {
            AdministradorIndexViewModel viewModel = new AdministradorIndexViewModel
            {
                CantidadAlumno = _usuarioBl.TotalAlumnosActivos(),
                CantidadCursos = _usuarioBl.TotalCursosActivos(),
                CantidadCertificados = _usuarioBl.TotalCertificados(),
                CantidadEmpresa = _usuarioBl.TotalEmpresaActivos(),
                EnlacesInteres = _enlacesInteresBl.ListarEnlacesInteres(),
                CursosOnlineRanking = _cursosBl.ListarCursoOnlineRanking(),
                Certificados = _usuarioBl.ListarUsuarioConCertificado()
            };


            return View("AdministradorIndexNew", viewModel);
        }

        [AppAuthorize(AppRol.Supervisor)]
        public ActionResult SupervisorIndex()
        {
            SupervisorIndexViewModel viewModel = new SupervisorIndexViewModel
            {
                UsuarioId = Session.GetUsuarioId(),
                EmpresaId = Session.GetEmpresaId()
            };

            DateTime fechaMatriculaVigente = ConstantHelpers.GET_FECHA_INICIO_MATRICULA_VIGENTE();
            List<MatriculaCursoOnline> queryMatricula = _cursosBl.ListarMatriculaCursoOnline(viewModel.UsuarioId);
            queryMatricula = queryMatricula
                .Where(x => x.Estado != ConstantHelpers.ESTADO_MATRICULA.DESAPROBADO &&
                            x.Estado != ConstantHelpers.ESTADO_MATRICULA.INACTIVO)
                .OrderByDescending(x => x.FechaMatricula).ToList();

            viewModel.LstMatriculaActual = queryMatricula.Where(x =>
                    (x.Estado == ConstantHelpers.ESTADO_MATRICULA.PENDIENTE &&
                     x.FechaMatricula >= fechaMatriculaVigente) ||
                    x.Estado == ConstantHelpers.ESTADO_MATRICULA.COMPLETADO ||
                    x.Estado == ConstantHelpers.ESTADO_MATRICULA.APROBADO)
                .Take(ConstantHelpers.DASHBOARD.MAX_ITEMS_CURSO)
                .ToList();

            List<int> cursosMatriculadosId = viewModel.LstMatriculaActual.Select(x => x.CursoOnlineId).ToList();

            List<DisponibilidadCursoOnline> queryCursosDisponibles =
                _cursosBl.ListarDisponibilidadCursoOnlineUsuarioId(viewModel
                    .UsuarioId); // dataContext.context.DisponibilidadCursoOnline.Include(x => x.CursoOnline).Where(x => x.CursoOnline.Estado == ConstantHelpers.ESTADO.ACTIVO && (x.FechaFin == null || x.FechaFin > DateTime.Now) && !cursosMatriculadosId.Any(c => c == x.CursoOnlineId)).AsQueryable();
            queryCursosDisponibles = queryCursosDisponibles.Where(x =>
                x.CursoOnline.Estado == ConstantHelpers.ESTADO.ACTIVO &&
                (x.FechaFin == null || x.FechaFin > DateTime.Now)).ToList();
            //queryCursosDisponibles = queryCursosDisponibles.Where(x => x.
            viewModel.LstCursosDisponibles =
                queryCursosDisponibles.Take(ConstantHelpers.DASHBOARD.MAX_ITEMS_CURSO).ToList();

            viewModel.CantidadCursosDisponibles = queryCursosDisponibles.Count();
            //viewModel.CantidadUsuariosMatriculados = dataContext.context.MatriculaCursoOnline.Include(x => x.Usuario).Where(x => x.Usuario.EmpresaId == EmpresaId && x.Estado != ConstantHelpers.ESTADO_MATRICULA.INACTIVO && x.FechaMatricula >= fechaMatriculaVigente).Count();
            //viewModel.CantidadUsuariosActivos = dataContext.context.Usuario.Where(x => x.Estado == ConstantHelpers.ESTADO.ACTIVO && x.EmpresaId == EmpresaId).Count();

            return View(viewModel);
        }

        [AppAuthorize(AppRol.Alumno, AppRol.Empresa, AppRol.Supervisor)]
        public ActionResult AlumnoIndex()
        {
            AlumnoIndexViewModel viewModel = new AlumnoIndexViewModel();

            viewModel.CargarDatos(DatosContext);
            return View("AlumnoIndexNew", viewModel);
        }

        public ActionResult CambiarEmpresa(int empresaId)
        {
            //var usuarioId = Session.GetUsuarioId();
            //var empresasUsuario = context.UsuarioMultiEmpresa.Include(x => x.Empresa)
            //                                                 .FirstOrDefault(x => x.empresaId == EmpresaId && x.UsuarioId == usuarioId);

            //if (empresasUsuario != null && empresasUsuario.Empresa != null)
            //{
            //    Session.Set(SessionKey.EmpresaId, empresasUsuario.EmpresaId);
            //    Session.Set(SessionKey.RazonSocial, empresasUsuario.Empresa.RazonSocial);
            //    PostMessage(MessageType.Success, "Cambio de empresa exitoso. Se muestra la información relacionada a " + empresasUsuario.Empresa.RazonSocial);
            //}

            return Redirect(Request.UrlReferrer?.ToString());
        }

        /*
        public ActionResult LoginDirecto() {
            var viewModel = new LoginViewModel() { Codigo = "dimm", Contrasena = "1234" };
            return Login(viewModel);
        }*/

        public ActionResult Dashboard()
        {
            switch (Session.GetRol())
            {
                case AppRol.Administrador:
                {
                    return RedirectToAction("AdministradorIndex");
                }
                case AppRol.Supervisor:
                {
                    return RedirectToAction("SupervisorIndex");
                }
                case AppRol.Alumno:
                {
                    return RedirectToAction("AlumnoIndex");
                }
                case AppRol.Empresa:
                {
                    return RedirectToAction("AlumnoIndex");
                }
                case AppRol.Asesor:
                {
                    return RedirectToAction("Index", "AsesoriaAsesor");
                }
                case AppRol.Profesor:
                {
                    return RedirectToAction("AdministradorIndex");
                }
            }

            return RedirectToAction("Login");
        }

        public ActionResult ExportarUsuarioConCertificado()
        {
            List<Usuario> data = _usuarioBl.ListarUsuarioConCertificado();
            DataTable certificados = new DataTable("Certificados");
            certificados.Columns.AddRange(new DataColumn[4]
            {
                new DataColumn("Nombre", typeof(string)),
                new DataColumn("Apellido", typeof(string)),
                new DataColumn("Email", typeof(string)),
                new DataColumn("Curso", typeof(string))
            });

            foreach (Usuario item in data)
            {
                certificados.Rows.Add(item.Nombre, item.Apellido, item.Email, item.Curso);
            }

            using (XLWorkbook wb = new XLWorkbook())
            {
                wb.Worksheets.Add(certificados, "Alumnos");
                wb.Style.Alignment.Horizontal = XLAlignmentHorizontalValues.Center;  
                wb.Style.Font.Bold = true;

                Response.Clear();  
                Response.Buffer = true;  
                Response.Charset = "";  
                Response.ContentType = "application/vnd.openxmlformats-officedocument.spreadsheetml.sheet";  
                Response.AddHeader("content-disposition", "attachment;filename= ListaAlumnosCertificado.xlsx");  
  
                using(MemoryStream myMemoryStream = new MemoryStream()) {  
                    wb.SaveAs(myMemoryStream);  
                    myMemoryStream.WriteTo(Response.OutputStream);  
                    Response.Flush();  
                    Response.End();  
                }
            }

            return RedirectToAction("AdministradorIndex", "Home");  
        }

        public ActionResult Logout()
        {
            CookieHelpers.DeleteAll();
            Session.Clear();
            return RedirectToAction("Dashboard", "Login");
        }
    }
}