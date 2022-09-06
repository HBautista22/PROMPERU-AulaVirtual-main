//using PROMPERU.AULAVIRTUAL.WEB.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
//using System.Data.Entity;
using PROMPERU.AULAVIRTUAL.WEB.Helpers;
using System.Threading.Tasks;
using PROMPERU.AULAVIRTUAL.BE.CURSOS;
using PROMPERU.AULAVIRTUAL.BL;

namespace PROMPERU.AULAVIRTUAL.WEB.Logic
{
    public class EvaluationLogic
    {
        //public EvolEntities context { get; set; }
        CursosBL cursosBL = new CursosBL();

        public EvaluationLogic()
        {
        }

        //TODO : NO SE USA VERIFICAR
        //public Int32 GenerarEvaluacionActual(Int32 matriculaCursoOnlineId)
        //{

        //    var matriculaCursoOnline = cursosBL.ObtenerMatriculaCursoOnlinePorId(matriculaCursoOnlineId);    //context.MatriculaCursoOnline.Include(x => x.CursoOnline).FirstOrDefault(x => x.MatriculaCursoOnlineId == matriculaCursoOnlineId);
        //    var cursoOnline = cursosBL.ObtenerCursoOnlinePorId(matriculaCursoOnline.CursoOnlineId);   //matriculaCursoOnline.CursoOnline;

        //    var evaluacionExistente = cursosBL.ListarEvaluacionCursoOnlinePorMatriculaCursoOnlineIdAgrupado(matriculaCursoOnlineId).Where
        //                                (x => x.MatriculaCursoOnlineId == matriculaCursoOnlineId && x.FechaFin == null).OrderByDescending(x => x.FechaInicio).FirstOrDefault();    //context.EvaluacionCursoOnline.Where(x => x.MatriculaCursoOnlineId == matriculaCursoOnlineId && x.FechaFin == null).OrderByDescending(x => x.FechaInicio).FirstOrDefault();

        //    if (evaluacionExistente != null && evaluacionExistente.FechaInicio.AddMinutes(cursoOnline.TiempoEvaluacion) > DateTime.Now)
        //        return evaluacionExistente.EvaluacionCursoOnlineId;

        //    //generación de nueva evaluación

        //    var lstPreguntas = cursosBL.ListarPreguntaCursoOnlinePorCursoOnlineId(cursoOnline.CursoOnlineId);   //context.PreguntaCursoOnline.Include(x => x.RespuestaCursoOnline).Where(x => x.CursoOnlineId == cursoOnline.CursoOnlineId && x.Estado == ConstantHelpers.ESTADO.ACTIVO).ToList();
        //    var random = new Random();

        //    var lstPreguntasEvaluacion = lstPreguntas.OrderBy(x => random.Next()).Take(Math.Min(cursoOnline.NumPreguntasEvaluacion, lstPreguntas.Count)).ToList();

        //    var evaluacion = new EvaluacionCursoOnline();
        //    evaluacion.FechaInicio = DateTime.Now;
        //    evaluacion.MatriculaCursoOnlineId = matriculaCursoOnlineId;
        //    evaluacion.PorcentajeAprobacion = cursoOnline.PorcentajeAprobacion;
        //    context.EvaluacionCursoOnline.Add(evaluacion);

        //    var puntajeTotal = 0;

        //    foreach (var pregunta in lstPreguntasEvaluacion)
        //    {
        //        puntajeTotal += pregunta.Puntaje;

        //        var preguntaEvaluacion = new PreguntaEvaluacionCursoOnline();
        //        preguntaEvaluacion.PreguntaCursoOnlineId = pregunta.PreguntaCursoOnlineId;

        //        var respuestasSolucion = pregunta.RespuestaCursoOnline.Where(x => x.EsSolucion).ToList();
        //        var numRespuestasSolucion = respuestasSolucion.Count();
        //        var respuestasNoSolucion = pregunta.RespuestaCursoOnline.Where(x => !x.EsSolucion).Take(Math.Max(pregunta.NumRespuestas - numRespuestasSolucion, 0));

