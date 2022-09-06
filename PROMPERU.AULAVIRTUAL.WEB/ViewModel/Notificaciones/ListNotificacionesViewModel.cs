using PagedList;
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
    public class ListNotificacionesViewModel
    {
        NotificacionesBL notificacionesBL = new NotificacionesBL();

        public Int32 p { get; set; }
        public IPagedList<PROMPERU.AULAVIRTUAL.BE.CURSOS.Notificaciones> LstNotificaciones { get; set; }

        [Display(Name = "Buscar Notificación")]
        public String CadenaBuscar { get; set; }
        [Display(Name = "Tipo Notificación")]
        public Int32? TipoNotificacionId { get; set; }
        
        public List<TipoNotificaciones> LstTipoNotificaciones { get; set; }

        public ListNotificacionesViewModel()
        {

        }
        public void CargarDatos(CargarDatosContext dataContext, Int32? p, String cadenaBuscar, Int32? TipoNotificacionId)
        {
            this.p = p ?? 1;
            this.CadenaBuscar = cadenaBuscar;

            //this.LstTipoNotificaciones = dataContext.context.TipoNotificaciones.Where(x => !x.Estado.Equals(ConstantHelpers.ESTADO.ELIMINADO)).ToList();
            this.LstTipoNotificaciones = notificacionesBL.ListarTipoNotificaciones().Where(x => !x.Estado.Equals(ConstantHelpers.ESTADO.ELIMINADO)).ToList(); // dataContext.context.TipoNotificaciones.Where(x => !x.Estado.Equals(ConstantHelpers.ESTADO.ELIMINADO)).ToList();

            //var query = dataContext.context.Notificaciones.Where(x => !x.Estado.Equals(ConstantHelpers.ESTADO.ELIMINADO)).AsQueryable();
            List<PROMPERU.AULAVIRTUAL.BE.CURSOS.Notificaciones> query = notificacionesBL.ListarNotificaciones().Where(x => !x.Estado.Equals(ConstantHelpers.ESTADO.ELIMINADO)).ToList();

            if (TipoNotificacionId.HasValue)
            {
                this.TipoNotificacionId = TipoNotificacionId;
                query = query.Where(x => x.TipoNotificacionId == TipoNotificacionId).ToList();
            }

            if (!String.IsNullOrEmpty(CadenaBuscar))
            {
                foreach (var token in CadenaBuscar.Split(' '))
                    query = query.Where(x => x.Titulo.ToLower().Contains(token.ToLower())).ToList();
            }

            //query = query.OrderBy(x => x.Titulo);
            this.LstNotificaciones = query.ToPagedList(this.p, ConstantHelpers.DEFAULT_PAGE_SIZE);
        }



    }
}