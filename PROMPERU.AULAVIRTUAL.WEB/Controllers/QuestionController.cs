using PROMPERU.AULAVIRTUAL.WEB.Filters;
using PROMPERU.AULAVIRTUAL.WEB.Helpers;
using PROMPERU.AULAVIRTUAL.WEB.Models;
using PROMPERU.AULAVIRTUAL.WEB.ViewModel.Question;
using PROMPERU.AULAVIRTUAL.BE.CURSOS;
using PROMPERU.AULAVIRTUAL.BL;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Transactions;
using System.Web;
using System.Web.Mvc;

namespace PROMPERU.AULAVIRTUAL.WEB.Controllers
{
    public class QuestionController : BaseController
    {
        //
        // GET: /Question/
        CursosBL cursosBL = new CursosBL();


        [AppAuthorize(AppRol.Administrador, AppRol.Profesor)]
        public ActionResult ManagePreguntasCursoOnline(Int32 unidadOnlineId)
        {
            var viewModel = new ManagePreguntasCursoOnlineViewModel();
            viewModel.CargarDatos(DatosContext, unidadOnlineId);
            return View("ManagePreguntasCursoOnlineNew",viewModel);
        }

        [AppAuthorize(AppRol.Administrador, AppRol.Profesor)]
        public ActionResult _EditPreguntaCursoOnline(Int32 unidadCursoOnlineId, Int32? PreguntaCursoOnlineId)
        {
            var viewModel = new _EditPreguntaCursoOnlineViewModel();
            viewModel.CargarDatos(DatosContext, unidadCursoOnlineId, PreguntaCursoOnlineId);
            return View(viewModel);
        }
        //EditarPreguntaCursoOnlime
        [AppAuthorize(AppRol.Administrador, AppRol.Profesor)]
        public ActionResult EditarPreguntaCursoOnlime(Int32 unidadCursoOnlineId, Int32? PreguntaCursoOnlineId)
        {
            var viewModel = new _EditPreguntaCursoOnlineViewModel();
            viewModel.CargarDatos(DatosContext, unidadCursoOnlineId, PreguntaCursoOnlineId);
            return View("EditarPreguntaCursoOnlimeNew", viewModel);
        }


        [AppAuthorize(AppRol.Administrador, AppRol.Profesor)]
        public ActionResult ChangeStatePregunta(Int32 PreguntaCursoOnlineId)
        {
            try
            {
                using (var TransactionScope = new TransactionScope())
                {
                    PreguntaCursoOnline preguntaCursoOnline = cursosBL.ObtenerPreguntaCursoOnline(PreguntaCursoOnlineId);

                    preguntaCursoOnline.Estado = preguntaCursoOnline.Estado.Equals(ConstantHelpers.ESTADO.ACTIVO) ? ConstantHelpers.ESTADO.INACTIVO : ConstantHelpers.ESTADO.ACTIVO;

                    int _resp = cursosBL.ActualizarPreguntaCursoOnline(preguntaCursoOnline);
                    
                    TransactionScope.Complete();
                    PostMessage(MessageType.Success);
                    return RedirectToAction("ManagePreguntasCursoOnline", new { unidadOnlineId = preguntaCursoOnline.UnidadOnlineId });
                }
            }
            catch
            {
                //InvalidarContext();
                PostMessage(MessageType.Error);
                return RedirectToAction("ManagePreguntasCursoOnline", new { CursoOnlineId = Request.Url.ToString() });
            }
        }


        [AppAuthorize(AppRol.Administrador)]
        public ActionResult DeleteStatePregunta(int preguntaCursoOnlineId)
        {
            try
            {
                using (TransactionScope transactionScope = new TransactionScope())
                {
                    PreguntaCursoOnline preguntaCursoOnline = cursosBL
                        .ListarPreguntaCursoOnline()
                        .First(x => x.PreguntaCursoOnlineId == preguntaCursoOnlineId);

                    preguntaCursoOnline.Estado = preguntaCursoOnline.Estado.Equals(ConstantHelpers.ESTADO.INACTIVO) ? ConstantHelpers.ESTADO.ELIMINADO : ConstantHelpers.ESTADO.INACTIVO;
                    cursosBL.ActualizarPreguntaCursoOnline(preguntaCursoOnline);

                    transactionScope.Complete();
                    
                    PostMessage(MessageType.Success);

                    return RedirectToAction("ManagePreguntasCursoOnline", new { unidadOnlineId = preguntaCursoOnline.UnidadOnlineId });
                }
            }
            catch
            {
                //InvalidarContext();
                PostMessage(MessageType.Error);
                return RedirectToAction("ManagePreguntasCursoOnline", new { CursoOnlineId = Request.Url.ToString() });
            }
        }

