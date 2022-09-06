using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using PROMPERU.AULAVIRTUAL.BE.CURSOS;
using PROMPERU.AULAVIRTUAL.BL;
using PROMPERU.AULAVIRTUAL.WEB.Controllers;
using PROMPERU.AULAVIRTUAL.WEB.Helpers;
//using System.Data.Entity;
using PagedList;
using System.ComponentModel.DataAnnotations;
using System.Web.Configuration;

namespace PROMPERU.AULAVIRTUAL.WEB.ViewModel.Course
{
    public class ViewCertificadoViewModel
    {
        public Int32 UsuarioId { get; set; }
        public Int32 p { get; set; }

        public IPagedList<ViewCertificadoAlumno> LstMatriculaCursoOnline { get; set; }

        [Display(Name = "Buscar curso")]
        public String CadenaBuscar { get; set; }

        public ViewCertificadoViewModel()
        {
        }

        public void CargarDatos(CargarDatosContext dataContext, Int32? p, String cadenaBuscar, Int32? usuarioId)
        {
            CursosBL cursosBL = new CursosBL();
            bool isTurismo = bool.Parse(WebConfigurationManager.AppSettings["EsTurismo"]);
            this.CadenaBuscar = cadenaBuscar;
            this.p = p ?? 1;

            if (dataContext.Session.GetRol() == AppRol.Administrador)
            {
                this.UsuarioId = usuarioId.Value;
            }
            else
            {
                this.UsuarioId = dataContext.Session.GetUsuarioId();
            }

            if (isTurismo)
            {
                var query = cursosBL.ListarCertificadoTurismoAlumnoPorUsuarioId((int)this.UsuarioId);// dataContext.context.ViewCertificadoAlumno.Where(x => x.UsuarioId == UsuarioId).AsQueryable();

                if (CadenaBuscar != null)
                {
                    foreach (var token in CadenaBuscar.Split(' '))
                        query = query.Where(x => x.Nombre.Contains(token)).ToList();
                }

                query = query.OrderByDescending(x => x.CursoOnlineId).ToList();
                LstMatriculaCursoOnline = query.ToPagedList(this.p, ConstantHelpers.DEFAULT_PAGE_SIZE);
            }
            else
            {
                var query = cursosBL.ListarCertificadoAlumnoPorUsuarioId((int)this.UsuarioId);// dataContext.context.ViewCertificadoAlumno.Where(x => x.UsuarioId == UsuarioId).AsQueryable();

                if (CadenaBuscar != null)
                {
                    foreach (var token in CadenaBuscar.Split(' '))
                        query = query.Where(x => x.Nombre.Contains(token)).ToList();
                }

                query = query.OrderByDescending(x => x.CursoOnlineMaestroId).ToList();
                LstMatriculaCursoOnline = query.ToPagedList(this.p, ConstantHelpers.DEFAULT_PAGE_SIZE);
            }

            //var query = cursosBL.ListarCertificadoAlumnoPorUsuarioId((int)this.UsuarioId);// dataContext.context.ViewCertificadoAlumno.Where(x => x.UsuarioId == UsuarioId).AsQueryable();

            //if (CadenaBuscar != null)
            //{
            //    foreach (var token in CadenaBuscar.Split(' '))
            //        query = query.Where(x => x.Nombre.Contains(token)).ToList();
            //}

            //query = query.OrderByDescending(x => x.CursoOnlineMaestroId).ToList();
            //LstMatriculaCursoOnline = query.ToPagedList(this.p, ConstantHelpers.DEFAULT_PAGE_SIZE);
        }
    }


}

