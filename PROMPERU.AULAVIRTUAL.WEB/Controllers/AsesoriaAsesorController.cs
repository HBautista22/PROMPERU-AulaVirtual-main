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
    public class AsesoriaAsesorController : BaseController
    {
        AsesoriaAsesorBL asesoriaAsesorBL = new AsesoriaAsesorBL();

        [AppAuthorize(AppRol.Asesor)]
        public ActionResult Index()
        {
            AsesoriaAsesorViewModel model = new AsesoriaAsesorViewModel();
            int UsuarioId = DatosContext.Session.GetUsuarioId();
            ViewBag.UsuarioId = UsuarioId;

            model.AsesoriasSolicitadas = asesoriaAsesorBL.ListarSolicitudPorAsesor(UsuarioId).Where(x => x.Asre_Fin >= DateTime.Now).ToList();

            return View("IndexNew", model);
        }

        public ContentResult AsesoriasRemotasListar(RequestCalendar cal)
        {
            List<CalendarItemViewModel> retVal = new List<CalendarItemViewModel>();
            int UsuarioId = DatosContext.Session.GetUsuarioId();
            List<AsesoriaRemota> listaAsesoriaRemota = asesoriaAsesorBL.ListarAsesoriaPorAsesor(UsuarioId);

            listaAsesoriaRemota = listaAsesoriaRemota.Where(x => x.Asre_Inicio >= cal.start && x.Asre_Fin <= cal.end).ToList();

            foreach (AsesoriaRemota asr in listaAsesoriaRemota)
            {
                CalendarItemViewModel calm = new CalendarItemViewModel();

                calm.title = asr.Asre_Nombre;
                calm.start = asr.Asre_Inicio;
                calm.end = asr.Asre_Fin;
                calm.url = @"javascript:AnularAsesoria(" + asr.Asre_Id + @");";
                calm.backgroundColor = asr.Sola_Estado == "ACE" ? "#ff5e5e" : "";

                retVal.Add(calm);
            }

            return Content(JsonConvert.SerializeObject(retVal), "application/json");
        }

        public ContentResult AsesoriasRemotasAdd(AsesoriaRemota data)
        {


            RespuestaJsonViewModel retVal = new RespuestaJsonViewModel();
            int UsuarioId = DatosContext.Session.GetUsuarioId();
            data.Asre_AsesorId = UsuarioId;

            data.Asre_Fin = data.Asre_Inicio.AddMinutes(60);

            if (data.Asre_Inicio.Hour < 09 || data.Asre_Fin.Hour > 19)
            {
                retVal.Estado = "Error";
                retVal.Informacion = "Horario Invalido";

                return Content(JsonConvert.SerializeObject(retVal), "application/json");
            }
            try
            {
                asesoriaAsesorBL.SaveAsesoriaAsesor(data);
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

        public ContentResult AsesoriasRemotasAddDetallado(AsesoriaRemota data)
        {


            RespuestaJsonViewModel retVal = new RespuestaJsonViewModel();
            int UsuarioId = DatosContext.Session.GetUsuarioId();
            data.Asre_AsesorId = UsuarioId;

            data.Asre_Fin = data.Asre_Inicio.AddMinutes(data.Asre_Duracion);

            if (data.Asre_Inicio.Hour < 09 || data.Asre_Fin.Hour > 19)
            {
                retVal.Estado = "Error";
                retVal.Informacion = "Horario Invalido";

                return Content(JsonConvert.SerializeObject(retVal), "application/json");
            }
            try
            {
                asesoriaAsesorBL.SaveAsesoriaAsesor(data);
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

        public ContentResult AsesoriasRemotasAddBloque(AsesoriaRemota data)
        {
            RespuestaJsonViewModel retVal = new RespuestaJsonViewModel();
            int UsuarioId = DatosContext.Session.GetUsuarioId();

            DateTime InicioBloque = new DateTime(data.Asre_Inicio.Year, data.Asre_Inicio.Month, data.Asre_Inicio.Day, 9, 0, 0);
            DateTime FinBloque = new DateTime(data.Asre_Fin.Year, data.Asre_Fin.Month, data.Asre_Fin.Day, 18, 0, 0);

            data.Asre_AsesorId = UsuarioId;
            data.Asre_Fin = data.Asre_Inicio.AddMinutes(data.Asre_Duracion);

            DateTime PartBloqueInicio = InicioBloque;
            DateTime PartBloqueFin = InicioBloque.AddMinutes(data.Asre_Duracion);

            bool ErrorFound = false;

            AsesoriaRemota ARBloque = data;

            ARBloque.Asre_Inicio = PartBloqueInicio;
            ARBloque.Asre_Fin = PartBloqueFin;

            while (ARBloque.Asre_Fin <= FinBloque)
            {
                if (ARBloque.Asre_Inicio.Hour >= 9 && ARBloque.Asre_Fin.AddSeconds(-1).Hour < 18)
                {
                    try
                    {
                        asesoriaAsesorBL.SaveAsesoriaAsesor(ARBloque);

                    }
                    catch (Exception ex)
                    {
                        ErrorFound = true;
                    }
                }
                ARBloque.Asre_Inicio = ARBloque.Asre_Inicio.AddMinutes(data.Asre_Duracion);
                ARBloque.Asre_Fin = ARBloque.Asre_Fin.AddMinutes(data.Asre_Duracion);
            }

            retVal.Estado = "OK";
            retVal.Informacion = "" + (ErrorFound ? ("WithErrors") : "");

            return Content(JsonConvert.SerializeObject(retVal), "application/json");
        }

        public ContentResult AnularAsesoria(int AsesoriaId)
        {
            RespuestaJsonViewModel retVal = new RespuestaJsonViewModel();
            int UsuarioId = DatosContext.Session.GetUsuarioId();

            try
            {
                int ret = asesoriaAsesorBL.ActualizarEstadoAsesoria(UsuarioId, AsesoriaId, "ELI");

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

        public ContentResult AceptarSolicitud(int SolicitudId)
        {
            RespuestaJsonViewModel retVal = new RespuestaJsonViewModel();
            int UsuarioId = DatosContext.Session.GetUsuarioId();



            try
            {
                int ret = asesoriaAsesorBL.ActualizarEstadoSolicitud(UsuarioId, SolicitudId, "ACE", "");

                if (ret == 0)
                {
                    retVal.Estado = "Error";
                    retVal.Informacion = "Ocurrio un error en la operacion.";

                    return Content(JsonConvert.SerializeObject(retVal), "application/json");
                }

                AsesoriasSolicitudCalendar asics = asesoriaAsesorBL.ObtenerDatosSolicitudCalendar(SolicitudId);


                MailSMTP mail = new MailSMTP();
                mail.SendCalendar(asics.AsesorEmail, asics.AsesorName, "Asesoria", "Esta programada la asesoria.", asics.AsesoriaInicio, asics.AsesoriaFin, asics.TituloAsesoria);
                mail.SendCalendar(asics.AsesoradoEmail, asics.AsesoradoName, "Asesoría confirmada", "Esta programada la asesoria.", asics.AsesoriaInicio, asics.AsesoriaFin, asics.TituloAsesoria);



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

        public ContentResult RechazarSolicitud(int SolicitudId, string Comentario)
        {
            RespuestaJsonViewModel retVal = new RespuestaJsonViewModel();
            int UsuarioId = DatosContext.Session.GetUsuarioId();

            try
            {
                int ret = asesoriaAsesorBL.ActualizarEstadoSolicitud(UsuarioId, SolicitudId, "REC", Comentario);

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

        public ContentResult TerminarSolicitud(int SolicitudId)
        {
            RespuestaJsonViewModel retVal = new RespuestaJsonViewModel();
            int UsuarioId = DatosContext.Session.GetUsuarioId();



            try
            {
                int ret = asesoriaAsesorBL.ActualizarEstadoSolicitud(UsuarioId, SolicitudId, "ATE", "");

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
