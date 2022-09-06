using PROMPERU.AULAVIRTUAL.WEB.Controllers;
using PROMPERU.AULAVIRTUAL.WEB.Helpers;
using System;
using System.Collections.Generic;
using System.Linq;
using PROMPERU.AULAVIRTUAL.BE.CURSOS;
using PROMPERU.AULAVIRTUAL.BL;
using PROMPERU.AULAVIRTUAL.BE.USUARIO;


namespace PROMPERU.AULAVIRTUAL.WEB.ViewModel.Course
{
    public class ViewCursoOnlineEncuestaViewModel
    {
        CursosBL cursosBL = new CursosBL();
        UsuarioBL usuarioBL = new UsuarioBL();
        EmpresaBL empresaBL = new EmpresaBL();

    
        public Int32 CursoOnlineEncuestaId { get; set; }
        public String Nombre { get; set; }
        public String Descripcion { get; set; }
        public int CursoOnlineId { get; set; }
        public int CursoOnlineEncuestaRespuestaId { get; set; }
        public MatriculaCursoOnline MatriculaCursoOnline { get; set; }

        public List<DetalleCursoOnlineEncuesta> DetalleCursoOnlineEncuesta { get; set; }
        public CursoOnline CursoOnline { get; set; }
        public int Calificacion { get; set; }


        public Dictionary<string, string> preguntas { get; set; }
        public Dictionary<string, string> respuestas { get; set; }
        public int MatriculaCursoOnlineId { get;  set; }

        public void CargarDatos(CargarDatosContext dataContext, Int32 cursoOnlineId,Int32 matriculaCursoOnlineId)
        {

            this.MatriculaCursoOnlineId = matriculaCursoOnlineId;
            CursoOnlineEncuesta cursoOnlineEncuesta = cursosBL.ListarCursoOnlineEncuesta(cursoOnlineId);
            this.Nombre = cursoOnlineEncuesta.Nombre;
            this.Descripcion = cursoOnlineEncuesta.Descripcion;


            List<DetalleCursoOnlineEncuesta> detalleCursoOnlineEncuesta = cursosBL.ListarCursoOnlineEncuestaDetalle(cursoOnlineEncuesta.CursoOnlineEncuestaId);
            this.DetalleCursoOnlineEncuesta = detalleCursoOnlineEncuesta;

           
           
            Int32 usuarioId = dataContext.Session.GetUsuarioId();


            
            var empresaId = dataContext.Session.GetEmpresaId();

            



            CursoOnline cursoOnline = cursosBL.ObtenerCursoOnlinePorId(cursoOnlineId);
            this.CursoOnline = cursoOnline;


            CursoOnlineEncuestaRespuesta oCursoOnlineEncuestaRespuesta = cursosBL.ObtenerCursoOnlineEncuestaRespuesta(MatriculaCursoOnlineId);
            this.CursoOnlineEncuestaRespuestaId = oCursoOnlineEncuestaRespuesta.CursoOnlineEncuestaRespuestaId;

            


        }

    }
}