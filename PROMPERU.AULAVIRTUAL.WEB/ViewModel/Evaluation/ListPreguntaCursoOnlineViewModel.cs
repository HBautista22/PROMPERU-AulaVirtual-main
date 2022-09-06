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
	public ActionResult ListPreguntaCursoOnline(Int32? p)
	{
		var ListPreguntaCursoOnlineViewModel = new ListPreguntaCursoOnlineViewModel();
		ListPreguntaCursoOnlineViewModel.CargarDatos(CargarDatosContext(),p);
		return View(ListPreguntaCursoOnlineViewModel);
	}
*/
//************** END CONTROLLER *******************//
namespace Rimac.EVOL.ViewModel.Evaluation
{
    public class ListPreguntaCursoOnlineViewModel
    {
        public Int32 p { get; set; }
        public IPagedList<PreguntaCursoOnline> LstPreguntaCursoOnline { get; set; }

        public ListPreguntaCursoOnlineViewModel()
        {
        }

        public void CargarDatos(CargarDatosContext dataContext, Int32? p)
        {
            this.p = p ?? 1;
            IQueryable<PreguntaCursoOnline> queryPreguntaCursoOnline = dataContext.context.PreguntaCursoOnline.AsQueryable();
            queryPreguntaCursoOnline = queryPreguntaCursoOnline.OrderBy(x => x.PreguntaCursoOnlineId);
            LstPreguntaCursoOnline = queryPreguntaCursoOnline.ToPagedList(this.p, ConstantHelpers.DEFAULT_PAGE_SIZE);
        }
    }
}
