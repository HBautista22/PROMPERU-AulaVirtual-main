using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using PROMPERU.AULAVIRTUAL.BE.USUARIO;
using PROMPERU.AULAVIRTUAL.DA;

namespace PROMPERU.AULAVIRTUAL.BL
{
    public class EmpresaBL
    {
        public Empresa ObtenerEmpresaPorRUC(string RUC)
        {
            EmpresaDA SQL = new EmpresaDA();
            return SQL.ObtenerEmpresaPorRUC(RUC);
        }

        public List<Empresa> ListarEmpresa()
        {
            EmpresaDA usuarioSQL = new EmpresaDA();
            return usuarioSQL.ListarEmpresa();
        }

        public List<Empresa> ObtenerEmpresaPorEmpresaId(int empresaId)
        {
            EmpresaDA usuarioSQL = new EmpresaDA();
            return usuarioSQL.ObtenerEmpresaPorEmpresaId(empresaId);
        }

        public int InsertarEmpresa(Empresa empresa)
        {
            EmpresaDA usuarioSQL = new EmpresaDA();
            return usuarioSQL.InsertarEmpresa(empresa);
        }

        public int ActualizarEmpresa(Empresa empresa)
        {
            EmpresaDA usuarioSQL = new EmpresaDA();
            return usuarioSQL.ActualizarEmpresa(empresa);
        }
    }
}
