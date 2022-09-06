using PROMPERU.AULAVIRTUAL.WEB.Helpers;
using PROMPERU.AULAVIRTUAL.WEB.Logic;
using PROMPERU.AULAVIRTUAL.WEB.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Transactions;
using System.Web;
using System.Web.Mvc;
using PROMPERU.AULAVIRTUAL.WEB.Filters;
using PROMPERU.AULAVIRTUAL.BE.CURSOS;
using PROMPERU.AULAVIRTUAL.BL;
using PROMPERU.AULAVIRTUAL.BE.PARAMETROS;

namespace PROMPERU.AULAVIRTUAL.WEB.Controllers
{
    [AppAuthorize(AppRol.Administrador, AppRol.Empresa, AppRol.Alumno, AppRol.Supervisor, AppRol.Proveedor, AppRol.Ingeniero)]
    public class JsonController : BaseController
    {

        JsonBL jsonBL = new JsonBL();

        [HttpPost]
        public JsonResult GetProvinciasPorDepartamento(Int32? DepartamentoId)
        {

            //var lstProvincia = context.Provincia.Where(x => x.DepartamentoId == DepartamentoId && x.EstadoId == 3).Select(x => new
            //{
            //    x.ProvinciaId,
            //    x.Nombre
            //});
            List<Provincia> lstProvincia = jsonBL.ListarProvinciaPorDepartamentoPorEstado(DepartamentoId, 3).ToList();
            lstProvincia = lstProvincia.Where(x => x.DepartamentoId == DepartamentoId && x.EstadoId == 3).ToList();


            return Json(lstProvincia, JsonRequestBehavior.AllowGet);
        }

        [HttpPost]
        public JsonResult GetDistritoPorProvincia(Int32? ProvinciaId)
        {

            //var lstDistrito = context.Distrito.Where(x => x.ProvinciaId == ProvinciaId && x.EstadoId == 3).Select(x => new
            //{
            //    x.DistritoId,
            //    x.Nombre
            //});
            List<Distrito> lstDistrito = jsonBL.ListarDistritoPorProvinciaPorEstado(ProvinciaId, 3).ToList();
            lstDistrito = lstDistrito.Where(x => x.ProvinciaId == ProvinciaId && x.EstadoId == 3).ToList();

            return Json(lstDistrito, JsonRequestBehavior.AllowGet);
        }

        [HttpPost]
        public JsonResult PushUnidadCursoOnline(String UnidadCursoOnlineIdEnc, String MatriculaCursoOnlineIdEnc)
        {
            var data = false;
            try
            {
                var encriptiacion = new Encriptacion();
                var matriculaCursoOnlineId = encriptiacion.Desencriptar(MatriculaCursoOnlineIdEnc).ToInteger();
                var unidadCursoOnlineId = encriptiacion.Desencriptar(UnidadCursoOnlineIdEnc).ToInteger();

                using (var ts = new TransactionScope())
                {
                    var usuarioId = Session.GetUsuarioId();

                    var logUnidad = new LogUnidad();
                    logUnidad.FechaRegistro = DateTime.Now;
                    logUnidad.MatriculaCursoOnlineId = matriculaCursoOnlineId;
                    logUnidad.UnidadCursoOnlineId = unidadCursoOnlineId;
                    logUnidad.UsuarioId = usuarioId;
                    logUnidad.Estado = "PUSH";
                    //context.LogUnidad.Add(logUnidad);
                    //context.SaveChanges();
                    jsonBL.InsertarLogUnidad(logUnidad);

                    

                    ts.Complete();
                }
                data = true;
            }
            catch (Exception ex)
            {


            }
            return Json(data, JsonRequestBehavior.AllowGet);
        }

    }
}
