using PagedList;
using PROMPERU.AULAVIRTUAL.BE.CURSOS;
using PROMPERU.AULAVIRTUAL.BE.USUARIO;
using PROMPERU.AULAVIRTUAL.BL;
using PROMPERU.AULAVIRTUAL.DA;
using PROMPERU.AULAVIRTUAL.Helpers;
using PROMPERU.AULAVIRTUAL.WEB.Filters;
using PROMPERU.AULAVIRTUAL.WEB.Helpers;
using PROMPERU.AULAVIRTUAL.WEB.Logic;
using PROMPERU.AULAVIRTUAL.WEB.ViewModel.Asesor;
using PROMPERU.AULAVIRTUAL.WEB.ViewModel.User;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web.Mvc;

namespace PROMPERU.AULAVIRTUAL.WEB.Controllers
{
    public class AsesorController : BaseController
    {
        AsesorBL asesorBL = new AsesorBL();
        UsuarioBL usuarioBL = new UsuarioBL();
        EmpresaBL empresaBL = new EmpresaBL();
        NotificacionesBL notificacionesBL = new NotificacionesBL();

        [AppAuthorize(AppRol.Administrador)]
        public ActionResult Index()
        {
            return RedirectToAction("ListAddAsesor");
        }

        #region LISTAR/AGREGARs Asesores
        [AppAuthorize(AppRol.Administrador)]
        public ActionResult ListAddAsesor(Int32? p, String CadenaBuscar, Int32? EmpresaId, String MarcaTodo, String EmailBuscar, String DniBuscar, String RolId, String EstadoId)
        {
            ListAddAsesorViewModel viewModel = new ListAddAsesorViewModel();


            //viewModel.CadenaBuscar = CadenaBuscar;
            viewModel.p = p;
            //viewModel.EmpresaId = EmpresaId;
            //viewModel.MarcaTodo = MarcaTodo;
            //viewModel.EmailBuscar = EmailBuscar;
            //viewModel.DniBuscar = DniBuscar;
            //viewModel.RolId = RolId;
            //viewModel.EstadoId = EstadoId;
            //viewModel.LstEmpresa = usuarioBL.ListarEmpresa();

            viewModel.UsuariosAsesores = usuarioBL.ListarUsuarioAsesor();
            viewModel.UsuarioId = DatosContext.Session.GetUsuarioId();
            viewModel.EmpresaId = DatosContext.Session.GetEmpresaId();
            viewModel.LstUsuario = viewModel.UsuariosAsesores.ToPagedList(viewModel.p ?? 1, ConstantHelpers.DEFAULT_PAGE_SIZE);

            viewModel.UsuariosAsesores = viewModel.UsuariosAsesores.OrderByDescending(x => x.UsuarioId).ToList();

            ParametrosDA parametrosDA = new ParametrosDA();

            viewModel.UsuarioAsesor = new RegisterAsesorViewModel();

            viewModel.UsuarioAsesor.LstGeneros = new List<AsesorBaseModel>
            {
                new AsesorBaseModel("MAL","Hombre"),
                new AsesorBaseModel("FEM","Mujer"),
                new AsesorBaseModel("OTH","Prefiero no decirlo")
            };

            viewModel.UsuarioAsesor.Nacionalidades = parametrosDA.ListarParametro();
            viewModel.UsuarioAsesor.LstSector = parametrosDA.ListarRutexSector();

            return View("ListAddAsesorNew", viewModel);
        }
        #endregion

        [HttpPost]
        [AppAuthorize(AppRol.Administrador)]
        public ActionResult SaveAsesor(ListAddAsesorViewModel modelComplex)
        {
            RegisterAsesorViewModel model = modelComplex.UsuarioAsesor;
            if (!ModelState.IsValid)
            {
                PostMessage(MessageType.Error, "Los campos no son correctos, " + ModelState.AllErrors());
                return RedirectToAction("ListAddAsesor");
            }

            UsuarioDA usuarioDA = new UsuarioDA();

            Usuario usuario = usuarioDA.ObtenerUsuarioPorEmail(model.Email);

            if (usuario.Codigo != null && model.Conversion == 0)
            {
                PostMessage(MessageType.Warning, "La cuenta de correo ya se encuentra registrada.");
                return RedirectToAction("ListAddAsesor");
            }

            usuario = new Usuario
            {
                Apellido = model.Apellidos,
                Cargo = model.Cargo,
                Codigo = model.Email,
                Email = model.Email,
                Estado = "ACT",
                Grupo = "asesor",
                Nombre = model.Nombres,
                //Password = RandomHelper.GenerateRandomPassword(10),
                Password = model.Password,
                Rol = "ASE",
                DNI = model.DNI,
                //TestExportador = model.TieneTest,
                TipoDocumento = (model.Nacionalidad == "DNI") ? 1 : 2,
                NacionalidadId = model.NacionalidadId2,
                Telefono = model.Telefono,
                Sector = String.Join("|", model.Sector.Select(x => x.ToString()).ToArray())
            };

            try
            {
                int resultGrabarUsuario = usuarioBL.InsertarUsuario(usuario);

            }
            catch (Exception ex)
            {
                Util.SaveErrorLog(ex);
                throw new Exception(ex.Message);
            }

            PostMessage(MessageType.Success);

            return RedirectToAction("ListAddAsesor");
        }

