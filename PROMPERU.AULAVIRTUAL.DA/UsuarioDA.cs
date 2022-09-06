using System;
using System.Collections.Generic;
using System.Data;
using System.Data.SqlClient;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using PROMPERU.AULAVIRTUAL.BE.USUARIO;
using PROMPERU.AULAVIRTUAL.BE.CURSOS;
using PROMPERU.AULAVIRTUAL.Helpers;
//using PROMPERU.AULAVIRTUAL.WEB.Helpers;


namespace PROMPERU.AULAVIRTUAL.DA
{
    public class UsuarioDA
    {
        /*  public List<Usuario> ReadDates(Usuario filter)
          {
              try
              {
                  var cs = Util.CadenaConexion;

                  string connetionString = null;
                  connetionString = cs;
                  var cnn = new SqlConnection(connetionString);


                  var command = new SqlCommand("usp_read_date", cnn);
                  command.Parameters.AddWithValue("@usuario", filter.Email);
                  command.Parameters.AddWithValue("@password", Util.Decifrar("MyProgram", filter.Password));

                  command.CommandType = System.Data.CommandType.StoredProcedure;

                  cnn.Open();

                  List<Usuario> result = null;
                  var reader = command.ExecuteReader();
                  if (reader.HasRows)
                  {
                      result = new List<Usuario>();
                      while (reader.Read())
                      {
                          var r = new Usuario
                          {
                              Token = reader["token"].ToString(),
                              Sala = reader["sala"].ToString(),
                              NombreEmpresaHost = reader["nombre_empresa_host"].ToString(),
                              AcompananteHost = reader["acompanante_host"].ToString(),
                              NombreEmpresaGuest = reader["nombre_empresa_guest"].ToString(),
                              AcompananteGuest = reader["acompanante_guest"].ToString(),
                              FechaInicio = DateTime.Parse(reader["fecha_inicio"].ToString()),
                              FechaFin = DateTime.Parse(reader["fecha_fin"].ToString()),
                              FechaServidor = DateTime.Parse(reader["fecha_servidor"].ToString()),
                              Tipo = reader["tipo_usuario"].ToString(),
                              IdPrincipal = reader["id_principal"].ToString(),
                              Rango = (int)reader["id_rango"]
                          };
                          result.Add(r);
                      }
                  }

                  cnn.Close();
                  return result;
              }
              catch (Exception e)
              {
                  Util.SaveErrorLog(e);
                  return null;
              }
          }
          */

        public int ConsultarCuentaPorEmailRol(string email, string rol)
        {
            int retVal = 0;
            
            DataTable dt = new DataTable();

            string cnxconSQL = Util.CadenaConexion;

            using (SqlConnection conSQL = new SqlConnection(cnxconSQL))
            {
                conSQL.Open();

                using (SqlCommand cmdSQL = new SqlCommand())
                {
                    cmdSQL.Connection = conSQL;
                    cmdSQL.CommandType = CommandType.StoredProcedure;
                    cmdSQL.CommandText = "USP_ConsultarCuentaPorEmailRol_SEL";

                    cmdSQL.Parameters.Add("@Email", SqlDbType.VarChar, -1).Value = email;
                    cmdSQL.Parameters.Add("@Rol", SqlDbType.VarChar, -1).Value = rol;

                    retVal = (int)cmdSQL.ExecuteScalar();
                    
                }
                if (conSQL.State == ConnectionState.Open)
                {
                    conSQL.Close();
                }
            }

            

            return retVal;
        }

        public List<Usuario> ListarUsuarioConCertificado()
        {
            DataTable dt = new DataTable();
            string cadenaConexion = Util.CadenaConexion;
            using (SqlConnection conexion = new SqlConnection(cadenaConexion))
            {
                conexion.Open();
                using (SqlCommand cmdSql = new SqlCommand())
                {
                    cmdSql.Connection = conexion;
                    cmdSql.CommandType = CommandType.StoredProcedure;
                    cmdSql.CommandText = "USP_Usuario_LIS_ConCertificado";
                    using (SqlDataAdapter da = new SqlDataAdapter(cmdSql))
                    {
                        da.Fill(dt);
                    }
                }
                if (conexion.State == ConnectionState.Open)
                {
                    conexion.Close();
                }
            }

            return (from DataRow dr in dt.Rows select asignarValoresUsuario(dr)).ToList();
        }

