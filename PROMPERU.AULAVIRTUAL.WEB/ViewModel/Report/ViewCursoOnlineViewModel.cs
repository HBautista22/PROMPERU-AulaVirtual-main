using PROMPERU.AULAVIRTUAL.WEB.Controllers;
using PROMPERU.AULAVIRTUAL.WEB.Helpers;
using PROMPERU.AULAVIRTUAL.BE.CURSOS;

using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
//using System.Data.Entity;
using PagedList;
using PROMPERU.AULAVIRTUAL.BL;

namespace PROMPERU.AULAVIRTUAL.WEB.ViewModel.Report
{
    public class ViewCursoOnlineViewModel
    {
        public IPagedList<MatriculaCursoOnline> LstMatriculaCursoOnline { get; set; }
        public Int32? p { get; set; }
        public Int32 CursoOnlineId { get; set; }
        public CursoOnline CursoOnline { get; set; }

        public String Nombre { get; set; }

        CursosBL cursosBL = new CursosBL();

        public void CargarDatos(CargarDatosContext dataContext, Int32 cursoOnlineId, Int32? p, String nombre)
        {
            this.p = p ?? 1;
            CursoOnlineId = cursoOnlineId;
            Nombre = nombre;

            CursoOnline = new CursoOnline(); // dataContext.context.CursoOnline.Find(cursoOnlineId);
            CursoOnline = cursosBL.ListarCursoOnline().First(x=>x.CursoOnlineId == cursoOnlineId);


            var empresaId = dataContext.Session.GetEmpresaId();
            var fechaInicioMatriculaVigente = ConstantHelpers.GET_FECHA_INICIO_MATRICULA_VIGENTE();
            // dataContext.context.MatriculaCursoOnline.Include(x => x.Usuario).Include(x => x.CursoOnline).Where(
            //x => x.CursoOnlineId == CursoOnlineId && x.Usuario.EmpresaId == empresaId && x.FechaAprobado != null).AsQueryable();

            //var query = new List<MatriculaCursoOnline>(); 
            var query = cursosBL.ListarMatriculaCursoOnlinePorEmpresaId(empresaId).Where(x => x.CursoOnlineId == CursoOnlineId && x.FechaAprobado != null).AsQueryable();


            if (!String.IsNullOrEmpty(Nombre))
            {
                foreach (var token in Nombre.Split(' '))
                {
                    query =  query.Where(x => x.Usuario.Nombre.Contains(token) || x.Usuario.Apellido.Contains(token) || x.Usuario.Email.Contains(token));
                }
            }

            LstMatriculaCursoOnline = query.OrderBy(x => x.Usuario.Apellido).ThenBy(x => x.Usuario.Nombre).ToPagedList(this.p.Value, ConstantHelpers.DEFAULT_PAGE_SIZE);
        }
    }
}