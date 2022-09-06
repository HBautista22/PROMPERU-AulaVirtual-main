using PROMPERU.AULAVIRTUAL.WEB.Filters;
using PROMPERU.AULAVIRTUAL.WEB.Helpers;
using PROMPERU.AULAVIRTUAL.BE.USUARIO;
using PROMPERU.AULAVIRTUAL.BE.CURSOS;
using PROMPERU.AULAVIRTUAL.BE.PARAMETROS;
using PROMPERU.AULAVIRTUAL.WEB.Logic;
using PROMPERU.AULAVIRTUAL.WEB.ViewModel.User;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Transactions;
using System.Web;
using System.Web.Mvc;
using System.Web.Script.Serialization;
using PROMPERU.AULAVIRTUAL.WEB;
using PROMPERU.AULAVIRTUAL.BL;
using Newtonsoft.Json;
using PagedList;
using PROMPERU.AULAVIRTUAL.Helpers;

namespace PROMPERU.AULAVIRTUAL.WEB.Controllers
{
    public class UserController : BaseController
    {
        UsuarioBL usuarioBL = new UsuarioBL();
        CursosBL cursosBL = new CursosBL();
        EmpresaBL empresaBL = new EmpresaBL();
        ParametroBL parametroBL = new ParametroBL();
        GrupoBL grupoBL = new GrupoBL();
        //
        // GET: /User/

        public ActionResult Index()
        {
            return View("IndexNew");
        }

        #region LISTAR_USUARIOS
        [AppAuthorize(AppRol.Administrador, AppRol.Supervisor, AppRol.Profesor)]
        public ActionResult ListUsuario(Int32? p, String CadenaBuscar, Int32? EmpresaId, String MarcaTodo, String EmailBuscar, String DniBuscar, String RolId, String EstadoId)
        {
            var viewModel = new ListUsuarioViewModel();

            viewModel.CadenaBuscar = CadenaBuscar;
            viewModel.p = p ?? 1;
            viewModel.EmpresaId = EmpresaId;
            viewModel.MarcaTodo = MarcaTodo;
            viewModel.EmailBuscar = EmailBuscar;
            viewModel.DniBuscar = DniBuscar;
            viewModel.RolId = RolId;
            viewModel.EstadoId = EstadoId;
            viewModel.LstEmpresa = empresaBL.ListarEmpresa();

            List<Empresa> lstEmpresa = empresaBL.ListarEmpresa();
            List<Usuario> lstAlumnosActivos = usuarioBL.ListarAlumnosActivos();




            if (Session.GetRolCompleto() == ConstantHelpers.ROL.SUPERVISOR)
            {
                EmpresaId = Session.GetEmpresaId();
                viewModel.LstEmpresa = lstEmpresa.Where(x => x.EmpresaId == EmpresaId).ToList();
            }

            if (EmpresaId.HasValue)
            {
                viewModel.EmpresaId = EmpresaId;
                lstAlumnosActivos = lstAlumnosActivos.Where(x => x.EmpresaId == EmpresaId).ToList();
            }
            if (!String.IsNullOrEmpty(CadenaBuscar))
            {
                foreach (var token in CadenaBuscar.Split(' '))
                    lstAlumnosActivos = lstAlumnosActivos.Where(x => x.Nombre.ToLower().Contains(token.ToLower()) || x.Apellido.ToLower().Contains(token.ToLower())).ToList();
            }

            if (!String.IsNullOrEmpty(EmailBuscar))
            {
                foreach (var token in EmailBuscar.Split(' '))
                    lstAlumnosActivos = lstAlumnosActivos.Where(x => x.Email.Contains(token)).ToList();
            }

            if (!String.IsNullOrEmpty(DniBuscar))
            {
                foreach (var token in DniBuscar.Split(' '))
                    lstAlumnosActivos = lstAlumnosActivos.Where(x => x.DNI.Contains(token)).ToList();
            }

            if (!String.IsNullOrEmpty(RolId))
            {
                foreach (var token in RolId.Split(' '))
                    lstAlumnosActivos = lstAlumnosActivos.Where(x => x.Rol.Contains(token)).ToList();
            }

            if (!String.IsNullOrEmpty(EstadoId))
            {
                foreach (var token in EstadoId.Split(' '))
                    lstAlumnosActivos = lstAlumnosActivos.Where(x => x.Estado.Contains(token)).ToList();
            }

            lstAlumnosActivos = lstAlumnosActivos.OrderByDescending(x => x.UsuarioId).ToList();

            var query2 = lstAlumnosActivos.Where(x => x.Estado.Contains(ConstantHelpers.ESTADO.ACTIVO));
            Session["TotalId"] = query2.Select(x => x.UsuarioId).ToJson();

            viewModel.LstUsuario = lstAlumnosActivos.ToPagedList((int)viewModel.p, ConstantHelpers.DEFAULT_PAGE_SIZE);

            //viewModel.CargarDatos(_DatosContext, p, MarcaTodo, CadenaBuscar, EmpresaId, EmailBuscar, DniBuscar, RolId, EstadoId);
            return View("ListUsuarioNew", viewModel);
        }

