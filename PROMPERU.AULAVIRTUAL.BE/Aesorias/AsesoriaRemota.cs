using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace PROMPERU.AULAVIRTUAL.BE.Aesorias
{
    public class AsesoriaRemota
    {
        public int Asre_Id { get; set; }
        public int Asre_AsesorId { get; set; }
        public string Asre_Nombre { get; set; }
        public DateTime Asre_Inicio { get; set; }
        public DateTime Asre_Fin { get; set; }
        public DateTime Asre_Creacion { get; set; }
        public Guid Asre_ConferenciaId { get; set; }
        public string Asre_Estado { get; set; }
        public decimal Asre_Calificacion { get; set; }

        //Not table items
        public int Asre_Duracion { get; set; }
        public string Sola_Estado { get; set; }
    }
}
