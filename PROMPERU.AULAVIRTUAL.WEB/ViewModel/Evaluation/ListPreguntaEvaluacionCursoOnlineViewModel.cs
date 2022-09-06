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
	public ActionResult ListPreguntaEvaluacionCursoOnline(Int32? p)
	{
		var ListPreguntaEvaluacionCursoOnlineViewModel = new ListPreguntaEvaluacionCursoOnlineViewModel();
		ListPreguntaEvaluacionCursoOnlineViewModel.CargarDatos(CargarDatosContext(),p);
		return View(ListPreguntaEvaluacionCursoOnlineViewModel);
	}
*/
//************** END CONTROLLER *******************//
namespace Rimac.EVOL.ViewModel.Evaluation
{
    public class ListPreguntaEvaluacionCursoOnlineViewModel
    {
        public Int32 p { get; set; }
        public IPagedList<PreguntaEvaluacionCursoOnline> LstPreguntaEvaluacionCursoOnline { get; set; }

        public ListPreguntaEvaluacionCursoOnlineViewModel()
        {
        }

        public void CargarDatos(CargarDatosContext dataContext, Int32? p)
        {
            this.p = p ?? 1;
            IQueryable<PreguntaEvaluacionCursoOnline> queryPreguntaEvaluacionCursoOnline = dataContext.context.PreguntaEvaluacionCursoOnline.AsQueryable();
            queryPreguntaEvaluacionCursoOnline = queryPreguntaEvaluacionCursoOnline.OrderBy(x => x.PreguntaEvaluacionCursoOnlineId);
            LstPreguntaEvaluacionCursoOnline = queryPreguntaEvaluacionCursoOnline.ToPagedList(this.p, ConstantHelpers.DEFAULT_PAGE_SIZE);
        }
    }
}
