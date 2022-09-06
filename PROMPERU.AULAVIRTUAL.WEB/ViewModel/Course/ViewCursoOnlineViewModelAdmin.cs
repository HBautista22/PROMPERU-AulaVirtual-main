using PROMPERU.AULAVIRTUAL.WEB.Controllers;
using PROMPERU.AULAVIRTUAL.WEB.Helpers;
using PROMPERU.AULAVIRTUAL.WEB.Models;
using System;
using System.Collections.Generic;
//using System.Data.Entity;
using System.Linq;
using System.Web;
using PROMPERU.AULAVIRTUAL.BE.CURSOS;

namespace PROMPERU.AULAVIRTUAL.WEB.ViewModel.Course
{
    public class ViewCursoOnlineViewModelAdmin
    {
        public Int32 CursoOnlineId { get; set; }
        public CursoOnline CursoOnline { get; set; }

        public List<UnidadCursoOnline> LstUnidadCursoOnline { get; set; }

        public Boolean EstaDisponible { get; set; }

        public ViewCursoOnlineViewModelAdmin()
        {
        }

        public void CargarDatos(CargarDatosContext dataContext, Int32 cursoOnlineId)
        {
            //this.CursoOnlineId = cursoOnlineId;
            //this.EstaDisponible = dataContext.context.DisponibilidadCursoOnline.FirstOrDefault(x => x.CursoOnlineId == this.CursoOnlineId && x.FechaInicio <= DateTime.Now && (x.FechaFin == null || x.FechaFin >= DateTime.Now)) != null;

            //CursoOnline = dataContext.context.CursoOnline.Include(x => x.CategoriaCursoOnline).FirstOrDefault(x => x.CursoOnlineId == cursoOnlineId);
            //LstUnidadCursoOnline = dataContext.context.UnidadCursoOnline.Include(x => x.UnidadCursoOnline1).Where(x => x.CursoOnlineId == cursoOnlineId && x.UnidadCursoOnlinePadreId == null).OrderByDescending(x => x.Estado == ConstantHelpers.ESTADO.ACTIVO).ThenBy(x => x.Orden).ToList();
        }

    }
}