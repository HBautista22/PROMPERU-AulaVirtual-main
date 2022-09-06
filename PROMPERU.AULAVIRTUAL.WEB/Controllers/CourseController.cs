using PROMPERU.AULAVIRTUAL.WEB.Filters;
using PROMPERU.AULAVIRTUAL.WEB.Helpers;
using PROMPERU.AULAVIRTUAL.BE.CURSOS;
using PROMPERU.AULAVIRTUAL.BL;
using PROMPERU.AULAVIRTUAL.WEB.Logic;
using PROMPERU.AULAVIRTUAL.WEB.Models;
using PROMPERU.AULAVIRTUAL.WEB.ViewModel.Course;
using System;
using System.Collections.Generic;
using System.IO;
using System.IO.Compression;
using System.Linq;
using System.Transactions;
using System.Web;
using System.Web.Mvc;
using System.Web.UI.WebControls;
using System.Data;
using System.Web.Script.Serialization;
using PROMPERU.AULAVIRTUAL.Helpers;
using PagedList;
using PROMPERU.AULAVIRTUAL.BE.USUARIO;
using ClosedXML.Excel;
using System.Text;
using System.Text.RegularExpressions;

namespace PROMPERU.AULAVIRTUAL.WEB.Controllers
{
    public class CourseController : BaseController
    {
        //
        // GET: /Course/

        UsuarioBL usuarioBL = new UsuarioBL();
        CursosBL cursosBL = new CursosBL();
        EmpresaBL empresaBL = new EmpresaBL();
        JsonBL jsonBL = new JsonBL();


        public ActionResult Index()
        {
            return RedirectToAction("ListCursoOnlineAdmin");
        }

        [AppAuthorize(AppRol.Alumno, AppRol.Supervisor, AppRol.Administrador, AppRol.Empresa, AppRol.Ingeniero,
            AppRol.Proveedor, AppRol.Profesor)]
        public ActionResult ListCategoriaCursoOnline(int? p)
        {
            var viewModel = new ListCategoriaCursoOnlineViewModel();
            viewModel.p = p ?? 1;
            List<CategoriaCursoOnline> queryCategoriaCursoOnline = cursosBL.ListarCategoriaCursoOnline();
            queryCategoriaCursoOnline = queryCategoriaCursoOnline.OrderBy(x => x.Nombre).ToList();
            viewModel.LstCategoriaCursoOnline =
                queryCategoriaCursoOnline.ToPagedList(viewModel.p, ConstantHelpers.DEFAULT_PAGE_SIZE);
            return View("ListCategoriaCursoOnlineNew", viewModel);
        }

        [AppAuthorize(AppRol.Administrador, AppRol.Profesor)]
        public ActionResult EditCategoriaCursoOnline(int? CategoriaCursoOnlineId)
        {
            var viewModel = new EditCategoriaCursoOnlineViewModel();

            viewModel.CategoriaCursoOnlineId = CategoriaCursoOnlineId;

            if (CategoriaCursoOnlineId.HasValue)
            {
                List<CategoriaCursoOnline> lstCategoriaCursoOnline = cursosBL.ListarCategoriaCursoOnline();
                var categoriaCursoOnline =
                    lstCategoriaCursoOnline.Find(x => x.CategoriaCursoOnlineId == CategoriaCursoOnlineId);
                viewModel.CategoriaCursoOnlineId = categoriaCursoOnline.CategoriaCursoOnlineId;
                viewModel.Nombre = categoriaCursoOnline.Nombre;
                viewModel.Color = categoriaCursoOnline.Color;
            }

            return View("EditCategoriaCursoOnlineNew", viewModel);
        }

        [AppAuthorize(AppRol.Administrador, AppRol.Profesor)]
        public ActionResult EliminarCategoriaCursoOnline(int categoriaCursoOnlineId)
        {
            cursosBL.EliminarCategoriaCursoOnline(categoriaCursoOnlineId);

            return RedirectToAction("ListCategoriaCursoOnline");
        }
   
        [AppAuthorize(AppRol.Administrador, AppRol.Profesor)]
        public ActionResult DeleteCursoMaestroOnline(Int32 CursoOnlineId)
        {
            cursosBL.DeleteCursoMaestroOnline(CursoOnlineId);

            return RedirectToAction("ListCursoOnlineAdminMaestro");
        }

        [AppAuthorize(AppRol.Administrador, AppRol.Profesor)]
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult EditCategoriaCursoOnline(EditCategoriaCursoOnlineViewModel model)
        {
            try
            {
                List<CategoriaCursoOnline> lstCategoriaCursoOnline = cursosBL.ListarCategoriaCursoOnline();

                model.Color = "-";
                using (var TransactionScope = new TransactionScope())
                {
                    if (!ModelState.IsValid)
                    {
                        var categoriaCursoOnline = lstCategoriaCursoOnline.Find(x =>
                            x.CategoriaCursoOnlineId == model.CategoriaCursoOnlineId);
                        model.CategoriaCursoOnlineId = categoriaCursoOnline.CategoriaCursoOnlineId;
                        model.Nombre = categoriaCursoOnline.Nombre;
                        model.Color = categoriaCursoOnline.Color;
                        TryUpdateModel(model);
                        PostMessage(MessageType.Error);
                        return View(model);
                    }

                    var CategoriaCursoOnline = new CategoriaCursoOnline();

                    if (model.CategoriaCursoOnlineId.HasValue)
                    {
                        CategoriaCursoOnline = lstCategoriaCursoOnline.Find(x =>
                            x.CategoriaCursoOnlineId == model.CategoriaCursoOnlineId);
                        CategoriaCursoOnline.Nombre = model.Nombre;
                        CategoriaCursoOnline.Color = model.Color;
                        cursosBL.ActualizarCategoriaCursoOnline(CategoriaCursoOnline);
                    }
                    else
                    {
                        CategoriaCursoOnline.Nombre = model.Nombre;
                        CategoriaCursoOnline.Color = model.Color;
                        cursosBL.InsertarCategoriaCursoOnline(CategoriaCursoOnline);
                    }

                    TransactionScope.Complete();

                    PostMessage(MessageType.Success);
                    return RedirectToAction("ListCategoriaCursoOnline");
                }
            }
            catch (Exception ex)
            {
                PostMessage(MessageType.Error, ex.Message);


                model.CargarDatos(DatosContext, model.CategoriaCursoOnlineId);
                TryUpdateModel(model);
                return View(model);
            }
        }

        [AppAuthorize(AppRol.Alumno, AppRol.Supervisor, AppRol.Administrador, AppRol.Empresa, AppRol.Ingeniero,
            AppRol.Proveedor, AppRol.Profesor)]
        public ActionResult ListCursoOnline(Int32? p, String NombreCurso, Int32? CategoriaCursoId)
        {
            var viewModel = new ListCursoOnlineViewModel();

            viewModel.p = p ?? 1;
            Int32 usuarioId = Session.GetUsuarioId(); //dataContext.session.GetUsuarioId();
            viewModel.NombreCurso = NombreCurso;
            viewModel.CategoriaCursoId = CategoriaCursoId;

            //LstCategoriaCursoOnline = dataContext.context.CategoriaCursoOnline.ToList();

            viewModel.LstCategoriaCursoOnline = cursosBL.ListarCategoriaCursoOnline();

            var fechaInicioMatriculaVigente = ConstantHelpers.GET_FECHA_INICIO_MATRICULA_VIGENTE();

            viewModel.LstMatriculaCursoOnline = cursosBL.ListarMatriculaCursoOnline(usuarioId);
            viewModel.LstMatriculaCursoOnline = viewModel.LstMatriculaCursoOnline
                .Where
                (x =>
                    (
                        (x.Estado != ConstantHelpers.ESTADO_MATRICULA.INACTIVO &&
                         x.FechaMatricula >= fechaInicioMatriculaVigente)
                        || (x.Estado == ConstantHelpers.ESTADO_MATRICULA.COMPLETADO
                            || x.Estado == ConstantHelpers.ESTADO_MATRICULA.DESAPROBADO
                            || x.Estado == ConstantHelpers.ESTADO_MATRICULA.APROBADO)
                    )
                )
                .ToList();

            //LstCursoOnlineDisponibe = dataContext.context.DisponibilidadCursoOnline.Where(x => x.CursoOnline.Estado == ConstantHelpers.ESTADO.ACTIVO && (x.FechaFin == null || x.FechaFin > DateTime.Now)).Select(x => x.CursoOnlineId).ToList();// modificado por OTI

            viewModel.LstCursoOnlineDisponibe = cursosBL.ListarDisponibilidadCursoOnline()
                .Where(x => x.CursoOnline.Estado == ConstantHelpers.ESTADO.ACTIVO &&
                            (x.FechaFin == null || x.FechaFin > DateTime.Now)).Select(x => x.CursoOnlineId).ToList();

            //IQueryable<CursoOnline> queryCursoOnline = dataContext.context.DisponibilidadCursoOnline.Where(x => x.CursoOnline.Estado == ConstantHelpers.ESTADO.ACTIVO && (x.FechaFin == null || x.FechaFin > DateTime.Now) && x.CursoOnline.MatriculaCursoOnline.All(a => a.UsuarioId != usuarioId)).Select(x => x.CursoOnline).AsQueryable();

            List<CursoOnline> tmpDisponible = cursosBL.ListarDisponibilidadCursoOnlinePorCursoOnlineMatricula(usuarioId)
                .Where(x => x.CursoOnline.Estado == ConstantHelpers.ESTADO.ACTIVO &&
                            (x.FechaFin == null || x.FechaFin > DateTime.Now) &&
                            x.CursoOnline.MatriculaCursoOnline.All(a => a.UsuarioId != usuarioId))
                .Select(x => x.CursoOnline).ToList();

            List<CursoOnline> LstCursosDisponibles = tmpDisponible;
            LstCursosDisponibles.RemoveAll(x =>
                viewModel.LstMatriculaCursoOnline.Select(y => y.CursoOnlineId).Contains(x.CursoOnlineId));
            //.Where
            //(x =>
            //    (x.FechaFin == null || x.FechaFin > DateTime.Now)
            //).Select(x => x.CursoOnline).ToList();

            if (!String.IsNullOrEmpty(NombreCurso))
            {
                LstCursosDisponibles = LstCursosDisponibles
                    .Where(x => x.Nombre.ToUpper().Contains(NombreCurso.ToUpper())).ToList();
            }

            if (CategoriaCursoId.HasValue)
            {
                LstCursosDisponibles = LstCursosDisponibles.Where(x => x.CategoriaCursoOnlineId == CategoriaCursoId)
                    .ToList();
            }
            //Se edito la disponibilidad de cursos para que sea lo mismo que el home del dashboard
            //LstCursosDisponibles = LstCursosDisponibles.Where(x => x.DisponibilidadCurso == 1).ToList();

            viewModel.LstCursoOnline = LstCursosDisponibles.OrderBy(x => x.Orden).ThenByDescending(y => y.FechaCreacion)
                .ToPagedList(viewModel.p, ConstantHelpers.DEFAULT_PAGE_SIZE);

            viewModel.LstCursoOnlineTotal = LstCursosDisponibles.OrderBy(x => x.Orden)
                .ThenByDescending(y => y.FechaCreacion).ToList();

            //viewModel.CargarDatos(_DatosContext, p, NombreCurso, CategoriaCursoId);
            return View("ListCursoOnlineNew", viewModel);
        }

        [AppAuthorize(AppRol.Administrador, AppRol.Profesor)]
        public ActionResult ListCursoOnlineAdmin(Int32? p)
        {
            var viewModel = new ListCursoOnlineAdminViewModel();
            viewModel.p = p ?? 1;
            Int32 usuarioId = Session.GetUsuarioId();
            viewModel.LstDisponibilidadCursoOnline =
                cursosBL.ListarDisponibilidadCursoOnlinePorCursoOnlineMatricula(
                    usuarioId);
            viewModel.LstDisponibilidadCursoOnline = viewModel.LstDisponibilidadCursoOnline
                .Where(x => x.FechaFin == null || x.FechaFin > DateTime.Now)
                .OrderByDescending(x => x.CursoOnlineId)
                .ToList();


            List<CursoOnline> queryCursoOnline = cursosBL.ListarCursoOnline();
            queryCursoOnline = queryCursoOnline.OrderBy(x => x.Orden)
                .Where(x => x.Estado != ConstantHelpers.ESTADO.ELIMINADO)
                .ToList(); //aqui se agrego que no muestre los eliminados

            if (!String.IsNullOrEmpty(viewModel.NombreCurso))
                queryCursoOnline = queryCursoOnline.Where(x => x.Nombre.Contains(viewModel.NombreCurso)).ToList();

            viewModel.LstCursoOnline = queryCursoOnline.ToPagedList(viewModel.p, ConstantHelpers.DEFAULT_PAGE_SIZE);
            viewModel.DictCursoDisponibilidad = queryCursoOnline.ToDictionary(x => x.CursoOnlineId,
                x => viewModel.LstDisponibilidadCursoOnline.Exists(l => l.CursoOnlineId == x.CursoOnlineId));

            return View("ListCursoOnlineAdminNew", viewModel);
        }

