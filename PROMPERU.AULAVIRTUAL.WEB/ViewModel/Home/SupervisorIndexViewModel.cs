using PROMPERU.AULAVIRTUAL.WEB.Controllers;
using PROMPERU.AULAVIRTUAL.BE.CURSOS;
using PROMPERU.AULAVIRTUAL.WEB.Helpers;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;


namespace PROMPERU.AULAVIRTUAL.WEB.ViewModel.Home
{
    public class SupervisorIndexViewModel
    {
        public Int32 UsuarioId { get; set; }
        public Int32 EmpresaId { get; set; }
        public List<MatriculaCursoOnline> LstMatriculaActual { get; set; }
        public List<DisponibilidadCursoOnline> LstCursosDisponibles { get; set; }
        public Int32 CantidadUsuariosMatriculados { get; set; }
        public Int32 CantidadUsuariosActivos { get; set; }
        public Int32 CantidadCursosDisponibles { get; set; }

        public SupervisorIndexViewModel()
        {

        }

        public void CargarDatos(CargarDatosContext dataContext)
        {
            //this.UsuarioId = dataContext.session.GetUsuarioId();
            //this.EmpresaId = dataContext.session.GetEmpresaId();

            //var fechaMatriculaVigente = ConstantHelpers.GET_FECHA_INICIO_MATRICULA_VIGENTE();

            //var queryMatricula = dataContext.context.MatriculaCursoOnline.Include(x => x.CursoOnline).Where(x => x.UsuarioId == UsuarioId && x.Estado != ConstantHelpers.ESTADO_MATRICULA.DESAPROBADO && x.Estado != ConstantHelpers.ESTADO_MATRICULA.INACTIVO).OrderByDescending(x => x.FechaMatricula).AsQueryable();

            //LstMatriculaActual = queryMatricula.Where(x => (x.Estado == ConstantHelpers.ESTADO_MATRICULA.PENDIENTE && x.FechaMatricula >= fechaMatriculaVigente) || x.Estado == ConstantHelpers.ESTADO_MATRICULA.COMPLETADO || x.Estado == ConstantHelpers.ESTADO_MATRICULA.APROBADO).Take(ConstantHelpers.DASHBOARD.MAX_ITEMS_CURSO).ToList();

            //var cursosMatriculadosId = this.LstMatriculaActual.Select(x => x.CursoOnlineId).ToList();

            //var queryCursosDisponibles = dataContext.context.DisponibilidadCursoOnline.Include(x => x.CursoOnline).Where(x => x.CursoOnline.Estado == ConstantHelpers.ESTADO.ACTIVO && (x.FechaFin == null || x.FechaFin > DateTime.Now) && !cursosMatriculadosId.Any(c => c == x.CursoOnlineId)).AsQueryable();

            //LstCursosDisponibles = queryCursosDisponibles.Take(ConstantHelpers.DASHBOARD.MAX_ITEMS_CURSO).ToList();

            //CantidadCursosDisponibles = queryCursosDisponibles.Count();
            //CantidadUsuariosMatriculados = dataContext.context.MatriculaCursoOnline.Include(x => x.Usuario).Where(x => x.Usuario.EmpresaId == EmpresaId && x.Estado != ConstantHelpers.ESTADO_MATRICULA.INACTIVO && x.FechaMatricula >= fechaMatriculaVigente).Count();
            //CantidadUsuariosActivos = dataContext.context.Usuario.Where(x => x.Estado == ConstantHelpers.ESTADO.ACTIVO && x.EmpresaId == EmpresaId).Count();

        }
    }
}