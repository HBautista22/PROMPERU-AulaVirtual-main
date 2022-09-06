using PROMPERU.AULAVIRTUAL.WEB.ViewModel.Report;
using PROMPERU.AULAVIRTUAL.WEB.ViewModel.User;
using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Web;
using System.Web.Mvc;
//using System.Data.Entity;
using PROMPERU.AULAVIRTUAL.WEB.Helpers;
using PROMPERU.AULAVIRTUAL.WEB.Logic;
using System.IO;
using Microsoft.Reporting.WebForms;
using PROMPERU.AULAVIRTUAL.WEB.Filters;
using System.Globalization;
//using CsvHelper;
using System.Text;
using System.Web.Configuration;
using PROMPERU.AULAVIRTUAL.BL;
using PROMPERU.AULAVIRTUAL.BE.CURSOS;
using PROMPERU.AULAVIRTUAL.Helpers;

namespace PROMPERU.AULAVIRTUAL.WEB.Controllers
{
    [AppAuthorize(AppRol.Supervisor, AppRol.Administrador)]
    public class ReportController : BaseController
    {
        private readonly ReporteBL _reporteBl = new ReporteBL();

        private readonly CursosBL _cursosBl = new CursosBL();
        //
        // GET: /Report/

        [ViewParameter("Reportes", "fa fa-file-archive-o", "Certificados")]
        public ActionResult ListCursoOnline(int? p)
        {
            var viewModel = new ListCursoOnlineViewModel();
            viewModel.CargarDatos(DatosContext, p);
            return View(viewModel);
        }

        [HttpPost]
        public ActionResult ListCursoOnline(ListCursoOnlineViewModel model)
        {

            model.CargarDatos(DatosContext, null);
            return View(model);
        }

        [ViewParameter("Reportes", "fa fa-file-archive-o", "")]
        public ActionResult ViewCursoOnline(Int32 CursoOnlineId, Int32? p, String Nombre)
        {
            var viewModel = new ViewCursoOnlineViewModel();
            viewModel.CargarDatos(DatosContext, CursoOnlineId, p, Nombre);
            return View(viewModel);
        }

        public ActionResult _SendRecordatorio(Int32 CursoOnlineId)
        {
            var viewModel = new SendRecordatorioViewModel();
            viewModel.CargarDatos(DatosContext, CursoOnlineId);
            return PartialView(viewModel);
        }

        [ViewParameter("Reportes", "fa fa-file-archive-o", "")]
        [HttpPost]
        public ActionResult _SendRecordatorio(SendRecordatorioViewModel model)
        {
            try
            {
                //var cursoOnline = context.CursoOnline.Include(x => x.MatriculaCursoOnline).FirstOrDefault(x => x.CursoOnlineId == model.CursoOnlineId);
                CursoOnline cursoOnline = _cursosBl.ObtenerCursoOnlinePorId(model.CursoOnlineId);

                int empresaId = Session.GetEmpresaId();
                DateTime fechaInicioMatriculaVigente = ConstantHelpers.GET_FECHA_INICIO_MATRICULA_VIGENTE();

                List<MatriculaCursoOnline> lstMatriculaCursoOnlinePendienteAprobacion = cursoOnline.MatriculaCursoOnline.Where(x => x.Usuario.EmpresaId == empresaId && x.Estado != ConstantHelpers.ESTADO_MATRICULA.APROBADO && x.Estado != ConstantHelpers.ESTADO_MATRICULA.INACTIVO && x.FechaMatricula >= fechaInicioMatriculaVigente).ToList();
                //var emailLogic = new EmailLogic(context);
                EmailLogic emailLogic = new EmailLogic();
                int contMailEnviados = 0;

                if (lstMatriculaCursoOnlinePendienteAprobacion.Count <= 0)
                {
                    PostMessage(MessageType.Info, "Todos los alumnos matriculados han aprobado este curso.");
                    return RedirectToAction("ViewCursoOnline", "Report", new { CursoOnlineId = model.CursoOnlineId });
                }

                foreach (MatriculaCursoOnline matriculaCursoOnline in lstMatriculaCursoOnlinePendienteAprobacion)
                {
                    emailLogic.SendMailOnEnrollment("RECORDATORIO", "Se le hace recordar que aún no ha finalizado el curso ", matriculaCursoOnline.UsuarioId, cursoOnline.CursoOnlineId);
                    contMailEnviados += 1;
                }

                PostMessage(MessageType.Success, "Se enviaron recordatorios a los alumnos que no han aprobado el curso (" + contMailEnviados + " alumnos)");
                return RedirectToAction("ViewCursoOnline", "Course", new { CursoOnlineId = model.CursoOnlineId });

            }
            catch (Exception ex)
            {
                throw;
            }

            return View();

        }

