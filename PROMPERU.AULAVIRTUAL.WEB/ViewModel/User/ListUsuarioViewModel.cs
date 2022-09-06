using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using PROMPERU.AULAVIRTUAL.BE.USUARIO;
using PROMPERU.AULAVIRTUAL.WEB.Controllers;
using PROMPERU.AULAVIRTUAL.WEB.Helpers;
//using PROMPERU.AULAVIRTUAL.Helpers;
//using System.Data.Entity;
using PagedList;
using System.ComponentModel.DataAnnotations;


namespace PROMPERU.AULAVIRTUAL.WEB.ViewModel.User
{
    public class ListUsuarioViewModel
    {
        public Int32? p { get; set; }
        public IPagedList<Usuario> LstUsuario { get; set; }

        [Display(Name = "Buscar Usuario")]
        public String CadenaBuscar { get; set; }

        [Display(Name = "Buscar por Email")]
        public String EmailBuscar { get; set; }

        [Display(Name = "Buscar por Dni")]
        public String DniBuscar { get; set; }

        [Display(Name = "Buscar por RUC")]
        public String RucBuscar { get; set; }

        [Display(Name = "Empresa")]
        public Int32? EmpresaId { get; set; }

        public List<Empresa> LstEmpresa { get; set; }

        public String MarcaTodo { get; set; }


        public String MarcaAlgunos { get; set; }


        [Display(Name = "Rol")]
        public string RolId { get; set; }

        public List<BaseModel> LstRol { get; set; }

        [Display(Name = "Estado")]
        public string EstadoId { get; set; }

        public List<BaseModel> LstEstado { get; set; }



        public ListUsuarioViewModel()
        {
            LstRol = new List<BaseModel>
            {
                new BaseModel(ConstantHelpers.ROL.ALUMNO,"ALUMNO"),
                new BaseModel(ConstantHelpers.ROL.EMPRESA,"EMPRESA"),
            };

            LstEstado = new List<BaseModel>
            {
                new BaseModel(ConstantHelpers.ESTADO.ACTIVO,"ACTIVO"),
                new BaseModel(ConstantHelpers.ESTADO.INACTIVO,"INACTIVO"),
                new BaseModel(ConstantHelpers.ESTADO.PENDIENTE,"PENDIENTE"),
            };

        }

        //public void CargarDatos(CargarDatosContext dataContext, Int32? p, String MarcaTodo, String cadenaBuscar, Int32? empresaId, String emailBuscar, String dniBuscar, String RolId, String EstadoId)
        //{
        //    this.p = p ?? 1;
        //    this.CadenaBuscar = cadenaBuscar;
        //    this.EmailBuscar = emailBuscar;
        //    this.DniBuscar = dniBuscar;
        //    this.MarcaTodo = MarcaTodo;

        //    this.LstEmpresa = dataContext.context.Empresa.Where(x => !x.RUC.Contains("e")).OrderBy(x => x.RazonSocial).ToList();



            
        //    if (Session.GetRolCompleto() == ConstantHelpers.ROL.SUPERVISOR)
        //    {

                
        //        empresaId = dataContext.session.GetEmpresaId();
        //        LstEmpresa = LstEmpresa.Where(x => x.EmpresaId == empresaId).ToList();
        //    }

        //    var query = dataContext.context.Usuario.Where(x => !x.Estado.Equals(ConstantHelpers.ESTADO.ELIMINADO) && (x.Rol.Equals(ConstantHelpers.ROL.ALUMNO) || x.Rol.Equals(ConstantHelpers.ROL.EMPRESA))).AsQueryable();

        //    if (empresaId.HasValue)
        //    {
        //        this.EmpresaId = empresaId;
        //        query = query.Where(x => x.EmpresaId == EmpresaId);
        //    }



        //    if (!String.IsNullOrEmpty(CadenaBuscar))
        //    {
        //        foreach (var token in CadenaBuscar.Split(' '))
        //            query = query.Where(x => x.Nombre.Contains(token) || x.Apellido.Contains(token));
        //    }

        //    if (!String.IsNullOrEmpty(EmailBuscar))
        //    {
        //        foreach (var token in EmailBuscar.Split(' '))
        //            query = query.Where(x => x.Email.Contains(token));
        //    }

        //    if (!String.IsNullOrEmpty(DniBuscar))
        //    {
        //        foreach (var token in DniBuscar.Split(' '))
        //            query = query.Where(x => x.DNI.Contains(token));
        //    }

        //    if (!String.IsNullOrEmpty(RolId))
        //    {
        //        foreach (var token in RolId.Split(' '))
        //            query = query.Where(x => x.Rol.Contains(token));
        //    }

        //    if (!String.IsNullOrEmpty(EstadoId))
        //    {
        //        foreach (var token in EstadoId.Split(' '))
        //            query = query.Where(x => x.Estado.Contains(token));
        //    }

        //    //query = query.OrderBy(x => x.Apellido).ThenBy(x => x.Nombre);
        //    query = query.OrderByDescending(x => x.UsuarioId);

        //    //aqui llenamos la session
        //    var query2 = query.Where(x => x.Estado.Contains(ConstantHelpers.ESTADO.ACTIVO));
        //    HttpContext.Current.Session["TotalId"] = query2.Select(x => x.UsuarioId).ToJson();

        //    LstUsuario = query.ToPagedList(this.p, ConstantHelpers.DEFAULT_PAGE_SIZE);
        //}


    }
}
