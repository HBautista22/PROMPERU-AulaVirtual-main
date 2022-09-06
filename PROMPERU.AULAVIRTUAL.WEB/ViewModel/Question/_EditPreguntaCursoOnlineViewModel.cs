using PROMPERU.AULAVIRTUAL.WEB.Controllers;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
//using System.Data.Entity;
using PROMPERU.AULAVIRTUAL.WEB.Helpers;
using PROMPERU.AULAVIRTUAL.BE.CURSOS;
using System.ComponentModel.DataAnnotations;
using PROMPERU.AULAVIRTUAL.BL;


namespace PROMPERU.AULAVIRTUAL.WEB.ViewModel.Question
{
    public class _EditPreguntaCursoOnlineViewModel
    {

        CursosBL cursosBL = new CursosBL();

        public Int32 UnidadOnlineId { get; set; }

        public Int32? PreguntaCursoOnlineId { get; set; }
        public List<RespuestaCursoOnline> LstRespuestaCursoOnline { get; set; }

        public String Tipo { get; set; }
        public Int32 NumRespuestas { get; set; }
        public String Texto { get; set; }
        public Int32 Puntaje { get; set; }
        public Boolean Estado { get; set; }

        public _EditPreguntaCursoOnlineViewModel()
        {
            //LstRespuestaCursoOnline = new List<RespuestaCursoOnline>();
        }

        public void CargarDatos(CargarDatosContext dataContext, Int32 unidadOnlineId, Int32? preguntaCursoOnlineId)
        {
            this.UnidadOnlineId = unidadOnlineId;
            this.PreguntaCursoOnlineId = preguntaCursoOnlineId;
            if (preguntaCursoOnlineId.HasValue)
            {
                var preguntaCursoOnline = cursosBL.ListarPreguntaCursoOnlinePorUnidadOnlineId(unidadOnlineId).
                                          Where(x => x.PreguntaCursoOnlineId == (int)preguntaCursoOnlineId).
                                          FirstOrDefault();  // new PreguntaCursoOnline();// dataContext.context.PreguntaCursoOnline.Find(preguntaCursoOnlineId);
                this.LstRespuestaCursoOnline = preguntaCursoOnline.RespuestaCursoOnline.ToList(); //dataContext.context.RespuestaCursoOnline.Where(x => x.PreguntaCursoOnlineId == preguntaCursoOnlineId).ToList();
                this.UnidadOnlineId = preguntaCursoOnline.UnidadOnlineId;
                this.Tipo = preguntaCursoOnline.Tipo;
                this.NumRespuestas = preguntaCursoOnline.NumRespuestas;
                this.Texto = preguntaCursoOnline.Texto;
                this.Puntaje = preguntaCursoOnline.Puntaje;
                this.Estado = (preguntaCursoOnline.Estado == ConstantHelpers.ESTADO.ACTIVO);
            }
            else
            {
                this.LstRespuestaCursoOnline = new List<RespuestaCursoOnline>();
            }
        }
    }
}