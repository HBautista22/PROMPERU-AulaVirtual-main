using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace PROMPERU.AULAVIRTUAL.BE.CMS
{

    using System;
    using System.Collections.Generic;

    public partial class CMS_Banner
    {

        public int Id { get; set; }

        public string Nombre { get; set; }

        public string Imagen { get; set; }

        public string TituloSuperior { get; set; }

        public string TituloInferior { get; set; }

        public string SubTitulo { get; set; }

        public bool EsPublicado { get; set; }

        public Nullable<System.DateTime> FechaEdicion { get; set; }

        public Nullable<int> UsuarioEdicionId { get; set; }

    }

}
