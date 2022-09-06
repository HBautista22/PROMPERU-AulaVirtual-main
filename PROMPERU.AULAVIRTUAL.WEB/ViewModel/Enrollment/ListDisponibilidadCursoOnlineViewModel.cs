using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using PROMPERU.AULAVIRTUAL.BE.CURSOS;
using PROMPERU.AULAVIRTUAL.WEB.Controllers;
using PROMPERU.AULAVIRTUAL.WEB.Helpers;
using PagedList;

namespace PROMPERU.AULAVIRTUAL.WEB.ViewModel.Enrollment
{
    public class ListDisponibilidadCursoOnlineViewModel
    {
        public Int32 p { get; set; }
        public IPagedList<DisponibilidadCursoOnline> LstDisponibilidadCursoOnline { get; set; }

        public ListDisponibilidadCursoOnlineViewModel()
        {
        }

        public void CargarDatos(Int32? p, Int32 usuarioId)
        {
            this.p = p ?? 1;
            //IQueryable<DisponibilidadCursoOnline> queryDisponibilidadCursoOnline = dataContext.context.DisponibilidadCursoOnline.AsQueryable();

            //var Usuario = dataContext.context.Usuario.Find(usuarioId);
            //if (Usuario.Rol == ConstantHelpers.ROL.ADMINISTRADOR)
            //{
            //    queryDisponibilidadCursoOnline = queryDisponibilidadCursoOnline.OrderBy(x => x.DisponibilidadCursoOnlineId);
            //}
            //else
            //{
            //    queryDisponibilidadCursoOnline = queryDisponibilidadCursoOnline.Where(x => x.FechaFin == null && x.FechaFin < DateTime.Now).OrderBy(x => x.DisponibilidadCursoOnlineId);
            //}

            //LstDisponibilidadCursoOnline = queryDisponibilidadCursoOnline.ToPagedList(this.p, ConstantHelpers.DEFAULT_PAGE_SIZE);
        }
    }
}
