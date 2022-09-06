using System;
using System.Collections.Generic;
using System.Configuration;
using System.Web;
using System.Web.Mvc;
using PROMPERU.AULAVIRTUAL.BE.CMS;
using PROMPERU.AULAVIRTUAL.BL;
using PROMPERU.AULAVIRTUAL.DA;
using PROMPERU.AULAVIRTUAL.WEB.Helpers;
using PROMPERU.AULAVIRTUAL.WEB.ViewModel.Cms;

namespace PROMPERU.AULAVIRTUAL.WEB.Controllers
{
    public class CmsController : BaseController
    {
        private readonly CMSBL _cmsbl = new CMSBL();

        // GET: Cms

        public ActionResult ViewPagina(string url)
        {
            url = url ?? "";
            //CargarDatosContext _DatosContext = new CargarDatosContext();
            CMS_Page pagina = _cmsbl.SeleccionarPagina(url);
            CursosDA cursosDA = new CursosDA();

            if (pagina == null || (!pagina.EsPublicado && Session.GetRol() != AppRol.Administrador))
                return new HttpNotFoundResult("Esta página no está disponible");

            string rutaPagina = "~/Views/Cms/Templates/Pages/" + pagina.RutaPagina + "_new.cshtml";
            string rutaLayout = "~/Views/Cms/Templates/Layouts/" + pagina.RutaLayout + "_new.cshtml";

            ViewPaginaViewModel model = new ViewPaginaViewModel
            {
                Session = Session,
                Pagina = pagina,
                Testimonios = _cmsbl.ListarTestimoniosDisponibles(),
                Certificates = _cmsbl.ListarCertificateDisponibles(), //Model.Datos.CMS_Certificate.Where(x => x.EsPublicado).OrderByDescending(x => x.Id).ToList();
                Banners = _cmsbl.ListarBannerDisponibles(), //Model.Datos.CMS_Banner.Where(x => x.EsPublicado).OrderByDescending(x => x.Id).ToList();
                Menu = _cmsbl.ListarMenuItem("PRINCIPAL"), //("PRINCIPAL");
                MapaSitio = _cmsbl.ListarMenuItem("MAPA_SITIO"),
                Agendas = _cmsbl.ListarAgenda(),
                PreguntaFrecuentes = _cmsbl.ListarPreguntaFrecuente(),
                Url_aula = ConfigurationManager.AppSettings["PORTAL.AbsoluteUrl"],
                Analytics = ConvertHelpers.GetAppSeting("EVOL.Analytics"),
                CategoriaCursoOnline = cursosDA.ListarCategoriaCursoOnline(),
                Cursos = cursosDA.ListarCursosOnlineResponse()
            };

            return View(rutaPagina, rutaLayout, model);
        }

        public ActionResult ListBanner()
        {
            List<CMS_Banner> items = _cmsbl.ListarBannerDisponibles();
            return View(items);
        }

        public ActionResult ListAgenda()
        {
            List<CMS_Agenda> items = _cmsbl.ListarAgenda();
            return View("ListAgendaNew", items);
        }

        public ActionResult ListTestimonio()
        {
            List<CMS_Testimonio> items = _cmsbl.ListarTestimoniosDisponibles(); 
            return View("ListTestimonioNew", items);
        }

        public ActionResult ListPreguntaFrecuente()
        {
            List<CMS_PreguntaFrecuente> items = _cmsbl.ListarPreguntaFrecuente();
            return View("ListPreguntaFrecuenteNew", items);
        }

        public ActionResult ListCertificate()
        {
            List<CMS_Certificate> items = _cmsbl.ListarCertificateDisponibles();
            return View("ListCertificateNew", items);
        }

        public ActionResult AddEditBanner(int? id)
        {
            CMS_Banner item = _cmsbl.ObtenerBannerPorId(id);
            return View("AddEditBannerNew", item);
        }

        public ActionResult AddEditAgenda(int? id)
        {
            CMS_Agenda item = _cmsbl.ObtenerAgendaPorId(id);
            return View("AddEditAgendaNew",item);
        }

