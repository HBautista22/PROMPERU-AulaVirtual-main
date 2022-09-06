using PagedList;
using PROMPERU.AULAVIRTUAL.BE.PARAMETROS;
using PROMPERU.AULAVIRTUAL.BE.USUARIO;
using PROMPERU.AULAVIRTUAL.WEB.ViewModel.User;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace PROMPERU.AULAVIRTUAL.WEB.ViewModel.Asesor
{
    public class ListAddAsesorViewModel
    {
        public Int32 UsuarioId { get; set; }
        public Int32 EmpresaId { get; set; }
        public IPagedList<Usuario> LstUsuario { get; set; }
        public List<Usuario> UsuariosAsesores { get; set; }
        public RegisterAsesorViewModel UsuarioAsesor { get; set; }
        public Int32? p { get; set; }

    }
}