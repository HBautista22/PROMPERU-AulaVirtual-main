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
	public ActionResult EditRespuestaSeleccionadaEvaluacionCursoOnline(Int32? RespuestaSeleccionadaEvaluacionCursoOnlineId)
	{
		var EditRespuestaSeleccionadaEvaluacionCursoOnlineViewModel = new EditRespuestaSeleccionadaEvaluacionCursoOnlineViewModel();
		EditRespuestaSeleccionadaEvaluacionCursoOnlineViewModel.CargarDatos(CargarDatosContext(), RespuestaSeleccionadaEvaluacionCursoOnlineId);
		return View(EditRespuestaSeleccionadaEvaluacionCursoOnlineViewModel);
	}

	[HttpPost]
	public ActionResult EditRespuestaSeleccionadaEvaluacionCursoOnline(EditRespuestaSeleccionadaEvaluacionCursoOnlineViewModel model)
	{
		try
		{
			using (var TransactionScope = new TransactionScope())
			{
				if (!ModelState.IsValid)
				{
					model.CargarDatos(CargarDatosContext(), model.RespuestaSeleccionadaEvaluacionCursoOnlineId);
					TryUpdateModel(model);
					PostMessage(MessageType.Error, "");
					return View(model);
				}

				var RespuestaSeleccionadaEvaluacionCursoOnline = new RespuestaSeleccionadaEvaluacionCursoOnline();

				if (model.RespuestaSeleccionadaEvaluacionCursoOnlineId.HasValue)
				{
					RespuestaSeleccionadaEvaluacionCursoOnline = context.RespuestaSeleccionadaEvaluacionCursoOnline.First(x => x.RespuestaSeleccionadaEvaluacionCursoOnlineId == model.RespuestaSeleccionadaEvaluacionCursoOnlineId);
				}
				else
				{
					//Establecer las variables por defecto
					context.RespuestaSeleccionadaEvaluacionCursoOnline.Add(RespuestaSeleccionadaEvaluacionCursoOnline);
				}

				RespuestaSeleccionadaEvaluacionCursoOnline.PreguntaEvaluacionCursoOnlineId = model.PreguntaEvaluacionCursoOnlineId;
				RespuestaSeleccionadaEvaluacionCursoOnline.RespuestaCursoOnlineId = model.RespuestaCursoOnlineId;
				RespuestaSeleccionadaEvaluacionCursoOnline.OrdenSeleccionado = model.OrdenSeleccionado;

				context.SaveChanges();

				TransactionScope.Complete();

				PostMessage(MessageType.Success);
				return RedirectToAction("ListRespuestaSeleccionadaEvaluacionCursoOnline");
			}
		}
		catch (Exception ex)
		{
			InvalidarContext();
			PostMessage(MessageType.Error);
			model.CargarDatos(CargarDatosContext(), model.RespuestaSeleccionadaEvaluacionCursoOnlineId);
			TryUpdateModel(model);
			return View(model);
		}
	}
*/
//************** END CONTROLLER *******************//
namespace Rimac.EVOL.ViewModel.Evaluation
{
    public class EditRespuestaSeleccionadaEvaluacionCursoOnlineViewModel
    {
        public Int32? RespuestaSeleccionadaEvaluacionCursoOnlineId { get; set; }

        [Display(Name = "PreguntaEvaluacionCursoOnlineId")]
        [Required]
        public Int32 PreguntaEvaluacionCursoOnlineId { get; set; }
        [Display(Name = "RespuestaCursoOnlineId")]
        [Required]
        public Int32 RespuestaCursoOnlineId { get; set; }
        [Display(Name = "OrdenSeleccionado")]
        [Required]
        public Int32 OrdenSeleccionado { get; set; }

        public List<PreguntaEvaluacionCursoOnline> LstPreguntaEvaluacionCursoOnline { get; set; }
        public List<RespuestaCursoOnline> LstRespuestaCursoOnline { get; set; }

        public EditRespuestaSeleccionadaEvaluacionCursoOnlineViewModel()
        {
        }

        public void CargarDatos(CargarDatosContext dataContext, Int32? RespuestaSeleccionadaEvaluacionCursoOnlineId)
        {
            this.RespuestaSeleccionadaEvaluacionCursoOnlineId = RespuestaSeleccionadaEvaluacionCursoOnlineId;

            if (RespuestaSeleccionadaEvaluacionCursoOnlineId.HasValue)
            {
                var RespuestaSeleccionadaEvaluacionCursoOnline = dataContext.context.RespuestaSeleccionadaEvaluacionCursoOnline.First(x => x.RespuestaSeleccionadaEvaluacionCursoOnlineId == RespuestaSeleccionadaEvaluacionCursoOnlineId);
                this.RespuestaSeleccionadaEvaluacionCursoOnlineId = RespuestaSeleccionadaEvaluacionCursoOnline.RespuestaSeleccionadaEvaluacionCursoOnlineId;
                this.PreguntaEvaluacionCursoOnlineId = RespuestaSeleccionadaEvaluacionCursoOnline.PreguntaEvaluacionCursoOnlineId;
                this.RespuestaCursoOnlineId = RespuestaSeleccionadaEvaluacionCursoOnline.RespuestaCursoOnlineId;
                this.OrdenSeleccionado = RespuestaSeleccionadaEvaluacionCursoOnline.OrdenSeleccionado;
            }

            LstPreguntaEvaluacionCursoOnline = dataContext.context.PreguntaEvaluacionCursoOnline.ToList();
            LstRespuestaCursoOnline = dataContext.context.RespuestaCursoOnline.ToList();
        }
    }
}
