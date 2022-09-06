using System;
using System.Collections.Generic;
using System.Configuration;
using System.Linq;
using System.Net;
using System.Net.Http;
using System.Web.Http;
using System.Web.Http.Cors;
//using PROMPERU.AULAVIRTUAL.WEB.Models;
using PROMPERU.AULAVIRTUAL.BE.CURSOS;
using PROMPERU.AULAVIRTUAL.DA;

namespace PROMPERU.AULAVIRTUAL.WEB.Controllers
{
    public class CourseApiController : ApiController
    {
        CursosDA cursosDA = new CursosDA();
        //TODO : Procedures para poder cargar 

        //protected EvolEntities context;
        //1499.95
        public CourseApiController()
        {
            //context = new EvolEntities();

            
        }

        [HttpGet]
        [EnableCors(origins: "*", headers: "*", methods: "*")]
        public List<CursoOnlineResponse> LstCursos(string filtro = "")
        {
            //var query = //context.MatriculaCursoOnline.AsQueryable();

            //if (!string.IsNullOrEmpty(filtro))
            //    foreach (var item in filtro.Split(' '))
            //    {
            //        query = query.Where(x => x.CursoOnline.Nombre.Contains(item) || x.CursoOnline.Descripcion.Contains(item));
            //    }
            List<CursoOnlineResponse> retVal = cursosDA.ListarCursosOnlineResponse();


            var basePath = ConfigurationManager.AppSettings.Get("file_path");

            foreach (CursoOnlineResponse cursoOnline in retVal)
            {
                cursoOnline.RutaImagen = basePath + cursoOnline.RutaImagen;
            }

            return retVal;
            //return result.ToList();

            //TODO : esto se borra
            //return new List<CursoOnlineResponse>();
        }
    }

    //public class CursoOnlineResponse
    //{
    //    public string Nombre { get; set; }
    //    public string Descripcion { get; set; }
    //    public DateTime? FechaCreacion { get; set; }
    //    public int CursoOnlineId { get; set; }
    //    public string RutaImagen { get; set; }
    //    public int Ranking { get; set; }
    //}
}
