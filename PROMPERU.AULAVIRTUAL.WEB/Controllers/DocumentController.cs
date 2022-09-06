using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using PROMPERU.AULAVIRTUAL.WEB.Models;
using PROMPERU.AULAVIRTUAL.WEB.ViewModel.Document;
using PROMPERU.AULAVIRTUAL.WEB.Logic;
using PROMPERU.AULAVIRTUAL.WEB.Helpers;
using PROMPERU.AULAVIRTUAL.WEB.Filters;
using System.Data;
//using Microsoft.Reporting.WebForms;
using System.Web.Configuration;
using System.IO;
//using Zen.Barcode;
using System.Transactions;
using PROMPERU.AULAVIRTUAL.Helpers;
using Microsoft.Reporting.WebForms;
using Zen.Barcode;
using PROMPERU.AULAVIRTUAL.BL;
using PROMPERU.AULAVIRTUAL.BE.CURSOS;
using PROMPERU.AULAVIRTUAL.BE.USUARIO;

namespace PROMPERU.AULAVIRTUAL.WEB.Controllers
{
    [AppAuthorize(AppRol.Administrador, AppRol.Empresa, AppRol.Alumno, AppRol.Supervisor, AppRol.Proveedor, AppRol.Ingeniero)]
    public class DocumentController : BaseController
    {
        protected CursosBL cursosBL = new CursosBL();
        protected UsuarioBL usuarioBL = new UsuarioBL();

        public ActionResult EmpresasDocumentos(Int32 EmpresaSeleccionadaId)
        {
            if (Session.GetRol() != null && Session.GetRol() == AppRol.Supervisor)
            {
                var viewModel = new ReleaseDocumentoViewModel();
                viewModel.CargarDatos(DatosContext, EmpresaSeleccionadaId);
                return View(viewModel);
            }
            return RedirectToAction("Dashboard", "Home");
        }

        public ActionResult DescargarDocumentos(Int32 CursoOnlineId)
        {
            int empresaId = 0;
            DocumentsLogic documentsLogic = new DocumentsLogic();
            
            if (Session.GetEmpresaId() != null && Session.GetRol() == AppRol.Supervisor)
            {
                empresaId = Session.GetEmpresaId();
            }

            

            List<Documento> documentosEmpleado = documentsLogic.ObtenerDocumentos(empresaId, CursoOnlineId);
            Documento documento = null;

            documento = documentosEmpleado.FirstOrDefault(x => x.Tipo == TipoDocumento.Certificado);

            if (documento == null)
                return new EmptyResult();

            var downloadName = String.Empty;
            var duracion_curso = documento.FechaFin;

            var versionCertificado = "";

            if (Session.GetRol() == AppRol.Alumno)
            {
                versionCertificado = WebConfigurationManager.AppSettings["EVOL.VersionCertificadoAlumno"];
            }
            else if (Session.GetRol() == AppRol.Alumno)
            {
                versionCertificado = WebConfigurationManager.AppSettings["EVOL.VersionCertificadoEmpresa"];
            }

            var reportPath = Server.MapPath("~/Reports/rptPlantillaCertificado_" + versionCertificado + ".rdlc");
            downloadName = "Constancia_" + documento.Usuario.Nombre + "_" + documento.Usuario.Apellido + ".pdf";
            ReportViewer viewer = new ReportViewer();

            viewer.ProcessingMode = ProcessingMode.Local;
            viewer.LocalReport.ReportPath = reportPath;
            viewer.LocalReport.DisplayName = downloadName;

            ReportDataSource dataSource = new ReportDataSource("dtsReporte");

            var textoCodigoBarras = "PROMPERU-" + documento.MatriculaCursoOnlineId.ToString("D8");
            var paramTextCodigoBarras = new ReportParameter()
            {
                Name = "TextCodigoBarras",
                Values = { textoCodigoBarras }
            };
            var paramCodigoBarras = new ReportParameter()
            {
                Name = "CodigoBarras",
                Values = { "R0lGODlhAQABAAAAACH5BAEKAAEALAAAAAABAAEAAAICTAEAOw==" }
            };

            try
            {
                using (var memoryStreamCodigoBarras = new MemoryStream())
                {
                    Code39BarcodeDraw barcode39 = BarcodeDrawFactory.Code39WithoutChecksum;
                    System.Drawing.Image codigoBarras = barcode39.Draw(textoCodigoBarras, 40);
                    codigoBarras.RotateFlip(System.Drawing.RotateFlipType.Rotate270FlipNone);

                    codigoBarras.Save(memoryStreamCodigoBarras, System.Drawing.Imaging.ImageFormat.Png);
                    paramCodigoBarras = new ReportParameter()
                    {
                        Name = "CodigoBarras",
                        Values = { Convert.ToBase64String(memoryStreamCodigoBarras.ToArray()) }
                    };
                }

            }
            catch (Exception)
            {

            }


            viewer.LocalReport.SetParameters(paramCodigoBarras);
            viewer.LocalReport.SetParameters(paramTextCodigoBarras);


            var bytes = viewer.LocalReport.Render("PDF");

            return File(bytes, "application/pdf", downloadName);

        }

