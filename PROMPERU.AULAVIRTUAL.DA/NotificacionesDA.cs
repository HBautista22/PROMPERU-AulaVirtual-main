using PROMPERU.AULAVIRTUAL.BE.CURSOS;
using PROMPERU.AULAVIRTUAL.Helpers;
using System;
using System.Collections.Generic;
using System.Data;
using System.Data.SqlClient;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace PROMPERU.AULAVIRTUAL.DA
{
    public class NotificacionesDA
    {
        public List<Notificaciones> ListarNotificaciones()
        {
            List<Notificaciones> retVal = new List<Notificaciones>();

            DataSet ds = new DataSet();

            string cadenaConexion = Util.CadenaConexion;

            using (SqlConnection conexion = new SqlConnection(cadenaConexion))
            {
                conexion.Open();

                using (SqlCommand cmdSQL = new SqlCommand())
                {
                    cmdSQL.Connection = conexion;
                    cmdSQL.CommandType = CommandType.StoredProcedure;
                    cmdSQL.CommandText = "USP_Notificaciones_LIS";


                    using (SqlDataAdapter da = new SqlDataAdapter(cmdSQL))
                    {
                        da.Fill(ds);
                    }
                }
                if (conexion.State == ConnectionState.Open)
                {
                    conexion.Close();
                }
            }

            DataTable dt1 = new DataTable();
            DataTable dt2 = new DataTable();

            dt1 = ds.Tables[0];
            dt2 = ds.Tables[1];

            List<TipoNotificaciones> tmpTipoNotificaciones = new List<TipoNotificaciones>();

            foreach (DataRow dr in dt2.Rows)
            {
                TipoNotificaciones obj = new TipoNotificaciones();
                obj.TipoNotificacionId = (int)(dr["TipoNotificacionId"]);
                obj.Nombre = (string)(dr["Nombre"]);
                obj.Codigo = (string)(dr["Codigo"]);
                obj.Estado = (string)(dr["Estado"]);

                tmpTipoNotificaciones.Add(obj);
            }


            foreach (DataRow dr in dt1.Rows)
            {
                Notificaciones obj = new Notificaciones();
                obj.NotificacionId = (int)(dr["NotificacionId"]);
                obj.TipoNotificacionId = (int)(dr["TipoNotificacionId"]);
                obj.Nombre = (string)(dr["Nombre"]);
                obj.Titulo = (string)(dr["Titulo"]);
                obj.Contenido = (string)(dr["Contenido"]);
                obj.Estado = (string)(dr["Estado"]);
                obj.CursoOnlineId = (dr["CursoOnlineId"].ToString() != String.Empty ? (int?)(dr["CursoOnlineId"]) : null);
                obj.FechaRegistro = (dr["FechaRegistro"].ToString() != String.Empty ? (DateTime)(dr["FechaRegistro"]) : DateTime.MinValue);
                obj.TipoNotificaciones = tmpTipoNotificaciones.First(x=>x.TipoNotificacionId == obj.TipoNotificacionId);

                retVal.Add(obj);
            }
            return retVal;

        }

        public int ActualizarNotificacion(Notificaciones notificaciones_)
        {
            int lintResultado = 0;

            string cadenaConexion = Util.CadenaConexion;

            using (SqlConnection conexion = new SqlConnection(cadenaConexion))
            {
                conexion.Open();

                using (SqlCommand cmdSQL = new SqlCommand())
                {
                    cmdSQL.Connection = conexion;
                    cmdSQL.CommandType = CommandType.StoredProcedure;
                    cmdSQL.CommandText = "USP_Notificaciones_UPD";

                    cmdSQL.Parameters.Add("@NotificacionId", SqlDbType.Int).Value = notificaciones_.NotificacionId;
                    cmdSQL.Parameters.Add("@Nombre", SqlDbType.VarChar).Value = notificaciones_.Nombre;
                    cmdSQL.Parameters.Add("@Titulo", SqlDbType.VarChar).Value = notificaciones_.Titulo;
                    cmdSQL.Parameters.Add("@TipoNotificacionId", SqlDbType.Int).Value = notificaciones_.TipoNotificacionId;
                    cmdSQL.Parameters.Add("@Estado", SqlDbType.VarChar).Value = notificaciones_.Estado;
                    cmdSQL.Parameters.Add("@Contenido", SqlDbType.VarChar).Value = notificaciones_.Contenido;
                    cmdSQL.Parameters.Add("@CursoOnlineId", SqlDbType.Int).Value = notificaciones_.CursoOnlineId ;

                    //cmdSQL.Parameters.Add("@NotificacionId", SqlDbType.Int).Value = notificacionId;
                    //cmdSQL.Parameters.Add("@Estado", SqlDbType.VarChar, -1).Value = estado;

                    lintResultado = cmdSQL.ExecuteNonQuery();
                }
                if (conexion.State == ConnectionState.Open)
                {
                    conexion.Close();
                }
            }

            return lintResultado;
        }

        public int InsertarNotificacion(Notificaciones notificaciones_)
        {
            int lintResultado = 0;

            string cadenaConexion = Util.CadenaConexion;

            using (SqlConnection conexion = new SqlConnection(cadenaConexion))
            {
                conexion.Open();

                using (SqlCommand cmdSQL = new SqlCommand())
                {
                    cmdSQL.Connection = conexion;
                    cmdSQL.CommandType = CommandType.StoredProcedure;
                    cmdSQL.CommandText = "USP_Notificaciones_INS";

                    cmdSQL.Parameters.Add("@Nombre", SqlDbType.VarChar).Value = notificaciones_.Nombre;
                    cmdSQL.Parameters.Add("@Titulo", SqlDbType.VarChar).Value = notificaciones_.Titulo;
                    cmdSQL.Parameters.Add("@TipoNotificacionId", SqlDbType.Int).Value = notificaciones_.TipoNotificacionId;
                    cmdSQL.Parameters.Add("@Estado", SqlDbType.VarChar).Value = notificaciones_.Estado;
                    cmdSQL.Parameters.Add("@Contenido", SqlDbType.VarChar).Value = notificaciones_.Contenido;
                    cmdSQL.Parameters.Add("@CursoOnlineId", SqlDbType.Int).Value = notificaciones_.CursoOnlineId;
                    //cmdSQL.Parameters.Add("@NotificacionId", SqlDbType.Int).Value = notificacionId;
                    //cmdSQL.Parameters.Add("@Estado", SqlDbType.VarChar, -1).Value = estado;

                    lintResultado = cmdSQL.ExecuteNonQuery();
                }
                if (conexion.State == ConnectionState.Open)
                {
                    conexion.Close();
                }
            }

            return lintResultado;
        }

        public int EliminarNotificacion(int notificacionId)
        {
            int lintResultado;

            string cadenaConexion = Util.CadenaConexion;

            using (SqlConnection conexion = new SqlConnection(cadenaConexion))
            {
                conexion.Open();

                using (SqlCommand cmdSql = new SqlCommand())
                {
                    cmdSql.Connection = conexion;
                    cmdSql.CommandType = CommandType.StoredProcedure;
                    cmdSql.CommandText = "USP_Notificacion_DEL";

                    cmdSql.Parameters.Add("@NotificacionId", SqlDbType.Int).Value = notificacionId;

                    lintResultado = cmdSql.ExecuteNonQuery();
                }
                if (conexion.State == ConnectionState.Open)
                {
                    conexion.Close();
                }
            }

            return lintResultado;
        }

        public int ChangeStateNotificacion(int notificacionId, string estado)
        {
            int lintResultado = 0;

            string cadenaConexion = Util.CadenaConexion;

            using (SqlConnection conexion = new SqlConnection(cadenaConexion))
            {
                conexion.Open();

                using (SqlCommand cmdSQL = new SqlCommand())
                {
                    cmdSQL.Connection = conexion;
                    cmdSQL.CommandType = CommandType.StoredProcedure;
                    cmdSQL.CommandText = "USP_Notificaciones_UPD_Estado";

                    cmdSQL.Parameters.Add("@NotificacionId", SqlDbType.Int).Value = notificacionId;
                    cmdSQL.Parameters.Add("@Estado", SqlDbType.VarChar, -1).Value = estado;

                    lintResultado = cmdSQL.ExecuteNonQuery();
                }
                if (conexion.State == ConnectionState.Open)
                {
                    conexion.Close();
                }
            }

            return lintResultado;
        }

        public List<TipoNotificaciones> ListarTipoNotificaciones()
        {
            List<TipoNotificaciones> retVal = new List<TipoNotificaciones>();

            DataTable dt = new DataTable();

            string cadenaConexion = Util.CadenaConexion;

            using (SqlConnection conexion = new SqlConnection(cadenaConexion))
            {
                conexion.Open();

                using (SqlCommand cmdSQL = new SqlCommand())
                {
                    cmdSQL.Connection = conexion;
                    cmdSQL.CommandType = CommandType.StoredProcedure;
                    cmdSQL.CommandText = "USP_TipoNotificaciones_LIS";


                    using (SqlDataAdapter da = new SqlDataAdapter(cmdSQL))
                    {
                        da.Fill(dt);
                    }
                }
                if (conexion.State == ConnectionState.Open)
                {
                    conexion.Close();
                }
            }

            foreach (DataRow dr in dt.Rows)
            {
                TipoNotificaciones obj = new TipoNotificaciones();
                obj.TipoNotificacionId = (int)(dr["TipoNotificacionId"]);
                obj.Nombre = (string)(dr["Nombre"]);
                obj.Codigo = (string)(dr["Codigo"]);
                obj.Estado = (string)(dr["Estado"]);

                retVal.Add(obj);
            }

            return retVal;
        }
    }
}
