using Newtonsoft.Json;
using PROMPERU.AULAVIRTUAL.BE.Aesorias;
using PROMPERU.AULAVIRTUAL.BL;
using PROMPERU.AULAVIRTUAL.WEB.Filters;
using PROMPERU.AULAVIRTUAL.WEB.Helpers;
using PROMPERU.AULAVIRTUAL.WEB.ViewModel.Asesoria;
using PROMPERU.AULAVIRTUAL.WEB.Logic;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web.Mvc;
using PROMPERU.AULAVIRTUAL.BE.USUARIO;

namespace PROMPERU.AULAVIRTUAL.WEB.Controllers
{
    public class AsesoriaAsesoradoController : BaseController
    {
        AsesoriaAsesoradoBL asesoriaAsesoradoBL = new AsesoriaAsesoradoBL();
        UsuarioBL usuarioBL = new UsuarioBL();

        #region PAGE OPS

        [AppAuthorize(AppRol.Alumno, AppRol.Empresa)]
        public ActionResult Index()
        {
            int UsuarioId = DatosContext.Session.GetUsuarioId();

            List<AsesoriaAsesoradoSolicitud> asesol = asesoriaAsesoradoBL.ListarAsesoriaSolicitudes(UsuarioId).Where(x => x.Asre_Fin >= DateTime.Now.AddHours(-2)).ToList();

            AsesoriaAsesoradoViewModel model = new AsesoriaAsesoradoViewModel();
            model.AsesoriasSolicitadas = asesol;

            return View("IndexNew", model);




        }

        [HttpGet]
        public ActionResult CalificacionAsesoria(int AsesoriaId)
        {
            int UsuarioId = DatosContext.Session.GetUsuarioId();
            //TODO
            //Obtener la Asesoria para mostrar
            AsesoriaRemota ase = asesoriaAsesoradoBL.ObtenerAsesoriaCompletada(AsesoriaId, UsuarioId);

            AsesoriaCalificacionViewModel model = new AsesoriaCalificacionViewModel();
            model.AsesoriaId = AsesoriaId;
            model.AsesoriaNombre = ase.Asre_Nombre;

            return View("CalificacionAsesoria", model);

        }

        [HttpPost]
        public ActionResult CalificacionAsesoria(RequestAsesoriaCalificacion calif)
        {
            int UsuarioId = DatosContext.Session.GetUsuarioId();
            //TODO
            //Obtener la Asesoria para mostrar
            AsesoriaRemota ase = asesoriaAsesoradoBL.ObtenerAsesoriaCompletada(calif.AsesoriaId, UsuarioId);

            AsesoriaCalificacionViewModel model = new AsesoriaCalificacionViewModel();
            model.AsesoriaId = calif.AsesoriaId;
            model.AsesoriaNombre = ase.Asre_Nombre;


            try
            {
                //TODO
                //Calificar la asesoria
                int ret = asesoriaAsesoradoBL.CalificarAsesoriaCompletada(calif.AsesoriaId, calif.Calificacion, UsuarioId);
                if (ret == 0)
                {

                }

                //TODO
                //Enviar correo
            }
            catch (Exception ex)
            {

            }


            return View("CalificacionAsesoria", model);

        }

        #endregion

        #region XHTTP 

        public ContentResult AsesoriasRemotasListarBloques(RequestCalendar cal)
        {
            List<CalendarItemViewModel> retVal = new List<CalendarItemViewModel>();
            int UsuarioId = DatosContext.Session.GetUsuarioId();
            List<AsesoriaRemota> listaAsesoriaRemota = asesoriaAsesoradoBL.ListarAsesoriaPorAsesoradoBloques(UsuarioId, cal.start, cal.end);



            foreach (AsesoriaRemota asr in listaAsesoriaRemota)
            {
                CalendarItemViewModel calm = new CalendarItemViewModel();

                calm.title = "Solicitar";
                calm.start = asr.Asre_Inicio;
                calm.end = asr.Asre_Fin;
                calm.url = @"javascript:MostrarOpciones('" + asr.Asre_Inicio.ToString("yyyy-MM-dd HH:mm:ss") + @"','" + asr.Asre_Fin.ToString("yyyy-MM-dd HH:mm:ss") + @"');";

                retVal.Add(calm);
            }

            return Content(JsonConvert.SerializeObject(retVal), "application/json");
        }

        public ContentResult AsesoriasRemotasListarDisponible(RequestCalendar cal)
        {
            List<AsesoriaAsesor> retVal = new List<AsesoriaAsesor>();
            int UsuarioId = DatosContext.Session.GetUsuarioId();
            retVal = asesoriaAsesoradoBL.ListarAsesoriaPorAsesoradoDisponible(UsuarioId, cal.start);

            return Content(JsonConvert.SerializeObject(retVal), "application/json");
        }

        public ContentResult SolicitarAsesoria(RequestAsesoriaSolicitud data)
        {
            RespuestaJsonViewModel retVal = new RespuestaJsonViewModel();
            int UsuarioId = DatosContext.Session.GetUsuarioId();
            data.AsesoradoId = UsuarioId;

            try
            {
                int ret = asesoriaAsesoradoBL.SolicitarAsesoria(data.AsesoriaId, data.AsesoradoId, data.AsesoriaConsulta);
                if (ret == 0)
                {
                    retVal.Estado = "Error";
                    retVal.Informacion = "Ocurrio un error en la operacion.";

                    return Content(JsonConvert.SerializeObject(retVal), "application/json");
                }

                List<AsesoriaAsesoradoSolicitud> asesol = asesoriaAsesoradoBL.ListarAsesoriaSolicitudes(UsuarioId).Where(x => x.Asre_Fin >= DateTime.Now).ToList();

                AsesoriaAsesoradoSolicitud aseSolSelecionada = asesol.First(x => x.Asre_Id == data.AsesoriaId);

                int AsesorId = aseSolSelecionada.Asre_AsesorId;

                Usuario asesor = usuarioBL.ObtenerUsuarioPorId(AsesorId);


                MailSMTP mail = new MailSMTP();
                mail.SendSingleEmail(asesor.Email, "Solicitud de Asesoria Nueva", "Han solicitado una Asesoría para la fecha: " + aseSolSelecionada.Asre_Inicio);

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

        public ContentResult AnularSolicitud(int SolicitudId)
        {
            RespuestaJsonViewModel retVal = new RespuestaJsonViewModel();
            int UsuarioId = DatosContext.Session.GetUsuarioId();



            try
            {
                int ret = asesoriaAsesoradoBL.ActualizarEstadoSolicitud(UsuarioId, SolicitudId, "CAN");

                if (ret == 0)
                {
                    retVal.Estado = "Error";
                    retVal.Informacion = "Ocurrio un error en la operacion.";

                    return Content(JsonConvert.SerializeObject(retVal), "application/json");
                }


                //TODO
                //Enviar correo
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

        #endregion

    }
}
