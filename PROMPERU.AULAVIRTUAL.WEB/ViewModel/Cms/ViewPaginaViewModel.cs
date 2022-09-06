using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using PROMPERU.AULAVIRTUAL.BE.CMS;
using PROMPERU.AULAVIRTUAL.BE.CURSOS;
using PROMPERU.AULAVIRTUAL.WEB.Controllers;


namespace PROMPERU.AULAVIRTUAL.WEB.ViewModel.Cms
{
    public class ViewPaginaViewModel
    {
        public CMS_Page Pagina { get; set; }
        //public EvolEntities Datos { get; set; }
        public HttpSessionStateBase Session { get; set; }
        public List<CMS_Testimonio> Testimonios { get; set; }
        public List<CMS_Certificate> Certificates { get; set; }
        public List <CMS_Banner> Banners { get; set; }
        public List<CMS_MenuItem> Menu { get; set; }
        public List<CMS_MenuItem> MapaSitio { get; set; }
        public List<CMS_Agenda> Agendas { get; set; }
        public List<CMS_PreguntaFrecuente> PreguntaFrecuentes { get; set; }

        public List<CategoriaCursoOnline> CategoriaCursoOnline { get; set; }


        public List<CursoOnlineResponse> Cursos { get; set; }
        public string Url_aula { get; set; }
        public string Analytics { get; set; }

        


        //public void Fill(CargarDatosContext c, CMS_Page pagina)
        //{

        //    Session = c.session;
        //    Pagina = pagina;
        //}
    }
}
