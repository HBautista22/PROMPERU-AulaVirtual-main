using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;


namespace PROMPERU.AULAVIRTUAL.BE.CMS
{
    using System;
    using System.Collections.Generic;

    public partial class CMS_Page
    {

        public CMS_Page()
        {

            this.CMS_MenuItem = new HashSet<CMS_MenuItem>();

        }


        public int Id { get; set; }

        public string Nombre { get; set; }

        public string Url { get; set; }

        public string RutaPagina { get; set; }

        public string RutaLayout { get; set; }

        public bool EsPublicado { get; set; }

        public Nullable<System.DateTime> FechaEdicion { get; set; }

        public Nullable<int> UsuarioEdicionId { get; set; }


        public virtual ICollection<CMS_MenuItem> CMS_MenuItem { get; set; }

    }
}
