using PROMPERU.AULAVIRTUAL.BE.CURSOS;
using PROMPERU.AULAVIRTUAL.BE.PARAMETROS;
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
    public class JsonDA
    {
        public List<Provincia> ListarProvinciaPorDepartamentoPorEstado(int? departamentoId, int estadoId)
        {

            List<Provincia> retVal = new List<Provincia>();

            DataTable dt = new DataTable();

            string cadenaConexion = Util.CadenaConexion;

            using (SqlConnection conexion = new SqlConnection(cadenaConexion))
            {
                conexion.Open();

                using (SqlCommand cmdSQL = new SqlCommand())
                {
                    cmdSQL.Connection = conexion;
                    cmdSQL.CommandType = CommandType.StoredProcedure;
                    //cmdSQL.CommandText = "USP_Provincia_LIS_PorDepartamentoIdPorEstadoId";
                    cmdSQL.CommandText = "USP_Provincia_LIS";

                    //cmdSQL.Parameters.Add("@DepartamentoId", SqlDbType.Int).Value = departamentoId;
                    //cmdSQL.Parameters.Add("@EstadoId", SqlDbType.Int).Value = estadoId;

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
                Provincia obj = new Provincia();
                obj.DepartamentoId = Convert.ToInt32(dr["DepartamentoId"].ToString());
                obj.ProvinciaId = Convert.ToInt32(dr["ProvinciaId"].ToString());
                obj.EstadoId = Convert.ToInt32(dr["EstadoId"].ToString());
                obj.Nombre = dr["Nombre"].ToString();

                retVal.Add(obj);
            }


            return retVal;
        }

        public List<Distrito> ListarDistritoPorProvinciaPorEstado(int? provinciaId, int estadoId)
        {

            List<Distrito> retVal = new List<Distrito>();

            DataTable dt = new DataTable();

            string cadenaConexion = Util.CadenaConexion;

            using (SqlConnection conexion = new SqlConnection(cadenaConexion))
            {
                conexion.Open();

                using (SqlCommand cmdSQL = new SqlCommand())
                {
                    cmdSQL.Connection = conexion;
                    cmdSQL.CommandType = CommandType.StoredProcedure;

                    cmdSQL.CommandText = "USP_Distrito_LIS";

                    //cmdSQL.CommandText = "USP_Distrito_LIS_PorProvinciaIdPorEstadoId";
                    //cmdSQL.Parameters.Add("@ProvinciaId", SqlDbType.Int).Value = provinciaId;
                    //cmdSQL.Parameters.Add("@EstadoId", SqlDbType.Int).Value = estadoId;


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
                Distrito obj = new Distrito();
                obj.DepartamentoId = Convert.ToInt32(dr["DepartamentoId"].ToString());
                obj.ProvinciaId = Convert.ToInt32(dr["ProvinciaId"].ToString());
                obj.DistritoId = Convert.ToInt32(dr["DistritoId"].ToString());
                obj.Nombre = dr["Nombre"].ToString();
                obj.EstadoId = Convert.ToInt32(dr["EstadoId"].ToString());

                retVal.Add(obj);
            }


            return retVal;
        }

        public int InsertarLogUnidad(LogUnidad logUnidad)
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
                    cmdSQL.CommandText = "USP_LogUnidad_INS";

                    cmdSQL.Parameters.Add("@UsuarioId", SqlDbType.Int).Value = logUnidad.UsuarioId;
                    cmdSQL.Parameters.Add("@UnidadCursoOnlineId", SqlDbType.Int).Value = logUnidad.UnidadCursoOnlineId;
                    cmdSQL.Parameters.Add("@MatriculaCursoOnlineId", SqlDbType.Int).Value = logUnidad.MatriculaCursoOnlineId;
                    cmdSQL.Parameters.Add("@FechaRegistro", SqlDbType.DateTime).Value = logUnidad.FechaRegistro;
                    cmdSQL.Parameters.Add("@Estado", SqlDbType.VarChar, -1).Value = logUnidad.Estado;

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