        [AppAuthorize(AppRol.Administrador, AppRol.Profesor)]
        public ActionResult ListCursoOnlineAdminMaestro(Int32? p)
        {
            //var viewModel = new ListCursoMaestroOnlineAdminViewModel();
            //viewModel.CargarDatos(DatosContext, p);
            //return View("ListCursoOnlineAdminMaestroNew", viewModel);

            var viewModel = new ListCursoMaestroOnlineAdminViewModel();
            viewModel.p = p ?? 1;
            Int32 usuarioId = Session.GetUsuarioId();
            viewModel.LstDisponibilidadCursoOnline =
                cursosBL.ListarDisponibilidadCursoOnline();
            viewModel.LstDisponibilidadCursoOnline = viewModel.LstDisponibilidadCursoOnline
                .Where(x => x.FechaFin == null || x.FechaFin > DateTime.Now)
                .OrderByDescending(x => x.CursoOnlineId)
                .ToList();

            List<CursoOnlineMaestro> queryCursoOnline = cursosBL.ListarCursoOnlineMaestro();
            queryCursoOnline = queryCursoOnline.OrderBy(x => x.Orden)
                .Where(x => x.Estado != ConstantHelpers.ESTADO.ELIMINADO)
                .ToList(); //aqui se agrego que no muestre los eliminados

            if (!String.IsNullOrEmpty(viewModel.NombreCurso))
                queryCursoOnline = queryCursoOnline.Where(x => x.Nombre.Contains(viewModel.NombreCurso)).ToList();

            viewModel.LstCursoOnline = queryCursoOnline.ToPagedList(viewModel.p, ConstantHelpers.DEFAULT_PAGE_SIZE);
            viewModel.DictCursoDisponibilidad = queryCursoOnline.ToDictionary(x => x.CursoOnlineMaestroId,
                x => viewModel.LstDisponibilidadCursoOnline.Exists(l => l.CursoOnlineId == x.CursoOnlineMaestroId));

            return View("ListCursoOnlineAdminMaestroNew", viewModel);

        }

        [HttpPost]
        public ActionResult ListCursoOnlineAdmin(ListCursoOnlineAdminViewModel model)
        {
            var viewModel = model;
            viewModel.p = 1;
            Int32 usuarioId = Session.GetUsuarioId();
            viewModel.NombreCurso = model.NombreCurso;
            viewModel.LstDisponibilidadCursoOnline =
                cursosBL.ListarDisponibilidadCursoOnlinePorCursoOnlineMatricula(
                    usuarioId); //dataContext.context.DisponibilidadCursoOnline.Where(x => x.FechaFin == null || x.FechaFin > DateTime.Now).ToList();
            viewModel.LstDisponibilidadCursoOnline = viewModel.LstDisponibilidadCursoOnline
                .Where(x => x.FechaFin == null || x.FechaFin > DateTime.Now).ToList();


            List<CursoOnline> queryCursoOnline = cursosBL.ListarCursoOnline();
            queryCursoOnline = queryCursoOnline.OrderBy(x => x.Orden)
                .Where(x => x.Estado != ConstantHelpers.ESTADO.ELIMINADO)
                .ToList(); //aqui se agrego que no muestre los eliminados

            if (!String.IsNullOrEmpty(viewModel.NombreCurso))
                queryCursoOnline = queryCursoOnline
                    .Where(x => x.Nombre.ToUpper().Contains(viewModel.NombreCurso.ToUpper())).ToList();

            viewModel.LstCursoOnline = queryCursoOnline.ToPagedList(viewModel.p, ConstantHelpers.DEFAULT_PAGE_SIZE);
            viewModel.DictCursoDisponibilidad = queryCursoOnline.ToDictionary(x => x.CursoOnlineId,
                x => viewModel.LstDisponibilidadCursoOnline.Exists(l => l.CursoOnlineId == x.CursoOnlineId));

            //model.CargarDatos(_DatosContext, null);
            return View("ListCursoOnlineAdminNew", viewModel);
        }

        [AppAuthorize(AppRol.Alumno, AppRol.Empresa, AppRol.Supervisor, AppRol.Profesor)]
        public ActionResult ListCursoOnlineAlumno(Int32? p, String NombreCurso, Int32? CategoriaCursoId)
        {
            var viewModel = new ListCursoOnlineAlumnoViewModel();
            viewModel.CargarDatos(DatosContext, p, Session.GetUsuarioId(), NombreCurso, CategoriaCursoId);
            return View("ListCursoOnlineAlumnoNew", viewModel);
        }

        [AppAuthorize(AppRol.Administrador, AppRol.Profesor)]
        public ActionResult CursoOnlineAdminEncuesta(int id)
        {
            var viewModel = new EditCursoOnlineEncuestaViewModel();
            viewModel.CursoOnlineId = id;
            viewModel.CargarDatos(DatosContext, id);
            return View(viewModel);
        }

        [AppAuthorize(AppRol.Administrador, AppRol.Profesor)]
        public ActionResult CursoOnlineAdminResultadoEncuesta(int id)
        {
            var data = cursosBL.ListarCursoOnlineEncuestaResultado(id);

            DataTable resultadoEncuesta = new DataTable("ResultadoEncuesta");

            resultadoEncuesta.Columns.AddRange(new DataColumn[5] 
            {
                new DataColumn("CursoOnlineId", typeof(int)),
                new DataColumn("Calificacion", typeof(int)),
                new DataColumn("Pregunta", typeof(string)),
                new DataColumn("Respuesta", typeof(string)),
                new DataColumn("FechaRegistro", typeof(string))
            });

            foreach(var item in data)
            {
                resultadoEncuesta.Rows.Add(item.CursoOnlineId, item.Calificacion, item.Pregunta, item.Respuesta, item.FechaRegistro);
            }

            using(XLWorkbook wb = new XLWorkbook())
            {
                wb.Worksheets.Add(resultadoEncuesta, "EncuestaResultado");
                wb.Style.Alignment.Horizontal = XLAlignmentHorizontalValues.Center;
                wb.Style.Font.Bold = true;

                Response.Clear();
                Response.Buffer = true;
                Response.Charset = "";
                Response.ContentType = "application/vnd.openxmlformats-officedocument.spreadsheetml.sheet";
                Response.AddHeader("content-disposition", "attachment;filename= ListaResultadoEncuesta.xlsx");

                using (MemoryStream myMemoryStream = new MemoryStream())
                {
                    wb.SaveAs(myMemoryStream);
                    myMemoryStream.WriteTo(Response.OutputStream);
                    Response.Flush();
                    Response.End();
                }
            }

            return RedirectToAction("ViewCursoOnlineAdmin", new { CursoOnlineId = id });
        }

        [AppAuthorize(AppRol.Empresa, AppRol.Alumno, AppRol.Supervisor, AppRol.Profesor)]
        public ActionResult ListCursoOnlineDetalleAlumno(Int32? p, Int32? CursoOnlineId, String NombreCurso,
            Int32? CategoriaCursoId)
        {
            var viewModel = new ListCursoOnlineDetalleAlumnoViewModel();
            viewModel.CargarDatos(DatosContext, p, CursoOnlineId, Session.GetUsuarioId(), NombreCurso,
                CategoriaCursoId);
            return View("ListCursoOnlineDetalleAlumnoNew", viewModel);
        }


        [AppAuthorize(AppRol.Administrador, AppRol.Profesor)]
        public ActionResult ListMatriculaCursoOnline(Int32? CursoOnlineId, Int32? p)
        {
            var viewModel = new ListaMatriculaCursoOnlineViewModel();
            viewModel.CargarDatos(DatosContext, CursoOnlineId, p);
            return View(viewModel);
        }


        [AppAuthorize(AppRol.Empresa, AppRol.Alumno, AppRol.Supervisor)]
        [AppAuthorizeCursosMaestros(AppCursoMaestro.TieneCurso)]
        public ActionResult ListCursoOnlineMaestroAlumno(Int32? p, String NombreCurso, Int32? CategoriaCursoId)
        {
            var viewModel = new ListCursoOnlineMaestroAlumnoViewModel();

            viewModel.p = p ?? 1;
            viewModel.UsuarioId = Session.GetUsuarioId();
            viewModel.NombreCurso = NombreCurso;
            viewModel.CategoriaCursoId = CategoriaCursoId;
            viewModel.LstCategoriaCursoOnline =
                cursosBL.ListarCategoriaCursoOnline(); // dataContext.context.CategoriaCursoOnline.ToList();


            viewModel.LstCursoOnlineDisponibe = cursosBL.ListarDisponibilidadCursoOnlineId();
            //this.LstCursoOnlineDisponibe = dataContext.context.DisponibilidadCursoOnline.Where(x => x.FechaFin == null || x.FechaFin > DateTime.Now).Select(x => x.CursoOnlineId).ToList();

            var fechaInicioMatriculaVigente = ConstantHelpers.GET_FECHA_INICIO_MATRICULA_VIGENTE();

            viewModel.LstMatriculaCursoOnline = cursosBL.ListarMatriculaCursoOnline(viewModel.UsuarioId);
            viewModel.LstMatriculaCursoOnline = viewModel.LstMatriculaCursoOnline.Where(
                    (x => x.Estado != ConstantHelpers.ESTADO_MATRICULA.INACTIVO &
                          x.FechaMatricula >= fechaInicioMatriculaVigente)
                )
                .ToList();
            viewModel.LstMatriculaCursoOnline = viewModel.LstMatriculaCursoOnline.Where((x =>
                x.Estado == ConstantHelpers.ESTADO_MATRICULA.COMPLETADO |
                x.Estado == ConstantHelpers.ESTADO_MATRICULA.DESAPROBADO |
                x.Estado == ConstantHelpers.ESTADO_MATRICULA.APROBADO)).ToList();


            List<CursoOnlineMaestro> queryCursoOnline =
                cursosBL.ListarCursosMaestroUsuarioPorUsuarioId(viewModel
                    .UsuarioId); //dataContext.context.CursoOnlineMaestro.SqlQuery(@"USP_CursoOnlineMaestro_LIS {0}", usuarioId).AsQueryable();

            if (!String.IsNullOrEmpty(NombreCurso))
            {
                queryCursoOnline = queryCursoOnline.Where(x => x.Nombre.Contains(NombreCurso)).ToList();
            }

            if (CategoriaCursoId.HasValue)
            {
                queryCursoOnline = queryCursoOnline.Where(x => x.CategoriaCursoOnlineId == CategoriaCursoId).ToList();
            }


            viewModel.LstCursoOnline = queryCursoOnline.OrderBy(x => x.Orden).ThenByDescending(y => y.FechaCreacion)
                .ToPagedList(viewModel.p, ConstantHelpers.DEFAULT_PAGE_SIZE);

            //viewModel.CargarDatos(_DatosContext, p, Session.GetUsuarioId(), NombreCurso, CategoriaCursoId);
            return View("ListCursoOnlineMaestroAlumnoNew", viewModel);
        }

        [AppAuthorize(AppRol.Alumno, AppRol.Supervisor, AppRol.Administrador, AppRol.Empresa, AppRol.Ingeniero,
            AppRol.Proveedor, AppRol.Profesor)]
        public ActionResult ListCursoOnlineCategoria(Int32 CategoriaCUrsoOnlineId, Int32? p)
        {
            var viewModel = new ListCursoOnlineCategoriaViewModel();
            viewModel.CargarDatos(DatosContext, p, CategoriaCUrsoOnlineId);
            return View(viewModel);
        }

