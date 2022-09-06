using PROMPERU.AULAVIRTUAL.WEB.Helpers;
using PROMPERU.AULAVIRTUAL.BE.CURSOS;
using PROMPERU.AULAVIRTUAL.BL;
using System;
using System.Collections.Generic;
using System.Linq;

namespace PROMPERU.AULAVIRTUAL.WEB.Logic
{
    public class AvanceUnidadLogic
    {
        //public EvolEntities Context { get; set; }
        public CursosBL cursosBL = new CursosBL();
        public AvanceUnidadLogic()
        {
        }

        public string ControlAvanceUnidad(int MatriculaCursoOnlineId, int UnidadCursoOnlineId)
        {
            MatriculaCursoOnline MatriculaCursoOnline = cursosBL.ObtenerMatriculaCursoOnlinePorId(MatriculaCursoOnlineId);   // Context.MatriculaCursoOnline.Find(MatriculaCursoOnlineId);
            UnidadCursoOnline UnidadCursoOnline = cursosBL.ObtenerUnidadCursoOnlinePorId(UnidadCursoOnlineId); //  Context.UnidadCursoOnline.Find(UnidadCursoOnlineId);
            List<UnidadCursoOnline> lstUnidadCursoOnline = cursosBL.ListarUnidadCursoOnlinePorCursoOnlineId(MatriculaCursoOnline.CursoOnlineId);
            List<AvanceMatriculaCursoOnline> lstAvanceMatriculaOnline = cursosBL.ListarAvanceMatriculaCursoOnlinePorMatriculaCursoOnlineId(MatriculaCursoOnlineId);

            List<UnidadCursoOnline> lstUnidadCursoOnline1 = cursosBL.ListarUnidadCursoOnlinePorCursoOnlineId(UnidadCursoOnline.CursoOnlineId).Where
                                       (x => x.CursoOnlineId == UnidadCursoOnline.CursoOnlineId
                                           && x.Estado == ConstantHelpers.ESTADO.ACTIVO
                                           && x.TipoUnidadId == ConstantHelpers.TIPO_UNIDAD.DB_TEMA).
                                       OrderBy(x => x.Orden).ToList();

            bool lstExamenCursoOnline = cursosBL
                .ListarPreguntaCursoOnlinePorCursoOnlineId(MatriculaCursoOnline.CursoOnlineId)
                .Select(x => x.UnidadOnlineId)
                .Any();

            int totalUnidadesCursoOnline = lstUnidadCursoOnline1.Count();

            if (lstExamenCursoOnline)
            {
                totalUnidadesCursoOnline -= 1;
            }

            List<AvanceMatriculaCursoOnline> lstUnidadCursoOnlineIdActiva = cursosBL.ListarAvanceMatriculaCursoOnlinePorMatriculaCursoOnlineId(MatriculaCursoOnline.MatriculaCursoOnlineId);

            Dictionary<UnidadCursoOnline, string> DictUnidadCursoOnline = lstUnidadCursoOnline1.ToDictionary(x => x,
                    x => (lstUnidadCursoOnlineIdActiva.FirstOrDefault(a => a.UnidadCursoOnlineId == x.UnidadCursoOnlineId) == null ? ConstantHelpers.ESTADO_UNIDAD.INACTIVO :
                        (lstUnidadCursoOnlineIdActiva.FirstOrDefault(a => a.UnidadCursoOnlineId == x.UnidadCursoOnlineId && a.FechaFin.HasValue) != null ? ConstantHelpers.ESTADO_UNIDAD.COMPLETADO :
                        ConstantHelpers.ESTADO_UNIDAD.PENDIENTE)));

            int totalAvanceMatriculaCursoOnline = DictUnidadCursoOnline.Count(x => x.Value.Contains("FIN"));

            decimal porcentajeAvance = ((totalAvanceMatriculaCursoOnline * 1.0) / (totalUnidadesCursoOnline * 1.0) * 100).ToDecimal();

            MatriculaCursoOnline.PorcentajeAvance = porcentajeAvance;

            if (porcentajeAvance == 100 && MatriculaCursoOnline.FechaCompletado == null)
            {
                MatriculaCursoOnline.FechaCompletado = DateTime.Now;
                MatriculaCursoOnline.Estado = ConstantHelpers.ESTADO_MATRICULA.COMPLETADO;
            }

            int resMat = cursosBL.ActualizarMatriculaCursoOnline(MatriculaCursoOnline);





            if (UnidadCursoOnline.Orden > 1)
            {
                //var UnidadCursoOnlineAnterior = Context.UnidadCursoOnline.Where(x => x.Estado == ConstantHelpers.ESTADO.ACTIVO && x.CursoOnlineId == MatriculaCursoOnline.CursoOnlineId && x.TipoUnidadId == ConstantHelpers.TIPO_UNIDAD.DB_TEMA && x.UnidadCursoOnlineId != UnidadCursoOnlineId).OrderBy(x => x.Orden).FirstOrDefault();
                UnidadCursoOnline UnidadCursoOnlineAnterior = lstUnidadCursoOnline.Where
                                                (x => x.Estado == ConstantHelpers.ESTADO.ACTIVO 
                                                && x.CursoOnlineId == MatriculaCursoOnline.CursoOnlineId 
                                                && x.TipoUnidadId == ConstantHelpers.TIPO_UNIDAD.DB_TEMA 
                                                && x.UnidadCursoOnlineId != UnidadCursoOnlineId).
                                                OrderBy(x => x.Orden)
                                                .FirstOrDefault();

                //var AvanceMatriculaCursoOnlineAnterior = Context.AvanceMatriculaCursoOnline.Where(x => x.MatriculaCursoOnlineId == MatriculaCursoOnlineId && x.UnidadCursoOnlineId == UnidadCursoOnlineAnterior.UnidadCursoOnlineId).FirstOrDefault();
                AvanceMatriculaCursoOnline AvanceMatriculaCursoOnlineAnterior = lstAvanceMatriculaOnline.Where
                                                        (x => x.MatriculaCursoOnlineId == MatriculaCursoOnlineId 
                                                        && x.UnidadCursoOnlineId == UnidadCursoOnlineAnterior.UnidadCursoOnlineId).
                                                        FirstOrDefault();

                if (AvanceMatriculaCursoOnlineAnterior == null || AvanceMatriculaCursoOnlineAnterior.FechaFin == null)
                {
                    return ConstantHelpers.ESTADO_UNIDAD.INACTIVO;
                }
            }

            
            //var AvanceMatriculaCursoOnline = Context.AvanceMatriculaCursoOnline.Where(x => x.MatriculaCursoOnlineId == MatriculaCursoOnlineId && x.UnidadCursoOnlineId == UnidadCursoOnline.UnidadCursoOnlineId).FirstOrDefault();
            AvanceMatriculaCursoOnline AvanceMatriculaCursoOnline = lstAvanceMatriculaOnline.Where
                                            (x => x.MatriculaCursoOnlineId == MatriculaCursoOnlineId 
                                            && x.UnidadCursoOnlineId == UnidadCursoOnline.UnidadCursoOnlineId).
                                            FirstOrDefault();

            if (AvanceMatriculaCursoOnline == null)
            {
                AvanceMatriculaCursoOnline NuevoAvanceMatriculaCursoOnline = new AvanceMatriculaCursoOnline();
                NuevoAvanceMatriculaCursoOnline.FechaInicio = DateTime.Now;
                NuevoAvanceMatriculaCursoOnline.MatriculaCursoOnlineId = MatriculaCursoOnline.MatriculaCursoOnlineId;
                NuevoAvanceMatriculaCursoOnline.UnidadCursoOnlineId = UnidadCursoOnline.UnidadCursoOnlineId;

                cursosBL.InsertarAvanceMatriculaCursoOnline(NuevoAvanceMatriculaCursoOnline);
                //Context.AvanceMatriculaCursoOnline.Add(NuevoAvanceMatriculaCursoOnline);
                //Context.SaveChanges();

                return ConstantHelpers.ESTADO_UNIDAD.PENDIENTE;
            }
            else if (AvanceMatriculaCursoOnline.FechaFin != null)
            {
                //return ConstantHelpers.ESTADO_MATRICULA.COMPLETADO;
                return ConstantHelpers.ESTADO_UNIDAD.COMPLETADO;
            }
            else if (AvanceMatriculaCursoOnline.FechaFin == null)
            {
                TimeSpan TiempoEstadiaUnidad = DateTime.Now - AvanceMatriculaCursoOnline.FechaInicio.AddMinutes(ConstantHelpers.TIEMPO_ESTADIA_UNIDAD);
                
                AvanceMatriculaCursoOnline.FechaFin = DateTime.Now;

                if (TiempoEstadiaUnidad.Seconds.ToInteger() < 0)
                {
                    //return ConstantHelpers.ESTADO_MATRICULA.PENDIENTE;
                    return ConstantHelpers.ESTADO_UNIDAD.PENDIENTE;
                }

                cursosBL.ActualizarAvanceMatriculaCursoOnline(AvanceMatriculaCursoOnline);
            }

            //Context.SaveChanges();


            


            //return ConstantHelpers.ESTADO_MATRICULA.COMPLETADO;
            return ConstantHelpers.ESTADO_UNIDAD.COMPLETADO;
        }

