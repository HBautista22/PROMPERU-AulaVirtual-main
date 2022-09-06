using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace PROMPERU.AULAVIRTUAL.BE.CURSOS
{
    public partial class CursoOnlineEncuestaRespuestaDetalle
    {
    
        public int CursoOnlineEncuestaRespuestaDetalleId { get; set; }

        public int CursoOnlineEncuestaRespuestaId { get; set; }

        public string Pregunta { get; set; }
        public string Respuesta { get; set; }
        public string FechaCreacion { get; set; }


    }
}
