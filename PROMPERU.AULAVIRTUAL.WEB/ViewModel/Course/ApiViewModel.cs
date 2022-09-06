using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace PROMPERU.AULAVIRTUAL.WEB.ViewModel.Course
{
    public class ApiViewModel
    {
        public Int32 UnidadCursoOnlineId { get; set; }
        public Int32? MatriculaCursoOnlineId { get; set; }

        public void CargarDatos(Int32 unidadCursoOnlineId, Int32? matriculaCursoOnlineId)
        {
            UnidadCursoOnlineId = unidadCursoOnlineId;
            MatriculaCursoOnlineId = matriculaCursoOnlineId;
        }
    }
}