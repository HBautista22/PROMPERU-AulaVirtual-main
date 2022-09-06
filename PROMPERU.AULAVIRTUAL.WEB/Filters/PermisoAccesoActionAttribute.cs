using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using PROMPERU.AULAVIRTUAL.WEB.Helpers;
using System.Web.Mvc;
using System.Web.Routing;
using PROMPERU.AULAVIRTUAL.WEB.Logic;

namespace PROMPERU.AULAVIRTUAL.WEB.Filters
{
    public enum PermisoInsuficienteResultado
    {
        Modal,
        Vista
    }

    #region Templates
        /*
        public class PermisoAccesoExperienciaExitosaAttribute : ActionFilterAttribute
        {
            private String _parametero;
            private PermisoInsuficienteResultado _vistaPermisoInsuficiente;
            private PermisoAccesoLogic.TipoPermiso _tipoPermiso;
            private Int32 _faseId;

            public PermisoAccesoExperienciaExitosaAttribute(String parametero, PermisoAccesoLogic.TipoPermiso tipoPermiso, Int32 faseId = -1, PermisoInsuficienteResultado vistaPermisoInsuficiente = PermisoInsuficienteResultado.Vista)
            {
                _parametero = parametero;
                _tipoPermiso = tipoPermiso;
                _vistaPermisoInsuficiente = vistaPermisoInsuficiente;
                _faseId = faseId;
            }

            public override void OnActionExecuting(ActionExecutingContext filterContext)
            {
                var authorized = false;

                try
                {
                    var usuarioId = filterContext.HttpContext.Session.GetUsuarioId();
                    var experienciaExitosaId = filterContext.RequestContext.HttpContext.Request.Unvalidated[_parametero].ToInteger();

                    var permisoLogic = new PermisoAccesoLogic();
                    var permiso = permisoLogic.GetPermisoExperienciaExitosa(usuarioId, experienciaExitosaId, _faseId);

                    if (permisoLogic.TienePermisoSuficiente(permiso, _tipoPermiso))
                        authorized = true;
                }
                catch (Exception ex)
                {
                    authorized = false;
                }

                if (!authorized)
                {
                    var viewName = "";
                    switch (_vistaPermisoInsuficiente)
                    {
                        case PermisoInsuficienteResultado.Vista: viewName = "PermisoInsuficiente"; break;
                        case PermisoInsuficienteResultado.Modal: viewName = "_PermisoInsuficienteModal"; break;
                    }

                    filterContext.Result = new ViewResult() { ViewName = viewName };
                }
            }
        } 
        */
    #endregion
}