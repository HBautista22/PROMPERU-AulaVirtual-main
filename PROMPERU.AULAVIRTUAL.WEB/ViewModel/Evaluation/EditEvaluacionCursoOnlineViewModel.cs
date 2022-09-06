using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using PROMPERU.AULAVIRTUAL.BE.CURSOS;
using PROMPERU.AULAVIRTUAL.WEB.Controllers;
using PROMPERU.AULAVIRTUAL.WEB.Helpers;
using System.ComponentModel.DataAnnotations;

//using System.Data.Entity;

namespace PROMPERU.AULAVIRTUAL.WEB.ViewModel.Evaluation
{
    public class EditEvaluacionCursoOnlineViewModel
    {
        public Int32? EvaluacionCursoOnlineId { get; set; }

        [Display(Name = "MatriculaCursoOnlineId")]
        [Required]
        public Int32 MatriculaCursoOnlineId { get; set; }
        [Display(Name = "FechaInicio")]
        [Required]
        public DateTime FechaInicio { get; set; }
        [Display(Name = "FechaFin")]
        public DateTime? FechaFin { get; set; }
        [Display(Name = "Nota")]
        public Decimal? Nota { get; set; }
        [Display(Name = "PorcentajeAprobacion")]
        [Required]
        public Decimal PorcentajeAprobacion { get; set; }
        [Display(Name = "PuntajeTotal")]
        [Required]
        public Decimal PuntajeTotal { get; set; }
        [Display(Name = "PuntajeObtenido")]
        public Decimal? PuntajeObtenido { get; set; }

        public List<MatriculaCursoOnline> LstMatriculaCursoOnline { get; set; }

        public EditEvaluacionCursoOnlineViewModel()
        {
        }

        public void CargarDatos(Int32? EvaluacionCursoOnlineId)
        {
            this.EvaluacionCursoOnlineId = EvaluacionCursoOnlineId;

            if (EvaluacionCursoOnlineId.HasValue)
            {
                var EvaluacionCursoOnline = new EvaluacionCursoOnline(); //dataContext.context.EvaluacionCursoOnline.First(x => x.EvaluacionCursoOnlineId == EvaluacionCursoOnlineId);
                this.EvaluacionCursoOnlineId = EvaluacionCursoOnline.EvaluacionCursoOnlineId;
                this.MatriculaCursoOnlineId = EvaluacionCursoOnline.MatriculaCursoOnlineId;
                this.FechaInicio = EvaluacionCursoOnline.FechaInicio;
                this.FechaFin = EvaluacionCursoOnline.FechaFin;
                this.Nota = EvaluacionCursoOnline.Nota;
                this.PorcentajeAprobacion = EvaluacionCursoOnline.PorcentajeAprobacion;
                this.PuntajeTotal = EvaluacionCursoOnline.PuntajeTotal;
                this.PuntajeObtenido = EvaluacionCursoOnline.PuntajeObtenido;
            }

            LstMatriculaCursoOnline = new List<MatriculaCursoOnline>();// dataContext.context.MatriculaCursoOnline.ToList();
            }
    }
}
