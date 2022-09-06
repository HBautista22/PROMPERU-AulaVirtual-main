using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace PROMPERU.AULAVIRTUAL.BE.CMS
{
    using System;
    using System.Collections.Generic;

    public partial class CMS_Certificate
    {

        public int Id { get; set; }

        public string Nombre { get; set; }

        public string Imagen { get; set; }

        public string Sumilla { get; set; }

        public bool EsPublicado { get; set; }

        public DateTime? FechaEdicion { get; set; }

        public int? UsuarioEdicionId { get; set; }

    }


}
