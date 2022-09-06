using PROMPERU.AULAVIRTUAL.WEB.Filters;
using PROMPERU.AULAVIRTUAL.WEB.Helpers;
using PROMPERU.AULAVIRTUAL.BE.CURSOS;
using PROMPERU.AULAVIRTUAL.BE.USUARIO;
using PROMPERU.AULAVIRTUAL.BL;
using PROMPERU.AULAVIRTUAL.WEB.Logic;
using PROMPERU.AULAVIRTUAL.WEB.Models;
using PROMPERU.AULAVIRTUAL.WEB.ViewModel;
using PROMPERU.AULAVIRTUAL.WEB.ViewModel.Evaluation;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
//using System.Data.Entity;
using System.Threading.Tasks;

namespace PROMPERU.AULAVIRTUAL.WEB.Controllers
{
    public class EvaluationController : BaseController
    {

        // GET: /Evaluation/

        UsuarioBL usuarioBL = new UsuarioBL();
        CursosBL cursosBL = new CursosBL();
        EmpresaBL empresaBL = new EmpresaBL();


        //[AppAuthorize(AppRol.Alumno, AppRol.Empresa, AppRol.Supervisor)]
        //public ActionResult DoEvaluacionCursoOnlineNoAsync(Int32 CursoOnlineId)
        //{
        //    try
        //    {
        //        var usuarioId = Session.GetUsuarioId();

        //        var fechaInicioMatriculaVigente = ConstantHelpers.GET_FECHA_INICIO_MATRICULA_VIGENTE();
        //        var matricula = context.MatriculaCursoOnline.Where(x => x.CursoOnlineId == CursoOnlineId && x.UsuarioId == usuarioId && x.Estado != ConstantHelpers.ESTADO_MATRICULA.INACTIVO && x.FechaMatricula >= fechaInicioMatriculaVigente).FirstOrDefault();

        //        if (matricula.Estado == ConstantHelpers.ESTADO_MATRICULA.APROBADO)
        //        {
        //            PostMessage(MessageType.Warning, "Usted ya aprobó el curso. No es necesario volver a rendir el examen.");
        //            return RedirectToAction("ViewCursoOnline", "Course", new { CursoOnlineId = matricula.CursoOnlineId });
        //        }

        //        if (matricula.Estado == ConstantHelpers.ESTADO_MATRICULA.INACTIVO)
        //        {
        //            PostMessage(MessageType.Warning, "La matrícula está inactiva. Por favor vuelva a matricularse.");
        //            return RedirectToAction("ViewCursoOnline", "Course", new { CursoOnlineId = matricula.CursoOnlineId });
        //        }

        //        if (matricula.Estado != ConstantHelpers.ESTADO_MATRICULA.COMPLETADO)
        //        {
        //            PostMessage(MessageType.Warning, "Para rendir el examen debe completar todas las unidades.");
        //            return RedirectToAction("ViewCursoOnline", "Course", new { CursoOnlineId = matricula.CursoOnlineId });
        //        }

        //        //Para el limite del dia anterior

        //        //var fechaAyer = DateTime.Now.AddDays(-1);
        //        //var ultimaEvaluacion = context.EvaluacionCursoOnline.Where(x => x.MatriculaCursoOnlineId == matricula.MatriculaCursoOnlineId && x.FechaInicio > fechaAyer && x.FechaInicio < DateTime.Now).OrderByDescending(x => x.FechaInicio).FirstOrDefault();
        //        //if (ultimaEvaluacion != null)
        //        //{
        //        //    var tiepoParaPrueba = ultimaEvaluacion.FechaInicio - fechaAyer;
        //        //    PostMessage(MessageType.Warning, "Usted podra dar la sigiente prueba en : " + tiepoParaPrueba);
        //        //    return RedirectToAction("ViewCursoOnline", "Course", new { CursoOnlineId = matricula.CursoOnlineId });
        //        //}

        //        var evaluationLogic = new EvaluationLogic(context);
        //        if (!evaluationLogic.ExixtenPreguntasCursoOnline(matricula.CursoOnlineId))
        //        {
        //            PostMessage(MessageType.Error, "No existen Preguntas Suficientes para generar un Examen");
        //            return RedirectToAction("ViewCursoOnline", "Course", new { CursoOnlineId = matricula.CursoOnlineId });
        //        }

        //        var evaluacionCursoOnlineId = evaluationLogic.GenerarEvaluacionActual(matricula.MatriculaCursoOnlineId);
        //        //var evaluacionCursoOnline = context.EvaluacionCursoOnline.Find(evaluacionCursoOnlineId);

