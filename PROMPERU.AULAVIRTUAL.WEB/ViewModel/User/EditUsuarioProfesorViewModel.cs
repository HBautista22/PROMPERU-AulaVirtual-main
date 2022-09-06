using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using PROMPERU.AULAVIRTUAL.BE.USUARIO;
using PROMPERU.AULAVIRTUAL.BE.CURSOS;
using PROMPERU.AULAVIRTUAL.WEB.Controllers;
using PROMPERU.AULAVIRTUAL.WEB.Helpers;
using System.ComponentModel.DataAnnotations;
//using System.Data.Entity;
using System.Web.Mvc;
using PROMPERU.AULAVIRTUAL.BE.PARAMETROS;
using PROMPERU.AULAVIRTUAL.BL;

namespace PROMPERU.AULAVIRTUAL.WEB.ViewModel.User
{
    public class EditUsuarioProfesorViewModel
    {
        public Int32? UsuarioId { get; set; }

        [Display(Name = "Codigo")]
        //[Required]
        public String Codigo { get; set; }
        [Display(Name = "Nombres")]
        [Required]
        public String Nombre { get; set; }
        [Display(Name = "Apellidos")]
        [Required]
        public String Apellido { get; set; }
        [Display(Name = "Cargo")]
        public String Cargo { get; set; }
        [Display(Name = "Grupo")]
        public String Grupo { get; set; }
        [Display(Name = "Rol")]
        //[Required]
        public String Rol { get; set; }
        [Display(Name = "Email")]
        public String Email { get; set; }
        [Display(Name = "Password")]
        [Required]
        public String Password { get; set; }
        [Display(Name = "Empresa")]
        public Int32? EmpresaId { get; set; }
        [Display(Name = "Grupo")]
        public Int32? GrupoId { get; set; }

        [Display(Name = "Avatar")]
        public String Avatar { get; set; }

        [Display(Name = "Descripción")]
        public String Descripcion { get; set; }

        public List<Empresa> LstEmpresa { get; set; }

        [Display(Name = "¿Esta Activo?")]
        [Required]
        public Boolean Estado { get; set; }

        public List<SelectListItem> LstRol { get; set; }

        public List<Grupo> LstGrupo { get; set; }

        public List<Departamento> LstDepartamento { get; set; }
        public List<Provincia> LstProvincia { get; set; }
        public List<Distrito> LstDistrito { get; set; }

        public int Region { get; set; }
        public int Provincia { get; set; }
        public int Departamento { get; set; }
        UsuarioBL usuarioBL = new UsuarioBL();
        EmpresaBL empresaBL = new EmpresaBL();
        ParametroBL parametro = new ParametroBL();
        public EditUsuarioProfesorViewModel()
        {
            LstRol = new List<SelectListItem>();
            LstEmpresa = new List<Empresa>();
            LstGrupo = new List<Grupo>();

            LstDepartamento = parametro.ListarDepartamento().ToList();

        }
        public void CargarDatos(CargarDatosContext dataContext, Int32? usuarioId)
        {
            this.UsuarioId = usuarioId;
            if (dataContext.Session.GetRolCompleto() == ConstantHelpers.ROL.ADMINISTRADOR)
                LstRol.Add(new SelectListItem { Text = "Administrador", Value = ConstantHelpers.ROL.ADMINISTRADOR });
            LstRol.Add(new SelectListItem { Text = "Supervisor", Value = ConstantHelpers.ROL.SUPERVISOR });
            LstRol.Add(new SelectListItem { Text = "Alumno", Value = ConstantHelpers.ROL.ALUMNO });
            LstRol.Add(new SelectListItem { Text = "Empresa", Value = ConstantHelpers.ROL.EMPRESA });

            if (dataContext.Session.GetRolCompleto() == ConstantHelpers.ROL.SUPERVISOR)
            {
                this.EmpresaId = dataContext.Session.GetEmpresaId();
                LstEmpresa = empresaBL.ObtenerEmpresaPorEmpresaId((int)(this.EmpresaId));
            }
            else
            {
                LstEmpresa = empresaBL.ListarEmpresa();
            }

            LstGrupo = usuarioBL.ListarGrupo().Where(x => x.Estado == ConstantHelpers.ESTADO.ACTIVO).ToList();


            if (usuarioId.HasValue)
            {

                var Grupo = usuarioBL.ObtenerUsuarioGrupoPorUsuarioId((int)usuarioId); //dataContext.context.UsuarioGrupo.Where(x => x.UsuarioId == usuarioId && x.Estado == ConstantHelpers.ESTADO.ACTIVO).FirstOrDefault();
                var Usuario = usuarioBL.ObtenerUsuarioPorId((int)usuarioId); //dataContext.context.Usuario.Find(usuarioId);



                var dep = 0;
                var prov = 0;

                this.UsuarioId = Usuario.UsuarioId;
                this.Codigo = Usuario.Codigo;
                this.Nombre = Usuario.Nombre;
                this.Apellido = Usuario.Apellido;
                this.Cargo = Usuario.Cargo;
                this.Grupo = Usuario.Grupo;
                this.Rol = Usuario.Rol;
                this.Email = Usuario.Email;
                this.Password = Usuario.Password;
                this.EmpresaId = Usuario.EmpresaId;
                this.Region = Usuario.DistritoId == null ? 0 : (int)Usuario.DistritoId;
                this.Estado = (Usuario.Estado == ConstantHelpers.ESTADO.ACTIVO);

                
                this.Departamento = parametro.ListarDistrito().Where(x => x.DistritoId == (int)Usuario.DistritoId).Select(x => x.DepartamentoId).FirstOrDefault();
                this.Provincia = parametro.ListarDistrito().Where(x => x.DistritoId == (int)Usuario.DistritoId).Select(x => x.ProvinciaId).FirstOrDefault();

                dep = parametro.ListarDistrito().Where(x => x.DistritoId == (int)Usuario.DistritoId).Select(x => x.DepartamentoId).FirstOrDefault();
                prov = parametro.ListarDistrito().Where(x => x.DistritoId == (int)Usuario.DistritoId).Select(x => x.ProvinciaId).FirstOrDefault();

                this.LstProvincia =  parametro.ListarProvincia().Where(x => x.DepartamentoId == dep).ToList();
                this.LstDistrito = parametro.ListarDistrito().Where(x => x.ProvinciaId == prov).ToList();

                this.Avatar = Usuario.Avatar;
                this.Descripcion = Usuario.Descripcion;

                if (Grupo != null)
                {
                    this.GrupoId = Grupo.GrupoId;
                }
                else
                {
                    this.GrupoId = 0;
                }


            }
            else
            {

                this.LstProvincia = parametro.ListarProvincia().Where(x => x.DepartamentoId == 0).ToList();
                this.LstDistrito = parametro.ListarDistrito().Where(x => x.ProvinciaId == 0).ToList();
            }


        }
    }
}