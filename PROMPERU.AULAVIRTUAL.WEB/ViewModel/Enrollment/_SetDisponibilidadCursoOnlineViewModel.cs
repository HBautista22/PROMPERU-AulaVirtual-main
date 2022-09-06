using PROMPERU.AULAVIRTUAL.WEB.Controllers;
using PROMPERU.AULAVIRTUAL.BE.CURSOS;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Web;

namespace PROMPERU.AULAVIRTUAL.WEB.ViewModel.Enrollment
{
    public class _SetDisponibilidadCursoOnlineViewModel
    {
        public Int32? DisponibilidadCursoOnlineId { get; set; }

        [Required]
        public Int32 CursoOnlineId { get; set; }
        [Required]

        [Display(Name = "Fecha Inicio")]
        [DisplayFormat(DataFormatString = "{0:dd/MM/yyyy}")]
        public DateTime FechaInicio { get; set; }

        [Display(Name = "Fecha Fin")]
        [DisplayFormat(DataFormatString = "{0:dd/MM/yyyy}")]
        public DateTime? FechaFin { get; set; }

        public CursoOnline CursoOnline { get; set; }

        public _SetDisponibilidadCursoOnlineViewModel()
        {
        }

        public void CargarDatos(Int32 cursoOnlineId)
        {
            this.CursoOnlineId = cursoOnlineId;
            this.CursoOnline = new CursoOnline(); // dataContext.context.CursoOnline.Find(CursoOnlineId);

            //var disponibilidadCursoOnline = dataContext.context.DisponibilidadCursoOnline.FirstOrDefault(x => x.CursoOnlineId == this.CursoOnlineId && x.FechaInicio <= DateTime.Now && (x.FechaFin == null || x.FechaFin >= DateTime.Now));

            //if (disponibilidadCursoOnline != null)
            //{
            //    this.DisponibilidadCursoOnlineId = disponibilidadCursoOnline.DisponibilidadCursoOnlineId;
            //    this.FechaFin = disponibilidadCursoOnline.FechaFin;
            //    this.FechaInicio = disponibilidadCursoOnline.FechaInicio;
            //}
            this.FechaInicio = DateTime.Now;
        }
    }
}