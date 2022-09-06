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
	public ActionResult ListRespuestaSeleccionadaEvaluacionCursoOnline(Int32? p)
	{
		var ListRespuestaSeleccionadaEvaluacionCursoOnlineViewModel = new ListRespuestaSeleccionadaEvaluacionCursoOnlineViewModel();
		ListRespuestaSeleccionadaEvaluacionCursoOnlineViewModel.CargarDatos(CargarDatosContext(),p);
		return View(ListRespuestaSeleccionadaEvaluacionCursoOnlineViewModel);
	}
*/
//************** END CONTROLLER *******************//
namespace Rimac.EVOL.ViewModel.Evaluation
{
    public class ListRespuestaSeleccionadaEvaluacionCursoOnlineViewModel
    {
        public Int32 p { get; set; }
        public IPagedList<RespuestaSeleccionadaEvaluacionCursoOnline> LstRespuestaSeleccionadaEvaluacionCursoOnline { get; set; }

        public ListRespuestaSeleccionadaEvaluacionCursoOnlineViewModel()
        {
        }

        public void CargarDatos(CargarDatosContext dataContext, Int32? p)
        {
            this.p = p ?? 1;
            IQueryable<RespuestaSeleccionadaEvaluacionCursoOnline> queryRespuestaSeleccionadaEvaluacionCursoOnline = dataContext.context.RespuestaSeleccionadaEvaluacionCursoOnline.AsQueryable();
            queryRespuestaSeleccionadaEvaluacionCursoOnline = queryRespuestaSeleccionadaEvaluacionCursoOnline.OrderBy(x => x.RespuestaSeleccionadaEvaluacionCursoOnlineId);
            LstRespuestaSeleccionadaEvaluacionCursoOnline = queryRespuestaSeleccionadaEvaluacionCursoOnline.ToPagedList(this.p, ConstantHelpers.DEFAULT_PAGE_SIZE);
        }
    }
}