        public List<Usuario> ListarAlumnosActivos()
        {
            List<Usuario> retVal = new List<Usuario>();
            DataTable dt = new DataTable();
            string cadenaConexion = Util.CadenaConexion;
            using (SqlConnection conexion = new SqlConnection(cadenaConexion))
            {
                conexion.Open();
                using (SqlCommand cmdSQL = new SqlCommand())
                {
                    cmdSQL.Connection = conexion;
                    cmdSQL.CommandType = CommandType.StoredProcedure;
                    cmdSQL.CommandText = "USP_Usuario_LIS_AlumnosActivos";
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
                retVal.Add(asignarValoresUsuario(dr));
            }
            return retVal;
        }

        public List<Usuario> ListarProfesoresActivos()
        {
            List<Usuario> retVal = new List<Usuario>();
            DataTable dt = new DataTable();
            string cadenaConexion = Util.CadenaConexion;
            using (SqlConnection conexion = new SqlConnection(cadenaConexion))
            {
                conexion.Open();
                using (SqlCommand cmdSQL = new SqlCommand())
                {
                    cmdSQL.Connection = conexion;
                    cmdSQL.CommandType = CommandType.StoredProcedure;
                    cmdSQL.CommandText = "USP_Usuario_LIS_ProfesoresActivos";
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
                retVal.Add(asignarValoresUsuario(dr));
            }
            return retVal;
        }


        public Usuario ObtenerUsuarioPorEmail(string Email)
        {
            Usuario retVal = new Usuario();

            DataTable dt = new DataTable();

            string cadenaConexion = Util.CadenaConexion;

            using (SqlConnection conexion = new SqlConnection(cadenaConexion))
            {
                conexion.Open();

                using (SqlCommand cmdSQL = new SqlCommand())
                {
                    cmdSQL.Connection = conexion;
                    cmdSQL.CommandType = CommandType.StoredProcedure;
                    cmdSQL.CommandText = "USP_Usuario_SEL_PorEmail";
                    cmdSQL.Parameters.Add("@Email", SqlDbType.VarChar, -1).Value = Email;

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
                retVal = asignarValoresUsuario(dr);
            }

            return retVal;
        
        }

        public Usuario ObtenerUsuarioPorId(int usuarioId)
        {
            Usuario retVal = new Usuario();

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
                    cmdSQL.CommandText = "USP_Usuario_SEL_PorUsuarioId";
                    cmdSQL.Parameters.Add("@UsuarioId", SqlDbType.VarChar, -1).Value = usuarioId;

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
                retVal = asignarValoresUsuario(dr);
            }

            return retVal;

        }

        public int InsertarCambioContrasena(CambioContrasena cambio)
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
                    cmdSQL.CommandText = "USP_CambioContrasena_INS";

                    cmdSQL.Parameters.Add("@Fecha", SqlDbType.DateTime).Value = cambio.Fecha;
                    cmdSQL.Parameters.Add("@Token", SqlDbType.VarChar, -1).Value = cambio.Token;
                    cmdSQL.Parameters.Add("@UsuarioId", SqlDbType.Int).Value = cambio.UsuarioId;
                    cmdSQL.Parameters.Add("@FechaCambio", SqlDbType.DateTime).Value = cambio.FechaCambio;

                    lintResultado = cmdSQL.ExecuteNonQuery();
                }
                if (conexion.State == ConnectionState.Open)
                {
                    conexion.Close();
                }
            }

            return lintResultado;
        }

        public int GrabarLogAccesoUsuario(LogAccesosUsuario log)
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
                    cmdSQL.CommandText = "USP_LogAccesosUsuario_INS";

                      cmdSQL.Parameters.Add("@FechaAcceso", SqlDbType.DateTime).Value = log.FechaAcceso;
                    cmdSQL.Parameters.Add("@DatosNavegador", SqlDbType.VarChar, -1).Value = log.DatosNavegador;
                    cmdSQL.Parameters.Add("@UserAgent", SqlDbType.VarChar, -1).Value = log.UserAgent;
                    cmdSQL.Parameters.Add("@UsuarioId", SqlDbType.Int).Value = log.UsuarioId;
                    cmdSQL.Parameters.Add("@IPAcceso", SqlDbType.VarChar, -1).Value = log.IPAcceso;
                    cmdSQL.Parameters.Add("@TipoAcceso", SqlDbType.VarChar, -1).Value = log.TipoAcceso;

