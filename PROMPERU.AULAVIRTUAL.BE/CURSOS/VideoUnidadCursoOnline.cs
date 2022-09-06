using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace PROMPERU.AULAVIRTUAL.BE.CURSOS
{
    public partial class VideoUnidadCursoOnline
    {
        public int VideoUnidadCursoOnlineId { get; set; }
        public int UnidadCursoOnlineUnidadCursoOnlineId { get; set; }
        public string Titulo { get; set; }
        public string CodigoYoutube { get; set; }
        public string Duracion { get; set; }
        public string Descripcion { get; set; }
        public string Estado { get; set; }
        public string Tipo { get; set; }
        public string Transcripcion { get; set; }
        public Nullable<DateTime> FechaTransmision { get; set; }
    }
}
