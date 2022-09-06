using PROMPERU.AULAVIRTUAL.BE.Aesorias;
using PROMPERU.AULAVIRTUAL.BL;
using PROMPERU.AULAVIRTUAL.WEB.Filters;
using PROMPERU.AULAVIRTUAL.WEB.Helpers;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web.Mvc;

namespace PROMPERU.AULAVIRTUAL.WEB.Controllers
{
    public class ConferenciaController : BaseController
    {
        AsesoriaAsesoradoBL asesoriaAsesoradoBL = new AsesoriaAsesoradoBL();
        AsesoriaAsesorBL asesoriaAsesorBL = new AsesoriaAsesorBL();

        [AppAuthorize(AppRol.Alumno, AppRol.Supervisor, AppRol.Administrador, AppRol.Empresa, AppRol.Ingeniero, AppRol.Proveedor, AppRol.Asesor)]
        public ActionResult Index(Guid RoomName)
        {
            int UsuarioId = DatosContext.Session.GetUsuarioId();

            DateTime ini = DateTime.Now;
            DateTime fin = DateTime.Now;


            List<AsesoriaAsesoradoSolicitud> asesol = asesoriaAsesoradoBL.ListarAsesoriaSolicitudes(UsuarioId).Where(x => x.Asre_Inicio >= DateTime.Now.AddDays(-1)).ToList();
            if (asesol.Any(x => x.Asre_ConferenciaId == RoomName.ToString()))
            {
                AsesoriaAsesoradoSolicitud aseso = asesol.First(x => x.Asre_ConferenciaId == RoomName.ToString());

                ini = aseso.Asre_Inicio;
                fin = aseso.Asre_Fin;
            }

            List<AsesoriaAsesorSolicitud> asesol2 = asesoriaAsesorBL.ListarSolicitudPorAsesor(UsuarioId).Where(x => x.Asre_Inicio >= DateTime.Now.AddDays(-1)).ToList();
            if (asesol2.Any(x => x.Asre_ConferenciaId == RoomName))
            {
                AsesoriaAsesorSolicitud aseso = asesol2.First(x => x.Asre_ConferenciaId == RoomName);

                ini = aseso.Asre_Inicio;
                fin = aseso.Asre_Fin;
            }

            ViewBag.TiempoIni = ini;
            ViewBag.TiempoFin = fin;

            TimeSpan ts = fin - ini;

            ViewBag.Duration = ts.TotalSeconds;
            ViewBag.DurationMilliseconds = ts.TotalMilliseconds;

            ViewBag.RoomName = RoomName;
            return View("Index");
        }



    }
}
