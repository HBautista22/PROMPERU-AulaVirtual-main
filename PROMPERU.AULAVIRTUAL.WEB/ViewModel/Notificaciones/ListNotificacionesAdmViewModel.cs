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
    public class ListNotificacionesAdmViewModel
    {
        NotificacionesBL notificacionesBL = new NotificacionesBL();
        public Int32 p { get; set; }
        //public IPagedList<ViewListaNotificaciones> LstNotificaciones { get; set; }
        public IPagedList<PROMPERU.AULAVIRTUAL.BE.CURSOS.Notificaciones> LstNotificaciones { get; set; }

        [Display(Name = "Buscar Notificación")]
        public String CadenaBuscar { get; set; }
        [Display(Name = "Tipo Notificación")]
        public Int32? TipoNotificacionId { get; set; }

        public Int32? NotificacionId { get; set; }

        public List<TipoNotificaciones> LstTipoNotificaciones { get; set; }

        public ListNotificacionesAdmViewModel()
        {

        }
        public void CargarDatos(CargarDatosContext dataContext, Int32? p, String cadenaBuscar, Int32? TipoNotificacionId, String Rol, int UsuarioCreacion)
        {
            this.p = p ?? 1;
            this.CadenaBuscar = cadenaBuscar;

            //this.LstTipoNotificaciones = dataContext.context.TipoNotificaciones.Where(x => !x.Estado.Equals(ConstantHelpers.ESTADO.ELIMINADO)).ToList();
            this.LstTipoNotificaciones = notificacionesBL.ListarTipoNotificaciones().Where(x => !x.Estado.Equals(ConstantHelpers.ESTADO.ELIMINADO)).ToList(); // dataContext.context.TipoNotificaciones.Where(x => !x.Estado.Equals(ConstantHelpers.ESTADO.ELIMINADO)).ToList();


            //var query = dataContext.context.ViewListaNotificaciones.AsQueryable();
            List<PROMPERU.AULAVIRTUAL.BE.CURSOS.Notificaciones> query = notificacionesBL.ListarNotificaciones().Where(x => !x.Estado.Equals(ConstantHelpers.ESTADO.ELIMINADO)).ToList();

            this.NotificacionId = TipoNotificacionId;

            if (TipoNotificacionId.HasValue)
            {
                this.TipoNotificacionId = TipoNotificacionId;
                query = query.Where(x => x.TipoNotificacionId == TipoNotificacionId).ToList();
            }

            if (!String.IsNullOrEmpty(CadenaBuscar))
            {
                foreach (var token in CadenaBuscar.Split(' '))
                    query = query.Where(x => x.Titulo.ToUpper().Contains(token.ToUpper())).ToList();
            }

            //if (Rol.Equals(ConstantHelpers.ROL.PROFESOR))
            //{
            //    query = query.Where(x => x. == UsuarioCreacion);
            //}

            query = query.OrderBy(x => x.Titulo).ToList();
            LstNotificaciones = query.ToPagedList(this.p, ConstantHelpers.DEFAULT_PAGE_SIZE);
        }


    }
}