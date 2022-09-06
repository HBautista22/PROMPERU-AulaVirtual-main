using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using PROMPERU.AULAVIRTUAL.DA;
using PROMPERU.AULAVIRTUAL.BE.PARAMETROS;
using System.Data;
using PROMPERU.AULAVIRTUAL.BE.CURSOS;

namespace PROMPERU.AULAVIRTUAL.BL
{
    public class ParametroBL
    {
        public List<Parametro> ListarParametro()
        {
            ParametrosDA parametrosDA = new ParametrosDA();
            return parametrosDA.ListarParametro();
        }

        public List<Departamento> ListarDepartamento()
        {
            ParametrosDA parametrosDA = new ParametrosDA();
            return parametrosDA.ListarDepartamento();

        }

        public List<Provincia> ListarProvincia()
        {
            ParametrosDA parametrosDA = new ParametrosDA();
            return parametrosDA.ListarProvincia();

        }

        public List<Distrito> ListarDistrito()
        {
            ParametrosDA parametrosDA = new ParametrosDA();
            return parametrosDA.ListarDistrito();

        }




    }
}