        [AppAuthorize(AppRol.Administrador, AppRol.Supervisor, AppRol.Profesor)]
        public ActionResult ListUsuarioProfesor(Int32? p, String CadenaBuscar, Int32? EmpresaId, String MarcaTodo, String EmailBuscar, String DniBuscar)
        {
            var viewModel = new ListUsuarioViewModel();

            viewModel.CadenaBuscar = CadenaBuscar;
            viewModel.p = p ?? 1;
            viewModel.EmpresaId = EmpresaId;
            viewModel.MarcaTodo = MarcaTodo;
            viewModel.EmailBuscar = EmailBuscar;
            viewModel.DniBuscar = DniBuscar;
            viewModel.LstEmpresa = empresaBL.ListarEmpresa();

            List<Empresa> lstEmpresa = empresaBL.ListarEmpresa();
            List<Usuario> lstAlumnosActivos = usuarioBL.ListarProfesoresActivos();




            if (Session.GetRolCompleto() == ConstantHelpers.ROL.SUPERVISOR)
            {
                EmpresaId = Session.GetEmpresaId();
                viewModel.LstEmpresa = lstEmpresa.Where(x => x.EmpresaId == EmpresaId).ToList();
            }
            lstAlumnosActivos = lstAlumnosActivos.Where(x => !x.Estado.Equals(ConstantHelpers.ESTADO.ELIMINADO) && x.Rol.Equals(ConstantHelpers.ROL.PROFESOR)).ToList();
            if (EmpresaId.HasValue)
            {
                viewModel.EmpresaId = EmpresaId;
                lstAlumnosActivos = lstAlumnosActivos.Where(x => x.EmpresaId == EmpresaId).ToList();
            }
            if (!String.IsNullOrEmpty(CadenaBuscar))
            {
                foreach (var token in CadenaBuscar.Split(' '))
                    lstAlumnosActivos = lstAlumnosActivos.Where(x => x.Nombre.Contains(token) || x.Apellido.Contains(token)).ToList();
            }

            if (!String.IsNullOrEmpty(EmailBuscar))
            {
                foreach (var token in EmailBuscar.Split(' '))
                    lstAlumnosActivos = lstAlumnosActivos.Where(x => x.Email.Contains(token)).ToList();
            }

            if (!String.IsNullOrEmpty(DniBuscar))
            {
                foreach (var token in DniBuscar.Split(' '))
                    lstAlumnosActivos = lstAlumnosActivos.Where(x => x.DNI.Contains(token)).ToList();
            }



            lstAlumnosActivos = lstAlumnosActivos.OrderByDescending(x => x.UsuarioId).ToList();

            var query2 = lstAlumnosActivos.Where(x => x.Estado.Contains(ConstantHelpers.ESTADO.ACTIVO));
            Session["TotalId"] = query2.Select(x => x.UsuarioId).ToJson();

            viewModel.LstUsuario = lstAlumnosActivos.ToPagedList((int)viewModel.p, ConstantHelpers.DEFAULT_PAGE_SIZE);

            //viewModel.CargarDatos(_DatosContext, p, MarcaTodo, CadenaBuscar, EmpresaId, EmailBuscar, DniBuscar, RolId, EstadoId);
            return View("ListUsuarioProfesor", viewModel);
        }

        #endregion

