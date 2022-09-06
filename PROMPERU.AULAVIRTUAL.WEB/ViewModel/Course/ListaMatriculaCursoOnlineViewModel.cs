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
    public class ListaMatriculaCursoOnlineViewModel
    {
        CursosBL cursosBL = new CursosBL();
        public Int32 p { get; set; }
        public Int32? CursoOnlineId { set; get; }
        /*public List<CursoOnline> LstCursoOnline { set; get; }*/
        public List<ViewCursoMatricula> LstCursoOnline { set; get; }

        public IPagedList<MatriculaCursoOnline> LstMatriculaCursoOnline { get; set; }

        public ListaMatriculaCursoOnlineViewModel()
        {

        }

        public void CargarDatos(CargarDatosContext dataContext, Int32? CursoOnlineId, Int32? p)
        {

            this.p = p ?? 1;

            this.CursoOnlineId = CursoOnlineId;
            this.LstCursoOnline = cursosBL.ListaViewCursoMatricula();

            var _MatriculaCursoOnline = new List<MatriculaCursoOnline>();
            List<MatriculaCursoOnline> queryMatriculaCursoOnline;


            if (CursoOnlineId.HasValue)
            {
                // filtro
                //queryMatriculaCursoOnline = dataContext.context.MatriculaCursoOnline.Where(x => x.CursoOnlineId == this.CursoOnlineId && x.Estado == ConstantHelpers.ESTADO_MATRICULA.INACTIVO).AsQueryable();

                queryMatriculaCursoOnline = cursosBL.ListarMatriculaCursoOnlinePorCursoOnlineId(this.CursoOnlineId, ConstantHelpers.ESTADO_MATRICULA.INACTIVO);
            }
            else
            {
                // sin filtro
                //_MatriculaCursoOnline = dataContext.context.MatriculaCursoOnline.Where(x => x.Estado == ConstantHelpers.ESTADO_MATRICULA.INACTIVO).ToList();
                queryMatriculaCursoOnline = cursosBL.ListarMatriculaCursoOnlinePorCursoOnlineId(ConstantHelpers.ESTADO_MATRICULA.INACTIVO);
            }

            
            //queryMatriculaCursoOnline = queryMatriculaCursoOnline.OrderBy(x => x.MatriculaCursoOnlineId);
            LstMatriculaCursoOnline = queryMatriculaCursoOnline.ToPagedList(this.p, ConstantHelpers.DEFAULT_PAGE_SIZE);
        }
    }
}