using PROMPERU.AULAVIRTUAL.WEB.Controllers;
using PROMPERU.AULAVIRTUAL.WEB.Models;
using System;
using System.Collections.Generic;
using System.Linq;
//using System.Data.Entity;
using PROMPERU.AULAVIRTUAL.WEB.Helpers;
//using PagedList;

namespace PROMPERU.AULAVIRTUAL.WEB.ViewModel.Enrollment
{
    public class ListUsuarioMatriculadoViewModel
    {
        public Int32 CursoOnlineId { get; set; }
        //public IPagedList<MatriculaCursoOnline> LstMatriculaCursoOnline { get; set; }
        //public Int32? np { get; set; }
        //public String Nombre { get; set; }
        //public String Correo { get; set; }
        //public void CargarDatos(CargarDatosContext dataContext,Int32 cursoOnlineId,Int32? np)
        //{
        //    CursoOnlineId = cursoOnlineId;
        //    this.np = np ?? 1;
        //    var empresaId = dataContext.session.GetEmpresaId();
        //    var fechaInicioMatriculaVigente = ConstantHelpers.GET_FECHA_INICIO_MATRICULA_VIGENTE();
        //    var query = dataContext.context.MatriculaCursoOnline.Include(x=>x.Usuario).Where(x => x.CursoOnlineId == CursoOnlineId && x.Usuario.EmpresaId == empresaId && x.Estado != ConstantHelpers.ESTADO_MATRICULA.INACTIVO && x.FechaMatricula >= fechaInicioMatriculaVigente).AsQueryable();
        //    if (!String.IsNullOrEmpty(Nombre))
        //        query = query.Where(x => x.Usuario.Nombre.Contains(Nombre) || x.Usuario.Apellido.Contains(Nombre));
        //    if (!String.IsNullOrEmpty(Correo))
        //        query = query.Where(x => x.Usuario.Email.Contains(Correo));
        //    LstMatriculaCursoOnline = query.OrderBy(x=>x.Usuario.Apellido).ToPagedList(this.np.Value,ConstantHelpers.DEFAULT_PAGE_SIZE);

        //}
    }
}