        public ActionResult DownloadCertificado(Int32 MatriculaCursoOnlineId, Int32 CursoOnlineId)
        {
            var usuarioId = Session.GetUsuarioId();

            //var viewUsuarioMatricula = context.ViewUsuarioMatriculaCursoOnline.AsNoTracking().FirstOrDefault(x => x.UsuarioId == usuarioId && x.CursoOnlineId == CursoOnlineId && x.MatriculaCursoOnlineId == MatriculaCursoOnlineId);
            ViewUsuarioMatriculaCursoOnline viewUsuarioMatricula = cursosBL.ListarViewUsuarioMatriculaCursoOnline().FirstOrDefault(x => x.UsuarioId == usuarioId && x.CursoOnlineId == CursoOnlineId && x.MatriculaCursoOnlineId == MatriculaCursoOnlineId);

            if (viewUsuarioMatricula == null)
            {
                PostMessage(MessageType.Error, "No existe un certificado para el curso seleccionado.");
                return RedirectToAction("Dashboard", "Home");
            }

            var downloadName = "Constancia_" + viewUsuarioMatricula.NombreCursoOnline + "_" + viewUsuarioMatricula.ApellidoUsuario + ".pdf";

            var versionCertificado = WebConfigurationManager.AppSettings["EVOL.VersionCertificado"];

            LocalReport report = new LocalReport();
            report.ReportPath = Server.MapPath("~/Reports/rptPlantillaCertificado_" + versionCertificado + ".rdlc");
            report.DataSources.Add(new ReportDataSource("dtsUsuarioMatricula", new List<ViewUsuarioMatriculaCursoOnline> { viewUsuarioMatricula }));

            var textoCodigoBarras = "PROMPERU-" + "-" + viewUsuarioMatricula.MatriculaCursoOnlineId.ToString("D8");
            var paramTextCodigoBarras = new ReportParameter()
            {
                Name = "TextCodigoBarras",
                Values = { textoCodigoBarras }
            };
            var paramCodigoBarras = new ReportParameter()
            {
                Name = "CodigoBarras",
                Values = { "R0lGODlhAQABAAAAACH5BAEKAAEALAAAAAABAAEAAAICTAEAOw==" }
            };

            try
            {
                using (var memoryStreamCodigoBarras = new MemoryStream())
                {
                    Code39BarcodeDraw barcode39 = BarcodeDrawFactory.Code39WithoutChecksum;
                    System.Drawing.Image codigoBarras = barcode39.Draw(textoCodigoBarras, 40);
                    codigoBarras.RotateFlip(System.Drawing.RotateFlipType.Rotate270FlipNone);

                    codigoBarras.Save(memoryStreamCodigoBarras, System.Drawing.Imaging.ImageFormat.Png);
                    paramCodigoBarras = new ReportParameter()
                    {
                        Name = "CodigoBarras",
                        Values = { Convert.ToBase64String(memoryStreamCodigoBarras.ToArray()) }
                    };
                }

            }
            catch (Exception)
            {

            }

            report.SetParameters(paramCodigoBarras);
            report.SetParameters(paramTextCodigoBarras);

            var reportBytes = report.Render("PDF");

            using (var ts = new TransactionScope())
            {

                var logCertificado = new LogCertificado();
                logCertificado.FechaDescarga = DateTime.Now;
                logCertificado.MatriculaCursoOnlineId = viewUsuarioMatricula.MatriculaCursoOnlineId;
                logCertificado.UsuarioId = usuarioId;

                int res = cursosBL.InsertarLogCertificado(logCertificado);
                //context.SaveChanges();
                ts.Complete();
                return File(reportBytes, "application/pdf", downloadName);

            }
            return null;

        }