        //        var viewModel = new DoEvaluacionCursoOnlineViewModel();
        //        viewModel.CargarDatos(_DatosContext, CursoOnlineId, evaluacionCursoOnlineId);
        //        return View(viewModel);
        //    }
        //    catch (Exception ex)
        //    {
        //        PostMessage(MessageType.Error, "No se pudo Generar la evaluacion solicidada. " + ex.Message);
        //        return RedirectToAction("ViewCursoOnline", "Course", new { CursoOnlineId = CursoOnlineId });
        //        throw;
        //    }
        //}

        public ActionResult DoEvaluacionCursoOnline(Int32 CursoOnlineId, Int32 UnidadCursoOnlineId)
        {
            try
            {
                var usuarioId = Session.GetUsuarioId();

                var fechaInicioMatriculaVigente = ConstantHelpers.GET_FECHA_INICIO_MATRICULA_VIGENTE();

                var matricula = cursosBL.ListarMatriculaCursoOnlinePorCursoOnlineIdPorUsuarioId(CursoOnlineId, usuarioId).Where
                                (x => x.CursoOnlineId == CursoOnlineId
                                && x.UsuarioId == usuarioId
                                && x.Estado != ConstantHelpers.ESTADO_MATRICULA.INACTIVO
                                && x.FechaMatricula >= fechaInicioMatriculaVigente).FirstOrDefault();




                //context.MatriculaCursoOnline.Where(x => x.CursoOnlineId == CursoOnlineId && x.UsuarioId == usuarioId && x.Estado != ConstantHelpers.ESTADO_MATRICULA.INACTIVO && x.FechaMatricula >= fechaInicioMatriculaVigente).FirstOrDefaultAsync();



                if (matricula.Estado == ConstantHelpers.ESTADO_MATRICULA.INACTIVO)
                {
                    PostMessage(MessageType.Warning, "La matrícula está inactiva. Por favor vuelva a matricularse.");
                    return RedirectToAction("ViewCursoOnline", "Course", new { CursoOnlineId = matricula.CursoOnlineId });
                }


                var evaluationLogic = new EvaluationLogic();
                if (!evaluationLogic.ExixtenPreguntasCursoOnlineAsync(UnidadCursoOnlineId))
                {
                    PostMessage(MessageType.Error, "No existen Preguntas Suficientes para generar un Examen");
                    return RedirectToAction("ViewCursoOnline", "Course", new { CursoOnlineId = matricula.CursoOnlineId });
                }

                var evaluacionCursoOnlineId =   evaluationLogic.GenerarEvaluacionActualAsync(matricula.MatriculaCursoOnlineId, UnidadCursoOnlineId);
                //var evaluacionCursoOnline = context.EvaluacionCursoOnline.Find(evaluacionCursoOnlineId);
                

                var viewModel = new DoEvaluacionCursoOnlineViewModel();
                viewModel.CargarDatos(CursoOnlineId, evaluacionCursoOnlineId);
                viewModel.UnidadCursoOnlineId = UnidadCursoOnlineId;
                viewModel.CursoOnline = cursosBL.ObtenerCursoOnlinePorId(CursoOnlineId);
                viewModel.EvaluacionCursoOnline = cursosBL.ObtenerEvaluacionCursoOnlinePorId(evaluacionCursoOnlineId);
                return View(viewModel);
            }
            catch (Exception ex)
            {
                PostMessage(MessageType.Error, "No se pudo Generar la evaluacion solicidada. " + ex.Message);
                return RedirectToAction("ViewCursoOnline", "Course", new { CursoOnlineId = CursoOnlineId });
                throw;
            }
        }




