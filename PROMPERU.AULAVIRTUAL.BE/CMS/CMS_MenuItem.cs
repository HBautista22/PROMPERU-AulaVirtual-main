using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace PROMPERU.AULAVIRTUAL.BE.CMS
{
    public partial class CMS_MenuItem
    {

        public CMS_MenuItem()
        {

            this.CMS_MenuItem1 = new HashSet<CMS_MenuItem>();

        }


        public int Id { get; set; }

        public int MenuId { get; set; }

        public Nullable<int> PageId { get; set; }

        public Nullable<int> ParentMenuItemId { get; set; }

        public string CustomUrl { get; set; }

        public string Titulo { get; set; }

        public int Orden { get; set; }

        public bool EsPublicado { get; set; }

        public Nullable<System.DateTime> FechaEdicion { get; set; }

        public Nullable<int> UsuarioEdicionId { get; set; }



        public virtual CMS_Menu CMS_Menu { get; set; }

        public virtual ICollection<CMS_MenuItem> CMS_MenuItem1 { get; set; }

        public virtual CMS_MenuItem CMS_MenuItem2 { get; set; }

        public virtual CMS_Page CMS_Page { get; set; }

    }
}