        public ActionResult AddEditTestimonio(int? id)
        {
            CMS_Testimonio item = _cmsbl.ObtenerTestimonioPorId(id);
            return View("AddEditTestimonioNew", item);
        }

        public ActionResult AddEditPreguntaFrecuente(int? id)
        {
            CMS_PreguntaFrecuente item = _cmsbl.ObtenerPreguntaFrecuentePorId(id);
            return View("AddEditPreguntaFrecuenteNew",item);
        }

        public ActionResult AddEditCertificate(int? id)
        {
            CMS_Certificate item = _cmsbl.ObtenerCertificatePorId(id);
            return View("AddEditCertificateNew",item);
        }

        [HttpPost]
        public ActionResult AddImageLabel(EditLabelsViewModel model,HttpPostedFileBase fileImagen)
        {
            if (fileImagen != null && fileImagen.ContentLength > 0)
            {
                string ruta = "~/Content/CMS/Banner/" + Guid.NewGuid() + "_" + fileImagen.FileName;
                fileImagen.SaveAs(Server.MapPath(ruta));
                ViewBag.Imagen = ruta;
            }

            foreach (string labelId in model.DictCodigo.Keys)
            {
                int traduccionIdInt = Convert.ToInt32(labelId);
                //var traduccion = context.CMS_Label.FirstOrDefault(x => x.CMS_LabelId == traduccionIdInt);
                CMS_Label traduccion = _cmsbl.ObtenerCMS_LabelPorId(traduccionIdInt);
                traduccion.Texto = model.DictTextoES.ContainsKey(labelId) ? model.DictTextoES[labelId] : null;
                traduccion.TipoComponente = model.DictTipoComponente.ContainsKey(labelId) ? model.DictTipoComponente[labelId] : null;
                traduccion.EsRaw = traduccion.TipoComponente.EndsWith("-RA");
                traduccion.FechaModificacion = DateTime.Now;
                CmsHelpers.Remove(traduccion.Codigo);

                _cmsbl.ActualizarLabel(traduccion);
            }

            //context.SaveChanges();

            model.Fill(DatosContext);
            return View("EditLabelsNew", model);
        }

        [HttpPost]
        public ActionResult AddEditBanner(CMS_Banner model, HttpPostedFileBase fileImagen)
        {

            try
            {
                bool isUpdate = true;
                CMS_Banner item = _cmsbl.ObtenerBannerPorId(model.Id);

                if (item == null)
                {
                    isUpdate = false;
                    item = new CMS_Banner();
                }

                if (fileImagen != null && fileImagen.ContentLength > 0)
                {
                    string ruta = "~/Content/CMS/Banner/" + Guid.NewGuid() + "_" + fileImagen.FileName;
                    fileImagen.SaveAs(Server.MapPath(ruta));
                    item.Imagen = ruta;
                }

                item.Nombre = model.Nombre;
                item.TituloSuperior = model.TituloSuperior;
                item.TituloInferior = model.TituloInferior;
                item.SubTitulo = model.SubTitulo;
                item.EsPublicado = model.EsPublicado;
                item.FechaEdicion = DateTime.Now;
                item.UsuarioEdicionId = Session.GetUsuarioId();


                if (isUpdate)
                {
                    _cmsbl.ActualizarBanner(item);
                }
                else
                {
                    _cmsbl.InsertarBanner(item);
                }


                PostMessage(MessageType.Success);
                return RedirectToAction("ListBanner");
            }
            catch (Exception ex)
            {
                PostMessage(MessageType.Warning, "Ha ocurrido un error al procesar la operación: " + ex.Message);
                return AddEditBanner(model.Id);
            }
        }
        
        public ActionResult ChangeStateBanner(int bannerid)
        {
            try
            {
                CMS_Banner banner = _cmsbl.ObtenerBannerPorId(bannerid);

                banner.EsPublicado = !banner.EsPublicado.Equals(true);
                _cmsbl.ActualizarBanner(banner);

                //context.SaveChanges();
                PostMessage(MessageType.Success);
                return RedirectToAction("ListBanner");
            }
            catch (Exception e)
            {
                //Util.SaveErrorLog(e);
                //InvalidarContext();
                PostMessage(MessageType.Error);
            }
            return RedirectToAction("ListBanner");
        }

