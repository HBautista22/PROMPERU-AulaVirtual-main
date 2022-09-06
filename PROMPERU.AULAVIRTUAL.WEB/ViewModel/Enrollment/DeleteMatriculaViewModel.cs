using PROMPERU.AULAVIRTUAL.WEB.Controllers;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace PROMPERU.AULAVIRTUAL.WEB.ViewModel.Enrollment
{
    public class DeleteMatriculaViewModel
    {
        public Int32 MatriculaCursoOnlineId { get; set; }
        public String UrlRefferer { get; set; }
        public void CargarDatos(CargarDatosContext dataContext,Int32 matriculaCursoOnlineId)
        {
            MatriculaCursoOnlineId = matriculaCursoOnlineId;
        }
    }
}