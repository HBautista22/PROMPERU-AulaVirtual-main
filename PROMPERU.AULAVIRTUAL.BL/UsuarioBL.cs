using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using PROMPERU.AULAVIRTUAL.DA;
using PROMPERU.AULAVIRTUAL.BE.USUARIO;
using System.Data;
using PROMPERU.AULAVIRTUAL.BE.CURSOS;

namespace PROMPERU.AULAVIRTUAL.BL
{
    public class UsuarioBL
    {
        public int EliminarUsuarioGrupo(UsuarioItems usuario)
        {
            int total = 0;
            int contador = 0;
            UsuarioDA usuarioSQL = new UsuarioDA();



            foreach (var item1 in usuario.UsuarioId)
            {
                if (usuario.GrupoId != null && !usuario.GrupoId.Length.Equals(0))
                {
                    foreach (var item2 in usuario.GrupoId)
                    {
                        contador += usuarioSQL.EliminarUsuarioGrupo(item1, item2);
                        total++;
                    }
                }
                else
                {
                    usuarioSQL.EliminarUsuarioGrupo(item1, 0);
                }

            }


            if (total == contador)
            {
                return 1;
            }
            else
            {
                return 0;
            }

        }

        public int InsertarUsuarioGrupo(UsuarioItems usuario)
        {

            int total = 0;
            int contador = 0;
            UsuarioDA usuarioSQL = new UsuarioDA();

            foreach (var item1 in usuario.UsuarioId)
            {
                foreach (var item2 in usuario.GrupoId)
                {
                    contador += usuarioSQL.InsertarUsuarioGrupo(item1, item2);
                    total++;
                }
            }

            if (total == contador)
            {
                return 1;
            }
            else
            {
                return 0;
            }

        }

        public int? TotalEmpresaActivos()
        {
            UsuarioDA usuarioSQL = new UsuarioDA();
            return usuarioSQL.TotalEmpresaActivos();
        }

        public int VolcadoUsuarioGrupo()
        {
            UsuarioDA usuarioSQL = new UsuarioDA();
            return usuarioSQL.VolcadoUsuarioGrupo();
        }

        public int ConsultarGrupo(int sessionId)
        {
            UsuarioDA usuarioSQL = new UsuarioDA();
            return usuarioSQL.ConsultarGrupo(sessionId);
        }

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
            UsuarioDA usuarioSQL = new UsuarioDA();
            return usuarioSQL.ConsultarCuentaPorEmailRol(email, rol);
        }

        public Usuario ObtenerUsuarioPorEmail(string email)
        {
            UsuarioDA usuarioSQL = new UsuarioDA();
            return usuarioSQL.ObtenerUsuarioPorEmail(email);

        }

        public List<Usuario> ListarUsuarioConCertificado()
        {
            UsuarioDA usuarioSql = new UsuarioDA();
            return usuarioSql.ListarUsuarioConCertificado();
        }

        public List<Usuario> ListarAlumnosActivos()
        {
            UsuarioDA usuarioSQL = new UsuarioDA();
            return usuarioSQL.ListarAlumnosActivos();
        }

        public List<Usuario> ListarProfesoresActivos()
        {
            UsuarioDA usuarioSQL = new UsuarioDA();
            return usuarioSQL.ListarProfesoresActivos();
        }


        public int TotalAlumnosActivos()
        {
            UsuarioDA usuarioSQL = new UsuarioDA();
            return usuarioSQL.TotalAlumnosActivos();
        }

        public int TotalUsuariosPorEmpresa(int empresaId)
        {
            UsuarioDA usuarioSQL = new UsuarioDA();
            return usuarioSQL.TotalUsuariosPorEmpresa(empresaId);

        }



        public int TotalCertificados()
        {
            UsuarioDA usuarioSQL = new UsuarioDA();
            return usuarioSQL.TotalCertificados();
        }

            public int TotalCursosActivos()
        {
            UsuarioDA usuarioSQL = new UsuarioDA();
            return usuarioSQL.TotalCursosActivos();

        }


        public int TotalUsuariossActivosPorGrupo(int grupoId)
        {
            UsuarioDA usuarioSQL = new UsuarioDA();
            return usuarioSQL.TotalUsuariosActivosPorGrupo(grupoId);
        }

        public Usuario ObtenerUsuarioPorId(int usuarioId)
        {
            UsuarioDA usuarioSQL = new UsuarioDA();
            return usuarioSQL.ObtenerUsuarioPorId(usuarioId);

        }

        public List<LogAccesosUsuario> ObtenerLogAccesosUsuario(int usuarioId)
        {
            UsuarioDA usuarioSQL = new UsuarioDA();
            return usuarioSQL.ObtenerLogAccesosUsuario(usuarioId);
        }
        public UsuarioGrupo ObtenerUsuarioGrupoPorUsuarioId(int usuarioId)
        {
            UsuarioDA usuarioSQL = new UsuarioDA();
            return usuarioSQL.ObtenerUsuarioGrupoPorUsuarioId(usuarioId);

        }

