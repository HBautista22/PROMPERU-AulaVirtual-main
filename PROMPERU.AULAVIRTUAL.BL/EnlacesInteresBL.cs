using PROMPERU.AULAVIRTUAL.BE.CURSOS;
using PROMPERU.AULAVIRTUAL.BE.USUARIO;
using PROMPERU.AULAVIRTUAL.DA;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace PROMPERU.AULAVIRTUAL.BL
{
    public class EnlacesInteresBL
    {
        public List<EnlacesInteres> ListarEnlacesInteres()
        {
            EnlacesInteresDA usuarioSQL = new EnlacesInteresDA();
            return usuarioSQL.ListarEnlacesInteres();
        }

        public EnlacesInteres ObtenerEnlacesInteresPorEnlacesInteresId(int EnlacesInteresId)
        {
            EnlacesInteresDA usuarioSQL = new EnlacesInteresDA();
            return usuarioSQL.ObtenerEnlacesInteresPorEnlacesInteresId(EnlacesInteresId);
        }

        public int InsertarEnlacesInteres(EnlacesInteres EnlacesInteres)
        {
            EnlacesInteresDA usuarioSQL = new EnlacesInteresDA();
            return usuarioSQL.InsertarEnlacesInteres(EnlacesInteres);
        }

        public int ActualizarEnlacesInteres(EnlacesInteres EnlacesInteres)
        {
            EnlacesInteresDA usuarioSQL = new EnlacesInteresDA();
            return usuarioSQL.ActualizarEnlacesInteres(EnlacesInteres);
        }
        public int ActualizarEnlacesInteresEstado(int tipoUsuarioId, string estado)
        {
            EnlacesInteresDA usuarioSQL = new EnlacesInteresDA();
            return usuarioSQL.ActualizarEnlacesInteresEstado(tipoUsuarioId, estado);
        }
    }
}
