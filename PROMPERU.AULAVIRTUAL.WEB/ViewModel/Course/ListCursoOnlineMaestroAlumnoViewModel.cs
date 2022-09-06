using System;
using System.Collections.Generic;
//using System.Data.Entity;
using System.Linq;
using System.Web;
using PROMPERU.AULAVIRTUAL.WEB.Models;
using PROMPERU.AULAVIRTUAL.WEB.Controllers;
using PROMPERU.AULAVIRTUAL.WEB.Helpers;
using PROMPERU.AULAVIRTUAL.BE.CURSOS;
using PagedList;

namespace PROMPERU.AULAVIRTUAL.WEB.ViewModel.Course
{
    public class ListCursoOnlineMaestroAlumnoViewModel
    {
        public Int32 p { get; set; }
        public Int32 UsuarioId { get; set; }

        public IPagedList<CursoOnlineMaestro> LstCursoOnline { get; set; }
        public List<CategoriaCursoOnline> LstCategoriaCursoOnline { get; set; }
        public List<MatriculaCursoOnline> LstMatriculaCursoOnline { get; set; }
        public List<Int32> LstCursoOnlineDisponibe { get; set; }
        public String NombreCurso { get; set; }
        public Int32? CategoriaCursoId { get; set; }

        public ListCursoOnlineMaestroAlumnoViewModel()
        {
        }

        //public void CargarDatos(CargarDatosContext dataContext, Int32? p, Int32 usuarioId, String NombreCurso, Int32? CategoriaCursoId)
        //{
        //    this.p = p ?? 1;
        //    this.UsuarioId = usuarioId;
        //    this.NombreCurso = NombreCurso;
        //    this.CategoriaCursoId = CategoriaCursoId;

        //    this.LstCategoriaCursoOnline = dataContext.context.CategoriaCursoOnline.ToList();



        //    this.LstCursoOnlineDisponibe = dataContext.context.DisponibilidadCursoOnline.Where(x => x.FechaFin == null || x.FechaFin > DateTime.Now).Select(x => x.CursoOnlineId).ToList();
        //    var fechaInicioMatriculaVigente = ConstantHelpers.GET_FECHA_INICIO_MATRICULA_VIGENTE();
        //    this.LstMatriculaCursoOnline = dataContext.context.MatriculaCursoOnline.Where(x => x.UsuarioId == usuarioId && ((x.Estado != ConstantHelpers.ESTADO_MATRICULA.INACTIVO && x.FechaMatricula >= fechaInicioMatriculaVigente) ||
        //                                                (x.Estado == ConstantHelpers.ESTADO_MATRICULA.COMPLETADO || x.Estado == ConstantHelpers.ESTADO_MATRICULA.DESAPROBADO || x.Estado == ConstantHelpers.ESTADO_MATRICULA.APROBADO))).ToList();

        //    /*IQueryable<CursoOnlineMaestro> queryCursoOnline = dataContext.context.MatriculaCursoOnline
        //                                                .Include(x => x.CursoOnline)
        //                                                .Include(x => x.CursoOnline.DisponibilidadCursoOnline)
        //                                                .Where(x => x.UsuarioId == usuarioId && (
        //                                                (x.Estado != ConstantHelpers.ESTADO_MATRICULA.INACTIVO && x.FechaMatricula >= fechaInicioMatriculaVigente) || (x.Estado == ConstantHelpers.ESTADO_MATRICULA.COMPLETADO || x.Estado == ConstantHelpers.ESTADO_MATRICULA.DESAPROBADO || x.Estado == ConstantHelpers.ESTADO_MATRICULA.APROBADO)
        //                                                )
        //    ).Select(x => x.CursoOnline).AsQueryable();*/

        //    IQueryable<CursoOnlineMaestro> queryCursoOnline = dataContext.context.CursoOnlineMaestro.SqlQuery(@"USP_CursoOnlineMaestro_LIS {0}", usuarioId).AsQueryable();

        //    if (!String.IsNullOrEmpty(NombreCurso))
        //    {
        //        queryCursoOnline = queryCursoOnline.Where(x => x.Nombre.Contains(NombreCurso));
        //    }

        //    if (CategoriaCursoId.HasValue)
        //    {
        //        queryCursoOnline = queryCursoOnline.Where(x => x.CategoriaCursoOnlineId == CategoriaCursoId);
        //    }


        //    this.LstCursoOnline = queryCursoOnline.OrderBy(x => x.Orden).ThenByDescending(y => y.FechaCreacion).ToPagedList(this.p, ConstantHelpers.DEFAULT_PAGE_SIZE);
        //}
    }
}