        public ActionResult DownloadCertificadoAlumno(Int32 CursoOnlineMaestroId,  Int32 UsuarioId)
        {
            

            

                //TODO: Stores;
                //var query = context.ViewCertificadoAlumno.FirstOrDefault(x => x.UsuarioId == UsuarioId && x.CursoOnlineMaestroId == CursoOnlineMaestroId && x.PorcentajeObtenido >= 70);
                var query = cursosBL.ListarViewCertificadoAlumno().FirstOrDefault(x => x.UsuarioId == UsuarioId && x.CursoOnlineMaestroId == CursoOnlineMaestroId && x.PorcentajeObtenido >= 70);

                if (query != null)
            {

                String[] Meses = { "", "Enero", "Febrero", "Marzo", "Abril", "Mayo", "Junio", "Julio", "Agosto", "Septiembre", "Octubre", "Noviembre", "Diciembre" };

                //var viewUsuarioMatricula = context.ViewUsuarioCursoMaestroReporte.AsNoTracking().FirstOrDefault(x => x.UsuarioId == UsuarioId && x.CursoOnlineMaestroId == CursoOnlineMaestroId);
                var viewUsuarioMatricula = cursosBL.ListarViewUsuarioCursoMaestroReporte().FirstOrDefault(x => x.UsuarioId == UsuarioId && x.CursoOnlineMaestroId == CursoOnlineMaestroId);

                if (viewUsuarioMatricula == null)
                {
                    PostMessage(MessageType.Error, "No existe un certificado para el curso seleccionado.");
                    return RedirectToAction("Dashboard", "Home");
                }

                var downloadName = "Constancia_" + viewUsuarioMatricula.NombreCurso + "_" + viewUsuarioMatricula.Apellido + ".pdf";

                var versionCertificado = "";

                if (Session.GetRol() == AppRol.Alumno)
                {
                    versionCertificado = WebConfigurationManager.AppSettings["EVOL.VersionCertificadoAlumno"];
                }
                else if (Session.GetRol() == AppRol.Empresa)
                {
                    versionCertificado = WebConfigurationManager.AppSettings["EVOL.VersionCertificadoEmpresa"];
                }

                //versionCertificado = WebConfigurationManager.AppSettings["EVOL.VersionCertificadoAlumno"];

                LocalReport report = new LocalReport();
                report.ReportPath = Server.MapPath("~/Reports/rptPlantillaCertificado_" + versionCertificado + ".rdlc");
                report.DisplayName = downloadName;

                var NombreEmpresa = viewUsuarioMatricula.RazonSocial;
                var NombreAlumno = viewUsuarioMatricula.Apellido + ' ' + viewUsuarioMatricula.Nombre;
                var NombreCursoMaestro = viewUsuarioMatricula.NombreCurso;
                var Footer = DateTime.Now.ToString("dd") + " de " + Meses[int.Parse(DateTime.Now.ToString("MM"))] + " de " + DateTime.Now.ToString("yyyy") + ",\n Lima - Perú";


                var paramTextCursoMaestro = new ReportParameter()
                {
                    Name = "NombreCursoMaestro",
                    Values = { NombreCursoMaestro }
                };

                var paramTextEmpresa = new ReportParameter()
                {
                    Name = "NombreEmpresa",
                    Values = { NombreEmpresa }
                };

                var paramTextUsuario = new ReportParameter()
                {
                    Name = "NombreAlumno",
                    Values = { NombreAlumno }
                };

                var paramTextFooter = new ReportParameter()
                {
                    Name = "Footer",
                    Values = { Footer }
                };

                var documento = new Usuario();

                documento = usuarioBL.ObtenerUsuarioPorId(viewUsuarioMatricula.UsuarioId);

                var textoCodigoBarras = "PROMPERU-" + viewUsuarioMatricula.CursoOnlineMaestroId.ToString("D8");

                var paramCodigoBarras = new ReportParameter()
                {
                    Name = "CodigoBarras",
                    Values = { "R0lGODlhAQABAAAAACH5BAEKAAEALAAAAAABAAEAAAICTAEAOw==" }
                };

                try
                {
                    using (var memoryStreamCodigoBarras = new MemoryStream())
                    {
                        Code39BarcodeDraw barcode39 = BarcodeDrawFactory.Code39WithoutChecksum;
                        System.Drawing.Image codigoBarras = barcode39.Draw(textoCodigoBarras, 40);
                        codigoBarras.RotateFlip(System.Drawing.RotateFlipType.Rotate270FlipNone);

                        codigoBarras.Save(memoryStreamCodigoBarras, System.Drawing.Imaging.ImageFormat.Png);
                        paramCodigoBarras = new ReportParameter()
                        {
                            Name = "CodigoBarras",
                            Values = { Convert.ToBase64String(memoryStreamCodigoBarras.ToArray()) }
                        };
                    }
                }
                catch (Exception e)
                {
                    Util.SaveErrorLog(e);
                }

                report.SetParameters(paramCodigoBarras);

                report.SetParameters(paramTextCursoMaestro);

                if (Session.GetRol() == AppRol.Empresa)
                {
                    report.SetParameters(paramTextEmpresa);
                }

                report.SetParameters(paramTextUsuario);
                report.SetParameters(paramTextFooter);

                Warning[] warnings;
                string[] streamIds;
                string mimeType = string.Empty;
                string encoding = string.Empty;
                string extension = string.Empty;


                /*string deviceInfo = "<DeviceInfo>" +
                                    "  <OutputFormat>PDF</OutputFormat>" +
                                    "  <PageWidth>8.5in</PageWidth>" +
                                    "  <PageHeight>11in</PageHeight>" +
                                    "  <MarginTop>0.5in</MarginTop>" +
                                    "  <MarginLeft>1in</MarginLeft>" +
                                    "  <MarginRight>1in</MarginRight>" +
                                    "  <MarginBottom>0.5in</MarginBottom>" +
                                    "</DeviceInfo>";*/

                string deviceInfo = "<DeviceInfo>" +
                                    "  <OutputFormat>PDF</OutputFormat>" +
                                    "</DeviceInfo>";

                var reportBytes = report.Render("PDF", deviceInfo, out mimeType, out encoding, out extension, out streamIds, out warnings);


                using (var ts = new TransactionScope())
                {
                    var logCertificado = new LogCertificado();
                    logCertificado.FechaDescarga = DateTime.Now;
                    logCertificado.MatriculaCursoOnlineId = viewUsuarioMatricula.CursoOnlineMaestroId;
                    logCertificado.UsuarioId = UsuarioId;

                    //context.LogCertificado.Add(logCertificado);
                    //context.SaveChanges();
                    int res = cursosBL.InsertarLogCertificado(logCertificado);
                    ts.Complete();
                }


                return File(reportBytes, "application/pdf", downloadName);

            }
            else
            {
                //InvalidarContext();
                PostMessage(MessageType.Error);
                return RedirectToAction("ViewCertificadoOnline","Course");
            }

            return null;
        }

