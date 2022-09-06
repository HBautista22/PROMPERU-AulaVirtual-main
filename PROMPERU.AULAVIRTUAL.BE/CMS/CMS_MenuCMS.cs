using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace PROMPERU.AULAVIRTUAL.BE.CMS
{
    public partial class CMS_MenuCMS
    {
        public int MenuID { get; set; }
        public string URL { get; set; }
        public string Nombre { get; set; }
        public string Seccion { get; set; }
        public string Class { get; set; }
        public bool Activo { get; set; }
        public string Perfil { get; set; }
        public  Nullable<int> MenuIDPadre { get; set; }
    }
}
