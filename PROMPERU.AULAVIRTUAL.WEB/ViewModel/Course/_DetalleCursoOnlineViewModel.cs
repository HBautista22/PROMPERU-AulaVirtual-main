using PROMPERU.AULAVIRTUAL.WEB.Controllers;
using PROMPERU.AULAVIRTUAL.BE.CURSOS;
using PROMPERU.AULAVIRTUAL.WEB.Helpers;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using PROMPERU.AULAVIRTUAL.BL;


namespace PROMPERU.AULAVIRTUAL.WEB.ViewModel.Course
{
    public class _DetalleCursoOnlineViewModel
    {
        //public Int32 MatriculaOnclineId { get; set; }
        //public Int32 CursoOnlineId { get; set; }

        CursosBL cursosBL = new CursosBL();


        public CursoOnline CursoOnline { get; set; }
        public MatriculaCursoOnline MatriculaCursoOnline { get; set; }
        public EvaluacionCursoOnline EvaluacionCursoOnline { get; set; }

        public _DetalleCursoOnlineViewModel()
        {
        }

        public void CargarDatos(CargarDatosContext dataContext, Int32 cursoOnlineId, Int32 matriculaCursoOnlineId)
        {
            CursoOnline = cursosBL.ObtenerCursoOnlinePorId(cursoOnlineId);
            CursoOnline.CategoriaCursoOnline = cursosBL.ObtenerCategoriaCursoOnline(CursoOnline.CategoriaCursoOnlineId);
            MatriculaCursoOnline = cursosBL.ObtenerMatriculaCursoOnlinePorId(matriculaCursoOnlineId);   //dataContext.context.MatriculaCursoOnline.Find(matriculaCursoOnlineId);
            EvaluacionCursoOnline = cursosBL.ListarEvaluacionCursoOnlinePorCursoOnlineId(cursoOnlineId).FirstOrDefault(x => x.MatriculaCursoOnlineId == matriculaCursoOnlineId); // dataContext.context.EvaluacionCursoOnline.Where(x => x.MatriculaCursoOnlineId == MatriculaCursoOnline.MatriculaCursoOnlineId && x.FechaFin == MatriculaCursoOnline.FechaAprobado).FirstOrDefault();
        }
    }
}