        //        var respuestas = respuestasSolucion;
        //        respuestas.AddRange(respuestasNoSolucion);
        //        respuestas = respuestas.OrderBy(x => random.Next()).ToList();

        //        var ordenMostrado = 1;

        //        foreach (var respuesta in respuestas)
        //        {
        //            var respuestaEvaluacion = new RespuestaEvaluacionCursoOnline();
        //            respuestaEvaluacion.OrdenMostrado = ordenMostrado;
        //            respuestaEvaluacion.RespuestaCursoOnlineId = respuesta.RespuestaCursoOnlineId;

        //            preguntaEvaluacion.RespuestaEvaluacionCursoOnline.Add(respuestaEvaluacion);
        //            ordenMostrado++;
        //        }

        //        evaluacion.PreguntaEvaluacionCursoOnline.Add(preguntaEvaluacion);
        //    }

        //    evaluacion.PuntajeTotal = puntajeTotal;
        //    context.SaveChanges();

        //    return evaluacion.EvaluacionCursoOnlineId;
        //}



        public Int32 GenerarEvaluacionActualAsync(Int32 matriculaCursoOnlineId, Int32 unidadCursoOnlineId)
        {
            MatriculaCursoOnline matriculaCursoOnline = cursosBL.ObtenerMatriculaCursoOnlinePorId(matriculaCursoOnlineId);   //context.MatriculaCursoOnline.Include(x => x.CursoOnline).FirstOrDefault(x => x.MatriculaCursoOnlineId == matriculaCursoOnlineId);
            matriculaCursoOnline.CursoOnline = cursosBL.ObtenerCursoOnlinePorId(matriculaCursoOnline.CursoOnlineId);
            EvaluacionCursoOnline evaluacionExistente = cursosBL.ListarEvaluacionCursoOnlinePorCursoOnlineId(matriculaCursoOnline.CursoOnlineId).
                                Where(x => x.MatriculaCursoOnlineId == matriculaCursoOnlineId && x.FechaFin == null).
                                OrderByDescending(x => x.FechaInicio).FirstOrDefault();    //context.EvaluacionCursoOnline.Where(x => x.MatriculaCursoOnlineId == matriculaCursoOnlineId && x.FechaFin == null).OrderByDescending(x => x.FechaInicio).FirstOrDefault();

            
            CursoOnline cursoOnline = matriculaCursoOnline.CursoOnline;

            if (evaluacionExistente != null && evaluacionExistente.FechaInicio.AddMinutes(cursoOnline.TiempoEvaluacion) > DateTime.Now)
            {
                return evaluacionExistente.EvaluacionCursoOnlineId;
            }

            //generación de nueva evaluación
            var random = new Random();
            var lstPreguntas = cursosBL.ListarPreguntaCursoOnlinePorUnidadCursoOnlineId(unidadCursoOnlineId)
                .Where(x => x.Estado == ConstantHelpers.ESTADO.ACTIVO).ToList();    //new List<PreguntaCursoOnline>();
                //await context.PreguntaCursoOnline.Include(x => x.RespuestaCursoOnline).Where(x => x.UnidadOnlineId == unidadCursoOnlineId && x.Estado == ConstantHelpers.ESTADO.ACTIVO).ToListAsync();

            var lstPreguntasEvaluacion = lstPreguntas.OrderBy(x => random.Next()).Take(Math.Min((sbyte)cursoOnline.NumPreguntasEvaluacion, lstPreguntas.Count())).ToList();

            var evaluacion = new EvaluacionCursoOnline();
            evaluacion.FechaInicio = DateTime.Now;
            evaluacion.MatriculaCursoOnlineId = matriculaCursoOnlineId;
            evaluacion.PorcentajeAprobacion = cursoOnline.PorcentajeAprobacion;
            evaluacion.UnidadCursoOnlineId = unidadCursoOnlineId;

            var puntajeTotal = 0;

             foreach (var pregunta in lstPreguntasEvaluacion)
            {
                puntajeTotal += pregunta.Puntaje;
            }
            evaluacion.PuntajeTotal = puntajeTotal;

            //context.EvaluacionCursoOnline.Add(evaluacion);
            int res = cursosBL.InsertarEvaluacionCursoOnline(evaluacion);
            evaluacion.EvaluacionCursoOnlineId = res;

            foreach (var pregunta in lstPreguntasEvaluacion)
            {
                puntajeTotal += pregunta.Puntaje;

                var preguntaEvaluacion = new PreguntaEvaluacionCursoOnline();
                preguntaEvaluacion.PreguntaCursoOnlineId = pregunta.PreguntaCursoOnlineId;

                var respuestasSolucion = pregunta.RespuestaCursoOnline.Where(x => x.EsSolucion).ToList();
                var numRespuestasSolucion = respuestasSolucion.Count();
                var respuestasNoSolucion = pregunta.RespuestaCursoOnline.Where(x => !x.EsSolucion).Take(Math.Max(pregunta.NumRespuestas - numRespuestasSolucion, 0));

                var respuestas = respuestasSolucion;
                respuestas.AddRange(respuestasNoSolucion);
                respuestas = respuestas.OrderBy(x => random.Next()).ToList();

                var ordenMostrado = 1;

                preguntaEvaluacion.EvaluacionCursoOnlineId = res;

                int resPreg = cursosBL.InsertarPreguntaEvaluacionCursoOnline(preguntaEvaluacion);

                foreach (var respuesta in respuestas)
                {
                    var respuestaEvaluacion = new RespuestaEvaluacionCursoOnline();
                    respuestaEvaluacion.OrdenMostrado = ordenMostrado;
                    respuestaEvaluacion.RespuestaCursoOnlineId = respuesta.RespuestaCursoOnlineId;
                    respuestaEvaluacion.PreguntaEvaluacionCursoOnlineId = resPreg;

                    int resRCO = cursosBL.InsertarRespuestaEvaluacionCursoOnline(respuestaEvaluacion);

                    preguntaEvaluacion.RespuestaEvaluacionCursoOnline.Add(respuestaEvaluacion);
                    ordenMostrado++;
                }

                evaluacion.PreguntaEvaluacionCursoOnline.Add(preguntaEvaluacion);

            }
            //await context.SaveChangesAsync();

            return evaluacion.EvaluacionCursoOnlineId;
            
        }


        //TODO NO ES USADO, VERIFICAR
        //public Boolean ExixtenPreguntasCursoOnline(Int32 cursoOnlineId)
        //{
        //    try
        //    {
        //        var cursoOnline = context.CursoOnline.Find(cursoOnlineId);
        //        var cantidadPreguntas = context.PreguntaCursoOnline.Where(x => x.CursoOnlineId == cursoOnline.CursoOnlineId && x.Estado == ConstantHelpers.ESTADO.ACTIVO).ToList().Count();
        //        return (cantidadPreguntas >= cursoOnline.NumPreguntasEvaluacion ? true : false);
        //    }
        //    catch (Exception)
        //    {
        //        return false;
        //        throw;
        //    }
        //}

        public Boolean ExixtenPreguntasCursoOnlineAsync(Int32 UnidadCursoOnlineId)
        {
            try
            {
                var cantidadPreguntas = cursosBL.ListarPreguntaCursoOnlinePorUnidadOnlineId(UnidadCursoOnlineId).
                    Where(x => x.Estado == ConstantHelpers.ESTADO.ACTIVO).Count();
                return (cantidadPreguntas > 0 ? true : false);
            }
            catch (Exception)
            {
                return false;
                throw;
            }
        }


    }
}