        [AppAuthorize(AppRol.Alumno, AppRol.Supervisor, AppRol.Administrador, AppRol.Empresa, AppRol.Ingeniero,
            AppRol.Proveedor, AppRol.Profesor)]
        public ActionResult ListNotificacion()
        {
            return View();
        }


        [AppAuthorize(AppRol.Administrador, AppRol.Profesor)]
        public ActionResult EditCursoOnline(Int32? CursoOnlineId)
        {
            var viewModel = new EditCursoOnlineViewModel();

            viewModel.CursoOnlineId = CursoOnlineId;

            if (CursoOnlineId.HasValue)
            {
                CursoOnline
                    cursoOnline =
                        cursosBL.ObtenerCursoOnlinePorId(
                            (int)CursoOnlineId); //dataContext.context.CursoOnline.Find(CursoOnlineId);
                viewModel.CursoOnlineId = cursoOnline.CursoOnlineId;
                viewModel.Codigo = cursoOnline.Codigo;
                viewModel.Nombre = cursoOnline.Nombre;
                viewModel.CategoriaCursoOnlineId = cursoOnline.CategoriaCursoOnlineId;
                viewModel.Estado = (cursoOnline.Estado == ConstantHelpers.ESTADO.ACTIVO);
                viewModel.Descripcion = cursoOnline.Descripcion;
                viewModel.NumPreguntasEvaluacion = cursoOnline.NumPreguntasEvaluacion;
                viewModel.TiempoEvaluacion = cursoOnline.TiempoEvaluacion;
                viewModel.PorcentajeAprobacion = cursoOnline.PorcentajeAprobacion.ToString();
                viewModel.RutaImagen = cursoOnline.RutaImagen;
                viewModel.TieneExamen = (cursoOnline.TieneExamen == ConstantHelpers.ESTADO.ACTIVO);
                viewModel.AutoMatricula = cursoOnline.AutoMatriculaHabilitada;
                viewModel.ReinicioScorm = cursoOnline.PermiteReinicioScorm != null
                    ? (bool)cursoOnline.PermiteReinicioScorm
                    : false;
                viewModel.RutaBanner = cursoOnline.RutaBanner;
                viewModel.DisponibleCurso = (cursoOnline.DisponibilidadCurso == 1);

                viewModel.UsuarioProfesor = cursoOnline.UsuarioProfesor;
            }

            viewModel.LstCategoriaCursoOnline = cursosBL.ListarCategoriaCursoOnline();
            viewModel.LstUsuarioProfesor = usuarioBL.ListarProfesoresActivos();

            return View("EditCursoOnlineNew", viewModel);
        }

        [HttpPost, ValidateInput(false), AppAuthorize(AppRol.Administrador, AppRol.Profesor), ValidateAntiForgeryToken]
        public ActionResult EditCursoOnline(EditCursoOnlineViewModel model, HttpPostedFileBase BannerImage)
        {
            try
            {
                using (var TransactionScope = new TransactionScope())
                {
                    //Codigo Promperu

                    String errormessage = "";

                    if (String.IsNullOrEmpty(model.RutaImagen) &&
                        (model.ArchivoImagen == null || model.ArchivoImagen.ContentLength == 0))
                    {
                        ModelState.AddModelError("ArchivoImagen", "Debe seleccionar una imagen");
                        errormessage = "Debe seleccionar una imagen";
                    }

                    //if (!ModelState.IsValid)
                    //{
                    //    TryUpdateModel(model);
                    //    PostMessage(MessageType.Error, errormessage);
                    //    return View(model);
                    //}

                    var CursoOnline = new CursoOnline();

                    if (model.CursoOnlineId.HasValue)
                    {
                        CursoOnline = cursosBL.ObtenerCursoOnlinePorId((int)model.CursoOnlineId);
                    }
                    else
                    {
                        CursoOnline.FechaCreacion = DateTime.Now;
                    }

                    string basePathAdditionalFiles = Server.MapPath("~/Content/bannercursos");
                    if (!Directory.Exists(basePathAdditionalFiles))
                    {
                        Directory.CreateDirectory(basePathAdditionalFiles);
                    }

                    if (BannerImage != null)
                    {
                        string extension = Path.GetExtension(BannerImage.FileName);
                        string filename = Guid.NewGuid().ToString() + extension;
                        string additionalFileName = Path.Combine(basePathAdditionalFiles, filename);
                        BannerImage.SaveAs(additionalFileName);
                        CursoOnline.RutaBanner = filename;
                    }
                    else
                    {
                        CursoOnline.RutaBanner = null;
                    }

                    CursoOnline.Codigo = model.Codigo;
                    CursoOnline.Nombre = model.Nombre;
                    CursoOnline.CategoriaCursoOnlineId = model.CategoriaCursoOnlineId;
                    CursoOnline.Estado =
                        (model.Estado) ? ConstantHelpers.ESTADO.ACTIVO : ConstantHelpers.ESTADO.INACTIVO;
                    CursoOnline.Descripcion = model.Descripcion;
                    CursoOnline.NumPreguntasEvaluacion = model.NumPreguntasEvaluacion;
                    CursoOnline.TiempoEvaluacion = model.TiempoEvaluacion;
                    CursoOnline.PorcentajeAprobacion = Convert.ToDecimal(model.PorcentajeAprobacion);
                    CursoOnline.TieneExamen = (model.TieneExamen)
                        ? ConstantHelpers.ESTADO.ACTIVO
                        : ConstantHelpers.ESTADO.INACTIVO;
                    CursoOnline.AutoMatriculaHabilitada = model.AutoMatricula;
                    CursoOnline.PermiteReinicioScorm = model.ReinicioScorm;
                    CursoOnline.DisponibilidadCurso =
                        (model.DisponibleCurso) ? 1 : 0; //1=activo para todos, 0=solo para curso maestro

                    CursoOnline.UsuarioCreacion = Session.GetUsuarioId();
                    CursoOnline.UsuarioProfesor = model.UsuarioProfesor;

                    if (model.ArchivoImagen != null && model.ArchivoImagen.ContentLength != 0)
                    {
                        var fileName = Guid.NewGuid().ToString() + "_" + Path.GetFileName(model.ArchivoImagen.FileName);
                        var path = Server.MapPath(ConstantHelpers.DEFAULT_SERVER_PATH);
                        if (!Directory.Exists(path))
                            Directory.CreateDirectory(path);

                        var image = System.Drawing.Image.FromStream(model.ArchivoImagen.InputStream);
                        image = image.GetThumbnailImage(280, (int)(280 * ((float)image.Height / image.Width)), null,
                            IntPtr.Zero);
                        image.Save(Path.Combine(path, fileName));
                        CursoOnline.RutaImagen = fileName;
                    }

                    if (model.CursoOnlineId.HasValue)
                    {
                        //CursoOnline = cursosBL.ObtenerCursoOnlinePorId((int)model.CursoOnlineId);
                        cursosBL.ActualizarCursoOnline(CursoOnline);
                    }
                    else
                    {
                        CursoOnline.FechaCreacion = DateTime.Now;
                        CursoOnline.CategoriaCursoOnlineId = model.CategoriaCursoOnlineId;
                        var cursoOnlineId = cursosBL.InsertarCursoOnline(CursoOnline);
                        CursoOnline.CursoOnlineId = (int)cursoOnlineId;
                    }

                    //context.SaveChanges();

                    TransactionScope.Complete();

                    PostMessage(MessageType.Success);
                    return RedirectToAction("ViewCursoOnlineAdmin", new { CursoOnlineId = CursoOnline.CursoOnlineId });
                }
            }
            catch (Exception ex)
            {
                //InvalidarContext();
                PostMessage(MessageType.Error);
                //model.CargarDatos(_DatosContext, model.CursoOnlineId);
                TryUpdateModel(model);
                return View("EditCursoOnlineNew", model);
            }
        }


        [HttpPost, ValidateInput(false), AppAuthorize(AppRol.Administrador, AppRol.Profesor), ValidateAntiForgeryToken]
        public ActionResult CursoOnlineAdminEncuesta(EditCursoOnlineEncuestaViewModel model)
        {
            try
            {
                using (var TransactionScope = new TransactionScope())
                {
                    String errormessage = "";

                    if (!ModelState.IsValid)
                    {
                        TryUpdateModel(model);
                        PostMessage(MessageType.Error, errormessage);
                        return View(model);
                    }

                    CursoOnlineEncuesta cursoOnlineEncuesta = new CursoOnlineEncuesta();
                    cursoOnlineEncuesta.Nombre = model.Nombre;
                    cursoOnlineEncuesta.Descripcion = model.Descripcion;
                    cursoOnlineEncuesta.Activo = model.Activo;
                    cursoOnlineEncuesta.CursoOnlineId = (int)model.CursoOnlineId;
                    cursoOnlineEncuesta.CursoOnlineEncuestaId = (int)model.CursoOnlineEncuestaId;
                    if (model.CursoOnlineEncuestaId > 0)
                    {
                        cursosBL.ActualizarCursoOnlineEncuesta(cursoOnlineEncuesta);
                    }
                    else
                    {
                        cursoOnlineEncuesta.CursoOnlineEncuestaId =
                            cursosBL.InsertarCursoOnlineEncuesta(cursoOnlineEncuesta);
                    }

                    var viewModel = new EditCursoOnlineEncuestaViewModel();
                    viewModel.CargarDatos(DatosContext, cursoOnlineEncuesta.CursoOnlineId);
                    TransactionScope.Complete();
                    PostMessage(MessageType.Success);
                    //return RedirectToAction("ViewCursoOnlineAdmin", new { CursoOnlineId = model.CursoOnlineId });
                    return RedirectToAction("CursoOnlineAdminEncuesta", new { id = model.CursoOnlineId });
                }
            }
            catch (Exception ex)
            {
                //InvalidarContext();
                PostMessage(MessageType.Error);
                //model.CargarDatos(_DatosContext, model.CursoOnlineId);
                TryUpdateModel(model);
                return View(model);
            }
        }

        [HttpPost, ValidateInput(false), AppAuthorize(AppRol.Administrador, AppRol.Profesor), ValidateAntiForgeryToken]
        public ActionResult CursoOnlineAdminEncuestaAdd(DetalleCursoOnlineEncuesta model, int CursoOnlineId)
        {
            try
            {
                using (var TransactionScope = new TransactionScope())
                {
                    String errormessage = "";

                    if (!ModelState.IsValid)
                    {
                        TryUpdateModel(model);
                        PostMessage(MessageType.Error, errormessage);
                        return View(model);
                    }

                    if (model.CursoOnlineEncuestaDetalleId > 0)
                    {
                        cursosBL.ActualizarCursoOnlineEncuestaDetalle(model);
                    }
                    else
                    {
                        model.CursoOnlineEncuestaDetalleId = cursosBL.InsertarCursoOnlineEncuestaDetalle(model);
                    }

                    var viewModel = new EditCursoOnlineEncuestaViewModel();
                    viewModel.CargarDatos(DatosContext, model.CursoOnlineEncuestaId);
                    TransactionScope.Complete();
                    PostMessage(MessageType.Success);
                    return RedirectToAction("CursoOnlineAdminEncuesta", new { Id = CursoOnlineId });
                }
            }
            catch (Exception ex)
            {
                //InvalidarContext();
                PostMessage(MessageType.Error);
                //model.CargarDatos(_DatosContext, model.CursoOnlineId);
                TryUpdateModel(model);
                return View(model);
            }
        }


        [AppAuthorize(AppRol.Administrador, AppRol.Profesor)]
        public ActionResult EditCursoMaestroOnline(Int32? CursoOnlineId)
        {
            var viewModel = new EditCursoMaestroOnlineViewModel();
            viewModel.CargarDatos(DatosContext, CursoOnlineId);
            return View("EditCursoMaestroOnlineNew", viewModel);
        }


        [AppAuthorize(AppRol.Administrador, AppRol.Profesor)]
        public ActionResult _DeleteCursoOnline(Int32? CursoOnlineId)
        {
            var viewModel = new _DeleteCursoOnlineViewModel();
            viewModel.CargarDatos(DatosContext, CursoOnlineId);
            return View();
        }