        public string UpdateAvanceCursoOnline(int MatriculaCursoOnlineId, int UnidadCursoOnlineId)
        {

            MatriculaCursoOnline matriculaCursoOnline = cursosBL.ObtenerMatriculaCursoOnlinePorId(MatriculaCursoOnlineId);   //Context.MatriculaCursoOnline.Find(MatriculaCursoOnlineId);
            UnidadCursoOnline unidadCursoOnline = cursosBL.ObtenerUnidadCursoOnlinePorId(UnidadCursoOnlineId);    //Context.UnidadCursoOnline.Find(UnidadCursoOnlineId);

            

            List<AvanceMatriculaCursoOnline> lstAvanceMatriculaOnline = cursosBL.ListarAvanceMatriculaCursoOnlinePorMatriculaCursoOnlineId(MatriculaCursoOnlineId);

            //var avanceMatriculaCursoOnline = Context.AvanceMatriculaCursoOnline.Where
            //                                (x => x.MatriculaCursoOnlineId == MatriculaCursoOnlineId
            //                                && x.UnidadCursoOnlineId == unidadCursoOnline.UnidadCursoOnlineId)
            //                                .FirstOrDefault();

            AvanceMatriculaCursoOnline avanceMatriculaCursoOnline = lstAvanceMatriculaOnline
                .FirstOrDefault(x => x.MatriculaCursoOnlineId == MatriculaCursoOnlineId 
                                     && x.UnidadCursoOnlineId == unidadCursoOnline.UnidadCursoOnlineId);

            if (avanceMatriculaCursoOnline.FechaFin == null)
            {
                avanceMatriculaCursoOnline.FechaFin = DateTime.Now;
                cursosBL.ActualizarAvanceMatriculaCursoOnline(avanceMatriculaCursoOnline);
            }

            //Context.SaveChanges();

            //var totalUnidadesCursoOnline = Context.UnidadCursoOnline.Where(x => x.CursoOnlineId == unidadCursoOnline.CursoOnlineId && x.Estado == ConstantHelpers.ESTADO_UNIDAD.ACTIVADO && x.TipoUnidadId == ConstantHelpers.TIPO_UNIDAD.DB_TEMA).Count();

            List<UnidadCursoOnline> lstUnidadCursoOnline = cursosBL.ListarUnidadCursoOnlinePorCursoOnlineId(unidadCursoOnline.CursoOnlineId).Where
                                        (x => x.CursoOnlineId == unidadCursoOnline.CursoOnlineId
                                            && x.Estado == ConstantHelpers.ESTADO.ACTIVO
                                            && x.TipoUnidadId == ConstantHelpers.TIPO_UNIDAD.DB_TEMA).
                                        OrderBy(x => x.Orden).ToList();

            int totalUnidadesCursoOnline = lstUnidadCursoOnline.Count();

            //desde aqui el cambio

            List<AvanceMatriculaCursoOnline> lstUnidadCursoOnlineIdActiva = cursosBL.ListarAvanceMatriculaCursoOnlinePorMatriculaCursoOnlineId(MatriculaCursoOnlineId);

            

            Dictionary<UnidadCursoOnline, string> DictUnidadCursoOnline = lstUnidadCursoOnline.ToDictionary(x => x,
                    x => (lstUnidadCursoOnlineIdActiva.FirstOrDefault(a => a.UnidadCursoOnlineId == x.UnidadCursoOnlineId) == null ? ConstantHelpers.ESTADO_UNIDAD.INACTIVO :
                        (lstUnidadCursoOnlineIdActiva.FirstOrDefault(a => a.UnidadCursoOnlineId == x.UnidadCursoOnlineId && a.FechaFin.HasValue) != null ? ConstantHelpers.ESTADO_UNIDAD.COMPLETADO :
                        ConstantHelpers.ESTADO_UNIDAD.PENDIENTE)));

            int totalAvanceMatriculaCursoOnline = DictUnidadCursoOnline.Count(x => x.Value.Contains("FIN"));





            //var totalUnidadesCursoOnline =  cursosBL.ListarUnidadCursoOnlinePorCursoOnlineId(unidadCursoOnline.CursoOnlineId).Where
            //                                (x => x.CursoOnlineId == unidadCursoOnline.CursoOnlineId 
            //                                && x.Estado == ConstantHelpers.ESTADO_UNIDAD.ACTIVADO 
            //                                && x.TipoUnidadId == ConstantHelpers.TIPO_UNIDAD.DB_TEMA).
            //                                Count();


            //var totalAvanceMatriculaCursoOnline = Context.AvanceMatriculaCursoOnline.Where(x => x.MatriculaCursoOnlineId == MatriculaCursoOnlineId && x.FechaFin != null && x.UnidadCursoOnline.Estado == ConstantHelpers.ESTADO_UNIDAD.ACTIVADO).Count();
            //var totalAvanceMatriculaCursoOnline = cursosBL.ListarAvanceMatriculaCursoOnlinePorMatriculaCursoOnlineId(MatriculaCursoOnlineId).Where
            //                                    (x => x.MatriculaCursoOnlineId == MatriculaCursoOnlineId 
            //                                    && x.FechaFin != null 
            //                                    && x.UnidadCursoOnline.Estado == ConstantHelpers.ESTADO_UNIDAD.ACTIVADO).
            //                                    Count();

            decimal porcentajeAvance = ((totalAvanceMatriculaCursoOnline * 1.0) / (totalUnidadesCursoOnline * 1.0) * 100).ToDecimal();






            matriculaCursoOnline.PorcentajeAvance = porcentajeAvance;

            if (porcentajeAvance == 100 && matriculaCursoOnline.FechaCompletado == null)
            {
                matriculaCursoOnline.FechaCompletado = DateTime.Now;
                matriculaCursoOnline.Estado = ConstantHelpers.ESTADO_MATRICULA.COMPLETADO;
            }


            cursosBL.ActualizarMatriculaCursoOnline(matriculaCursoOnline);
            //Context.SaveChanges();

            return ConstantHelpers.ESTADO_MATRICULA.COMPLETADO;
        }
    }

}
