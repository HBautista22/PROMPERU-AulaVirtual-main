using System;
using System.Collections.Generic;
//using System.Data.Entity;
using System.Linq;
using System.Web;
using PROMPERU.AULAVIRTUAL.BE.CURSOS;
using PROMPERU.AULAVIRTUAL.BL;
using PROMPERU.AULAVIRTUAL.WEB.Controllers;
using PROMPERU.AULAVIRTUAL.WEB.Helpers;
using PagedList;
using System.Data.SqlClient;

namespace PROMPERU.AULAVIRTUAL.WEB.ViewModel.Course
{
    public class ListCursoOnlineDetalleAlumnoViewModel
    {

        CursosBL cursosBL = new CursosBL();

        public Int32 p { get; set; }
        public Int32 UsuarioId { get; set; }

        public IPagedList<CursoOnline> LstCursoOnline { get; set; }
        public List<CategoriaCursoOnline> LstCategoriaCursoOnline { get; set; }
        public List<MatriculaCursoOnline> LstMatriculaCursoOnline { get; set; }
        public List<Int32> LstCursoOnlineDisponibe { get; set; }
        public String NombreCurso { get; set; }
        public Int32? CategoriaCursoId { get; set; }
        public Int32? CursoOnlineId { get; set; }

        public ListCursoOnlineDetalleAlumnoViewModel()
        {
        }

        public void CargarDatos(CargarDatosContext dataContext, Int32? p, Int32? CursoOnlineId, Int32 usuarioId, String NombreCurso, Int32? CategoriaCursoId)
        {
            this.p = p ?? 1;
            this.UsuarioId = usuarioId;
            this.NombreCurso = NombreCurso;
            this.CategoriaCursoId = CategoriaCursoId;
            this.CursoOnlineId = CursoOnlineId;

            this.LstCategoriaCursoOnline = cursosBL.ListarCategoriaCursoOnline();
            //this.LstCategoriaCursoOnline = dataContext.context.CategoriaCursoOnline.ToList();
            this.LstCursoOnlineDisponibe = cursosBL.ListarDisponibilidadCursoOnline()
                                            .Where(x => x.FechaFin == null || x.FechaFin > DateTime.Now)
                                            .Select(x => x.CursoOnlineId).ToList();
            //this.LstCursoOnlineDisponibe = dataContext.context.DisponibilidadCursoOnline.Where(x => x.FechaFin == null || x.FechaFin > DateTime.Now).Select(x => x.CursoOnlineId).ToList();
            var fechaInicioMatriculaVigente = ConstantHelpers.GET_FECHA_INICIO_MATRICULA_VIGENTE();

            this.LstMatriculaCursoOnline = cursosBL.ListarMatriculaCursoOnline(usuarioId).
                                            Where(x => x.UsuarioId == usuarioId && ((x.Estado != ConstantHelpers.ESTADO_MATRICULA.INACTIVO && x.FechaMatricula >= fechaInicioMatriculaVigente)
                                            || (x.Estado == ConstantHelpers.ESTADO_MATRICULA.COMPLETADO
                                                    || x.Estado == ConstantHelpers.ESTADO_MATRICULA.DESAPROBADO
                                                    || x.Estado == ConstantHelpers.ESTADO_MATRICULA.APROBADO))
                                            ).ToList();


            //this.LstMatriculaCursoOnline = dataContext.context.MatriculaCursoOnline.
            //                                Where(x => x.UsuarioId == usuarioId && ((x.Estado != ConstantHelpers.ESTADO_MATRICULA.INACTIVO && x.FechaMatricula >= fechaInicioMatriculaVigente) ||
            //                                (x.Estado == ConstantHelpers.ESTADO_MATRICULA.COMPLETADO || x.Estado == ConstantHelpers.ESTADO_MATRICULA.DESAPROBADO || x.Estado == ConstantHelpers.ESTADO_MATRICULA.APROBADO))).ToList();


            /*IQueryable<CursoOnline> queryCursoOnline = dataContext.context.MatriculaCursoOnline
                                                        .Include(x => x.CursoOnline)
                                                        .Include(x => x.CursoOnline.DisponibilidadCursoOnline)
                                                        .Where(x => x.UsuarioId == usuarioId && (
                                                        (x.Estado != ConstantHelpers.ESTADO_MATRICULA.INACTIVO && x.FechaMatricula >= fechaInicioMatriculaVigente) || (x.Estado == ConstantHelpers.ESTADO_MATRICULA.COMPLETADO || x.Estado == ConstantHelpers.ESTADO_MATRICULA.DESAPROBADO || x.Estado == ConstantHelpers.ESTADO_MATRICULA.APROBADO)
                                                        )
            ).Select(x => x.CursoOnline).AsQueryable();*/

            //List<CursoOnline> tmpCursoOnline = cursosBL.ListarCursoOnline().Where(x => x.CursoOnlineId == CursoOnlineId).ToList();

            //IQueryable<CursoOnline> queryCursoOnline = dataContext.context.CursoOnline.SqlQuery(@"USP_DetalleCursoOnline_LIS {0}", CursoOnlineId).AsQueryable();

            List<CursoOnline> queryCursoOnline = cursosBL.ListarCursoOnlineDetalleCursoOnlineMaestro((int)CursoOnlineId);


            if (!String.IsNullOrEmpty(NombreCurso))
            {
                queryCursoOnline = queryCursoOnline.Where(x => x.Nombre.Contains(NombreCurso)).ToList();
            }

            if (CategoriaCursoId.HasValue)
            {
                queryCursoOnline = queryCursoOnline.Where(x => x.CategoriaCursoOnlineId == CategoriaCursoId).ToList();
            }

            this.LstCursoOnline = queryCursoOnline.OrderBy(x => x.Orden).ThenByDescending(y => y.FechaCreacion).ToPagedList(this.p, ConstantHelpers.DEFAULT_PAGE_SIZE);
        }
    }
}
