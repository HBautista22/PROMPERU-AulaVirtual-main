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
	public ActionResult EditPreguntaEvaluacionCursoOnline(Int32? PreguntaEvaluacionCursoOnlineId)
	{
		var EditPreguntaEvaluacionCursoOnlineViewModel = new EditPreguntaEvaluacionCursoOnlineViewModel();
		EditPreguntaEvaluacionCursoOnlineViewModel.CargarDatos(CargarDatosContext(), PreguntaEvaluacionCursoOnlineId);
		return View(EditPreguntaEvaluacionCursoOnlineViewModel);
	}

	[HttpPost]
	public ActionResult EditPreguntaEvaluacionCursoOnline(EditPreguntaEvaluacionCursoOnlineViewModel model)
	{
		try
		{
			using (var TransactionScope = new TransactionScope())
			{
				if (!ModelState.IsValid)
				{
					model.CargarDatos(CargarDatosContext(), model.PreguntaEvaluacionCursoOnlineId);
					TryUpdateModel(model);
					PostMessage(MessageType.Error, "");
					return View(model);
				}

				var PreguntaEvaluacionCursoOnline = new PreguntaEvaluacionCursoOnline();

				if (model.PreguntaEvaluacionCursoOnlineId.HasValue)
				{
					PreguntaEvaluacionCursoOnline = context.PreguntaEvaluacionCursoOnline.First(x => x.PreguntaEvaluacionCursoOnlineId == model.PreguntaEvaluacionCursoOnlineId);
				}
				else
				{
					//Establecer las variables por defecto
					context.PreguntaEvaluacionCursoOnline.Add(PreguntaEvaluacionCursoOnline);
				}

				PreguntaEvaluacionCursoOnline.EvaluacionCursoOnlineId = model.EvaluacionCursoOnlineId;
				PreguntaEvaluacionCursoOnline.PreguntaCursoOnlineId = model.PreguntaCursoOnlineId;
				PreguntaEvaluacionCursoOnline.TieneRespuestaCorrecta = model.TieneRespuestaCorrecta;

				context.SaveChanges();

				TransactionScope.Complete();

				PostMessage(MessageType.Success);
				return RedirectToAction("ListPreguntaEvaluacionCursoOnline");
			}
		}
		catch (Exception ex)
		{
			InvalidarContext();
			PostMessage(MessageType.Error);
			model.CargarDatos(CargarDatosContext(), model.PreguntaEvaluacionCursoOnlineId);
			TryUpdateModel(model);
			return View(model);
		}
	}
*/
//************** END CONTROLLER *******************//
namespace Rimac.EVOL.ViewModel.Evaluation
{
    public class EditPreguntaEvaluacionCursoOnlineViewModel
    {
        public Int32? PreguntaEvaluacionCursoOnlineId { get; set; }

        [Display(Name = "EvaluacionCursoOnlineId")]
        [Required]
        public Int32 EvaluacionCursoOnlineId { get; set; }
        [Display(Name = "PreguntaCursoOnlineId")]
        [Required]
        public Int32 PreguntaCursoOnlineId { get; set; }
        [Display(Name = "TieneRespuestaCorrecta")]
        public Boolean? TieneRespuestaCorrecta { get; set; }

        public List<EvaluacionCursoOnline> LstEvaluacionCursoOnline { get; set; }
        public List<PreguntaCursoOnline> LstPreguntaCursoOnline { get; set; }

        public EditPreguntaEvaluacionCursoOnlineViewModel()
        {
        }

        public void CargarDatos(CargarDatosContext dataContext, Int32? PreguntaEvaluacionCursoOnlineId)
        {
            this.PreguntaEvaluacionCursoOnlineId = PreguntaEvaluacionCursoOnlineId;

            if (PreguntaEvaluacionCursoOnlineId.HasValue)
            {
                var PreguntaEvaluacionCursoOnline = dataContext.context.PreguntaEvaluacionCursoOnline.First(x => x.PreguntaEvaluacionCursoOnlineId == PreguntaEvaluacionCursoOnlineId);
                this.PreguntaEvaluacionCursoOnlineId = PreguntaEvaluacionCursoOnline.PreguntaEvaluacionCursoOnlineId;
                this.EvaluacionCursoOnlineId = PreguntaEvaluacionCursoOnline.EvaluacionCursoOnlineId;
                this.PreguntaCursoOnlineId = PreguntaEvaluacionCursoOnline.PreguntaCursoOnlineId;
                this.TieneRespuestaCorrecta = PreguntaEvaluacionCursoOnline.TieneRespuestaCorrecta;
            }

            LstEvaluacionCursoOnline = dataContext.context.EvaluacionCursoOnline.ToList();
            LstPreguntaCursoOnline = dataContext.context.PreguntaCursoOnline.ToList();
        }
    }
}