        [AppAuthorize(AppRol.Administrador, AppRol.Profesor)]
        public ActionResult _AddCursoOnline(Int32? CursoOnlineId)
        {
            var viewModel = new _AddCursoOnlineViewModel();
            viewModel.CargarDatos(DatosContext, CursoOnlineId);
            return View(viewModel);
        }


        [HttpPost, ValidateInput(false), AppAuthorize(AppRol.Administrador, AppRol.Profesor), ValidateAntiForgeryToken]
        public ActionResult _AddCursoOnline(_AddCursoOnlineViewModel model, HttpPostedFileBase BannerImage)
        {
            //try
            //{
            //    using (var TransactionScope = new TransactionScope())
            //    {
            //        //TODO: Transactionscope y stores

            //        List<DetalleCursoOnlineMaestro> ListCursoOnline = new List<DetalleCursoOnlineMaestro>();
            //        var serializer = new JavaScriptSerializer();
            //        dynamic result = serializer.DeserializeObject(model.ListItems);

            //        var CursoOnline = new DetalleCursoOnlineMaestro();

            //        if (model.CursoOnlineId.HasValue)
            //        {
            //            //ListCursoOnline = context.DetalleCursoOnlineMaestro.Where(x => x.CursoOnlineMaestroId == model.CursoOnlineId).ToList();
            //            ListCursoOnline = cursosBL.ListarCursoOnlineDetalleCursoOnlineMaestroPorCursoOnlineMaestroId(model.CursoOnlineId ?? 0);
            //            if (ListCursoOnline.Count == 0)
            //            {
            //                ListCursoOnline = null;
            //            }                 
            //        }

            //        if (ListCursoOnline != null)
            //        {
            //            foreach (var item in ListCursoOnline)
            //            {
            //                item.Estado = ConstantHelpers.ESTADO.ELIMINADO;
            //                //cursosBL.ActualizarDetalleCursoOnlineMaestro(item);
            //                //context.SaveChanges();
            //            }
            //        }
            //        else
            //        {
            //            CursoOnline = new DetalleCursoOnlineMaestro();
            //        }


            //        var i = 1;

            //        foreach (var item in result)
            //        {
            //            //context.DetalleCursoOnlineMaestro.Add(CursoOnline);
            //            CursoOnline.CursoOnlineId = int.Parse(item);
            //            CursoOnline.CursoOnlineMaestroId = (int)model.CursoOnlineId;
            //            CursoOnline.FechaCreacion = DateTime.Now;
            //            CursoOnline.Estado = ConstantHelpers.ESTADO.ACTIVO;
            //            CursoOnline.Orden = i;
            //            //cursosBL.InsertarDetalleCursoOnlineMaestro(CursoOnline);
            //            //context.SaveChanges();
            //            i++;
            //        }

            //        //context.SaveChanges();
            //        TransactionScope.Complete();
            //        PostMessage(MessageType.Success);
            //        return RedirectToAction("ListCursoOnlineAdminMaestro");

            //    }
            //}
            //catch (Exception e)
            //{
            //    //InvalidarContext();
            //    PostMessage(MessageType.Error);
            //    model.CargarDatos(_DatosContext, model.CursoOnlineId);
            //    TryUpdateModel(model);
            //    return View(model);
            //}
            PostMessage(MessageType.Success);
            return RedirectToAction("ListCursoOnlineAdminMaestro");
        }

        [HttpPost, ValidateInput(false), AppAuthorize(AppRol.Administrador, AppRol.Profesor), ValidateAntiForgeryToken]
        public ActionResult _DeleteCursoOnline(EditCursoOnlineViewModel model, HttpPostedFileBase BannerImage)
        {
            try
            {
                //TODO: TRANSACTIOnscope y stores

                using (var TransactionScope = new TransactionScope())
                {
                    var CursoOnline = new CursoOnline();
                    CursoOnline =
                        cursosBL.ObtenerCursoOnlinePorId((int)model
                            .CursoOnlineId); //context.CursoOnline.First(x => x.CursoOnlineId == model.CursoOnlineId);
                    CursoOnline.Estado = ConstantHelpers.ESTADO.ELIMINADO;
                    int _cursoOnlineId = cursosBL.ActualizarCursoOnline(CursoOnline);

                    TransactionScope.Complete();
                    PostMessage(MessageType.Success);
                    return RedirectToAction("ListCursoOnlineAdmin");
                }
            }
            catch (Exception)
            {
                //InvalidarContext();
                PostMessage(MessageType.Error);
                model.CargarDatos(DatosContext, model.CursoOnlineId);
                TryUpdateModel(model);
                return View(model);
            }
        }


        [HttpPost, ValidateInput(false), AppAuthorize(AppRol.Administrador, AppRol.Profesor), ValidateAntiForgeryToken]
        public ActionResult EditCursoMaestroOnline(EditCursoMaestroOnlineViewModel model,
            HttpPostedFileBase BannerImage)
        {
            try
            {
                //TODO: Transaction scope y stores
                using (var TransactionScope = new TransactionScope())
                {
                    //Codigo Promperu

                    String errormessage = "";

                    if (String.IsNullOrEmpty(model.RutaImagen) &&
                        (model.ArchivoImagen == null || model.ArchivoImagen.ContentLength == 0))
                    {
                        ModelState.AddModelError("ArchivoImagen", "Debe seleccionar una imagen");
                        errormessage = "Debe seleccionar una imagen";
                    }

                    if (!ModelState.IsValid)
                    {
                        model.CargarDatos(DatosContext, model.CursoOnlineId);
                        TryUpdateModel(model);
                        PostMessage(MessageType.Error, errormessage);
                        return View(model);
                    }

                    var CursoOnline = new CursoOnlineMaestro();

                    if (model.CursoOnlineId.HasValue)
                    {
                        //CursoOnline = context.CursoOnlineMaestro.First(x => x.CursoOnlineMaestroId == model.CursoOnlineId);
                    }
                    else
                    {
                        //Establecer las variables por defecto
                        CursoOnline.FechaCreacion = DateTime.Now;
                        //context.CursoOnlineMaestro.Add(CursoOnline);
                    }

                    string basePathAdditionalFiles = Server.MapPath("~/Content/bannercursos");
                    if (!Directory.Exists(basePathAdditionalFiles))
                    {
                        Directory.CreateDirectory(basePathAdditionalFiles);
                    }

                    if (BannerImage != null)
                    {
                        string extension = Path.GetExtension(BannerImage.FileName);
                        string filename = Guid.NewGuid().ToString() + extension;
                        string additionalFileName = Path.Combine(basePathAdditionalFiles, filename);
                        BannerImage.SaveAs(additionalFileName);
                        CursoOnline.RutaBanner = filename;
                    }
                    else
                    {
                        CursoOnline.RutaBanner = null;
                    }

                    CursoOnline.CursoOnlineMaestroId = Convert.ToInt32(model.CursoOnlineId);
                    CursoOnline.Codigo = model.Codigo;
                    CursoOnline.Nombre = model.Nombre;
                    CursoOnline.CategoriaCursoOnlineId = model.CategoriaCursoOnlineId;
                    CursoOnline.Estado =
                        (model.Estado) ? ConstantHelpers.ESTADO.ACTIVO : ConstantHelpers.ESTADO.INACTIVO;
                    CursoOnline.Descripcion = model.Descripcion;
                    CursoOnline.NumPreguntasEvaluacion = model.NumPreguntasEvaluacion;
                    CursoOnline.TiempoEvaluacion = model.TiempoEvaluacion;
                    //CursoOnline.PorcentajeAprobacion = model.PorcentajeAprobacion.ToDecimal();
                    CursoOnline.PorcentajeAprobacion = Convert.ToDecimal(model.PorcentajeAprobacion);

                    CursoOnline.TieneExamen = (model.TieneExamen)
                        ? ConstantHelpers.ESTADO.ACTIVO
                        : ConstantHelpers.ESTADO.INACTIVO;
                    CursoOnline.AutoMatriculaHabilitada = model.AutoMatricula;
                    CursoOnline.PermiteReinicioScorm = model.ReinicioScorm;


                    if (model.ArchivoImagen != null && model.ArchivoImagen.ContentLength != 0)
                    {
                        var fileName = Guid.NewGuid().ToString() + "_" + Path.GetFileName(model.ArchivoImagen.FileName);
                        var path = Server.MapPath(ConstantHelpers.DEFAULT_SERVER_PATH);
                        if (!Directory.Exists(path))
                            Directory.CreateDirectory(path);

                        var image = System.Drawing.Image.FromStream(model.ArchivoImagen.InputStream);
                        image = image.GetThumbnailImage(280, (int)(280 * ((float)image.Height / image.Width)), null,
                            IntPtr.Zero);
                        image.Save(Path.Combine(path, fileName));
                        CursoOnline.RutaImagen = fileName;
                    }


                    if (model.CursoOnlineId.HasValue)
                    {
                        //CursoOnline = cursosBL.ObtenerCursoOnlinePorId((int)model.CursoOnlineId);
                        cursosBL.ActualizarCursoOnlineMaestro(CursoOnline);
                    }
                    else
                    {
                        CursoOnline.FechaCreacion = DateTime.Now;
                        CursoOnline.CategoriaCursoOnlineId = model.CategoriaCursoOnlineId;
                        var cursoOnlineId = cursosBL.InsertarCursoOnlineMaestro(CursoOnline);
                        CursoOnline.CursoOnlineMaestroId = (int)cursoOnlineId;
                    }

                    //context.SaveChanges();

                    TransactionScope.Complete();

                    //PostMessage(MessageType.Success);
                    //return RedirectToAction("ViewCursoMaestroOnlineAdmin",
                    //    new { CursoOnlineId = CursoOnline.CursoOnlineMaestroId });

                    //PostMessage(MessageType.Success);
                    return RedirectToAction("ListCursoOnlineAdminMaestro",
                        new { CursoOnlineId = CursoOnline.CursoOnlineMaestroId });

                }
            }
            catch (Exception e)
            {
                //InvalidarContext();
                PostMessage(MessageType.Error);
                model.CargarDatos(DatosContext, model.CursoOnlineId);
                TryUpdateModel(model);
                return View("EditCursoMaestroOnlineNew", model);
            }
        }

        public PartialViewResult _OrderUnidad(Int32 CursoId, Int32? UnidadId)
        {
            var vm = new _OrderUnidadViewModel();
            vm.Fill(DatosContext, CursoId, UnidadId);
            return PartialView(vm);
        }

        [HttpPost]
        public JsonResult _OrderUnidad(_OrderUnidadViewModel model)
        {
            model.Register(DatosContext, model);
            PostMessage(MessageType.Success);
            return Json("Se ha modificado el oreden de los temas. Recargando...");
        }

        [AppAuthorize(AppRol.Administrador, AppRol.Profesor)]
        public ActionResult ViewCursoOnlineAdmin(Int32 CursoOnlineId)
        {
            try
            {
                var viewModel = new ViewCursoOnlineViewModelAdmin();


                viewModel.CursoOnlineId = CursoOnlineId;
                viewModel.CursoOnline = cursosBL.ObtenerCursoOnlinePorId(viewModel.CursoOnlineId);
                List<int> cursosDisponibles = cursosBL.ListarDisponibilidadCursoOnlineId();
                List<CategoriaCursoOnline> lstCategoriaCursoOnline = cursosBL.ListarCategoriaCursoOnline();
                List<UnidadCursoOnline> lstUnidadCursoOnline =
                    cursosBL.ListarUnidadCursoOnlinePorCursoOnlineId(CursoOnlineId);
                List<TipoUnidad> lstTipoUnidad = cursosBL.ListarTipoUnidad();

                foreach (UnidadCursoOnline UC in lstUnidadCursoOnline)
                {
                    UC.TipoUnidad1 = lstTipoUnidad.First(x => x.TipoUnidadId == UC.TipoUnidadId);
                }


                viewModel.EstaDisponible = cursosDisponibles.Any(x => x == CursoOnlineId);
                viewModel.CursoOnline = cursosBL.ObtenerCursoOnlinePorId(CursoOnlineId);


                CategoriaCursoOnline categoriaCursoOnline = lstCategoriaCursoOnline.First(x =>
                    x.CategoriaCursoOnlineId == viewModel.CursoOnline.CategoriaCursoOnlineId);
                viewModel.CursoOnline.CategoriaCursoOnline = categoriaCursoOnline;
                viewModel.CursoOnline.UnidadCursoOnline = lstUnidadCursoOnline;
                viewModel.LstUnidadCursoOnline =
                    lstUnidadCursoOnline.Where(x => x.UnidadCursoOnlinePadreId == null).ToList();
                viewModel.LstUnidadCursoOnline = viewModel.LstUnidadCursoOnline
                    .OrderByDescending(x => x.Estado == ConstantHelpers.ESTADO.ACTIVO).ThenBy(x => x.Orden).ToList();

                foreach (UnidadCursoOnline UCO in viewModel.LstUnidadCursoOnline)
                {
                    UCO.UnidadCursoOnline1 = lstUnidadCursoOnline
                        .Where(x => x.UnidadCursoOnlinePadreId == UCO.UnidadCursoOnlineId).ToList();
                }


                return View("ViewCursoOnlineAdminNew", viewModel);
            }
            catch (Exception e)
            {
                return RedirectToAction("ListCursoOnlineAdmin");
                throw;
            }
        }

