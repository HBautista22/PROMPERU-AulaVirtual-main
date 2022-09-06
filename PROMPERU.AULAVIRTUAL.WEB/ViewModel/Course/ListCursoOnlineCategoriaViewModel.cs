//using PagedList;
using PagedList;
using PROMPERU.AULAVIRTUAL.BE.CURSOS;
using PROMPERU.AULAVIRTUAL.BL;
using PROMPERU.AULAVIRTUAL.WEB.Controllers;
using PROMPERU.AULAVIRTUAL.WEB.Helpers;
using PROMPERU.AULAVIRTUAL.WEB.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
//using System.Data.Entity;

namespace PROMPERU.AULAVIRTUAL.WEB.ViewModel.Course
{
    public class ListCursoOnlineCategoriaViewModel
    {
        public Int32 p { get; set; }
        public Int32 CategoriaCursoOnlineId { get; set; }
        public CategoriaCursoOnline CategoriaCursoOnline { get; set; }

        public List<int> LstCursoOnlineDisponibe { get; set; }

        public IPagedList<CursoOnline> LstCursoOnline { get; set; }
        public List<MatriculaCursoOnline> LstMatriculaCursoOnline { get; set; }


        public ListCursoOnlineCategoriaViewModel()
        {
        }

        public void CargarDatos(CargarDatosContext dataContext, Int32? p, Int32 categoriaCursoOnlineId)
        {
            CursosBL cursosBL = new CursosBL();

            this.p = p ?? 1;
            this.CategoriaCursoOnlineId = categoriaCursoOnlineId;
            //this.CategoriaCursoOnline = dataContext.context.CategoriaCursoOnline.Find(CategoriaCursoOnlineId);
            this.CategoriaCursoOnline = cursosBL.ObtenerCategoriaCursoOnline(CategoriaCursoOnlineId);
            Int32 usuarioId = dataContext.Session.GetUsuarioId();

            //LstCursoOnlineDisponibe = dataContext.context.DisponibilidadCursoOnline.Where(x => x.FechaFin == null || x.FechaFin > DateTime.Now).Select(x => x.CursoOnlineId).ToList();
            LstCursoOnlineDisponibe = cursosBL.ListarDisponibilidadCursoOnline().Where(x => x.FechaFin == null || x.FechaFin > DateTime.Now).Select(x => x.CursoOnlineId).ToList();

            //IQueryable<CursoOnline> queryCursoOnline = dataContext.context.CursoOnline.Include(x => x.CategoriaCursoOnline).Where(x => x.CategoriaCursoOnlineId == categoriaCursoOnlineId).AsQueryable();
            List<CursoOnline> queryCursoOnline = cursosBL.ListarCursoOnline().Where(x => x.CategoriaCursoOnlineId == categoriaCursoOnlineId).ToList();

            var fechaInicioMatriculaVigente = ConstantHelpers.GET_FECHA_INICIO_MATRICULA_VIGENTE();
            //LstMatriculaCursoOnline = dataContext.context.MatriculaCursoOnline.Where(x => x.UsuarioId == usuarioId && x.Estado != ConstantHelpers.ESTADO_MATRICULA.INACTIVO && x.FechaMatricula >= fechaInicioMatriculaVigente).ToList();
            LstMatriculaCursoOnline = cursosBL.ListarMatriculaCursoOnline(usuarioId ).Where(x => x.Estado != ConstantHelpers.ESTADO_MATRICULA.INACTIVO && x.FechaMatricula >= fechaInicioMatriculaVigente).ToList();

            queryCursoOnline = queryCursoOnline.OrderBy(x => x.Nombre).ToList();
            LstCursoOnline = queryCursoOnline.ToPagedList(this.p, ConstantHelpers.DEFAULT_PAGE_SIZE);
        }
    }
}