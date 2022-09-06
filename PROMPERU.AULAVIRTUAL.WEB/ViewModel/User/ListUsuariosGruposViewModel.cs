//using PagedList;
using PROMPERU.AULAVIRTUAL.WEB.Controllers;
using PROMPERU.AULAVIRTUAL.WEB.Helpers;
using PROMPERU.AULAVIRTUAL.BE.USUARIO;
using PROMPERU.AULAVIRTUAL.BE.CURSOS;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Web;
using PagedList;

namespace PROMPERU.AULAVIRTUAL.WEB.ViewModel.User
{
    public class ListUsuariosGruposViewModel
    {
        public Int32 p { get; set; }
        [Display(Name = "Buscar Grupo")]
        public String CadenaBuscar { get; set; }
        public IPagedList<Grupo> LstUsuario { get; set; }
        public List<UsuarioGrupo> LstUsuarioGrupo { get; set; }
        public int[] CantidadRegistros { get; set; }

        public List<BaseModel> LstTipoGrupo { get; set; }
        public String TipoGrupo { get; set; }
        public ListUsuariosGruposViewModel()
        {
            LstTipoGrupo = new List<BaseModel>
            {
                new BaseModel(ConstantHelpers.ROL.ALUMNO,"ALUMNO"),
                new BaseModel(ConstantHelpers.ROL.EMPRESA,"EMPRESA"),
                new BaseModel("GNR","GENERAL"),
            };
        }

        //public void CargarDatos(CargarDatosContext dataContext, String cadenaBuscar, Int32? p)
        //{
        //    Int32 empresaId = dataContext.session.GetEmpresaId();
        //    Int32 usuarioId = dataContext.session.GetUsuarioId();
        //    this.p = p ?? 1;

        //    this.CadenaBuscar = cadenaBuscar;
        //    IQueryable<Grupo> queryUsuario = dataContext.context.Grupo.Where(x => !x.Estado.Equals(ConstantHelpers.ESTADO.ELIMINADO)).AsQueryable();

        //    if (!String.IsNullOrEmpty(CadenaBuscar))
        //    {
        //        foreach (var token in CadenaBuscar.Split(' '))
        //            queryUsuario = queryUsuario.Where(x => x.Descripcion.Contains(token));
        //    }

        //    CantidadRegistros = new int[queryUsuario.Select(x => x.GrupoId).Max() + 1];

        //    foreach (var item in queryUsuario.Select(x => x.GrupoId))
        //    {
        //        CantidadRegistros[item] = dataContext.context.UsuarioGrupo.Where(x => x.Estado == ConstantHelpers.ESTADO.ACTIVO && x.GrupoId == item).Count();
        //    }

        //    queryUsuario = queryUsuario.OrderByDescending(x => x.FechaCreacion);

        //    LstUsuario = queryUsuario.ToPagedList(this.p, ConstantHelpers.DEFAULT_PAGE_SIZE);
        //}
    }
}