using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using PROMPERU.AULAVIRTUAL.BE.Aesorias;
using PROMPERU.AULAVIRTUAL.DA;

namespace PROMPERU.AULAVIRTUAL.BL
{
    public class AsesoriaAsesoradoBL
    {
        AsesoriaAsesoradoDA asesoriaAsesoradoDA = new AsesoriaAsesoradoDA();

        public List<AsesoriaRemota> ListarAsesoriaPorAsesoradoBloques(int asesoradoId, DateTime start, DateTime end)
        {
            List<AsesoriaRemota> retVal = new List<AsesoriaRemota>();
            retVal = asesoriaAsesoradoDA.ListarAsesoriaPorAsesoradoBloques(asesoradoId, start,end);

            return retVal;
        }

        public int SolicitarAsesoria(int asesoriaID, int asesoradoId, string asesoriaConsulta)
        {
            int retVal = 0;
            retVal = asesoriaAsesoradoDA.SolicitarAsesoria(asesoriaID, asesoradoId, asesoriaConsulta);

            return retVal;
        }

        public List<AsesoriaAsesoradoSolicitud> ListarAsesoriaSolicitudes(int asesoradoId)
        {
            List<AsesoriaAsesoradoSolicitud> retVal = new List<AsesoriaAsesoradoSolicitud>();
            retVal = asesoriaAsesoradoDA.ListarAsesoriaSolicitudes(asesoradoId);

            return retVal;
        }

        public List<AsesoriaAsesor> ListarAsesoriaPorAsesoradoDisponible(int asesoradoId, DateTime start)
        {
            List<AsesoriaAsesor> retVal = new List<AsesoriaAsesor>();
            retVal = asesoriaAsesoradoDA.ListarAsesoriaPorAsesoradoDisponible(asesoradoId, start);

            return retVal;
        }

        public AsesoriaRemota ObtenerAsesoriaCompletada(int asesoriaId, int asesoradoId)
        {
            throw new NotImplementedException();
        }

        public int ActualizarEstadoSolicitud(int asesoradoId, int solicitudId, string estado)
        {
            int retVal = 0;
            retVal = asesoriaAsesoradoDA.ActualizarEstadoSolicitud(asesoradoId, solicitudId, estado);

            return retVal;
        }

        public int CalificarAsesoriaCompletada(int asesoriaId, int calificacion, int asesoradoId)
        {
            throw new NotImplementedException();
        }
    }
}
