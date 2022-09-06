using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using PROMPERU.AULAVIRTUAL.WEB.Models;
using PROMPERU.AULAVIRTUAL.WEB.Controllers;
using PROMPERU.AULAVIRTUAL.WEB.Helpers;
using System.ComponentModel.DataAnnotations;
using PROMPERU.AULAVIRTUAL.BE.CURSOS;
using PROMPERU.AULAVIRTUAL.BL;

namespace PROMPERU.AULAVIRTUAL.WEB.ViewModel.Course
{
    public class EditCursoOnlineEncuestaViewModel
    {
        CursosBL cursosBL = new CursosBL();

        public Int32? CursoOnlineEncuestaId { get; set; }

        [Required(ErrorMessage = "el campo nombre es obligatorio"),
        Display(Name = "Nombre")]
        [RegularExpression(ConstantHelpers.Expresion_Estandar)]
        public String Nombre { get; set; }

        [Required(ErrorMessage = "el campo Descripcion es obligatorio"),
          Display(Name = "Descripcion")]
        //[RegularExpression(ConstantHelpers.Expresion_Estandar)]
        public String Descripcion { get; set; }

        [Required(ErrorMessage = "el campo curso es obligatorio"),
       Display(Name = "Curso")]
        public Int32? CursoOnlineId { get; set; }

        [Required(ErrorMessage = "el campo Estado es obligatorio"),
       Display(Name = "¿Está Activo?")]
        public Boolean Activo { get; set; }

        public List<DetalleCursoOnlineEncuesta> Preguntas { get; set; }

        public EditCursoOnlineEncuestaViewModel()
        {
            this.Activo = true;
        }

        public void CargarDatos(CargarDatosContext dataContext, Int32? CursoOnlineId)
        {
            this.CursoOnlineId = CursoOnlineId;
            if (CursoOnlineId.HasValue)
            {
                CursoOnlineEncuesta cursoOnline = cursosBL.ListarCursoOnlineEncuesta((int)CursoOnlineId);   //dataContext.context.CursoOnline.Find(CursoOnlineId);
                if (cursoOnline.Activo == null)
                    cursoOnline.Activo = true;

                this.CursoOnlineId = CursoOnlineId;
                this.CursoOnlineEncuestaId = cursoOnline.CursoOnlineEncuestaId;
                this.Nombre = cursoOnline.Nombre;
                this.Descripcion = cursoOnline.Descripcion;
                this.Activo =(bool) cursoOnline.Activo;
            }
            Preguntas = cursosBL.ListarCursoOnlineEncuestaDetalle((int)this.CursoOnlineEncuestaId);
        }
    }
}