        [AppAuthorize(AppRol.Administrador, AppRol.Profesor)]
        public ActionResult ViewCursoMaestroOnlineAdmin(Int32 CursoOnlineId)
        {
            try
            {
                var viewModel = new ViewCursoMaestroOnlineViewModelAdmin();
                viewModel.CargarDatos(DatosContext, CursoOnlineId);
                return View("ListCursoOnlineAlumnoNew", viewModel);
            }
            catch (Exception)
            {
                return RedirectToAction("ListCursoOnlineAlumnoNew");
                throw;
            }
        }

        [AppAuthorize(AppRol.Alumno, AppRol.Empresa, AppRol.Supervisor, AppRol.Administrador, AppRol.Profesor)]
        public ActionResult ViewCursoOnlineEncuesta(Int32 CursoOnlineId, Int32 matriculaCursoOnlineId)
        {
            var viewModel = new ViewCursoOnlineEncuestaViewModel();

            viewModel.CargarDatos(DatosContext, CursoOnlineId, matriculaCursoOnlineId);


            return View("ViewCursoOnlineEncuesta", viewModel);
        }

        [HttpPost]
        [ValidateInput(false)]
        [AppAuthorize(AppRol.Alumno)]
        [ValidateAntiForgeryToken]
        public ActionResult ViewCursoOnlineEncuesta(ViewCursoOnlineEncuestaViewModel viewCursoOnlineEncuestaViewModel)
        {
            CursoOnlineEncuestaRespuesta cursoOnlineEncuestaRespuesta = new CursoOnlineEncuestaRespuesta
            {
                Calificacion = viewCursoOnlineEncuestaViewModel.Calificacion,
                MatriculaCursoOnlineId = viewCursoOnlineEncuestaViewModel.MatriculaCursoOnlineId
            };

            cursoOnlineEncuestaRespuesta.CursoOnlineEncuestaRespuestaId =
                cursosBL.InsertarCursoOnlineEncuestaRespuesta(cursoOnlineEncuestaRespuesta);

            if (viewCursoOnlineEncuestaViewModel.respuestas == null)
                return RedirectToAction("ViewCursoOnlineEncuesta",
                    new
                    {
                        viewCursoOnlineEncuestaViewModel.CursoOnlineId, viewCursoOnlineEncuestaViewModel.MatriculaCursoOnlineId
                    });

            foreach (CursoOnlineEncuestaRespuestaDetalle oCursoOnlineEncuestaRespuestaDetalle in from claves in viewCursoOnlineEncuestaViewModel.respuestas.Keys let preguntaId = Convert.ToInt32(claves) select new CursoOnlineEncuestaRespuestaDetalle
                     {
                         Pregunta = viewCursoOnlineEncuestaViewModel.preguntas[claves],
                         Respuesta = viewCursoOnlineEncuestaViewModel.respuestas[claves],
                         CursoOnlineEncuestaRespuestaId = cursoOnlineEncuestaRespuesta.CursoOnlineEncuestaRespuestaId
                     })
            {
                cursosBL.InsertarCursoOnlineEncuestaRespuestaDetalle(oCursoOnlineEncuestaRespuestaDetalle);
            }

            //return RedirectToAction("ViewCursoOnlineEncuesta");
            return RedirectToAction("ViewCursoOnlineEncuesta",
                new
                {
                    viewCursoOnlineEncuestaViewModel.CursoOnlineId, viewCursoOnlineEncuestaViewModel.MatriculaCursoOnlineId
                });
        }

        [AppAuthorize(AppRol.Alumno, AppRol.Empresa, AppRol.Supervisor, AppRol.Administrador, AppRol.Profesor)]
        public ActionResult ViewCursoOnline(int cursoOnlineId, int? matriculaCursoOnlineId, bool? disponible)
        {
            try
            {
                if (disponible == true)
                {
                    ViewBag.Vista = "ListCursoOnline";
                    ViewBag.Name = "Cursos Disponibles";
                    ViewBag.Seccion = "CursosDisponibles";
                }
                else
                {
                    ViewBag.Vista = "ListCursoOnlineAlumno";
                    ViewBag.Name = "Mis Cursos";
                    ViewBag.Seccion = "MisCursos";
                }

                ViewCursoOnlineViewModel viewModel = new ViewCursoOnlineViewModel();

                viewModel.CargarDatos(DatosContext, cursoOnlineId, matriculaCursoOnlineId);


                return View("ViewCursoOnlineNew", viewModel);
            }
            catch (Exception ex)
            {
                PostMessage(new FlashMessage
                {
                    Title = "Error",
                    Body = ex.Message,
                    Type = MessageType.Error
                });
                return RedirectToAction("ListCursoOnlineAlumno");
            }
        }

        [HttpPost]
        [ValidateInput(false)]
        [AppAuthorize(AppRol.Alumno)]
        [ValidateAntiForgeryToken]
        public ActionResult InsertarTareaUnidadCursoOnline(int cursoOnlineId, int unidadCursoOnlineId, int? matriculaCursoOnlineId, string rutaTarea)
        {
            try
            {
                using (TransactionScope transactionScope = new TransactionScope())
                {
                    TareaUnidadCursoOnline tareaUnidadCursoOnline = new TareaUnidadCursoOnline
                    {
                        UnidadCursoOnlineId = unidadCursoOnlineId,
                        AlumnoId = DatosContext.Session.GetUsuarioId(),
                        RutaTarea = rutaTarea,
                        Estado = ConstantHelpers.ESTADO.PENDIENTE,
                        FechaEnvio = DateTime.UtcNow.AddHours(-5)
                    };

                    cursosBL.InsertarTareaUnidadCursoOnline(tareaUnidadCursoOnline);
                    transactionScope.Complete();
                }

                return RedirectToAction("ViewCursoOnline", new { cursoOnlineId, matriculaCursoOnlineId });
            }
            catch (Exception ex)
            {
                PostMessage(new FlashMessage
                {
                    Title = "Error",
                    Body = ex.Message,
                    Type = MessageType.Error
                });

                return RedirectToAction("ListCursoOnlineAlumno");
            }
        }

        [AppAuthorize(AppRol.Alumno, AppRol.Supervisor, AppRol.Administrador, AppRol.Empresa, AppRol.Ingeniero,
            AppRol.Proveedor, AppRol.Profesor)]
        public ActionResult ViewSiguienteUnidadCursoOnline(Int32 UnidadCursoOnlineId)
        {
            //TODO: Stores

            UnidadCursoOnline unidadCursoOnlineActual =
                cursosBL.ObtenerUnidadCursoOnlinePorId(
                    UnidadCursoOnlineId); //context.UnidadCursoOnline.FirstOrDefault(x => x.UnidadCursoOnlineId == UnidadCursoOnlineId);
            try
            {
                AvanceUnidadLogic avanceUnidadLogic = new AvanceUnidadLogic();

                UnidadCursoOnline unidadCursoOnlineSiguiente = cursosBL
                    .ListarUnidadCursoOnlinePorCursoOnlineId(unidadCursoOnlineActual.CursoOnlineId)
                    .Where(x => x.Estado == ConstantHelpers.ESTADO.ACTIVO
                                && x.Orden > unidadCursoOnlineActual.Orden
                                && x.TipoUnidadId == ConstantHelpers.TIPO_UNIDAD.DB_TEMA)
                    .OrderBy(x => x.Orden)
                    .FirstOrDefault();

                CursoOnline cursoOnline = cursosBL.ObtenerCursoOnlinePorId(unidadCursoOnlineActual.CursoOnlineId);

                if (AppRol.Administrador == Session.GetRol())
                {
                    return RedirectToAction("ViewUnidadCursoOnline",
                        new
                        {
                            CursoOnlineId = unidadCursoOnlineSiguiente.CursoOnlineId,
                            UnidadCursoOnlineId = unidadCursoOnlineSiguiente.UnidadCursoOnlineId
                        });
                }

                int usuarioId = Session.GetUsuarioId();

                DateTime fechaInicioMatriculaVigente = ConstantHelpers.GET_FECHA_INICIO_MATRICULA_VIGENTE();

                //var matriculaOnline = context.MatriculaCursoOnline.Where(x => x.CursoOnlineId == unidadCursoOnlineActual.CursoOnlineId && x.UsuarioId == usuarioId && x.Estado != ConstantHelpers.ESTADO_MATRICULA.INACTIVO && x.FechaMatricula >= fechaInicioMatriculaVigente).FirstOrDefault();
                MatriculaCursoOnline matriculaOnline = cursosBL
                    .ListarMatriculaCursoOnlinePorCursoOnlineIdPorUsuarioId(unidadCursoOnlineActual.CursoOnlineId, usuarioId)
                    .FirstOrDefault(x => 
                        x.Estado != ConstantHelpers.ESTADO_MATRICULA.INACTIVO && 
                        x.FechaMatricula >= fechaInicioMatriculaVigente);

                string estadoUnidadCursoOnline = avanceUnidadLogic
                    .ControlAvanceUnidad(matriculaOnline.MatriculaCursoOnlineId, UnidadCursoOnlineId);

                if (unidadCursoOnlineSiguiente != null)
                {
                    LogUnidad logUnidad = new LogUnidad
                    {
                        FechaRegistro = DateTime.Now,
                        MatriculaCursoOnlineId = matriculaOnline.MatriculaCursoOnlineId,
                        UnidadCursoOnlineId = UnidadCursoOnlineId,
                        UsuarioId = matriculaOnline.UsuarioId,
                        Estado = ConstantHelpers.ESTADO_UNIDAD.COMPLETADO
                    };

                    jsonBL.InsertarLogUnidad(logUnidad);

                    switch (estadoUnidadCursoOnline)
                    {
                        case ConstantHelpers.ESTADO_UNIDAD.COMPLETADO:
                            PostMessage(MessageType.Success,
                                "Ahora ya puedes rendir la evaluación de la unidad: " + unidadCursoOnlineActual.Nombre);
                            return RedirectToAction("ViewCursoOnline",
                                new
                                {
                                    CursoOnlineId = unidadCursoOnlineActual.CursoOnlineId,
                                    MatriculaCursoOnlineId = matriculaOnline.MatriculaCursoOnlineId
                                });
                            /*modificado por OTI*/
                        case ConstantHelpers.ESTADO_UNIDAD.PENDIENTE:
                            PostMessage(MessageType.Info, "Complete Esta Unidad");
                            return RedirectToAction("ViewUnidadCursoOnline",
                                new
                                {
                                    CursoOnlineId = unidadCursoOnlineActual.CursoOnlineId,
                                    UnidadCursoOnlineId = unidadCursoOnlineActual.UnidadCursoOnlineId
                                });
                    }
                }

                if (cursoOnline.TieneExamen != ConstantHelpers.ESTADO.INACTIVO)
                    return RedirectToAction("ViewCursoOnline",
                        new
                        {
                            unidadCursoOnlineActual.CursoOnlineId,
                            UnidadCursoOnlineId = unidadCursoOnlineSiguiente?.UnidadCursoOnlineId
                        });

                matriculaOnline.Estado = ConstantHelpers.ESTADO_MATRICULA.APROBADO;

                cursosBL.ActualizarMatriculaCursoOnline(matriculaOnline);

                //se agrego la unidad
                return RedirectToAction("ViewCursoOnline",
                    new
                    {
                        unidadCursoOnlineActual.CursoOnlineId, 
                        UnidadCursoOnlineId = unidadCursoOnlineSiguiente?.UnidadCursoOnlineId
                    });
            }
            catch (Exception)
            {
                return RedirectToAction(AppRol.Administrador == Session.GetRol() 
                    ? "ViewCursoOnlineAdmin" 
                    : "ViewCursoOnline", new { unidadCursoOnlineActual.CursoOnlineId });
            }
        }

