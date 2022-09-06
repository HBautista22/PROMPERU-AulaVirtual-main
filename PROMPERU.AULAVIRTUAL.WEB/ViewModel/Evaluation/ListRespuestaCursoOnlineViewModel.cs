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
	public ActionResult ListRespuestaCursoOnline(Int32? p)
	{
		var ListRespuestaCursoOnlineViewModel = new ListRespuestaCursoOnlineViewModel();
		ListRespuestaCursoOnlineViewModel.CargarDatos(CargarDatosContext(),p);
		return View(ListRespuestaCursoOnlineViewModel);
	}
*/
//************** END CONTROLLER *******************//
namespace Rimac.EVOL.ViewModel.Evaluation
{
    public class ListRespuestaCursoOnlineViewModel
    {
        public Int32 p { get; set; }
        public IPagedList<RespuestaCursoOnline> LstRespuestaCursoOnline { get; set; }

        public ListRespuestaCursoOnlineViewModel()
        {
        }

        public void CargarDatos(CargarDatosContext dataContext, Int32? p)
        {
            this.p = p ?? 1;
            IQueryable<RespuestaCursoOnline> queryRespuestaCursoOnline = dataContext.context.RespuestaCursoOnline.AsQueryable();
            queryRespuestaCursoOnline = queryRespuestaCursoOnline.OrderBy(x => x.RespuestaCursoOnlineId);
            LstRespuestaCursoOnline = queryRespuestaCursoOnline.ToPagedList(this.p, ConstantHelpers.DEFAULT_PAGE_SIZE);
        }
    }
}
