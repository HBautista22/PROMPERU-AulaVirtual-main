using PROMPERU.AULAVIRTUAL.WEB.Controllers;
using PROMPERU.AULAVIRTUAL.WEB.Helpers;
using PROMPERU.AULAVIRTUAL.WEB.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.ComponentModel.DataAnnotations;
using PagedList;
using PROMPERU.AULAVIRTUAL.BL;
using PROMPERU.AULAVIRTUAL.BE.CURSOS;

namespace PROMPERU.AULAVIRTUAL.WEB.ViewModel.Report
{
    public class ReportHistoricoAlumnosViewModel
    {
        ReporteBL reporteBL = new ReporteBL();

        public IPagedList<ViewReporteHistoricoAlumno> LstViewReporteHistoricoAlumno { get; set; }
        public Int32? np { get; set; }
        [Display(Name = "Nombre / Código")]
        public String Codigo { get; set; }
        [Display(Name = "Razón Social / Ruc")]
        public String RazonSocial { get; set; }
        [Display(Name = "Curso")]
        public String Curso { get; set; }
        [Display(Name = "Cargo")]
        public String Cargo { get; set; }
        [Display(Name = "Grupo")]
        public String Grupo { get; set; }
        [Display(Name = "Estado")]
        public String Estado { get; set; }

        //agregado por oti
        [Display(Name = "DNI")]
        public String Dni { get; set; }

        [Display(Name = "EMAIL")]
        public String Email { get; set; }

        public void CargarDatos(CargarDatosContext dataContext, Int32? np)
        {
            this.np = np ?? 1;
            var empresaId = dataContext.Session.GetEmpresaId();
            //var query = dataContext.context.ViewReporteHistoricoAlumno.Where(x => x.Usuario_Estado == ConstantHelpers.ESTADO.ACTIVO).AsNoTracking().AsQueryable();
            var query = reporteBL.ListarViewReporteHistoricoAlumno().Where(x => x.Usuario_Estado == ConstantHelpers.ESTADO.ACTIVO); //dataContext.context.ViewReporteHistoricoAlumno.Where(x => x.Usuario_Estado == ConstantHelpers.ESTADO.ACTIVO).AsNoTracking().AsQueryable();

            if (!dataContext.Session.TieneRol(AppRol.Administrador))
            {
                query = query.Where(x => x.Usuario_EmpresaId == empresaId);
            }

            if (!String.IsNullOrEmpty(Codigo))
                query = query.Where(x => x.Usuario_Codigo.Contains(Codigo) || x.Usuario_NombreCompleto.Contains(Codigo));
            if (!String.IsNullOrEmpty(RazonSocial))
                query = query.Where(x => x.Empresa_RazonSocial.Contains(RazonSocial) || x.Empresa_RUC.Contains(RazonSocial));
            if (!String.IsNullOrEmpty(Curso))
                query = query.Where(x => x.Curso_Nombre.Contains(Curso));
            if (!String.IsNullOrEmpty(Cargo))
                query = query.Where(x => x.Usuario_Cargo.Contains(Cargo));
            if (!String.IsNullOrEmpty(Grupo))
                query = query.Where(x => x.Usuario_Grupo.Contains(Grupo));
            if (!String.IsNullOrEmpty(Estado))
                query = query.Where(x => x.Matricula_Estado.Contains(Estado));
            if (!String.IsNullOrEmpty(Email))
                query = query.Where(x => x.Usuario_Email.Contains(Email));

            LstViewReporteHistoricoAlumno = query.OrderBy(x => x.Usuario_NombreCompleto).ToPagedList(this.np.Value, ConstantHelpers.DEFAULT_PAGE_SIZE);

        }
    }
}