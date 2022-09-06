using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace PROMPERU.AULAVIRTUAL.BE.Aesorias
{
    public class AsesoriaAsesoradoSolicitud
    {
        public int Asre_Id { get; set; }
        public string Asre_Nombre { get; set; }
        public DateTime Asre_Inicio { get; set; }
        public DateTime Asre_Fin { get; set; }
        public int Asre_AsesorId { get; set; }
        public string Asre_Estado { get; set; }
        public string Asre_ConferenciaId { get; set; }
        public string NombreAsesor { get; set; }
        public string ApellidoAsesor { get; set; }
        public string Cargo { get; set; }
        public int Sola_Id { get; set; }
        public string Sola_Estado { get; set; }
    }
}
