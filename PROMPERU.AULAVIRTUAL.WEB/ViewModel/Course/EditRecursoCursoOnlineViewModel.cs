using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using PROMPERU.AULAVIRTUAL.WEB.Models;
using PROMPERU.AULAVIRTUAL.WEB.Controllers;
using PROMPERU.AULAVIRTUAL.WEB.Helpers;
using System.ComponentModel.DataAnnotations;
using PROMPERU.AULAVIRTUAL.BE.CURSOS;

//using System.Data.Entity;

//************** BEGIN CONTROLLER *******************//
/*
	public ActionResult EditRecursoCursoOnline(Int32? RecursoCursoOnlineId)
	{
		var EditRecursoCursoOnlineViewModel = new EditRecursoCursoOnlineViewModel();
		EditRecursoCursoOnlineViewModel.CargarDatos(CargarDatosContext, RecursoCursoOnlineId);
		return View(EditRecursoCursoOnlineViewModel);
	}

	[HttpPost]
	public ActionResult EditRecursoCursoOnline(EditRecursoCursoOnlineViewModel model)
	{
		try
		{
			using (var TransactionScope = new TransactionScope())
			{
				if (!ModelState.IsValid)
				{
					model.CargarDatos(CargarDatosContext, model.RecursoCursoOnlineId);
					TryUpdateModel(model);
					PostMessage(MessageType.Error, String.Empty);
					return View(model);
				}

				var RecursoCursoOnline = new RecursoCursoOnline();

				if (model.RecursoCursoOnlineId.HasValue)
				{
					RecursoCursoOnline = context.RecursoCursoOnline.First(x => x.RecursoCursoOnlineId == model.RecursoCursoOnlineId);
				}
				else
				{
					//Establecer las variables por defecto
					context.RecursoCursoOnline.Add(RecursoCursoOnline);
				}

				RecursoCursoOnline.Nombre = model.Nombre;
				RecursoCursoOnline.Ruta = model.Ruta;
				RecursoCursoOnline.Tipo = model.Tipo;
				RecursoCursoOnline.Estado = model.Estado;
				RecursoCursoOnline.UnidadCursoOnlineId = model.UnidadCursoOnlineId;

				context.SaveChanges();

				TransactionScope.Complete();

				PostMessage(MessageType.Success);
				return RedirectToAction("ListRecursoCursoOnline");
			}
		}
		catch (Exception ex)
		{
			InvalidarContext();
			PostMessage(MessageType.Error);
			model.CargarDatos(CargarDatosContext, model.RecursoCursoOnlineId);
			TryUpdateModel(model);
			return View(model);
		}
	}
*/
//************** END CONTROLLER *******************//
namespace PROMPERU.AULAVIRTUAL.WEB.ViewModel.Course
{
    public class EditRecursoCursoOnlineViewModel
    {
        public Int32? RecursoCursoOnlineId { get; set; }

        [Display(Name = "Nombre")]
        [Required(ErrorMessage = "El campo Nombre es obligatorio")]
        public String Nombre { get; set; }
        [Display(Name = "Ruta")]
        [Required(ErrorMessage = "El campo Ruta es obligatorio")]
        public String Ruta { get; set; }
        [Display(Name = "Tipo")]
        [Required(ErrorMessage = "El campo Tipo es obligatorio")]
        public String Tipo { get; set; }
        [Display(Name = "Estado")]
        [Required(ErrorMessage = "El campo Estado es obligatorio")]
        public String Estado { get; set; }
        [Display(Name = "UnidadCursoOnlineId")]
        [Required(ErrorMessage = "El campo Unidad es obligatorio")]
        public Int32 UnidadCursoOnlineId { get; set; }

        public List<UnidadCursoOnline> LstUnidadCursoOnline { get; set; }

        public EditRecursoCursoOnlineViewModel()
        {
        }

        //public void CargarDatos(CargarDatosContext dataContext, Int32? RecursoCursoOnlineId)
        //{
        //    this.RecursoCursoOnlineId = RecursoCursoOnlineId;

        //    if (RecursoCursoOnlineId.HasValue)
        //    {
        //        var RecursoCursoOnline = dataContext.context.RecursoCursoOnline.First(x => x.RecursoCursoOnlineId == RecursoCursoOnlineId);
        //        this.RecursoCursoOnlineId = RecursoCursoOnline.RecursoCursoOnlineId;
        //        this.Nombre = RecursoCursoOnline.Nombre;
        //        this.Ruta = RecursoCursoOnline.Ruta;
        //        this.Tipo = RecursoCursoOnline.Tipo;
        //        this.Estado = RecursoCursoOnline.Estado;
        //        this.UnidadCursoOnlineId = RecursoCursoOnline.UnidadCursoOnlineId;
        //    }

        //    LstUnidadCursoOnline = dataContext.context.UnidadCursoOnline.ToList();
        //}
    }
}
