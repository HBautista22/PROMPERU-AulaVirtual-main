using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace PROMPERU.AULAVIRTUAL.BE.CURSOS
{
    public partial class DetalleCursoOnlineEncuesta
    {
        public int CursoOnlineEncuestaDetalleId { get; set; }
        public int CursoOnlineEncuestaId { get; set; }
        public string Pregunta { get; set; }
        public int TipoPregunta { get; set; }
        public string Opciones { get; set; }
        public Nullable<Boolean> Activo { get; set; }
    }
}
