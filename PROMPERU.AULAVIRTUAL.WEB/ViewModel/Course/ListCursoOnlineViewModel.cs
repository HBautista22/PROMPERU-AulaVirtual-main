using PagedList;
using PROMPERU.AULAVIRTUAL.BE.CURSOS;
using PROMPERU.AULAVIRTUAL.BL;
using PROMPERU.AULAVIRTUAL.WEB.Controllers;
using PROMPERU.AULAVIRTUAL.WEB.Helpers;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace PROMPERU.AULAVIRTUAL.WEB.ViewModel.Course
{
    public class ListCursoOnlineViewModel
    {
        CursosBL cursosBL = new CursosBL();

        public Int32 p { get; set; }
        public List<int> LstCursoOnlineDisponibe { get; set; }
        public IPagedList<CursoOnline> LstCursoOnline { get; set; }
        public List<CursoOnline> LstCursoOnlineTotal { get; set; }
        public List<CategoriaCursoOnline> LstCategoriaCursoOnline { get; set; }
        public List<MatriculaCursoOnline> LstMatriculaCursoOnline { get; set; }
        public String NombreCurso { get; set; }
        public Int32? CategoriaCursoId { get; set; }

        public ListCursoOnlineViewModel()
        {

        }

        //public void CargarDatos(Int32? p, String NombreCurso, Int32? CategoriaCursoId)
        //{
        //    this.p = p ?? 1;
        //    //Int32 usuarioId = dataContext.session.GetUsuarioId();
        //    this.NombreCurso = NombreCurso;
        //    this.CategoriaCursoId = CategoriaCursoId;

        //    //LstCategoriaCursoOnline = dataContext.context.CategoriaCursoOnline.ToList();

        //    LstCategoriaCursoOnline = cursosBL.ListarCategoriaCursoOnline();

        //    var fechaInicioMatriculaVigente = ConstantHelpers.GET_FECHA_INICIO_MATRICULA_VIGENTE();

        //    //LstMatriculaCursoOnline = dataContext.context.MatriculaCursoOnline.Where(x => x.UsuarioId == usuarioId && ((x.Estado != ConstantHelpers.ESTADO_MATRICULA.INACTIVO && x.FechaMatricula >= fechaInicioMatriculaVigente) || (x.Estado == ConstantHelpers.ESTADO_MATRICULA.COMPLETADO || x.Estado == ConstantHelpers.ESTADO_MATRICULA.DESAPROBADO || x.Estado == ConstantHelpers.ESTADO_MATRICULA.APROBADO))).ToList();

        //    LstMatriculaCursoOnline = cursosBL.ListarMatriculaCursoOnline(usuarioId);

        //    LstMatriculaCursoOnline = LstMatriculaCursoOnline
        //        .Where
        //        (x =>
        //            (
        //                (x.Estado != ConstantHelpers.ESTADO_MATRICULA.INACTIVO && x.FechaMatricula >= fechaInicioMatriculaVigente)
        //                || (x.Estado == ConstantHelpers.ESTADO_MATRICULA.COMPLETADO
        //                    || x.Estado == ConstantHelpers.ESTADO_MATRICULA.DESAPROBADO
        //                    || x.Estado == ConstantHelpers.ESTADO_MATRICULA.APROBADO)
        //                )
        //            )
        //        .ToList();


        //    //LstCursoOnlineDisponibe = dataContext.context.DisponibilidadCursoOnline
        //    //.Where(x => x.CursoOnline.Estado == ConstantHelpers.ESTADO.ACTIVO && (x.FechaFin == null || x.FechaFin > DateTime.Now)).Select(x => x.CursoOnlineId).ToList();// modificado por OTI

        //    List<DisponibilidadCursoOnline> tmpDisponible = cursosBL.ListarDisponibilidadCursoOnline(usuarioId);
        //    LstCursoOnlineDisponibe = tmpDisponible
        //        .Where
        //        (x =>
        //            (x.FechaFin == null || x.FechaFin > DateTime.Now)
        //        )

        //        .Select(x => x.CursoOnlineId)
        //        .ToList();

        //    //IQueryable<CursoOnline> queryCursoOnline = dataContext.context.DisponibilidadCursoOnline.Where(x => x.CursoOnline.Estado == ConstantHelpers.ESTADO.ACTIVO && (x.FechaFin == null || x.FechaFin > DateTime.Now) && x.CursoOnline.MatriculaCursoOnline.All(a => a.UsuarioId != usuarioId)).Select(x => x.CursoOnline).AsQueryable();

        //    List<CursoOnline> LstCursosDisponibles = tmpDisponible
        //        .Where
        //        (x =>
        //            (x.FechaFin == null || x.FechaFin > DateTime.Now)
        //        ).Select(x => x.CursoOnline).ToList();

        //    if (!String.IsNullOrEmpty(NombreCurso))
        //    {
        //        LstCursosDisponibles = LstCursosDisponibles.Where(x => x.Nombre.Contains(NombreCurso)).ToList();
        //    }

        //    if (CategoriaCursoId.HasValue)
        //    {
        //        LstCursosDisponibles = LstCursosDisponibles.Where(x => x.CategoriaCursoOnlineId == CategoriaCursoId).ToList();
        //    }

        //    LstCursosDisponibles = LstCursosDisponibles.Where(x => x.DisponibilidadCurso == 1).ToList();

        //    LstCursoOnline = LstCursosDisponibles.OrderBy(x => x.Orden).ThenByDescending(y => y.FechaCreacion).ToPagedList(this.p, ConstantHelpers.DEFAULT_PAGE_SIZE);

        //    LstCursoOnlineTotal = LstCursosDisponibles.OrderBy(x => x.Orden).ThenByDescending(y => y.FechaCreacion).ToList();
        //}


    }
}