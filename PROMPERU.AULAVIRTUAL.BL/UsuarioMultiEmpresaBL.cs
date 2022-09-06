using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using PROMPERU.AULAVIRTUAL.BE.USUARIO;
using PROMPERU.AULAVIRTUAL.DA;

namespace PROMPERU.AULAVIRTUAL.BL
{
    public class UsuarioMultiEmpresaBL
    {
        public List<UsuarioMultiEmpresa> ListarUsuarioMultiEmpresa()
        {
            UsuarioMultiEmpresaDA usuarioSQL = new UsuarioMultiEmpresaDA();
            return usuarioSQL.ListarUsuarioMultiEmpresa();
        }
    }
}
