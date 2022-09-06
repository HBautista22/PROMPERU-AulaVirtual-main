using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using PROMPERU.AULAVIRTUAL.BE.CURSOS;
using PROMPERU.AULAVIRTUAL.BE.PARAMETROS;
using PROMPERU.AULAVIRTUAL.DA;

namespace PROMPERU.AULAVIRTUAL.BL
{
    public class JsonBL
    {
        public List<Provincia> ListarProvinciaPorDepartamentoPorEstado(int? departamentoId, int estadoId)
        {
            JsonDA SQL = new JsonDA();
            return SQL.ListarProvinciaPorDepartamentoPorEstado(departamentoId, estadoId);//.Where(x => x.DepartamentoId == departamentoId).ToList();
        }

        public List<Distrito> ListarDistritoPorProvinciaPorEstado(int? provinciaId, int estadoId)
        {
            JsonDA SQL = new JsonDA();
            return SQL.ListarDistritoPorProvinciaPorEstado(provinciaId, estadoId);
        }

        public void InsertarLogUnidad(LogUnidad logUnidad)
        {
            JsonDA SQL = new JsonDA();
            SQL.InsertarLogUnidad(logUnidad);
        }
    }
}
