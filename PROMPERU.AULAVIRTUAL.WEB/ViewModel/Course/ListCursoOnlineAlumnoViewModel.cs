using System;
using System.Collections.Generic;
//using System.Data.Entity;
using System.Linq;
using System.Web;
using PROMPERU.AULAVIRTUAL.WEB.Controllers;
using PROMPERU.AULAVIRTUAL.WEB.Helpers;
using PagedList;
using PROMPERU.AULAVIRTUAL.BE.CURSOS;
using PROMPERU.AULAVIRTUAL.BL;

namespace PROMPERU.AULAVIRTUAL.WEB.ViewModel.Course
{
    public class ListCursoOnlineAlumnoViewModel
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

        public ListCursoOnlineAlumnoViewModel()
        {
        }

        public void CargarDatos(CargarDatosContext dataContext, Int32? p, Int32 usuarioId, String NombreCurso, Int32? CategoriaCursoId)
        {
            this.p = p ?? 1;
            this.UsuarioId = usuarioId;
            this.NombreCurso = NombreCurso;
            this.CategoriaCursoId = CategoriaCursoId;

            this.LstCategoriaCursoOnline = cursosBL.ListarCategoriaCursoOnline();    //dataContext.context.CategoriaCursoOnline.ToList();
            this.LstCursoOnlineDisponibe = cursosBL.ListarDisponibilidadCursoOnline().Where(x => x.FechaFin == null || x.FechaFin > DateTime.Now).Select(x => x.CursoOnlineId).ToList();    //cursosBL.ListarDisponibilidadCursoOnlineId();   //dataContext.context.DisponibilidadCursoOnline.Where(x => x.FechaFin == null || x.FechaFin > DateTime.Now).Select(x => x.CursoOnlineId).ToList();
            var fechaInicioMatriculaVigente = ConstantHelpers.GET_FECHA_INICIO_MATRICULA_VIGENTE();
            //this.LstMatriculaCursoOnline = cursosBL.ListarMatriculaCursoOnline(usuarioId).Where
            //                            (x => x.UsuarioId == usuarioId
            //                            && ((x.Estado != ConstantHelpers.ESTADO_MATRICULA.INACTIVO && x.FechaMatricula >= fechaInicioMatriculaVigente)
            //                                || (x.Estado == ConstantHelpers.ESTADO_MATRICULA.COMPLETADO
            //                                    || x.Estado == ConstantHelpers.ESTADO_MATRICULA.DESAPROBADO
            //                                    || x.Estado == ConstantHelpers.ESTADO_MATRICULA.APROBADO)
            //                                   )
            //                             ).ToList();

            //Los resultados que se traen se puso igual que el home de dashboard, a solicitud

            this.LstMatriculaCursoOnline = cursosBL.ListarMatriculaCursoOnline(usuarioId).Where
                (x =>
                    (x.Estado == ConstantHelpers.ESTADO_MATRICULA.PENDIENTE && x.FechaMatricula >= fechaInicioMatriculaVigente)
                    || x.Estado == ConstantHelpers.ESTADO_MATRICULA.COMPLETADO
                    || x.Estado == ConstantHelpers.ESTADO_MATRICULA.APROBADO
                )
                .ToList();



            List<CursoOnline> queryCursoOnline = new List<CursoOnline>();

            foreach (MatriculaCursoOnline obj in LstMatriculaCursoOnline)
            {
                obj.CursoOnline.MatriculaCursoOnline = cursosBL.ListarMatriculaCursoOnlinePorCursoOnlineIdPorUsuarioId(obj.CursoOnlineId,usuarioId);
                queryCursoOnline.Add(obj.CursoOnline);
            }

            if (!String.IsNullOrEmpty(NombreCurso))
            {
                queryCursoOnline = queryCursoOnline.Where(x => x.Nombre.Contains(NombreCurso)).ToList();
            }

            if (CategoriaCursoId.HasValue)
            {
                queryCursoOnline = queryCursoOnline.Where(x => x.CategoriaCursoOnlineId == CategoriaCursoId).ToList();
            }


            //this.LstCursoOnline = queryCursoOnline.Where(x => x.DisponibilidadCurso == 1).OrderBy(x => x.Orden).ThenByDescending(y => y.FechaCreacion).ToPagedList(this.p, ConstantHelpers.DEFAULT_PAGE_SIZE);
            this.LstCursoOnline = queryCursoOnline.OrderBy(x => x.Orden).ThenByDescending(y => y.FechaCreacion).ToPagedList(this.p, ConstantHelpers.DEFAULT_PAGE_SIZE);
        }
    }
}
