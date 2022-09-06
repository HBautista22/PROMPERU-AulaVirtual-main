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
	public ActionResult EditPreguntaCursoOnline(Int32? PreguntaCursoOnlineId)
	{
		var EditPreguntaCursoOnlineViewModel = new EditPreguntaCursoOnlineViewModel();
		EditPreguntaCursoOnlineViewModel.CargarDatos(CargarDatosContext(), PreguntaCursoOnlineId);
		return View(EditPreguntaCursoOnlineViewModel);
	}

	[HttpPost]
	public ActionResult EditPreguntaCursoOnline(EditPreguntaCursoOnlineViewModel model)
	{
		try
		{
			using (var TransactionScope = new TransactionScope())
			{
				if (!ModelState.IsValid)
				{
					model.CargarDatos(CargarDatosContext(), model.PreguntaCursoOnlineId);
					TryUpdateModel(model);
					PostMessage(MessageType.Error, "");
					return View(model);
				}

				var PreguntaCursoOnline = new PreguntaCursoOnline();

				if (model.PreguntaCursoOnlineId.HasValue)
				{
					PreguntaCursoOnline = context.PreguntaCursoOnline.First(x => x.PreguntaCursoOnlineId == model.PreguntaCursoOnlineId);
				}
				else
				{
					//Establecer las variables por defecto
					context.PreguntaCursoOnline.Add(PreguntaCursoOnline);
				}

				PreguntaCursoOnline.CursoOnlineId = model.CursoOnlineId;
				PreguntaCursoOnline.Tipo = model.Tipo;
				PreguntaCursoOnline.NumRespuestas = model.NumRespuestas;
				PreguntaCursoOnline.Texto = model.Texto;
				PreguntaCursoOnline.Puntaje = model.Puntaje;
				PreguntaCursoOnline.Estado = model.Estado;

				context.SaveChanges();

				TransactionScope.Complete();

				PostMessage(MessageType.Success);
				return RedirectToAction("ListPreguntaCursoOnline");
			}
		}
		catch (Exception ex)
		{
			InvalidarContext();
			PostMessage(MessageType.Error);
			model.CargarDatos(CargarDatosContext(), model.PreguntaCursoOnlineId);
			TryUpdateModel(model);
			return View(model);
		}
	}
*/
//************** END CONTROLLER *******************//
namespace Rimac.EVOL.ViewModel.Evaluation
{
    public class EditPreguntaCursoOnlineViewModel
    {
        public Int32? PreguntaCursoOnlineId { get; set; }

        [Display(Name = "CursoOnlineId")]
        [Required]
        public Int32 CursoOnlineId { get; set; }
        [Display(Name = "Tipo")]
        [Required]
        public String Tipo { get; set; }
        [Display(Name = "NumRespuestas")]
        [Required]
        public Int32 NumRespuestas { get; set; }
        [Display(Name = "Texto")]
        [Required]
        public String Texto { get; set; }
        [Display(Name = "Puntaje")]
        [Required]
        public Int32 Puntaje { get; set; }
        [Display(Name = "Estado")]
        [Required]
        public String Estado { get; set; }

        public List<CursoOnline> LstCursoOnline { get; set; }

        public EditPreguntaCursoOnlineViewModel()
        {
        }

        public void CargarDatos(CargarDatosContext dataContext, Int32? PreguntaCursoOnlineId)
        {
            this.PreguntaCursoOnlineId = PreguntaCursoOnlineId;

            if (PreguntaCursoOnlineId.HasValue)
            {
                var PreguntaCursoOnline = dataContext.context.PreguntaCursoOnline.First(x => x.PreguntaCursoOnlineId == PreguntaCursoOnlineId);
                this.PreguntaCursoOnlineId = PreguntaCursoOnline.PreguntaCursoOnlineId;
                this.CursoOnlineId = PreguntaCursoOnline.CursoOnlineId;
                this.Tipo = PreguntaCursoOnline.Tipo;
                this.NumRespuestas = PreguntaCursoOnline.NumRespuestas;
                this.Texto = PreguntaCursoOnline.Texto;
                this.Puntaje = PreguntaCursoOnline.Puntaje;
                this.Estado = PreguntaCursoOnline.Estado;
            }

            LstCursoOnline = dataContext.context.CursoOnline.ToList();
        }
    }
}
