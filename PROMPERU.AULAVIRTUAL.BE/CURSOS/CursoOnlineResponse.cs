using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace PROMPERU.AULAVIRTUAL.BE.CURSOS
{
    public class CursoOnlineResponse
    {
        public string Nombre { get; set; }
        public string Descripcion { get; set; }
        public DateTime? FechaCreacion { get; set; }
        public int CursoOnlineId { get; set; }
        public string RutaImagen { get; set; }
        public int Ranking { get; set; }
        public int Total { get; set; }
        public int Calificacion { get; set; }
        public int CantidadCalificacion { get; set; }
        public string tipo   // property
        {

            get
            {
                string resultado = "";
                if (FechaCreacion.Value > (DateTime.Now.AddMonths(-5)))
                {
                    resultado += "mas-recientes";
                }
                else
                {
                    if (Ranking > 100)
                        resultado += "mas-valorados";
                    else
                        resultado += "eventos-webinar";
                }


                return (resultado);
            }

        }
        public bool esMasValorado
        {
            get
            {
                return (Ranking > 100);
            }
        }

        public int CategoriaCursoOnlineId { get; set; }
    }
}