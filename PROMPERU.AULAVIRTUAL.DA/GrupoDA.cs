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
    public class GrupoDA
    {
        public List<Grupo> ListarGrupo()
        {
            List<Grupo> retVal = new List<Grupo>();

            DataTable dt = new DataTable();

            string cadenaConexion = Util.CadenaConexion;

            using (SqlConnection conexion = new SqlConnection(cadenaConexion))
            {
                conexion.Open();

                using (SqlCommand cmdSQL = new SqlCommand())
                {
                    cmdSQL.Connection = conexion;
                    cmdSQL.CommandType = CommandType.StoredProcedure;
                    cmdSQL.CommandText = "USP_Grupo_LIS";


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
                Grupo obj = new Grupo();
                obj.GrupoId = (int)(dr["GrupoId"]);
                obj.Descripcion = (string)(dr["Descripcion"]);
                obj.Estado = (string)(dr["Estado"]);
                obj.FechaCreacion = (dr["FechaCreacion"].ToString() != String.Empty ? (DateTime?)(dr["FechaCreacion"]) : null);
                obj.Aforo = (dr["Aforo"].ToString() != String.Empty ? (int?)(dr["Aforo"]) : null);
                obj.TipoRegistro = (string)(dr["TipoRegistro"]);
                retVal.Add(obj);
            }
            return retVal;
        }

        public void InsertarGrupo(Grupo grupo)
        {
            string cadenaConexion = Util.CadenaConexion;
            using (SqlConnection conexion = new SqlConnection(cadenaConexion))
            {
                conexion.Open();

                using (SqlCommand cmdSQL = new SqlCommand())
                {
                    cmdSQL.Connection = conexion;
                    cmdSQL.CommandType = CommandType.StoredProcedure;
                    cmdSQL.CommandText = "USP_Grupo_INS";

                    cmdSQL.Parameters.Add("@Descripcion", SqlDbType.VarChar).Value = grupo.Descripcion;
                    cmdSQL.Parameters.Add("@Estado", SqlDbType.VarChar, -1).Value = grupo.Estado;
                    cmdSQL.Parameters.Add("@Aforo", SqlDbType.Int, -1).Value = grupo.Aforo;
                    cmdSQL.Parameters.Add("@TipoRegistro", SqlDbType.VarChar).Value = grupo.TipoRegistro;
                    
                     cmdSQL.ExecuteNonQuery();
                }
                if (conexion.State == ConnectionState.Open)
                {
                    conexion.Close();
                }
            }
        }

        public void ActualizarGrupo(Grupo grupo)
        {
            string cadenaConexion = Util.CadenaConexion;
            using (SqlConnection conexion = new SqlConnection(cadenaConexion))
            {
                conexion.Open();

                using (SqlCommand cmdSQL = new SqlCommand())
                {
                    cmdSQL.Connection = conexion;
                    cmdSQL.CommandType = CommandType.StoredProcedure;
                    cmdSQL.CommandText = "USP_Grupo_UPD";
                    cmdSQL.Parameters.Add("@GrupoId", SqlDbType.Int).Value = grupo.GrupoId;
                    cmdSQL.Parameters.Add("@Descripcion", SqlDbType.VarChar).Value = grupo.Descripcion;
                    cmdSQL.Parameters.Add("@Estado", SqlDbType.VarChar, -1).Value = grupo.Estado;
                    cmdSQL.Parameters.Add("@Aforo", SqlDbType.Int, -1).Value = grupo.Aforo;
                    cmdSQL.Parameters.Add("@TipoRegistro", SqlDbType.VarChar).Value = grupo.TipoRegistro;

                    cmdSQL.ExecuteNonQuery();
                }
                if (conexion.State == ConnectionState.Open)
                {
                    conexion.Close();
                }
            }
        }

        public Grupo ObtenerGrupoPorId(int? grupoId)
        {
            Grupo retVal = new Grupo();

            DataTable dt = new DataTable();

            string cadenaConexion = Util.CadenaConexion;

            using (SqlConnection conexion = new SqlConnection(cadenaConexion))
            {
                conexion.Open();

                using (SqlCommand cmdSQL = new SqlCommand())
                {
                    cmdSQL.Connection = conexion;
                    cmdSQL.CommandType = CommandType.StoredProcedure;
                    cmdSQL.CommandText = "USP_Grupo_SEL_PorId";

                    cmdSQL.Parameters.Add("@GrupoId", SqlDbType.Int).Value = grupoId;

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
                retVal.GrupoId = (int)(dr["GrupoId"]);
                retVal.Descripcion = (string)(dr["Descripcion"]);
                retVal.Estado = (string)(dr["Estado"]);
                retVal.FechaCreacion = (dr["FechaCreacion"].ToString() != String.Empty ? (DateTime?)(dr["FechaCreacion"]) : null);
                retVal.Aforo = (dr["Aforo"].ToString() != String.Empty ? (int?)(dr["Aforo"]) : null);
                retVal.TipoRegistro = (string)(dr["TipoRegistro"]);
            }
            return retVal;

        }

        public List<Grupo> ListarAddGrupoLISPorUsuarioId(int usuarioId)
        {
            List<Grupo> retVal = new List<Grupo>();

            DataTable dt = new DataTable();

            string cadenaConexion = Util.CadenaConexion;

            using (SqlConnection conexion = new SqlConnection(cadenaConexion))
            {
                conexion.Open();

                using (SqlCommand cmdSQL = new SqlCommand())
                {
                    cmdSQL.Connection = conexion;
                    cmdSQL.CommandType = CommandType.StoredProcedure;
                    cmdSQL.CommandText = "USP_AddGrupo_LIS";

                    cmdSQL.Parameters.Add("@UsuarioId", SqlDbType.Int).Value = usuarioId;

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
                Grupo obj = new Grupo();
                obj.GrupoId = (int)(dr["GrupoId"]);
                obj.Descripcion = (string)(dr["Descripcion"]);
                obj.Estado = (string)(dr["Estado"]);
                obj.FechaCreacion = (dr["FechaCreacion"].ToString() != String.Empty ? (DateTime?)(dr["FechaCreacion"]) : null);
                obj.Aforo = (dr["Aforo"].ToString() != String.Empty ? (int?)(dr["Aforo"]) : null);
                obj.TipoRegistro = (string)(dr["TipoRegistro"]);
                retVal.Add(obj);
            }
            return retVal;

        }

        public List<Grupo> ListarAddGrupoSELPorUsuarioId(int usuarioId)
        {
            List<Grupo> retVal = new List<Grupo>();

            DataTable dt = new DataTable();

            string cadenaConexion = Util.CadenaConexion;

            using (SqlConnection conexion = new SqlConnection(cadenaConexion))
            {
                conexion.Open();

                using (SqlCommand cmdSQL = new SqlCommand())
                {
                    cmdSQL.Connection = conexion;
                    cmdSQL.CommandType = CommandType.StoredProcedure;
                    cmdSQL.CommandText = "USP_AddGrupo_SEL";
                    cmdSQL.Parameters.Add("@UsuarioId", SqlDbType.Int).Value = usuarioId;

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
                Grupo obj = new Grupo();
                obj.GrupoId = (int)(dr["GrupoId"]);
                obj.Descripcion = (string)(dr["Descripcion"]);
                obj.Estado = (string)(dr["Estado"]);
                obj.FechaCreacion = (dr["FechaCreacion"].ToString() != String.Empty ? (DateTime?)(dr["FechaCreacion"]) : null);
                obj.Aforo = (dr["Aforo"].ToString() != String.Empty ? (int?)(dr["Aforo"]) : null);
                obj.TipoRegistro = (string)(dr["TipoRegistro"]);
                retVal.Add(obj);
            }
            return retVal;

        }



    }
}
