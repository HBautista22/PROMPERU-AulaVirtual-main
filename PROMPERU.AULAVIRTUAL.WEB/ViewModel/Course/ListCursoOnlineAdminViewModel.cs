using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using PROMPERU.AULAVIRTUAL.WEB.Models;
using PROMPERU.AULAVIRTUAL.WEB.Controllers;
using PROMPERU.AULAVIRTUAL.WEB.Helpers;
using PROMPERU.AULAVIRTUAL.BE.CURSOS;
//using System.Data.Entity;
using PagedList;
using System.ComponentModel.DataAnnotations;

namespace PROMPERU.AULAVIRTUAL.WEB.ViewModel.Course
{
    public class ListCursoOnlineAdminViewModel
    {
        public Int32 p { get; set; }
        public IPagedList<CursoOnline> LstCursoOnline { get; set; }
        public List<DisponibilidadCursoOnline> LstDisponibilidadCursoOnline { get; set; }
        public Dictionary<Int32, Boolean> DictCursoDisponibilidad { get; set; }

        [Display(Name = "Nombre del curso")]
        public String NombreCurso { get; set; }

        public ListCursoOnlineAdminViewModel()
        {
        }

        public void CargarDatos(CargarDatosContext dataContext, Int32? p)
        {
            //this.p = p ?? 1;

            //LstDisponibilidadCursoOnline = dataContext.context.DisponibilidadCursoOnline.Where(x => x.FechaFin == null || x.FechaFin > DateTime.Now).ToList();

            //IQueryable<CursoOnline> queryCursoOnline = dataContext.context.CursoOnline.Include(x => x.CategoriaCursoOnline).AsQueryable();
            ////queryCursoOnline = queryCursoOnline.OrderBy(x => x.Nombre);
            //queryCursoOnline = queryCursoOnline.OrderBy(x => x.Orden).Where(x => x.Estado != ConstantHelpers.ESTADO.ELIMINADO);//aqui se agrego que no muestre los eliminados

            //if (!String.IsNullOrEmpty(NombreCurso))
            //    queryCursoOnline = queryCursoOnline.Where(x => x.Nombre.Contains(NombreCurso));

            //LstCursoOnline = queryCursoOnline.ToPagedList(this.p, ConstantHelpers.DEFAULT_PAGE_SIZE);

            //DictCursoDisponibilidad = queryCursoOnline.ToDictionary(x => x.CursoOnlineId, x => LstDisponibilidadCursoOnline.Exists(l => l.CursoOnlineId == x.CursoOnlineId));
        }
    }
}
