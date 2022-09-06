using PROMPERU.AULAVIRTUAL.BE.CURSOS;
using PROMPERU.AULAVIRTUAL.BE.Aesorias;
using PROMPERU.AULAVIRTUAL.DA;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace PROMPERU.AULAVIRTUAL.BL
{
    public class ReporteBL
    {
        public List<ViewReporteHistoricoAlumno> ListarViewReporteHistoricoAlumno()
        {
            ReporteDA reporteBA = new ReporteDA();
            return reporteBA.ListarViewReporteHistoricoAlumno();
        }

        public List<ViewReporteHistoricoAlumnoLst> ListarViewReporteHistoricoAlumnoLst()
        {
            ReporteDA reporteBA = new ReporteDA();
            return reporteBA.ListarViewReporteHistoricoAlumnoLst();
        }

        public List<ViewReporteAsesoriaRemota> ListarViewReporteAsesoriaRemota()
        {
            ReporteDA reporteBA = new ReporteDA();
            return reporteBA.ListarViewReporteAsesoriaRemota();
        }

        public List<ViewReporteSolicitudAsesoriaRemota> ListarViewReporteSolicitudAsesoriaRemota()
        {
            ReporteDA reporteBA = new ReporteDA();
            return reporteBA.ListarViewReporteSolicitudAsesoriaRemota();

        }


    }
}
