using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace PROMPERU.AULAVIRTUAL.BE.CURSOS
{
    public class CursoOnlineVisto
    {
        public int CursoOnlineVistoId { get; set; }
        public int UsuarioId { get; set; }
        public int CursoOnlineId { get; set; }
        public int FechaRegisgtro { get; set; }

        public virtual CursoOnline CursoOnline { get; set; }
    }
}
