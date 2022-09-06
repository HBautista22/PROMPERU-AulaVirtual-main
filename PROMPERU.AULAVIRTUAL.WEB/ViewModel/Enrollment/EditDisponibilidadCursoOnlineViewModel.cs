using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using PROMPERU.AULAVIRTUAL.BE.CURSOS;
using PROMPERU.AULAVIRTUAL.WEB.Controllers;
using PROMPERU.AULAVIRTUAL.WEB.Helpers;
using System.ComponentModel.DataAnnotations;
//using System.Data.Entity;

namespace PROMPERU.AULAVIRTUAL.WEB.ViewModel.Enrollment
{
    public class EditDisponibilidadCursoOnlineViewModel
    {
        public Int32? DisponibilidadCursoOnlineId { get; set; }

        [Display(Name = "Categoria")]
        public Int32 CategoriaCursoOnlineId { get; set; }

        [Required]
        [Display(Name = "Curso")]
        public Int32 CursoOnlineId { get; set; }

        [Display(Name = "Fecha de Inicio")]
        [Required]
        public DateTime FechaInicio { get; set; }

        [Display(Name = "Fecha de Finalización")]
        public DateTime? FechaFin { get; set; }

        public List<CategoriaCursoOnline> LstCategoriaCursOnline { get; set; }
        public List<CursoOnline> LstCursoOnline { get; set; }

        public EditDisponibilidadCursoOnlineViewModel()
        {
        }

        public void CargarDatos(Int32? disponibilidadCursoOnlineId)
        {
            this.DisponibilidadCursoOnlineId = disponibilidadCursoOnlineId;

            if (disponibilidadCursoOnlineId.HasValue)
            {
                //var DisponibilidadCursoOnline = dataContext.context.DisponibilidadCursoOnline.Find(disponibilidadCursoOnlineId);
                //this.DisponibilidadCursoOnlineId = DisponibilidadCursoOnline.DisponibilidadCursoOnlineId;
                //this.CursoOnlineId = DisponibilidadCursoOnline.CursoOnlineId;
                //this.CategoriaCursoOnlineId = dataContext.context.CursoOnline.Find(this.CursoOnlineId).CategoriaCursoOnlineId;
                //this.FechaInicio = DisponibilidadCursoOnline.FechaInicio;
                //this.FechaFin = DisponibilidadCursoOnline.FechaFin;
            }
            //LstCategoriaCursOnline = dataContext.context.CategoriaCursoOnline.ToList();
            //LstCursoOnline = dataContext.context.CursoOnline.Where(x => x.Estado == ConstantHelpers.ESTADO.ACTIVO).ToList();
        }
    }
}
