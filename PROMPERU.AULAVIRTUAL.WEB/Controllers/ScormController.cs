using PROMPERU.AULAVIRTUAL.WEB.Helpers;
using PROMPERU.AULAVIRTUAL.WEB.Logic;
using PROMPERU.AULAVIRTUAL.WEB.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace PROMPERU.AULAVIRTUAL.WEB.Controllers
{
    public class ScormController : BaseController
    {
        //[HttpPost]
        //public ContentResult GetValue(String Variable, Int32 UnidadCursoOnlineId, Int32? MatriculaCursoOnlineId )
        //{
        //    try
        //    {
        //        //var usuarioId = Session.GetUsuarioId();

        //        var parametro = context.ParametroScorm.FirstOrDefault(x=>/*x.UsuarioId == usuarioId &&*/ x.UnidadCursoOnlineId == UnidadCursoOnlineId && x.Parametro == Variable && x.MatriculaCursoOnlineId == MatriculaCursoOnlineId);

        //        if (parametro == null)
        //            return Content(String.Empty);

        //        return Content(parametro.Valor);
        //    }
        //    catch (Exception ex)
        //    {
        //        return Content("Sucedió un error en GetValue");
        //        throw;
        //    }

        //}

        //[HttpPost]
        //public ActionResult SetValue(String Variable, String Value, Int32 UnidadCursoOnlineId, Int32? MatriculaCursoOnlineId)
        //{
        //    try
        //    {
        //        //var usuarioId = Session.GetUsuarioId();
        //        var unidadCursoOnline = context.UnidadCursoOnline.Find(UnidadCursoOnlineId);
        //        var parametro = context.ParametroScorm.FirstOrDefault(x =>/* x.UsuarioId == usuarioId &&*/ x.UnidadCursoOnlineId == UnidadCursoOnlineId && x.Parametro == Variable && x.MatriculaCursoOnlineId == MatriculaCursoOnlineId);
        //        var matricula = context.MatriculaCursoOnline.Find(MatriculaCursoOnlineId);
        //        var avanceUnidadLogic = new AvanceUnidadLogic(context);

        //        if (parametro == null)
        //        {
        //            parametro = new ParametroScorm();
        //            parametro.UsuarioId = matricula.UsuarioId;
        //            parametro.UnidadCursoOnlineId = UnidadCursoOnlineId;
        //            parametro.MatriculaCursoOnlineId = MatriculaCursoOnlineId;
        //            parametro.Parametro = Variable;
        //            context.ParametroScorm.Add(parametro);
        //        }

        //        parametro.Valor = Value;
        //        context.SaveChanges();

        //        if (ConstantHelpers.CMI.CORE.LESSON_STATUS == Variable && (Value == ConstantHelpers.STATUS_PASSED || Value == ConstantHelpers.STATUS_COMPLETED) && unidadCursoOnline.TipoUnidad == ConstantHelpers.TIPO_UNIDAD.SCORM)
        //        {
        //            var matriculaOnline = context.MatriculaCursoOnline.Where(x => x.MatriculaCursoOnlineId == MatriculaCursoOnlineId).FirstOrDefault();
        //            avanceUnidadLogic.UpdateAvanceCursoOnline(matriculaOnline.MatriculaCursoOnlineId, UnidadCursoOnlineId);

        //            var logUnidad = new LogUnidad();
        //            logUnidad.FechaRegistro = DateTime.Now;
        //            logUnidad.MatriculaCursoOnlineId = matriculaOnline.MatriculaCursoOnlineId;
        //            logUnidad.UnidadCursoOnlineId = UnidadCursoOnlineId;
        //            logUnidad.UsuarioId = matriculaOnline.UsuarioId;
        //            logUnidad.Estado = ConstantHelpers.ESTADO_UNIDAD.COMPLETADO;
        //            context.LogUnidad.Add(logUnidad);
        //            context.SaveChanges();

        //            return Json(new { finalizado = true });
        //        }
        //        return Json(new { mensaje = String.Empty });

        //    }
        //    catch (Exception ex)
        //    {
        //        return Json(new { error = "Sucedió un error en SetValue"});
        //    }

        //}
    }
}