        public ActionResult DownloadCertificadoTurismoAlumno(Int32 CursoOnlineId, Int32 UsuarioId)
        {
            var query = cursosBL.ListarViewCertificadoTurismoAlumno().FirstOrDefault(x => x.UsuarioId == UsuarioId && x.CursoOnlineId == CursoOnlineId && x.PorcentajeObtenido >= 70);

            if (query != null)
            {

                String[] Meses = { "", "Enero", "Febrero", "Marzo", "Abril", "Mayo", "Junio", "Julio", "Agosto", "Septiembre", "Octubre", "Noviembre", "Diciembre" };

                var viewUsuarioMatricula = cursosBL.ListarViewUsuarioCursoTurismoReporte().FirstOrDefault(x => x.UsuarioId == UsuarioId && x.CursoOnlineId == CursoOnlineId);

                if (viewUsuarioMatricula == null)
                {
                    PostMessage(MessageType.Error, "No existe un certificado para el curso seleccionado.");
                    return RedirectToAction("Dashboard", "Home");
                }

                var downloadName = "Constancia_" + viewUsuarioMatricula.NombreCurso + "_" + viewUsuarioMatricula.Apellido + ".pdf";

                var versionCertificado = "";

                if (Session.GetRol() == AppRol.Alumno)
                {
                    versionCertificado = WebConfigurationManager.AppSettings["EVOL.VersionCertificadoAlumno"];
                }
                else if (Session.GetRol() == AppRol.Empresa)
                {
                    versionCertificado = WebConfigurationManager.AppSettings["EVOL.VersionCertificadoEmpresa"];
                }

                LocalReport report = new LocalReport();
                report.ReportPath = Server.MapPath("~/Reports/rptPlantillaCertificado_" + versionCertificado + ".rdlc");
                report.DisplayName = downloadName;

                var NombreEmpresa = viewUsuarioMatricula.RazonSocial;
                var NombreAlumno = viewUsuarioMatricula.Apellido + ' ' + viewUsuarioMatricula.Nombre;
                var NombreCursoMaestro = viewUsuarioMatricula.NombreCurso;
                var Footer = DateTime.Now.ToString("dd") + " de " + Meses[int.Parse(DateTime.Now.ToString("MM"))] + " de " + DateTime.Now.ToString("yyyy") + ",\n Lima - Perú";

                var paramTextCursoMaestro = new ReportParameter()
                {
                    Name = "NombreCursoMaestro",
                    Values = { NombreCursoMaestro }
                };

                var paramTextEmpresa = new ReportParameter()
                {
                    Name = "NombreEmpresa",
                    Values = { NombreEmpresa }
                };

                var paramTextUsuario = new ReportParameter()
                {
                    Name = "NombreAlumno",
                    Values = { NombreAlumno }
                };

                var paramTextFooter = new ReportParameter()
                {
                    Name = "Footer",
                    Values = { Footer }
                };

                var documento = new Usuario();

                documento = usuarioBL.ObtenerUsuarioPorId(viewUsuarioMatricula.UsuarioId);

                var textoCodigoBarras = "PROMPERU-" + viewUsuarioMatricula.CursoOnlineId.ToString("D8");

                var paramCodigoBarras = new ReportParameter()
                {
                    Name = "CodigoBarras",
                    Values = { "R0lGODlhAQABAAAAACH5BAEKAAEALAAAAAABAAEAAAICTAEAOw==" }
                };

                try
                {
                    using (var memoryStreamCodigoBarras = new MemoryStream())
                    {
                        Code39BarcodeDraw barcode39 = BarcodeDrawFactory.Code39WithoutChecksum;
                        System.Drawing.Image codigoBarras = barcode39.Draw(textoCodigoBarras, 40);
                        codigoBarras.RotateFlip(System.Drawing.RotateFlipType.Rotate270FlipNone);

                        codigoBarras.Save(memoryStreamCodigoBarras, System.Drawing.Imaging.ImageFormat.Png);
                        paramCodigoBarras = new ReportParameter()
                        {
                            Name = "CodigoBarras",
                            Values = { Convert.ToBase64String(memoryStreamCodigoBarras.ToArray()) }
                        };
                    }
                }
                catch (Exception e)
                {
                    Util.SaveErrorLog(e);
                }

                report.SetParameters(paramCodigoBarras);

                report.SetParameters(paramTextCursoMaestro);

                if (Session.GetRol() == AppRol.Empresa)
                {
                    report.SetParameters(paramTextEmpresa);
                }

                report.SetParameters(paramTextUsuario);
                report.SetParameters(paramTextFooter);

                Warning[] warnings;
                string[] streamIds;
                string mimeType = string.Empty;
                string encoding = string.Empty;
                string extension = string.Empty;

                string deviceInfo = "<DeviceInfo>" +
                                    "  <OutputFormat>PDF</OutputFormat>" +
                                    "</DeviceInfo>";

                var reportBytes = report.Render("PDF", deviceInfo, out mimeType, out encoding, out extension, out streamIds, out warnings);

                using (var ts = new TransactionScope())
                {
                    var logCertificado = new LogCertificado();
                    logCertificado.FechaDescarga = DateTime.Now;
                    logCertificado.MatriculaCursoOnlineId = viewUsuarioMatricula.CursoOnlineId;
                    logCertificado.UsuarioId = UsuarioId;
                    int res = cursosBL.InsertarLogCertificado(logCertificado);
                    ts.Complete();
                }
                return File(reportBytes, "application/pdf", downloadName);
            }
            else
            {
                PostMessage(MessageType.Error);
                return RedirectToAction("ViewCertificadoOnline", "Course");
            }

            return null;
        }
    }
}
