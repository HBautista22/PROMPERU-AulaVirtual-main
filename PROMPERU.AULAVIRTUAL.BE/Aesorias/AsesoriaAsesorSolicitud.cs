using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace PROMPERU.AULAVIRTUAL.BE.Aesorias
{
    public class AsesoriaAsesorSolicitud
    {
        public int Asre_Id { get; set; }
        public string Asre_Nombre { get; set; }
        public DateTime Asre_Inicio { get; set; }
        public DateTime Asre_Fin { get; set; }
        public Guid Asre_ConferenciaId { get; set; }
        public int Sola_Id { get; set; }
        public string Sola_Estado { get; set; }
        public string NombreAsesorado { get; set; }
        public string ApellidoAsesorado { get; set; }
        public string Email { get; set; }
        public string Comentario { get; set; }
        public string Consulta { get; set; }
        public string Telefono { get; set; }
        public string Cargo { get; set; }


    }
}
