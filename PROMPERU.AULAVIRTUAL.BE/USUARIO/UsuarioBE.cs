using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace PROMPERU.AULAVIRTUAL.BE.USUARIO
{
    public class UsuarioItemsBE
    {

        public string UsuarioStr { get; set; }
        public int[] GrupoId { get; set; }
        public int[] UsuarioId { get; set; }

    }

    public class UsuarioBulkItemBE
    {

        public int UsuarioId { get; set; }
        public int GrupoId { get; set; }
        public string Estado { get; set; }

    }

    public class UsuarioBulkListBE
    {
        public List<UsuarioBulkItemBE> Lista { get; set; }
    }


}
