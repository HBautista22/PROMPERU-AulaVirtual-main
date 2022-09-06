using PagedList;
using PROMPERU.AULAVIRTUAL.WEB.Controllers;
using PROMPERU.AULAVIRTUAL.WEB.Helpers;
using PROMPERU.AULAVIRTUAL.BE.USUARIO;
using PROMPERU.AULAVIRTUAL.BE.CURSOS;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
//using System.Data.Entity;
using System.Linq;
using System.Web;

namespace PROMPERU.AULAVIRTUAL.WEB.ViewModel.Course
{
    public class ListAlumnoCursoOnlineViewModel
    {

        public Int32 UsuarioId { get; set; }
        public Usuario Usuario { get; set; }
        public Int32 p { get; set; }

        [Display(Name = "Buscar curso")]
        public String CadenaBuscar { get; set; }

        public IPagedList<MatriculaCursoOnline> LstMatriculaCursoOnline { get; set; }

        public String NombreCursobuscar { get; set; }

        public ListAlumnoCursoOnlineViewModel()
        {

        }

        public void CargarDatos(CargarDatosContext dataContext, Int32? p, Int32 usuarioId, String cadenaBuscar)
        {
            //this.p = p ?? 1;
            //this.UsuarioId = usuarioId;
            //this.CadenaBuscar = cadenaBuscar;

            //Usuario = dataContext.context.Usuario.Find(UsuarioId);

            //IQueryable<MatriculaCursoOnline> queryMatriculaCursoOnline = dataContext.context.MatriculaCursoOnline.Include(x => x.CursoOnline).Include(x => x.CursoOnline.CategoriaCursoOnline).Where(x => x.UsuarioId == usuarioId).AsQueryable();

            //if (cadenaBuscar != null)
            //{
            //    foreach (var token in CadenaBuscar.Split(' '))
            //        queryMatriculaCursoOnline = queryMatriculaCursoOnline.Where(x => x.CursoOnline.Nombre.Contains(token));
            //}

            //queryMatriculaCursoOnline = queryMatriculaCursoOnline.OrderBy(x => x.MatriculaCursoOnlineId);
            //LstMatriculaCursoOnline = queryMatriculaCursoOnline.ToPagedList(this.p, ConstantHelpers.DEFAULT_PAGE_SIZE);
        }


    }
}