        [HttpPost]
        public ActionResult AddEditAgenda(CMS_Agenda model, HttpPostedFileBase fileArchivo, HttpPostedFileBase fileImagen)
        {

            try
            {
                bool isUpdate = true;
                CMS_Agenda item = _cmsbl.ObtenerAgendaPorId(model.Id);

                if (item == null)
                {
                    isUpdate = false;
                    item = new CMS_Agenda();
                    
                }

                if (fileArchivo != null && fileArchivo.ContentLength > 0)
                {
                    string ruta = "~/Content/CMS/Agenda/" + Guid.NewGuid() + "_" + fileArchivo.FileName;
                    fileArchivo.SaveAs(Server.MapPath(ruta));
                    item.Archivo = ruta;
                }

                if (fileImagen != null && fileImagen.ContentLength > 0)
                {
                    string ruta = "~/Content/CMS/Agenda/" + Guid.NewGuid() + "_" + fileImagen.FileName;
                    fileImagen.SaveAs(Server.MapPath(ruta));
                    item.Imagen = ruta;
                }

                item.Nombre = model.Nombre;
                item.FechaInicio = model.FechaInicio;
                item.FechaFin = model.FechaFin;
                item.Sumilla = model.Sumilla;
                item.RutaInscripcion = model.RutaInscripcion;
                item.EsPublicado = model.EsPublicado;
                item.FechaEdicion = DateTime.Now;
                item.UsuarioEdicionId = Session.GetUsuarioId();

                if (isUpdate)
                {
                    _cmsbl.ActualizarAgenda(item);
                }
                else
                {
                    _cmsbl.InsertarAgenda(item);
                }

                PostMessage(MessageType.Success);
                return RedirectToAction("ListAgenda");
            }
            catch (Exception ex)
            {
                PostMessage(MessageType.Warning, "Ha ocurrido un error al procesar la operación: " + ex.Message);
                return AddEditAgenda(model.Id);
            }
        }

        [HttpPost]
        public ActionResult AddEditTestimonio(CMS_Testimonio model, HttpPostedFileBase fileImagen)
        {
            try
            {
                bool isUpdate = true;
                CMS_Testimonio item = _cmsbl.ObtenerTestimonioPorId(model.Id);

                if (item == null)
                {
                    isUpdate = false;
                    item = new CMS_Testimonio();
                }

                if (fileImagen != null && fileImagen.ContentLength > 0)
                {
                    string ruta = "~/Content/CMS/Testimonio/" + Guid.NewGuid() + "_" + fileImagen.FileName;
                    fileImagen.SaveAs(Server.MapPath(ruta));
                    item.Imagen = ruta;
                }

                item.Titulo = model.Titulo;
                item.Descripcion = model.Descripcion;
                item.Nombre = model.Nombre;
                item.Empresa = model.Empresa;
                item.EsPublicado = model.EsPublicado;
                item.FechaEdicion = DateTime.Now;
                item.UsuarioEdicionId = Session.GetUsuarioId();

                if (isUpdate)
                {
                    _cmsbl.ActualizarTestimonio(item);
                }
                else
                {
                    _cmsbl.InsertarTestimonio(item);
                }

                PostMessage(MessageType.Success);
                return RedirectToAction("ListTestimonio");
            }
            catch (Exception ex)
            {
                PostMessage(MessageType.Warning, "Ha ocurrido un error al procesar la operación: " + ex.Message);
                return AddEditTestimonio(model.Id);
            }
        }

