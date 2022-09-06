using PROMPERU.AULAVIRTUAL.BE.CURSOS;
using PROMPERU.AULAVIRTUAL.BL;
using PROMPERU.AULAVIRTUAL.WEB.Controllers;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using PROMPERU.AULAVIRTUAL.WEB.Helpers;


//using System.Data.Entity;

namespace PROMPERU.AULAVIRTUAL.WEB.ViewModel.Evaluation
{
    public class ResultEvaluacionCursoOnlineViewModel
    {
        public Int32 EvaluacionCursoOnlineId { get; set; }
        public Int32 UnidadCursoOnlineId { get; set; }

        public EvaluacionCursoOnline EvaluacionCursoOnline { get; set; }
        public MatriculaCursoOnline MatriculaCursoOnline { get; set; }
        public CursoOnline CursoOnline { get; set; }
        public UnidadCursoOnline UnidadCursoOnline { get; set; }
        public Int32 NumeroIntentos { get; set; }
        public DateTime MyProperty { get; set; }

        public ResultEvaluacionCursoOnlineViewModel()
        {
            this.NumeroIntentos = 1;
        }

        public void CargarDatos(Int32 evaluacionCursoOnlineId)
        {
            CursosBL cursosBL = new CursosBL();

            this.EvaluacionCursoOnlineId = evaluacionCursoOnlineId;
            //dataContext.context.EvaluacionCursoOnline.Include(x => x.MatriculaCursoOnline).Where(x => x.EvaluacionCursoOnlineId == evaluacionCursoOnlineId).OrderByDescending(x => x.FechaFin).FirstOrDefault();
            EvaluacionCursoOnline = cursosBL.ObtenerEvaluacionCursoOnlinePorId(evaluacionCursoOnlineId);
            EvaluacionCursoOnline.MatriculaCursoOnline = cursosBL.ObtenerMatriculaCursoOnlinePorId(EvaluacionCursoOnline.MatriculaCursoOnlineId);
            EvaluacionCursoOnline.MatriculaCursoOnline.CursoOnline = cursosBL.ObtenerCursoOnlinePorId(EvaluacionCursoOnline.MatriculaCursoOnline.CursoOnlineId);
            //dataContext.context.UnidadCursoOnline.FirstOrDefault(x => x.UnidadCursoOnlineId == EvaluacionCursoOnline.UnidadCursoOnlineId);
            UnidadCursoOnline = cursosBL.ObtenerUnidadCursoOnlinePorId(EvaluacionCursoOnline.UnidadCursoOnlineId);
            this.UnidadCursoOnlineId = UnidadCursoOnline.UnidadCursoOnlineId;
            if (EvaluacionCursoOnline.Nota != null)
            {
                this.MatriculaCursoOnline = EvaluacionCursoOnline.MatriculaCursoOnline;
                this.CursoOnline = MatriculaCursoOnline.CursoOnline;

                //this.NumeroIntentos = dataContext.context.EvaluacionCursoOnline.Count(x => x.UnidadCursoOnlineId == EvaluacionCursoOnline.UnidadCursoOnlineId);
                this.NumeroIntentos = cursosBL.ContarEvaluacionCursoOnlinePorUnidadCursoOnlineId(EvaluacionCursoOnline.UnidadCursoOnlineId);

                if (CursoOnline.PorcentajeAprobacion <= EvaluacionCursoOnline.PuntajeObtenido / EvaluacionCursoOnline.PuntajeTotal)
                {
                    MatriculaCursoOnline.Nota = EvaluacionCursoOnline.PuntajeObtenido;
                    if (MatriculaCursoOnline.PorcentajeObtenido > EvaluacionCursoOnline.PorcentajeAprobacion)
                    {
                        MatriculaCursoOnline.FechaAprobado = EvaluacionCursoOnline.FechaFin;
                    }
                }

                //dataContext.context.SaveChanges();



                var lstUnidadCursoOnline = cursosBL.ListarUnidadCursoOnlinePorCursoOnlineId(this.CursoOnline.CursoOnlineId).Where
                                        (x => x.CursoOnlineId == this.CursoOnline.CursoOnlineId
                                            && x.Estado == ConstantHelpers.ESTADO.ACTIVO
                                            && x.TipoUnidadId == ConstantHelpers.TIPO_UNIDAD.DB_TEMA).
                                        OrderBy(x => x.Orden).ToList();
                var totalUnidadesCursoOnline = lstUnidadCursoOnline.Count();

                var lstUnidadCursoOnlineIdActiva = cursosBL.ListarAvanceMatriculaCursoOnlinePorMatriculaCursoOnlineId(MatriculaCursoOnline.MatriculaCursoOnlineId);

                var DictUnidadCursoOnline = lstUnidadCursoOnline.ToDictionary(x => x,
                        x => (lstUnidadCursoOnlineIdActiva.FirstOrDefault(a => a.UnidadCursoOnlineId == x.UnidadCursoOnlineId) == null ? ConstantHelpers.ESTADO_UNIDAD.INACTIVO :
                            (lstUnidadCursoOnlineIdActiva.FirstOrDefault(a => a.UnidadCursoOnlineId == x.UnidadCursoOnlineId && a.FechaFin.HasValue) != null ? ConstantHelpers.ESTADO_UNIDAD.COMPLETADO :
                            ConstantHelpers.ESTADO_UNIDAD.PENDIENTE)));

                var totalAvanceMatriculaCursoOnline = DictUnidadCursoOnline.Count(x => x.Value.Contains("FIN"));

                var porcentajeAvance = ((totalAvanceMatriculaCursoOnline * 1.0) / (totalUnidadesCursoOnline * 1.0) * 100).ToDecimal();

                MatriculaCursoOnline.PorcentajeAvance = porcentajeAvance;

                if (porcentajeAvance == 100 && MatriculaCursoOnline.FechaCompletado == null)
                {
                    MatriculaCursoOnline.FechaCompletado = DateTime.Now;
                    MatriculaCursoOnline.Estado = ConstantHelpers.ESTADO_MATRICULA.COMPLETADO;
                }





                int resMat = cursosBL.ActualizarMatriculaCursoOnline(MatriculaCursoOnline);
            }
        }
    }
}