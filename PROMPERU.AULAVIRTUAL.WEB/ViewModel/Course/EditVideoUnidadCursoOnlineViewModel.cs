using PROMPERU.AULAVIRTUAL.BE.CURSOS;
using PROMPERU.AULAVIRTUAL.BL;
using PROMPERU.AULAVIRTUAL.WEB.Controllers;
using PROMPERU.AULAVIRTUAL.WEB.Helpers;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Web;

namespace PROMPERU.AULAVIRTUAL.WEB.ViewModel.Course
{
    public class EditVideoUnidadCursoOnlineViewModel
    {
		CursosBL cursosBL = new CursosBL();
		public Int32? VideoUnidadCursoOnlineId { get; set; }
		public Int32 UnidadCursoOnlineUnidadCursoOnlineId { get; set; }
        public String Titulo { get; set; }
        public String CodigoYoutube { get; set; }
        public String Duracion { get; set; }
        public String Descripcion { get; set; }
		public Boolean Estado { get; set; }
        public String Tipo { get; set; }
        public string Transcripcion { get;  set; }
        public Nullable<DateTime> FechaTransmision { get; set; }

		public EditVideoUnidadCursoOnlineViewModel()
		{
		}

		public void CargarDatos(CargarDatosContext dataContext, Int32 unidadCursoOnlineId, Int32? videoUnidadCursoOnlineId)
		{
			this.VideoUnidadCursoOnlineId = videoUnidadCursoOnlineId;
			this.UnidadCursoOnlineUnidadCursoOnlineId = unidadCursoOnlineId;
			if (videoUnidadCursoOnlineId.ToString() != "")
			{
				//ObtenerVideoUnidadCursoOnlinePorId
				var videoUnidadCursoOnline = cursosBL.ObtenerVideoUnidadCursoOnlinePorId(videoUnidadCursoOnlineId);
				//var videoUnidadCursoOnline = dataContext.context.VideoUnidadCursoOnline.Find(videoUnidadCursoOnlineId);
				this.VideoUnidadCursoOnlineId = videoUnidadCursoOnline.VideoUnidadCursoOnlineId;
				this.Titulo = videoUnidadCursoOnline.Titulo;
				this.CodigoYoutube = videoUnidadCursoOnline.CodigoYoutube;
				this.Duracion = videoUnidadCursoOnline.Duracion;
				this.Descripcion = videoUnidadCursoOnline.Descripcion;
				this.Estado = (videoUnidadCursoOnline.Estado == ConstantHelpers.ESTADO.ACTIVO);
				this.Tipo = videoUnidadCursoOnline.Tipo;
				this.Transcripcion = videoUnidadCursoOnline.Transcripcion;
				this.FechaTransmision = videoUnidadCursoOnline.FechaTransmision;

			}
		}
	}
}