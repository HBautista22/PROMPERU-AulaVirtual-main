using PROMPERU.AULAVIRTUAL.BE.CURSOS;
using PROMPERU.AULAVIRTUAL.DA;
using PROMPERU.AULAVIRTUAL.WEB.Controllers;

using PROMPERU.AULAVIRTUAL.WEB.Helpers;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using PROMPERU.AULAVIRTUAL.BE.USUARIO;


namespace PROMPERU.AULAVIRTUAL.WEB.ViewModel.Home
{
    public class AdministradorIndexViewModel
    {
        public Int32? CantidadAlumno { get; set; }
        public Int32? CantidadCursos { get; set; }
        public Int32? CantidadCertificados { get; set; }

        public Int32? CantidadEmpresa { get; set; }

        public List<EnlacesInteres> EnlacesInteres { get; set; }
        public List<CursoOnlineResponse> CursosOnlineRanking { get; set; }
        public List<Usuario> Certificados { get; set; }
    }
}