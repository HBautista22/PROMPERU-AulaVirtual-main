using PROMPERU.AULAVIRTUAL.WEB.Controllers;
using PROMPERU.AULAVIRTUAL.WEB.Helpers;
using PROMPERU.AULAVIRTUAL.WEB.Models;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Web;
//using System.Data.Entity;
using PROMPERU.AULAVIRTUAL.WEB.Logic;
using PROMPERU.AULAVIRTUAL.BE.CURSOS;
using PROMPERU.AULAVIRTUAL.BL;

namespace PROMPERU.AULAVIRTUAL.WEB.ViewModel.Course
{
    public class ViewUnidadCursoOnlineViewModel
    {
        public CursosBL cursosBL = new CursosBL();
        public Int32 UnidadCursoOnlineId { get; set; }
        public Int32 CursoOnlineId { get; set; }
        public Int32 MatriculaCursoOnlineId { get; set; }
        public Int32 TiempoPermanencia { get; set; }
        public CursoOnline CursoOnline { get; set; }
        public UnidadCursoOnline UnidadCursoOnline { get; set; }
        public List<RecursoCursoOnline> LstRecurso { get; set; }
        public List<UnidadCursoOnline> LstTemas { get; set; }
        public List<VideoUnidadCursoOnline> LstVideos { get; set; }
        public String TipoUnidad { get; set; }

        public String MatriculaCursoOnlineIdEnc { get; set; }
        public String UnidadCursoOnlineIdEnc { get; set; }
        public String RutaArchivoAdicional { get; set; }

        
        public EvaluacionCursoOnline EvaluacionCursoOnline { get; set; }

        public AvanceMatriculaCursoOnline AvanceMatriculaCursoOnline { get; set; }

        public ViewUnidadCursoOnlineViewModel()
        {
        }

        public void CargarDatos(CargarDatosContext dataContext, Int32 unidadCursoOnlineId, Int32 cursoOnlineId, Int32 matriculaCursoOnlineId)
        {
            this.UnidadCursoOnlineId = unidadCursoOnlineId;
            this.CursoOnlineId = cursoOnlineId;
            this.MatriculaCursoOnlineId = matriculaCursoOnlineId;
            //modificado por OTI, se agrego que muestre solo con estado Activo
            this.UnidadCursoOnline = cursosBL.ObtenerUnidadCursoOnlinePorId(unidadCursoOnlineId);//dataContext.context.UnidadCursoOnline.Include(x => x.CursoOnline).Include(x => x.UnidadCursoOnline1).FirstOrDefault(x => x.UnidadCursoOnlineId == unidadCursoOnlineId);
            this.TipoUnidad = UnidadCursoOnline.TipoUnidad;
            this.CursoOnline = cursosBL.ObtenerCursoOnlinePorId(UnidadCursoOnline.CursoOnlineId);
            this.CursoOnline.CategoriaCursoOnline = cursosBL.ObtenerCategoriaCursoOnline(this.CursoOnline.CategoriaCursoOnlineId);
            this.UnidadCursoOnline.UnidadCursoOnline1 = cursosBL.ListarUnidadCursoOnlinePorCursoOnlineId(this.CursoOnlineId);

            


            /*****TODO> RecursoCursoOnline no tiene registros ni referencias *****/

            this.LstRecurso = new List<RecursoCursoOnline>(); //dataContext.context.RecursoCursoOnline.Where(x => x.UnidadCursoOnlineId == unidadCursoOnlineId).ToList();
            this.TiempoPermanencia = UnidadCursoOnline.TiempoPermanencia;
            this.RutaArchivoAdicional = UnidadCursoOnline.RutaArchivoAdicional;
            var esAdministrador = dataContext.Session.GetRol() == AppRol.Administrador;
            if (this.MatriculaCursoOnlineId != -1 && !esAdministrador)
            {
                //this.AvanceMatriculaCursoOnline = dataContext.context.AvanceMatriculaCursoOnline.FirstOrDefault(x => x.MatriculaCursoOnlineId == matriculaCursoOnlineId && x.UnidadCursoOnlineId == unidadCursoOnlineId);
                List<AvanceMatriculaCursoOnline> lstAvanceMatriculaCursoOnline = cursosBL.ListarAvanceMatriculaCursoOnlinePorMatriculaCursoOnlineId(matriculaCursoOnlineId);
                this.AvanceMatriculaCursoOnline = lstAvanceMatriculaCursoOnline.FirstOrDefault
                                                (x => x.MatriculaCursoOnlineId == matriculaCursoOnlineId && x.UnidadCursoOnlineId == unidadCursoOnlineId);
            }

            var encriptar = new Encriptacion();

            this.MatriculaCursoOnlineIdEnc = encriptar.Encriptar(MatriculaCursoOnlineId.ToString());
            this.UnidadCursoOnlineIdEnc = encriptar.Encriptar(UnidadCursoOnlineId.ToString());
            this.LstTemas = UnidadCursoOnline.UnidadCursoOnline1
                .Where(x => x.Estado == ConstantHelpers.ESTADO.ACTIVO && x.UnidadCursoOnlinePadreId == unidadCursoOnlineId)
                .ToList();

            this.LstVideos = cursosBL.ListarVideoUnidadCursoOnline(unidadCursoOnlineId);

            //this.LstVideos = dataContext.context.VideoUnidadCursoOnline.Where(x => x.UnidadCursoOnlineUnidadCursoOnlineId == unidadCursoOnlineId).ToList();
        }

    }
}