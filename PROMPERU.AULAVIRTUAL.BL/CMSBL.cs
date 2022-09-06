using System;
using System.Collections.Generic;
using System.Data;
using System.Data.SqlClient;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
//using PROMPERU.AULAVIRTUAL.WEB;
using PROMPERU.AULAVIRTUAL.BE;
using PROMPERU.AULAVIRTUAL.DA;
using PROMPERU.AULAVIRTUAL.Helpers;
using PROMPERU.AULAVIRTUAL.BE.CMS;

namespace PROMPERU.AULAVIRTUAL.BL
{
    public class CMSBL
    {

        public CMS_Page SeleccionarPagina(string url)
        {
            CMSDA cmsda = new CMSDA();
            return cmsda.SeleccionarPagina(url);
        }

        public List<CMS_Testimonio> ListarTestimoniosDisponibles()
        {
            CMSDA cmsda = new CMSDA();
            return cmsda.ListarTestimoniosDisponibles();
        }

        public List<CMS_Banner> ListarBannerDisponibles()
        {
            CMSDA cmsda = new CMSDA();
            return cmsda.ListarBannerDisponibles();
        }

        public List<CMS_Certificate> ListarCertificateDisponibles()
        {
            CMSDA cmsda = new CMSDA();
            return cmsda.ListarCertificateDisponibles();
        }

        public List<CMS_MenuItem> ListarMenuItem(string Codigo)
        {
            CMSDA cmsda = new CMSDA();
            return cmsda.ListarMenuItem(Codigo);
        }

        public List<CMS_Label> ListarLabel()
        {
            CMSDA cmsda = new CMSDA();
            return cmsda.ListarLabel();
        }

        public List<CMS_Agenda> ListarAgenda()
        {
            CMSDA cmsda = new CMSDA();
            return cmsda.ListarAgenda();
        }

        public List<CMS_PreguntaFrecuente> ListarPreguntaFrecuente()
        {
            CMSDA cmsda = new CMSDA();
            return cmsda.ListarPreguntaFrecuente();
        }

        public CMS_Banner ObtenerBannerPorId(Int32? id)
        {
            CMSDA cmsda = new CMSDA();
            return cmsda.ObtenerBannerPorId(id);
        }

        public CMS_Agenda ObtenerAgendaPorId(Int32? id)
        {
            CMSDA cmsda = new CMSDA();
            return cmsda.ObtenerAgendaPorId(id);

        }

        public CMS_Testimonio ObtenerTestimonioPorId(Int32? id)
        {
            CMSDA cmsda = new CMSDA();
            return cmsda.ObtenerTestimonioPorId(id);
        }

        public CMS_PreguntaFrecuente ObtenerPreguntaFrecuentePorId(Int32? id)
        {
            CMSDA cmsda = new CMSDA();
            return cmsda.ObtenerPreguntaFrecuentePorId(id);
        }

        public CMS_Certificate ObtenerCertificatePorId(Int32? id)
        {
            CMSDA cmsda = new CMSDA();
            return cmsda.ObtenerCertificatePorId(id);
        }

        public int InsertarBanner(CMS_Banner banner)
        {
            CMSDA SQL = new CMSDA();
            return SQL.InsertarBanner(banner);
        }

        public int ActualizarBanner(CMS_Banner banner)
        {

            CMSDA SQL = new CMSDA();
            return SQL.ActualizarBanner(banner);
        }

        public int InsertarAgenda(CMS_Agenda agenda)
        {
            CMSDA SQL = new CMSDA();
            return SQL.InsertarAgenda(agenda);
        }

        public int ActualizarAgenda(CMS_Agenda agenda)
        {
            CMSDA SQL = new CMSDA();
            return SQL.ActualizarAgenda(agenda);

        }

        public int InsertarTestimonio(CMS_Testimonio testimonio)
        {
            CMSDA SQL = new CMSDA();
            return SQL.InsertarTestimonio(testimonio);
        }

        public int ActualizarTestimonio(CMS_Testimonio testimonio)
        {
            CMSDA SQL = new CMSDA();
            return SQL.ActualizarTestimonio(testimonio);

        }

        public int InsertarPreguntaFrecuente(CMS_PreguntaFrecuente preguntaFrecuente)
        {
            CMSDA SQL = new CMSDA();
            return SQL.InsertarPreguntaFrecuente(preguntaFrecuente);
        }

        public int ActualizarPreguntaFrecuente(CMS_PreguntaFrecuente preguntaFrecuente)
        {
            CMSDA SQL = new CMSDA();
            return SQL.ActualizarPreguntaFrecuente(preguntaFrecuente);

        }

        public int InsertarCertificate(CMS_Certificate certificate)
        {
            CMSDA SQL = new CMSDA();
            return SQL.InsertarCertificate(certificate);
        }

        public int ActualizarCertificate(CMS_Certificate certificate)
        {

            CMSDA SQL = new CMSDA();
            return SQL.ActualizarCertificate(certificate);
        }

        public CMS_Label ObtenerCMS_LabelPorId(int traduccionIdInt)
        {
            CMSDA SQL = new CMSDA();
            return SQL.ObtenerCMS_LabelPorId(traduccionIdInt);
        }

        public int ActualizarLabel(CMS_Label traduccion)
        {
            CMSDA SQL = new CMSDA();
            return SQL.ActualizarLabel(traduccion);
        }

        public List<CMS_MenuCMS> ObtenerMenuCMS()
        {
            CMSDA SQL = new CMSDA();
            return SQL.ObtenerMenuCMS();
        }

        public void ActualizarMenuCMS(int menuId, bool estado)
        {
            CMSDA SQL = new CMSDA();
            SQL.ActualizarMenuCMS(menuId, estado);
        }
    }
}
