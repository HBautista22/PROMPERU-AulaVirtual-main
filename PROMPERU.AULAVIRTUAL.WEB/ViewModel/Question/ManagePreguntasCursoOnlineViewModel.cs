using PROMPERU.AULAVIRTUAL.WEB.Controllers;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
//using System.Data.Entity;
using PROMPERU.AULAVIRTUAL.WEB.Helpers;
using PROMPERU.AULAVIRTUAL.BE.CURSOS;
using PROMPERU.AULAVIRTUAL.BL;


namespace PROMPERU.AULAVIRTUAL.WEB.ViewModel.Question
{
    public class ManagePreguntasCursoOnlineViewModel
    {

        CursosBL cursosBL = new CursosBL();
        public Int32 UnidadCursoOnlineId { get; set; }
        public UnidadCursoOnline UnidadOnline { get; set; }
        public List<PreguntaCursoOnline> LstPreguntaCursoOnline { get; set; }



        public ManagePreguntasCursoOnlineViewModel()
        {
        }

        public void CargarDatos(CargarDatosContext dataContext, Int32 unidadCursoOnlineId)
        {
            this.UnidadCursoOnlineId = unidadCursoOnlineId;
            UnidadOnline = cursosBL.ObtenerUnidadCursoOnlinePorId(unidadCursoOnlineId);
            UnidadOnline.CursoOnline = cursosBL.ObtenerCursoOnlinePorId(UnidadOnline.CursoOnlineId);

            LstPreguntaCursoOnline = cursosBL.ListarPreguntaCursoOnlinePorUnidadCursoOnlineId(this.UnidadCursoOnlineId); // dataContext.context.PreguntaCursoOnline.Where(x => x.UnidadOnlineId == unidadCursoOnlineId).ToList();
        }
    }
}