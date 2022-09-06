using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using Rimac.EVOL.Models;
using Rimac.EVOL.Controllers;
using Rimac.EVOL.Helpers;
using System.ComponentModel.DataAnnotations;
using System.Data.Entity;

//************** BEGIN CONTROLLER *******************//
/*
	public ActionResult EditRespuestaCursoOnline(Int32? RespuestaCursoOnlineId)
	{
		var EditRespuestaCursoOnlineViewModel = new EditRespuestaCursoOnlineViewModel();
		EditRespuestaCursoOnlineViewModel.CargarDatos(CargarDatosContext(), RespuestaCursoOnlineId);
		return View(EditRespuestaCursoOnlineViewModel);
	}

	[HttpPost]
	public ActionResult EditRespuestaCursoOnline(EditRespuestaCursoOnlineViewModel model)
	{
		try
		{
			using (var TransactionScope = new TransactionScope())
			{
				if (!ModelState.IsValid)
				{
					model.CargarDatos(CargarDatosContext(), model.RespuestaCursoOnlineId);
					TryUpdateModel(model);
					PostMessage(MessageType.Error, "");
					return View(model);
				}

				var RespuestaCursoOnline = new RespuestaCursoOnline();

				if (model.RespuestaCursoOnlineId.HasValue)
				{
					RespuestaCursoOnline = context.RespuestaCursoOnline.First(x => x.RespuestaCursoOnlineId == model.RespuestaCursoOnlineId);
				}
				else
				{
					//Establecer las variables por defecto
					context.RespuestaCursoOnline.Add(RespuestaCursoOnline);
				}

				RespuestaCursoOnline.PreguntaCursoOnlineId = model.PreguntaCursoOnlineId;
				RespuestaCursoOnline.Texto = model.Texto;
				RespuestaCursoOnline.EsSolucion = model.EsSolucion;
				RespuestaCursoOnline.OrdenSolucion = model.OrdenSolucion;

				context.SaveChanges();

				TransactionScope.Complete();

				PostMessage(MessageType.Success);
				return RedirectToAction("ListRespuestaCursoOnline");
			}
		}
		catch (Exception ex)
		{
			InvalidarContext();
			PostMessage(MessageType.Error);
			model.CargarDatos(CargarDatosContext(), model.RespuestaCursoOnlineId);
			TryUpdateModel(model);
			return View(model);
		}
	}
*/
//************** END CONTROLLER *******************//
namespace Rimac.EVOL.ViewModel.Evaluation
{
    public class EditRespuestaCursoOnlineViewModel
    {
        public Int32? RespuestaCursoOnlineId { get; set; }

        [Display(Name = "PreguntaCursoOnlineId")]
        [Required]
        public Int32 PreguntaCursoOnlineId { get; set; }
        [Display(Name = "Texto")]
        [Required]
        public String Texto { get; set; }
        [Display(Name = "EsSolucion")]
        [Required]
        public Boolean EsSolucion { get; set; }
        [Display(Name = "OrdenSolucion")]
        [Required]
        public Int32 OrdenSolucion { get; set; }

        public List<PreguntaCursoOnline> LstPreguntaCursoOnline { get; set; }

        public EditRespuestaCursoOnlineViewModel()
        {
        }

        public void CargarDatos(CargarDatosContext dataContext, Int32? RespuestaCursoOnlineId)
        {
            this.RespuestaCursoOnlineId = RespuestaCursoOnlineId;

            if (RespuestaCursoOnlineId.HasValue)
            {
                var RespuestaCursoOnline = dataContext.context.RespuestaCursoOnline.First(x => x.RespuestaCursoOnlineId == RespuestaCursoOnlineId);
                this.RespuestaCursoOnlineId = RespuestaCursoOnline.RespuestaCursoOnlineId;
                this.PreguntaCursoOnlineId = RespuestaCursoOnline.PreguntaCursoOnlineId;
                this.Texto = RespuestaCursoOnline.Texto;
                this.EsSolucion = RespuestaCursoOnline.EsSolucion;
                this.OrdenSolucion = RespuestaCursoOnline.OrdenSolucion;
            }

            LstPreguntaCursoOnline = dataContext.context.PreguntaCursoOnline.ToList();
        }
    }
}