        [AppAuthorize(AppRol.Alumno, AppRol.Supervisor, AppRol.Administrador, AppRol.Empresa, AppRol.Ingeniero,
            AppRol.Proveedor, AppRol.Profesor)]
        public ActionResult ViewUnidadCursoOnline(Int32 CursoOnlineId, Int32 UnidadCursoOnlineId)
        {
            var viewModel = new ViewUnidadCursoOnlineViewModel();
            try
            {
                AvanceUnidadLogic AvanceUnidadLogic = new AvanceUnidadLogic();

                Int32 usuarioId = Session.GetUsuarioId();

                if (Session.GetRol() == AppRol.Administrador)
                {
                    viewModel.CargarDatos(DatosContext, UnidadCursoOnlineId, CursoOnlineId, -1);
                    return View(viewModel);
                }

                var fechaInicioMatriculaVigente = ConstantHelpers.GET_FECHA_INICIO_MATRICULA_VIGENTE();
                var MatriculaCursoOnline = cursosBL
                    .ListarMatriculaCursoOnlinePorCursoOnlineIdPorUsuarioId(CursoOnlineId, usuarioId)
                    .FirstOrDefault(x => x.Estado != ConstantHelpers.ESTADO_MATRICULA.INACTIVO);

                //context.MatriculaCursoOnline.FirstOrDefault(x => x.CursoOnlineId == CursoOnlineId && x.UsuarioId == usuarioId && x.Estado != ConstantHelpers.ESTADO_MATRICULA.INACTIVO /*&& x.FechaMatricula >= fechaInicioMatriculaVigente*/);
                if (MatriculaCursoOnline == null)
                {
                    PostMessage(MessageType.Error, "Matrícula no encontrada",
                        "Para ingresar a esta unidad debe matricularse en el curso.");
                    return RedirectToAction("ViewCursoOnline", new { CursoOnlineId = CursoOnlineId });
                }

                String EstadoUnidadCursoOnline =
                    AvanceUnidadLogic.ControlAvanceUnidad(MatriculaCursoOnline.MatriculaCursoOnlineId,
                        UnidadCursoOnlineId);

                if (EstadoUnidadCursoOnline.Equals(ConstantHelpers.ESTADO_UNIDAD.COMPLETADO) ||
                    EstadoUnidadCursoOnline.Equals(ConstantHelpers.ESTADO_UNIDAD.PENDIENTE))
                {
                    var logUnidad = new LogUnidad();
                    logUnidad.FechaRegistro = DateTime.Now;
                    logUnidad.MatriculaCursoOnlineId = MatriculaCursoOnline.MatriculaCursoOnlineId;
                    logUnidad.UnidadCursoOnlineId = UnidadCursoOnlineId;
                    logUnidad.UsuarioId = usuarioId;
                    logUnidad.Estado = "INICIO";
                    //context.LogUnidad.Add(logUnidad);
                    //context.SaveChanges();

                    viewModel.CargarDatos(DatosContext, UnidadCursoOnlineId, CursoOnlineId,
                        MatriculaCursoOnline.MatriculaCursoOnlineId);
                    return View("ViewUnidadCursoOnlineNew", viewModel);
                }

                PostMessage(MessageType.Warning, "Complete las unidades anteriores");
                return RedirectToAction("ViewCursoOnline", new { CursoOnlineId = CursoOnlineId });
            }
            catch (Exception e)
            {
                PostMessage(MessageType.Error, "Usted no tiene acceso a la unidad");
                return RedirectToAction("ViewCursoOnline", new { CursoOnlineId = CursoOnlineId });
                throw;
            }
        }

        [AppAuthorize(AppRol.Administrador, AppRol.Profesor)]
        public ActionResult EditUnidadCursoOnline(Int32 CursoOnlineId, Int32? UnidadCursoOnlineId,
            int? UnidadCursoOnlinePadreId)
        {
            var viewModel = new EditUnidadCursoOnlineViewModel();
            viewModel.CargarDatos(DatosContext, UnidadCursoOnlineId, CursoOnlineId, UnidadCursoOnlinePadreId);
            return View("EditUnidadCursoOnlineNew", viewModel);
        }

        [HttpPost]
        [ValidateInput(false)]
        [AppAuthorize(AppRol.Administrador, AppRol.Profesor)]
        [ValidateAntiForgeryToken]
        public ActionResult EditUnidadCursoOnline(EditUnidadCursoOnlineViewModel model,
            HttpPostedFileBase ArchivoAdicional)
        {
            try
            {
                using (var TransactionScope = new TransactionScope())
                {
                    if (!ModelState.IsValid)
                    {
                        model.CargarDatos(DatosContext, model.UnidadCursoOnlineId, model.CursoOnlineId);
                        TryUpdateModel(model);
                        PostMessage(MessageType.Error);
                        return View(model);
                    }

                    var UnidadCursoOnline = new UnidadCursoOnline();

                    if (model.UnidadCursoOnlineId.HasValue)
                    {
                        List<UnidadCursoOnline> lstUnidadCursoOnline =
                            cursosBL.ListarUnidadCursoOnlinePorCursoOnlineId((int)model.CursoOnlineId);
                        UnidadCursoOnline =
                            lstUnidadCursoOnline.First(x => x.UnidadCursoOnlineId == model.UnidadCursoOnlineId);
                    }
                    else
                    {
                        //Establecer las variables por defecto
                        var cursoOnline = cursosBL.ObtenerCursoOnlinePorId(model.CursoOnlineId);
                        UnidadCursoOnline.Orden = cursoOnline.UnidadCursoOnline
                            .Where(x => x.Estado == ConstantHelpers.ESTADO.ACTIVO).Count() + 1;
                    }

                    string basePathAdditionalFiles = Server.MapPath("~/Archivos adicionales");
                    if (!Directory.Exists(basePathAdditionalFiles))
                    {
                        Directory.CreateDirectory(basePathAdditionalFiles);
                    }

                    if (ArchivoAdicional != null)
                    {
                        string extension = Path.GetExtension(ArchivoAdicional.FileName);
                        string additionalFileName =
                            Path.Combine(basePathAdditionalFiles, Guid.NewGuid().ToString() + extension);
                        ArchivoAdicional.SaveAs(additionalFileName);
                        UnidadCursoOnline.RutaArchivoAdicional = additionalFileName;
                        UnidadCursoOnline.ExtensionArchivoAdicional = extension;
                        UnidadCursoOnline.TipoContenidoArchivoAdicional = ArchivoAdicional.ContentType;
                    }

                    UnidadCursoOnline.CursoOnlineId = model.CursoOnlineId;
                    UnidadCursoOnline.Nombre = model.Nombre;
                    UnidadCursoOnline.Descripcion = model.Descripcion;
                    UnidadCursoOnline.RutaArchivoOriginal =
                        ConstantHelpers.GET_RELATIVE_COURSE_RESOUCE_PATH(model.CodigoCursoOnline, model.GUID);
                    UnidadCursoOnline.RutaArchivoInicio = model.RutaArchivoInicio;
                    UnidadCursoOnline.Estado =
                        (model.Estado) ? ConstantHelpers.ESTADO.ACTIVO : ConstantHelpers.ESTADO.INACTIVO;
                    UnidadCursoOnline.TiempoPermanencia = model.TiempoPermanencia;
                    UnidadCursoOnline.AnchoContenedor = model.AnchoContenedor;
                    UnidadCursoOnline.AltoContenedor = model.AltoContenedor;
                    UnidadCursoOnline.RutaWeb = model.RutaWeb;
                    UnidadCursoOnline.GUID = model.GUID;
                    UnidadCursoOnline.TipoUnidad = model.TipoUnidad;
                    UnidadCursoOnline.TipoUnidadId = model.TipoUnidadId;
                    UnidadCursoOnline.UnidadCursoOnlinePadreId = model.UnidadCursoOnlinePadreId;
                    UnidadCursoOnline.HasTarea = model.HasTarea;
                    UnidadCursoOnline.TareaFechaLimite = model.TareaFechaLimite;
                    int _unidadCursoOnlineId;
                    int _cursoOnlineId;

                    if (model.UnidadCursoOnlineId.HasValue)
                    {
                        _unidadCursoOnlineId = cursosBL.ActualizarUnidadCursoOnline(UnidadCursoOnline);
                        _unidadCursoOnlineId = UnidadCursoOnline.UnidadCursoOnlineId;
                    }
                    else
                    {
                        _unidadCursoOnlineId = cursosBL.InsertarUnidadCursoOnline(UnidadCursoOnline);
                    }

                    _cursoOnlineId = model.CursoOnlineId;
                    TransactionScope.Complete();

                    PostMessage(MessageType.Success);
                    return RedirectToAction("ViewCursoOnlineAdmin", new { CursoOnlineId = _cursoOnlineId });
                }
            }
            catch (Exception ex)
            {
                PostMessage(MessageType.Error);
                model.CargarDatos(DatosContext, model.UnidadCursoOnlineId, model.CursoOnlineId);
                TryUpdateModel(model);
                return View("EditUnidadCursoOnlineNew", model);
            }
        }

        [AppAuthorize(AppRol.Administrador, AppRol.Profesor)]
        public ActionResult ChangeStateUnidad(Int32 UnidadCursoOnlineId)
        {
            UnidadCursoOnline
                unidadCursoOnline =
                    cursosBL.ObtenerUnidadCursoOnlinePorId(
                        UnidadCursoOnlineId); //lstUnidadCursoOnline.Find(x => x.UnidadCursoOnlineId == UnidadCursoOnlineId);
            try
            {
                unidadCursoOnline.Estado = unidadCursoOnline.Estado.Equals(ConstantHelpers.ESTADO.ACTIVO)
                    ? ConstantHelpers.ESTADO.INACTIVO
                    : ConstantHelpers.ESTADO.ACTIVO;
                int _unidadCursoOnlineId = cursosBL.ActualizarUnidadCursoOnline(unidadCursoOnline);

                //context.SaveChanges();
                PostMessage(MessageType.Success);
            }
            catch (Exception ex)
            {
                PostMessage(MessageType.Error);
            }

            return RedirectToAction("ViewCursoOnlineAdmin", new { CursoOnlineId = unidadCursoOnline.CursoOnlineId });
        }

        [AppAuthorize(AppRol.Administrador, AppRol.Profesor)] //eliminiar unidad
        public ActionResult DeleteStateUnidad(Int32 UnidadCursoOnlineId)
        {
            UnidadCursoOnline unidadCursoOnline = cursosBL.ObtenerUnidadCursoOnlinePorId(UnidadCursoOnlineId);
            int cursoOnlineId = unidadCursoOnline.CursoOnlineId;
            try
            {
                unidadCursoOnline.Estado = unidadCursoOnline.Estado.Equals(ConstantHelpers.ESTADO.INACTIVO)
                    ? ConstantHelpers.ESTADO.ELIMINADO
                    : ConstantHelpers.ESTADO.INACTIVO;
                cursosBL.ActualizarUnidadCursoOnline(unidadCursoOnline);
                PostMessage(MessageType.Success);
            }
            catch
            {
                PostMessage(MessageType.Error);
            }

            return RedirectToAction("ViewCursoOnlineAdmin", new { CursoOnlineId = cursoOnlineId });
        }

