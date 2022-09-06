using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace PROMPERU.AULAVIRTUAL.BE.CURSOS
{
    public partial class CursoOnlineEncuestaRespuesta
    {
        public CursoOnlineEncuestaRespuesta()
        {

            this.CursoOnlineEncuestaRespuestaDetalle = new HashSet<CursoOnlineEncuestaRespuestaDetalle>();
        }
        public int CursoOnlineEncuestaRespuestaId { get; set; }

        public int MatriculaCursoOnlineId { get; set; }

        public int Calificacion { get; set; }

 



        [System.Diagnostics.CodeAnalysis.SuppressMessage("Microsoft.Usage", "CA2227:CollectionPropertiesShouldBeReadOnly")]

        public virtual ICollection<CursoOnlineEncuestaRespuestaDetalle> CursoOnlineEncuestaRespuestaDetalle { get; set; }
    }
}
