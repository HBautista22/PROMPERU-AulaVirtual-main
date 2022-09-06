using PROMPERU.AULAVIRTUAL.BE.Aesorias;
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
    public class AsesoriaAsesorDA
    {
        public List<AsesoriaRemota> ListarAsesoriaPorAsesor(int asesorId)
        {
            List<AsesoriaRemota> retVal = new List<AsesoriaRemota>();

            DataTable dt = new DataTable();

            string cadenaConexion = Util.CadenaConexion;

            using (SqlConnection conexion = new SqlConnection(cadenaConexion))
            {
                conexion.Open();

                using (SqlCommand cmdSQL = new SqlCommand())
                {
                    cmdSQL.Connection = conexion;
                    cmdSQL.CommandType = CommandType.StoredProcedure;
                    cmdSQL.CommandText = "USP_AsesoriaRemota_LIS_PorAsesor";

                    cmdSQL.Parameters.Add("@Asre_AsesorId", SqlDbType.Int).Value = asesorId;

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
                AsesoriaRemota obj = new AsesoriaRemota();
                obj.Asre_Id = (int)(dr["Asre_Id"]);
                obj.Asre_AsesorId = (int)(dr["Asre_AsesorId"]);
                obj.Asre_Nombre = (string)(dr["Asre_Nombre"]);
                obj.Asre_Inicio = (DateTime)(dr["Asre_Inicio"]);
                obj.Asre_Fin = (DateTime)(dr["Asre_Fin"]);
                obj.Asre_Creacion = (DateTime)(dr["Asre_Creacion"]);
                obj.Asre_ConferenciaId = new Guid(dr["Asre_ConferenciaId"].ToSafeString());
                obj.Asre_Estado = (string)(dr["Asre_Estado"]);
                obj.Asre_Calificacion = (decimal)(dr["Asre_Calificacion"]);
                obj.Sola_Estado = (string)(dr["Sola_Estado"]);


                retVal.Add(obj);
            }

            return retVal;
        }

        public AsesoriasSolicitudCalendar ObtenerDatosSolicitudCalendar(int solicitudId)
        {
            AsesoriasSolicitudCalendar retVal = new AsesoriasSolicitudCalendar();

            DataTable dt = new DataTable();

            string cadenaConexion = Util.CadenaConexion;

            using (SqlConnection conexion = new SqlConnection(cadenaConexion))
            {
                conexion.Open();

                using (SqlCommand cmdSQL = new SqlCommand())
                {
                    cmdSQL.Connection = conexion;
                    cmdSQL.CommandType = CommandType.StoredProcedure;
                    cmdSQL.CommandText = "USP_AsesoriaRemotaParaCalendar_SEL_PorSolicitudId";

                    cmdSQL.Parameters.Add("@Sola_Id", SqlDbType.Int).Value = solicitudId;

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

            AsesoriasSolicitudCalendar obj = new AsesoriasSolicitudCalendar();

            foreach (DataRow dr in dt.Rows)
            {

                obj.AsesoradoEmail = (string)(dr["AsesoradoEmail"]);
                obj.AsesoradoName = (string)(dr["AsesoradoNombre"]);
                obj.AsesorEmail = (string)(dr["AsesorEmail"]);
                obj.AsesorName = (string)(dr["AsesorNombre"]);
                obj.AsesoriaInicio = (DateTime)(dr["Asre_Inicio"]);
                obj.AsesoriaFin = (DateTime)(dr["Asre_Fin"]);
                obj.TituloAsesoria = (string)(dr["Asre_Nombre"]);
            }

            retVal = obj;

            return retVal;
        }

        public List<AsesoriaAsesorSolicitud> ListarSolicitudPorAsesor(int asesorId)
        {
            List<AsesoriaAsesorSolicitud> retVal = new List<AsesoriaAsesorSolicitud>();

            DataTable dt = new DataTable();

            string cadenaConexion = Util.CadenaConexion;

            using (SqlConnection conexion = new SqlConnection(cadenaConexion))
            {
                conexion.Open();

                using (SqlCommand cmdSQL = new SqlCommand())
                {
                    cmdSQL.Connection = conexion;
                    cmdSQL.CommandType = CommandType.StoredProcedure;
                    cmdSQL.CommandText = "USP_AsesoriaRemotaSolicitud_LIS_PorAsesor";

                    cmdSQL.Parameters.Add("@Asre_AsesorId", SqlDbType.Int).Value = asesorId;

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
                AsesoriaAsesorSolicitud obj = new AsesoriaAsesorSolicitud();
                obj.Asre_Id = (int)(dr["Asre_Id"]);
                obj.Asre_Nombre = (string)(dr["Asre_Nombre"]);
                obj.Asre_Inicio = (DateTime)(dr["Asre_Inicio"]);
                obj.Asre_Fin = (DateTime)(dr["Asre_Fin"]);
                obj.Asre_ConferenciaId = new Guid(dr["Asre_ConferenciaId"].ToSafeString());
                obj.Sola_Id = (int)(dr["Sola_Id"]);
                obj.Sola_Estado = (string)(dr["Sola_Estado"]);
                obj.NombreAsesorado = (string)(dr["Nombre"]);
                obj.ApellidoAsesorado = (string)(dr["Apellido"]);
                obj.Email = (string)(dr["Email"]);
                obj.Consulta = (string)(dr["Sola_Consulta"]);
                obj.Cargo = dr["Cargo"].ToString();
                obj.Telefono = dr["Telefono"].ToString();


                retVal.Add(obj);
            }

            return retVal;
        }

        public int ActualizarEstadoAsesoria(int asesorId, int asesoriaId, string estado)
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
                    cmdSQL.CommandText = "USP_Asesoria_UPD_Estado";

                    cmdSQL.Parameters.Add("@Asre_AsesorId", SqlDbType.Int).Value = asesorId;
                    cmdSQL.Parameters.Add("@Asre_Id", SqlDbType.Int).Value = asesoriaId;
                    cmdSQL.Parameters.Add("@Asre_Estado", SqlDbType.VarChar, -1).Value = estado;

                    lintResultado = cmdSQL.ExecuteNonQuery();
                }
                if (conexion.State == ConnectionState.Open)
                {
                    conexion.Close();
                }
            }

            return lintResultado;
        }

        public int SaveAsesoriaAsesor(AsesoriaRemota model)
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
                    cmdSQL.CommandText = "USP_AsesoriaRemota_INS";

                    cmdSQL.Parameters.Add("@Asre_AsesorId", SqlDbType.Int).Value = model.Asre_AsesorId;
                    cmdSQL.Parameters.Add("@Asre_Nombre", SqlDbType.VarChar, -1).Value = model.Asre_Nombre;
                    cmdSQL.Parameters.Add("@Asre_Inicio", SqlDbType.DateTime).Value = model.Asre_Inicio;
                    cmdSQL.Parameters.Add("@Asre_Fin", SqlDbType.DateTime).Value = model.Asre_Fin;

                    lintResultado = cmdSQL.ExecuteNonQuery();
                }
                if (conexion.State == ConnectionState.Open)
                {
                    conexion.Close();
                }
            }

            return lintResultado;
        }

        public int ActualizarEstadoSolicitud(int usuarioAsesorId, int solicitudId, string estado, string comentario)
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
                    cmdSQL.CommandText = "USP_SolicitudAsesoriaAsesor_UPD_Estado";

                    cmdSQL.Parameters.Add("@Asre_AsesorId", SqlDbType.Int).Value = usuarioAsesorId;
                    cmdSQL.Parameters.Add("@Sola_Id", SqlDbType.Int).Value = solicitudId;
                    cmdSQL.Parameters.Add("@Sola_Estado", SqlDbType.VarChar, -1).Value = estado;
                    cmdSQL.Parameters.Add("@Sola_Comentario", SqlDbType.VarChar, -1).Value = comentario;

                    lintResultado = cmdSQL.ExecuteNonQuery();
                }
                if (conexion.State == ConnectionState.Open)
                {
                    conexion.Close();
                }
            }

            return lintResultado;
        }

    }
}
