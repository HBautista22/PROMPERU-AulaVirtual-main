using PROMPERU.AULAVIRTUAL.BE.USUARIO;
using PROMPERU.AULAVIRTUAL.DA;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace PROMPERU.AULAVIRTUAL.BL
{
    public class TipoEmpresaBL
    {
       

        public List<TipoEmpresa> ListarTipoEmpresa()
        {
            TipoEmpresaDA usuarioSQL = new TipoEmpresaDA();
            return usuarioSQL.ListarTipoEmpresa();
        }

        public TipoEmpresa ObtenerTipoEmpresaPorTipoEmpresaId(int TipoEmpresaId)
        {
            TipoEmpresaDA usuarioSQL = new TipoEmpresaDA();
            return usuarioSQL.ObtenerTipoEmpresaPorTipoEmpresaId(TipoEmpresaId);
        }

        public int InsertarTipoEmpresa(TipoEmpresa TipoEmpresa)
        {
            TipoEmpresaDA usuarioSQL = new TipoEmpresaDA();
            return usuarioSQL.InsertarTipoEmpresa(TipoEmpresa);
        }

        public int ActualizarTipoEmpresa(TipoEmpresa TipoEmpresa)
        {
            TipoEmpresaDA usuarioSQL = new TipoEmpresaDA();
            return usuarioSQL.ActualizarTipoEmpresa(TipoEmpresa);
        }
        public int ActualizarTipoEmpresaEstado(int tipoUsuarioId,string estado)
        {
            TipoEmpresaDA usuarioSQL = new TipoEmpresaDA();
            return usuarioSQL.ActualizarTipoEmpresaEstado(tipoUsuarioId,estado);
        }
    }

}
