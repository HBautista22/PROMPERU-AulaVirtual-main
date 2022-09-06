using PROMPERU.AULAVIRTUAL.WEB.Controllers;
using PROMPERU.AULAVIRTUAL.WEB.Helpers;
using PROMPERU.AULAVIRTUAL.BE.USUARIO;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using PROMPERU.AULAVIRTUAL.BL;

namespace PROMPERU.AULAVIRTUAL.WEB.ViewModel.Document
{
    public class ReleaseDocumentoViewModel
    {
        public Int32 EmpresaId { get; set; }
        public List<Usuario> LstUsuario { get; set; }

        public ReleaseDocumentoViewModel() { 
        }
        public void CargarDatos(CargarDatosContext dataContext, Int32 empresaId)
        {
            this.EmpresaId = EmpresaId;

            //LstUsuario = dataContext.context.Usuario.Where(x => x.EmpresaId == empresaId && !x.Estado.Equals(ConstantHelpers.ESTADO.ELIMINADO)).OrderBy(x => x.Nombre).ToList();

            UsuarioBL usuarioBL = new UsuarioBL();
            LstUsuario = usuarioBL.ObtenerUsuariosPorEmpresa(empresaId).Where(x=> x.Estado.Equals(ConstantHelpers.ESTADO.ELIMINADO)).OrderBy(x => x.Nombre).ToList();
        }
    }
}