        //COMENTADO PERMANENTE
        //[ViewParameter("Reportes", "fa fa-file-archive-o", "Alumnos")]
        //public ActionResult ReportHistoricoAlumnos(Int32? np)
        //{
        //    var viewModel = new ReportHistoricoAlumnosViewModel();
        //    viewModel.CargarDatos(CargarDatosContext, np);
        //    return View(viewModel);

        //}

        //[HttpPost]

        [ViewParameter("Reportes", "fa fa-file-archive-o", "Alumnos")]
        public ActionResult ReportHistoricoAlumnos(ReportHistoricoAlumnosViewModel model)
        {
            model.CargarDatos(DatosContext, model.np);
            return View("ReportHistoricoAlumnosNew", model);
        }

        [ViewParameter("Reportes", "fa fa-file-archive-o", "Alumnos")]
        public ActionResult ReportAsesoriaRemota(ReportAsesoriaRemotaViewModel model)
        {
            model.CargarDatos(DatosContext, model.np);
            return View("ReportAsesoriaRemotaNew", model);
        }

        [ViewParameter("Reportes", "fa fa-file-archive-o", "Alumnos")]
        public ActionResult ReportSolicitudAsesoria(ReportSolicitudAsesoriaViewModel model)
        {
            model.CargarDatos(DatosContext, model.np);
            return View("ReportSolicitudAsesoriaNew", model);
        }

        [ViewParameter("Reportes", "fa fa-file-archive-o", "Alumnos")]
        public ActionResult DownloadReportAlumnos(ListUsuarioViewModel viewModel, string type)
        {
            try
            {
                bool isTurismo = bool.Parse(WebConfigurationManager.AppSettings["EsTurismo"]);

                string path = Path.Combine(Server.MapPath("~/Reports"), "rptReportHistoricoAlumnosLstExportacion.rdlc");

                if (isTurismo)
                {
                    path= Path.Combine(Server.MapPath("~/Reports"), "rptReportHistoricoAlumnosLst.rdlc");
                }

                List<ViewReporteHistoricoAlumnoLst> query = _reporteBl.ListarViewReporteHistoricoAlumnoLst();

                if (!string.IsNullOrEmpty(viewModel.EmailBuscar))
                {
                    query = query.Where(x => x.Email.Contains(viewModel.EmailBuscar) || x.Nombre.Contains(viewModel.CadenaBuscar)).ToList();
                }

                if (!string.IsNullOrEmpty(viewModel.DniBuscar))
                {
                    query = query.Where(x => x.Usuario_Dni.Contains(viewModel.DniBuscar)).ToList();
                }

                if (!string.IsNullOrEmpty(viewModel.EstadoId))
                {
                    query = query.Where(x => x.Estado.Contains(viewModel.EstadoId)).ToList();
                }
                
                if (!string.IsNullOrEmpty(viewModel.RolId))
                {
                    query = query.Where(x => x.Rol == viewModel.RolId).ToList();
                }

                if (!string.IsNullOrEmpty(viewModel.RucBuscar))
                {
                    query = query.Where(x => x.Ruc.Contains(viewModel.RucBuscar)).ToList();
                }
                
                //DataTable dtDetalleDeuda = query.ToList().ToDataTable();

                ReporteLogic_new reporteLogic = new ReporteLogic_new();
                ReportDataSource reportDataSource = new ReportDataSource("EvolDataSet", query);

                return reporteLogic.GenerarReporte(reportDataSource, type, path, false, "DownloadReportHistoricoAlumnosLst");
            }
            catch (Exception ex)
            {
                PostMessage(MessageType.Error, ex.Message);
                return RedirectToAction("ListUsuario","User");
            }

        }

