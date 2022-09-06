using System;
using System.Collections.Generic;
using System.Data;
//using System.Data.Entity;
using System.Linq;
using System.Net;
using System.Web;
using System.Web.Mvc;
using PROMPERU.AULAVIRTUAL.WEB.Filters;
using System.Transactions;
using PROMPERU.AULAVIRTUAL.WEB.Helpers;
using PROMPERU.AULAVIRTUAL.WEB.Models;
using PROMPERU.AULAVIRTUAL.WEB.ViewModel.Notificaciones;
using PROMPERU.AULAVIRTUAL.Helpers;
using PROMPERU.AULAVIRTUAL.BL;
using PROMPERU.AULAVIRTUAL.BE.CURSOS;
//using System.Data.Entity.Validation;

namespace PROMPERU.AULAVIRTUAL.WEB.Controllers
{
    public class NotificacionesController : BaseController
    {
        NotificacionesBL notificacionesBL = new NotificacionesBL();
        // GET: Notificaciones
        [AppAuthorize(AppRol.Administrador, AppRol.Supervisor, AppRol.Profesor)]
        public ActionResult ListNotificaciones(Int32? p, String CadenaBuscar, Int32? TipoNotificacionId)
        {
            var viewModel = new ListNotificacionesViewModel();
            viewModel.CargarDatos(DatosContext, p, CadenaBuscar, TipoNotificacionId);
            return View("ListNotificacionesNew",viewModel);
        }

        [AppAuthorize(AppRol.Administrador, AppRol.Supervisor, AppRol.Profesor)]
        public ActionResult EditAddNotificacion(Int32? NotificacionId)
        {
            var viewModel = new EditAddNotificacionViewModel();
            viewModel.CargarDatos(DatosContext, NotificacionId);
            return View("EditAddNotificacionNew", viewModel);
        }



        [AppAuthorize(AppRol.Alumno)]
        public ActionResult NotificacionView(Int32? NotificacionId)
        {
            var viewModel = new EditAddNotificacionViewModel();
            viewModel.CargarDatos(DatosContext, NotificacionId);
            return View("NotificacionView", viewModel);
        }


        [AppAuthorize(AppRol.Administrador, AppRol.Supervisor, AppRol.Profesor)]
        public ActionResult EditAddNotificacionCurso(Int32? NotificacionId)
        {
            var viewModel = new EditAddNotificacionViewModel();
            viewModel.CargarDatos(DatosContext, NotificacionId);
            return View("EditAddNotificacionCurso", viewModel);
        }

        
        [AppAuthorize(AppRol.Administrador, AppRol.Supervisor, AppRol.Profesor)]
        public ActionResult ListNotificacionesAdm(Int32? p, String CadenaBuscar, Int32? TipoNotificacionId)
        {
            var viewModel = new ListNotificacionesAdmViewModel();
            viewModel.CargarDatos(DatosContext, p, CadenaBuscar, 6, Session.GetRolCompleto(), Session.GetUsuarioId());
            return View(viewModel);
        }
        [AppAuthorize(AppRol.Administrador, AppRol.Supervisor, AppRol.Profesor)]
        public ActionResult ChangeStateNotificacion(Int32 NotificacionId)
        {
            try
            {
                //Notificaciones Notificaciones_ = context.Notificaciones.Find(NotificacionId);
                BE.CURSOS.Notificaciones Notificaciones_ = notificacionesBL.ListarNotificaciones().Find(x => x.NotificacionId == NotificacionId);

                Notificaciones_.Estado = Notificaciones_.Estado.Equals(ConstantHelpers.ESTADO.ACTIVO) ? ConstantHelpers.ESTADO.INACTIVO : ConstantHelpers.ESTADO.ACTIVO;

                notificacionesBL.ChangeStateNotificacion(NotificacionId, Notificaciones_.Estado);
                //context.SaveChanges();
                PostMessage(MessageType.Success);
            }
            catch
            {
                //*InvalidarContext(); 
                PostMessage(MessageType.Error);
            }
            return RedirectToAction("ListNotificaciones");
        }

