
namespace PROMPERU.AULAVIRTUAL.BE.CURSOS
{

    using System;
    using System.Collections.Generic;

    public partial class Notificaciones
    {

        public int NotificacionId { get; set; }

        public int TipoNotificacionId { get; set; }

        public string Nombre { get; set; }

        public string Titulo { get; set; }

        public string Contenido { get; set; }

        public string Estado { get; set; }

        public Int32? CursoOnlineId { get; set; }

        public DateTime FechaRegistro { get; set; }

        public virtual TipoNotificaciones TipoNotificaciones { get; set; }

    }

}
