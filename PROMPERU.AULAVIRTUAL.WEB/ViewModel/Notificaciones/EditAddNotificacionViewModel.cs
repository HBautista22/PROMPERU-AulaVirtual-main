using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Web;
using PROMPERU.AULAVIRTUAL.WEB.Controllers;
using PROMPERU.AULAVIRTUAL.WEB.Helpers;
using PROMPERU.AULAVIRTUAL.BE.CURSOS;
using PROMPERU.AULAVIRTUAL.BL;

namespace PROMPERU.AULAVIRTUAL.WEB.ViewModel.Notificaciones
{
    public class EditAddNotificacionViewModel
    {
        NotificacionesBL notificacionesBL = new NotificacionesBL();
        CursosBL cursosBL = new CursosBL();
        public Int32? NotificacionId { get; set; }
        
        [Display(Name = "Titulo")]
        [Required]
        public String Titulo { get; set; }

        [Display(Name = "Asunto")]
        [Required]
        public String Nombre { get; set; }

        [Display(Name = "Contenido")]
        [Required]
        public String Contenido { get; set; }
        public Int32? TipoNotificacionId { get; set; }

        [Display(Name = "¿Esta Activo?")]
        [Required]
        public Boolean Estado { get; set; }

        public Int32? CursoOnlineId { get; set; }

        public List<CursoOnline> LstCursoOnline { get; set; }
        public List<TipoNotificaciones> LstTipoNotificaciones { get; set; }

        public EditAddNotificacionViewModel()
        {
            LstTipoNotificaciones = new List<TipoNotificaciones>();
        }

        public void CargarDatos(CargarDatosContext dataContext, Int32? NotificacionId)
        {
            this.NotificacionId = NotificacionId;

            //LstTipoNotificaciones = dataContext.context.TipoNotificaciones.ToList();
            LstTipoNotificaciones = notificacionesBL.ListarTipoNotificaciones();
            LstCursoOnline = cursosBL.ListarCursoOnline().Where(x=> x.Estado==ConstantHelpers.ESTADO.ACTIVO).ToList();
            

            if (NotificacionId.HasValue)
            {
                //var Notificaciones_ = dataContext.context.Notificaciones.Find(NotificacionId);
                BE.CURSOS.Notificaciones Notificaciones_ = notificacionesBL.ListarNotificaciones().Find(x => x.NotificacionId == NotificacionId);
                this.NotificacionId = Notificaciones_.NotificacionId;
                this.TipoNotificacionId = Notificaciones_.TipoNotificacionId;
                this.Titulo = Notificaciones_.Titulo;
                this.Contenido = Notificaciones_.Contenido;
                this.Nombre = Notificaciones_.Nombre;
                this.Estado = (Notificaciones_.Estado == ConstantHelpers.ESTADO.ACTIVO);
                this.CursoOnlineId = Notificaciones_.CursoOnlineId;
            }
        }
    }
}