        [AppAuthorize(AppRol.Administrador, AppRol.Supervisor, AppRol.Profesor)]
        [HttpPost]
        [ValidateAntiForgeryToken]
        [ValidateInput(false)]
        public ActionResult EditAddNotificacion(EditAddNotificacionViewModel model)
        {
            try
            {
                using (var TransactionScope = new TransactionScope())
                {
                    if (!ModelState.IsValid)
                    {
                        model.CargarDatos(DatosContext, model.NotificacionId);
                        TryUpdateModel(model);
                        PostMessage(MessageType.Error, String.Empty);
                        return View(model);
                    }

                    var Notificaciones_ = new Notificaciones();

                    bool IsNew = false;

                    if (model.NotificacionId.HasValue)
                    {
                        //Notificaciones_ = context.Notificaciones.First(x => x.NotificacionId == model.NotificacionId);
                        Notificaciones_ = notificacionesBL.ListarNotificaciones().First(x => x.NotificacionId == model.NotificacionId);
                    }
                    else
                    {
                        IsNew = true;
                        Notificaciones_.TipoNotificacionId = 6; //model.TipoNotificacionId == null ? 1 : Convert.ToInt32(model.TipoNotificacionId);
                        //Establecer las variables por defecto
                        //context.Notificaciones.Add(Notificaciones_);
                        

                    }

                    Notificaciones_.Titulo = model.Titulo;
                    Notificaciones_.Nombre = model.Nombre;
                    Notificaciones_.Contenido = model.Contenido;
                    
                    //if (!model.NotificacionId.HasValue)
                    //{
                    //    Notificaciones_.TipoNotificacionId = 6; //model.TipoNotificacionId == null ? 1 : Convert.ToInt32(model.TipoNotificacionId);
                    //}
                    Notificaciones_.Estado = (model.Estado) ? ConstantHelpers.ESTADO.ACTIVO : ConstantHelpers.ESTADO.INACTIVO;

                    
                    //context.SaveChanges();
                    if(IsNew == true)
                    {
                        notificacionesBL.InsertarNotificacion(Notificaciones_);
                    }
                    else
                    {
                        notificacionesBL.ActualizarNotificacion(Notificaciones_);
                    }

                    TransactionScope.Complete();
                    PostMessage(MessageType.Success);
                    return RedirectToAction("ListNotificaciones");
                }
            }
            catch (Exception e)
            {
                Util.SaveErrorLog(e);
                //InvalidarContext();
                PostMessage(MessageType.Error);
                model.CargarDatos(DatosContext, model.NotificacionId);
                TryUpdateModel(model);
                return View(model);
            }
        }

        [AppAuthorize(AppRol.Administrador, AppRol.Supervisor, AppRol.Profesor)]
        public ActionResult DeleteNotificacion(int notificacionId)
        {
            try
            {
                notificacionesBL.EliminarNotificacion(notificacionId);
                
                PostMessage(MessageType.Success);
            }
            catch
            {
                //*InvalidarContext(); 
                PostMessage(MessageType.Error);
            }

            return RedirectToAction("ListNotificaciones");
        }


        [AppAuthorize(AppRol.Administrador, AppRol.Supervisor, AppRol.Profesor)]
        [HttpPost]
        [ValidateAntiForgeryToken]
        [ValidateInput(false)]
        public ActionResult EditAddNotificacionCurso(EditAddNotificacionViewModel model)
        {
            try
            {
                using (var TransactionScope = new TransactionScope())
                {
                    if (!ModelState.IsValid)
                    {
                        model.CargarDatos(DatosContext, model.NotificacionId);
                        TryUpdateModel(model);
                        PostMessage(MessageType.Error, String.Empty);
                        return View(model);
                    }

                    var Notificaciones_ = new Notificaciones();

                    bool IsNew = false;

                    if (model.NotificacionId.HasValue)
                    {
                        //Notificaciones_ = context.Notificaciones.First(x => x.NotificacionId == model.NotificacionId);
                        Notificaciones_ = notificacionesBL.ListarNotificaciones().First(x => x.NotificacionId == model.NotificacionId);
                    }
                    else
                    {
                        IsNew = true;
                        Notificaciones_.TipoNotificacionId = 6; //model.TipoNotificacionId == null ? 1 : Convert.ToInt32(model.TipoNotificacionId);
                                                                //Establecer las variables por defecto
                                                                //context.Notificaciones.Add(Notificaciones_);


                    }

                    Notificaciones_.Titulo = model.Titulo;
                    Notificaciones_.Nombre = model.Nombre;
                    Notificaciones_.Contenido = model.Contenido;
                    Notificaciones_.CursoOnlineId = model.CursoOnlineId;
                    //if (!model.NotificacionId.HasValue)
                    //{
                    //    Notificaciones_.TipoNotificacionId = 6; //model.TipoNotificacionId == null ? 1 : Convert.ToInt32(model.TipoNotificacionId);
                    //}
                    Notificaciones_.Estado = (model.Estado) ? ConstantHelpers.ESTADO.ACTIVO : ConstantHelpers.ESTADO.INACTIVO;


                    //context.SaveChanges();
                    if (IsNew == true)
                    {
                        notificacionesBL.InsertarNotificacion(Notificaciones_);
                    }
                    else
                    {
                        notificacionesBL.ActualizarNotificacion(Notificaciones_);
                    }

                    TransactionScope.Complete();
                    PostMessage(MessageType.Success);
                    return RedirectToAction("ListNotificacionesAdm");
                }
            }
            catch (Exception e)
            {
                Util.SaveErrorLog(e);
                //InvalidarContext();
                PostMessage(MessageType.Error);
                model.CargarDatos(DatosContext, model.NotificacionId);
                TryUpdateModel(model);
                return View(model);
            }
        }


        //protected override void Dispose(bool disposing)
        //{
        //    if (disposing)
        //    {
        //        context.Dispose();
        //    }
        //    base.Dispose(disposing);
        //}
    }
}
