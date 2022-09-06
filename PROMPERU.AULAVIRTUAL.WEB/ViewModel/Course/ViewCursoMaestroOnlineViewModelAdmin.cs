using PROMPERU.AULAVIRTUAL.BE.CURSOS;
using PROMPERU.AULAVIRTUAL.BL;
using PROMPERU.AULAVIRTUAL.WEB.Controllers;
using PROMPERU.AULAVIRTUAL.WEB.Helpers;
using PROMPERU.AULAVIRTUAL.WEB.Models;
using System;
using System.Collections.Generic;
//using System.Data.Entity;
using System.Linq;
using System.Web;

namespace PROMPERU.AULAVIRTUAL.WEB.ViewModel.Course
{
    public class ViewCursoMaestroOnlineViewModelAdmin
    {
        CursosBL cursosBL = new CursosBL();

        public Int32 CursoOnlineId { get; set; }
        public CursoOnlineMaestro CursoOnline { get; set; }

        public List<DetalleCursoOnlineMaestro> LstUnidadCursoOnline { get; set; }

        public Boolean EstaDisponible { get; set; }

        public ViewCursoMaestroOnlineViewModelAdmin()
        {
        }

        public void CargarDatos(CargarDatosContext dataContext, Int32 cursoOnlineId)
        {
            this.CursoOnlineId = cursoOnlineId;
            //this.EstaDisponible = dataContext.context.DisponibilidadCursoOnline.FirstOrDefault(x => x.CursoOnlineId == this.CursoOnlineId && x.FechaInicio <= DateTime.Now && (x.FechaFin == null || x.FechaFin >= DateTime.Now)) != null;
            DisponibilidadCursoOnline con = cursosBL.ObtenerDisponibilidadCursoOnlinePorId(this.CursoOnlineId);
            if(con.DisponibilidadCursoOnlineId!=0)
            {
                this.EstaDisponible = (con.FechaInicio <= DateTime.Now && (con.FechaFin == null || con.FechaFin >= DateTime.Now));
            }


            //CursoOnline = dataContext.context.CursoOnlineMaestro.FirstOrDefault(x => x.CursoOnlineMaestroId == cursoOnlineId);
            CursoOnline = cursosBL.ObtenerCursoOnlineMaestroPorId(cursoOnlineId); 

            //LstUnidadCursoOnline = dataContext.context.DetalleCursoOnlineMaestro.Where(x => x.CursoOnlineId == cursoOnlineId).OrderByDescending(x => x.Estado == ConstantHelpers.ESTADO.ACTIVO).ThenBy(x => x.Orden).ToList();
            LstUnidadCursoOnline =  cursosBL.ListarDetalleCursoOnlineMaestroPorCursoOnlineId(cursoOnlineId).OrderByDescending(x => x.Estado == ConstantHelpers.ESTADO.ACTIVO).ThenBy(x => x.Orden).ToList();
        }

    }
}