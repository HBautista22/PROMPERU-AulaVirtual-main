using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using PROMPERU.AULAVIRTUAL.WEB.Controllers;
using PROMPERU.AULAVIRTUAL.WEB.Helpers;
using System.ComponentModel.DataAnnotations;
using PROMPERU.AULAVIRTUAL.BE.CURSOS;
using PROMPERU.AULAVIRTUAL.BL;



namespace PROMPERU.AULAVIRTUAL.WEB.ViewModel.Course
{
    public class EditUnidadCursoOnlineViewModel
    {
        CursosBL cursosBL = new CursosBL();

        public UnidadCursoOnline UnidadPadre { get; set; }
        public int? UnidadCursoOnlinePadreId { get; set; }
        public Int32? UnidadCursoOnlineId { get; set; }


        [Required(ErrorMessage = "El campo CursoId es obligatorio")]
        public Int32 CursoOnlineId { get; set; }
        [Display(Name = "Nombre")]
        [Required(ErrorMessage = "El campo Nombre es obligatorio")]
        //[RegularExpression(ConstantHelpers.Expresion_Estandar, ErrorMessage = ConstantHelpers.Mesage_Expresion_Estandar)]
        public String Nombre { get; set; }
        [Display(Name = "Descripcción")]
        public String Descripcion { get; set; }
        [Display(Name = "Ruta de Archivo Original")]
        public String RutaArchivoOriginal { get; set; }
        [Display(Name = "Ruta externa")]
        public string RutaWeb { get; set; }
        [Display(Name = "Ruta del Archivo de Inicio")]
        public String RutaArchivoInicio { get; set; }
        [Display(Name = "Orden")]
        [Required(ErrorMessage = "El campo Orden es obligatorio")]
        public Int32 Orden { get; set; }
        [Display(Name = "¿Está Activo?")]
        [Required(ErrorMessage = "El campo Estado es obligatorio")]
        public Boolean Estado { get; set; }
        [Display(Name = "Tiempo de Permanencia (min)")]
        [Required(ErrorMessage = "El campo Tiempo de permanencia es obligatorio")]
        public Int32 TiempoPermanencia { get; set; }
        [Display(Name = "Ancho del Contenedor")]
        public Int32? AnchoContenedor { get; set; }
        [Display(Name = "Alto del Contenedor")]
        public Int32? AltoContenedor { get; set; }

        [Display(Name = "Estructura")]
        public Int32? TipoUnidadId { get; set; }

        [Required(ErrorMessage = "El campo Tipo de Unidad es obligatorio")]
        public String TipoUnidad { get; set; }

        public String CodigoCursoOnline { get; set; }

        public String GUID { get; set; }
        public ICollection<TipoUnidad> LstTipoUnidad { get; set; }

        [Display(Name = "Cargar archivo (PDF, Video/mp4 - Video/x-m4v)")]
        public HttpPostedFileBase ArchivoAdicional { get; set; }
        public String RutaArchivoAdicional { get; set; }


        public Boolean HasTarea { get; set; }

        public DateTime? TareaFechaLimite { get; set; }

        public String _TareaFechaLimite { get; set; }
        public EditUnidadCursoOnlineViewModel()
        {
            this.Orden = 1;
            this.TiempoPermanencia = 5;
            this.Estado = true;
        }

        public void CargarDatos(CargarDatosContext dataContext, Int32? unidadCursoOnlineId, Int32 cursoOnlineId, Int32? unidadCursoOnlinePadreId = null)
        {
            CursosBL cursosBL = new CursosBL();
            
            List<UnidadCursoOnline> lstUnidadCursoOnline = cursosBL.ListarUnidadCursoOnlinePorCursoOnlineId(cursoOnlineId);
            
            if (unidadCursoOnlinePadreId.HasValue)
            {
                this.UnidadPadre = lstUnidadCursoOnline.FirstOrDefault(x => x.UnidadCursoOnlinePadreId == UnidadCursoOnlinePadreId); 
                this.UnidadCursoOnlinePadreId = UnidadPadre.UnidadCursoOnlineId;
            }

            var cursoOnline = cursosBL.ObtenerCursoOnlinePorId(cursoOnlineId);  //dataContext.context.CursoOnline.Find(cursoOnlineId);
            this.CodigoCursoOnline = cursoOnline.Codigo;
            this.UnidadCursoOnlineId = unidadCursoOnlineId;
            this.CursoOnlineId = cursoOnlineId;
            this.GUID = Guid.NewGuid().ToString().Substring(0, 6);
            if (UnidadCursoOnlineId.HasValue)
            {
                var unidadCursoOnline = lstUnidadCursoOnline.FirstOrDefault(x => x.UnidadCursoOnlineId == UnidadCursoOnlineId);
                this.UnidadCursoOnlinePadreId = unidadCursoOnline.UnidadCursoOnlinePadreId;
                this.UnidadCursoOnlineId = unidadCursoOnline.UnidadCursoOnlineId;
                this.CursoOnlineId = CursoOnlineId;
                this.Nombre = unidadCursoOnline.Nombre;
                this.Descripcion = unidadCursoOnline.Descripcion;
                this.RutaArchivoOriginal = unidadCursoOnline.RutaArchivoOriginal;
                this.RutaWeb = unidadCursoOnline.RutaWeb;
                this.RutaArchivoInicio = unidadCursoOnline.RutaArchivoInicio;
                this.Orden = unidadCursoOnline.Orden;
                this.Estado = (unidadCursoOnline.Estado == ConstantHelpers.ESTADO.ACTIVO);
                this.TiempoPermanencia = unidadCursoOnline.TiempoPermanencia;
                this.AnchoContenedor = unidadCursoOnline.AnchoContenedor;
                this.AltoContenedor = unidadCursoOnline.AltoContenedor;
                this.GUID = unidadCursoOnline.GUID;
                this.TipoUnidad = unidadCursoOnline.TipoUnidad;
                this.TipoUnidadId = unidadCursoOnline.TipoUnidadId ?? ConstantHelpers.TIPO_UNIDAD.DB_CONTENIDO;
                this.RutaArchivoAdicional = unidadCursoOnline.RutaArchivoAdicional;
                this.HasTarea = unidadCursoOnline.HasTarea;
                this._TareaFechaLimite = unidadCursoOnline.TareaFechaLimite.ToString();

            }
            else
            {
                this.AnchoContenedor = 720;
                this.AltoContenedor = 512;
            }
            this.LstTipoUnidad = cursosBL.ListarTipoUnidad().Where(x => x.Estado == ConstantHelpers.ESTADO.ACTIVO).ToArray();  //dataContext.context.TipoUnidad.Where(x => x.Estado == ConstantHelpers.ESTADO.ACTIVO).ToArray();
            if (UnidadPadre != null)
                this.LstTipoUnidad = cursosBL.ListarTipoUnidad().Where(x => x.TipoUnidadId != UnidadPadre.TipoUnidadId).ToArray();

        }
    }
}