        [HttpPost]
        public ActionResult AddEditPreguntaFrecuente(CMS_PreguntaFrecuente model)
        {
            try
            {
                bool isUpdate = true;
                CMS_PreguntaFrecuente item = _cmsbl.ObtenerPreguntaFrecuentePorId(model.Id);

                if (item == null)
                {
                    isUpdate = false;
                    item = new CMS_PreguntaFrecuente();
                    
                }

                item.Pregunta = model.Pregunta;
                item.Respuesta = model.Respuesta;
                item.EsPublicado = model.EsPublicado;
                item.FechaEdicion = DateTime.Now;
                item.UsuarioEdicionId = Session.GetUsuarioId();

                if (isUpdate)
                {
                    _cmsbl.ActualizarPreguntaFrecuente(item);
                }
                else
                {
                    _cmsbl.InsertarPreguntaFrecuente(item);
                }

                PostMessage(MessageType.Success);
                return RedirectToAction("ListPreguntaFrecuente");
            }
            catch (Exception ex)
            {
                PostMessage(MessageType.Warning, "Ha ocurrido un error al procesar la operación: " + ex.Message);
                return AddEditPreguntaFrecuente(model.Id);
            }
        }

        [HttpPost]
        public ActionResult AddEditCertificate(CMS_Certificate model, HttpPostedFileBase fileImagen)
        {

            try
            {
                bool isUpdate = true;
                CMS_Certificate item = _cmsbl.ObtenerCertificatePorId(model.Id);

                if (item == null)
                {
                    isUpdate = false;
                    item = new CMS_Certificate();
                    
                }

                if (fileImagen != null && fileImagen.ContentLength > 0)
                {
                    string ruta = "~/Content/CMS/Certificate/" + Guid.NewGuid() + "_" + fileImagen.FileName;
                    fileImagen.SaveAs(Server.MapPath(ruta));
                    item.Imagen = ruta;
                }

                item.Nombre = model.Nombre;
                item.Sumilla = model.Sumilla;
                item.EsPublicado = model.EsPublicado;
                item.FechaEdicion = DateTime.Now;
                item.UsuarioEdicionId = Session.GetUsuarioId();

                if (isUpdate)
                {
                    _cmsbl.ActualizarCertificate(item);
                }
                else
                {
                    _cmsbl.InsertarCertificate(item);
                }

                PostMessage(MessageType.Success);
                return RedirectToAction("ListCertificate");
            }
            catch (Exception ex)
            {
                PostMessage(MessageType.Warning, "Ha ocurrido un error al procesar la operación: " + ex.Message);
                return AddEditCertificate(model.Id);
            }
        }

        [ValidateInput(false)]
        public ActionResult EditLabels(EditLabelsViewModel model)
        {
            foreach (string labelId in model.DictCodigo.Keys)
            {
                int traduccionIdInt = Convert.ToInt32(labelId);
                //var traduccion = context.CMS_Label.FirstOrDefault(x => x.CMS_LabelId == traduccionIdInt);
                CMS_Label traduccion = _cmsbl.ObtenerCMS_LabelPorId(traduccionIdInt);
                traduccion.Texto = model.DictTextoES.ContainsKey(labelId) ? model.DictTextoES[labelId] : null;
                traduccion.TipoComponente = model.DictTipoComponente.ContainsKey(labelId) ? model.DictTipoComponente[labelId] : null;
                traduccion.EsRaw = traduccion.TipoComponente.EndsWith("-RA");
                traduccion.FechaModificacion = DateTime.Now;
                CmsHelpers.Remove(traduccion.Codigo);

                _cmsbl.ActualizarLabel( traduccion);
            }

            //context.SaveChanges();

            model.Fill(DatosContext);
            return View("EditLabelsNew",model);
        }

        [ValidateInput(false)]
        public ActionResult EditMenuCMS(EditMenuViewModel model)
        {
            foreach (string menu in model.MenuID.Keys)
            {
                int menuId = Convert.ToInt32(menu);
                bool estado =Convert.ToBoolean(model.Estado[menu]);
                _cmsbl.ActualizarMenuCMS(menuId,estado);
            }

            //context.SaveChanges();

            model.Fill(DatosContext);
            return View("EditMenuCMS", model);
        }
    }
}