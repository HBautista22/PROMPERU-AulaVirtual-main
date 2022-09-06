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
    public class AsesoriaAsesoradoDA
    {
        public List<AsesoriaRemota> ListarAsesoriaPorAsesoradoBloques(int id, DateTime inicio, DateTime fin)
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
                    cmdSQL.CommandText = "USP_AsesoriaRemota_LIS_PorAsesoradoBloques";

                    cmdSQL.Parameters.Add("@Sola_AsesoradoId", SqlDbType.Int).Value = id;
                    cmdSQL.Parameters.Add("@inicio", SqlDbType.DateTime).Value = inicio;
                    cmdSQL.Parameters.Add("@fin", SqlDbType.DateTime).Value = fin;

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

                obj.Asre_Inicio = (DateTime)(dr["Asre_Inicio"]);
                obj.Asre_Fin = (DateTime)(dr["Asre_Fin"]);



                retVal.Add(obj);
            }

            return retVal;
        }

        public int ActualizarEstadoSolicitud(int usuarioAsesoradoId, int solicitudId, string estado)
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
                    cmdSQL.CommandText = "USP_SolicitudAsesoriaAsesorado_UPD_Estado";

                    cmdSQL.Parameters.Add("@Sola_AsesoradoId", SqlDbType.Int).Value = usuarioAsesoradoId;
                    cmdSQL.Parameters.Add("@Sola_Id", SqlDbType.Int).Value = solicitudId;
                    cmdSQL.Parameters.Add("@Sola_Estado", SqlDbType.VarChar, -1).Value = estado;

                    lintResultado = cmdSQL.ExecuteNonQuery();
                }
                if (conexion.State == ConnectionState.Open)
                {
                    conexion.Close();
                }
            }

            return lintResultado;
        }

        public List<AsesoriaAsesoradoSolicitud> ListarAsesoriaSolicitudes(int usuarioId)
        {
            List<AsesoriaAsesoradoSolicitud> retVal = new List<AsesoriaAsesoradoSolicitud>();

            DataTable dt = new DataTable();

            string cadenaConexion = Util.CadenaConexion;

            using (SqlConnection conexion = new SqlConnection(cadenaConexion))
            {
                conexion.Open();

                using (SqlCommand cmdSQL = new SqlCommand())
                {
                    cmdSQL.Connection = conexion;
                    cmdSQL.CommandType = CommandType.StoredProcedure;
                    cmdSQL.CommandText = "USP_AsesoriaRemotaAsesorado_LIS_Solicitudes";

                    cmdSQL.Parameters.Add("@Sola_AsesoradoId", SqlDbType.Int).Value = usuarioId;

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
                AsesoriaAsesoradoSolicitud obj = new AsesoriaAsesoradoSolicitud();

                obj.Asre_Id = (int)(dr["Asre_Id"]);
                obj.Asre_Nombre = (string)(dr["Asre_Nombre"]);
                obj.Asre_AsesorId = (int)(dr["Asre_AsesorId"]);
                obj.Asre_Inicio = (DateTime)(dr["Asre_Inicio"]);
                obj.Asre_Fin = (DateTime)(dr["Asre_Fin"]);
                obj.Asre_Estado = (string)(dr["Asre_Estado"]);
                obj.Asre_ConferenciaId = (dr["Asre_ConferenciaId"].ToString());
                obj.ApellidoAsesor = (string)(dr["Apellido"]);
                obj.NombreAsesor = (string)(dr["Nombre"]);
                obj.Cargo = (string)(dr["Cargo"]);
                obj.Sola_Id = (int)(dr["Sola_Id"]);
                obj.Sola_Estado = (string)(dr["Sola_Estado"]);


                retVal.Add(obj);
            }

            return retVal;
        }

        public List<AsesoriaAsesor> ListarAsesoriaPorAsesoradoDisponible(int usuarioId, DateTime start)
        {
            List<AsesoriaAsesor> retVal = new List<AsesoriaAsesor>();

            DataTable dt = new DataTable();

            string cadenaConexion = Util.CadenaConexion;

            using (SqlConnection conexion = new SqlConnection(cadenaConexion))
            {
                conexion.Open();

                using (SqlCommand cmdSQL = new SqlCommand())
                {
                    cmdSQL.Connection = conexion;
                    cmdSQL.CommandType = CommandType.StoredProcedure;
                    cmdSQL.CommandText = "USP_AsesoriaRemotaAsesor_LIS_PorAsesoradoPorInicio";

                    cmdSQL.Parameters.Add("@Sola_AsesoradoId", SqlDbType.Int).Value = usuarioId;
                    cmdSQL.Parameters.Add("@start", SqlDbType.DateTime).Value = start;

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
                AsesoriaAsesor obj = new AsesoriaAsesor();

                obj.Asre_Id = (int)(dr["Asre_Id"]);
                obj.Asre_Nombre = (string)(dr["Asre_Nombre"]);
                obj.Asre_AsesorId = (int)(dr["Asre_AsesorId"]);
                obj.Apellido = (string)(dr["Apellido"]);
                obj.Nombre = (string)(dr["Nombre"]);
                obj.Cargo = (string)(dr["Cargo"]);


                retVal.Add(obj);
            }

            return retVal;
        }

        public int SolicitarAsesoria(int AsesoriaID, int AsesoradoId, string AsesoriaConsulta)
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
                    cmdSQL.CommandText = "USP_SolicitudAsesoria_INS";

                    cmdSQL.Parameters.Add("@Asre_Id", SqlDbType.Int).Value = AsesoriaID;
                    cmdSQL.Parameters.Add("@Sola_AsesoradoId", SqlDbType.Int).Value = AsesoradoId;
                    cmdSQL.Parameters.Add("@Sola_Consulta", SqlDbType.VarChar, -1).Value = AsesoriaConsulta;

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
