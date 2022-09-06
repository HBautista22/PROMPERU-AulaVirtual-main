using PROMPERU.AULAVIRTUAL.WEB.Controllers;
using PROMPERU.AULAVIRTUAL.WEB.Helpers;
using System;
using System.Collections.Generic;
using System.Linq;
using PagedList;
using PROMPERU.AULAVIRTUAL.BL;
using PROMPERU.AULAVIRTUAL.BE.Aesorias;

namespace PROMPERU.AULAVIRTUAL.WEB.ViewModel.Report
{
    public class ReportSolicitudAsesoriaViewModel
    {
        ReporteBL reporteBL = new ReporteBL();

        public IPagedList<ViewReporteSolicitudAsesoriaRemota> LstViewReporteSolicitudAsesoria { get; set; }
        public Int32? np { get; set; }
        
        public void CargarDatos(CargarDatosContext dataContext, Int32? np)
        {
            this.np = np ?? 1;
            
            List<ViewReporteSolicitudAsesoriaRemota> query = reporteBL.ListarViewReporteSolicitudAsesoriaRemota();

            LstViewReporteSolicitudAsesoria = query.OrderBy(x => x.Apellido).ToPagedList(this.np.Value, ConstantHelpers.DEFAULT_PAGE_SIZE);

        }
    }
}