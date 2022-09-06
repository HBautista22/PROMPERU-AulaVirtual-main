using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using PROMPERU.AULAVIRTUAL.WEB.Models;
using PROMPERU.AULAVIRTUAL.WEB.Controllers;
using PROMPERU.AULAVIRTUAL.WEB.Helpers;
//using System.Data.Entity;
//using PagedList;

//************** BEGIN CONTROLLER *******************//
/*
	public ActionResult ListUnidadCursoOnline(Int32? p)
	{
		var ListUnidadCursoOnlineViewModel = new ListUnidadCursoOnlineViewModel();
		ListUnidadCursoOnlineViewModel.CargarDatos(CargarDatosContext,p);
		return View(ListUnidadCursoOnlineViewModel);
	}
*/
//************** END CONTROLLER *******************//
namespace PROMPERU.AULAVIRTUAL.WEB.ViewModel.Course
{
    public class ListUnidadCursoOnlineViewModel
    {
        //public Int32 p { get; set; }
        //public IPagedList<UnidadCursoOnline> LstUnidadCursoOnline { get; set; }

        //public ListUnidadCursoOnlineViewModel()
        //{
        //}

        //public void CargarDatos(CargarDatosContext dataContext, Int32? p)
        //{
        //    this.p = p ?? 1;
        //    IQueryable<UnidadCursoOnline> queryUnidadCursoOnline = dataContext.context.UnidadCursoOnline.AsQueryable();
        //    queryUnidadCursoOnline = queryUnidadCursoOnline.OrderBy(x => x.UnidadCursoOnlineId);
        //    LstUnidadCursoOnline = queryUnidadCursoOnline.ToPagedList(this.p, ConstantHelpers.DEFAULT_PAGE_SIZE);
        //}
    }
}
