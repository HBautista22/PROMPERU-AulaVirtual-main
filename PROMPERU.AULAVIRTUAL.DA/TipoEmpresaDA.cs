using PROMPERU.AULAVIRTUAL.BE.USUARIO;
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
    public class TipoEmpresaDA
    {
        public List<TipoEmpresa> ListarTipoEmpresa()
        {
            List<TipoEmpresa> retVal = new List<TipoEmpresa>();

            DataTable dt = new DataTable();

            string cadenaConexion = Util.CadenaConexion;

            using (SqlConnection conexion = new SqlConnection(cadenaConexion))
            {
                conexion.Open();

                using (SqlCommand cmdSQL = new SqlCommand())
                {
                    cmdSQL.Connection = conexion;
                    cmdSQL.CommandType = CommandType.StoredProcedure;

                    cmdSQL.CommandText = "USP_TipoEmpresa_LIS";


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

                TipoEmpresa TipoEmpresa = new TipoEmpresa();
                TipoEmpresa.TipoEmpresaId = ConvertHelpers.ToInteger(dr["TipoEmpresaId"].ToString());
                TipoEmpresa.Nombre = dr["Nombre"].ToString();
                TipoEmpresa.Estado = dr["Estado"].ToString();
              
                retVal.Add(TipoEmpresa);
            }
            return retVal;
        }

        public int ActualizarTipoEmpresa(TipoEmpresa TipoEmpresa)
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
                    cmdSQL.CommandText = "USP_TipoEmpresa_UPD";

                    cmdSQL.Parameters.Add("@TipoEmpresaId", SqlDbType.Int).Value = TipoEmpresa.TipoEmpresaId;
                    cmdSQL.Parameters.Add("@Nombre", SqlDbType.VarChar, -1).Value = TipoEmpresa.Nombre;
                    cmdSQL.Parameters.Add("@Estado", SqlDbType.VarChar, -1).Value = TipoEmpresa.Estado;
                  
                    lintResultado = cmdSQL.ExecuteNonQuery();
                }
                if (conexion.State == ConnectionState.Open)
                {
                    conexion.Close();
                }
            }

            return lintResultado;
        }

        public int InsertarTipoEmpresa(TipoEmpresa TipoEmpresa)
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
                    cmdSQL.CommandText = "USP_TipoEmpresa_INS";

                    cmdSQL.Parameters.Add("@Nombre", SqlDbType.VarChar, -1).Value = TipoEmpresa.Nombre;
                    cmdSQL.Parameters.Add("@Estado", SqlDbType.VarChar, -1).Value = TipoEmpresa.Estado;
               

                    lintResultado = cmdSQL.ExecuteNonQuery();
                }
                if (conexion.State == ConnectionState.Open)
                {
                    conexion.Close();
                }
            }

            return lintResultado;
        }

        public TipoEmpresa ObtenerTipoEmpresaPorTipoEmpresaId(int TipoEmpresaId)
        {
            List<TipoEmpresa> retVal = new List<TipoEmpresa>();

            DataTable dt = new DataTable();

            string cadenaConexion = Util.CadenaConexion;

            using (SqlConnection conexion = new SqlConnection(cadenaConexion))
            {
                conexion.Open();

                using (SqlCommand cmdSQL = new SqlCommand())
                {
                    cmdSQL.Connection = conexion;
                    cmdSQL.CommandType = CommandType.StoredProcedure;
                    //todo SP
                    cmdSQL.CommandText = "USP_TipoEmpresa_SEL_PorTipoEmpresaId";
                    cmdSQL.Parameters.Add("@TipoEmpresaId", SqlDbType.VarChar, -1).Value = TipoEmpresaId;

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

            TipoEmpresa TipoEmpresa = new TipoEmpresa();
            foreach (DataRow dr in dt.Rows)
            {

                TipoEmpresa.TipoEmpresaId = ConvertHelpers.ToInteger(dr["TipoEmpresaId"].ToString());
                TipoEmpresa.Nombre = dr["Nombre"].ToString();
                TipoEmpresa.Estado = dr["Estado"].ToString();
                
            }
            return TipoEmpresa;
        }


        public int ActualizarTipoEmpresaEstado(int tipoUsuarioId, string estado)
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
                    cmdSQL.CommandText = "USP_TipoEmpresa_UPD_Estado";

                    cmdSQL.Parameters.Add("@TipoEmpresaId", SqlDbType.Int).Value = tipoUsuarioId;
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



    }
}
