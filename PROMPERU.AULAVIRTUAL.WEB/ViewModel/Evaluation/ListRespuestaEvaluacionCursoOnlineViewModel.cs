using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using Rimac.EVOL.Models;
using Rimac.EVOL.Controllers;
using Rimac.EVOL.Helpers;
using System.Data.Entity;
using PagedList;

//************** BEGIN CONTROLLER *******************//
/*
	public ActionResult ListRespuestaEvaluacionCursoOnline(Int32? p)
	{
		var ListRespuestaEvaluacionCursoOnlineViewModel = new ListRespuestaEvaluacionCursoOnlineViewModel();
		ListRespuestaEvaluacionCursoOnlineViewModel.CargarDatos(CargarDatosContext(),p);
		return View(ListRespuestaEvaluacionCursoOnlineViewModel);
	}
*/
//************** END CONTROLLER *******************//
namespace Rimac.EVOL.ViewModel.Evaluation
{
    public class ListRespuestaEvaluacionCursoOnlineViewModel
    {
        public Int32 p { get; set; }
        public IPagedList<RespuestaEvaluacionCursoOnline> LstRespuestaEvaluacionCursoOnline { get; set; }

        public ListRespuestaEvaluacionCursoOnlineViewModel()
        {
        }

        public void CargarDatos(CargarDatosContext dataContext, Int32? p)
        {
            this.p = p ?? 1;
            IQueryable<RespuestaEvaluacionCursoOnline> queryRespuestaEvaluacionCursoOnline = dataContext.context.RespuestaEvaluacionCursoOnline.AsQueryable();
            queryRespuestaEvaluacionCursoOnline = queryRespuestaEvaluacionCursoOnline.OrderBy(x => x.RespuestaEvaluacionCursoOnlineId);
            LstRespuestaEvaluacionCursoOnline = queryRespuestaEvaluacionCursoOnline.ToPagedList(this.p, ConstantHelpers.DEFAULT_PAGE_SIZE);
        }
    }
}
