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
	public ActionResult ListEvaluacionCursoOnline(Int32? p)
	{
		var ListEvaluacionCursoOnlineViewModel = new ListEvaluacionCursoOnlineViewModel();
		ListEvaluacionCursoOnlineViewModel.CargarDatos(CargarDatosContext,p);
		return View(ListEvaluacionCursoOnlineViewModel);
	}
*/
//************** END CONTROLLER *******************//
namespace PROMPERU.AULAVIRTUAL.WEB.ViewModel.Evaluation
{
    public class ListEvaluacionCursoOnlineViewModel
    {
        public Int32 p { get; set; }
        //public IPagedList<EvaluacionCursoOnline> LstEvaluacionCursoOnline { get; set; }

        //public ListEvaluacionCursoOnlineViewModel()
        //{
        //}

        //public void CargarDatos(CargarDatosContext dataContext, Int32? p)
        //{
        //    this.p = p ?? 1;
        //    IQueryable<EvaluacionCursoOnline> queryEvaluacionCursoOnline = dataContext.context.EvaluacionCursoOnline.AsQueryable();
        //    queryEvaluacionCursoOnline = queryEvaluacionCursoOnline.OrderBy(x => x.EvaluacionCursoOnlineId);
        //    LstEvaluacionCursoOnline = queryEvaluacionCursoOnline.ToPagedList(this.p, ConstantHelpers.DEFAULT_PAGE_SIZE);
        //}
    }
}
