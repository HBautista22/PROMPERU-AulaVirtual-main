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
	public ActionResult ListRecursoCursoOnline(Int32? p)
	{
		var ListRecursoCursoOnlineViewModel = new ListRecursoCursoOnlineViewModel();
		ListRecursoCursoOnlineViewModel.CargarDatos(CargarDatosContext,p);
		return View(ListRecursoCursoOnlineViewModel);
	}
*/
//************** END CONTROLLER *******************//
namespace PROMPERU.AULAVIRTUAL.WEB.ViewModel.Course
{
    public class ListRecursoCursoOnlineViewModel
    {
        //    public Int32 p { get; set; }
        //    public IPagedList<RecursoCursoOnline> LstRecursoCursoOnline { get; set; }

        //    public ListRecursoCursoOnlineViewModel()
        //    {
        //    }

        //    public void CargarDatos(CargarDatosContext dataContext, Int32? p)
        //    {
        //        this.p = p ?? 1;
        //        IQueryable<RecursoCursoOnline> queryRecursoCursoOnline = dataContext.context.RecursoCursoOnline.AsQueryable();
        //        queryRecursoCursoOnline = queryRecursoCursoOnline.OrderBy(x => x.RecursoCursoOnlineId);
        //        LstRecursoCursoOnline = queryRecursoCursoOnline.ToPagedList(this.p, ConstantHelpers.DEFAULT_PAGE_SIZE);
        //    }
    }
}
