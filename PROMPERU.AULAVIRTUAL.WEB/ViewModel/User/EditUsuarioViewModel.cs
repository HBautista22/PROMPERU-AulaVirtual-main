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
    public class EditUsuarioViewModel
    {
        public Int32? UsuarioId { get; set; }

        [Display(Name = "Codigo")]
        [Required]
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
        [Required]
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
        [Display(Name = "Sexo")]

        public string Descripcion { get; set; }
        public string Avatar { get; set; }
        public Int32? SexoId { get; set; }

        public List<Empresa> LstEmpresa { get; set; }

        [Display(Name = "¿Esta Activo?")]
        [Required]
        public Boolean Estado { get; set; }

        public List<SelectListItem> LstRol { get; set; }

        public List<Grupo> LstGrupo { get; set; }

        public List<SelectListItem> LstSexo { get; set; }

        ParametroBL parametroBL = new ParametroBL();
        UsuarioBL usuarioBL = new UsuarioBL();
        EmpresaBL empresaBL = new EmpresaBL();

        public EditUsuarioViewModel()
        {
            LstRol = new List<SelectListItem>();
            LstEmpresa = new List<Empresa>();
            LstGrupo = new List<Grupo>();
            LstSexo = new List<SelectListItem>();
        }

        public void CargarDatos(CargarDatosContext dataContext, Int32? usuarioId)
        {
            this.UsuarioId = usuarioId;
            if (dataContext.Session.GetRolCompleto() == ConstantHelpers.ROL.ADMINISTRADOR)
            {
                LstRol.Add(new SelectListItem { Text = "Administrador", Value = ConstantHelpers.ROL.ADMINISTRADOR });
            }
            LstRol.Add(new SelectListItem { Text = "Supervisor", Value = ConstantHelpers.ROL.SUPERVISOR });
            LstRol.Add(new SelectListItem { Text = "Alumno", Value = ConstantHelpers.ROL.ALUMNO });
            LstRol.Add(new SelectListItem { Text = "Empresa", Value = ConstantHelpers.ROL.EMPRESA });


            List<Parametro> lstParametros = parametroBL.ListarParametro();

            foreach(Parametro par in lstParametros.Where(x => x.ParametroGrupoId == 5 && x.EsActivo).OrderBy(x => x.Orden).ToList())
            {
                LstSexo.Add(new SelectListItem { Text = par.Descripcion, Value = par.ParametroId.ToString() });
            }

            

            if (dataContext.Session.GetRolCompleto() == ConstantHelpers.ROL.SUPERVISOR)
            {
                //this.EmpresaId = dataContext.session.GetEmpresaId();
                // dataContext.context.Empresa.Where(x => x.EmpresaId == this.EmpresaId).ToList();
                this.EmpresaId = dataContext.Session.GetEmpresaId();
                this.LstEmpresa = empresaBL.ObtenerEmpresaPorEmpresaId((int)(this.EmpresaId));
            }
            else
            {
                this.LstEmpresa = empresaBL.ListarEmpresa(); //dataContext.context.Empresa.ToList();
            }

            LstGrupo = usuarioBL.ListarGrupo().Where(x => x.Estado == ConstantHelpers.ESTADO.ACTIVO).ToList(); //  dataContext.context.Grupo.SqlQuery("USP_GrupoXUsuario_LIS").ToList();

            if (usuarioId.HasValue)
            {

                var Grupo = usuarioBL.ObtenerUsuarioGrupoPorUsuarioId((int)usuarioId); //dataContext.context.UsuarioGrupo.Where(x => x.UsuarioId == usuarioId && x.Estado == ConstantHelpers.ESTADO.ACTIVO).FirstOrDefault();
                var Usuario = usuarioBL.ObtenerUsuarioPorId((int)usuarioId); //dataContext.context.Usuario.Find(usuarioId);


                this.UsuarioId = Usuario.UsuarioId;
                this.Codigo = Usuario.Codigo;
                this.Nombre = Usuario.Nombre;
                this.Apellido = Usuario.Apellido;
                this.Cargo = Usuario.Cargo;
                this.Grupo = Usuario.Grupo;
                this.Rol = Usuario.Rol;
                this.Email = Usuario.Email;
                this.Password = Usuario.Password;
                this.EmpresaId = Usuario.EmpresaId == 0 ? null : Usuario.EmpresaId;
                this.Estado = (Usuario.Estado == ConstantHelpers.ESTADO.ACTIVO);
                this.SexoId = Usuario.SexoId;
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

        }
    }
}




