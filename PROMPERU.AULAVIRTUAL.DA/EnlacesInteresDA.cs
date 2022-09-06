using PROMPERU.AULAVIRTUAL.BE.CURSOS;
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
    public class EnlacesInteresDA
    {
        public List<EnlacesInteres> ListarEnlacesInteres()
        {
            List<EnlacesInteres> retVal = new List<EnlacesInteres>();

            DataTable dt = new DataTable();

            string cadenaConexion = Util.CadenaConexion;

            using (SqlConnection conexion = new SqlConnection(cadenaConexion))
            {
                conexion.Open();

                using (SqlCommand cmdSQL = new SqlCommand())
                {
                    cmdSQL.Connection = conexion;
                    cmdSQL.CommandType = CommandType.StoredProcedure;

                    cmdSQL.CommandText = "USP_EnlacesInteres_LIS";


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

                EnlacesInteres EnlacesInteres = new EnlacesInteres();
                EnlacesInteres.EnlaceInteresId = ConvertHelpers.ToInteger(dr["EnlaceInteresId"].ToString());
                EnlacesInteres.Titulo = dr["Titulo"].ToString();
                EnlacesInteres.Imagen = dr["Imagen"].ToString();

                EnlacesInteres.Descripcion = dr["Descripcion"].ToString();
                EnlacesInteres.Tipo = dr["Tipo"].ToString();
                EnlacesInteres.Url = dr["Url"].ToString();
                EnlacesInteres.Estado = dr["Estado"].ToString();

                EnlacesInteres.FechaEdicion = ConvertHelpers.ToDateTime(dr["FechaEdicion"]);
                EnlacesInteres.UsuarioEdicionId = ConvertHelpers.ToInteger(dr["UsuarioEdicionId"].ToString());
                EnlacesInteres.Pdf = dr["Pdf"].ToString();

                retVal.Add(EnlacesInteres);
            }
            return retVal;
        }

        public int ActualizarEnlacesInteres(EnlacesInteres EnlacesInteres)
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
                    cmdSQL.CommandText = "USP_EnlacesInteres_UPD";

                    cmdSQL.Parameters.Add("@EnlaceInteresId", SqlDbType.Int).Value = EnlacesInteres.EnlaceInteresId;
                    cmdSQL.Parameters.Add("@Titulo", SqlDbType.VarChar, -1).Value = EnlacesInteres.Titulo;
                    cmdSQL.Parameters.Add("@Imagen", SqlDbType.VarChar, -1).Value = EnlacesInteres.Imagen;

                    cmdSQL.Parameters.Add("@Descripcion", SqlDbType.VarChar, -1).Value = EnlacesInteres.Descripcion;
                    cmdSQL.Parameters.Add("@Tipo", SqlDbType.VarChar, -1).Value = EnlacesInteres.Tipo;
                    cmdSQL.Parameters.Add("@CodigoYoutube", SqlDbType.VarChar, -1).Value = EnlacesInteres.CodigoYoutube;
                    cmdSQL.Parameters.Add("@Url", SqlDbType.VarChar, -1).Value = EnlacesInteres.Url;

                    cmdSQL.Parameters.Add("@Estado", SqlDbType.VarChar, -1).Value = EnlacesInteres.Estado;
                    cmdSQL.Parameters.Add("@UsuarioEdicionId", SqlDbType.Int, -1).Value = EnlacesInteres.UsuarioEdicionId;
                    cmdSQL.Parameters.Add("@Pdf", SqlDbType.VarChar, -1).Value = EnlacesInteres.Pdf;


                    lintResultado = cmdSQL.ExecuteNonQuery();
                }
                if (conexion.State == ConnectionState.Open)
                {
                    conexion.Close();
                }
            }

            return lintResultado;
        }

        public int InsertarEnlacesInteres(EnlacesInteres EnlacesInteres)
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
                    cmdSQL.CommandText = "USP_EnlacesInteres_INS";

                    
                    cmdSQL.Parameters.Add("@Titulo", SqlDbType.VarChar, -1).Value = EnlacesInteres.Titulo;
                    cmdSQL.Parameters.Add("@Imagen", SqlDbType.VarChar, -1).Value = EnlacesInteres.Imagen;

                    cmdSQL.Parameters.Add("@Descripcion", SqlDbType.VarChar, -1).Value = EnlacesInteres.Descripcion;
                    cmdSQL.Parameters.Add("@Tipo", SqlDbType.VarChar, -1).Value = EnlacesInteres.Tipo;
                    cmdSQL.Parameters.Add("@CodigoYoutube", SqlDbType.VarChar, -1).Value = EnlacesInteres.CodigoYoutube;
                    cmdSQL.Parameters.Add("@Url", SqlDbType.VarChar, -1).Value = EnlacesInteres.Url;

                    cmdSQL.Parameters.Add("@Estado", SqlDbType.VarChar, -1).Value = EnlacesInteres.Estado;
                    cmdSQL.Parameters.Add("@UsuarioEdicionId", SqlDbType.Int, -1).Value = EnlacesInteres.UsuarioEdicionId;
                    cmdSQL.Parameters.Add("@Pdf", SqlDbType.VarChar, -1).Value = EnlacesInteres.Pdf;


                    lintResultado = cmdSQL.ExecuteNonQuery();
                }
                if (conexion.State == ConnectionState.Open)
                {
                    conexion.Close();
                }
            }

            return lintResultado;
        }

        public EnlacesInteres ObtenerEnlacesInteresPorEnlacesInteresId(int EnlacesInteresId)
        {
            List<EnlacesInteres> retVal = new List<EnlacesInteres>();

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
                    cmdSQL.CommandText = "USP_EnlacesInteres_SEL_PorEnlacesInteresId";
                    cmdSQL.Parameters.Add("@EnlacesInteresId", SqlDbType.VarChar, -1).Value = EnlacesInteresId;

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

            EnlacesInteres EnlacesInteres = new EnlacesInteres();
            foreach (DataRow dr in dt.Rows)
            {

                
                EnlacesInteres.EnlaceInteresId = ConvertHelpers.ToInteger(dr["EnlaceInteresId"].ToString());
                EnlacesInteres.Titulo = dr["Titulo"].ToString();
                EnlacesInteres.Imagen = dr["Imagen"].ToString();

                EnlacesInteres.Descripcion = dr["Descripcion"].ToString();
                EnlacesInteres.Tipo = dr["Tipo"].ToString();
                EnlacesInteres.Url = dr["Url"].ToString();
                EnlacesInteres.Estado = dr["Estado"].ToString();

                EnlacesInteres.FechaEdicion = ConvertHelpers.ToDateTime(dr["FechaEdicion"]);
                EnlacesInteres.UsuarioEdicionId = ConvertHelpers.ToInteger(dr["UsuarioEdicionId"].ToString());
                EnlacesInteres.Pdf = dr["Pdf"].ToString();

            }
            return EnlacesInteres;
        }


        public int ActualizarEnlacesInteresEstado(int tipoUsuarioId, string estado)
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
                    cmdSQL.CommandText = "USP_EnlacesInteres_UPD_Estado";

                    cmdSQL.Parameters.Add("@EnlaceInteresId", SqlDbType.Int).Value = tipoUsuarioId;
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