        [AppAuthorize(AppRol.Alumno, AppRol.Supervisor, AppRol.Empresa, AppRol.Administrador)]
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult DoEvaluacionCursoOnline(DoEvaluacionCursoOnlineViewModel model, FormCollection form)
        {
            //Aqui es donde hay que poner el porcentaje de avance y el porcentaje completado




            try
            {
                //var evaluacionCursoOnline = context.EvaluacionCursoOnline.Find(model.EvaluaciuonCursoOnlineId);
                var evaluacionCursoOnline = cursosBL.ObtenerEvaluacionCursoOnlinePorId(model.EvaluacionCursoOnlineId);
                //var lstPreguntaEvaluacionCursoOnline = context.PreguntaEvaluacionCursoOnline.Include(x => x.PreguntaCursoOnline).Where(x => x.EvaluacionCursoOnlineId == model.EvaluacionCursoOnlineId).ToList();
                var lstPreguntaEvaluacionCursoOnline = cursosBL.ListarPreguntaEvaluacionCursoOnlinePorEvaluacionCursoOnlineId(model.EvaluacionCursoOnlineId);
                var dictPreguntaEvaluacionCursoOnline = lstPreguntaEvaluacionCursoOnline.ToDictionary(x => x.PreguntaCursoOnlineId, x => x);
                var lstPreguntaCursoOnlineId = lstPreguntaEvaluacionCursoOnline.Select(x => x.PreguntaCursoOnlineId).ToList();
                //var dictRespuestasCursoOnline = context.RespuestaCursoOnline.Where(x => lstPreguntaCursoOnlineId.Contains(x.PreguntaCursoOnlineId) && x.EsSolucion).OrderBy(x => x.OrdenSolucion).ToList().GroupBy(x => x.PreguntaCursoOnlineId).ToDictionary(x => x.Key, x => x);

                var dictRespuestasCursoOnline = cursosBL.ListarRespuestaCursoOnlinePorEvaluacionCursoOnlineId(model.EvaluacionCursoOnlineId).Where(x => x.EsSolucion).OrderBy(x => x.OrdenSolucion).ToList().GroupBy(x => x.PreguntaCursoOnlineId).ToDictionary(x => x.Key, x => x);

                evaluacionCursoOnline.FechaFin = DateTime.Now;

                //context.RespuestaSeleccionadaEvaluacionCursoOnline.Where(x => x.PreguntaEvaluacionCursoOnline.EvaluacionCursoOnlineId == model.EvaluacionCursoOnlineId).ToList().ForEach(x => context.RespuestaSeleccionadaEvaluacionCursoOnline.Remove(x));
                int res = cursosBL.EliminarRespuestaSeleccionadaEvaluacionCursoOnlinePorEvaluacionCursoOnlineId(model.EvaluacionCursoOnlineId);

                var respuestasKey = form.AllKeys.Where(x => x.StartsWith("PRE-"));
                var puntajeObtenido = new Decimal();

                Queue<Int32> queueRespuestaCrusoOnlineId = new Queue<Int32>();
                foreach (var respuestaKey in respuestasKey)
                {
                    String respuesta = form[respuestaKey.ToString()];
                    Int32 preguntaId = respuestaKey.Split('-')[1].ToInteger();
                    List<Int32> respuestasId = respuesta.Split(',').Select(x => x.ToInteger()).ToList();

                    var preguntaEvaluacionCursoOnline = dictPreguntaEvaluacionCursoOnline[preguntaId];
                    var lstRespuestasCursoOnline = dictRespuestasCursoOnline[preguntaId];
                    var lstRespuestasCursoOnlineId = lstRespuestasCursoOnline.Select(x => x.RespuestaCursoOnlineId).ToList();
                    var ordenSeleccionado = 1;
                    if ((preguntaEvaluacionCursoOnline.PreguntaCursoOnline.Tipo == ConstantHelpers.TIPO_PREGUNTA.RELACIONAR && queueRespuestaCrusoOnlineId.Count() == 0) ||
                        (preguntaEvaluacionCursoOnline.PreguntaCursoOnline.Tipo == ConstantHelpers.TIPO_PREGUNTA.COMPLETAR && queueRespuestaCrusoOnlineId.Count() == 0))
                    {
                        queueRespuestaCrusoOnlineId = new Queue<Int32>(lstRespuestasCursoOnlineId.AsEnumerable());
                    }

                    var esRespuestaCorrecta = !(lstRespuestasCursoOnlineId.Except(respuestasId).Any() || respuestasId.Except(lstRespuestasCursoOnlineId).Any()); //Todas las seleccionadas son correctas;

                    if (preguntaEvaluacionCursoOnline.PreguntaCursoOnline.Tipo == ConstantHelpers.TIPO_PREGUNTA.COMPLETAR ||
                        preguntaEvaluacionCursoOnline.PreguntaCursoOnline.Tipo == ConstantHelpers.TIPO_PREGUNTA.RELACIONAR)
                    {
                        //esRespuestaCorrecta = respuestasId.SequenceEqual(lstRespuestasCursoOnlineId);

                        esRespuestaCorrecta = respuestasId.First() == queueRespuestaCrusoOnlineId.Dequeue();
                    }

                    preguntaEvaluacionCursoOnline.TieneRespuestaCorrecta = esRespuestaCorrecta;
                    if (esRespuestaCorrecta && (preguntaEvaluacionCursoOnline.PreguntaCursoOnline.Tipo == ConstantHelpers.TIPO_PREGUNTA.COMPLETAR ||
                                                preguntaEvaluacionCursoOnline.PreguntaCursoOnline.Tipo == ConstantHelpers.TIPO_PREGUNTA.RELACIONAR))
                        puntajeObtenido += 1 / lstRespuestasCursoOnlineId.Count().ToDecimal();
                    else if (esRespuestaCorrecta)
                        puntajeObtenido += preguntaEvaluacionCursoOnline.PreguntaCursoOnline.Puntaje;

                    foreach (var respuestaId in respuestasId)
                    {
                        var respuestaSeleccionadaEvaluacionCursoOnline = new RespuestaSeleccionadaEvaluacionCursoOnline();
                        respuestaSeleccionadaEvaluacionCursoOnline.PreguntaEvaluacionCursoOnlineId = preguntaEvaluacionCursoOnline.PreguntaEvaluacionCursoOnlineId;
                        respuestaSeleccionadaEvaluacionCursoOnline.RespuestaCursoOnlineId = respuestaId;
                        respuestaSeleccionadaEvaluacionCursoOnline.OrdenSeleccionado = ordenSeleccionado;

                        //context.RespuestaSeleccionadaEvaluacionCursoOnline.Add(respuestaSeleccionadaEvaluacionCursoOnline);
                        int resRSECO = cursosBL.InsertarRespuestaSeleccionadaEvaluacionCursoOnline(respuestaSeleccionadaEvaluacionCursoOnline);

                        ordenSeleccionado++;
                    }
                }

                evaluacionCursoOnline.PuntajeObtenido = puntajeObtenido;
                var nota = (puntajeObtenido / evaluacionCursoOnline.PuntajeTotal) * Convert.ToDecimal(100.00);
                evaluacionCursoOnline.Nota = Convert.ToDecimal(nota);

                //Guarda la evaluacion en la matricula para culminar el curso
                int resEval = cursosBL.ActualizarEvaluacionCursoOnline(evaluacionCursoOnline);

                //var matricula = context.MatriculaCursoOnline.Find(evaluacionCursoOnline.MatriculaCursoOnlineId);
                var matricula = cursosBL.ObtenerMatriculaCursoOnlinePorId(evaluacionCursoOnline.MatriculaCursoOnlineId);

                matricula.PorcentajeObtenido = evaluacionCursoOnline.Nota;
                matricula.Nota = Math.Round(nota * Convert.ToDecimal(20.0) / Convert.ToDecimal(100.0), 0).ToInteger();

                

                if (matricula.PorcentajeObtenido >= matricula.CursoOnline.PorcentajeAprobacion)
                {
                    matricula.Estado = ConstantHelpers.ESTADO_MATRICULA.APROBADO;
                    matricula.FechaAprobado = DateTime.Now;

                    SendNotificationOnEvaluationSucces(matricula);
                }
                else
                {
                    matricula.Estado = ConstantHelpers.ESTADO_MATRICULA.COMPLETADO;
                }

                //DESDE AQUI EL CAMBIO


                var lstUnidadCursoOnline = cursosBL.ListarUnidadCursoOnlinePorCursoOnlineId(model.CursoOnlineId).Where
                                        (x => x.CursoOnlineId == model.CursoOnlineId
                                            && x.Estado == ConstantHelpers.ESTADO.ACTIVO
                                            && x.TipoUnidadId == ConstantHelpers.TIPO_UNIDAD.DB_TEMA).
                                        OrderBy(x => x.Orden).ToList();
                var totalUnidadesCursoOnline = lstUnidadCursoOnline.Count();

                var lstUnidadCursoOnlineIdActiva = cursosBL.ListarAvanceMatriculaCursoOnlinePorMatriculaCursoOnlineId(matricula.MatriculaCursoOnlineId);

                var DictUnidadCursoOnline = lstUnidadCursoOnline.ToDictionary(x => x,
                        x => (lstUnidadCursoOnlineIdActiva.FirstOrDefault(a => a.UnidadCursoOnlineId == x.UnidadCursoOnlineId) == null ? ConstantHelpers.ESTADO_UNIDAD.INACTIVO :
                            (lstUnidadCursoOnlineIdActiva.FirstOrDefault(a => a.UnidadCursoOnlineId == x.UnidadCursoOnlineId && a.FechaFin.HasValue) != null ? ConstantHelpers.ESTADO_UNIDAD.COMPLETADO :
                            ConstantHelpers.ESTADO_UNIDAD.PENDIENTE)));

                var totalAvanceMatriculaCursoOnline = DictUnidadCursoOnline.Count(x => x.Value.Contains("FIN"));

                var porcentajeAvance = ((totalAvanceMatriculaCursoOnline * 1.0) / (totalUnidadesCursoOnline * 1.0) * 100).ToDecimal();

                matricula.PorcentajeAvance = porcentajeAvance;

                if (porcentajeAvance == 100 && matricula.FechaCompletado == null)
                {
                    matricula.FechaCompletado = DateTime.Now;
                    matricula.Estado = ConstantHelpers.ESTADO_MATRICULA.COMPLETADO;
                }






                int resMat = cursosBL.ActualizarMatriculaCursoOnline(matricula);

                return RedirectToAction("ResultEvaluacionCursoOnline", "Evaluation", new { EvaluacionCursoOnlineId = evaluacionCursoOnline.EvaluacionCursoOnlineId });

            }
            catch (Exception ex)
            {
                PostMessage(MessageType.Error);
                return RedirectToAction("ViewCursoOnline", "Course", new { CursoOnlineId = model.CursoOnlineId });
                throw;
            }
        }

