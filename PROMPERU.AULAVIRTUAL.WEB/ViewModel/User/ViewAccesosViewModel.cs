using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using PROMPERU.AULAVIRTUAL.WEB.Controllers;
using PROMPERU.AULAVIRTUAL.WEB.Helpers;
using PagedList;
using PROMPERU.AULAVIRTUAL.BE.USUARIO;
using PROMPERU.AULAVIRTUAL.WEB.Models;

namespace PROMPERU.AULAVIRTUAL.WEB.ViewModel.User
{
    public class ViewAccesosViewModel
    {
        public int CadenaBuscar { get; set; }
        public IPagedList<LogAccesosUsuario> LstAccesos { get; set; }
        public int p { get; set; }
        public Usuario Usuario { get; set; }
        public int UsuarioId { get; set; }

        internal void CargarDatos(CargarDatosContext cd, int? p, int usuarioId)
        {
            //this.p = p ?? 1;
            //this.UsuarioId = usuarioId;

            //this.Usuario = cd.context.Usuario.Find(usuarioId);

            //var queryUsuario = cd.context.LogAccesosUsuario.Where(x => x.UsuarioId == usuarioId).AsQueryable();
            //this.LstAccesos = queryUsuario.OrderByDescending(x => x.FechaAcceso).ToPagedList(this.p, ConstantHelpers.DEFAULT_PAGE_SIZE * 2);
        }
    }
}