        [HttpPost,
            ValidateAntiForgeryToken,
        AppAuthorize(AppRol.Administrador, AppRol.Profesor)]
        public ActionResult _EditPreguntaCursoOnline(_EditPreguntaCursoOnlineViewModel model, FormCollection form)
        {

            try
            {
                using (var TransactionScope = new TransactionScope())
                {
                    var esNuevo = !model.PreguntaCursoOnlineId.HasValue;
                    if (!ModelState.IsValid)
                    {
                        model.CargarDatos(DatosContext, model.UnidadOnlineId, model.PreguntaCursoOnlineId);
                        TryUpdateModel(model);
                        PostMessage(MessageType.Error, String.Empty);
                        return RedirectToAction("ManagePreguntasCursoOnline", new { CursoOnlineId = model.UnidadOnlineId });
                    }

                    PreguntaCursoOnline preguntaCursoOnline = new PreguntaCursoOnline();
                    List<RespuestaCursoOnline> lstRespuestas = new List<RespuestaCursoOnline>();
                    int _preguntaCursoOnlineId;


                    if (!esNuevo)
                    {
                        //preguntaCursoOnline = cursosBL.ListarPreguntaCursoOnline().First(x => x.PreguntaCursoOnlineId == model.PreguntaCursoOnlineId);    //context.PreguntaCursoOnline.First(x => x.PreguntaCursoOnlineId == model.PreguntaCursoOnlineId);
                         preguntaCursoOnline = cursosBL.ListarPreguntaCursoOnlinePorUnidadOnlineId(model.UnidadOnlineId).
                                          Where(x => x.PreguntaCursoOnlineId == (int)model.PreguntaCursoOnlineId).
                                          FirstOrDefault();
                        lstRespuestas = preguntaCursoOnline.RespuestaCursoOnline.ToList();  //context.RespuestaCursoOnline.Where(x => x.PreguntaCursoOnlineId == PreguntaCursoOnline.PreguntaCursoOnlineId).ToList();
                        _preguntaCursoOnlineId = preguntaCursoOnline.PreguntaCursoOnlineId;

                    }
                    else
                    {
                        //Establecer las variables por defecto 
                        
                        //context.PreguntaCursoOnline.Add(PreguntaCursoOnline);
                        preguntaCursoOnline.UnidadOnlineId = model.UnidadOnlineId;
                        preguntaCursoOnline.CursoOnlineId = cursosBL.ObtenerUnidadCursoOnlinePorId(model.UnidadOnlineId).CursoOnlineId; //context.UnidadCursoOnline.Find(model.UnidadOnlineId).CursoOnlineId;
                        preguntaCursoOnline.Tipo = model.Tipo;
                        preguntaCursoOnline.Puntaje = 1;
                        preguntaCursoOnline.NumRespuestas = 0;
                        preguntaCursoOnline.Estado = ConstantHelpers.ESTADO.ACTIVO;

                        _preguntaCursoOnlineId = cursosBL.InsertarPreguntaCursoOnline(preguntaCursoOnline);
                        
                        preguntaCursoOnline.PreguntaCursoOnlineId = _preguntaCursoOnlineId;
                        

                    }

                    //model.PreguntaCursoOnlineId = _preguntaCursoOnlineId;

                    preguntaCursoOnline.Texto = form["TXT-PRE-" + model.Tipo + "-" + model.PreguntaCursoOnlineId];


                    if (model.Tipo == ConstantHelpers.TIPO_PREGUNTA.VERDADERO_FALSO)
                    {
                        //model.PreguntaCursoOnlineId = preguntaCursoOnline.PreguntaCursoOnlineId;

                        var respuestaValue = form["PRE-" + model.Tipo + "-" + (model.PreguntaCursoOnlineId.HasValue ? model.PreguntaCursoOnlineId.ToString() : "0")].ToString();
                        if (lstRespuestas.Count() != 0)
                        {
                            foreach (var respuesta in lstRespuestas)
                            {
                                var respuestaEdit = respuesta;   //cursosBL.ListarRespuestaEvaluacionCursoOnline//context.RespuestaCursoOnline.Find(respuesta.RespuestaCursoOnlineId);
                                respuestaEdit.EsSolucion = (respuesta.Texto == respuestaValue);
                                respuestaEdit.OrdenSolucion = (respuestaEdit.EsSolucion ? 1 : (Int32?)null);

                                int res = cursosBL.ActualizarRespuestaCursoOnline(respuestaEdit);
                            }
                        }
                        else
                        {
                            for (int i = 0; i < 2; i++)
                            {
                                var RespuestaCursoOnline = new RespuestaCursoOnline();
                                RespuestaCursoOnline.PreguntaCursoOnline = preguntaCursoOnline;
                                RespuestaCursoOnline.Texto = (i == 0 ? "Verdadero" : "Falso");
                                RespuestaCursoOnline.EsSolucion = RespuestaCursoOnline.Texto == respuestaValue;
                                RespuestaCursoOnline.OrdenSolucion = (RespuestaCursoOnline.EsSolucion ? 1 : (Int32?)null);

                                //context.RespuestaCursoOnline.Add(RespuestaCursoOnline);
                                int res = cursosBL.InsertarRespuestaCursoOnline(RespuestaCursoOnline);
                            }
                        }
                        preguntaCursoOnline.NumRespuestas = 2;
                    }
                    else
                    {
                        var cantidadRelaciones = 0;
                        var txtPreguntasRelacionKey = form.AllKeys.Where(x => x.StartsWith("TXT-ENU-RES-" + model.Tipo));
                        var LstEnunciadoRelacion = new List<String>();


                        if (ConstantHelpers.TIPO_PREGUNTA.RELACIONAR == model.Tipo)
                        {
                            foreach (var txtpreguntasRelacionKey in txtPreguntasRelacionKey)
                            {
                                var respuestaValue = txtpreguntasRelacionKey.Split('-')[5].ToString();
                                LstEnunciadoRelacion.Add(respuestaValue);
                                cantidadRelaciones++;
                            }
                        }
                        else if (ConstantHelpers.TIPO_PREGUNTA.COMPLETAR == model.Tipo)
                        {
                            var preguntaTexto = preguntaCursoOnline.Texto.Split(new[] { "###" }, StringSplitOptions.None);
                            cantidadRelaciones = preguntaTexto.Length - 1;
                        }

                        var seleccionKey = "";
                        seleccionKey = form["PRE-" + model.Tipo + "-" + (model.PreguntaCursoOnlineId.HasValue ? model.PreguntaCursoOnlineId.ToString() : "0")];

                        var RespuestasKey = form.AllKeys.Where(x => x.StartsWith("TXT-RES-" + model.Tipo));
                        var OrdenRespuesta = 0;
                        foreach (var respuestaKey in RespuestasKey)
                        {
                            OrdenRespuesta++;
                            var respuestaCursoOnline = new RespuestaCursoOnline();
                            var respuestaId = respuestaKey.Split('-')[4].ToInteger();
                            var respuestaValue = respuestaKey.Split('-')[4].ToString();
                            if (respuestaId != 0)
                            {
                                respuestaCursoOnline =  preguntaCursoOnline.RespuestaCursoOnline.FirstOrDefault(x => x.RespuestaCursoOnlineId == respuestaId);  //context.RespuestaCursoOnline.Find(respuestaId);

                            }
                            else
                            {
                                respuestaCursoOnline.PreguntaCursoOnline = preguntaCursoOnline;
                                respuestaCursoOnline.PreguntaCursoOnlineId = preguntaCursoOnline.PreguntaCursoOnlineId;



                                //context.RespuestaCursoOnline.Add(respuestaCursoOnline);

                            }

                            if (respuestaId != 0)
                            {
                                respuestaCursoOnline.Texto = (model.Tipo == ConstantHelpers.TIPO_PREGUNTA.RELACIONAR ? form["TXT-ENU-RES-" + model.Tipo + "-" + preguntaCursoOnline.PreguntaCursoOnlineId + "-" + respuestaValue] + "###" + form[respuestaKey] :
                                                        form[respuestaKey]);
                            }
                            else {
                                respuestaCursoOnline.Texto = (model.Tipo == ConstantHelpers.TIPO_PREGUNTA.RELACIONAR ? form["TXT-ENU-RES-" + model.Tipo + "-0-" + respuestaValue] + "###" + form[respuestaKey] :
                                                        form[respuestaKey]);
                            }
                            


                            respuestaCursoOnline.EsSolucion = (model.Tipo == ConstantHelpers.TIPO_PREGUNTA.RELACIONAR ? LstEnunciadoRelacion.Contains(respuestaValue) :
                                                                model.Tipo == ConstantHelpers.TIPO_PREGUNTA.COMPLETAR ? cantidadRelaciones >= OrdenRespuesta :
                                                                seleccionKey.Contains(respuestaValue) ? true : false);
                            respuestaCursoOnline.OrdenSolucion = (model.Tipo == ConstantHelpers.TIPO_PREGUNTA.RELACIONAR ? (respuestaCursoOnline.EsSolucion ? OrdenRespuesta : (Int32?)null) :
                                                                    respuestaCursoOnline.EsSolucion ? 1 : (Int32?)null);

                            if (respuestaId != 0)
                            {
                                int res = cursosBL.ActualizarRespuestaCursoOnline(respuestaCursoOnline);
                            }
                            else
                            {
                                int res = cursosBL.InsertarRespuestaCursoOnline(respuestaCursoOnline);
                            }
                        }
                        preguntaCursoOnline.NumRespuestas = OrdenRespuesta;
                    }


                    int _res = cursosBL.ActualizarPreguntaCursoOnline(preguntaCursoOnline);

                    //context.SaveChanges();

                    TransactionScope.Complete();

                    PostMessage(MessageType.Success);
                    return RedirectToAction("ManagePreguntasCursoOnline", new { unidadOnlineId = model.UnidadOnlineId });
                }
            }
            catch (Exception ex)
            {
                //InvalidarContext();
                PostMessage(MessageType.Error);
                return RedirectToAction("ManagePreguntasCursoOnline", new { unidadOnlineId = model.UnidadOnlineId });
                throw;
            }
        }


