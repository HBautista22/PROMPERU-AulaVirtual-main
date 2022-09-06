using PROMPERU.AULAVIRTUAL.WEB.Controllers;
using PROMPERU.AULAVIRTUAL.WEB.Helpers;
using PROMPERU.AULAVIRTUAL.WEB.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.ComponentModel.DataAnnotations;
using PagedList;
using PROMPERU.AULAVIRTUAL.BL;
using PROMPERU.AULAVIRTUAL.BE.CURSOS;
using PROMPERU.AULAVIRTUAL.BE.Aesorias;

namespace PROMPERU.AULAVIRTUAL.WEB.ViewModel.Report
{
    public class ReportAsesoriaRemotaViewModel
    {
        ReporteBL reporteBL = new ReporteBL();

        public IPagedList<ViewReporteAsesoriaRemota> LstViewReporteAsesoriaRemota { get; set; }
        public Int32? np { get; set; }
        /*[Display(Name = "Nombre / Código")]
        public String Codigo { get; set; }
        [Display(Name = "Razón Social / Ruc")]
        public String RazonSocial { get; set; }
        [Display(Name = "Curso")]
        public String Curso { get; set; }
        [Display(Name = "Cargo")]
        public String Cargo { get; set; }
        [Display(Name = "Grupo")]
        public String Grupo { get; set; }
        [Display(Name = "Estado")]
        public String Estado { get; set; }
        [Display(Name = "DNI")]
        public String Dni { get; set; }
        [Display(Name = "EMAIL")]
        public String Email { get; set; }
        */

        public void CargarDatos(CargarDatosContext dataContext, Int32? np)
        {
            this.np = np ?? 1;
            
            List<ViewReporteAsesoriaRemota> query = reporteBL.ListarViewReporteAsesoriaRemota();
            
            LstViewReporteAsesoriaRemota = query.OrderBy(x => x.Apellido).ToPagedList(this.np.Value, ConstantHelpers.DEFAULT_PAGE_SIZE);

        }
    }

}
