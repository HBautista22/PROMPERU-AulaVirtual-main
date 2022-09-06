using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using PROMPERU.AULAVIRTUAL.BE.CURSOS;
using PROMPERU.AULAVIRTUAL.WEB.Controllers;
using PROMPERU.AULAVIRTUAL.WEB.Helpers;
//using System.Data.Entity;
using PagedList;
using System.ComponentModel.DataAnnotations;


namespace PROMPERU.AULAVIRTUAL.WEB.ViewModel.Course
{
    public class HistoryMatriculaCursoOnlineViewModel
    {
        public Int32 UsuarioId { get; set; }
        public Int32 p { get; set; }

        [Display(Name = "Buscar curso")]
        public String CadenaBuscar { get; set; }

        public IPagedList<MatriculaCursoOnline> LstMatriculaCursoOnline { get; set; }

        public HistoryMatriculaCursoOnlineViewModel()
        {
        }

        public void CargarDatos(CargarDatosContext dataContext, Int32? p, String cadenaBuscar, Int32? usuarioId)
        {
            //this.CadenaBuscar = cadenaBuscar;
            //this.p = p ?? 1;

            //if (dataContext.session.GetRol() == AppRol.Administrador)
            //{
            //    this.UsuarioId = usuarioId.Value;
            //}
            //else
            //{
            //    this.UsuarioId = dataContext.session.GetUsuarioId();
            //}

            //var query = dataContext.context.MatriculaCursoOnline.Include(x => x.CursoOnline).Include(x => x.CursoOnline.CategoriaCursoOnline).Where(x => x.UsuarioId == UsuarioId).AsQueryable();

            //if (CadenaBuscar != null)
            //{
            //    foreach (var token in CadenaBuscar.Split(' '))
            //        query = query.Where(x => x.CursoOnline.Nombre.Contains(token));
            //}

            //query = query.OrderByDescending(x => x.MatriculaCursoOnlineId);
            //LstMatriculaCursoOnline = query.ToPagedList(this.p, ConstantHelpers.DEFAULT_PAGE_SIZE);
        }
    }
}

