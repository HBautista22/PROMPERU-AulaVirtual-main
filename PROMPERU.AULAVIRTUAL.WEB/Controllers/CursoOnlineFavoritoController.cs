using Newtonsoft.Json;
using PROMPERU.AULAVIRTUAL.BE.Aesorias;
using PROMPERU.AULAVIRTUAL.BL;
using PROMPERU.AULAVIRTUAL.WEB.Filters;
using PROMPERU.AULAVIRTUAL.WEB.Helpers;
using PROMPERU.AULAVIRTUAL.WEB.Logic;
using PROMPERU.AULAVIRTUAL.WEB.ViewModel.Asesoria;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web.Mvc;

namespace PROMPERU.AULAVIRTUAL.WEB.Controllers
{
    public class CursoOnlineFavoritoController : BaseController
    {
     

        CursoOnlineFavoritoBL cursoOnlineFavoritoBL = new CursoOnlineFavoritoBL();
        public ContentResult favorito(int CursoOnlineId, bool estadoFavorito)
        {
            RespuestaJsonViewModel retVal = new RespuestaJsonViewModel();
            int UsuarioId = DatosContext.Session.GetUsuarioId();

            try
            {
                int ret = cursoOnlineFavoritoBL.ActualizarFavorito(UsuarioId, CursoOnlineId, estadoFavorito);

                if (ret == 0)
                {
                    retVal.Estado = "Error";
                    retVal.Informacion = "Ocurrio un error en la operacion.";

                    return Content(JsonConvert.SerializeObject(retVal), "application/json");
                }


            }
            catch (Exception ex)
            {
                retVal.Estado = "Error";
                retVal.Informacion = ex.Message;

                return Content(JsonConvert.SerializeObject(retVal), "application/json");
            }

            retVal.Estado = "OK";
            retVal.Informacion = "";

            return Content(JsonConvert.SerializeObject(retVal), "application/json");
        }
    }
}