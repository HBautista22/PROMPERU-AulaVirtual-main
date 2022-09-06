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
    public class CursoOnlineFavoritoDA
    {
        public int ActualizarFavorito(int usuarioId, int cursoOnlineId, bool estadoFavorito)
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
                    cmdSQL.CommandText = "USP_CursoOnlineFavorito_INS";

                    cmdSQL.Parameters.Add("@UsuarioId", SqlDbType.Int).Value = usuarioId;
                    cmdSQL.Parameters.Add("@CursoOnlineId", SqlDbType.Int).Value = cursoOnlineId;
                    cmdSQL.Parameters.Add("@EstadoFavorito", SqlDbType.Bit, -1).Value = estadoFavorito;
                    

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