        #region LISTAR_GRUPOS
        [AppAuthorize(AppRol.Administrador, AppRol.Supervisor)]
        public ActionResult ListUsuariosGrupos(Int32? p, String CadenaBuscar)
        {
            var viewModel = new ListUsuariosGruposViewModel();
            viewModel.p = p ?? 1;
            viewModel.CadenaBuscar = CadenaBuscar;
            List<Grupo> queryUsuario = usuarioBL.ListarGrupo();     //dataContext.context.Grupo.Where(x => !x.Estado.Equals(ConstantHelpers.ESTADO.ELIMINADO)).AsQueryable();

            if (!String.IsNullOrEmpty(CadenaBuscar))
            {
                foreach (var token in CadenaBuscar.Split(' '))
                    queryUsuario = queryUsuario.Where(x => x.Descripcion.ToUpper().Contains(token.ToUpper())).ToList();
            }
            if (queryUsuario.Count == 0)
                viewModel.CantidadRegistros = new int[0];
            else
                viewModel.CantidadRegistros = new int[queryUsuario.Select(x => x.GrupoId).Max() + 1];

            foreach (var item in queryUsuario.Select(x => x.GrupoId))
            {
                viewModel.CantidadRegistros[item] = usuarioBL.TotalUsuariossActivosPorGrupo(item); //dataContext.context.UsuarioGrupo.Where(x => x.Estado == ConstantHelpers.ESTADO.ACTIVO && x.GrupoId == item).Count();
            }

            queryUsuario = queryUsuario.OrderByDescending(x => x.FechaCreacion).ToList();

            viewModel.LstUsuario = queryUsuario.ToPagedList(viewModel.p, ConstantHelpers.DEFAULT_PAGE_SIZE);
            //viewModel.CargarDatos(_DatosContext, CadenaBuscar, p);
            return View("ListUsuariosGruposNew", viewModel);
        }
        #endregion

        #region ADMINISTRAR_USUARIO
        [AppAuthorize(AppRol.Administrador, AppRol.Supervisor)]
        public ActionResult EditUsuario(Int32? usuarioId)
        {
            var viewModel = new EditUsuarioViewModel();
            viewModel.UsuarioId = usuarioId;

            viewModel.CargarDatos(DatosContext, viewModel.UsuarioId);


            return View("EditUsuarioNew", viewModel);
        }

        [AppAuthorize(AppRol.Administrador, AppRol.Supervisor)]
        public ActionResult EditUsuarioProfesor(Int32? usuarioId)
        {
            var viewModel = new EditUsuarioProfesorViewModel();
            viewModel.UsuarioId = usuarioId;

            viewModel.CargarDatos(DatosContext, viewModel.UsuarioId);


            return View(viewModel);
        }
        #endregion

        [AppAuthorize(AppRol.Administrador, AppRol.Supervisor, AppRol.Profesor)]
        public ActionResult EditGrupo(int? grupoId)
        {
            EditGrupoViewModel viewModel = new EditGrupoViewModel();
            viewModel.CargarDatos(DatosContext, grupoId);
            return View(viewModel);
        }


        [AppAuthorize(AppRol.Administrador)]
        public ActionResult _AddUsuario(int? grupoId)
        {
            _AddUsuarioViewModel viewModel = new _AddUsuarioViewModel();
            viewModel.CargarDatos(DatosContext, grupoId);
            return View(viewModel);
        }

        [AppAuthorize(AppRol.Administrador, AppRol.Profesor)]
        public ActionResult _AddGrupo(Int32? UsuarioId, String MarcaAlgunos)
        {
            var viewModel = new _AddGrupoViewModel();

            viewModel.UsuarioId = UsuarioId;

            viewModel.LstCursoOnlineMaestro = usuarioBL.ListarAddGrupoLISPorUsuarioId((int)viewModel.UsuarioId); //dataContext.context.Grupo.SqlQuery("USP_AddGrupo_LIS {0}", UsuarioId).AsQueryable(); ;
            viewModel.LstCursoGrupo = usuarioBL.ListarAddGrupoSELPorUsuarioId((int)viewModel.UsuarioId);

            //viewModel.CargarDatos(_DatosContext, UsuarioId, MarcaAlgunos);
            return View(viewModel);
        }


        //[HttpPost, ValidateInput(false), AppAuthorize(AppRol.Administrador), ValidateAntiForgeryToken]
        //public ActionResult _AddUsuario(_AddUsuarioViewModel model, HttpPostedFileBase BannerImage)
        //{
        //    try
        //    {
        //        using (var TransactionScope = new TransactionScope())
        //        {
        //            var Grupo = new Grupo();
        //            Grupo = context.Grupo.FirstOrDefault(x => x.GrupoId == model.GrupoId);
        //            Grupo.Aforo = Grupo.Aforo == 1 ? 0 : 1;

        //            context.SaveChanges();
        //            TransactionScope.Complete();
        //            PostMessage(MessageType.Success);
        //            return RedirectToAction("ListUsuariosGrupos");

        //        }
        //    }
        //    catch (Exception e)
        //    {
        //        Util.SaveErrorLog(e);
        //        InvalidarContext();
        //        PostMessage(MessageType.Error);
        //        return RedirectToAction("ListUsuariosGrupos");
        //    }
        //}


