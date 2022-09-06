using PROMPERU.AULAVIRTUAL.BE.CMS;
using PROMPERU.AULAVIRTUAL.DA;
using PROMPERU.AULAVIRTUAL.WEB.Controllers;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace PROMPERU.AULAVIRTUAL.WEB.ViewModel.Cms
{
    public class EditMenuViewModel
    {
        public string FiltroCodigo { get; set; }
        public string FiltroTextoES { get; set; }

        public string MostrarPendientes { get; set; }

        public Dictionary<string, string> MenuID { get; set; }
        public Dictionary<string, string> Estado { get; set; }
        public Dictionary<string, string> DictTipoComponente { get; set; }

        public EditMenuViewModel()
        {
            MenuID = new Dictionary<string, string>();
            Estado = new Dictionary<string, string>();
            DictTipoComponente = new Dictionary<string, string>();
        }

        public List<CMS_MenuCMS> LstcMS_MenuCMs { get; set; }
        public void Fill(CargarDatosContext c/*, string filtroCodigo, string textoES, string textoEN, string mostrarPendientes*/)
        {
            /*this.FiltroCodigo = filtroCodigo;
            this.FiltroTextoES = textoES;
            this.FiltroTextoEN = textoEN;
            this.MostrarPendientes = mostrarPendientes;*/
            CMSDA cMSDA = new CMSDA();

            var query = cMSDA.ObtenerMenuCMS();    //c.context.CMS_Label.AsQueryable();

            

            this.LstcMS_MenuCMs = query.OrderBy(x => x.MenuID).ToList();
        }
    }
}