        ///_EditPreguntaCursoOnline
             [HttpPost,
        ValidateAntiForgeryToken,
        AppAuthorize(AppRol.Administrador, AppRol.Profesor)]
        public ActionResult EditPreguntaCursoOnline(_EditPreguntaCursoOnlineViewModel model, FormCollection form)
        {

            try
            {
                using (var TransactionScope = new TransactionScope())
                {
                    var esNuevo = !model.PreguntaCursoOnlineId.HasValue;
                    if (!ModelState.IsValid)
                    {
                        model.CargarDatos(DatosContext, model.UnidadOnlineId, model.PreguntaCursoOnlineId);
                        TryUpdateModel(model);
                        PostMessage(MessageType.Error, String.Empty);
                        return RedirectToAction("ManagePreguntasCursoOnline", new { CursoOnlineId = model.UnidadOnlineId });
                    }

                    PreguntaCursoOnline preguntaCursoOnline = new PreguntaCursoOnline();
                    List<RespuestaCursoOnline> lstRespuestas = new List<RespuestaCursoOnline>();
                    int _preguntaCursoOnlineId;


                    if (!esNuevo)
                    {
                        //preguntaCursoOnline = cursosBL.ListarPreguntaCursoOnline().First(x => x.PreguntaCursoOnlineId == model.PreguntaCursoOnlineId);    //context.PreguntaCursoOnline.First(x => x.PreguntaCursoOnlineId == model.PreguntaCursoOnlineId);
                        preguntaCursoOnline = cursosBL.ListarPreguntaCursoOnlinePorUnidadOnlineId(model.UnidadOnlineId).
                                         Where(x => x.PreguntaCursoOnlineId == (int)model.PreguntaCursoOnlineId).
                                         FirstOrDefault();
                        lstRespuestas = preguntaCursoOnline.RespuestaCursoOnline.ToList();  //context.RespuestaCursoOnline.Where(x => x.PreguntaCursoOnlineId == PreguntaCursoOnline.PreguntaCursoOnlineId).ToList();
                        _preguntaCursoOnlineId = preguntaCursoOnline.PreguntaCursoOnlineId;

                    }
                    else
                    {
                        //Establecer las variables por defecto 

                        //context.PreguntaCursoOnline.Add(PreguntaCursoOnline);
                        preguntaCursoOnline.UnidadOnlineId = model.UnidadOnlineId;
                        preguntaCursoOnline.CursoOnlineId = cursosBL.ObtenerUnidadCursoOnlinePorId(model.UnidadOnlineId).CursoOnlineId; //context.UnidadCursoOnline.Find(model.UnidadOnlineId).CursoOnlineId;
                        preguntaCursoOnline.Tipo = model.Tipo;
                        preguntaCursoOnline.Puntaje = 1;
                        preguntaCursoOnline.NumRespuestas = 0;
                        preguntaCursoOnline.Estado = ConstantHelpers.ESTADO.ACTIVO;

                        _preguntaCursoOnlineId = cursosBL.InsertarPreguntaCursoOnline(preguntaCursoOnline);


                    }

                    //model.PreguntaCursoOnlineId = _preguntaCursoOnlineId;

                    preguntaCursoOnline.Texto = form["TXT-PRE-" + model.Tipo + "-" + model.PreguntaCursoOnlineId];


                    if (model.Tipo == ConstantHelpers.TIPO_PREGUNTA.VERDADERO_FALSO)
                    {
                        var respuestaValue = form["PRE-" + model.Tipo + "-" + (model.PreguntaCursoOnlineId.HasValue ? model.PreguntaCursoOnlineId.ToString() : "0")].ToString();
                        if (lstRespuestas.Count() != 0)
                        {
                            foreach (var respuesta in lstRespuestas)
                            {
                                var respuestaEdit = respuesta;   //cursosBL.ListarRespuestaEvaluacionCursoOnline//context.RespuestaCursoOnline.Find(respuesta.RespuestaCursoOnlineId);
                                respuestaEdit.EsSolucion = (respuesta.Texto == respuestaValue);
                                respuestaEdit.OrdenSolucion = (respuestaEdit.EsSolucion ? 1 : (Int32?)null);

                                int res = cursosBL.ActualizarRespuestaCursoOnline(respuestaEdit);
                            }
                        }
                        else
                        {
                            for (int i = 0; i < 2; i++)
                            {
                                var RespuestaCursoOnline = new RespuestaCursoOnline();
                                RespuestaCursoOnline.PreguntaCursoOnline = preguntaCursoOnline;
                                RespuestaCursoOnline.Texto = (i == 0 ? "Verdadero" : "Falso");
                                RespuestaCursoOnline.EsSolucion = RespuestaCursoOnline.Texto == respuestaValue;
                                RespuestaCursoOnline.OrdenSolucion = (RespuestaCursoOnline.EsSolucion ? 1 : (Int32?)null);

                                //context.RespuestaCursoOnline.Add(RespuestaCursoOnline);
                                int res = cursosBL.InsertarRespuestaCursoOnline(RespuestaCursoOnline);
                            }
                        }
                        preguntaCursoOnline.NumRespuestas = 2;
                    }
                    else
                    {
                        var cantidadRelaciones = 0;
                        var txtPreguntasRelacionKey = form.AllKeys.Where(x => x.StartsWith("TXT-ENU-RES-" + model.Tipo));
                        var LstEnunciadoRelacion = new List<String>();


                        if (ConstantHelpers.TIPO_PREGUNTA.RELACIONAR == model.Tipo)
                        {
                            foreach (var txtpreguntasRelacionKey in txtPreguntasRelacionKey)
                            {
                                var respuestaValue = txtpreguntasRelacionKey.Split('-')[5].ToString();
                                LstEnunciadoRelacion.Add(respuestaValue);
                                cantidadRelaciones++;
                            }
                        }
                        else if (ConstantHelpers.TIPO_PREGUNTA.COMPLETAR == model.Tipo)
                        {
                            var preguntaTexto = preguntaCursoOnline.Texto.Split(new[] { "###" }, StringSplitOptions.None);
                            cantidadRelaciones = preguntaTexto.Length - 1;
                        }

                        var seleccionKey = "";
                        seleccionKey = form["PRE-" + model.Tipo + "-" + (model.PreguntaCursoOnlineId.HasValue ? model.PreguntaCursoOnlineId.ToString() : "0")];

                        var RespuestasKey = form.AllKeys.Where(x => x.StartsWith("TXT-RES-" + model.Tipo));
                        var OrdenRespuesta = 0;
                        foreach (var respuestaKey in RespuestasKey)
                        {
                            OrdenRespuesta++;
                            var respuestaCursoOnline = new RespuestaCursoOnline();
                            var respuestaId = respuestaKey.Split('-')[4].ToInteger();
                            var respuestaValue = respuestaKey.Split('-')[4].ToString();
                            if (respuestaId != 0)
                            {
                                respuestaCursoOnline = preguntaCursoOnline.RespuestaCursoOnline.FirstOrDefault(x => x.RespuestaCursoOnlineId == respuestaId);  //context.RespuestaCursoOnline.Find(respuestaId);

                            }
                            else
                            {
                                respuestaCursoOnline.PreguntaCursoOnline = preguntaCursoOnline;
                                respuestaCursoOnline.PreguntaCursoOnlineId = preguntaCursoOnline.PreguntaCursoOnlineId;



                                //context.RespuestaCursoOnline.Add(respuestaCursoOnline);

                            }
                            respuestaCursoOnline.Texto = (model.Tipo == ConstantHelpers.TIPO_PREGUNTA.RELACIONAR ? form["TXT-ENU-RES-" + model.Tipo + "-" + preguntaCursoOnline.PreguntaCursoOnlineId + "-" + respuestaValue] + "###" + form[respuestaKey] :
                                                            form[respuestaKey]);
                            respuestaCursoOnline.EsSolucion = (model.Tipo == ConstantHelpers.TIPO_PREGUNTA.RELACIONAR ? LstEnunciadoRelacion.Contains(respuestaValue) :
                                                                model.Tipo == ConstantHelpers.TIPO_PREGUNTA.COMPLETAR ? cantidadRelaciones >= OrdenRespuesta :
                                                                seleccionKey.Contains(respuestaValue) ? true : false);
                            respuestaCursoOnline.OrdenSolucion = (model.Tipo == ConstantHelpers.TIPO_PREGUNTA.RELACIONAR ? (respuestaCursoOnline.EsSolucion ? OrdenRespuesta : (Int32?)null) :
                                                                    respuestaCursoOnline.EsSolucion ? 1 : (Int32?)null);

                            if (respuestaId != 0)
                            {
                                int res = cursosBL.ActualizarRespuestaCursoOnline(respuestaCursoOnline);
                            }
                            else
                            {
                                int res = cursosBL.InsertarRespuestaCursoOnline(respuestaCursoOnline);
                            }
                        }
                        preguntaCursoOnline.NumRespuestas = OrdenRespuesta;
                    }


                    int _res = cursosBL.ActualizarPreguntaCursoOnline(preguntaCursoOnline);

                    //context.SaveChanges();

                    TransactionScope.Complete();

                    PostMessage(MessageType.Success);
                    return RedirectToAction("ManagePreguntasCursoOnline", new { unidadOnlineId = model.UnidadOnlineId });
                }
            }
            catch (Exception ex)
            {
                //InvalidarContext();
                PostMessage(MessageType.Error);
                return RedirectToAction("ManagePreguntasCursoOnline", new { unidadOnlineId = model.UnidadOnlineId });
                throw;
            }
        }
    }
}