        #region METODO_REGISTRAR_GRUPO
        [HttpPost, ValidateInput(false), AppAuthorize(AppRol.Administrador), ValidateAntiForgeryToken]
        public ActionResult _AddGrupo(_AddGrupoViewModel model, HttpPostedFileBase BannerImage)
        {
            try
            {
                using (var TransactionScope = new TransactionScope())
                {

                    #region VARIABLES
                    int[] listUsuarioId = null;
                    int[] listGrupoId = null;
                    UsuarioItems usuarioItems = new UsuarioItems();
                    UsuarioBL usuarioBL = new UsuarioBL();
                    int result = 0;

                    List<dynamic> Listado = new List<dynamic>();

                    #endregion

                    #region PROCEDIMIENTO
                    if (model.MarcarTodo.Equals(1))
                    {
                        listUsuarioId = Util.ConvertArrayDynamicToInt(Util.DeserealizeJscript((string)Session["TotalId"]));
                        if (model.ListItems != null)
                            listGrupoId = Util.ConvertArrayTextToInt(Util.DeserealizeJscript(Util.Base64toString(model.ListItems)));
                        else
                            listGrupoId = Util.ConvertArrayTextToInt(Util.DeserealizeJscript("[]"));

                        foreach (var item1 in listUsuarioId)
                        {
                            if (listGrupoId != null && !listGrupoId.Length.Equals(0))
                            {
                                foreach (var item2 in listGrupoId)
                                {
                                    Listado.Add(new { UsuarioId = item1, GrupoId = item2 });
                                }
                            }
                            else
                            {
                                Listado.Add(new { UsuarioId = item1, GrupoId = 0 });
                            }
                        }

                        Util.SaveBulkCsv(Listado);

                        Util.CopyToFTP(Util.RutaMasivo);


                        result = usuarioBL.VolcadoUsuarioGrupo();

                        TransactionScope.Complete();
                        PostMessage(MessageType.Success);
                        return RedirectToAction("ListUsuario");


                    }
                    else if (model.Usuario_Id != null && model.Usuario_Id != string.Empty)
                    {
                        // deciframos la cadena y la convertimos en un objeto de tipo arreglo
                        listUsuarioId = Util.ConvertArrayTextToInt(Util.DeserealizeJscript(Util.Base64toString(model.Usuario_Id)));
                    }
                    else
                    {
                        listUsuarioId = Util.ConvertArrayTextToInt(Util.DeserealizeJscript("[\"" + model.UsuarioId + "\"]"));
                    }

                    listGrupoId = Util.ConvertArrayTextToInt(Util.DeserealizeJscript(Util.Base64toString(model.ListItems)));

                    //llenamos las variables en el modelo
                    usuarioItems.UsuarioId = listUsuarioId;
                    usuarioItems.GrupoId = listGrupoId;
                    //TODO:******
                    result = usuarioBL.EliminarUsuarioGrupo(usuarioItems);//elimina usuarios del grupo

                    if (result.Equals(1))
                    {

                        result = usuarioBL.InsertarUsuarioGrupo(usuarioItems);//insertamos a los usuarios en los grupos correspondientes.

                    }
                    // END TODO**//

                    TransactionScope.Complete();
                    PostMessage(MessageType.Success);
                    return RedirectToAction("ListUsuario");
                    #endregion

                }
            }
            catch (Exception e)
            {
                #region ERROR
                Util.SaveErrorLog(e);
                //InvalidarContext();
                PostMessage(MessageType.Error);
                return RedirectToAction("ListUsuario");
                #endregion
            }
        }
        #endregion


        [AppAuthorize(AppRol.Administrador, AppRol.Profesor)]
        public ActionResult _AddCursoMaestroOnline(Int32? CursoOnlineMaestroId)
        {
            var viewModel = new _AddCursoMaestroOnlineViewModel();
            viewModel.CargarDatos(DatosContext, CursoOnlineMaestroId);
            return View("_AddCursoMaestroOnlineNew", viewModel);
        }
        [AppAuthorize(AppRol.Administrador)]


        //[HttpPost, ValidateInput(false), AppAuthorize(AppRol.Administrador), ValidateAntiForgeryToken]
        //public ActionResult _AddCursoMaestroOnline(_AddCursoMaestroOnlineViewModel model, HttpPostedFileBase BannerImage)
        //{
        //    try
        //    {
        //        using (var TransactionScope = new TransactionScope())
        //        {



        //            List<CursoGrupo> ListCursoOnline = new List<CursoGrupo>();
        //            var serializer = new JavaScriptSerializer();
        //            dynamic result = serializer.DeserializeObject(Util.Base64toString(model.ListItems));

        //            var CursoOnline = new CursoGrupo();

        //            if (model.GrupoId.HasValue)
        //            {
        //                ListCursoOnline = context.CursoGrupo.Where(x => x.GrupoId == model.GrupoId).ToList();
        //            }

