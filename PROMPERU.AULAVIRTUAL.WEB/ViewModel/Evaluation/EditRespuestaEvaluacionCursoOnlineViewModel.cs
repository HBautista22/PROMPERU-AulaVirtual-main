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
	public ActionResult EditRespuestaEvaluacionCursoOnline(Int32? RespuestaEvaluacionCursoOnlineId)
	{
		var EditRespuestaEvaluacionCursoOnlineViewModel = new EditRespuestaEvaluacionCursoOnlineViewModel();
		EditRespuestaEvaluacionCursoOnlineViewModel.CargarDatos(CargarDatosContext(), RespuestaEvaluacionCursoOnlineId);
		return View(EditRespuestaEvaluacionCursoOnlineViewModel);
	}

	[HttpPost]
	public ActionResult EditRespuestaEvaluacionCursoOnline(EditRespuestaEvaluacionCursoOnlineViewModel model)
	{
		try
		{
			using (var TransactionScope = new TransactionScope())
			{
				if (!ModelState.IsValid)
				{
					model.CargarDatos(CargarDatosContext(), model.RespuestaEvaluacionCursoOnlineId);
					TryUpdateModel(model);
					PostMessage(MessageType.Error, "");
					return View(model);
				}

				var RespuestaEvaluacionCursoOnline = new RespuestaEvaluacionCursoOnline();

				if (model.RespuestaEvaluacionCursoOnlineId.HasValue)
				{
					RespuestaEvaluacionCursoOnline = context.RespuestaEvaluacionCursoOnline.First(x => x.RespuestaEvaluacionCursoOnlineId == model.RespuestaEvaluacionCursoOnlineId);
				}
				else
				{
					//Establecer las variables por defecto
					context.RespuestaEvaluacionCursoOnline.Add(RespuestaEvaluacionCursoOnline);
				}

				RespuestaEvaluacionCursoOnline.PreguntaEvaluacionCursoOnlineId = model.PreguntaEvaluacionCursoOnlineId;
				RespuestaEvaluacionCursoOnline.RespuestaCursoOnlineId = model.RespuestaCursoOnlineId;
				RespuestaEvaluacionCursoOnline.OrdenMostrado = model.OrdenMostrado;

				context.SaveChanges();

				TransactionScope.Complete();

				PostMessage(MessageType.Success);
				return RedirectToAction("ListRespuestaEvaluacionCursoOnline");
			}
		}
		catch (Exception ex)
		{
			InvalidarContext();
			PostMessage(MessageType.Error);
			model.CargarDatos(CargarDatosContext(), model.RespuestaEvaluacionCursoOnlineId);
			TryUpdateModel(model);
			return View(model);
		}
	}
*/
//************** END CONTROLLER *******************//
namespace Rimac.EVOL.ViewModel.Evaluation
{
    public class EditRespuestaEvaluacionCursoOnlineViewModel
    {
        public Int32? RespuestaEvaluacionCursoOnlineId { get; set; }

        [Display(Name = "PreguntaEvaluacionCursoOnlineId")]
        [Required]
        public Int32 PreguntaEvaluacionCursoOnlineId { get; set; }
        [Display(Name = "RespuestaCursoOnlineId")]
        [Required]
        public Int32 RespuestaCursoOnlineId { get; set; }
        [Display(Name = "OrdenMostrado")]
        [Required]
        public Int32 OrdenMostrado { get; set; }

        public List<PreguntaEvaluacionCursoOnline> LstPreguntaEvaluacionCursoOnline { get; set; }
        public List<RespuestaCursoOnline> LstRespuestaCursoOnline { get; set; }

        public EditRespuestaEvaluacionCursoOnlineViewModel()
        {
        }

        public void CargarDatos(CargarDatosContext dataContext, Int32? RespuestaEvaluacionCursoOnlineId)
        {
            this.RespuestaEvaluacionCursoOnlineId = RespuestaEvaluacionCursoOnlineId;

            if (RespuestaEvaluacionCursoOnlineId.HasValue)
            {
                var RespuestaEvaluacionCursoOnline = dataContext.context.RespuestaEvaluacionCursoOnline.First(x => x.RespuestaEvaluacionCursoOnlineId == RespuestaEvaluacionCursoOnlineId);
                this.RespuestaEvaluacionCursoOnlineId = RespuestaEvaluacionCursoOnline.RespuestaEvaluacionCursoOnlineId;
                this.PreguntaEvaluacionCursoOnlineId = RespuestaEvaluacionCursoOnline.PreguntaEvaluacionCursoOnlineId;
                this.RespuestaCursoOnlineId = RespuestaEvaluacionCursoOnline.RespuestaCursoOnlineId;
                this.OrdenMostrado = RespuestaEvaluacionCursoOnline.OrdenMostrado;
            }

            LstPreguntaEvaluacionCursoOnline = dataContext.context.PreguntaEvaluacionCursoOnline.ToList();
            LstRespuestaCursoOnline = dataContext.context.RespuestaCursoOnline.ToList();
        }
    }
}