        public int InsertarUsuario(Usuario usuario)
        {
            UsuarioDA usuarioSQL = new UsuarioDA();
            return usuarioSQL.InsertarUsuario(usuario);

        }

        public int ActualizarUsuario(Usuario usuario)
        {
            UsuarioDA usuarioSQL = new UsuarioDA();
            return usuarioSQL.ActualizarUsuario(usuario);

        }

        public int InsertarUsuarioUltimoAcceso(Usuario usuario)
        {
            UsuarioDA usuarioSQL = new UsuarioDA();
            return usuarioSQL.InsertarUsuarioUltimoAcceso(usuario);
        }

        public int EliminarUsuarioGrupo(int usuarioId, int grupoId)
        {
            UsuarioDA usuarioSQL = new UsuarioDA();
            return usuarioSQL.EliminarUsuarioGrupo(usuarioId, grupoId);
        }

        public int InsertarUsuarioGrupo(int usuarioId, int grupoId)
        {
            UsuarioDA usuarioSQL = new UsuarioDA();
            return usuarioSQL.InsertarUsuarioGrupo(usuarioId, grupoId);
        }

        public List<Usuario> ObtenerUsuariosPorEmpresa(int empresaId)
        {
            UsuarioDA usuarioSQL = new UsuarioDA();
            return usuarioSQL.ObtenerUsuariosPorEmpresa(empresaId);
        }

        public List<Grupo> ListarGrupo()
        {
            UsuarioDA SQL = new UsuarioDA();
            return SQL.ListarGrupo();
        }

        public Grupo ObtenerGrupoPorId(int? GrupoId)
        {
            UsuarioDA SQL = new UsuarioDA();
            return SQL.ObtenerGrupoPorId(GrupoId);
        }

        public List<Grupo> ListarAddGrupoLISPorUsuarioId(int usuarioId)
        {
            UsuarioDA SQL = new UsuarioDA();
            return SQL.ListarAddGrupoLISPorUsuarioId(usuarioId);
        }

        public List<Grupo> ListarAddGrupoSELPorUsuarioId(int usuarioId)
        {
            UsuarioDA SQL = new UsuarioDA();
            return SQL.ListarAddGrupoSELPorUsuarioId(usuarioId);
        }


        public List<UsuarioGrupo> ListarUsuarioGrupos(int grupoId)
        {
            UsuarioDA sql = new UsuarioDA();
            return sql.ListarUsuarioGrupos(grupoId);
        }


        #region Asesores
        public List<Usuario> ListarUsuarioAsesor()
        {
            UsuarioDA usuarioSQL = new UsuarioDA();
            return usuarioSQL.ListarUsuarioAsesor();
        }

        public int GrabarLogAccesoUsuario(LogAccesosUsuario log)
        {
            UsuarioDA usuarioSQL = new UsuarioDA();
            return usuarioSQL.GrabarLogAccesoUsuario(log);
        }

        public int InsertarCambioContrasena(CambioContrasena cambio)
        {
            UsuarioDA usuarioSQL = new UsuarioDA();
            return usuarioSQL.InsertarCambioContrasena(cambio);
        }

        public int InsertarUsuarioGrupo(UsuarioGrupo usuarioLista)
        {
            UsuarioDA usuarioSQL = new UsuarioDA();
            return usuarioSQL.InsertarUsuarioGrupo(usuarioLista);
        }

        public CambioContrasena ObtenerCambioContrasena(int cambioId)
        {
            UsuarioDA usuarioSQL = new UsuarioDA();
            return usuarioSQL.ObtenerCambioContrasena(cambioId);
        }

        public int ActualizarContraseña(int usuarioId, string password)
        {
            UsuarioDA usuarioSQL = new UsuarioDA();
            return usuarioSQL.ActualizarContraseña(usuarioId, password);
        }

        public int ActualizarCambioContrasena(int cambioId, DateTime? fechaCambio)
        {
            UsuarioDA usuarioSQL = new UsuarioDA();
            return usuarioSQL.ActualizarCambioContrasena(cambioId, fechaCambio);
        }

        public int ActualizarUsuarioEstado(int usuarioId, string estado)
        {
            UsuarioDA usuarioSQL = new UsuarioDA();
            return usuarioSQL.ActualizarUsuarioEstado(usuarioId, estado);
        }

        public int ActualizarUsuarioCargo(Usuario usuario)
        {
            UsuarioDA usuarioSQL = new UsuarioDA();
            return usuarioSQL.ActualizarUsuarioCargo(usuario);
        }



        public CambioContrasena ObtenerCambioContrasena(string usermailtoken)
        {
            UsuarioDA usuarioSQL = new UsuarioDA();
            return usuarioSQL.ObtenerCambioContrasena(usermailtoken);
        }


        #endregion

    }
}
