using PROMPERU.AULAVIRTUAL.WEB.Controllers;
using PROMPERU.AULAVIRTUAL.WEB.Helpers;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI.WebControls;

namespace PROMPERU.AULAVIRTUAL.WEB.ViewModel.Course
{
    public class _SelectArchivoViewModel
    {
        public String CodigoCursoOnline { get; set; }
        public String UnidadCursoOnlineGUID { get; set; }
        public String SelectedFile { get; set; }

        public _SelectArchivoViewModel()
        {

        }

        public void CargarDatos(CargarDatosContext dataContext, String codigoCursoOnline, String unidadCursoOnlineGUID, String selectedFile)
        {
            CodigoCursoOnline = codigoCursoOnline;
            UnidadCursoOnlineGUID = unidadCursoOnlineGUID;
            SelectedFile = selectedFile;
             
        }
    }
}