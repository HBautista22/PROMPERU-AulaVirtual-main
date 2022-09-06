using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace PROMPERU.AULAVIRTUAL.BE.Aesorias
{
    public class AsesoriaSolicitud
    {
        public int Sola_Id { get; set; }
        public int Asre_Id { get; set; }
        public int Sola_AsesoradoId { get; set; }
        public string Sola_Estado { get; set; }
        public DateTime Sola_Creacion { get; set; }
        public string Sola_Consulta { get; set; }
        public string Sola_Comentario { get; set; }
    }
}
