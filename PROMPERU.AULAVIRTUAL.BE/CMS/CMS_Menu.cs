using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace PROMPERU.AULAVIRTUAL.BE.CMS
{
    public partial class CMS_Menu
    {
        public CMS_Menu()
        {
            this.CMS_MenuItem = new HashSet<CMS_MenuItem>();
        }

        public int Id { get; set; }

        public string Codigo { get; set; }

        public bool EsPublicado { get; set; }

        public Nullable<System.DateTime> FechaEdicion { get; set; }

        public Nullable<int> UsuarioEdicionId { get; set; }


        public virtual ICollection<CMS_MenuItem> CMS_MenuItem { get; set; }

    }
}
