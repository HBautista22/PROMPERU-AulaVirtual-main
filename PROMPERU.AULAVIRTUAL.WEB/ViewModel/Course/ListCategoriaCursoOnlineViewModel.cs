using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using PROMPERU.AULAVIRTUAL.BE.CURSOS;
using PROMPERU.AULAVIRTUAL.DA;
using PROMPERU.AULAVIRTUAL.WEB.Controllers;
using PROMPERU.AULAVIRTUAL.WEB.Helpers;
using PROMPERU.AULAVIRTUAL.BL;
using PagedList;
//using System.Data.Entity;


namespace PROMPERU.AULAVIRTUAL.WEB.ViewModel.Course
{
    public class ListCategoriaCursoOnlineViewModel
    {

        public Int32 p { get; set; }
        public IPagedList<CategoriaCursoOnline> LstCategoriaCursoOnline { get; set; }

        public ListCategoriaCursoOnlineViewModel()
        {
        }

        public void CargarDatos(CargarDatosContext dataContext, Int32? p)
        {
            //this.p = p ?? 1;
            ////IQueryable<CategoriaCursoOnline> queryCategoriaCursoOnline = dataContext.context.CategoriaCursoOnline.AsQueryable();
            //List<CategoriaCursoOnline> queryCategoriaCursoOnline = cur


            ////queryCategoriaCursoOnline = queryCategoriaCursoOnline.OrderBy(x => x.Nombre);
            //LstCategoriaCursoOnline = queryCategoriaCursoOnline.ToPagedList(this.p, ConstantHelpers.DEFAULT_PAGE_SIZE);
        }
    }
}
