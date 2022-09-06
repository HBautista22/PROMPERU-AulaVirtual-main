//using PagedList;
using PROMPERU.AULAVIRTUAL.WEB.Controllers;
using PROMPERU.AULAVIRTUAL.WEB.Helpers;
using PROMPERU.AULAVIRTUAL.WEB.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
//using System.Data.Entity;
using System.ComponentModel.DataAnnotations;
using PagedList;
using PROMPERU.AULAVIRTUAL.BE.CURSOS;
using PROMPERU.AULAVIRTUAL.BL;

namespace PROMPERU.AULAVIRTUAL.WEB.ViewModel.Report
{
    public class ListCursoOnlineViewModel
    {
        public Int32? p { get; set; }
        public IPagedList<CursoOnline> LstCursoOnline { get; set; }
        public Dictionary<Int32, Int32> DictAlumnosCursoOnline { get; set; }
        [Display(Name = "Nombre del curso")]
        public String NombreCurso { get; set; }

        CursosBL cursosBL = new CursosBL();

        public void CargarDatos(CargarDatosContext dataContext, Int32? p)
        {
            this.p = p ?? 1;
            var empresaId = dataContext.Session.GetEmpresaId();
            var fechaInicioMatriculaVigente = ConstantHelpers.GET_FECHA_INICIO_MATRICULA_VIGENTE();

            //var query = dataContext.context.MatriculaCursoOnline.Where(x => x.CursoOnline.Estado == ConstantHelpers.ESTADO.ACTIVO && x.Usuario.EmpresaId == empresaId && x.FechaAprobado != null).AsQueryable();
            var query = cursosBL.ListarMatriculaCursoOnlinePorEmpresaId(empresaId).Where(x => x.CursoOnline.Estado == ConstantHelpers.ESTADO.ACTIVO && x.FechaAprobado != null).AsQueryable();

            if (!String.IsNullOrEmpty(NombreCurso))
            {
                query = query.Where(x => x.CursoOnline.Nombre.Contains(NombreCurso));
            }

            var queryCursoOnline = query.Select(x => x.CursoOnline).Distinct().OrderBy(x => x.Nombre);

            LstCursoOnline = queryCursoOnline.ToPagedList(this.p.Value, ConstantHelpers.DEFAULT_PAGE_SIZE);

            DictAlumnosCursoOnline = query.GroupBy(x => x.CursoOnline).ToDictionary(x => x.Key.CursoOnlineId, x => x.Count());
        }
    }
}