        [AppAuthorize(AppRol.Alumno, AppRol.Empresa, AppRol.Supervisor, AppRol.Administrador, AppRol.Profesor)]
        public ActionResult HistoryMatriculaCursoOnline(Int32? p, String CadenaBuscar, Int32? UsuarioId)
        {
            var viewModel = new HistoryMatriculaCursoOnlineViewModel();

            viewModel.CadenaBuscar = CadenaBuscar;
            viewModel.p = p ?? 1;

            if (Session.GetRol() == AppRol.Administrador)
            {
                viewModel.UsuarioId = UsuarioId.Value;
            }
            else
            {
                viewModel.UsuarioId = Session.GetUsuarioId();
            }

            var query = cursosBL.ListarMatriculaCursoOnline(viewModel.UsuarioId);


            if (CadenaBuscar != null)
            {
                foreach (var token in CadenaBuscar.Split(' '))
                    query = query.Where(x => x.CursoOnline.Nombre.Contains(token)).ToList();
            }

            query = query.OrderByDescending(x => x.MatriculaCursoOnlineId).ToList();
            viewModel.LstMatriculaCursoOnline = query.ToPagedList(viewModel.p, ConstantHelpers.DEFAULT_PAGE_SIZE);

            //viewModel.CargarDatos(_DatosContext, p, CadenaBuscar, UsuarioId);
            return View("HistoryMatriculaCursoOnlineNew", viewModel);
        }

        [AppAuthorize(AppRol.Alumno, AppRol.Empresa, AppRol.Supervisor, AppRol.Administrador)]
        public ActionResult ViewCertificadoOnline(Int32? p, String CadenaBuscar, Int32? UsuarioId)
        {
            var viewModel = new ViewCertificadoViewModel();
            viewModel.CargarDatos(DatosContext, p, CadenaBuscar, UsuarioId);
            return View("ViewCertificadoOnlineNew", viewModel);
        }

        [AppAuthorize(AppRol.Alumno, AppRol.Empresa, AppRol.Supervisor, AppRol.Administrador)]
        public ActionResult ViewEnlaceInteresOnline()
        {
            var viewModel = new ViewEnlancesInteresViewModel();
            viewModel.CargarDatos(DatosContext);
            return View("ListEnlacesInteres", viewModel);
        }

        [AppAuthorize(AppRol.Alumno, AppRol.Supervisor, AppRol.Administrador, AppRol.Empresa, AppRol.Ingeniero,
            AppRol.Proveedor)]
        public ActionResult _DetalleCursoOnline(Int32 CursoOnlineId, Int32 MatriculaCursoOnlineId)
        {
            var _viewModel = new _DetalleCursoOnlineViewModel();
            _viewModel.CargarDatos(DatosContext, CursoOnlineId, MatriculaCursoOnlineId);
            return View(_viewModel);
        }

        [AppAuthorize(AppRol.Alumno, AppRol.Supervisor, AppRol.Administrador, AppRol.Empresa, AppRol.Ingeniero,
            AppRol.Proveedor)]
        public ActionResult ListAlumnoCursoOnline(Int32? p, Int32 UsuarioId, String CadenaBuscar)
        {
            var viewModel = new ListAlumnoCursoOnlineViewModel();

            viewModel.p = p ?? 1;
            viewModel.UsuarioId = UsuarioId;
            viewModel.CadenaBuscar = CadenaBuscar;

            viewModel.Usuario = usuarioBL.ObtenerUsuarioPorId(UsuarioId); //dataContext.context.Usuario.Find(UsuarioId);
            List<MatriculaCursoOnline> tempMatriculaCursoOnline = cursosBL.ListarMatriculaCursoOnline(UsuarioId);

            if (CadenaBuscar != null)
            {
                foreach (var token in CadenaBuscar.Split(' '))
                    tempMatriculaCursoOnline = tempMatriculaCursoOnline.Where(x => x.CursoOnline.Nombre.Contains(token))
                        .ToList();
            }

            tempMatriculaCursoOnline = tempMatriculaCursoOnline.OrderBy(x => x.MatriculaCursoOnlineId).ToList();
            viewModel.LstMatriculaCursoOnline =
                tempMatriculaCursoOnline.ToPagedList(viewModel.p, ConstantHelpers.DEFAULT_PAGE_SIZE);


            //viewModel.CargarDatos(_DatosContext, p, UsuarioId, CadenaBuscar);
            return View("ListAlumnoCursoOnlineNew", viewModel);
        }

        //    [AppAuthorize(AppRol.Alumno, AppRol.Supervisor, AppRol.Administrador, AppRol.Empresa, AppRol.Ingeniero, AppRol.Proveedor)]
        //    public ActionResult API(Int32 UnidadCursoOnlineId, Int32? MatriculaCursoOnlineId)
        //    {
        //        var viewModel = new ApiViewModel();
        //        viewModel.CargarDatos(UnidadCursoOnlineId, MatriculaCursoOnlineId);
        //        return View(viewModel);
        //    }

        public ContentResult _UploadArchivo(string codigoCursoOnline, string unidadCursoOnlineGuid)
        {
            try
            {
                if (Directory.Exists(ConstantHelpers.GET_SERVER_COURSE_RESOUCE_PATH(codigoCursoOnline, unidadCursoOnlineGuid)))
                {
                    DirectoryInfo directoryInfo = new DirectoryInfo(ConstantHelpers.GET_SERVER_COURSE_RESOUCE_PATH(codigoCursoOnline, unidadCursoOnlineGuid));
                    FileLogic fileLogic = new FileLogic();
                    fileLogic.DeleteArchivos(directoryInfo);
                }
                else
                {
                    Directory.CreateDirectory(ConstantHelpers.GET_SERVER_COURSE_RESOUCE_PATH(codigoCursoOnline, unidadCursoOnlineGuid));
                }

                HttpPostedFileBase uploadedFile = Request.Files["file"];
                ZipArchive zipArchive = new ZipArchive(uploadedFile.InputStream);
                //zipArchive.ExtractToDirectory(ConstantHelpers.GET_SERVER_COURSE_RESOUCE_PATH(CodigoCursoOnline, UnidadCursoOnlineGUID));

                return Content("Se cargó exitosamente");

            }
            catch (Exception ex)
            {
                throw;
            }
        }

        //    public ActionResult _SelectArchivo(String CodigoCursoOnline, String UnidadCursoOnlineGUID, String SelectedFile)
        //    {
        //        var viewModel = new _SelectArchivoViewModel();
        //        viewModel.CargarDatos(_DatosContext, CodigoCursoOnline, UnidadCursoOnlineGUID, SelectedFile);
        //        return PartialView(viewModel);
        //    }


        //    public JsonResult _GetTreeview(String CodigoCursoOnline, String UnidadCursoOnlineGUID, String SelectedFile)
        //    {
        //        try
        //        {

        //            var viewModel = new _SelectArchivoViewModel();
        //            TreeViewLogic treeViewLogic = new TreeViewLogic();
        //            String Path = ConstantHelpers.GET_SERVER_COURSE_RESOUCE_PATH(CodigoCursoOnline, UnidadCursoOnlineGUID);
        //            var rootDirectoryInfo = new DirectoryInfo(Path);
        //            return Json(treeViewLogic.CreateTreeView(rootDirectoryInfo, null, SelectedFile, String.Empty, 0), JsonRequestBehavior.AllowGet);
        //        }
        //        catch (Exception ex)
        //        {
        //            throw;
        //        }
        //    }

        //    //ordenar cursos por sorting

        [HttpPost]
        public ActionResult OrderCursoOnline(String Parametros)
        {
            try
            {
                var serializer = new JavaScriptSerializer();
                dynamic result = serializer.DeserializeObject(Parametros);

                using (var TransactionScope = new TransactionScope())
                {
                    //var lstCursoOnline = context.CursoOnline.Where(x => x.Estado == ConstantHelpers.ESTADO.ACTIVO).ToList();
                    var lstCursoOnline = cursosBL.ListarCursoOnline()
                        .Where(x => x.Estado == ConstantHelpers.ESTADO.ACTIVO).ToList();

                    for (var i = 0; i < result.Length; i++)
                    {
                        lstCursoOnline.FirstOrDefault(x => x.CursoOnlineId == int.Parse(result[i])).Orden = i + 1;
                        cursosBL.ActualizarCursoOnline(
                            lstCursoOnline.FirstOrDefault(x => x.CursoOnlineId == int.Parse(result[i])));
                    }

                    //context.SaveChanges();
                    TransactionScope.Complete();
                    PostMessage(MessageType.Success, "Se ordenó correctamente");
                    return Content("Éxito");
                }
            }
            catch (Exception e)
            {
                Util.SaveErrorLog(e);
                PostMessage(MessageType.Error, "No se ordenó correctamente");
                return Content("Error");
                throw;
            }

            return View();
        }

        [HttpPost]
        public ActionResult OrderUnidadCursoOnline(String LstUnidad, Int32 CursoOnlineId)
        {
            var cursoOnlineId = CursoOnlineId;
            try
            {
                LstUnidad = LstUnidad.Substring(1, LstUnidad.Length - 2);
                var LstUnidadCurso = LstUnidad.Split(',').Distinct().ToList();

                using (var TransactionScope = new TransactionScope())
                {
                    var queryUnidadCursoOnline =
                        cursosBL.ListarUnidadCursoOnlinePorCursoOnlineId(cursoOnlineId)
                            .AsQueryable(); //context.UnidadCursoOnline.Where(x => x.CursoOnlineId == cursoOnlineId).AsQueryable();
                    var lstUnidadCursoOnline = queryUnidadCursoOnline
                        .Where(x => x.Estado == ConstantHelpers.ESTADO.ACTIVO).ToList();

                    var i = 1;
                    foreach (var unidad in LstUnidadCurso)
                    {
                        var unidadid = Convert.ToInt32(unidad);
                        if (unidadid == 0)
                        {
                            continue;
                        }

                        var unidadCUrsoOnline =
                            cursosBL.ObtenerUnidadCursoOnlinePorId(
                                unidadid); // context.UnidadCursoOnline.FirstOrDefault(x => x.UnidadCursoOnlineId == unidadid);
                        unidadCUrsoOnline.Orden = i;
                        lstUnidadCursoOnline.FirstOrDefault(x => x.UnidadCursoOnlineId == unidadid).Orden = i;
                        i++;
                    }

                    lstUnidadCursoOnline = queryUnidadCursoOnline
                        .Where(x => x.Estado == ConstantHelpers.ESTADO.INACTIVO).ToList();
                    foreach (var unidad in lstUnidadCursoOnline)
                    {
                        var unidadCUrsoOnline =
                            cursosBL.ObtenerUnidadCursoOnlinePorId(unidad
                                .UnidadCursoOnlineId); //context.UnidadCursoOnline.FirstOrDefault(x => x.UnidadCursoOnlineId == unidad.UnidadCursoOnlineId);
                        unidadCUrsoOnline.Orden = 0;
                        cursosBL.ActualizarUnidadCursoOnline(unidadCUrsoOnline);
                    }


                    //context.SaveChanges();
                    TransactionScope.Complete();
                    PostMessage(MessageType.Success, "Se ordenó correctamente");
                    return Content("Éxito");
                }
            }
            catch (Exception ex)
            {
                PostMessage(MessageType.Error, "No se ordenó correctamente");
                return Content("Error");
                throw;
            }
        }

        //    public ActionResult SendNotificacion()
        //    {
        //        //TODO: STORES
        //        //var fechaActual = DateTime.Now;
        //        //var lstDisponibilidadCursoOnline = context.DisponibilidadCursoOnline.Include(x => x.CursoOnline).Where(x => x.FechaFin != null);
        //        //var lstCursoOnlineId = new List<Int32>();

        //        //foreach (var disponibilidadCursoOnline in lstDisponibilidadCursoOnline)
        //        //{
        //        //    var fechaFinCurso = disponibilidadCursoOnline.FechaFin;
        //        //    TimeSpan timeSpan = fechaFinCurso.Value.ToDateTime().Date - fechaActual.Date;

        //        //    if (timeSpan.TotalDays == 7)
        //        //    {
        //        //        lstCursoOnlineId.Add(disponibilidadCursoOnline.CursoOnlineId);
        //        //    }
        //        //}

        //        //foreach (var cursoOnlineId in lstCursoOnlineId)
        //        //{
        //        //    var lstMatriculaCursoOnline = context.UnidadCursoOnline.Include(x => x.CursoOnline).Include("CursoOnline.MatriculaCursoOnline").FirstOrDefault(x => x.CursoOnlineId == cursoOnlineId).CursoOnline.MatriculaCursoOnline.Where(x => x.Estado == ConstantHelpers.ESTADO_MATRICULA.PENDIENTE || x.Estado == ConstantHelpers.ESTADO_MATRICULA.COMPLETADO);

