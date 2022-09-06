//using PagedList;
using PROMPERU.AULAVIRTUAL.WEB.Controllers;
using PROMPERU.AULAVIRTUAL.WEB.Helpers;
using PROMPERU.AULAVIRTUAL.WEB.Models;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Web;

namespace PROMPERU.AULAVIRTUAL.WEB.ViewModel.User
{
    public class ListUsuariosEmpresaViewModel
    {
        public Int32 p { get; set; }
        [Display(Name = "Buscar Usuario")]
        public String CadenaBuscar { get; set; }
        //public IPagedList<Usuario> LstUsuario { get; set; }

        //public ListUsuariosEmpresaViewModel()
        //{

        //}

        //public void CargarDatos(CargarDatosContext dataContext, String cadenaBuscar, Int32? p)
        //{
        //    Int32 empresaId = dataContext.session.GetEmpresaId();
        //    Int32 usuarioId = dataContext.session.GetUsuarioId();
        //    this.p = p ?? 1;


        //    this.CadenaBuscar = cadenaBuscar;
        //    IQueryable<Usuario> queryUsuario = dataContext.context.Usuario.Where(x => x.EmpresaId == empresaId && x.UsuarioId != usuarioId && !x.Estado.Equals(ConstantHelpers.ESTADO.ELIMINADO)).AsQueryable();

        //    if (!String.IsNullOrEmpty(CadenaBuscar))
        //    {
        //        foreach (var token in CadenaBuscar.Split(' '))
        //            queryUsuario = queryUsuario.Where(x => x.Nombre.Contains(token) || x.Apellido.Contains(token));
        //    }

        //    queryUsuario = queryUsuario.OrderBy(x => x.Apellido).ThenBy(x => x.Nombre);
        //    LstUsuario = queryUsuario.ToPagedList(this.p, ConstantHelpers.DEFAULT_PAGE_SIZE);
        //}
    }
}