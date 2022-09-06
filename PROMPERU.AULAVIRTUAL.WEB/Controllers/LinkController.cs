using PROMPERU.AULAVIRTUAL.BE.CURSOS;
using PROMPERU.AULAVIRTUAL.BE.USUARIO;
using PROMPERU.AULAVIRTUAL.BL;
using PROMPERU.AULAVIRTUAL.Helpers;
using PROMPERU.AULAVIRTUAL.WEB.Filters;
using PROMPERU.AULAVIRTUAL.WEB.Helpers;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;


namespace PROMPERU.AULAVIRTUAL.WEB.Controllers
{
    public class LinkController : BaseController
    {
        // GET: EnlacesInteres
        EnlacesInteresBL EnlacesInteresBL = new EnlacesInteresBL();
        [AppAuthorize(AppRol.Administrador)]
        public ActionResult ListLinkAdmin()
        {
            List<EnlacesInteres> items = EnlacesInteresBL.ListarEnlacesInteres();
            return View(items);
        }
        public ActionResult ListLink()
        {
            List<EnlacesInteres> items = EnlacesInteresBL.ListarEnlacesInteres().Where(x => x.Estado == ConstantHelpers.ESTADO.ACTIVO).ToList();
            return View(items);
        }
        [AppAuthorize(AppRol.Administrador)]
        public ActionResult AddEditEnlacesInteres(int id)
        {
            EnlacesInteres item = EnlacesInteresBL.ObtenerEnlacesInteresPorEnlacesInteresId(id) ?? new EnlacesInteres();

            return View(item);
        }

        [HttpPost, AppAuthorize(AppRol.Administrador)]
        public ActionResult AddEditEnlacesInteres(EnlacesInteres model, HttpPostedFileBase fileImagen)
        {
            try
            {
                EnlacesInteres item = EnlacesInteresBL.ObtenerEnlacesInteresPorEnlacesInteresId(model.EnlaceInteresId) ?? new EnlacesInteres();

                if (fileImagen != null && fileImagen.ContentLength > 0)
                {
                    string ruta = "~/Content/CMS/Banner/" + Guid.NewGuid().ToString() + "_" + fileImagen.FileName;
                    fileImagen.SaveAs(Server.MapPath(ruta));
                    item.Imagen = ruta;
                }
                else {
                    if (item.Imagen == null)
                    {
                        PostMessage(MessageType.Error, "Debes adjuntar una imagen");

                        return AddEditEnlacesInteres(model.EnlaceInteresId);
                    }                    
                }

                item.Titulo = model.Titulo;
                //item.Imagen = model.Imagen;
                item.Descripcion = model.Descripcion;
                //item.Tipo = "ART";
                item.Tipo = model.Tipo;

                switch (model.Tipo)
                {
                    case "ART":
                        item.Url = model.Url;
                        break;
                    case "PDF":
                        item.Pdf = model.Pdf;
                        break;
                    case "VID":
                        item.CodigoYoutube = model.CodigoYoutube;
                        break;
                }

                item.Estado = ConstantHelpers.ESTADO.ACTIVO;
                item.FechaEdicion = DateTime.Now;
                item.UsuarioEdicionId = Session.GetUsuarioId();

                if (model.EnlaceInteresId > 0)
                    EnlacesInteresBL.ActualizarEnlacesInteres(item);
                else
                    EnlacesInteresBL.InsertarEnlacesInteres(item);

                PostMessage(MessageType.Success);

                return RedirectToAction("ListLinkAdmin");
            }
            catch (Exception ex)
            {
                PostMessage(MessageType.Warning, "Ha ocurrido un error al procesar la operación: " + ex.Message);
                return AddEditEnlacesInteres(model.EnlaceInteresId);
            }
        }


        [AppAuthorize(AppRol.Administrador, AppRol.Supervisor)]
        public ActionResult ChangeStateEnlacesInteres(Int32 id)
        {
            try
            {
                EnlacesInteres EnlacesInteres = EnlacesInteresBL.ObtenerEnlacesInteresPorEnlacesInteresId(id);

                EnlacesInteres.Estado = EnlacesInteres.Estado.Equals(ConstantHelpers.ESTADO.ACTIVO) ? ConstantHelpers.ESTADO.INACTIVO : ConstantHelpers.ESTADO.ACTIVO;
                EnlacesInteresBL.ActualizarEnlacesInteresEstado(id, EnlacesInteres.Estado);

                //context.SaveChanges();
                PostMessage(MessageType.Success);
            }
            catch (Exception e)
            {
                Util.SaveErrorLog(e);
                //InvalidarContext();
                PostMessage(MessageType.Error);
            }
            return RedirectToAction("ListLinkAdmin");
        }
    }
}