        [AppAuthorize(AppRol.Administrador)]
        public ActionResult EditAsesor(int id)
        {
            RegisterAsesorViewModel viewModel = new RegisterAsesorViewModel();
            Usuario usuario = usuarioBL.ObtenerUsuarioPorId(id);

            viewModel.LstGeneros = new List<AsesorBaseModel>
            {
                new AsesorBaseModel("MAL","Hombre"),
                new AsesorBaseModel("FEM","Mujer"),
                new AsesorBaseModel("OTH","Prefiero no decirlo")
            };

            ParametrosDA parametrosDA = new ParametrosDA();
            viewModel.Nacionalidades = parametrosDA.ListarParametro();
            viewModel.LstSector = parametrosDA.ListarRutexSector();

            viewModel.UsuarioId = usuario.UsuarioId;
            //model.Nacionalidad = usuario.Nacionalidad;
            //model.Conversion = usuario.Conversion;


            viewModel.Apellidos = usuario.Apellido;
            viewModel.Cargo = usuario.Cargo;
            viewModel.Email = usuario.Email;
            viewModel.ConfirmarEmail = usuario.Email;
            viewModel.Nombres = usuario.Nombre;
            viewModel.Password = usuario.Password;
            viewModel.DNI = usuario.DNI;
            viewModel.Nacionalidad = ((usuario.TipoDocumento ?? 0) == 1) ? "DNI" : "OTROS";
            viewModel.NacionalidadId2 = usuario.NacionalidadId ?? 0;
            viewModel.Telefono = usuario.Telefono;
            viewModel.Sector = usuario.Sector.Split('|').Select(Int32.Parse).ToArray();
            
            return View("EditAsesorNew", viewModel);
        }

        [AppAuthorize(AppRol.Administrador)]
        public ActionResult ChangeStateAsesor(int id)
        {
            RegisterAsesorViewModel viewModel = new RegisterAsesorViewModel();
            Usuario usuario = usuarioBL.ObtenerUsuarioPorId(id);


            if (usuario.Estado == "ACT")
            {
                usuario.Estado = "INA";
            }
            else
            {
                usuario.Estado = "ACT";
            }

            usuarioBL.ActualizarUsuarioEstado(id, usuario.Estado);
            return RedirectToAction("ListAddAsesor");
        }




        [HttpPost]
        [AppAuthorize(AppRol.Alumno, AppRol.Supervisor, AppRol.Administrador, AppRol.Empresa, AppRol.Ingeniero, AppRol.Proveedor)]
        public ActionResult UpdateAsesor(RegisterAsesorViewModel model)
        {
            bool GrabacionExitosa = false;

            if (!ModelState.IsValid)
            {
                PostMessage(MessageType.Error, "Los campos no son correctos, " + ModelState.AllErrors());
                return RedirectToAction("ListAddAsesor");
            }

            UsuarioDA usuarioDA = new UsuarioDA();

            Usuario usuario = usuarioDA.ObtenerUsuarioPorId(model.UsuarioId);

            if (usuario.Codigo == null)
            {
                PostMessage(MessageType.Warning, "La cuenta no existe.");
                return RedirectToAction("ListAddAsesor");
            }

            usuario = new Usuario();

            usuario.UsuarioId = model.UsuarioId;
            usuario.Apellido = model.Apellidos;
            usuario.Cargo = model.Cargo;
            usuario.Codigo = model.Email;
            usuario.Email = model.Email;
            usuario.Estado = "ACT";
            usuario.Grupo = "asesor";
            usuario.Nombre = model.Nombres;
            usuario.Password = model.Password == "" ? usuario.Password : model.Password;
            usuario.Rol = "ASE";
            usuario.DNI = model.DNI;
            usuario.TipoDocumento = (model.Nacionalidad == "DNI") ? 1 : 2;
            usuario.NacionalidadId = model.NacionalidadId2;
            usuario.Telefono = model.Telefono;
            usuario.Sector = String.Join("|", model.Sector.Select(x => x.ToString()).ToArray());

            //Valores que no se usan
            usuario.EmpresaId = null;
            usuario.SexoId = null;
            usuario.Conversion = null;
            try
            {
                int resultGrabarUsuario = usuarioBL.ActualizarUsuario(usuario);
                GrabacionExitosa = true;
            }
            catch (Exception ex)
            {
                Util.SaveErrorLog(ex);
                throw new Exception(ex.Message);
            }



            if (GrabacionExitosa == true)
            {
                PostMessage(MessageType.Success);
                return RedirectToAction("ListAddAsesor");
            }
            else
            {
                PostMessage(MessageType.Error);
                return RedirectToAction("EditAsesor", new { id = model.UsuarioId });
            }
        }
    }
}