        [ViewParameter("Reportes", "fa fa-file-archive-o", "Alumnos")]
        public ActionResult DownloadReportHistoricoAlumnos(ReportHistoricoAlumnosViewModel viewModel, string type)
        {
            try
            {
                string path = Path.Combine(Server.MapPath("~/Reports"), "rptReportHistoricoAlumnos.rdlc");

                //var query = context.ViewReporteHistoricoAlumno.Where(x => x.Matricula_Estado != ConstantHelpers.ESTADO_MATRICULA.INACTIVO).AsNoTracking().AsQueryable();
                List<ViewReporteHistoricoAlumno> query =  _reporteBl
                    .ListarViewReporteHistoricoAlumno()
                    .Where(x => x.Matricula_Estado != ConstantHelpers.ESTADO_MATRICULA.INACTIVO)
                    .ToList();

                if (!String.IsNullOrEmpty(viewModel.Codigo))
                {
                    query = query.Where(x => x.Usuario_Codigo.Contains(viewModel.Codigo) || x.Usuario_NombreCompleto.Contains(viewModel.Codigo)).ToList();
                }
                if (!String.IsNullOrEmpty(viewModel.Curso))
                {
                    query = query.Where(x => x.Curso_Nombre.Contains(viewModel.Curso)).ToList();
                }
                if (!String.IsNullOrEmpty(viewModel.Cargo))
                {
                    query = query.Where(x => x.Usuario_Cargo.Contains(viewModel.Cargo)).ToList();
                }
                if (!String.IsNullOrEmpty(viewModel.Grupo))
                {
                    query = query.Where(x => x.Usuario_Grupo.Contains(viewModel.Grupo)).ToList();
                }
                if (!String.IsNullOrEmpty(viewModel.Estado))
                {
                    query = query.Where(x => x.Matricula_Estado.Contains(viewModel.Estado)).ToList();
                }
                
                if (Session.GetRol() == AppRol.Administrador)
                {
                    if (!String.IsNullOrEmpty(viewModel.RazonSocial))
                    {
                        query = query.Where(x => x.Empresa_RazonSocial.Contains(viewModel.RazonSocial)).ToList();
                    }
                }
                else
                {
                    var empresaId = Session.GetEmpresaId();
                    query = query.Where(x => x.Usuario_EmpresaId == empresaId).ToList();
                }

                DataTable dtDetalleDeuda = query.ToList().ToDataTable();

                ReporteLogic_new reporteLogic = new ReporteLogic_new();
                ReportDataSource reportDataSource = new ReportDataSource("EvolDataSet", query);

                return reporteLogic.GenerarReporte(reportDataSource, type, path, false, "DownloadReportHistoricoAlumnos");
            }
            catch (Exception ex)
            {
                PostMessage(MessageType.Error, ex.Message);
                return RedirectToAction("ReportHistoricoAlumnos");
            }

        }



        [ViewParameter("Reportes", "fa fa-file-archive-o", "Alumnos")]
        public ActionResult DownloadReportHistoricoAlumnosLst(ReportHistoricoAlumnosViewModel viewModel, String Type)
        {
            try
            {
                String path = Path.Combine(Server.MapPath("~/Reports"), "rptReportHistoricoAlumnosLst.rdlc");

                //var query = context.ViewReporteHistoricoAlumnoLst.AsNoTracking().AsQueryable();
                var query = _reporteBl.ListarViewReporteHistoricoAlumnoLst().AsQueryable();

                if (!String.IsNullOrEmpty(viewModel.Codigo))
                {
                    //query = query.Where(x => x.Email.Contains(viewModel.Codigo));
                    query = query.Where(x => x.Email.Contains(viewModel.Codigo));
                    
                }

                var dtDetalleDeuda = query.ToList().ToDataTable();

                ReporteLogic_new reporteLogic = new ReporteLogic_new();

                ReportDataSource ReportDataSource = new ReportDataSource("EvolDataSet", query.AsQueryable());

                return reporteLogic.GenerarReporte(ReportDataSource, Type, path, false, "DownloadReportHistoricoAlumnosLst");
            }
            catch (Exception ex)
            {
                PostMessage(MessageType.Error, ex.Message);
                return RedirectToAction("ReportHistoricoAlumnos");
            }

        }

        [ViewParameter("Reportes", "fa fa-file-archive-o", "Alumnos")]
        public ActionResult DownloadReportAsesoriaRemota(ReportAsesoriaRemotaViewModel viewModel, String Type)
        {
            try
            {
                String path = Path.Combine(Server.MapPath("~/Reports"), "rptReportAsesoriaRemota.rdlc");

                var query = _reporteBl.ListarViewReporteAsesoriaRemota().AsQueryable();

                ReporteLogic_new reporteLogic = new ReporteLogic_new();
                ReportDataSource ReportDataSource = new ReportDataSource("EvolDataSet", query.AsQueryable());
                return reporteLogic.GenerarReporte(ReportDataSource, Type, path, false, "DownloadReportAsesoriaRemota");
            }
            catch (Exception ex)
            {
                PostMessage(MessageType.Error, ex.Message);
                return RedirectToAction("ReportAsesoriaRemota");
            }

        }

        public ActionResult DownloadReportSolicitudAsesoria(ReportSolicitudAsesoriaViewModel viewModel, String Type)
        {
            try
            {
                String path = Path.Combine(Server.MapPath("~/Reports"), "rptReportSolicitudAsesoriaRemota.rdlc");

                var query = _reporteBl.ListarViewReporteSolicitudAsesoriaRemota().AsQueryable();

                ReporteLogic_new reporteLogic = new ReporteLogic_new();
                ReportDataSource ReportDataSource = new ReportDataSource("EvolDataSet", query.AsQueryable());
                return reporteLogic.GenerarReporte(ReportDataSource, Type, path, false, "DownloadReportSolicitudAsesoriaRemota");
            }
            catch (Exception ex)
            {
                PostMessage(MessageType.Error, ex.Message);
                return RedirectToAction("ReportSolicitudAsesoriaRemota");
            }

        }



    }
}