        //        //    foreach (var matriculaCursoOnline in lstMatriculaCursoOnline)
        //        //    {
        //        //        var emailLogic = new EmailLogic(context);
        //        //        emailLogic.SendMailOnEnrollment("RECORDATORIO", "Falta una semana para el cierre del curso ", matriculaCursoOnline.UsuarioId, matriculaCursoOnline.CursoOnlineId);
        //        //    }
        //        //}

        //        return View();

        //    }


        public ContentResult _UpdateAvanceMatriculaCursoOnline(Int32 MatriculaCursoOnlineId, Int32 UnidadCursoOnlineId)
        {
            try
            {
                AvanceUnidadLogic avanceUnidadLogic = new AvanceUnidadLogic();
                avanceUnidadLogic.UpdateAvanceCursoOnline(MatriculaCursoOnlineId, UnidadCursoOnlineId);

                return Content("exito");
            }
            catch (Exception ex)
            {
                return Content("Sucedió un error");
            }
        }


        //    [AppAuthorize(AppRol.Administrador, AppRol.Supervisor, AppRol.Alumno, AppRol.Empresa)]
        //    public ActionResult RestartParametroScorm(Int32 UnidadCursoOnlineId, Int32 MatriculaCursoOnlineId)
        //    {
        //        //TODO: Stores
        //        //try
        //        //{
        //        //    using (var ts = new TransactionScope())
        //        //    {
        //        //        var usuarioId = Session.GetUsuarioId();
        //        //        var lstParametroScorm = context.ParametroScorm.Where(x => x.MatriculaCursoOnlineId == MatriculaCursoOnlineId && x.UnidadCursoOnlineId == UnidadCursoOnlineId && x.UsuarioId == usuarioId).ToList();

        //        //        foreach (var item in lstParametroScorm)
        //        //        {
        //        //            context.Entry(item).State = EntityState.Deleted;
        //        //        }
        //        //        context.SaveChanges();

        //        //        ts.Complete();
        //        //        PostMessage(MessageType.Success, "Se reinicio la unidad de manera satisfactoria.");
        //        //    }
        //        //}
        //        //catch (Exception)
        //        //{
        //        //    PostMessage(MessageType.Error);
        //        //}
        //        return Redirect(Request.UrlReferrer.ToString());
        //    }

        //    [AppAuthorize(AppRol.Administrador)]
        //    public ActionResult DisableMatricula(Int32 MatriculaCursoOnlineId)
        //    {
        //        //TODO: Stores
        //        //try
        //        //{
        //        //    using (var ts = new TransactionScope())
        //        //    {
        //        //        var matricula = context.MatriculaCursoOnline.Find(MatriculaCursoOnlineId);

        //        //        if (matricula.Estado != ConstantHelpers.ESTADO_MATRICULA.PENDIENTE)
        //        //        {
        //        //            throw new Exception("El curso no se puede desactivar.");
        //        //        }
        //        //        matricula.Estado = ConstantHelpers.ESTADO_MATRICULA.INACTIVO;

        //        //        context.SaveChanges();

        //        //        ts.Complete();
        //        //        PostMessage(MessageType.Success, "Se desactivo la matricula");
        //        //    }
        //        //}
        //        //catch (Exception ex)
        //        //{
        //        //    PostMessage(MessageType.Error, ex.Message);
        //        //}
        //        return Redirect(Request.UrlReferrer.ToString());
        //    }

        public ActionResult RenderAdditionalFile(int UnidadCursoOnlineId)
        {
            UnidadCursoOnline unidadCursoOnline = cursosBL.ObtenerUnidadCursoOnlinePorId(UnidadCursoOnlineId);
            byte[] renderedBytes = System.IO.File.ReadAllBytes(unidadCursoOnline.RutaArchivoAdicional);
            HttpContext.Response.AddHeader("content-disposition",
                "inline; filename=" + unidadCursoOnline.Nombre + unidadCursoOnline.ExtensionArchivoAdicional);
            return File(renderedBytes, unidadCursoOnline.TipoContenidoArchivoAdicional);
        }

        public ActionResult RenderBanner(int CursoOnlineId)
        {
            CursoOnline
                unidadCursoOnline =
                    cursosBL.ObtenerCursoOnlinePorId(
                        CursoOnlineId); //context.CursoOnline.FirstOrDefault(x => x.CursoOnlineId == CursoOnlineId);
            byte[] renderedBytes =
                System.IO.File.ReadAllBytes("~/Archivos adicionales/" + unidadCursoOnline.RutaBanner);
            HttpContext.Response.AddHeader("content-disposition", "inline; filename=" + unidadCursoOnline.Nombre);
            return File(renderedBytes, "image/png");
        }

        public ActionResult _ViewContentAdditionalFile(int? UnidadCursoOnlineId, string ExternalLink, bool? hasFile,
            int? CursoOnlineId)
        {
            return View("_ViewContentAdditionalFile", new
            {
                UnidadCursoOnlineId,
                ExternalLink,
                hasFile,
                CursoOnlineId
            }.ToExpando());
        }


        [AppAuthorize(AppRol.Administrador, AppRol.Profesor)]
        public ActionResult VideoUnidadCursoOnline(Int32 unidadCursoOnlineId)
        {
            var viewModel = new VideoUnidadCursoOnlineViewModel();
            viewModel.CargarDatos(DatosContext, unidadCursoOnlineId);
            return View(viewModel);
        }

        [AppAuthorize(AppRol.Administrador, AppRol.Profesor)]
        public ActionResult EditVideoUnidadCursoOnline(Int32 unidadCursoOnlineId, Int32? videoUnidadCursoOnlineId)
        {
            var viewModel = new EditVideoUnidadCursoOnlineViewModel();
            viewModel.CargarDatos(DatosContext, unidadCursoOnlineId, videoUnidadCursoOnlineId);
            return View(viewModel);
        }

        [HttpPost]
        [ValidateInput(false)]
        [AppAuthorize(AppRol.Administrador, AppRol.Profesor)]
        [ValidateAntiForgeryToken]
        public ActionResult EditVideoUnidadCursoOnline(EditVideoUnidadCursoOnlineViewModel model)
        {
            try
            {
                using (var TransactionScope = new TransactionScope())
                {
                    var isCreate = !model.VideoUnidadCursoOnlineId.HasValue;

                    if (!ModelState.IsValid)
                    {
                        model.CargarDatos(DatosContext, model.UnidadCursoOnlineUnidadCursoOnlineId,
                            model.VideoUnidadCursoOnlineId);
                        TryUpdateModel(model);
                        PostMessage(MessageType.Error, String.Empty);
                        return RedirectToAction("VideoUnidadCursoOnline",
                            new { unidadCursoOnlineId = model.UnidadCursoOnlineUnidadCursoOnlineId });
                    }

                    var VideoUnidadCursoOnline = new VideoUnidadCursoOnline();

                    if (!isCreate)
                    {
                        VideoUnidadCursoOnline =
                            cursosBL.ObtenerVideoUnidadCursoOnlinePorId(model.VideoUnidadCursoOnlineId);
                    }
                    else
                    {
                        //context.VideoUnidadCursoOnline.Add(VideoUnidadCursoOnline);
                        VideoUnidadCursoOnline.UnidadCursoOnlineUnidadCursoOnlineId =
                            model.UnidadCursoOnlineUnidadCursoOnlineId;
                        VideoUnidadCursoOnline.Estado = ConstantHelpers.ESTADO.ACTIVO;
                    }

                    VideoUnidadCursoOnline.Titulo = model.Titulo;
                    VideoUnidadCursoOnline.CodigoYoutube = model.CodigoYoutube;
                    VideoUnidadCursoOnline.Descripcion = model.Descripcion;
                    VideoUnidadCursoOnline.Duracion = model.Duracion;
                    VideoUnidadCursoOnline.Tipo = model.Tipo;
                    VideoUnidadCursoOnline.Transcripcion = model.Transcripcion;
                    VideoUnidadCursoOnline.FechaTransmision = model.FechaTransmision;

                    //context.SaveChanges();
                    if (!isCreate)
                    {
                        cursosBL.ActualizarVideoUnidadCursoOnline(VideoUnidadCursoOnline);
                    }
                    else
                    {
                        cursosBL.InsertarVideoUnidadCursoOnline(VideoUnidadCursoOnline);
                    }

                    TransactionScope.Complete();

                    //PostMessage(MessageType.Success);
                    return RedirectToAction("VideoUnidadCursoOnline",
                        new { unidadCursoOnlineId = model.UnidadCursoOnlineUnidadCursoOnlineId });
                }
            }
            catch (Exception e)
            {
                //InvalidarContext();
                PostMessage(MessageType.Error, e.Message);
                return RedirectToAction("VideoUnidadCursoOnline",
                    new { unidadCursoOnlineId = model.UnidadCursoOnlineUnidadCursoOnlineId });
            }
        }

        [AppAuthorize(AppRol.Administrador, AppRol.Profesor)]
        public ActionResult ChangeStateVideoUnidadCursoOnline(Int32 videoUnidadCursoOnlineId)
        {
            try
            {
                using (var TransactionScope = new TransactionScope())
                {
                    VideoUnidadCursoOnline videoUnidadCursoOnline =
                        cursosBL.ObtenerVideoUnidadCursoOnlinePorId(videoUnidadCursoOnlineId);

                    videoUnidadCursoOnline.Estado = videoUnidadCursoOnline.Estado.Equals(ConstantHelpers.ESTADO.ACTIVO)
                        ? ConstantHelpers.ESTADO.INACTIVO
                        : ConstantHelpers.ESTADO.ACTIVO;

                    cursosBL.ActualizarVideoUnidadCursoOnline(videoUnidadCursoOnline);

                    TransactionScope.Complete();
                    PostMessage(MessageType.Success);
                    return RedirectToAction("VideoUnidadCursoOnline",
                        new { unidadCursoOnlineId = videoUnidadCursoOnlineId });
                }
            }
            catch (Exception e)
            {
                //InvalidarContext();
                PostMessage(MessageType.Error, e.Message);
                return RedirectToAction("VideoUnidadCursoOnline", new { unidadCursoOnlineId = Request.Url.ToString() });
            }
        }

        [AppAuthorize(AppRol.Administrador, AppRol.Profesor)]
        public ActionResult DeleteStateVideoUnidadCursoOnline(Int32 videoUnidadCursoOnlineId)
        {
            try
            {
                using (var TransactionScope = new TransactionScope())
                {
                    VideoUnidadCursoOnline videoUnidadCursoOnline =
                        cursosBL.ObtenerVideoUnidadCursoOnlinePorId(videoUnidadCursoOnlineId);

                    videoUnidadCursoOnline.Estado =
                        videoUnidadCursoOnline.Estado.Equals(ConstantHelpers.ESTADO.INACTIVO)
                            ? ConstantHelpers.ESTADO.ELIMINADO
                            : ConstantHelpers.ESTADO.INACTIVO;
                    cursosBL.ActualizarVideoUnidadCursoOnline(videoUnidadCursoOnline);
                    TransactionScope.Complete();
                    PostMessage(MessageType.Success);
                    return RedirectToAction("VideoUnidadCursoOnline",
                        new { unidadCursoOnlineId = videoUnidadCursoOnline.UnidadCursoOnlineUnidadCursoOnlineId });
                }
            }
            catch (Exception e)
            {
                //InvalidarContext();
                PostMessage(MessageType.Error, e.Message);
                return RedirectToAction("VideoUnidadCursoOnline", new { unidadCursoOnlineId = Request.Url.ToString() });
            }
        }

        [HttpPost, AppAuthorize(AppRol.Administrador, AppRol.Profesor)]
        public JsonResult HabilitarMatricula()
        {
            try
            {
                var paramIds = Request.Form.Get("ids");

                string[] ids = paramIds.TrimStart(',').Split(',');

                foreach (var id in ids)
                {
                    var matricula = cursosBL.ObtenerMatriculaCursoOnlinePorId(Int32.Parse(id));

                    matricula.Estado = ConstantHelpers.ESTADO_MATRICULA.PENDIENTE;

                    cursosBL.ActualizarMatriculaCursoOnline(matricula);
                }

                return Json(new
                {
                    status = true,
                    message = "Las matrículas se han habilitado correctamente."
                });
            }
            catch (Exception e)
            {
                return Json(new
                {
                    status = false,
                    message = e.Message
                });
            }
        }
    }
}