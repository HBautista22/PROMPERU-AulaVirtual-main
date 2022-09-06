using PROMPERU.AULAVIRTUAL.WEB.Controllers;
using PROMPERU.AULAVIRTUAL.WEB.Helpers;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.IO;
using System.Linq;
using System.Web;

namespace PROMPERU.AULAVIRTUAL.WEB.ViewModel.Enrollment
{
    public class _RegisterUsuarioCargaMasivaViewModel
    {
        [FileExtensionsValidationHelpers(Extension = ".xls,.xlsx")]
        [Required]
        [Display(Name="Archivo")]
        public HttpPostedFileBase FileCargaMasiva { get; set; }
        public Int32 CursoOnlineId { get; set; }


        public void CargarDatos(CargarDatosContext dataContext, Int32 cursoOnlineId)
        {
            CursoOnlineId = cursoOnlineId;

        }

    
    }
}