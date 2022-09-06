using PROMPERU.AULAVIRTUAL.WEB.Controllers;
using PROMPERU.AULAVIRTUAL.WEB.Helpers;
using System;
using System.Collections.Generic;
using System.Linq;
using PROMPERU.AULAVIRTUAL.BE.CURSOS;
using PROMPERU.AULAVIRTUAL.BL;
using PROMPERU.AULAVIRTUAL.BE.USUARIO;

namespace PROMPERU.AULAVIRTUAL.WEB.ViewModel.Course
{
    public class ViewCursoOnlineViewModel
    {
        CursosBL cursosBL = new CursosBL();
        UsuarioBL usuarioBL = new UsuarioBL();
        EmpresaBL empresaBL = new EmpresaBL();

        public Int32 CursoOnlineId { get; set; }
        public Int32 MatriculaCursoOnlineId { get; set; }
        public CursoOnline CursoOnline { get; set; }
        public MatriculaCursoOnline MatriculaCursoOnline { get; set; }
        public IDictionary<UnidadCursoOnline, String> DictUnidadCursoOnline { get; set; }
        public Boolean EstaDisponible { get; set; }
        public Boolean PuedeMatricularse { get; set; }
        public Boolean MatriculaVencida { get; set; }
        public Boolean SeCompletoEvaluacionesSatisfactoriamente { get; set; }

        public Int32 CountUsuariosEmpresa { get; set; }
        public Int32 ProfesorId { get; set; }
        public Usuario Profesor { get; set; }

        public Boolean Videos { get; set; }
        public Boolean SessionVivo { get; set; }
        public ViewCursoOnlineViewModel()
        {
        }

        internal void CargarDatos(CargarDatosContext dataContext, int cursoOnlineId, int? matriculaCursoOnlineId)
        {
            Int32 usuarioId = dataContext.Session.GetUsuarioId();
            cursosBL.InsertarCursoOnlineVisto(cursoOnlineId, usuarioId);

            if (matriculaCursoOnlineId.HasValue)
            {
                CargarDatos(dataContext, cursoOnlineId, matriculaCursoOnlineId.Value);
            }
            else
            {
                CargarDatos(dataContext, cursoOnlineId);
            }
        }