        //            if (ListCursoOnline != null)
        //            {
        //                foreach (var item in ListCursoOnline)
        //                {
        //                    item.Estado = ConstantHelpers.ESTADO.ELIMINADO;
        //                    context.SaveChanges();
        //                }
        //            }
        //            else
        //            {
        //                CursoOnline = new CursoGrupo();
        //            }



        //            var i = 1;

        //            foreach (var item in result)
        //            {
        //                context.CursoGrupo.Add(CursoOnline);
        //                CursoOnline.CursoOnlineMaestroId = int.Parse(item);
        //                CursoOnline.GrupoId = (int)model.GrupoId;
        //                //CursoOnline.FechaCreacion = DateTime.Now;
        //                CursoOnline.Estado = ConstantHelpers.ESTADO.ACTIVO;
        //                //CursoOnline.Orden = i;
        //                context.SaveChanges();
        //                i++;
        //            }

        //            //context.SaveChanges();
        //            TransactionScope.Complete();
        //            PostMessage(MessageType.Success);
        //            return RedirectToAction("ListUsuariosGrupos");

        //        }
        //    }
        //    catch (Exception e)
        //    {
        //        Util.SaveErrorLog(e);
        //        InvalidarContext();
        //        PostMessage(MessageType.Error);
        //        return RedirectToAction("ListUsuariosGrupos");
        //    }
        //}



        // [AppAuthorize(AppRol.Administrador, AppRol.Supervisor)]
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult EditGrupo(EditGrupoViewModel model)
        {
            try
            {
                using (var TransactionScope = new TransactionScope())
                {
                    if (!ModelState.IsValid)
                    {
                        model.CargarDatos(DatosContext, model.GrupoId);
                        TryUpdateModel(model);
                        PostMessage(MessageType.Error, String.Empty);
                        return View(model);
                    }


                    var Grupo = new Grupo();

                    if (model.GrupoId.HasValue)
                    {
                        //Grupo = context.Grupo.First(x => x.GrupoId == model.GrupoId);
                        Grupo = usuarioBL.ListarGrupo().First(x => x.GrupoId == model.GrupoId);
                    }
                    else
                    {
                        //Establecer las variables por defecto
                        // context.Grupo.Add(Grupo);
                    }

                    Grupo.Descripcion = model.Descripcion;
                    Grupo.Estado = (model.Estado) ? ConstantHelpers.ESTADO.ACTIVO : ConstantHelpers.ESTADO.INACTIVO;
                    Grupo.FechaCreacion = DateTime.Now;
                    Grupo.TipoRegistro = model.TipoGrupo;
                    Grupo.Aforo = model.Aforo;

                    //TODO : insertupdate grupo, store,da ,bl

                    if (model.GrupoId.HasValue)
                    {
                        //TODO : Update grupo
                        grupoBL.ActualizarGrupo(Grupo);
                    }
                    else
                    {
                        grupoBL.InsertarGrupo(Grupo);
                    }

                    TransactionScope.Complete();

                    PostMessage(MessageType.Success);
                    return RedirectToAction("ListUsuariosGrupos");
                }
            }
            catch (Exception e)
            {
                Util.SaveErrorLog(e);
                //InvalidarContext();
                PostMessage(MessageType.Error);
                model.CargarDatos(DatosContext, model.GrupoId);
                TryUpdateModel(model);
                return View(model);
            }
        }



        //[AppAuthorize(AppRol.Administrador)]
        //public ActionResult _SendMensaje(Int32? GrupoId)
        //{
        //    var viewModel = new _SendMensajeViewModel();
        //    viewModel.CargarDatos(_DatosContext,GrupoId);
        //    return View(viewModel);
        //}


        //#region METODO_ENVIAR_MENSAJES
        //[AppAuthorize(AppRol.Administrador, AppRol.Supervisor)]
        //[HttpPost]
        //[ValidateAntiForgeryToken]
        //public ActionResult _SendMensaje(_SendMensajeViewModel model)
        //{

        //    try
        //    {
        //        var Grupo = context.Usuario.SqlQuery("USP_SendMensajeGrupo_LIS {0}", model.GrupoId).Select(s => Tuple.Create(s.Email, s.Nombre)).ToList();
        //        var nombreGrupo = context.Grupo.FirstOrDefault(x => x.GrupoId == model.GrupoId);

        //        MailSMTP mail = new MailSMTP();
        //        var direccionTemplate = Server.MapPath("~/Views/Shared/EditorTemplates/_TemplateCorreoNotificaciones.cshtml");
        //        var configuracion = context.Notificaciones.Where(a => a.Estado == Helpers.ConstantHelpers.ESTADO.ACTIVO && a.NotificacionId == model.Mensaje).FirstOrDefault();

