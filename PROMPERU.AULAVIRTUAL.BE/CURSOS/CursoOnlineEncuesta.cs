using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace PROMPERU.AULAVIRTUAL.BE.CURSOS
{
    using System;
    using System.Collections.Generic;
    public partial class CursoOnlineEncuesta
    {
        [System.Diagnostics.CodeAnalysis.SuppressMessage("Microsoft.Usage", "CA2214:DoNotCallOverridableMethodsInConstructors")]
        public CursoOnlineEncuesta()
        {

            this.DetalleCursoOnlineEncuesta = new HashSet<DetalleCursoOnlineEncuesta>();

        

        }
        public int CursoOnlineEncuestaId { get; set; }

        public string Nombre { get; set; }

        public string Descripcion { get; set; }

        public int CursoOnlineId { get; set; }

 

        
        public bool Activo { get; set; }

       

        [System.Diagnostics.CodeAnalysis.SuppressMessage("Microsoft.Usage", "CA2227:CollectionPropertiesShouldBeReadOnly")]

        public virtual ICollection<DetalleCursoOnlineEncuesta> DetalleCursoOnlineEncuesta { get; set; }
    }
}
