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
namespace PROMPERU.AULAVIRTUAL.WEB.ViewModel.Course
{
    public class ViewEnlancesInteresViewModel
    {
       



        public int EnlaceInteresId { get; set; }
        public string Titulo { get; set; }
        public string Imagen { get; set; }
        public string Descripcion { get; set; }
        public string Tipo { get; set; }
        public string CodigoYoutube { get; set; }
        public string Url { get; set; }
        public string Estado { get; set; }
        public Nullable<System.DateTime> FechaEdicion { get; set; }
        public Nullable<int> UsuarioEdicionId { get; set; }
        public string Pdf { get; set; }

        public List<EnlacesInteres> LstEnlancesInteresOnline { get; set; }

        public ViewEnlancesInteresViewModel()
        {
        }

        public void CargarDatos(CargarDatosContext dataContext)
        {
            CursosBL cursosBL = new CursosBL();
            

            var query = cursosBL.ListaEnlancesInteres();// dataContext.context.ViewCertificadoAlumno.Where(x => x.UsuarioId == UsuarioId).AsQueryable();

           

            query = query.OrderByDescending(x => x.EnlaceInteresId).ToList();
            LstEnlancesInteresOnline = query.ToList();
        }
    }
}