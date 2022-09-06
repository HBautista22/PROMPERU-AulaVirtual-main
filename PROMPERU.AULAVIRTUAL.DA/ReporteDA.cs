using System;
using System.Collections.Generic;
using System.Data;
using System.Data.SqlClient;
using System.Linq;
using System.Runtime.Serialization;
using System.Text;
using System.Threading.Tasks;
using PROMPERU.AULAVIRTUAL.BE.CURSOS;
using PROMPERU.AULAVIRTUAL.BE.Aesorias;
using PROMPERU.AULAVIRTUAL.Helpers;

namespace PROMPERU.AULAVIRTUAL.DA
{
    public class ReporteDA
    {
        public List<ViewReporteHistoricoAlumno> ListarViewReporteHistoricoAlumno()
        {
            List<ViewReporteHistoricoAlumno> retVal = new List<ViewReporteHistoricoAlumno>();

            DataTable dt = new DataTable();

            string cadenaConexion = Util.CadenaConexion;

            using (SqlConnection conexion = new SqlConnection(cadenaConexion))
            {
                conexion.Open();

                using (SqlCommand cmdSQL = new SqlCommand())
                {
                    cmdSQL.Connection = conexion;
                    cmdSQL.CommandType = CommandType.StoredProcedure;
                    cmdSQL.CommandText = "USP_ViewReporteHistoricoAlumno_LIS";

                    //cmdSQL.Parameters.Add("@CMS_LabelId", SqlDbType.Int).Value = traduccionIdInt;

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
                ViewReporteHistoricoAlumno obj = new ViewReporteHistoricoAlumno();
                obj.Ususario_UsuarioId = (int)(dr["Ususario_UsuarioId"]);
                obj.Usuario_Codigo = dr["Usuario_Codigo"].ToString();
                obj.Usuario_Cargo = dr["Usuario_Cargo"].ToString();
                obj.Usuario_Grupo = dr["Usuario_Grupo"].ToString();
                obj.Usuario_NombreCompleto = dr["Usuario_NombreCompleto"].ToString();
                obj.Usuario_Email = dr["Usuario_Email"].ToString();
                obj.Usuario_EmpresaId = (dr["Usuario_EmpresaId"].ToString() != String.Empty ? (int?)(dr["Usuario_EmpresaId"]) : null);
                obj.Usuario_Sexo = dr["Usuario_Sexo"].ToString();
                obj.Usuario_Nacionalidad = dr["Usuario_Nacionalidad"].ToString();
                obj.Usuario_Ubigeo = dr["Usuario_Ubigeo"].ToString();
                obj.Usuario_Distrito = dr["Usuario_Distrito"].ToString();
                obj.Usuario_Provincia = dr["Usuario_Provincia"].ToString();
                obj.Usuario_Departamento = dr["Usuario_Departamento"].ToString();
                obj.Usuario_UltimoAcceso = (dr["Usuario_UltimoAcceso"].ToString() != String.Empty ? (DateTime?)(dr["Usuario_UltimoAcceso"]) : null);
                obj.Usuario_Dni = dr["Usuario_Dni"].ToString();
                obj.Usuario_OtroDoc = dr["Usuario_OtroDoc"].ToString();
                obj.Empresa_RazonSocial = dr["Empresa_RazonSocial"].ToString();
                obj.Empresa_RUC = dr["Empresa_RUC"].ToString();
                obj.Curso_Nombre = dr["Curso_Nombre"].ToString();
                obj.Curso_CursoOnlineId = !string.IsNullOrEmpty(dr["Curso_CursoOnlineId"].ToString()) ? (int)dr["Curso_CursoOnlineId"] : (int?)null;
                obj.Usuario_Estado = dr["Usuario_Estado"].ToString();
                obj.Matricula_MatriculaCursoOnlineId = !string.IsNullOrEmpty(dr["Matricula_MatriculaCursoOnlineId"].ToString()) ? (int)dr["Matricula_MatriculaCursoOnlineId"] : (int?)null;
                obj.Matricula_Estado = dr["Matricula_Estado"].ToString();
                obj.Matricula_FechaMatricula = !string.IsNullOrEmpty(dr["Matricula_FechaMatricula"].ToString()) ? (DateTime)(dr["Matricula_FechaMatricula"]) : (DateTime?)null;
                obj.Matricula_FechaCompletado = (dr["Matricula_FechaCompletado"].ToString() != String.Empty ? (DateTime?)(dr["Matricula_FechaCompletado"]) : null);
                obj.Matricula_FechaAprobado = (dr["Matricula_FechaAprobado"].ToString() != String.Empty ? (DateTime?)(dr["Matricula_FechaAprobado"]) : null);
                obj.Matricula_PorcentajeAvance = !string.IsNullOrEmpty(dr["Matricula_PorcentajeAvance"].ToString()) ? (decimal)(dr["Matricula_PorcentajeAvance"]) : (decimal?) null;
                obj.Matricula_PorcentajeObtenido = (dr["Matricula_PorcentajeObtenido"].ToString() != String.Empty ? (decimal?)(dr["Matricula_PorcentajeObtenido"]) : null);
                obj.Matricula_Nota = (dr["Matricula_Nota"].ToString() != String.Empty ? (decimal?)(dr["Matricula_Nota"]) : null);

                retVal.Add(obj);
            }


            return retVal;
        }

        public List<ViewReporteHistoricoAlumnoLst> ListarViewReporteHistoricoAlumnoLst()
        {
            List<ViewReporteHistoricoAlumnoLst> retVal = new List<ViewReporteHistoricoAlumnoLst>();

            DataTable dt = new DataTable();

            string cadenaConexion = Util.CadenaConexion;

            using (SqlConnection conexion = new SqlConnection(cadenaConexion))
            {
                conexion.Open();

                using (SqlCommand cmdSQL = new SqlCommand())
                {
                    cmdSQL.Connection = conexion;
                    cmdSQL.CommandType = CommandType.StoredProcedure;
                    cmdSQL.CommandText = "USP_ViewReporteHistoricoAlumnoLst_LIS";

                    //cmdSQL.Parameters.Add("@CMS_LabelId", SqlDbType.Int).Value = traduccionIdInt;

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
                ViewReporteHistoricoAlumnoLst obj = new ViewReporteHistoricoAlumnoLst
                {
                    Usuario_Dni = dr["DNI"].ToString(),
                    Usuario_OtroDoc = dr["Usuario_OtroDoc"].ToString(),
                    Ruc = dr["Ruc"].ToString(),
                    Apellido = dr["Apellido"].ToString(),
                    Nombre = dr["Nombre"].ToString(),
                    Rol = dr["Rol"].ToString(),
                    Sexo = dr["Sexo"].ToString(),
                    Cargo = dr["Cargo"].ToString(),
                    Email = dr["Email"].ToString(),
                    FechaRegistro = dr["FechaRegistro"].ToString(),
                    Nacionalidad = dr["Nacionalidad"].ToString(),
                    Departamento = dr["Departamento"].ToString(),
                    Provincia = dr["Provincia"].ToString(),
                    Distrito = dr["Distrito"].ToString(),
                    Estado = dr["Estado"].ToString(),
                    CursoMaestroNombre = dr["CursoMaestro"].ToString(),
                    TieneConstancia = dr["TieneConstancia"].ToString()
                };


                retVal.Add(obj);

            }


            return retVal;
        }

        public List<ViewReporteAsesoriaRemota> ListarViewReporteAsesoriaRemota()
        {
            List<ViewReporteAsesoriaRemota> retVal = new List<ViewReporteAsesoriaRemota>();

            DataTable dt = new DataTable();

            string cadenaConexion = Util.CadenaConexion;

            using (SqlConnection conexion = new SqlConnection(cadenaConexion))
            {
                conexion.Open();

                using (SqlCommand cmdSQL = new SqlCommand())
                {
                    cmdSQL.Connection = conexion;
                    cmdSQL.CommandType = CommandType.StoredProcedure;
                    cmdSQL.CommandText = "USP_AsesoriaRemotaReporte_LIS";

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
                ViewReporteAsesoriaRemota obj = new ViewReporteAsesoriaRemota();

                obj.Asre_Nombre = dr["Asre_Nombre"].ToString();
                obj.Nombre = dr["Nombre"].ToString();
                obj.Apellido = dr["Apellido"].ToString();
                obj.Email = dr["Email"].ToString();
                obj.Cargo = dr["Cargo"].ToString();
                obj.Asre_Inicio = (DateTime)dr["Asre_Inicio"];
                obj.Asre_Fin = (DateTime)dr["Asre_Fin"];
                obj.Asre_Estado = dr["Asre_Estado"].ToString();
                retVal.Add(obj);
            }

            return retVal;

        }


        public List<ViewReporteSolicitudAsesoriaRemota> ListarViewReporteSolicitudAsesoriaRemota()
        {
            List<ViewReporteSolicitudAsesoriaRemota> retVal = new List<ViewReporteSolicitudAsesoriaRemota>();

            DataTable dt = new DataTable();

            string cadenaConexion = Util.CadenaConexion;

            using (SqlConnection conexion = new SqlConnection(cadenaConexion))
            {
                conexion.Open();

                using (SqlCommand cmdSQL = new SqlCommand())
                {
                    cmdSQL.Connection = conexion;
                    cmdSQL.CommandType = CommandType.StoredProcedure;
                    cmdSQL.CommandText = "USP_SolicitudAsesoriaReporte_LIS";

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
                ViewReporteSolicitudAsesoriaRemota obj = new ViewReporteSolicitudAsesoriaRemota();

                obj.Asre_Nombre = dr["Asre_Nombre"].ToString();
                obj.Nombre = dr["Nombre"].ToString();
                obj.Apellido = dr["Apellido"].ToString();
                obj.Asre_Inicio = (DateTime)dr["Asre_Inicio"];
                obj.Asre_Fin = (DateTime)dr["Asre_Fin"];
                obj.Asre_Estado = dr["Asre_Estado"].ToString();
                obj.NombreAsesorado = dr["NombreAsesorado"].ToString();
                obj.ApellidoAsesorado = dr["ApellidoAsesorado"].ToString();
                obj.TipoDocumento = dr["TipoDocumento"].ToString();
                obj.DNI = dr["DNI"].ToString();
                obj.Telefono = dr["Telefono"].ToString();
                obj.RUC = dr["RUC"].ToString();
                obj.Cargo = dr["Cargo"].ToString();
                obj.EmailAsesorado = dr["EmailAsesorado"].ToString();
                obj.Sola_Consulta = dr["Sola_Consulta"].ToString();
                obj.Sola_Estado = dr["Sola_Estado"].ToString();

                retVal.Add(obj);
            }

            return retVal;

        }


    }
}
