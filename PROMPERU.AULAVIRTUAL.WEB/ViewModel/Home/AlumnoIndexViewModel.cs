using PROMPERU.AULAVIRTUAL.BE.CURSOS;
using PROMPERU.AULAVIRTUAL.BL;
using PROMPERU.AULAVIRTUAL.WEB.Controllers;
using PROMPERU.AULAVIRTUAL.WEB.Helpers;
using System;
using System.Collections.Generic;
using System.Linq;
using PROMPERU.AULAVIRTUAL.BE.USUARIO;
using System.Web.Configuration;

namespace PROMPERU.AULAVIRTUAL.WEB.ViewModel.Home
{
    public class AlumnoIndexViewModel
    {
        private readonly CursosBL _cursosBl = new CursosBL();
        private readonly CursoOnlineFavoritoBL _cursoOnlineFavoritoBl = new CursoOnlineFavoritoBL();
        private readonly UsuarioBL _usuarioBl = new UsuarioBL();

        public int UsuarioId { get; set; }
        public int EmpresaId { get; set; }
        public List<MatriculaCursoOnline> LstMatriculaActual { get; set; }
        public List<DisponibilidadCursoOnline> LstCursosDisponibles { get; set; }

        public int MyProperty { get; set; }
        public int CantidadCursosAprobados { get; set; }
        public int CantidadCursosMaticulaActual { get; set; }
        public int CantidadCursosDisponibles { get; set; }
        public  List<ViewCertificadoAlumno> certificados { get; set; }
        public List<CategoriaCursoOnline> CategoriaCurso { get; set; }

        public List<CursoOnlineVisto> oCursoOnlineVisto { get; set; }
        public List<CursoOnlineFavorito> oCursoOnlineFavorito { get; set; }

        public Usuario Usuario { get; set; }

        public AlumnoIndexViewModel()
        {

        }

        public void CargarDatos(CargarDatosContext dataContext)
        {
            this.UsuarioId = dataContext.Session.GetUsuarioId();
            this.EmpresaId = dataContext.Session.GetEmpresaId();

            DateTime fechaMatriculaVigente = ConstantHelpers.GET_FECHA_INICIO_MATRICULA_VIGENTE();

            //var queryMatricula = alumnoBL.ListarMatriculaCursosOnline.MatriculaCursoOnline.Include(x => x.CursoOnline).Where(x => x.UsuarioId == UsuarioId && x.Estado != ConstantHelpers.ESTADO_MATRICULA.DESAPROBADO && x.Estado != ConstantHelpers.ESTADO_MATRICULA.INACTIVO).OrderByDescending(x => x.FechaMatricula).AsQueryable();
            //this.LstMatriculaActual = queryMatricula.Where(x => (x.Estado == ConstantHelpers.ESTADO_MATRICULA.PENDIENTE && x.FechaMatricula >= fechaMatriculaVigente) || x.Estado == ConstantHelpers.ESTADO_MATRICULA.COMPLETADO || x.Estado == ConstantHelpers.ESTADO_MATRICULA.APROBADO).Take(ConstantHelpers.DASHBOARD.MAX_ITEMS_CURSO).ToList();


            this.oCursoOnlineFavorito = _cursoOnlineFavoritoBl.ObtenerCursosFavoritos(this.UsuarioId);
            this.oCursoOnlineVisto = _cursosBl.ObtenerCursosVisto(this.UsuarioId);
            Usuario = _usuarioBl.ObtenerUsuarioPorId(UsuarioId);


            List<MatriculaCursoOnline> tmpMatricula = _cursosBl.ListarMatriculaCursoOnline(UsuarioId);
            
            this.LstMatriculaActual =
                tmpMatricula
                .Where
                (x => 
                    (x.Estado == ConstantHelpers.ESTADO_MATRICULA.PENDIENTE && x.FechaMatricula >= fechaMatriculaVigente) 
                    || x.Estado == ConstantHelpers.ESTADO_MATRICULA.COMPLETADO 
                    || x.Estado == ConstantHelpers.ESTADO_MATRICULA.APROBADO
                )
                .Take(ConstantHelpers.DASHBOARD.MAX_ITEMS_CURSO)
                .OrderByDescending(x => x.FechaMatricula)
                .ToList();

            List<int> cursosMatriculadosId = this.LstMatriculaActual.Select(x => x.CursoOnlineId).ToList();

            //var queryCursosDisponibles = dataContext.context.DisponibilidadCursoOnline.Include(x => x.CursoOnline).Where(x => x.CursoOnline.Estado == ConstantHelpers.ESTADO.ACTIVO && (x.FechaFin == null || x.FechaFin > DateTime.Now) && !cursosMatriculadosId.Any(c => c == x.CursoOnlineId) && x.CursoOnline.MatriculaCursoOnline.All(g => g.UsuarioId != UsuarioId)).AsQueryable();

            //this.LstCursosDisponibles = queryCursosDisponibles.Take(ConstantHelpers.DASHBOARD.MAX_ITEMS_CURSO).ToList();

            List<DisponibilidadCursoOnline> tmpDisponible = _cursosBl.ListarDisponibilidadCursoOnlineUsuarioId(UsuarioId);
            this.LstCursosDisponibles = tmpDisponible
                .Where
                (x => 
                    (x.FechaFin == null || x.FechaFin > DateTime.Now) 
                    && cursosMatriculadosId.All(c => c != x.CursoOnlineId))
                .OrderBy(x => x.FechaInicio)
                .ThenBy(x => x.CursoOnlineId)
                .Take(ConstantHelpers.DASHBOARD.MAX_ITEMS_CURSO)
                .ToList();

            bool isTurismo = bool.Parse(WebConfigurationManager.AppSettings["EsTurismo"]);

            this.CantidadCursosAprobados = tmpMatricula.Count(x => x.Estado == ConstantHelpers.ESTADO_MATRICULA.APROBADO);
            this.CantidadCursosMaticulaActual = LstMatriculaActual.Count();
            this.CantidadCursosDisponibles = tmpDisponible.Count();
            this.CategoriaCurso = _cursosBl.ListarCategoriaCursoOnline();

            if (isTurismo)
            {
                this.certificados = _cursosBl.ListarCertificadoTurismoAlumnoPorUsuarioId((int)this.UsuarioId).ToList();
            }
            else
            {
                this.certificados = _cursosBl.ListarCertificadoAlumnoPorUsuarioId((int)this.UsuarioId).ToList();// dataContext.context.ViewCertificadoAlumno.Where(x => x.UsuarioId == UsuarioId).AsQueryable();
            }

            foreach (MatriculaCursoOnline matricula in this.LstMatriculaActual)
            {
                matricula.CursoOnline.CategoriaCursoOnline = _cursosBl.ListarCategoriaCursoOnline().First(x => x.CategoriaCursoOnlineId == matricula.CursoOnline.CategoriaCursoOnlineId);
            }
        }
    }
}