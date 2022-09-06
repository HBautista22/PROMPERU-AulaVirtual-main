using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using PROMPERU.AULAVIRTUAL.BE.CURSOS;
using PROMPERU.AULAVIRTUAL.WEB.Controllers;
using PROMPERU.AULAVIRTUAL.WEB.Helpers;
using System.ComponentModel.DataAnnotations;
//using System.Data.Entity;

namespace PROMPERU.AULAVIRTUAL.WEB.ViewModel.Course
{
    public class EditCategoriaCursoOnlineViewModel
    {
        public Int32? CategoriaCursoOnlineId { get; set; }

        [Display(Name = "Nombre")]
        [Required(ErrorMessage = "el campo Nombre es obligatorio")]
        public String Nombre { get; set; }

        [Display(Name = "Color")]
        public String Color { get; set; }


        public EditCategoriaCursoOnlineViewModel()
        {
        }

        public void CargarDatos(CargarDatosContext dataContext, Int32? CategoriaCursoOnlineId)
        {
            //this.CategoriaCursoOnlineId = CategoriaCursoOnlineId;

            //if (CategoriaCursoOnlineId.HasValue)
            //{
            //    CategoriaCursoOnline categoriaCursoOnline = new CategoriaCursoOnline();
            //    //var CategoriaCursoOnline = dataContext.context.CategoriaCursoOnline.Find(CategoriaCursoOnlineId); 
            //    //TODO: Data Acces
            //    this.CategoriaCursoOnlineId = categoriaCursoOnline.CategoriaCursoOnlineId;
            //    this.Nombre = categoriaCursoOnline.Nombre;
            //    this.Color = categoriaCursoOnline.Color;
            //}

        }
    }
}
