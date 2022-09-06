using PROMPERU.AULAVIRTUAL.WEB.Controllers;
using PROMPERU.AULAVIRTUAL.BE.CURSOS;
using PROMPERU.AULAVIRTUAL.BE.USUARIO;
using PROMPERU.AULAVIRTUAL.BL;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace PROMPERU.AULAVIRTUAL.WEB.ViewModel.Evaluation
{
    public class ViewHistorialEvaluacionAlumnoViewModel
    {
        CursosBL cursosBL = new CursosBL();
        UsuarioBL usuarioBL = new UsuarioBL();

        public Int32 UsuarioId { get; set; }
        public Int32 MatriculaCursoOnlineId { get; set; }

        public Int32 CantidadPreguntasEvaluaciones { get; set; }

        public Usuario Usuario { get; set; }
        public CursoOnline CursoOnline { get; set; }

        public MatriculaCursoOnline MatriculaCursoOnline { get; set; }
        public List<EvaluacionCursoOnline> LstEvaluacionCursoOnline { get; set; }

        public ViewHistorialEvaluacionAlumnoViewModel()
        {

        }

        public void CargarDatos(Int32 matriculaCursoOnlineId, Int32 usuarioId)
        {
            this.UsuarioId = usuarioId;
            this.MatriculaCursoOnlineId = matriculaCursoOnlineId;
            MatriculaCursoOnline = cursosBL.ObtenerMatriculaCursoOnlinePorId(matriculaCursoOnlineId); //  new MatriculaCursoOnline();// dataContext.context.MatriculaCursoOnline.Find(MatriculaCursoOnlineId);
            CursoOnline = cursosBL.ObtenerCursoOnlinePorId(MatriculaCursoOnline.CursoOnlineId);
            this.CursoOnline.UnidadCursoOnline = cursosBL.ListarUnidadCursoOnlinePorCursoOnlineId(CursoOnline.CursoOnlineId);

            foreach (UnidadCursoOnline unidad in this.CursoOnline.UnidadCursoOnline)
            {
                List<PreguntaCursoOnline> lstPreguntaCursoOnline = cursosBL.ListarPreguntaCursoOnlinePorUnidadCursoOnlineId(unidad.UnidadCursoOnlineId);
                unidad.PreguntaCursoOnline = lstPreguntaCursoOnline;
            }


            //var Preguntasvalidas = cursosBL.ListarPreguntaCursoOnlinePorUnidadCursoOnlineId(MatriculaCursoOnline.CursoOnlineId).Count();   //(CursoOnline.UnidadCursoOnline.Select(a => a.PreguntaCursoOnline.Select(l => l.PreguntaCursoOnlineId).Count()); new List<UnidadCursoOnline>(); //CursoOnline.UnidadCursoOnline.Select(a => a.PreguntaCursoOnline.Select(l => l.PreguntaCursoOnlineId).Count());
            //var Preguntasvalidas = CursoOnline.UnidadCursoOnline.Select(a => a.PreguntaCursoOnline.Select(l => l.PreguntaCursoOnlineId).Count());
            //new CursoOnline(); // dataContext.context.CursoOnline.Find(MatriculaCursoOnline.CursoOnlineId);
            //var Preguntasvalidas = cursosBL.ListarPreguntaCursoOnlinePorUnidadCursoOnlineId(CursoOnline.UnidadCursoOnline.Select(a => a.PreguntaCursoOnline.Select(l => l.PreguntaCursoOnlineId).Count()); new List<UnidadCursoOnline>(); 
            //CursoOnline.UnidadCursoOnline.Select(a => a.PreguntaCursoOnline.Select(l => l.PreguntaCursoOnlineId).Count());
            var Preguntasvalidas = CursoOnline.UnidadCursoOnline.Select(a => a.PreguntaCursoOnline.Select(l => l.PreguntaCursoOnlineId).Count());
            Usuario = usuarioBL.ObtenerUsuarioPorId(usuarioId); //dataContext.context.Usuario.Find(usuarioId);
            this.CantidadPreguntasEvaluaciones = Preguntasvalidas.Sum();
            //this.CantidadPreguntasEvaluaciones = 34;//Preguntasvalidas;

            LstEvaluacionCursoOnline = cursosBL.ListarEvaluacionCursoOnlinePorCursoOnlineId(CursoOnline.CursoOnlineId).
                Where(x => x.MatriculaCursoOnlineId == MatriculaCursoOnlineId).ToList();   ; //  new List<EvaluacionCursoOnline>(); //dataContext.context.EvaluacionCursoOnline.Where(x => x.MatriculaCursoOnlineId == MatriculaCursoOnlineId).ToList();
        }
    }
}