        public void CargarDatos(CargarDatosContext dataContext, Int32 cursoOnlineId)
        {

            DisponibilidadCursoOnline disponibilidadCursoOnline = cursosBL.ObtenerDisponibilidadCursoOnlinePorId(this.CursoOnlineId);

            this.CursoOnlineId = cursoOnlineId;
            Int32 usuarioId = dataContext.Session.GetUsuarioId();


            this.EstaDisponible = (disponibilidadCursoOnline.FechaInicio <= DateTime.Now
                                && (!disponibilidadCursoOnline.FechaFin.HasValue || disponibilidadCursoOnline.FechaFin >= DateTime.Now));

            var fechaInicioMatriculaVigente = ConstantHelpers.GET_FECHA_INICIO_MATRICULA_VIGENTE();

            var empresaId = dataContext.Session.GetEmpresaId();

            this.MatriculaCursoOnline = cursosBL.ObtenerMatriculaCursoOnlinePorId(MatriculaCursoOnlineId);

            this.CursoOnline = cursosBL.ObtenerCursoOnlinePorId(this.CursoOnlineId);
            this.CursoOnline.CategoriaCursoOnline = cursosBL.ObtenerCategoriaCursoOnline(this.CursoOnline.CategoriaCursoOnlineId);



            this.EstaDisponible = (
                disponibilidadCursoOnline.CursoOnlineId == this.CursoOnlineId
                && disponibilidadCursoOnline.FechaInicio <= DateTime.Now
                && (!disponibilidadCursoOnline.FechaFin.HasValue || disponibilidadCursoOnline.FechaFin >= DateTime.Now));


            CursoOnline cursoOnline = cursosBL.ObtenerCursoOnlinePorId(this.CursoOnlineId);

            cursoOnline.CategoriaCursoOnline = cursosBL.ListarCategoriaCursoOnline().FirstOrDefault(x => x.CategoriaCursoOnlineId == cursoOnline.CategoriaCursoOnlineId);

            this.CursoOnline = cursoOnline;



            //this.MatriculaCursoOnline = dataContext.context.MatriculaCursoOnline.Where(x => x.UsuarioId == usuarioId && x.CursoOnlineId == cursoOnlineId && ((x.Estado != ConstantHelpers.ESTADO_MATRICULA.INACTIVO && x.FechaMatricula >= fechaInicioMatriculaVigente) || x.Estado == ConstantHelpers.ESTADO_MATRICULA.APROBADO)).OrderByDescending(x => x.MatriculaCursoOnlineId).FirstOrDefault();

            List<MatriculaCursoOnline> lstMatriculaCursoOnline = cursosBL.ListarMatriculaCursoOnlinePorCursoOnlineIdPorUsuarioId(cursoOnlineId, usuarioId);

            this.MatriculaCursoOnline = lstMatriculaCursoOnline.Where
                (x =>
                    (
                        (
                            x.Estado != ConstantHelpers.ESTADO_MATRICULA.INACTIVO
                            && x.FechaMatricula >= fechaInicioMatriculaVigente
                        )
                        || x.Estado == ConstantHelpers.ESTADO_MATRICULA.APROBADO
                    )
                )
                .OrderByDescending(x => x.MatriculaCursoOnlineId)
                .FirstOrDefault();



            var lstUnidadCursoOnlineIdActiva = new List<AvanceMatriculaCursoOnline>();


            var lstUnidadCursoOnline = cursosBL.ListarUnidadCursoOnlinePorCursoOnlineId(cursoOnlineId).Where
                                        (x => x.CursoOnlineId == cursoOnlineId
                                            && x.Estado == ConstantHelpers.ESTADO.ACTIVO
                                            && x.TipoUnidadId == ConstantHelpers.TIPO_UNIDAD.DB_TEMA
                                            && x.UnidadCursoOnlinePadreId == null).
                                        OrderBy(x => x.Orden).ToList();


            if (MatriculaCursoOnline != null)
            {
                lstUnidadCursoOnlineIdActiva = cursosBL.ListarAvanceMatriculaCursoOnlinePorMatriculaCursoOnlineId(MatriculaCursoOnline.MatriculaCursoOnlineId);
            }

            this.DictUnidadCursoOnline = lstUnidadCursoOnline.ToDictionary(x => x,
                    x => (lstUnidadCursoOnlineIdActiva.FirstOrDefault(a => a.UnidadCursoOnlineId == x.UnidadCursoOnlineId) == null ? ConstantHelpers.ESTADO_UNIDAD.INACTIVO :
                        (lstUnidadCursoOnlineIdActiva.FirstOrDefault(a => a.UnidadCursoOnlineId == x.UnidadCursoOnlineId && a.FechaFin.HasValue) != null ? ConstantHelpers.ESTADO_UNIDAD.COMPLETADO :
                        ConstantHelpers.ESTADO_UNIDAD.PENDIENTE)));

            var usuario = usuarioBL.ObtenerUsuariosPorEmpresa(empresaId).FirstOrDefault(x => x.UsuarioId == usuarioId);  //dataContext.context.Usuario.Include(x => x.Empresa).FirstOrDefault(x => x.UsuarioId == usuarioId);
            Empresa empresa = empresaBL.ObtenerEmpresaPorEmpresaId(empresaId).FirstOrDefault();
            if (usuario != null)
            {
                usuario.Empresa = empresa;
                this.PuedeMatricularse = CursoOnline.AutoMatriculaHabilitada || (usuario.EmpresaId.HasValue && usuario.Empresa.AutoMatriculaHabilitada);
            }
            else
            {
                this.PuedeMatricularse = CursoOnline.AutoMatriculaHabilitada;
            }


            var lstUnidadesId = CursoOnline.UnidadCursoOnline.Select(x => x.UnidadCursoOnlineId).ToList();


            List<EvaluacionCursoOnline> lstEvaluacionCursoOnline = cursosBL.ListarEvaluacionCursoOnlinePorCursoOnlineId(this.CursoOnlineId);
            //.All(x => x.Max(y => y.Nota) > CursoOnline.PorcentajeAprobacion);

            //SeCompletoEvaluacionesSatisfactoriamente = dataContext.context.EvaluacionCursoOnline.Where(x => lstUnidadesId.Contains(x.UnidadCursoOnlineId)).GroupBy(x => x.UnidadCursoOnlineId).All(x => x.Max(y => y.Nota) > CursoOnline.PorcentajeAprobacion);
            SeCompletoEvaluacionesSatisfactoriamente = lstEvaluacionCursoOnline
                .GroupBy(x => x.UnidadCursoOnlineId)
                .All(x => x.Max(y => y.Nota) > CursoOnline.PorcentajeAprobacion);


            //CountUsuariosEmpresa = dataContext.context.Usuario.Where(x => x.EmpresaId == empresaId).Count();
            CountUsuariosEmpresa = usuarioBL.ObtenerUsuariosPorEmpresa(empresaId).Count();

            foreach (var item in this.DictUnidadCursoOnline)
            {
                item.Key.UnidadCursoOnline1 = cursosBL.ListarUnidadCursoOnlinePorUnidad(item.Key.UnidadCursoOnlineId).Where(x => x.Estado == ConstantHelpers.ESTADO.ACTIVO).ToList();
            }

        }

