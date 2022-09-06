using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using PROMPERU.AULAVIRTUAL.WEB.Models;
using PROMPERU.AULAVIRTUAL.WEB.Controllers;
using PROMPERU.AULAVIRTUAL.WEB.Helpers;
using System.ComponentModel.DataAnnotations;
using System.IO;

namespace PROMPERU.AULAVIRTUAL.WEB.ViewModel.Course
{
    public class _DeleteCursoOnlineViewModel
    {
        public Int32? CursoOnlineId { get; set; }

        public void CargarDatos(CargarDatosContext dataContext, Int32? CursoOnlineId)
        {
            this.CursoOnlineId = CursoOnlineId;
        }

    }

}