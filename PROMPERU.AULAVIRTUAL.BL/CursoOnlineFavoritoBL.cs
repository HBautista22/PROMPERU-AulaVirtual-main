using PROMPERU.AULAVIRTUAL.BE.Aesorias;
using PROMPERU.AULAVIRTUAL.BE.CURSOS;
using PROMPERU.AULAVIRTUAL.DA;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace PROMPERU.AULAVIRTUAL.BL
{
    public class CursoOnlineFavoritoBL
    {
        CursoOnlineFavoritoDA cursoOnlineFavoritoDA = new CursoOnlineFavoritoDA();
        public int ActualizarFavorito(int usuarioId, int cursoOnlineId, bool estadoFavorito)
        {
            int retVal = 0;
            retVal = cursoOnlineFavoritoDA.ActualizarFavorito(usuarioId, cursoOnlineId,estadoFavorito);

            return retVal;
        }

        public List<CursoOnlineFavorito> ObtenerCursosFavoritos(int usuarioId)
        {
            CursosDA SQL = new CursosDA();
            return SQL.ObtenerCursosFavoritos(usuarioId);
        }
    }
}