        public void SendNotificationOnEvaluationSucces(MatriculaCursoOnline matricula)
        {
            try
            {
                //EmailLogic emailLogic = new EmailLogic(context);
                EmailLogic emailLogic = new EmailLogic();
                emailLogic.SendMailOnEvaluationSucces("Felicidades: Curso Aprobado ", "Usted Aprobó el examen del curso " + matricula.CursoOnline.Nombre + " satisfactoriamente ", matricula.UsuarioId, matricula.CursoOnlineId);
            }
            catch (Exception)
            {
                throw;
            }
        }


        [AppAuthorize(AppRol.Alumno, AppRol.Supervisor, AppRol.Empresa, AppRol.Administrador)]
        public ActionResult ResultEvaluacionCursoOnline(Int32 EvaluacionCursoOnlineId)
        {
            var viewModel = new ResultEvaluacionCursoOnlineViewModel();
            //viewModel.CargarDatos(_DatosContext, EvaluacionCursoOnlineId);
            viewModel.CargarDatos(EvaluacionCursoOnlineId);
            return View(viewModel);
        }

        [AppAuthorize(AppRol.Supervisor, AppRol.Administrador)]
        public ActionResult ViewHistorialEvaluacionAlumno(Int32 MatriculaCursoOnlineId, Int32 UsuarioId)
        {
            var viewModel = new ViewHistorialEvaluacionAlumnoViewModel();
            viewModel.CargarDatos(MatriculaCursoOnlineId, UsuarioId);
            return View(viewModel);


            //var viewModel = new ViewHistorialEvaluacionAlumnoViewModel();

            //viewModel.UsuarioId = UsuarioId;
            //viewModel.MatriculaCursoOnlineId = MatriculaCursoOnlineId;

            //viewModel.MatriculaCursoOnline = cursosBL.ObtenerMatriculaCursoOnlinePorId(MatriculaCursoOnlineId);
            ////TODO: TERMINAR LOS ULTIMOS STORES


            ////new MatriculaCursoOnline();// dataContext.context.MatriculaCursoOnline.Find(MatriculaCursoOnlineId);
            //viewModel.CursoOnline = cursosBL.ObtenerCursoOnlinePorId(viewModel.MatriculaCursoOnline.CursoOnlineId);    //CursoOnline(); // dataContext.context.CursoOnline.Find(MatriculaCursoOnline.CursoOnlineId);
            //var Preguntasvalidas = viewModel.CursoOnline.UnidadCursoOnline.Select(a => a.PreguntaCursoOnline.Select(l => l.PreguntaCursoOnlineId).Count()).ToList();
            //viewModel.Usuario = usuarioBL.ObtenerUsuarioPorId(UsuarioId); //dataContext.context.Usuario.Find(usuarioId);
            //viewModel.CantidadPreguntasEvaluaciones = Preguntasvalidas.Sum();
            //viewModel.LstEvaluacionCursoOnline = new List<EvaluacionCursoOnline>(); //dataContext.context.EvaluacionCursoOnline.Where(x => x.MatriculaCursoOnlineId == MatriculaCursoOnlineId).ToList();
            ////viewModel.CargarDatos(_DatosContext, MatriculaCursoOnlineId, UsuarioId);
            //return View(viewModel);
        }

        [AppAuthorize(AppRol.Alumno, AppRol.Empresa, AppRol.Supervisor)]
        public ActionResult ViewHistorialEvaluacion(Int32 MatriculaCursoOnlineId)
        {
            var viewModel = new ViewHistorialEvaluacionAlumnoViewModel();
            viewModel.CargarDatos(MatriculaCursoOnlineId, Session.GetUsuarioId());
            return View(viewModel);
        }

    }
}
