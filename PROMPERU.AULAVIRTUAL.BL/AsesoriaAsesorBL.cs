using PROMPERU.AULAVIRTUAL.BE.Aesorias;
using PROMPERU.AULAVIRTUAL.DA;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace PROMPERU.AULAVIRTUAL.BL
{
    public class AsesoriaAsesorBL
    {
        AsesoriaAsesorDA asesoriaAsesorDA = new AsesoriaAsesorDA();
        public List<AsesoriaRemota> ListarAsesoriaPorAsesor(int asesorId)
        {
            List<AsesoriaRemota> retVal = new List<AsesoriaRemota>();
            retVal = asesoriaAsesorDA.ListarAsesoriaPorAsesor(asesorId);

            return retVal;
        }

        public List<AsesoriaAsesorSolicitud> ListarSolicitudPorAsesor(int asesorId)
        {
            List<AsesoriaAsesorSolicitud> retVal = new List<AsesoriaAsesorSolicitud>();
            retVal = asesoriaAsesorDA.ListarSolicitudPorAsesor(asesorId);

            return retVal;
        }

        public int SaveAsesoriaAsesor(AsesoriaRemota model)
        {
            int retVal = 0;
            retVal = asesoriaAsesorDA.SaveAsesoriaAsesor(model);

            return retVal;
        }

        public int ActualizarEstadoSolicitud(int asesorId, int solicitudId, string estado, string comentario)
        {
            int retVal = 0;
            retVal = asesoriaAsesorDA.ActualizarEstadoSolicitud(asesorId, solicitudId, estado, comentario);

            return retVal;
        }

        public int ActualizarEstadoAsesoria(int asesorId, int asesoriaId, string estado)
        {
            int retVal = 0;
            retVal = asesoriaAsesorDA.ActualizarEstadoAsesoria(asesorId, asesoriaId, estado);

            return retVal;

        }

        public AsesoriasSolicitudCalendar ObtenerDatosSolicitudCalendar(int solicitudId)
        {
            AsesoriasSolicitudCalendar retVal = new AsesoriasSolicitudCalendar();
            retVal = asesoriaAsesorDA.ObtenerDatosSolicitudCalendar(solicitudId);

            return retVal;
        }
    }
}
