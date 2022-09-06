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
    public class EmpresaDA
    {
        public List<Empresa> ListarEmpresa()
        {
            List<Empresa> retVal = new List<Empresa>();

            DataTable dt = new DataTable();

            string cadenaConexion = Util.CadenaConexion;

            using (SqlConnection conexion = new SqlConnection(cadenaConexion))
            {
                conexion.Open();

                using (SqlCommand cmdSQL = new SqlCommand())
                {
                    cmdSQL.Connection = conexion;
                    cmdSQL.CommandType = CommandType.StoredProcedure;

                    cmdSQL.CommandText = "USP_Empresa_LIS";


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

                Empresa empresa = new Empresa();
                empresa.EmpresaId = ConvertHelpers.ToInteger(dr["EmpresaId"].ToString());
                empresa.RUC = dr["RUC"].ToString();
                empresa.RazonSocial = dr["RazonSocial"].ToString();
                empresa.AutoMatriculaHabilitada = ConvertHelpers.ToBoolean(dr["AutoMatriculaHabilitada"].ToString());
                empresa.Email = dr["Email"].ToString();
                empresa.CIIU = dr["CIIU"].ToString();
                empresa.Productos = dr["Productos"].ToString();
                retVal.Add(empresa);
            }
            return retVal;
        }

        public int ActualizarEmpresa(Empresa empresa)
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
                    cmdSQL.CommandText = "USP_Empresa_UPD";

                    cmdSQL.Parameters.Add("@EmpresaId", SqlDbType.Int).Value = empresa.EmpresaId;
                    cmdSQL.Parameters.Add("@RUC", SqlDbType.VarChar, -1).Value = empresa.RUC;
                    cmdSQL.Parameters.Add("@RazonSocial", SqlDbType.VarChar, -1).Value = empresa.RazonSocial;
                    cmdSQL.Parameters.Add("@AutoMatriculaHabilitada", SqlDbType.Int).Value = empresa.AutoMatriculaHabilitada;
                    cmdSQL.Parameters.Add("@Email", SqlDbType.VarChar, -1).Value = empresa.Email;
                    cmdSQL.Parameters.Add("@CIIU", SqlDbType.VarChar, -1).Value = empresa.CIIU;
                    cmdSQL.Parameters.Add("@Productos", SqlDbType.VarChar, -1).Value = empresa.Productos;

                    lintResultado = cmdSQL.ExecuteNonQuery();
                }
                if (conexion.State == ConnectionState.Open)
                {
                    conexion.Close();
                }
            }

            return lintResultado;
        }

        public int InsertarEmpresa(Empresa empresa)
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
                    cmdSQL.CommandText = "USP_Empresa_INS";

                    cmdSQL.Parameters.Add("@RUC", SqlDbType.VarChar, -1).Value = empresa.RUC;
                    cmdSQL.Parameters.Add("@RazonSocial", SqlDbType.VarChar, -1).Value = empresa.RazonSocial;
                    cmdSQL.Parameters.Add("@AutoMatriculaHabilitada", SqlDbType.Int).Value = empresa.AutoMatriculaHabilitada;
                    cmdSQL.Parameters.Add("@Email", SqlDbType.VarChar, -1).Value = empresa.Email;
                    cmdSQL.Parameters.Add("@CIIU", SqlDbType.VarChar, -1).Value = empresa.CIIU;
                    cmdSQL.Parameters.Add("@Productos", SqlDbType.VarChar, -1).Value = empresa.Productos;

                    lintResultado = cmdSQL.ExecuteNonQuery();
                }
                if (conexion.State == ConnectionState.Open)
                {
                    conexion.Close();
                }
            }

            return lintResultado;
        }

        public List<Empresa> ObtenerEmpresaPorEmpresaId(int empresaId)
        {
            List<Empresa> retVal = new List<Empresa>();

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
                    cmdSQL.CommandText = "USP_Empresa_SEL_PorEmpresaId";
                    cmdSQL.Parameters.Add("@EmpresaId", SqlDbType.VarChar, -1).Value = empresaId;

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

                Empresa empresa = new Empresa();
                empresa.EmpresaId = ConvertHelpers.ToInteger(dr["EmpresaId"].ToString());
                empresa.RUC = dr["RUC"].ToString();
                empresa.RazonSocial = dr["RazonSocial"].ToString();
                empresa.AutoMatriculaHabilitada = ConvertHelpers.ToBoolean(dr["AutoMatriculaHabilitada"].ToString());
                empresa.Email = dr["Email"].ToString();
                empresa.CIIU = dr["CIIU"].ToString();
                empresa.Productos = dr["Productos"].ToString();
                retVal.Add(empresa);
            }
            return retVal;
        }

        public Empresa ObtenerEmpresaPorRUC(string RUC)
        {

            Empresa retVal = new Empresa();

            DataTable dt = new DataTable();

            string cadenaConexion = Util.CadenaConexion;

            using (SqlConnection conexion = new SqlConnection(cadenaConexion))
            {
                conexion.Open();

                using (SqlCommand cmdSQL = new SqlCommand())
                {
                    cmdSQL.Connection = conexion;
                    cmdSQL.CommandType = CommandType.StoredProcedure;
                    cmdSQL.CommandText = "USP_Empresa_LIS_PorRUC";

                    cmdSQL.Parameters.Add("@RUC", SqlDbType.VarChar, -1).Value = RUC;

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
                Empresa obj = new Empresa();
                obj.EmpresaId = (int)(dr["EmpresaId"]);
                obj.RUC = (string)(dr["RUC"]);
                obj.RazonSocial = (string)(dr["RazonSocial"]);
                obj.AutoMatriculaHabilitada = (bool)(dr["AutoMatriculaHabilitada"]);
                obj.Email = (string)(dr["Email"]);
                obj.CIIU = (string)(dr["CIIU"]);
                obj.Productos = (string)(dr["Productos"]);

                retVal = obj;
            }


            return retVal;
        }


    }
}
