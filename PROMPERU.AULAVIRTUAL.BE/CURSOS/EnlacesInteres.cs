using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace PROMPERU.AULAVIRTUAL.BE.CURSOS
{
    public class EnlacesInteres
    {
        public int EnlaceInteresId { get; set; }
        public string Titulo { get; set; }
        public string Imagen { get; set; }
        public string Descripcion { get; set; }
        public string Tipo { get; set; }
        public string CodigoYoutube { get; set; }
        public string Url { get; set; }
        public string Estado { get; set; }
        public Nullable<System.DateTime> FechaEdicion { get; set; }
        public Nullable<int> UsuarioEdicionId { get; set; }
        public string Pdf { get; set; }
    }
}
