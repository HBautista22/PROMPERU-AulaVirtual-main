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
    public class UsuarioMultiEmpresaDA
    {
        public List<UsuarioMultiEmpresa> ListarUsuarioMultiEmpresa()
        {
            List<UsuarioMultiEmpresa> retVal = new List<UsuarioMultiEmpresa>();

            DataTable dt = new DataTable();

            string cadenaConexion = Util.CadenaConexion;

            using (SqlConnection conexion = new SqlConnection(cadenaConexion))
            {
                conexion.Open();

                using (SqlCommand cmdSQL = new SqlCommand())
                {
                    cmdSQL.Connection = conexion;
                    cmdSQL.CommandType = CommandType.StoredProcedure;

                    cmdSQL.CommandText = "USP_UsuarioMultiEmpresa_LIS";


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

                UsuarioMultiEmpresa empresa = new UsuarioMultiEmpresa();
                empresa.UsuarioMultiEmpresaId = ConvertHelpers.ToInteger(dr["UsuarioMultiEmpresaId"].ToString());
                empresa.UsuarioId = ConvertHelpers.ToInteger(dr["UsuarioId"].ToString());
                empresa.EmpresaId = ConvertHelpers.ToInteger(dr["EmpresaId"].ToString());
                retVal.Add(empresa);
            }
            return retVal;
        }

    }
}