                    lintResultado = cmdSQL.ExecuteNonQuery();
                }
                if (conexion.State == ConnectionState.Open)
                {
                    conexion.Close();
                }
            }

            return lintResultado;
        }

        public CambioContrasena ObtenerCambioContrasena(string usermailtoken)
        {
            CambioContrasena retVal = new CambioContrasena();

            DataTable dt = new DataTable();

            string cadenaConexion = Util.CadenaConexion;

            using (SqlConnection conexion = new SqlConnection(cadenaConexion))
            {
                conexion.Open();

                using (SqlCommand cmdSQL = new SqlCommand())
                {
                    cmdSQL.Connection = conexion;
                    cmdSQL.CommandType = CommandType.StoredProcedure;

                     cmdSQL.CommandText = "USP_CambioContrasena_SEL_PorToken";
                    cmdSQL.Parameters.Add("@Token", SqlDbType.VarChar, -1).Value = usermailtoken;

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

                retVal.CambioContrasenaId = (int)(dr["CambioContrasenaId"]);
                retVal.Fecha = (DateTime)(dr["Fecha"]);
                retVal.UsuarioId = (int)(dr["UsuarioId"]);
                retVal.Token = (string)(dr["Token"]);

            }

            return retVal;
        }

        public int ActualizarUsuarioEstado(int usuarioId, string estado)
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
                    cmdSQL.CommandText = "USP_Usuario_UPD_Estado";

                    cmdSQL.Parameters.Add("@UsuarioId", SqlDbType.Int).Value = usuarioId;
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

        public int ActualizarUsuarioCargo(Usuario usuario)
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
                    cmdSQL.CommandText = "USP_Usuario_UPD_Cargo";

                    cmdSQL.Parameters.Add("@UsuarioId", SqlDbType.Int).Value = usuario.UsuarioId;
                    cmdSQL.Parameters.Add("@Cargo", SqlDbType.VarChar, -1).Value = usuario.Cargo;

                    lintResultado = cmdSQL.ExecuteNonQuery();
                }
                if (conexion.State == ConnectionState.Open)
                {
                    conexion.Close();
                }
            }

            return lintResultado;
        }



        public int ActualizarCambioContrasena(int cambioId, DateTime? fechaCambio)
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
                    cmdSQL.CommandText = "USP_CambioContrasena_UPD_FechaCambio";

                    cmdSQL.Parameters.Add("@CambioContrasenaId", SqlDbType.Int).Value = cambioId;
                    cmdSQL.Parameters.Add("@FechaCambio", SqlDbType.DateTime).Value = fechaCambio;

                    lintResultado = cmdSQL.ExecuteNonQuery();
                }
                if (conexion.State == ConnectionState.Open)
                {
                    conexion.Close();
                }
            }

            return lintResultado;
        }

        public int ActualizarContraseña(int usuarioId, string password)
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
                    cmdSQL.CommandText = "USP_Usuario_UPD_Password";

                    cmdSQL.Parameters.Add("@UsuarioId", SqlDbType.Int).Value = usuarioId;
                    cmdSQL.Parameters.Add("@Password", SqlDbType.VarChar, -1).Value = password;

                    lintResultado = cmdSQL.ExecuteNonQuery();
                }
                if (conexion.State == ConnectionState.Open)
                {
                    conexion.Close();
                }
            }

            return lintResultado;
        }

        public CambioContrasena ObtenerCambioContrasena(int cambioId)
        {
            CambioContrasena retVal = new CambioContrasena();

            DataTable dt = new DataTable();

            string cadenaConexion = Util.CadenaConexion;

            using (SqlConnection conexion = new SqlConnection(cadenaConexion))
            {
                conexion.Open();

                using (SqlCommand cmdSQL = new SqlCommand())
                {
                    cmdSQL.Connection = conexion;
                    cmdSQL.CommandType = CommandType.StoredProcedure;
                    
                    cmdSQL.CommandText = "USP_CambioContrasena_SEL_PorId";
                    cmdSQL.Parameters.Add("@CambioContrasenaId", SqlDbType.VarChar, -1).Value = cambioId;

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
                
                retVal.CambioContrasenaId = (int)(dr["CambioContrasenaId"]);
                retVal.Fecha = (DateTime)(dr["Fecha"]);
                retVal.UsuarioId = (int)(dr["UsuarioId"]);
                retVal.Token = (string)(dr["Token"]);
                
            }

            return retVal;
        }

        public UsuarioGrupo ObtenerUsuarioGrupoPorUsuarioId(int usuarioId)
        {
            UsuarioGrupo retVal = new UsuarioGrupo();

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
                    cmdSQL.CommandText = "USP_UsuarioGrupo_SEL_PorUsuarioId";
                    cmdSQL.Parameters.Add("@UsuarioId", SqlDbType.VarChar, -1).Value = usuarioId;

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
                UsuarioGrupo obj = new UsuarioGrupo();
                obj.UsuarioGrupoId = (int)(dr["UsuarioGrupoId"]);
                obj.GrupoId = (int)(dr["GrupoId"]);
                obj.UsuarioId = (int)(dr["UsuarioId"]);
                obj.Estado = (string)(dr["Estado"]);
                obj.Nombre = (string)(dr["Nombre"]);
                retVal = obj;
            }

            return retVal;

        }

        public int InsertarUsuarioGrupo(UsuarioGrupo usuarioLista)
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
                    cmdSQL.CommandText = "USP_UsuarioGrupo_INS";

                    cmdSQL.Parameters.Add("@GrupoId", SqlDbType.Int).Value = usuarioLista.GrupoId;
                    cmdSQL.Parameters.Add("@UsuarioId", SqlDbType.Int).Value = usuarioLista.UsuarioId;
                    cmdSQL.Parameters.Add("@Estado", SqlDbType.VarChar,-1).Value = usuarioLista.Estado;
                    cmdSQL.Parameters.Add("@Nombre", SqlDbType.VarChar, -1).Value = usuarioLista.Nombre;

                    lintResultado = cmdSQL.ExecuteNonQuery();
                }
                if (conexion.State == ConnectionState.Open)
                {
                    conexion.Close();
                }
            }

            return lintResultado;
        }

        public List<Usuario> ObtenerUsuariosPorEmpresa(int empresaId)
        {
            List<Usuario> retVal = new List<Usuario>();

            DataTable dt = new DataTable();

            string cadenaConexion = Util.CadenaConexion;

            using (SqlConnection conexion = new SqlConnection(cadenaConexion))
            {
                conexion.Open();

                using (SqlCommand cmdSQL = new SqlCommand())
                {
                    cmdSQL.Connection = conexion;
                    cmdSQL.CommandType = CommandType.StoredProcedure;
                    cmdSQL.CommandText = "USP_Usuario_LIS_PorEmpresaId";

                    cmdSQL.Parameters.Add("@EmpresaId", SqlDbType.Int).Value = empresaId;

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
                retVal.Add(asignarValoresUsuario(dr));
            }

            return retVal;
        }

        public List<LogAccesosUsuario> ObtenerLogAccesosUsuario(int usuarioId)
        {

            List<LogAccesosUsuario> retVal = new List<LogAccesosUsuario>();

            DataTable dt = new DataTable();

            string cadenaConexion = Util.CadenaConexion;

            using (SqlConnection conexion = new SqlConnection(cadenaConexion))
            {
                conexion.Open();

                using (SqlCommand cmdSQL = new SqlCommand())
                {
                    cmdSQL.Connection = conexion;
                    cmdSQL.CommandType = CommandType.StoredProcedure;
                    cmdSQL.CommandText = "USP_LogAccesosUsuario_SEL";
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
                LogAccesosUsuario obj = new LogAccesosUsuario();
                obj.LogAccesoUsuarioId = (int)(dr["LogAccesoUsuarioId"]);
                obj.FechaAcceso = (DateTime)(dr["FechaAcceso"]);
                obj.DatosNavegador = (string)(dr["DatosNavegador"]);
                obj.UserAgent = (string)(dr["UserAgent"]);
                obj.TipoAcceso = (string)(dr["TipoAcceso"]);
                obj.UsuarioId = (int)(dr["UsuarioId"]);
                obj.IPAcceso = (string)(dr["IPAcceso"]);

                retVal.Add(obj);
            }

            return retVal;


        }

        public int TotalAlumnosActivos()
        {
            int lintResult = 0;
            DataTable dt = new DataTable();

            string cadenaConexion = Util.CadenaConexion;
            using (SqlConnection conexion = new SqlConnection(cadenaConexion))
            {
                conexion.Open();
                using (SqlCommand cmdSQL = new SqlCommand())
                {
                    cmdSQL.Connection = conexion;
                    cmdSQL.CommandType = CommandType.StoredProcedure;
                    cmdSQL.CommandText = "USP_Usuario_SEL_TotalAlumnosActivos";
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
                lintResult = ConvertHelpers.ToInteger(dr["Total"].ToString());
            }

            return lintResult;
            
        }

        public int TotalCursosActivos()
        {
            int lintResult = 0;
            DataTable dt = new DataTable();

            string cadenaConexion = Util.CadenaConexion;
            using (SqlConnection conexion = new SqlConnection(cadenaConexion))
            {
                conexion.Open();
                using (SqlCommand cmdSQL = new SqlCommand())
                {
                    cmdSQL.Connection = conexion;
                    cmdSQL.CommandType = CommandType.StoredProcedure;
                    cmdSQL.CommandText = "USP_CursoOnline_SEL_TotalCursosActivos";
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
                lintResult = ConvertHelpers.ToInteger(dr["Total"].ToString());
            }

            return lintResult;



        }

        public int TotalCertificados()
        {
            int lintResult = 0;
            DataTable dt = new DataTable();

            string cadenaConexion = Util.CadenaConexion;
            using (SqlConnection conexion = new SqlConnection(cadenaConexion))
            {
                conexion.Open();
                using (SqlCommand cmdSQL = new SqlCommand())
                {
                    cmdSQL.Connection = conexion;
                    cmdSQL.CommandType = CommandType.StoredProcedure;
                    cmdSQL.CommandText = "USP_ViewCertificadoAlumno_SEL_TotalCertificados";
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
                lintResult = ConvertHelpers.ToInteger(dr["Total"].ToString());
            }

            return lintResult;



        }

        public int TotalUsuariosActivosPorGrupo(int grupoId)
        {
            int lintResult = 0;
            DataTable dt = new DataTable();

            string cadenaConexion = Util.CadenaConexion;
            using (SqlConnection conexion = new SqlConnection(cadenaConexion))
            {
                conexion.Open();
                using (SqlCommand cmdSQL = new SqlCommand())
                {
                    cmdSQL.Connection = conexion;
                    cmdSQL.CommandType = CommandType.StoredProcedure;
                    cmdSQL.CommandText = "USP_UsuarioGrupo_SEL_TotalUsuarioActivoPorGrupo";
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
                lintResult = (int)(dr["Total"]);
            }

            return lintResult;

        }

        public int TotalUsuariosPorEmpresa(int empresaId)
        {
            int lintResult = 0;
            DataTable dt = new DataTable();

            string cadenaConexion = Util.CadenaConexion;
            using (SqlConnection conexion = new SqlConnection(cadenaConexion))
            {
                conexion.Open();
                using (SqlCommand cmdSQL = new SqlCommand())
                {
                    cmdSQL.Connection = conexion;
                    cmdSQL.CommandType = CommandType.StoredProcedure;
                    cmdSQL.CommandText = "USP_Usuario_SEL_TotalUsuarioPorEmpresa";
                    cmdSQL.Parameters.Add("@EmpresaId", SqlDbType.Int).Value = empresaId;
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
                lintResult = (int)(dr["Total"]);
            }

            return lintResult;

        }


        public int InsertarUsuarioUltimoAcceso(Usuario usuario)
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
                    cmdSQL.CommandText = "USP_Usuario_UPD_UltimoAcceso";
                    cmdSQL.Parameters.Add("@UltimoAcceso", SqlDbType.DateTime).Value = usuario.UltimoAcceso;
                    cmdSQL.Parameters.Add("@UsuarioId", SqlDbType.Int,-1).Value = usuario.UsuarioId;

                    lintResultado = cmdSQL.ExecuteNonQuery();
                }
                if (conexion.State == ConnectionState.Open)
                {
                    conexion.Close();
                }
            }

            return lintResultado;
        }

        public int InsertarUsuario(Usuario usuario)
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
                    cmdSQL.CommandText = "USP_Usuario_INS";

                    cmdSQL.Parameters.Add("@Codigo", SqlDbType.VarChar, -1).Value = usuario.Codigo;
                    cmdSQL.Parameters.Add("@Nombre", SqlDbType.VarChar, -1).Value = usuario.Nombre;
                    cmdSQL.Parameters.Add("@Apellido", SqlDbType.VarChar, -1).Value = usuario.Apellido;
                    cmdSQL.Parameters.Add("@Grupo", SqlDbType.VarChar, -1).Value = usuario.Grupo;
                    cmdSQL.Parameters.Add("@Cargo", SqlDbType.VarChar, -1).Value = usuario.Cargo;
                    cmdSQL.Parameters.Add("@Rol", SqlDbType.VarChar, -1).Value = usuario.Rol;
                    cmdSQL.Parameters.Add("@Email", SqlDbType.VarChar, -1).Value = usuario.Email;
                    cmdSQL.Parameters.Add("@Password", SqlDbType.VarChar, -1).Value = usuario.Password;
                    cmdSQL.Parameters.Add("@EmpresaId", SqlDbType.Int).Value = usuario.EmpresaId;
                    cmdSQL.Parameters.Add("@Estado", SqlDbType.VarChar, -1).Value = usuario.Estado;
                    cmdSQL.Parameters.Add("@DNI", SqlDbType.VarChar, -1).Value = usuario.DNI;
                    cmdSQL.Parameters.Add("@Conversion", SqlDbType.Int).Value = usuario.Conversion;
                    cmdSQL.Parameters.Add("@DistritoId", SqlDbType.Int).Value = usuario.DistritoId;
                    cmdSQL.Parameters.Add("@NacionalidadId", SqlDbType.Int).Value = usuario.NacionalidadId;
                    cmdSQL.Parameters.Add("@SexoId", SqlDbType.Int).Value = usuario.SexoId;
                    cmdSQL.Parameters.Add("@UltimoAcceso", SqlDbType.DateTime).Value = usuario.UltimoAcceso;
                    cmdSQL.Parameters.Add("@TipoDocumento", SqlDbType.Int).Value = usuario.TipoDocumento;
                    cmdSQL.Parameters.Add("@Telefono", SqlDbType.VarChar, -1).Value = usuario.Telefono;
                    cmdSQL.Parameters.Add("@Sector", SqlDbType.VarChar, -1).Value = usuario.Sector;
                    cmdSQL.Parameters.Add("@Avatar", SqlDbType.VarChar, -1).Value = usuario.Avatar;
                    cmdSQL.Parameters.Add("@Descripcion", SqlDbType.VarChar, -1).Value = usuario.Descripcion;

                    lintResultado = cmdSQL.ExecuteNonQuery();
                }
                if (conexion.State == ConnectionState.Open)
                {
                    conexion.Close();
                }
            }

            return lintResultado;

        }

        public int ActualizarUsuario(Usuario usuario)
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
                    cmdSQL.CommandText = "USP_Usuario_UPD";

                    cmdSQL.Parameters.Add("@UsuarioId", SqlDbType.Int).Value = usuario.UsuarioId;
                    cmdSQL.Parameters.Add("@Nombre", SqlDbType.VarChar, -1).Value = usuario.Nombre;
                    cmdSQL.Parameters.Add("@Apellido", SqlDbType.VarChar, -1).Value = usuario.Apellido;
                    cmdSQL.Parameters.Add("@Grupo", SqlDbType.VarChar, -1).Value = usuario.Grupo;
                    cmdSQL.Parameters.Add("@Cargo", SqlDbType.VarChar, -1).Value = usuario.Cargo;
                    cmdSQL.Parameters.Add("@Rol", SqlDbType.VarChar, -1).Value = usuario.Rol;
                    cmdSQL.Parameters.Add("@Email", SqlDbType.VarChar, -1).Value = usuario.Email;
                    cmdSQL.Parameters.Add("@Password", SqlDbType.VarChar, -1).Value = usuario.Password;
                    cmdSQL.Parameters.Add("@EmpresaId", SqlDbType.Int).Value = usuario.EmpresaId;
                    cmdSQL.Parameters.Add("@Estado", SqlDbType.VarChar, -1).Value = usuario.Estado;
                    cmdSQL.Parameters.Add("@DNI", SqlDbType.VarChar, -1).Value = usuario.DNI;
                    cmdSQL.Parameters.Add("@Conversion", SqlDbType.Int).Value = usuario.Conversion;

                    cmdSQL.Parameters.Add("@DistritoId", SqlDbType.Int).Value = (usuario.DistritoId > 0 ? usuario.DistritoId : null);
                    cmdSQL.Parameters.Add("@NacionalidadId", SqlDbType.Int).Value = usuario.NacionalidadId;
                    cmdSQL.Parameters.Add("@SexoId", SqlDbType.Int).Value = usuario.SexoId;
                    cmdSQL.Parameters.Add("@UltimoAcceso", SqlDbType.DateTime).Value = DateTime.Now;
                    cmdSQL.Parameters.Add("@TipoDocumento", SqlDbType.Int).Value = usuario.TipoDocumento;
                    cmdSQL.Parameters.Add("@Telefono", SqlDbType.VarChar, -1).Value = usuario.Telefono;
                    cmdSQL.Parameters.Add("@Sector", SqlDbType.VarChar, -1).Value = usuario.Sector;

                    cmdSQL.Parameters.Add("@Avatar", SqlDbType.VarChar, -1).Value = usuario.Avatar;
                    cmdSQL.Parameters.Add("@Descripcion", SqlDbType.VarChar, -1).Value = usuario.Descripcion;


                    lintResultado = cmdSQL.ExecuteNonQuery();
                }
                if (conexion.State == ConnectionState.Open)
                {
                    conexion.Close();
                }
            }

            return lintResultado;

        }


        private Usuario asignarValoresUsuario(DataRow dr)
        {
            Usuario objUsu = new Usuario()
            {
                UsuarioId = Convert.ToInt32(dr["UsuarioId"].ToString()),
                Codigo = dr["Codigo"].ToString(),
                Nombre = dr["Nombre"].ToString(),
                Apellido = dr["Apellido"].ToString(),
                Grupo = dr["Grupo"].ToString(),
                Cargo = dr["Cargo"].ToString(),
                Rol = dr["Rol"].ToString(),
                Email = dr["Email"].ToString(),
                Password = dr["Password"].ToString(),
                EmpresaId = Convert.ToInt32(dr["EmpresaId"].ToString()),
                Estado = dr["Estado"].ToString(),
                DNI = dr["DNI"].ToString(),
                Conversion = Convert.ToBoolean(dr["Conversion"].ToString()),
                DistritoId = ConvertHelpers.ToInteger(dr["DistritoId"].ToString()),
                NacionalidadId = ConvertHelpers.ToInteger(dr["NacionalidadId"].ToString()),
                SexoId = ConvertHelpers.ToInteger(dr["SexoId"].ToString()),
                //TODO: revisar si es necesario el milisegundo
                UltimoAcceso = ConvertHelpers.ToDateTime(dr["UltimoAcceso"]),
                TipoDocumento = ConvertHelpers.ToInteger(dr["TipoDocumento"].ToString()),
                Telefono = dr["Telefono"].ToString(),
                Sector = dr["Sector"].ToString(),
                //usuario.Curso = dr["Curso"]?.ToString();
                Avatar = dr["Avatar"].ToString(),
                Descripcion = dr["Descripcion"].ToString(),
                Empresa = new Empresa()
                {
                    RazonSocial = dr["RazonSocial"].ToString()
                },
                Curso = dr.Table.Columns.Contains("Curso") ? dr["Curso"].ToString() : null
            };

            return objUsu;
        }




        public int EliminarUsuarioGrupo(int usuarioId, int grupoId)
        {
            try
            {
                var cnn = new SqlConnection(Util.CadenaConexion);
                var command = new SqlCommand("USP_AddGrupo_DEL", cnn);
                command.Parameters.AddWithValue("@UsuarioId", usuarioId);
                command.Parameters.AddWithValue("@GrupoId", grupoId);
                command.CommandType = System.Data.CommandType.StoredProcedure;

                cnn.Open();
                var reader = command.ExecuteReader();
                var numrows = 1;
                cnn.Close();

                return numrows;
            }
            catch (Exception e)
            {
                Util.SaveErrorLog(e);
                return 0;
            }
        }

        public int InsertarUsuarioGrupo(int usuarioId, int grupoId)
        {
            try
            {
                var cnn = new SqlConnection(Util.CadenaConexion);
                var command = new SqlCommand("USP_AddGrupo_INS", cnn);
                command.Parameters.AddWithValue("@UsuarioId", usuarioId);
                command.Parameters.AddWithValue("@GrupoId", grupoId);
                command.CommandType = System.Data.CommandType.StoredProcedure;

                cnn.Open();
                var reader = command.ExecuteReader();
                var numrows = 1;
                cnn.Close();
                return numrows;
            }
            catch (Exception e)
            {
                Util.SaveErrorLog(e);
                return 0;
            }
        }

        public int VolcadoUsuarioGrupo()
        {
            try
            {
                var cnn = new SqlConnection(Util.CadenaConexion);

                //var sql = @"BULK INSERT UsuarioGrupo FROM " + Util.RutaMasivo + " WITH (FIRSTROW = 1, FIELDTERMINATOR = ',', ROWTERMINATOR = '\n', TABLOCK)";

                var command = new SqlCommand("USP_AddGrupo_FULL", cnn);
                command.Parameters.AddWithValue("@ruta", Util.RutaMasivoRemote);
                command.CommandType = System.Data.CommandType.StoredProcedure;

                cnn.Open();
                var reader = command.ExecuteNonQuery();
                cnn.Close();
                return 1;

            }
            catch (Exception e)
            {
                Util.SaveErrorLog(e);
                return 0;
            }
        }

        public int ConsultarGrupo(int sessionId)
        {
            try
            {
                var cnn = new SqlConnection(Util.CadenaConexion);

                //var sql = @"BULK INSERT UsuarioGrupo FROM " + Util.RutaMasivo + " WITH (FIRSTROW = 1, FIELDTERMINATOR = ',', ROWTERMINATOR = '\n', TABLOCK)";
                int resultado = 0;
                var command = new SqlCommand("USP_DevolverGrupo", cnn);
                command.Parameters.AddWithValue("@UsuarioId", sessionId);
                command.CommandType = System.Data.CommandType.StoredProcedure;

                cnn.Open();
                var reader = command.ExecuteReader();

                if (reader.HasRows)
                {
                    resultado = 1;
                }
                cnn.Close();

                return resultado;
            }
            catch (Exception e)
            {
                Util.SaveErrorLog(e);
                return 0;
            }
        }



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
                obj.Aforo = (dr["Aforo"].ToString() != string.Empty ? (int?)(dr["Aforo"]) : null);
                obj.TipoRegistro = dr["FechaCreacion"] == null ? (string)(dr["TipoRegistro"]) : null;
                retVal.Add(obj);
            }
            return retVal;
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

        public List<UsuarioGrupo> ListarUsuarioGrupos(int grupoId)
        {
            DataTable dt = new DataTable();

            string cadenaConexion = Util.CadenaConexion;

            using (SqlConnection conexion = new SqlConnection(cadenaConexion))
            {
                conexion.Open();

                using (SqlCommand cmdSql = new SqlCommand())
                {
                    cmdSql.Connection = conexion;
                    cmdSql.CommandType = CommandType.StoredProcedure;
                    cmdSql.CommandText = "USP_GrupoUsuario_LIS";
                    cmdSql.Parameters.Add("@GrupoId", SqlDbType.Int).Value = grupoId;

                    using (SqlDataAdapter da = new SqlDataAdapter(cmdSql))
                    {
                        da.Fill(dt);
                    }
                }
                if (conexion.State == ConnectionState.Open)
                {
                    conexion.Close();
                }
            }

            return (from DataRow dr in dt.Rows
                select new UsuarioGrupo
                {
                    UsuarioGrupoId = (int)(dr["UsuarioGrupoId"]),
                    GrupoId = (int)(dr["GrupoId"]),
                    UsuarioId = (int)(dr["UsuarioId"]),
                    Estado = (string)(dr["Estado"]),
                    Nombre = (string)(dr["Nombre"])
                }).ToList();
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





        #region Asesor 
        public List<Usuario> ListarUsuarioAsesor()
        {
            List<Usuario> retVal = new List<Usuario>();

            DataTable dt = new DataTable();

            string cadenaConexion = Util.CadenaConexion;

            using (SqlConnection conexion = new SqlConnection(cadenaConexion))
            {
                conexion.Open();

                using (SqlCommand cmdSQL = new SqlCommand())
                {
                    cmdSQL.Connection = conexion;
                    cmdSQL.CommandType = CommandType.StoredProcedure;
                    cmdSQL.CommandText = "USP_Usuario_LIS_Asesores";


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
                retVal.Add(asignarValoresUsuario(dr));
            }

            return retVal;
        }

        public int? TotalEmpresaActivos()
        {
            int lintResult = 0;
            DataTable dt = new DataTable();

            string cadenaConexion = Util.CadenaConexion;
            using (SqlConnection conexion = new SqlConnection(cadenaConexion))
            {
                conexion.Open();
                using (SqlCommand cmdSQL = new SqlCommand())
                {
                    cmdSQL.Connection = conexion;
                    cmdSQL.CommandType = CommandType.StoredProcedure;
                    cmdSQL.CommandText = "USP_Usuario_SEL_TotalEmpresasActivos";
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
                lintResult = ConvertHelpers.ToInteger(dr["Total"].ToString());
            }

            return lintResult;
        }

        #endregion

    }
}