        //        if (configuracion != null)
        //        {
        //            var contenido = configuracion.Contenido.Replace("@servidor", "").Replace("@enlace", "");
        //            var titulo = configuracion.Nombre;
        //            var body = mail.PopulateEmailBody(direccionTemplate, contenido, nombreGrupo.Descripcion, titulo);
        //            mail.SendMultipleEmail(Grupo, titulo, body);
        //        }

        //        PostMessage(MessageType.Success);
        //        return RedirectToAction("ListUsuariosGrupos");
        //    }
        //    catch (Exception e)
        //    {
        //        Util.SaveErrorLog(e);
        //        PostMessage(MessageType.Error);
        //        return RedirectToAction("ListUsuariosGrupos");
        //    }


        //}
        //#endregion

        [AppAuthorize(AppRol.Administrador, AppRol.Supervisor, AppRol.Profesor)]
        public ActionResult ChangeStateUsuario(Int32 usuarioId)
        {
            try
            {
                Usuario Usuario = usuarioBL.ObtenerUsuarioPorId(usuarioId);

                Usuario.Estado = Usuario.Estado.Equals(ConstantHelpers.ESTADO.ACTIVO) ? ConstantHelpers.ESTADO.INACTIVO : ConstantHelpers.ESTADO.ACTIVO;
                usuarioBL.ActualizarUsuarioEstado(usuarioId, Usuario.Estado);

                //context.SaveChanges();
                PostMessage(MessageType.Success);
            }
            catch (Exception e)
            {
                Util.SaveErrorLog(e);
                //InvalidarContext();
                PostMessage(MessageType.Error);
            }
            return RedirectToAction("ListUsuario");
        }


        [AppAuthorize(AppRol.Administrador, AppRol.Supervisor, AppRol.Profesor)]
        public ActionResult ChangeStateUsuarioProfesor(Int32 usuarioId)
        {
            try
            {
                Usuario Usuario = usuarioBL.ObtenerUsuarioPorId(usuarioId);

                Usuario.Estado = Usuario.Estado.Equals(ConstantHelpers.ESTADO.ACTIVO) ? ConstantHelpers.ESTADO.INACTIVO : ConstantHelpers.ESTADO.ACTIVO;
                usuarioBL.ActualizarUsuarioEstado(usuarioId, Usuario.Estado);

                //context.SaveChanges();
                PostMessage(MessageType.Success);
            }
            catch (Exception e)
            {
                Util.SaveErrorLog(e);
                //InvalidarContext();
                PostMessage(MessageType.Error);
            }
            return RedirectToAction("ListUsuarioProfesor");
        }




        [AppAuthorize(AppRol.Administrador, AppRol.Supervisor, AppRol.Profesor)]
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult EditUsuario(EditUsuarioViewModel model)
        {
            Grupo grupo = new Grupo();
            try
            {
                using (var TransactionScope = new TransactionScope())
                {
                    if (!ModelState.IsValid)
                    {

                        //viewModel.UsuarioId = usuarioId;
                        model.CargarDatos(DatosContext, model.UsuarioId);
                        grupo = model.LstGrupo.Find(x => x.GrupoId == model.GrupoId);
                        model.Grupo = grupo.Descripcion;
                        TryUpdateModel(model);
                        PostMessage(MessageType.Error, String.Empty);
                        return View(model);
                    }
                    Usuario Usuario = new Usuario();
                    UsuarioGrupo UsuarioGrupo = new UsuarioGrupo();
                    model.LstGrupo = usuarioBL.ListarGrupo().Where(x => x.Estado == ConstantHelpers.ESTADO.ACTIVO).ToList();
                    grupo = model.LstGrupo.Find(x => x.GrupoId == model.GrupoId);
                    model.Grupo = grupo.Descripcion;
                    if (model.UsuarioId.HasValue)
                    {
                        Usuario = usuarioBL.ObtenerUsuarioPorId((int)model.UsuarioId);//context.Usuario.First(x => x.UsuarioId == model.UsuarioId);
                    }
                    Usuario.Codigo = model.Codigo;
                    Usuario.Nombre = model.Nombre;
                    Usuario.Apellido = model.Apellido;
                    Usuario.Rol = model.Rol;
                    Usuario.Cargo = model.Cargo;
                    Usuario.Grupo = model.Grupo;
                    Usuario.Email = model.Email;
                    Usuario.Password = model.Password;
                    Usuario.EmpresaId = model.EmpresaId == 0 ? null : model.EmpresaId;
                    //Usuario.DistritoId = model.
                    Usuario.Estado = (model.Estado) ? ConstantHelpers.ESTADO.ACTIVO : ConstantHelpers.ESTADO.INACTIVO;
                    Usuario.SexoId = model.SexoId;

                    usuarioBL.ActualizarUsuario(Usuario);

                    /*UsuarioGrupo.GrupoId = (int)model.GrupoId;
                    UsuarioGrupo.UsuarioId = (int)model.UsuarioId;
                    UsuarioGrupo.Estado = ConstantHelpers.ESTADO.ACTIVO;*/

                    //context.SaveChanges();

                    TransactionScope.Complete();

                    PostMessage(MessageType.Success);
                    return RedirectToAction("ListUsuario");
                }
            }
            catch (Exception e)
            {
                Util.SaveErrorLog(e);
                //InvalidarContext();
                PostMessage(MessageType.Error);

                model.CargarDatos(DatosContext, model.UsuarioId);
                TryUpdateModel(model);
                return View(model);
            }
        }


