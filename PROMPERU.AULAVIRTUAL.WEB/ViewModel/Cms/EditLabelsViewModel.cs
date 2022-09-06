using System;
//using PagedList;
using System.Collections.Generic;
using System.Linq;
using System.Web;
//using PROMPERU.AULAVIRTUAL.WEB.Models;
using PROMPERU.AULAVIRTUAL.BE.CMS;
using PROMPERU.AULAVIRTUAL.WEB.Controllers;
using PROMPERU.AULAVIRTUAL.DA;

namespace PROMPERU.AULAVIRTUAL.WEB.ViewModel.Cms
{
    public class EditLabelsViewModel
    {
        public string FiltroCodigo { get; set; }
        public string FiltroTextoES { get; set; }

        public string MostrarPendientes { get; set; }
        public string Imagen { get; set; }
        public Dictionary<string, string> DictCodigo { get; set; }
        public Dictionary<string, string> DictTextoES { get; set; }
        public Dictionary<string, string> DictTipoComponente { get; set; }

        public EditLabelsViewModel()
        {
            DictCodigo = new Dictionary<string, string>();
            DictTextoES = new Dictionary<string, string>();
            DictTipoComponente = new Dictionary<string, string>();
        }

        public List<CMS_Label> LstTraduccionTexto { get; set; }
        public void Fill(CargarDatosContext c/*, string filtroCodigo, string textoES, string textoEN, string mostrarPendientes*/)
        {
            /*this.FiltroCodigo = filtroCodigo;
            this.FiltroTextoES = textoES;
            this.FiltroTextoEN = textoEN;
            this.MostrarPendientes = mostrarPendientes;*/
            CMSDA cMSDA = new CMSDA();

            var query = cMSDA.ListarLabel();    //c.context.CMS_Label.AsQueryable();

            if (!string.IsNullOrEmpty(this.FiltroCodigo))
                foreach (var word in this.FiltroCodigo.ToLower().Split(' '))
                    query = query.Where(x => x.Codigo.ToLower().Contains(word)).ToList();

            if (!string.IsNullOrEmpty(this.FiltroTextoES))
                foreach (var word in this.FiltroTextoES.ToLower().Split(' '))
                    query = query.Where(x => x.Texto.ToLower().Contains(word)).ToList();

            if (!string.IsNullOrEmpty(MostrarPendientes))
            {
                switch (MostrarPendientes)
                {
                    case "es": query = query.Where(x => x.Texto == null || x.Texto == "").ToList(); break;
                }
            }

            this.LstTraduccionTexto = query.OrderBy(x => x.Codigo).ToList();
        }
    }
}
