using System.Collections.Generic;
using PROMPERU.AULAVIRTUAL.BE.CURSOS;
using PROMPERU.AULAVIRTUAL.DA;

namespace PROMPERU.AULAVIRTUAL.BL
{
    public class NotificacionesBL
    {
        public List<Notificaciones> ListarNotificaciones()
        {
            NotificacionesDA usuarioSql = new NotificacionesDA();
            return usuarioSql.ListarNotificaciones();
        }

        public List<TipoNotificaciones> ListarTipoNotificaciones()
        {
            NotificacionesDA usuarioSql = new NotificacionesDA();
            return usuarioSql.ListarTipoNotificaciones();
        }

        public int ChangeStateNotificacion(int notificacionId, string estado)
        {
            NotificacionesDA usuarioSql = new NotificacionesDA();
            return usuarioSql.ChangeStateNotificacion(notificacionId, estado);
        }

        public int InsertarNotificacion(Notificaciones notificaciones)
        {
            NotificacionesDA usuarioSql = new NotificacionesDA();
            return usuarioSql.InsertarNotificacion(notificaciones);
        }

        public int ActualizarNotificacion(Notificaciones notificaciones)
        {
            NotificacionesDA usuarioSql = new NotificacionesDA();
            return usuarioSql.ActualizarNotificacion(notificaciones);
        }

        public int EliminarNotificacion(int notificacionId)
        {
            NotificacionesDA usuarioSql = new NotificacionesDA();
            return usuarioSql.EliminarNotificacion(notificacionId);
        }
    }
}
