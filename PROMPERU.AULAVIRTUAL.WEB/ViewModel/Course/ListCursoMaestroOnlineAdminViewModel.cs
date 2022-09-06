using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using PROMPERU.AULAVIRTUAL.WEB.Controllers;
using PROMPERU.AULAVIRTUAL.WEB.Helpers;
using System.ComponentModel.DataAnnotations;
using PagedList;
using PROMPERU.AULAVIRTUAL.BE.CURSOS;
using PROMPERU.AULAVIRTUAL.BL;

namespace PROMPERU.AULAVIRTUAL.WEB.ViewModel.Course
{
    public class ListCursoMaestroOnlineAdminViewModel
    {
        CursosBL cursosBL = new CursosBL();
        public Int32 p { get; set; }
        public IPagedList<CursoOnlineMaestro> LstCursoOnline { get; set; }
        public List<DisponibilidadCursoOnline> LstDisponibilidadCursoOnline { get; set; }
        public Dictionary<Int32, Boolean> DictCursoDisponibilidad { get; set; }

        [Display(Name = "Nombre del curso")]
        public String NombreCurso { get; set; }

        public ListCursoMaestroOnlineAdminViewModel()
        {
        }

        public void CargarDatos(CargarDatosContext dataContext, Int32? p)
        {
            //this.p = p ?? 1;

            //LstDisponibilidadCursoOnline = cursosBL.ListarDisponibilidadCursoOnline().Where(x => x.FechaFin == null || x.FechaFin > DateTime.Now).ToList();    //dataContext.context.DisponibilidadCursoOnline.Where(x => x.FechaFin == null || x.FechaFin > DateTime.Now).ToList();

            //IQueryable<CursoOnlineMaestro> queryCursoOnline = cursosBL.ListarCursoOnlineMaestro().AsQueryable();
            ////queryCursoOnline = queryCursoOnline.OrderBy(x => x.Nombre);
            //queryCursoOnline = queryCursoOnline.OrderBy(x => x.Orden).Where(x => x.Estado != ConstantHelpers.ESTADO.ELIMINADO);//aqui se agrego que no muestre los eliminados

            //if (!String.IsNullOrEmpty(NombreCurso))
            //    queryCursoOnline = queryCursoOnline.Where(x => x.Nombre.Contains(NombreCurso));

            //LstCursoOnline = queryCursoOnline.ToPagedList(this.p, ConstantHelpers.DEFAULT_PAGE_SIZE);

            //DictCursoDisponibilidad = queryCursoOnline.ToDictionary(x => x.CursoOnlineMaestroId, x => LstDisponibilidadCursoOnline.Exists(l => l.CursoOnlineId == x.CursoOnlineMaestroId));
        }
    }
}
