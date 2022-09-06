using PROMPERU.AULAVIRTUAL.BE.CURSOS;
using PROMPERU.AULAVIRTUAL.BL;
using PROMPERU.AULAVIRTUAL.WEB.Controllers;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace PROMPERU.AULAVIRTUAL.WEB.ViewModel.Course
{
    public class VideoUnidadCursoOnlineViewModel
    {
		CursosBL cursosBL = new CursosBL();
		//UsuarioBL usuarioBL = new UsuarioBL();
		//EmpresaBL empresaBL = new EmpresaBL();
		public Int32 UnidadCursoOnlineId { get; set; }

		public UnidadCursoOnline UnidadCursoOnline { get; set; }

		public List<VideoUnidadCursoOnline> LstVideoUnidadCursoOnline { get; set; }

		public VideoUnidadCursoOnlineViewModel()
		{
		}

		public void CargarDatos(CargarDatosContext dataContext, Int32 unidadCursoOnlineId)
		{
			this.UnidadCursoOnlineId = unidadCursoOnlineId;
			UnidadCursoOnline = cursosBL.ObtenerUnidadCursoOnlinePorId(UnidadCursoOnlineId);
			LstVideoUnidadCursoOnline =cursosBL.ListarVideoUnidadCursoOnline(unidadCursoOnlineId).ToList();
		}
	}
}