        internal void CargarDatos(CargarDatosContext dataContext, int cursoOnlineId, int matriculaCursoOnlineId)
        {
            List<DisponibilidadCursoOnline> disponibilidadCursoOnline = cursosBL.ListarDisponibilidadCursoOnline();

            CursoOnlineId = cursoOnlineId;
            MatriculaCursoOnlineId = matriculaCursoOnlineId;
            //this.EstaDisponible = (disponibilidadCursoOnline.FechaInicio <= DateTime.Now && (!disponibilidadCursoOnline.FechaFin.HasValue || disponibilidadCursoOnline.FechaFin >= DateTime.Now));
            EstaDisponible = disponibilidadCursoOnline.FirstOrDefault(x => x.CursoOnlineId == this.CursoOnlineId && x.FechaInicio <= DateTime.Now && (x.FechaFin == null || x.FechaFin >= DateTime.Now)) != null;
            int usuarioId = dataContext.Session.GetUsuarioId();
            DateTime fechaInicioMatriculaVigente = ConstantHelpers.GET_FECHA_INICIO_MATRICULA_VIGENTE();

            int empresaId = dataContext.Session.GetEmpresaId();

            MatriculaCursoOnline = cursosBL.ObtenerMatriculaCursoOnlinePorId(matriculaCursoOnlineId);
            MatriculaCursoOnline.EvaluacionCursoOnline = cursosBL.ListarEvaluacionCursoOnlinePorCursoOnlineId(cursoOnlineId).Where(x => x.MatriculaCursoOnlineId == this.MatriculaCursoOnline.MatriculaCursoOnlineId).ToList();
            CursoOnline = cursosBL.ObtenerCursoOnlinePorId(CursoOnlineId);
            CursoOnline.CategoriaCursoOnline = cursosBL.ObtenerCategoriaCursoOnline(CursoOnline.CategoriaCursoOnlineId);
            CursoOnline.PreguntaCursoOnline = cursosBL.ListarPreguntaCursoOnlinePorCursoOnlineId(CursoOnlineId);

            List<UnidadCursoOnline> lstUnidadCursoOnline = cursosBL.ListarUnidadCursoOnlinePorCursoOnlineId(cursoOnlineId).Where
                                        (x => x.CursoOnlineId == cursoOnlineId
                                            && x.Estado == ConstantHelpers.ESTADO.ACTIVO
                                            && x.TipoUnidadId == ConstantHelpers.TIPO_UNIDAD.DB_TEMA).
                                        OrderBy(x => x.Orden).ToList();

            foreach (UnidadCursoOnline obj in lstUnidadCursoOnline)
            {
                obj.CursoOnline = cursosBL.ObtenerCursoOnlinePorId(CursoOnlineId);
                obj.AvanceMatriculaCursoOnline = cursosBL.ListarAvanceMatriculaCursoOnlinePorUnidadCursoOnlineId(obj.UnidadCursoOnlineId);
                obj.CursoOnline.PreguntaCursoOnline = cursosBL.ListarPreguntaCursoOnline().Where(x => x.UnidadOnlineId == obj.UnidadCursoOnlineId).ToList();
                obj.CursoOnline.CategoriaCursoOnline = cursosBL.ObtenerCategoriaCursoOnline(CursoOnline.CategoriaCursoOnlineId);
                obj.PreguntaCursoOnline = cursosBL.ListarPreguntaCursoOnline().Where(x => x.UnidadOnlineId == obj.UnidadCursoOnlineId).ToList();

                obj.TareaUnidadCursoOnline = cursosBL.ListarTareaUnidadCursoOnlinePorUnidadCursoOnlineId(obj.UnidadCursoOnlineId, usuarioId);
            }

            List<AvanceMatriculaCursoOnline> lstUnidadCursoOnlineIdActiva = cursosBL.ListarAvanceMatriculaCursoOnlinePorMatriculaCursoOnlineId(MatriculaCursoOnline.MatriculaCursoOnlineId);

            MatriculaVencida = MatriculaCursoOnline.FechaMatricula <= fechaInicioMatriculaVigente;

            // TODO: original
            DictUnidadCursoOnline = lstUnidadCursoOnline.ToDictionary(x => x,
                x => lstUnidadCursoOnlineIdActiva
                    .FirstOrDefault(a => a.UnidadCursoOnlineId == x.UnidadCursoOnlineId) == null 
                    ? ConstantHelpers.ESTADO_UNIDAD.INACTIVO 
                    : lstUnidadCursoOnlineIdActiva.FirstOrDefault(a => a.UnidadCursoOnlineId == x.UnidadCursoOnlineId && a.FechaFin.HasValue) != null 
                        ? ConstantHelpers.ESTADO_UNIDAD.COMPLETADO 
                        : ConstantHelpers.ESTADO_UNIDAD.PENDIENTE);

            /*this.DictUnidadCursoOnline = lstUnidadCursoOnline.ToDictionary(x => x,
                    x => (lstUnidadCursoOnlineIdActiva.FirstOrDefault(a => a.UnidadCursoOnlineId == x.UnidadCursoOnlineId) == null ? ConstantHelpers.ESTADO_UNIDAD.INACTIVO :
                        (lstUnidadCursoOnlineIdActiva.FirstOrDefault(a => a.UnidadCursoOnlineId == x.UnidadCursoOnlineId) != null ? ConstantHelpers.ESTADO_UNIDAD.COMPLETADO :
                        ConstantHelpers.ESTADO_UNIDAD.PENDIENTE)));*/

            try
            {

                Empresa empresa = empresaBL.ObtenerEmpresaPorEmpresaId(empresaId).FirstOrDefault();
                
                if (empresa != null && empresa.AutoMatriculaHabilitada != null)
                    PuedeMatricularse = CursoOnline.AutoMatriculaHabilitada || empresa.AutoMatriculaHabilitada;
            }
            catch
            {
                PuedeMatricularse = false;
            }

            List<int> lstUnidadesId = CursoOnline.UnidadCursoOnline.Select(x => x.UnidadCursoOnlineId).ToList();

            int cantidadEvaluaciones = cursosBL.ListarEvaluacionCursoOnlinePorMatriculaCursoOnlineIdAgrupado(matriculaCursoOnlineId).Count();
            
            SeCompletoEvaluacionesSatisfactoriamente = (cursosBL.ListarEvaluacionCursoOnlinePorMatriculaCursoOnlineIdAgrupado(matriculaCursoOnlineId).Max(y => y.Nota) > CursoOnline.PorcentajeAprobacion) && lstUnidadCursoOnlineIdActiva.Count() == cantidadEvaluaciones;
            
            CountUsuariosEmpresa = usuarioBL.TotalUsuariosPorEmpresa(empresaId);
            
            if (CursoOnline.UsuarioProfesor.HasValue)
            {
                ProfesorId = (int)CursoOnline.UsuarioProfesor;
                Profesor = usuarioBL.ObtenerUsuarioPorId((int)CursoOnline.UsuarioProfesor);
            }
            //this.UnidadCursoOnline.UnidadCursoOnline1 = cursosBL.ListarUnidadCursoOnlinePorCursoOnlineId(this.CursoOnlineId);
            //item.Key.UnidadCursoOnline1 = cursosBL.ListarUnidadCursoOnlinePorCursoOnlineId(item.Key.UnidadCursoOnlineId);

            foreach (KeyValuePair<UnidadCursoOnline, string> item in DictUnidadCursoOnline)
            {
                item.Key.UnidadCursoOnline1 = cursosBL.ListarUnidadCursoOnlinePorUnidad(item.Key.UnidadCursoOnlineId)
                    .Where(x => x.Estado == ConstantHelpers.ESTADO.ACTIVO)
                    .ToList();
            }

        }
        

    }

}