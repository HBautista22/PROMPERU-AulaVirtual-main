using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using PROMPERU.AULAVIRTUAL.WEB.Models;
using PROMPERU.AULAVIRTUAL.WEB.Controllers;
using PROMPERU.AULAVIRTUAL.WEB.Helpers;
using System.ComponentModel.DataAnnotations;
using PROMPERU.AULAVIRTUAL.BE.CURSOS;
using PROMPERU.AULAVIRTUAL.BL;
//using System.Data.Entity;
using System.IO;
using PROMPERU.AULAVIRTUAL.BE.USUARIO;

namespace PROMPERU.AULAVIRTUAL.WEB.ViewModel.Course
{
    public class EditCursoOnlineViewModel
    {
        CursosBL cursosBL = new CursosBL();
        UsuarioBL usuarioBL = new UsuarioBL();


        public Int32? CursoOnlineId { get; set; }

        [Required(ErrorMessage = "el campo Código es obligatorio"),
        Display(Name = "Código")]
        [RegularExpression(ConstantHelpers.Expresion_Estandar)]
        public String Codigo { get; set; }

        [Required(ErrorMessage = "el campo Nombre es obligatorio"),
          Display(Name = "Nombre")]
        //[RegularExpression(ConstantHelpers.Expresion_Estandar)]
        public String Nombre { get; set; }

        [Required(ErrorMessage = "el campo Categoria es obligatorio"),
       Display(Name = "Categoria")]
        public Int32 CategoriaCursoOnlineId { get; set; }

        [Display(Name = "Profesor")]
        public Int32? UsuarioProfesor { get; set; }

        [Required(ErrorMessage = "el campo Estado es obligatorio"),
       Display(Name = "¿Está Activo?")]
        public Boolean Estado { get; set; }

        [Required(ErrorMessage = "el campo Descripción es obligatorio"),
         Display(Name = "Descripción")]
        public String Descripcion { get; set; }

        [Required(ErrorMessage = "el campo Numero de preguntas es obligatorio"),
        Display(Name = "Numero de Preguntas de la Evaluación")]
        public Int32 NumPreguntasEvaluacion { get; set; }

        [Required(ErrorMessage = "el campo Tiempo de Evaluación es obligatorio"),
        Display(Name = "Tiempo de Evaluación")]
        public Int32 TiempoEvaluacion { get; set; }

        [Required(ErrorMessage = "el campo Porcentaje de Aprobación es obligatorio"),
        Display(Name = "Porcentaje de Aprobación")]
        public String PorcentajeAprobacion { get; set; }

        public HttpPostedFileBase ArchivoImagen { get; set; }

        [Display(Name = "Imagen")]
        public String RutaImagen { get; set; }

        public List<CategoriaCursoOnline> LstCategoriaCursoOnline { get; set; }

        public List<Usuario> LstUsuarioProfesor { get; set; }

        [Display(Name = "Tiene Examen")]
        public Boolean TieneExamen { get; set; }

        [Required(ErrorMessage = "el campo Auto Matricula es obligatorio"),
        Display(Name = "¿Los usuarios pueden auto-matricularse?")]
        public Boolean AutoMatricula { get; set; }

        [Display(Name = "¿Los usuarios pueden reiniciar una unidad del curso?")]
        public bool ReinicioScorm { get; set; }

        [Display(Name = "¿Disponible para todos los usuarios?")]
        public Boolean DisponibleCurso { get; set; }

        public string RutaBanner { get; set; }
        [Display(Name = "Banner(en .png)")]
        public HttpPostedFileBase BannerImage { get; set; }

        public EditCursoOnlineViewModel()
        {
            this.PorcentajeAprobacion = "70";
        }

        public void CargarDatos(CargarDatosContext dataContext, Int32? CursoOnlineId)
        {
            this.CursoOnlineId = CursoOnlineId;

            if (CursoOnlineId.HasValue)
            {
                CursoOnline cursoOnline = cursosBL.ObtenerCursoOnlinePorId((int)CursoOnlineId);   //dataContext.context.CursoOnline.Find(CursoOnlineId);
                this.CursoOnlineId = cursoOnline.CursoOnlineId;
                this.Codigo = cursoOnline.Codigo;
                this.Nombre = cursoOnline.Nombre;
                this.CategoriaCursoOnlineId = cursoOnline.CategoriaCursoOnlineId;
                this.Estado = (cursoOnline.Estado == ConstantHelpers.ESTADO.ACTIVO);
                this.Descripcion = cursoOnline.Descripcion;
                this.NumPreguntasEvaluacion = cursoOnline.NumPreguntasEvaluacion;
                this.TiempoEvaluacion = cursoOnline.TiempoEvaluacion;
                this.PorcentajeAprobacion = cursoOnline.PorcentajeAprobacion.ToString();
                this.RutaImagen = cursoOnline.RutaImagen;
                this.TieneExamen = (cursoOnline.TieneExamen == ConstantHelpers.ESTADO.ACTIVO);
                this.AutoMatricula = cursoOnline.AutoMatriculaHabilitada;
                this.ReinicioScorm = (bool)cursoOnline.PermiteReinicioScorm;
                this.RutaBanner = cursoOnline.RutaBanner;
                this.DisponibleCurso = (cursoOnline.DisponibilidadCurso == 1);

                if (cursoOnline.UsuarioProfesor.HasValue)
                {
                    this.UsuarioProfesor = (int)cursoOnline.UsuarioProfesor;
                }
            }
            LstCategoriaCursoOnline = cursosBL.ListarCategoriaCursoOnline();
            LstUsuarioProfesor = usuarioBL.ListarProfesoresActivos();
        }
    }
}