        [AppAuthorize(AppRol.Administrador, AppRol.Supervisor, AppRol.Profesor)]
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult EditUsuarioProfesor(EditUsuarioProfesorViewModel model)
        {
            try
            {
                using (var TransactionScope = new TransactionScope())
                {
                    if (!ModelState.IsValid)
                    {
                        model.CargarDatos(DatosContext, model.UsuarioId);
                        TryUpdateModel(model);
                        PostMessage(MessageType.Error, String.Empty);
                        return View(model);
                    }

                    var Usuario = new Usuario();

                    if (model.UsuarioId.HasValue)
                    {
                        Usuario = usuarioBL.ObtenerUsuarioPorId((int)model.UsuarioId);
                    }
                    else
                    {
                        //Establecer las variables por defecto
                        //context.Usuario.Add(Usuario);
                        //usuarioBL.ActualizarUsuario(Usuario);

                        var Usuario2 = usuarioBL.ObtenerUsuarioPorEmail(model.Email);

                        if (Usuario2 != null && Usuario2.Email != null)
                        {
                            model.CargarDatos(DatosContext, model.UsuarioId);
                            TryUpdateModel(model);
                            PostMessage(MessageType.Error, "El correo ya se encuentra registrado");
                            return View(model);
                        }

                    }

                    Usuario.Nombre = model.Nombre;
                    Usuario.Apellido = model.Apellido;
                    Usuario.Password = model.Password;
                    Usuario.Email = model.Email;
                    Usuario.Codigo = model.Email;
                    Usuario.DistritoId = model.Region;
                    Usuario.Rol = ConstantHelpers.ROL.PROFESOR;
                    if (!String.IsNullOrEmpty(model.Avatar))
                        Usuario.Avatar = model.Avatar;
                    if (!String.IsNullOrEmpty(model.Descripcion))
                        Usuario.Descripcion = model.Descripcion;
                    Usuario.Estado = (model.Estado) ? ConstantHelpers.ESTADO.ACTIVO : ConstantHelpers.ESTADO.INACTIVO;
                    if (Usuario.UsuarioId > 0)
                        usuarioBL.ActualizarUsuario(Usuario);
                    else
                        usuarioBL.InsertarUsuario(Usuario);
                    TransactionScope.Complete();

                    PostMessage(MessageType.Success);
                    return RedirectToAction("ListUsuarioProfesor");
                }
            }
            catch (Exception e)
            {
                Util.SaveErrorLog(e);
                //InvalidarContext();
                PostMessage(MessageType.Error);
                model.CargarDatos(DatosContext, model.UsuarioId);
                TryUpdateModel(model);
                return View(model);
            }
        }

        //[AppAuthorize(AppRol.Supervisor)]
        //public ActionResult ListUsuariosEmpresa(Int32? p, String CadenaBuscar)
        //{
        //    var viewModel = new ListUsuariosEmpresaViewModel();
        //    viewModel.CargarDatos(_DatosContext, CadenaBuscar, p);
        //    return View(viewModel);
        //}

        [AppAuthorize(AppRol.Supervisor, AppRol.Administrador, AppRol.Alumno, AppRol.Empresa, AppRol.Profesor)]
        public ActionResult ViewPerfil(Int32 UsuarioId)
        {
            var viewModel = new ViewPerfilViewModel();

            viewModel.CargarDatos(DatosContext, UsuarioId);
            viewModel.UrlReferrer = String.IsNullOrEmpty(viewModel.UrlReferrer) ? (String.IsNullOrEmpty(viewModel.UrlReferrer) ? null : Request.UrlReferrer.AbsoluteUri) : viewModel.UrlReferrer;

            //ViewBag.TotalCursosMaestros = cursosBL.ListarCursosMaestroUsuarioPorUsuarioId(viewModel.UsuarioId).Count;
            ViewBag.TotalCursosMaestrosInscritos = 0; // cursosBL.ListarCursosMaestroUsuarioPorUsuarioId(viewModel.UsuarioId).Count;
            ViewBag.TotalCursos = usuarioBL.TotalCursosActivos();
            //ViewBag.TotalCursosInscritos = 0;

            return View("ViewPerfilNew", viewModel);
        }


