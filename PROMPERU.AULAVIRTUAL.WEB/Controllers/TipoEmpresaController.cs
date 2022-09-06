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
    public class TipoEmpresaController : BaseController
    {
        TipoEmpresaBL tipoEmpresaBL = new TipoEmpresaBL();
        [AppAuthorize(AppRol.Administrador)]
        public ActionResult ListTipoEmpresaAdmin()
        {
            var items = tipoEmpresaBL.ListarTipoEmpresa();
            return View(items);
        }

        [AppAuthorize(AppRol.Administrador)]
        public ActionResult AddEditTipoEmpresa(Int32 id)
        {
            var item = tipoEmpresaBL.ObtenerTipoEmpresaPorTipoEmpresaId(id);
            if (item == null)
            {
                item = new TipoEmpresa();
            }
            return View(item);
        }

        [HttpPost, AppAuthorize(AppRol.Administrador)]
        public ActionResult AddEditTipoEmpresa(TipoEmpresa model)
        {
            try
            {
                var item = tipoEmpresaBL.ObtenerTipoEmpresaPorTipoEmpresaId(model.TipoEmpresaId);

                if (item == null)
                {
                    item = new TipoEmpresa();
                   
                }

                item.Nombre = model.Nombre;
                item.Estado = ConstantHelpers.ESTADO.ACTIVO;

                if (item.TipoEmpresaId > 0)
                    tipoEmpresaBL.ActualizarTipoEmpresa(item);
                else
                    tipoEmpresaBL.InsertarTipoEmpresa(item);


                PostMessage(Helpers.MessageType.Success);
                return RedirectToAction("ListTipoEmpresaAdmin");
            }
            catch (Exception ex)
            {
                PostMessage(Helpers.MessageType.Warning, "Ha ocurrido un error al procesar la operación: " + ex.Message);
                return AddEditTipoEmpresa(model.TipoEmpresaId);
            }
        }

   
        [AppAuthorize(AppRol.Administrador, AppRol.Supervisor)]
        public ActionResult ChangeStateTipoEmpresa(Int32 id)
        {
            try
            {
                TipoEmpresa tipoEmpresa =  tipoEmpresaBL.ObtenerTipoEmpresaPorTipoEmpresaId(id);

                tipoEmpresa.Estado = tipoEmpresa.Estado.Equals(ConstantHelpers.ESTADO.ACTIVO) ? ConstantHelpers.ESTADO.INACTIVO : ConstantHelpers.ESTADO.ACTIVO;
                tipoEmpresaBL.ActualizarTipoEmpresaEstado(id, tipoEmpresa.Estado);

                //context.SaveChanges();
                PostMessage(MessageType.Success);
            }
            catch (Exception e)
            {
                Util.SaveErrorLog(e);
                //InvalidarContext();
                PostMessage(MessageType.Error);
            }
            return RedirectToAction("ListTipoEmpresaAdmin");
        }
    }
}