        [HttpPost]
        [ValidateAntiForgeryToken]
        [AppAuthorize(AppRol.Supervisor, AppRol.Administrador, AppRol.Alumno, AppRol.Empresa, AppRol.Profesor)]
        public ActionResult ViewPerfil(ViewPerfilViewModel model)
        {
            try
            {
                using (var TransactionScope = new TransactionScope())
                {
                    if (!ModelState.IsValid)
                    {
                        model.CargarDatos(DatosContext, model.UsuarioId);
                        TryUpdateModel(model);
                        PostMessage(MessageType.Error, String.Empty);
                        return View(model);
                    }

                    var Usuario = new Usuario();

                    //Usuario = context.Usuario.First(x => x.UsuarioId == model.UsuarioId);
                    Usuario = usuarioBL.ObtenerUsuarioPorId(model.UsuarioId);

                    /*if (!String.IsNullOrEmpty(model.Usuario.Codigo))
                        Usuario.Codigo = model.Usuario.Codigo;*/
                    if (!String.IsNullOrEmpty(model.Usuario.Nombre))
                    {
                        Usuario.Nombre = model.Usuario.Nombre;
                    }
                    if (!String.IsNullOrEmpty(model.Usuario.Apellido))
                    {
                        Usuario.Apellido = model.Usuario.Apellido;
                    }
                    if (model.Usuario.TipoDocumento!=0)
                    {
                        Usuario.TipoDocumento = model.Usuario.TipoDocumento;
                    }
                    if (!String.IsNullOrEmpty(model.Usuario.Telefono))
                    {
                        Usuario.Telefono = model.Usuario.Telefono;
                    }
                    /*if (!String.IsNullOrEmpty(model.Usuario.Email))
                        Usuario.Email = model.Usuario.Email;*/
                    if (!String.IsNullOrEmpty(model.Usuario.Password))
                    {
                        Usuario.Password = model.Usuario.Password;
                    }
                    /*if (!String.IsNullOrEmpty(model.Usuario.Grupo))
                        Usuario.Grupo = model.Usuario.Grupo;
                    if (!String.IsNullOrEmpty(model.Usuario.Cargo))
                        Usuario.Cargo = model.Usuario.Cargo;*/

                    Usuario.SexoId = model.Usuario.SexoId;
                    Usuario.NacionalidadId = model.Usuario.NacionalidadId;
                    Usuario.DistritoId = model.Usuario.DistritoId;
                    //Usuario.TestExportador = model.Usuario.TestExportador;

                    //context.SaveChanges();
                    usuarioBL.ActualizarUsuario(Usuario);

                    TransactionScope.Complete();

                    PostMessage(MessageType.Success);
                    return RedirectToAction("ViewPerfil", new { UsuarioId = model.UsuarioId });
                }
            }
            catch (Exception e)
            {
                //InvalidarContext();
                PostMessage(MessageType.Error);
                model.CargarDatos(DatosContext, model.UsuarioId);
                TryUpdateModel(model);
                return View(model);
            }
        }

        [AppAuthorize(AppRol.Administrador, AppRol.Profesor)]
        public ActionResult ViewAccesos(Int32? p, Int32 UsuarioId)
        {
            var model = new ViewAccesosViewModel();
            model.p = p ?? 1;
            model.UsuarioId = UsuarioId;
            model.Usuario = usuarioBL.ObtenerUsuarioPorId(UsuarioId);

            var queryUsuario = usuarioBL.ObtenerLogAccesosUsuario(UsuarioId);
            model.LstAccesos = queryUsuario.OrderByDescending(x => x.FechaAcceso).ToPagedList(model.p, ConstantHelpers.DEFAULT_PAGE_SIZE * 2);

            model.CargarDatos(DatosContext, p, UsuarioId);
            return View("ViewAccesosNew", model);
        }


        public ContentResult ActualizarCargo(Usuario usuario)
        {


            ViewModel.Asesoria.RespuestaJsonViewModel retVal = new ViewModel.Asesoria.RespuestaJsonViewModel();
            int UsuarioId = DatosContext.Session.GetUsuarioId();
            usuario.UsuarioId = UsuarioId;
            try
            {
                usuarioBL.ActualizarUsuarioCargo(usuario);
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
