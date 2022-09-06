using System;
using System.Collections.Generic;
using System.Data;
using System.Data.SqlClient;
using System.Linq;
using MoreLinq;
using PROMPERU.AULAVIRTUAL.BE.CURSOS;
using PROMPERU.AULAVIRTUAL.BE.USUARIO;
using PROMPERU.AULAVIRTUAL.Helpers;


namespace PROMPERU.AULAVIRTUAL.DA
{
    public class CursosDA
    {
        public List<CursoOnlineResponse> ListarCursosOnlineResponse()
        {

            List<CursoOnlineResponse> retVal = new List<CursoOnlineResponse>();

            DataTable dt = new DataTable();

            string cadenaConexion = Util.CadenaConexion;

            using (SqlConnection conexion = new SqlConnection(cadenaConexion))
            {
                conexion.Open();

                using (SqlCommand cmdSQL = new SqlCommand())
                {
                    cmdSQL.Connection = conexion;
                    cmdSQL.CommandType = CommandType.StoredProcedure;
                    cmdSQL.CommandText = "USP_CursosOnlineSeleccionados_LIS";


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
                CursoOnlineResponse obj = new CursoOnlineResponse();
                obj.CursoOnlineId = Convert.ToInt32(dr["CursoOnlineId"].ToString());
                obj.CategoriaCursoOnlineId = Convert.ToInt32(dr["CategoriaCursoOnlineId"].ToString());
                obj.Calificacion = Convert.ToInt32(dr["Calificacion"].ToString());
                obj.CantidadCalificacion = Convert.ToInt32(dr["CantidadCalificacion"].ToString());
                obj.Nombre = dr["Nombre"].ToString();
                obj.RutaImagen = dr["RutaImagen"].ToString();
                obj.Ranking = Convert.ToInt32(dr["Ranking"].ToString());
                obj.Descripcion = dr["Descripcion"].ToString();
                obj.FechaCreacion = (dr["FechaCreacion"].ToString() != String.Empty ? (DateTime?)(dr["FechaCreacion"]) : null);  //Convert.ToDateTime(dr["FechaCreacion"].ToString());
                retVal.Add(obj);
            }


            return retVal;
        }

        public List<MatriculaCursoOnline> ListarMatriculaCursoOnlinePorCursoOnlineId(string iNACTIVO)
        {
            List<MatriculaCursoOnline> retVal = new List<MatriculaCursoOnline>();

            DataTable dt = new DataTable();

            string cadenaConexion = Util.CadenaConexion;

            using (SqlConnection conexion = new SqlConnection(cadenaConexion))
            {
                conexion.Open();

                using (SqlCommand cmdSQL = new SqlCommand())
                {
                    cmdSQL.Connection = conexion;
                    cmdSQL.CommandType = CommandType.StoredProcedure;
                    cmdSQL.CommandText = "USP_MatriculaCursoOnlineUsuario_LIS_PorEstado";

                    cmdSQL.Parameters.Add("@Estado", SqlDbType.VarChar).Value = iNACTIVO;


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


            List<DataRow> listDR = dt.AsEnumerable().ToList();

            retVal =
                listDR
                    .DistinctBy(
                        x =>
                            new
                            {
                                a = x["CursoOnlineId"]
                            })
                    .Select(
                        a => new MatriculaCursoOnline()
                        {
                            UsuarioId = (int)(a["UsuarioId"]),
                            CursoOnlineId = (int)(a["CursoOnlineId"]),
                            MatriculaCursoOnlineId = (int)(a["MatriculaCursoOnlineId"]),
                            Estado = (a["Estado"].ToString() != String.Empty ? (string)(a["Estado"]) : null),
                            FechaMatricula = (DateTime)(a["FechaMatricula"]),
                            FechaCompletado = (a["FechaCompletado"].ToString() != string.Empty ? (DateTime?)(a["FechaCompletado"]) : null),
                            FechaAprobado = (a["FechaAprobado"].ToString() != string.Empty ? (DateTime?)(a["FechaAprobado"]) : null),
                            PorcentajeAvance = (decimal)(a["PorcentajeAvance"]),
                            Nota = (a["Nota"].ToString() != String.Empty ? (decimal?)(a["Nota"]) : null),
                            PorcentajeObtenido = (a["PorcentajeObtenido"].ToString() != String.Empty ? (decimal?)(a["PorcentajeObtenido"]) : null),
                            TotalUnidadesCursoOnline = (a["TotalUnidadesCursoOnline"].ToString() != String.Empty ? (int?)(a["TotalUnidadesCursoOnline"]) : null),
                            Grupo = (a["Grupo"].ToString() != String.Empty ? (string)(a["Grupo"]) : null),
                            Ranking = (a["Ranking"].ToString() != String.Empty ? (int?)(a["Ranking"]) : null)
                        })
                        .ToList();
            UsuarioDA usuarioDA = new UsuarioDA();
            foreach (MatriculaCursoOnline mco in retVal)
            {
                mco.CursoOnline = ObtenerCursoOnlinePorId(mco.CursoOnlineId);
                mco.Usuario = usuarioDA.ObtenerUsuarioPorId(mco.UsuarioId);
            }

            return retVal;
        }

        public List<MatriculaCursoOnline> ListarMatriculaCursoOnlinePorCursoOnlineId(int? cursoOnlineId, string iNACTIVO)
        {
            List<MatriculaCursoOnline> retVal = new List<MatriculaCursoOnline>();

            DataTable dt = new DataTable();

            string cadenaConexion = Util.CadenaConexion;

            using (SqlConnection conexion = new SqlConnection(cadenaConexion))
            {
                conexion.Open();

                using (SqlCommand cmdSQL = new SqlCommand())
                {
                    cmdSQL.Connection = conexion;
                    cmdSQL.CommandType = CommandType.StoredProcedure;
                    cmdSQL.CommandText = "USP_MatriculaCursoOnlineUsuario_LIS_PorCurso";

                    cmdSQL.Parameters.Add("@CursoOnlineId", SqlDbType.Int).Value = cursoOnlineId;
                    cmdSQL.Parameters.Add("@Estado", SqlDbType.VarChar).Value = iNACTIVO;


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


            List<DataRow> listDR = dt.AsEnumerable().ToList();

            retVal =
                listDR
                    .DistinctBy(
                        x =>
                            new
                            {
                                a = x["CursoOnlineId"]
                            })
                    .Select(
                        a => new MatriculaCursoOnline()
                        {
                            UsuarioId = (int)(a["UsuarioId"]),
                            CursoOnlineId = (int)(a["CursoOnlineId"]),
                            MatriculaCursoOnlineId = (int)(a["MatriculaCursoOnlineId"]),
                            Estado = (a["Estado"].ToString() != String.Empty ? (string)(a["Estado"]) : null),
                            FechaMatricula = (DateTime)(a["FechaMatricula"]),
                            FechaCompletado = (a["FechaCompletado"].ToString() != string.Empty ? (DateTime?)(a["FechaCompletado"]) : null),
                            FechaAprobado = (a["FechaAprobado"].ToString() != string.Empty ? (DateTime?)(a["FechaAprobado"]) : null),
                            PorcentajeAvance = (decimal)(a["PorcentajeAvance"]),
                            Nota = (a["Nota"].ToString() != String.Empty ? (decimal?)(a["Nota"]) : null),
                            PorcentajeObtenido = (a["PorcentajeObtenido"].ToString() != String.Empty ? (decimal?)(a["PorcentajeObtenido"]) : null),
                            TotalUnidadesCursoOnline = (a["TotalUnidadesCursoOnline"].ToString() != String.Empty ? (int?)(a["TotalUnidadesCursoOnline"]) : null),
                            Grupo = (a["Grupo"].ToString() != String.Empty ? (string)(a["Grupo"]) : null),
                            Ranking = (a["Ranking"].ToString() != String.Empty ? (int?)(a["Ranking"]) : null)
                        })
                        .ToList();
            UsuarioDA usuarioDA = new UsuarioDA();

            foreach (MatriculaCursoOnline mco in retVal)
            {
                mco.CursoOnline = ObtenerCursoOnlinePorId(mco.CursoOnlineId);
                mco.Usuario = usuarioDA.ObtenerUsuarioPorId(mco.UsuarioId);
            }

            return retVal;
        }

        public List<ViewCursoMatricula> ListaViewCursoMatricula()
        {
            List<ViewCursoMatricula> retVal = new List<ViewCursoMatricula>();

            DataTable dt = new DataTable();

            string cadenaConexion = Util.CadenaConexion;

            using (SqlConnection conexion = new SqlConnection(cadenaConexion))
            {
                conexion.Open();

                using (SqlCommand cmdSQL = new SqlCommand())
                {
                    cmdSQL.Connection = conexion;
                    cmdSQL.CommandType = CommandType.StoredProcedure;
                    cmdSQL.CommandText = "USP_ViewCursoMatricula_LIS";


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
                ViewCursoMatricula obj = new ViewCursoMatricula();
                obj.CursoOnlineId = Convert.ToInt32(dr["CursoOnlineId"].ToString());
                obj.Nombre = dr["Nombre"].ToString();
                retVal.Add(obj);
            }


            return retVal;
        }

        public List<CursoOnlineFavorito> ObtenerCursosFavoritos(int usuarioId)
        {
            List<CursoOnlineFavorito> retVal = new List<CursoOnlineFavorito>();

            DataTable dt = new DataTable();

            string cadenaConexion = Util.CadenaConexion;

            using (SqlConnection conexion = new SqlConnection(cadenaConexion))
            {
                conexion.Open();

                using (SqlCommand cmdSQL = new SqlCommand())
                {
                    cmdSQL.Connection = conexion;
                    cmdSQL.CommandType = CommandType.StoredProcedure;
                    cmdSQL.CommandText = "USP_CursoOnlineFavorito_LIS";
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
                CursoOnlineFavorito obj = new CursoOnlineFavorito();
                obj.CursoOnlineFavoritoId = Convert.ToInt32(dr["CursoOnlineFavoritoId"].ToString());
                obj.CursoOnlineId = Convert.ToInt32(dr["CursoOnlineId"].ToString());
                obj.CursoOnline = ObtenerCursoOnlinePorId(obj.CursoOnlineId);
                retVal.Add(obj);
            }


            return retVal;
        }

        public List<CursoOnlineVisto> ObtenerCursosVisto(int usuarioId)
        {
            List<CursoOnlineVisto> retVal = new List<CursoOnlineVisto>();

            DataTable dt = new DataTable();

            string cadenaConexion = Util.CadenaConexion;

            using (SqlConnection conexion = new SqlConnection(cadenaConexion))
            {
                conexion.Open();

                using (SqlCommand cmdSQL = new SqlCommand())
                {
                    cmdSQL.Connection = conexion;
                    cmdSQL.CommandType = CommandType.StoredProcedure;
                    cmdSQL.CommandText = "USP_CursoOnlineVisto_LIS";
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
                CursoOnlineVisto obj = new CursoOnlineVisto();
                obj.CursoOnlineId = Convert.ToInt32(dr["CursoOnlineId"].ToString());
                obj.CursoOnline = ObtenerCursoOnlinePorId(obj.CursoOnlineId);
                retVal.Add(obj);
            }


            return retVal;
        }

        public List<UnidadCursoOnline> ListarUnidadCursoOnlinePorUnidad(int unidadCursoOnlineId)
        {
            List<UnidadCursoOnline> retVal = new List<UnidadCursoOnline>();

            DataTable dt = new DataTable();

            string cadenaConexion = Util.CadenaConexion;

            using (SqlConnection conexion = new SqlConnection(cadenaConexion))
            {
                conexion.Open();

                using (SqlCommand cmdSQL = new SqlCommand())
                {
                    cmdSQL.Connection = conexion;
                    cmdSQL.CommandType = CommandType.StoredProcedure;
                    cmdSQL.CommandText = "USP_UnidadCursoOnline_LIS_PorUnidadID";

                    cmdSQL.Parameters.Add("@UnidadCursoOnlineId", SqlDbType.Int).Value = unidadCursoOnlineId;


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
                UnidadCursoOnline obj = new UnidadCursoOnline();
                obj.UnidadCursoOnlineId = (int)(dr["UnidadCursoOnlineId"]);
                obj.CursoOnlineId = (int)(dr["CursoOnlineId"]);
                //obj.TipoUnidadId = (int)(dr["TipoUnidadId"]);
                obj.TipoUnidadId = (dr["TipoUnidadId"].ToString() != String.Empty ? (int?)(dr["TipoUnidadId"]) : null); ;
                obj.UnidadCursoOnlinePadreId = (dr["UnidadCursoOnlinePadreId"].ToString() != String.Empty ? (int?)(dr["UnidadCursoOnlinePadreId"]) : null);

                obj.HasTarea = (bool)(dr["HasTarea"]);
                obj.TareaFechaLimite = (dr["TareaFechaLimite"].ToString() != String.Empty ? (DateTime?)(dr["TareaFechaLimite"]) : null);  

                obj.Nombre = (string)(dr["Nombre"]);
                obj.Descripcion = (dr["Descripcion"].ToString() != String.Empty ? (string)(dr["Descripcion"]) : null);
                obj.GUID = (string)(dr["GUID"]);
                obj.RutaArchivoOriginal = (dr["RutaArchivoOriginal"].ToString() != String.Empty ? (string)(dr["RutaArchivoOriginal"]) : null);
                obj.RutaArchivoInicio = (dr["RutaArchivoInicio"].ToString() != String.Empty ? (string)(dr["RutaArchivoInicio"]) : null);
                obj.RutaWeb = (dr["RutaWeb"].ToString() != String.Empty ? (string)(dr["RutaWeb"]) : null);
                obj.Orden = (int)(dr["Orden"]);
                obj.Estado = (string)(dr["Estado"]);
                obj.TiempoPermanencia = (int)(dr["TiempoPermanencia"]);
                obj.AnchoContenedor = (dr["AnchoContenedor"].ToString() != String.Empty ? (int?)(dr["AnchoContenedor"]) : null);
                obj.AltoContenedor = (dr["AltoContenedor"].ToString() != String.Empty ? (int?)(dr["AltoContenedor"]) : null);
                obj.TipoUnidad = (string)(dr["TipoUnidad"]);
                obj.RutaArchivoAdicional = (dr["RutaArchivoAdicional"].ToString() != String.Empty ? (string)(dr["RutaArchivoAdicional"]) : null);
                obj.ExtensionArchivoAdicional = (dr["ExtensionArchivoAdicional"].ToString() != String.Empty ? (string)(dr["ExtensionArchivoAdicional"]) : null);
                obj.TipoContenidoArchivoAdicional = (dr["TipoContenidoArchivoAdicional"].ToString() != String.Empty ? (string)(dr["TipoContenidoArchivoAdicional"]) : null);
                retVal.Add(obj);
            }

            return retVal;
        }

        public int InsertarCursoOnlineVisto(int cursoOnlineId, int usuarioId)
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
                    cmdSQL.CommandText = "USP_CursoOnlineVisto_INS";
                    cmdSQL.Parameters.Add("@CursoOnlineId", SqlDbType.Int).Value = cursoOnlineId;
                    cmdSQL.Parameters.Add("@UsuarioId", SqlDbType.Int).Value = usuarioId;

                    lintResultado = Convert.ToInt32(cmdSQL.ExecuteScalar().ToString());

                }
                if (conexion.State == ConnectionState.Open)
                {
                    conexion.Close();
                }
            }

            return lintResultado;
        }

        public List<CursoOnlineResponse> ListarCursosOnlineRankingResponse()
        {

            List<CursoOnlineResponse> retVal = new List<CursoOnlineResponse>();

            DataTable dt = new DataTable();

            string cadenaConexion = Util.CadenaConexion;

            using (SqlConnection conexion = new SqlConnection(cadenaConexion))
            {
                conexion.Open();

                using (SqlCommand cmdSQL = new SqlCommand())
                {
                    cmdSQL.Connection = conexion;
                    cmdSQL.CommandType = CommandType.StoredProcedure;
                    cmdSQL.CommandText = "USP_CursosOnlineRanking_LIS";


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
                CursoOnlineResponse obj = new CursoOnlineResponse();
                obj.CursoOnlineId = Convert.ToInt32(dr["CursoOnlineId"].ToString());
                obj.CategoriaCursoOnlineId = Convert.ToInt32(dr["CategoriaCursoOnlineId"].ToString());
                obj.Total = Convert.ToInt32(dr["Total"].ToString());
                obj.Nombre = dr["Nombre"].ToString();
                obj.RutaImagen = dr["RutaImagen"].ToString();
                obj.Ranking = Convert.ToInt32(dr["Ranking"].ToString());
                obj.Descripcion = dr["Descripcion"].ToString();
                obj.FechaCreacion = (dr["FechaCreacion"].ToString() != String.Empty ? (DateTime?)(dr["FechaCreacion"]) : null);  //Convert.ToDateTime(dr["FechaCreacion"].ToString());
                retVal.Add(obj);
            }


            return retVal;
        }

        public CursoOnlineEncuestaRespuesta ObtenerCursoOnlineEncuestaRespuesta(int matriculaCursoOnlineId)
        {
            CursoOnlineEncuestaRespuesta retVal = new CursoOnlineEncuestaRespuesta();

            DataTable dt = new DataTable();

            string cadenaConexion = Util.CadenaConexion;

            using (SqlConnection conexion = new SqlConnection(cadenaConexion))
            {
                conexion.Open();

                using (SqlCommand cmdSQL = new SqlCommand())
                {
                    cmdSQL.Connection = conexion;
                    cmdSQL.CommandType = CommandType.StoredProcedure;
                    cmdSQL.Parameters.Add("@matriculaCursoOnlineId", SqlDbType.Int).Value = matriculaCursoOnlineId;
                    cmdSQL.CommandText = "USP_CursoOnlineEncuestaRespuesta_SEL";

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

                retVal.CursoOnlineEncuestaRespuestaId = Convert.ToInt32(dr["CursoOnlineEncuestaRespuestaId"].ToString());
                retVal.MatriculaCursoOnlineId = Convert.ToInt32(dr["MatriculaCursoOnlineId"].ToString());
                retVal.Calificacion = Convert.ToInt32(dr["Calificacion"].ToString());
            }


            return retVal;
        }

        public VideoUnidadCursoOnline ObtenerVideoUnidadCursoOnlinePorId(int? videoUnidadCursoOnlineId)
        {
            VideoUnidadCursoOnline retVal = new VideoUnidadCursoOnline();

            DataTable dt = new DataTable();

            string cadenaConexion = Util.CadenaConexion;

            using (SqlConnection conexion = new SqlConnection(cadenaConexion))
            {
                conexion.Open();

                using (SqlCommand cmdSQL = new SqlCommand())
                {
                    cmdSQL.Connection = conexion;
                    cmdSQL.CommandType = CommandType.StoredProcedure;
                    cmdSQL.Parameters.Add("@VideoUnidadCursoOnlineId", SqlDbType.Int).Value = videoUnidadCursoOnlineId;
                    cmdSQL.CommandText = "USP_VideoUnidadCursoOnline_SEL";
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

            VideoUnidadCursoOnline obj = new VideoUnidadCursoOnline();
            foreach (DataRow dr in dt.Rows)
            {

                obj.VideoUnidadCursoOnlineId = (int)(dr["VideoUnidadCursoOnlineId"]);
                obj.UnidadCursoOnlineUnidadCursoOnlineId = (int)(dr["UnidadCursoOnlineUnidadCursoOnlineId"]);
                obj.Titulo = (string)(dr["Titulo"]);
                obj.CodigoYoutube = (string)(dr["CodigoYoutube"]);
                obj.Duracion = (string)(dr["Duracion"]);
                obj.Descripcion = (string)(dr["Descripcion"]);
                obj.Estado = (string)(dr["Estado"]);
                obj.Transcripcion = dr["Transcripcion"].ToString() != string.Empty ? (string)dr["Transcripcion"] : null;
                obj.Tipo = (string)(dr["Tipo"]);
                
                obj.FechaTransmision = dr["FechaTransmision"].ToString() != string.Empty ? (DateTime?)dr["FechaTransmision"] : null;


                retVal = obj;
            }

            return retVal;
        }

        public List<CursoOnline> ListarCursoOnline()
        {
            List<CursoOnline> retVal = new List<CursoOnline>();

            DataTable dt = new DataTable();

            string cadenaConexion = Util.CadenaConexion;

            using (SqlConnection conexion = new SqlConnection(cadenaConexion))
            {
                conexion.Open();

                using (SqlCommand cmdSQL = new SqlCommand())
                {
                    cmdSQL.Connection = conexion;
                    cmdSQL.CommandType = CommandType.StoredProcedure;
                    cmdSQL.CommandText = "USP_CursoOnline_LIS";
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
                CursoOnline obj = new CursoOnline();
                obj.CursoOnlineId = (int)(dr["CursoOnlineId"]);
                obj.Codigo = (string)(dr["Codigo"]);
                obj.Nombre = (string)(dr["Nombre"]);
                obj.CategoriaCursoOnlineId = (int)(dr["CategoriaCursoOnlineId"]);
                obj.Estado = (string)(dr["Estado"]);
                obj.Descripcion = (string)(dr["Descripcion"]);
                obj.NumPreguntasEvaluacion = (int)(dr["NumPreguntasEvaluacion"]);
                obj.TiempoEvaluacion = (int)(dr["TiempoEvaluacion"]);
                obj.PorcentajeAprobacion = (decimal)(dr["PorcentajeAprobacion"]);
                obj.RutaImagen = (dr["RutaImagen"].ToString() != string.Empty ? (string)(dr["RutaImagen"]) : null); //(string)(dr["RutaImagen"]);
                obj.TieneExamen = (string)(dr["TieneExamen"]);
                obj.AutoMatriculaHabilitada = (bool)(dr["AutoMatriculaHabilitada"]);
                obj.PermiteReinicioScorm = (dr["PermiteReinicioScorm"].ToString() != String.Empty ? (bool?)(dr["PermiteReinicioScorm"]) : null);       //(bool)(dr["PermiteReinicioScorm"]);
                obj.RutaBanner = (dr["RutaBanner"].ToString() != String.Empty ? (string)(dr["RutaBanner"]) : null); //(string)(dr["RutaBanner"]);
                obj.FechaCreacion = (dr["FechaCreacion"].ToString() != String.Empty ? (DateTime?)(dr["FechaCreacion"]) : null);
                obj.Ranking = (dr["Ranking"].ToString() != String.Empty ? (decimal?)(dr["Ranking"]) : null);//(decimal)(dr["Ranking"]);
                obj.NroRankings = (dr["NroRankings"].ToString() != String.Empty ? (int?)(dr["NroRankings"]) : null); //(int)(dr["NroRankings"]);
                obj.Orden = (dr["Orden"].ToString() != String.Empty ? (int?)(dr["Orden"]) : null);
                obj.DisponibilidadCurso = (dr["DisponibilidadCurso"].ToString() != String.Empty ? (int?)(dr["DisponibilidadCurso"]) : null);//(int)(dr["DisponibilidadCurso"]);
                retVal.Add(obj);
            }

            return retVal;

        }

        public List<VideoUnidadCursoOnline> ListarVideoUnidadCursoOnline(int unidadCursoOnlineId)
        {
            List<VideoUnidadCursoOnline> retVal = new List<VideoUnidadCursoOnline>();

            DataTable dt = new DataTable();

            string cadenaConexion = Util.CadenaConexion;

            using (SqlConnection conexion = new SqlConnection(cadenaConexion))
            {
                conexion.Open();

                using (SqlCommand cmdSQL = new SqlCommand())
                {
                    cmdSQL.Connection = conexion;
                    cmdSQL.CommandType = CommandType.StoredProcedure;
                    cmdSQL.CommandText = "USP_VideoUnidadCursoOnline_LIS";
                    cmdSQL.Parameters.Add("@UnidadCursoOnlineUnidadCursoOnlineId", SqlDbType.Int).Value = unidadCursoOnlineId;
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
                VideoUnidadCursoOnline obj = new VideoUnidadCursoOnline
                {
                    VideoUnidadCursoOnlineId = (int)(dr["VideoUnidadCursoOnlineId"]),
                    UnidadCursoOnlineUnidadCursoOnlineId = (int)(dr["UnidadCursoOnlineUnidadCursoOnlineId"]),
                    Titulo = (string)(dr["Titulo"]),
                    CodigoYoutube = (string)(dr["CodigoYoutube"]),
                    Duracion = (string)(dr["Duracion"]),
                    Descripcion = (string)(dr["Descripcion"]),
                    Estado = (string)(dr["Estado"]),
                    Transcripcion = dr["Transcripcion"].ToString() != string.Empty ? (string)(dr["Transcripcion"]) : null,
                    Tipo = dr["Tipo"].ToString() != string.Empty ? (string)(dr["Tipo"]) : null,
                    FechaTransmision = dr["FechaTransmision"].ToString() != string.Empty ? (DateTime?)dr["FechaTransmision"] : null
                };

                retVal.Add(obj);
            }

            return retVal;
        }

        public CursoOnlineEncuesta ObtenerEncuesta(int cursoonlineid) {
            CursoOnlineEncuesta retVal = new CursoOnlineEncuesta();

            DataTable dt = new DataTable();

            string cadenaConexion = Util.CadenaConexion;

            using (SqlConnection conexion = new SqlConnection(cadenaConexion))
            {
                conexion.Open();

                using (SqlCommand cmdSQL = new SqlCommand())
                {
                    cmdSQL.Connection = conexion;
                    cmdSQL.CommandType = CommandType.StoredProcedure;
                    cmdSQL.Parameters.Add("@cursoonlineid", SqlDbType.Int).Value = @cursoonlineid;
                    cmdSQL.CommandText = "USP_CursoOnlineEncuesta_LIS";
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

            CursoOnlineEncuesta obj = new CursoOnlineEncuesta();
            foreach (DataRow dr in dt.Rows)
            {
                
                obj.CursoOnlineEncuestaId = (int)(dr["CursoOnlineEncuestaId"]);
                obj.Descripcion = (string)(dr["Descripcion"]);
                obj.Nombre = (string)(dr["Nombre"]);
                obj.CursoOnlineId = (int)(dr["CursoOnlineId"]);
                obj.Activo = (Boolean)(dr["Activo"]);

                retVal = obj;
            }

            return retVal;
        }

        public List<DetalleCursoOnlineEncuesta> ObtenerEncuestaDetalle(int CursoOnlineEncuestaIdb)
        {
            List<DetalleCursoOnlineEncuesta> retVal = new List<DetalleCursoOnlineEncuesta>();

            DataTable dt = new DataTable();

            string cadenaConexion = Util.CadenaConexion;

            using (SqlConnection conexion = new SqlConnection(cadenaConexion))
            {
                conexion.Open();

                using (SqlCommand cmdSQL = new SqlCommand())
                {
                    cmdSQL.Connection = conexion;
                    cmdSQL.CommandType = CommandType.StoredProcedure;
                    cmdSQL.CommandText = "USP_CursoOnlineEncuestaDetalle_LIS";
                    cmdSQL.Parameters.Add("@CursoOnlineEncuestaIdb", SqlDbType.Int).Value = CursoOnlineEncuestaIdb;
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
                DetalleCursoOnlineEncuesta obj = new DetalleCursoOnlineEncuesta();
                obj.CursoOnlineEncuestaDetalleId = (int)(dr["CursoOnlineEncuestaDetalleId"]);
                obj.CursoOnlineEncuestaId = (int)(dr["CursoOnlineEncuestaId"]);
                obj.Pregunta = (string)(dr["Pregunta"]);
                obj.TipoPregunta = (int)(dr["TipoPregunta"]);
                obj.Opciones = (dr["Opciones"].ToString() != String.Empty ? (string)(dr["Opciones"]) : null);
                obj.Activo = (Boolean)(dr["Activo"]);
                
                retVal.Add(obj);
            }

            return retVal;

        }

        public List<CursoOnline> AddCursoOnline(int cursoOnlineMaestro)
        {
            List<CursoOnline> retVal = new List<CursoOnline>();

            DataTable dt = new DataTable();

            string cadenaConexion = Util.CadenaConexion;

            using (SqlConnection conexion = new SqlConnection(cadenaConexion))
            {
                conexion.Open();

                using (SqlCommand cmdSQL = new SqlCommand())
                {
                    cmdSQL.Connection = conexion;
                    cmdSQL.CommandType = CommandType.StoredProcedure;
                    cmdSQL.Parameters.Add("@CursoOnlineMaestro", SqlDbType.Int).Value = cursoOnlineMaestro;
                    cmdSQL.CommandText = "USP_AddCursoOnline_LIS";
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
                CursoOnline obj = new CursoOnline();
                obj.CursoOnlineId = (int)(dr["CursoOnlineId"]);
                obj.Codigo = (string)(dr["Codigo"]);
                obj.Nombre = (string)(dr["Nombre"]);
                obj.CategoriaCursoOnlineId = (int)(dr["CategoriaCursoOnlineId"]);
                obj.Estado = (string)(dr["Estado"]);
                obj.Descripcion = (string)(dr["Descripcion"]);
                obj.NumPreguntasEvaluacion = (int)(dr["NumPreguntasEvaluacion"]);
                obj.TiempoEvaluacion = (int)(dr["TiempoEvaluacion"]);
                obj.PorcentajeAprobacion = (decimal)(dr["PorcentajeAprobacion"]);
                obj.RutaImagen = (string)(dr["RutaImagen"]);
                obj.TieneExamen = (string)(dr["TieneExamen"]);
                obj.AutoMatriculaHabilitada = (bool)(dr["AutoMatriculaHabilitada"]);
                obj.PermiteReinicioScorm = (dr["PermiteReinicioScorm"].ToString() != String.Empty ? (bool?)(dr["PermiteReinicioScorm"]) : null);       //(bool)(dr["PermiteReinicioScorm"]);
                obj.RutaBanner = (dr["RutaBanner"].ToString() != String.Empty ? (string)(dr["RutaBanner"]) : null); //(string)(dr["RutaBanner"]);
                obj.FechaCreacion = (dr["FechaCreacion"].ToString() != String.Empty ? (DateTime?)(dr["FechaCreacion"]) : null);
                obj.Ranking = (dr["Ranking"].ToString() != String.Empty ? (decimal?)(dr["Ranking"]) : null);//(decimal)(dr["Ranking"]);
                obj.NroRankings = (dr["NroRankings"].ToString() != String.Empty ? (int?)(dr["NroRankings"]) : null); //(int)(dr["NroRankings"]);
                obj.Orden = (dr["Orden"].ToString() != String.Empty ? (int?)(dr["Orden"]) : null);
                obj.DisponibilidadCurso = (dr["DisponibilidadCurso"].ToString() != String.Empty ? (int?)(dr["DisponibilidadCurso"]) : null);//(int)(dr["DisponibilidadCurso"]);
                retVal.Add(obj);
            }

            return retVal;

        }

        public int ContarEvaluacionCursoOnlinePorUnidadCursoOnlineId(int unidadCursoOnlineId)
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
                    cmdSQL.CommandText = "USP_EvaluacionCursoOnline_LIS_Contar";
                    cmdSQL.Parameters.Add("@UnidadCursoOnlineId", SqlDbType.Int).Value = unidadCursoOnlineId;

                    lintResultado = Convert.ToInt32(cmdSQL.ExecuteScalar().ToString());

                }
                if (conexion.State == ConnectionState.Open)
                {
                    conexion.Close();
                }
            }

            return lintResultado;
        }

        public List<CursoOnline> ListarCursoOnlineDetalleCursoOnlineMaestro(int cursoOnlineId)
        {
            List<CursoOnline> retVal = new List<CursoOnline>();

            DataTable dt = new DataTable();

            string cadenaConexion = Util.CadenaConexion;

            using (SqlConnection conexion = new SqlConnection(cadenaConexion))
            {
                conexion.Open();

                using (SqlCommand cmdSQL = new SqlCommand())
                {
                    cmdSQL.Connection = conexion;
                    cmdSQL.CommandType = CommandType.StoredProcedure;
                    cmdSQL.CommandText = "USP_CursoOnlineDetalleCursoOnlineMaestro_SEL_PorCursoOnlineId";
                    cmdSQL.Parameters.Add("@CursoOnlineId", SqlDbType.Int).Value = cursoOnlineId;

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
                CursoOnline obj = new CursoOnline();
                obj.CursoOnlineId = (int)(dr["CursoOnlineId"]);
                obj.Codigo = (string)(dr["Codigo"]);
                obj.Nombre = (string)(dr["Nombre"]);
                obj.CategoriaCursoOnlineId = (int)(dr["CategoriaCursoOnlineId"]);
                obj.Estado = (string)(dr["Estado"]);
                obj.Descripcion = (string)(dr["Descripcion"]);
                obj.NumPreguntasEvaluacion = (int)(dr["NumPreguntasEvaluacion"]);
                obj.TiempoEvaluacion = (int)(dr["TiempoEvaluacion"]);
                obj.PorcentajeAprobacion = (decimal)(dr["PorcentajeAprobacion"]);
                obj.RutaImagen = (string)(dr["RutaImagen"]);
                obj.TieneExamen = (string)(dr["TieneExamen"]);
                obj.AutoMatriculaHabilitada = (bool)(dr["AutoMatriculaHabilitada"]);
                obj.PermiteReinicioScorm = (dr["PermiteReinicioScorm"].ToString() != String.Empty ? (bool?)(dr["PermiteReinicioScorm"]) : null);       //(bool)(dr["PermiteReinicioScorm"]);
                obj.RutaBanner = (dr["RutaBanner"].ToString() != String.Empty ? (string)(dr["RutaBanner"]) : null); //(string)(dr["RutaBanner"]);
                obj.FechaCreacion = (dr["FechaCreacion"].ToString() != String.Empty ? (DateTime?)(dr["FechaCreacion"]) : null);
                obj.Ranking = (dr["Ranking"].ToString() != String.Empty ? (decimal?)(dr["Ranking"]) : null);//(decimal)(dr["Ranking"]);
                obj.NroRankings = (dr["NroRankings"].ToString() != String.Empty ? (int?)(dr["NroRankings"]) : null); //(int)(dr["NroRankings"]);
                obj.Orden = (dr["Orden"].ToString() != String.Empty ? (int?)(dr["Orden"]) : null);
                obj.DisponibilidadCurso = (dr["DisponibilidadCurso"].ToString() != String.Empty ? (int?)(dr["DisponibilidadCurso"]) : null);//(int)(dr["DisponibilidadCurso"]);
                retVal.Add(obj);
            }

            return retVal;

        }

        public int InsertarCursoOnlineEncuestaRespuestaDetalle(CursoOnlineEncuestaRespuestaDetalle oCursoOnlineEncuestaRespuestaDetalle)
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
                    cmdSQL.CommandText = "USP_CursoOnlineEncuestaRespuestaDetalle_INS";
                    cmdSQL.Parameters.Add("@CursoOnlineEncuestaRespuestaId", SqlDbType.Int).Value = oCursoOnlineEncuestaRespuestaDetalle.CursoOnlineEncuestaRespuestaId;
                    cmdSQL.Parameters.Add("@Pregunta", SqlDbType.VarChar).Value = oCursoOnlineEncuestaRespuestaDetalle.Pregunta;
                    cmdSQL.Parameters.Add("@Respuesta", SqlDbType.VarChar).Value = oCursoOnlineEncuestaRespuestaDetalle.Respuesta;

                    lintResultado = Convert.ToInt32(cmdSQL.ExecuteScalar().ToString());

                }
                if (conexion.State == ConnectionState.Open)
                {
                    conexion.Close();
                }
            }

            return lintResultado;
        }

        public int InsertarCursoOnlineEncuestaRespuesta(CursoOnlineEncuestaRespuesta cursoOnlineEncuestaRespuesta)
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
                    cmdSQL.CommandText = "USP_CursoOnlineEncuestaRespuesta_INS";
                    cmdSQL.Parameters.Add("@MatriculaCursoOnlineId", SqlDbType.Int).Value = cursoOnlineEncuestaRespuesta.MatriculaCursoOnlineId;
                    cmdSQL.Parameters.Add("@Calificacion", SqlDbType.Int).Value = cursoOnlineEncuestaRespuesta.Calificacion;

                    lintResultado = Convert.ToInt32(cmdSQL.ExecuteScalar().ToString());

                }
                if (conexion.State == ConnectionState.Open)
                {
                    conexion.Close();
                }
            }

            return lintResultado;
        }

        public List<CursoOnline> ListarCursoOnlineDetalleCursoOnlineMaestroPorCursoOnlineMaestroId(int cursoOnlineMaestroId)
        {
            List<CursoOnline> retVal = new List<CursoOnline>();

            DataTable dt = new DataTable();

            string cadenaConexion = Util.CadenaConexion;

            using (SqlConnection conexion = new SqlConnection(cadenaConexion))
            {
                conexion.Open();

                using (SqlCommand cmdSQL = new SqlCommand())
                {
                    cmdSQL.Connection = conexion;
                    cmdSQL.CommandType = CommandType.StoredProcedure;
                    cmdSQL.CommandText = "USP_CursoOnlineDetalleCursoOnlineMaestro_SEL_PorCursoOnlineId";
                    cmdSQL.Parameters.Add("@CursoOnlineId", SqlDbType.Int).Value = cursoOnlineMaestroId;

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
                CursoOnline obj = new CursoOnline();
                obj.CursoOnlineId = (int)(dr["CursoOnlineId"]);
                obj.Codigo = (string)(dr["Codigo"]);
                obj.Nombre = (string)(dr["Nombre"]);
                obj.CategoriaCursoOnlineId = (int)(dr["CategoriaCursoOnlineId"]);
                obj.Estado = (string)(dr["Estado"]);
                obj.Descripcion = (string)(dr["Descripcion"]);
                obj.NumPreguntasEvaluacion = (int)(dr["NumPreguntasEvaluacion"]);
                obj.TiempoEvaluacion = (int)(dr["TiempoEvaluacion"]);
                obj.PorcentajeAprobacion = (decimal)(dr["PorcentajeAprobacion"]);
                obj.RutaImagen = (string)(dr["RutaImagen"]);
                obj.TieneExamen = (string)(dr["TieneExamen"]);
                obj.AutoMatriculaHabilitada = (bool)(dr["AutoMatriculaHabilitada"]);
                obj.PermiteReinicioScorm = (dr["PermiteReinicioScorm"].ToString() != String.Empty ? (bool?)(dr["PermiteReinicioScorm"]) : null);       //(bool)(dr["PermiteReinicioScorm"]);
                obj.RutaBanner = (dr["RutaBanner"].ToString() != String.Empty ? (string)(dr["RutaBanner"]) : null); //(string)(dr["RutaBanner"]);
                obj.FechaCreacion = (dr["FechaCreacion"].ToString() != String.Empty ? (DateTime?)(dr["FechaCreacion"]) : null);
                obj.Ranking = (dr["Ranking"].ToString() != String.Empty ? (decimal?)(dr["Ranking"]) : null);//(decimal)(dr["Ranking"]);
                obj.NroRankings = (dr["NroRankings"].ToString() != String.Empty ? (int?)(dr["NroRankings"]) : null); //(int)(dr["NroRankings"]);
                obj.Orden = (dr["Orden"].ToString() != String.Empty ? (int?)(dr["Orden"]) : null);
                obj.DisponibilidadCurso = (dr["DisponibilidadCurso"].ToString() != String.Empty ? (int?)(dr["DisponibilidadCurso"]) : null);//(int)(dr["DisponibilidadCurso"]);
                retVal.Add(obj);
            }

            return retVal;

        }

        public int ActualizarVideoUnidadCursoOnline(VideoUnidadCursoOnline videoUnidadCursoOnline)
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
                    cmdSQL.CommandText = "USP_VideoUnidadCursoOnline_UPD";
                    cmdSQL.Parameters.Add("@VideoUnidadCursoOnlineId", SqlDbType.Int).Value = videoUnidadCursoOnline.VideoUnidadCursoOnlineId;
                    cmdSQL.Parameters.Add("@UnidadCursoOnlineUnidadCursoOnlineId", SqlDbType.Int).Value = videoUnidadCursoOnline.UnidadCursoOnlineUnidadCursoOnlineId;
                    cmdSQL.Parameters.Add("@Titulo", SqlDbType.VarChar).Value = videoUnidadCursoOnline.Titulo;
                    cmdSQL.Parameters.Add("@CodigoYoutube", SqlDbType.VarChar).Value = videoUnidadCursoOnline.CodigoYoutube;
                    cmdSQL.Parameters.Add("@Duracion", SqlDbType.VarChar).Value = videoUnidadCursoOnline.Duracion;

                    cmdSQL.Parameters.Add("@Descripcion", SqlDbType.VarChar).Value = videoUnidadCursoOnline.Descripcion;
                    cmdSQL.Parameters.Add("@Estado", SqlDbType.VarChar).Value = videoUnidadCursoOnline.Estado;
                    cmdSQL.Parameters.Add("@Tipo", SqlDbType.VarChar).Value = videoUnidadCursoOnline.Tipo;
                    cmdSQL.Parameters.Add("@Transcripcion", SqlDbType.VarChar).Value = videoUnidadCursoOnline.Transcripcion;
                    cmdSQL.Parameters.Add("@FechaTransmision", SqlDbType.DateTime).Value = videoUnidadCursoOnline.FechaTransmision;

                    lintResultado = Convert.ToInt32(cmdSQL.ExecuteScalar().ToString());

                }
                if (conexion.State == ConnectionState.Open)
                {
                    conexion.Close();
                }
            }

            return lintResultado;
        }

        public int InsertarVideoUnidadCursoOnline(VideoUnidadCursoOnline videoUnidadCursoOnline)
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
                    cmdSQL.CommandText = "USP_VideoUnidadCursoOnline_INS";
                    cmdSQL.Parameters.Add("@UnidadCursoOnlineUnidadCursoOnlineId", SqlDbType.Int).Value = videoUnidadCursoOnline.UnidadCursoOnlineUnidadCursoOnlineId;
                    cmdSQL.Parameters.Add("@Titulo", SqlDbType.VarChar, -1).Value = videoUnidadCursoOnline.Titulo;
                    cmdSQL.Parameters.Add("@CodigoYoutube", SqlDbType.VarChar, -1).Value = videoUnidadCursoOnline.CodigoYoutube;
                    cmdSQL.Parameters.Add("@Duracion", SqlDbType.VarChar, -1).Value = videoUnidadCursoOnline.Duracion;

                    cmdSQL.Parameters.Add("@Descripcion", SqlDbType.VarChar, -1).Value = videoUnidadCursoOnline.Descripcion;
                    cmdSQL.Parameters.Add("@Estado", SqlDbType.VarChar, -1).Value = videoUnidadCursoOnline.Estado;

                    cmdSQL.Parameters.Add("@Tipo", SqlDbType.VarChar, -1).Value = videoUnidadCursoOnline.Tipo;
                    cmdSQL.Parameters.Add("@Transcripcion", SqlDbType.VarChar, -1).Value = videoUnidadCursoOnline.Transcripcion ;
                    cmdSQL.Parameters.Add("@FechaTransmision", SqlDbType.DateTime, -1).Value = videoUnidadCursoOnline.FechaTransmision;
                    



                    lintResultado = cmdSQL.ExecuteNonQuery();

                    //lintResultado = Convert.ToInt32(cmdSQL.ExecuteScalar().ToStri--ng());

                }
                if (conexion.State == ConnectionState.Open)
                {
                    conexion.Close();
                }
            }

            return lintResultado;
        }

        public List<TipoUnidad> ListarTipoUnidad()
        {
            List<TipoUnidad> retVal = new List<TipoUnidad>();

            DataTable dt = new DataTable();

            string cadenaConexion = Util.CadenaConexion;

            using (SqlConnection conexion = new SqlConnection(cadenaConexion))
            {
                conexion.Open();

                using (SqlCommand cmdSQL = new SqlCommand())
                {
                    cmdSQL.Connection = conexion;
                    cmdSQL.CommandType = CommandType.StoredProcedure;
                    cmdSQL.CommandText = "USP_TipoUnidad_LIS";
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
                TipoUnidad obj = new TipoUnidad();
                obj.TipoUnidadId = (int)(dr["TipoUnidadId"]);
                obj.Nombre = (string)(dr["Nombre"]);
                obj.Estado = (string)(dr["Estado"]);
                retVal.Add(obj);
            }

            return retVal;

        }

        public int InsertarCursoOnlineEncuestaDetalle(DetalleCursoOnlineEncuesta detalleCursoOnlineEncuesta)
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
                    cmdSQL.CommandText = "USP_CursoOnlineEncuestaDetalle_INS";
                    cmdSQL.Parameters.Add("@CursoOnlineEncuestaId", SqlDbType.Int).Value = detalleCursoOnlineEncuesta.CursoOnlineEncuestaId;
                    cmdSQL.Parameters.Add("@Pregunta", SqlDbType.VarChar).Value = detalleCursoOnlineEncuesta.Pregunta;
                    cmdSQL.Parameters.Add("@TipoPregunta", SqlDbType.Int).Value = detalleCursoOnlineEncuesta.TipoPregunta;
                    cmdSQL.Parameters.Add("@Opciones", SqlDbType.VarChar).Value = detalleCursoOnlineEncuesta.Opciones;
                    cmdSQL.Parameters.Add("@Activo", SqlDbType.Bit).Value = detalleCursoOnlineEncuesta.Activo;

                    lintResultado = Convert.ToInt32(cmdSQL.ExecuteScalar().ToString());

                }
                if (conexion.State == ConnectionState.Open)
                {
                    conexion.Close();
                }
            }

            return lintResultado;
        }

        public int ActualizarCursoOnlineEncuestaDetalle(DetalleCursoOnlineEncuesta detalleCursoOnlineEncuesta)
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
              


                    cmdSQL.CommandText = "USP_CursoOnlineEncuestaDetalle_UPD";
                    cmdSQL.Parameters.Add("@CursoOnlineEncuestaDetalleId", SqlDbType.Int).Value = detalleCursoOnlineEncuesta.CursoOnlineEncuestaDetalleId;
                    cmdSQL.Parameters.Add("@CursoOnlineEncuestaId", SqlDbType.Int).Value = detalleCursoOnlineEncuesta.CursoOnlineEncuestaId;
                    cmdSQL.Parameters.Add("@Pregunta", SqlDbType.VarChar).Value = detalleCursoOnlineEncuesta.Pregunta;
                    cmdSQL.Parameters.Add("@TipoPregunta", SqlDbType.Int).Value = detalleCursoOnlineEncuesta.TipoPregunta;
                    cmdSQL.Parameters.Add("@Opciones", SqlDbType.VarChar).Value = detalleCursoOnlineEncuesta.Opciones;
                    cmdSQL.Parameters.Add("@Activo", SqlDbType.Bit).Value = detalleCursoOnlineEncuesta.Activo;


                    lintResultado = Convert.ToInt32(cmdSQL.ExecuteScalar().ToString());

                }
                if (conexion.State == ConnectionState.Open)
                {
                    conexion.Close();
                }
            }

            return lintResultado;
        }

        public List<DetalleCursoOnlineMaestro> ListarDetallCursoOnlineMaestroCursoOnline()
        {
            List<DetalleCursoOnlineMaestro> retVal = new List<DetalleCursoOnlineMaestro>();

            DataTable dt = new DataTable();

            string cadenaConexion = Util.CadenaConexion;

            using (SqlConnection conexion = new SqlConnection(cadenaConexion))
            {
                conexion.Open();

                using (SqlCommand cmdSQL = new SqlCommand())
                {
                    cmdSQL.Connection = conexion;
                    cmdSQL.CommandType = CommandType.StoredProcedure;
                    cmdSQL.CommandText = "USP_DetalleCursoOnlineMaestroCursoOnline_LIS";
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
                DetalleCursoOnlineMaestro obj = new DetalleCursoOnlineMaestro();

                obj.CursoOnlineId = (int)(dr["CursoOnlineId"]);
                obj.CursoOnlineMaestroId = (int)(dr["CursoOnlineMaestroId"]);
                obj.DetalleCursoOnlineId = (int)(dr["DetalleCursoOnlineId"]);
                obj.Orden = (int)(dr["Orden"]);
                obj.Nombre = (string)(dr["Nombre"]);
                obj.Estado = (string)(dr["Estado"]);


                obj.CursoOnline = ObtenerCursoOnlinePorId(obj.CursoOnlineId);
                obj.CursoOnlineMaestro = ObtenerCursoOnlineMaestroPorId(obj.DetalleCursoOnlineId);

                retVal.Add(obj);
            }

            return retVal;
        }

        public int InsertarRespuestaSeleccionadaEvaluacionCursoOnline(RespuestaSeleccionadaEvaluacionCursoOnline respuestaSeleccionadaEvaluacionCursoOnline)
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
                    cmdSQL.CommandText = "USP_RespuestaSeleccionadaEvaluacionCursoOnline_INS";

                    cmdSQL.Parameters.Add("@PreguntaEvaluacionCursoOnlineId", SqlDbType.Int).Value = respuestaSeleccionadaEvaluacionCursoOnline.PreguntaEvaluacionCursoOnlineId;
                    cmdSQL.Parameters.Add("@RespuestaCursoOnlineId", SqlDbType.Int).Value = respuestaSeleccionadaEvaluacionCursoOnline.RespuestaCursoOnlineId;
                    cmdSQL.Parameters.Add("@OrdenSeleccionado", SqlDbType.Int).Value = respuestaSeleccionadaEvaluacionCursoOnline.OrdenSeleccionado;

                    lintResultado = Convert.ToInt32(cmdSQL.ExecuteScalar().ToString());

                }
                if (conexion.State == ConnectionState.Open)
                {
                    conexion.Close();
                }
            }

            return lintResultado;
        }

        public int EliminarRespuestaSeleccionadaEvaluacionCursoOnlinePorEvaluacionCursoOnlineId(int evaluacionCursoOnlineId)
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
                    cmdSQL.CommandText = "USP_RespuestaSeleccionadaEvaluacionCursoOnline_DEL_PorEvaluacionCursoOnlineId";


                    cmdSQL.Parameters.Add("@EvaluacionCursoOnlineId", SqlDbType.Int).Value = evaluacionCursoOnlineId;

                    lintResultado = cmdSQL.ExecuteNonQuery();

                }
                if (conexion.State == ConnectionState.Open)
                {
                    conexion.Close();
                }
            }

            return lintResultado;
        }

        public List<DetalleCursoOnlineMaestro> ListarDetalleCursoOnlineMaestroPorCursoOnlineId(int cursoOnlineId)
        {
            List<DetalleCursoOnlineMaestro> retVal = new List<DetalleCursoOnlineMaestro>();

            DataTable dt = new DataTable();

            string cadenaConexion = Util.CadenaConexion;

            using (SqlConnection conexion = new SqlConnection(cadenaConexion))
            {
                conexion.Open();

                using (SqlCommand cmdSQL = new SqlCommand())
                {
                    cmdSQL.Connection = conexion;
                    cmdSQL.CommandType = CommandType.StoredProcedure;
                    cmdSQL.CommandText = "USP_DetalleCursoOnlineMaestroCursoOnline_LIS_PorCursoOnlineId";

                    cmdSQL.Parameters.Add("@CursoOnlineId", SqlDbType.Int).Value = cursoOnlineId;

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
                DetalleCursoOnlineMaestro obj = new DetalleCursoOnlineMaestro();

                obj.CursoOnlineId = (int)(dr["CursoOnlineId"]);
                obj.CursoOnlineMaestroId = (int)(dr["CursoOnlineMaestroId"]);
                obj.DetalleCursoOnlineId = (int)(dr["DetalleCursoOnlineId"]);
                obj.Orden = (int)(dr["Orden"]);
                obj.Nombre = (string)(dr["Nombre"]);
                obj.Estado = (string)(dr["Estado"]);


                obj.CursoOnline = ObtenerCursoOnlinePorId(obj.CursoOnlineId);
                obj.CursoOnlineMaestro = ObtenerCursoOnlineMaestroPorId(obj.DetalleCursoOnlineId);

                retVal.Add(obj);
            }

            return retVal;
        }

        public CursoOnlineMaestro ObtenerCursoOnlineMaestroPorId(int? CursoOnlineMaestroId = 0)
        {
            CursoOnlineMaestro retVal = new CursoOnlineMaestro();

            DataTable dt = new DataTable();

            string cadenaConexion = Util.CadenaConexion;

            using (SqlConnection conexion = new SqlConnection(cadenaConexion))
            {
                conexion.Open();

                using (SqlCommand cmdSQL = new SqlCommand())
                {
                    cmdSQL.Connection = conexion;
                    cmdSQL.CommandType = CommandType.StoredProcedure;
                    cmdSQL.CommandText = "USP_CursoOnlineMaestro_LIS_PorId";

                    cmdSQL.Parameters.Add("@CursoOnlineMaestroId", SqlDbType.Int).Value = CursoOnlineMaestroId;


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
                retVal.CursoOnlineMaestroId = (int)(dr["CursoOnlineMaestroId"]);
                retVal.Codigo = (string)(dr["Codigo"]);
                retVal.Nombre = (string)(dr["Nombre"]);
                retVal.CategoriaCursoOnlineId = (int)(dr["CategoriaCursoOnlineId"]);
                retVal.Estado = (string)(dr["Estado"]);
                retVal.Descripcion = (string)(dr["Descripcion"]);
                retVal.NumPreguntasEvaluacion = (int)(dr["NumPreguntasEvaluacion"]);
                retVal.TiempoEvaluacion = (int)(dr["TiempoEvaluacion"]);
                retVal.PorcentajeAprobacion = (decimal)(dr["PorcentajeAprobacion"]);
                retVal.RutaImagen = (string)(dr["RutaImagen"]);
                retVal.TieneExamen = (string)(dr["TieneExamen"]);
                retVal.AutoMatriculaHabilitada = (bool)(dr["AutoMatriculaHabilitada"]);
                retVal.PermiteReinicioScorm = (dr["PermiteReinicioScorm"].ToString() != String.Empty ? (bool?)(dr["PermiteReinicioScorm"]) : null);       //(bool)(dr["PermiteReinicioScorm"]);
                retVal.RutaBanner = (dr["RutaBanner"].ToString() != String.Empty ? (string)(dr["RutaBanner"]) : null); //(string)(dr["RutaBanner"]);
                retVal.FechaCreacion = (DateTime)(dr["FechaCreacion"]);
                retVal.Ranking = (dr["Ranking"].ToString() != String.Empty ? (decimal?)(dr["Ranking"]) : null);//(decimal)(dr["Ranking"]);
                retVal.NroRankings = (dr["NroRankings"].ToString() != String.Empty ? (int?)(dr["NroRankings"]) : null); //(int)(dr["NroRankings"]);
                retVal.Orden = (dr["Orden"].ToString() != String.Empty ? (int?)(dr["Orden"]) : null);  //(int)(dr["Orden"]);
                retVal.CantidadCursos = (dr["CantidadCursos"].ToString() != String.Empty ? (int?)(dr["CantidadCursos"]) : null);//(int)(dr["DisponibilidadCurso"]);
            }

            return retVal;
        }

        public List<UnidadCursoOnline> ListarUnidadCursoOnlinePorCursoOnlineId(int cursoOnlineId)
        {
            List<UnidadCursoOnline> retVal = new List<UnidadCursoOnline>();

            DataTable dt = new DataTable();

            string cadenaConexion = Util.CadenaConexion;

            using (SqlConnection conexion = new SqlConnection(cadenaConexion))
            {
                conexion.Open();

                using (SqlCommand cmdSQL = new SqlCommand())
                {
                    cmdSQL.Connection = conexion;
                    cmdSQL.CommandType = CommandType.StoredProcedure;
                    cmdSQL.CommandText = "USP_UnidadCursoOnline_LIS_PorCursoOnlineId";

                    cmdSQL.Parameters.Add("@CursoOnlineId", SqlDbType.Int).Value = cursoOnlineId;


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
                UnidadCursoOnline obj = new UnidadCursoOnline();
                obj.UnidadCursoOnlineId = (int)(dr["UnidadCursoOnlineId"]);
                obj.CursoOnlineId = (int)(dr["CursoOnlineId"]);
                //obj.TipoUnidadId = (int)(dr["TipoUnidadId"]);
                obj.TipoUnidadId = (dr["TipoUnidadId"].ToString() != String.Empty ? (int?)(dr["TipoUnidadId"]) : null);
                obj.UnidadCursoOnlinePadreId = (dr["UnidadCursoOnlinePadreId"].ToString() != String.Empty ? (int?)(dr["UnidadCursoOnlinePadreId"]) : null);

                obj.Video = (dr["Video"].ToString() != String.Empty ? (int?)(dr["Video"]) : null);
                obj.Vivo = (dr["Vivo"].ToString() != String.Empty ? (int?)(dr["Vivo"]) : null);

                obj.Nombre = (string)(dr["Nombre"]);
                obj.Descripcion = (dr["Descripcion"].ToString() != String.Empty ? (string)(dr["Descripcion"]) : null);
                obj.GUID = (string)(dr["GUID"]);
                obj.RutaArchivoOriginal = (dr["RutaArchivoOriginal"].ToString() != String.Empty ? (string)(dr["RutaArchivoOriginal"]) : null);
                obj.RutaArchivoInicio = (dr["RutaArchivoInicio"].ToString() != String.Empty ? (string)(dr["RutaArchivoInicio"]) : null);
                obj.RutaWeb = (dr["RutaWeb"].ToString() != String.Empty ? (string)(dr["RutaWeb"]) : null);
                obj.Orden = (int)(dr["Orden"]);
                obj.Estado = (string)(dr["Estado"]);
                obj.TiempoPermanencia = (int)(dr["TiempoPermanencia"]);
                obj.AnchoContenedor = (dr["AnchoContenedor"].ToString() != String.Empty ? (int?)(dr["AnchoContenedor"]) : null);
                obj.AltoContenedor = (dr["AltoContenedor"].ToString() != String.Empty ? (int?)(dr["AltoContenedor"]) : null);
                obj.TipoUnidad = (string)(dr["TipoUnidad"]);
                obj.RutaArchivoAdicional = (dr["RutaArchivoAdicional"].ToString() != String.Empty ? (string)(dr["RutaArchivoAdicional"]) : null);
                obj.ExtensionArchivoAdicional = (dr["ExtensionArchivoAdicional"].ToString() != String.Empty ? (string)(dr["ExtensionArchivoAdicional"]) : null);
                obj.TipoContenidoArchivoAdicional = (dr["TipoContenidoArchivoAdicional"].ToString() != String.Empty ? (string)(dr["TipoContenidoArchivoAdicional"]) : null);
                retVal.Add(obj);
            }

            return retVal;
        }

        public UnidadCursoOnline ObtenerUnidadCursoOnlinePorId(int unidadCursoOnlineId)
        {
            UnidadCursoOnline retVal = new UnidadCursoOnline();

            DataTable dt = new DataTable();

            string cadenaConexion = Util.CadenaConexion;

            using (SqlConnection conexion = new SqlConnection(cadenaConexion))
            {
                conexion.Open();

                using (SqlCommand cmdSQL = new SqlCommand())
                {
                    cmdSQL.Connection = conexion;
                    cmdSQL.CommandType = CommandType.StoredProcedure;
                    cmdSQL.CommandText = "USP_UnidadCursoOnline_SEL";

                    cmdSQL.Parameters.Add("@UnidadCursoOnlineId", SqlDbType.Int).Value = unidadCursoOnlineId;


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
                UnidadCursoOnline obj = new UnidadCursoOnline();
                obj.UnidadCursoOnlineId = (int)(dr["UnidadCursoOnlineId"]);
                obj.CursoOnlineId = (int)(dr["CursoOnlineId"]);
                obj.TipoUnidadId = (int)(dr["TipoUnidadId"]);
                obj.UnidadCursoOnlinePadreId = (dr["UnidadCursoOnlinePadreId"].ToString() != String.Empty ? (int?)(dr["UnidadCursoOnlinePadreId"]) : null);
                obj.Nombre = (string)(dr["Nombre"]);
                obj.Descripcion = (dr["Descripcion"].ToString() != String.Empty ? (string)(dr["Descripcion"]) : null);
                obj.GUID = (string)(dr["GUID"]);
                obj.RutaArchivoOriginal = (dr["RutaArchivoOriginal"].ToString() != String.Empty ? (string)(dr["RutaArchivoOriginal"]) : null);
                obj.RutaArchivoInicio = (dr["RutaArchivoInicio"].ToString() != String.Empty ? (string)(dr["RutaArchivoInicio"]) : null);
                obj.RutaWeb = (dr["RutaWeb"].ToString() != String.Empty ? (string)(dr["RutaWeb"]) : null);
                obj.Orden = (int)(dr["Orden"]);
                obj.Estado = (string)(dr["Estado"]);
                obj.TiempoPermanencia = (int)(dr["TiempoPermanencia"]);
                obj.AnchoContenedor = (dr["AnchoContenedor"].ToString() != String.Empty ? (int?)(dr["AnchoContenedor"]) : null);
                obj.AltoContenedor = (dr["AltoContenedor"].ToString() != String.Empty ? (int?)(dr["AltoContenedor"]) : null);
                obj.TipoUnidad = (string)(dr["TipoUnidad"]);
                obj.RutaArchivoAdicional = (dr["RutaArchivoAdicional"].ToString() != String.Empty ? (string)(dr["RutaArchivoAdicional"]) : null);
                obj.ExtensionArchivoAdicional = (dr["ExtensionArchivoAdicional"].ToString() != String.Empty ? (string)(dr["ExtensionArchivoAdicional"]) : null);
                obj.TipoContenidoArchivoAdicional = (dr["TipoContenidoArchivoAdicional"].ToString() != String.Empty ? (string)(dr["TipoContenidoArchivoAdicional"]) : null);

                retVal = obj;
            }

            return retVal;
        }

        public List<TareaUnidadCursoOnline> ListarTareaUnidadCursoOnlinePorUnidadCursoOnlineId(int unidadCursoOnlineId, int alumnoId)
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
                    cmdSql.CommandText = "USP_TareaUnidadCursoOnline_LIS_PorUnidadCursoOnlineId";

                    cmdSql.Parameters.Add("@UnidadCursoOnlineId", SqlDbType.Int).Value = unidadCursoOnlineId;
                    cmdSql.Parameters.Add("@AlumnoId", SqlDbType.Int).Value = alumnoId;

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
                select new TareaUnidadCursoOnline
                {
                    TareaUnidadCursoOnlineId = (int)dr["TareaUnidadCursoOnlineId"],
                    UnidadCursoOnlineId = (int)dr["UnidadCursoOnlineId"],
                    AlumnoId = (int)dr["AlumnoId"],
                    RutaTarea = (string)dr["RutaTarea"],
                    Nota = dr["Nota"].ToString() != string.Empty ? (int?)dr["Nota"] : null,
                    ComentarioProfesor = dr["ComentarioProfesor"].ToString() != string.Empty
                        ? dr["ComentarioProfesor"].ToString()
                        : null,
                    Estado = dr["Estado"].ToString() != string.Empty ? dr["Estado"].ToString() : null,
                    FechaAprobacion = dr["FechaAprobacion"].ToString() != string.Empty
                        ? (DateTime?)dr["FechaAprobacion"]
                        : null,
                    FechaEnvio = (DateTime)dr["FechaEnvio"],
                    RutaTareaCorregido = dr["RutaTareaCorregido"].ToString() != string.Empty
                        ? dr["RutaTareaCorregido"].ToString()
                        : null
                }).ToList();
        }

        public int InsertarTareaUnidadCursoOnline(TareaUnidadCursoOnline tarea)
        {
            int lintResultado = 0;

            string cadenaConexion = Util.CadenaConexion;

            using (SqlConnection conexion = new SqlConnection(cadenaConexion))
            {
                conexion.Open();

                using (SqlCommand cmdSql = new SqlCommand())
                {
                    cmdSql.Connection = conexion;
                    cmdSql.CommandType = CommandType.StoredProcedure;
                    cmdSql.CommandText = "USP_TareaUnidadCursoOnline_INS";

                    cmdSql.Parameters.Add("@UnidadCursoOnlineId", SqlDbType.Int).Value = tarea.UnidadCursoOnlineId;
                    cmdSql.Parameters.Add("@AlumnoId", SqlDbType.Int).Value = tarea.AlumnoId;
                    cmdSql.Parameters.Add("@RutaTarea", SqlDbType.VarChar).Value = tarea.RutaTarea;
                    cmdSql.Parameters.Add("@Nota", SqlDbType.Int).Value = tarea.Nota;
                    cmdSql.Parameters.Add("@ComentarioProfesor", SqlDbType.VarChar).Value = tarea.ComentarioProfesor;
                    cmdSql.Parameters.Add("@Estado", SqlDbType.VarChar,3).Value = tarea.Estado;
                    cmdSql.Parameters.Add("@FechaAprobacion", SqlDbType.DateTime).Value = tarea.FechaAprobacion;
                    cmdSql.Parameters.Add("@FechaEnvio", SqlDbType.DateTime).Value = tarea.FechaEnvio;
                    cmdSql.Parameters.Add("@RutaTareaCorregido", SqlDbType.VarChar).Value = tarea.RutaTareaCorregido;

                    lintResultado = Convert.ToInt32(cmdSql.ExecuteScalar().ToString());
                }

                if (conexion.State == ConnectionState.Open)
                {
                    conexion.Close();
                }
            }

            return lintResultado;
        }

        public int InsertarMatricualCursoOnline(MatriculaCursoOnline matricula)
        {
            int lintResultado = 0;

            string cadenaConexion = Util.CadenaConexion;

            using (SqlConnection conexion = new SqlConnection(cadenaConexion))
            {
                conexion.Open();

                using (SqlCommand cmdSql = new SqlCommand())
                {
                    cmdSql.Connection = conexion;
                    cmdSql.CommandType = CommandType.StoredProcedure;
                    cmdSql.CommandText = "USP_MatriculaCursoOnline_INS";

                    cmdSql.Parameters.Add("@UsuarioId", SqlDbType.Int).Value = matricula.UsuarioId;
                    cmdSql.Parameters.Add("@CursoOnlineId", SqlDbType.Int).Value = matricula.CursoOnlineId;
                    cmdSql.Parameters.Add("@FechaMatricula", SqlDbType.DateTime).Value = matricula.FechaMatricula;
                    cmdSql.Parameters.Add("@FechaCompletado", SqlDbType.DateTime).Value = matricula.FechaCompletado;
                    cmdSql.Parameters.Add("@FechaAprobado", SqlDbType.DateTime).Value = matricula.FechaAprobado;
                    cmdSql.Parameters.Add("@Estado", SqlDbType.VarChar, -1).Value = matricula.Estado;
                    cmdSql.Parameters.Add("@PorcentajeAvance", SqlDbType.Decimal).Value = matricula.PorcentajeAvance;
                    cmdSql.Parameters.Add("@Nota", SqlDbType.Decimal).Value = matricula.Nota;
                    cmdSql.Parameters.Add("@PorcentajeObtenido", SqlDbType.Decimal).Value = matricula.PorcentajeObtenido;
                    cmdSql.Parameters.Add("@TotalUnidadesCursoOnline", SqlDbType.Int).Value = matricula.TotalUnidadesCursoOnline;
                    cmdSql.Parameters.Add("@Grupo", SqlDbType.VarChar, -1).Value = matricula.Grupo;
                    cmdSql.Parameters.Add("@Ranking", SqlDbType.Int).Value = matricula.Ranking;

                    lintResultado = Convert.ToInt32(cmdSql.ExecuteScalar().ToString());
                }

                if (conexion.State == ConnectionState.Open)
                {
                    conexion.Close();
                }
            }

            return lintResultado;
        }

        public List<AvanceMatriculaCursoOnline> ListarAvanceMatriculaCursoOnlinePorMatriculaCursoOnlineId(int matriculaCursoOnlineId)
        {
            List<AvanceMatriculaCursoOnline> retVal = new List<AvanceMatriculaCursoOnline>();

            DataTable dt = new DataTable();

            string cadenaConexion = Util.CadenaConexion;

            using (SqlConnection conexion = new SqlConnection(cadenaConexion))
            {
                conexion.Open();

                using (SqlCommand cmdSQL = new SqlCommand())
                {
                    cmdSQL.Connection = conexion;
                    cmdSQL.CommandType = CommandType.StoredProcedure;
                    cmdSQL.CommandText = "USP_AvanceMatriculaCursoOnline_LIS_PorMatriculaCursoOnlineId"; //USP_AvanceMatriculaCursoOnline_LIS_PorMatriculaCursoOnlineId

                    cmdSQL.Parameters.Add("@MatriculaCursoOnlineId", SqlDbType.Int).Value = matriculaCursoOnlineId;


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
                AvanceMatriculaCursoOnline obj = new AvanceMatriculaCursoOnline();
                obj.AvanceMatriculaCursoOnlineId = (int)(dr["AvanceMatriculaCursoOnlineId"]);
                obj.MatriculaCursoOnlineId = (int)(dr["MatriculaCursoOnlineId"]);
                obj.UnidadCursoOnlineId = (int)(dr["UnidadCursoOnlineId"]);
                obj.FechaInicio = (DateTime)(dr["FechaInicio"]);
                obj.FechaFin = (dr["FechaFin"].ToString() != String.Empty ? (DateTime?)(dr["FechaFin"]) : null);

                retVal.Add(obj);
            }

            return retVal;
        }

        public List<AvanceMatriculaCursoOnline> ListarAvanceMatriculaCursoOnlinePorUnidadCursoOnlineId(int unidadCursoOnlineId)
        {
            List<AvanceMatriculaCursoOnline> retVal = new List<AvanceMatriculaCursoOnline>();

            DataTable dt = new DataTable();

            string cadenaConexion = Util.CadenaConexion;

            using (SqlConnection conexion = new SqlConnection(cadenaConexion))
            {
                conexion.Open();

                using (SqlCommand cmdSQL = new SqlCommand())
                {
                    cmdSQL.Connection = conexion;
                    cmdSQL.CommandType = CommandType.StoredProcedure;
                    cmdSQL.CommandText = "USP_AvanceMatriculaCursoOnline_LIS_PorUnidadCursoOnlineId"; //USP_AvanceMatriculaCursoOnline_LIS_PorMatriculaCursoOnlineId

                    cmdSQL.Parameters.Add("@UnidadCursoOnlineId", SqlDbType.Int).Value = unidadCursoOnlineId;


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
                AvanceMatriculaCursoOnline obj = new AvanceMatriculaCursoOnline();
                obj.AvanceMatriculaCursoOnlineId = (int)(dr["AvanceMatriculaCursoOnlineId"]);
                obj.MatriculaCursoOnlineId = (int)(dr["MatriculaCursoOnlineId"]);
                obj.UnidadCursoOnlineId = (int)(dr["UnidadCursoOnlineId"]);
                obj.FechaInicio = (DateTime)(dr["FechaInicio"]);
                obj.FechaFin = (dr["FechaFin"].ToString() != String.Empty ? (DateTime?)(dr["FechaFin"]) : null);

                retVal.Add(obj);
            }

            return retVal;
        }

        public List<EvaluacionCursoOnline> ListarEvaluacionCursoOnlinePorCursoOnlineId(int cursoOnlineId)
        {
            List<EvaluacionCursoOnline> retVal = new List<EvaluacionCursoOnline>();

            DataTable dt = new DataTable();

            string cadenaConexion = Util.CadenaConexion;

            using (SqlConnection conexion = new SqlConnection(cadenaConexion))
            {
                conexion.Open();

                using (SqlCommand cmdSQL = new SqlCommand())
                {
                    cmdSQL.Connection = conexion;
                    cmdSQL.CommandType = CommandType.StoredProcedure;
                    cmdSQL.CommandText = "USP_EvaluacionCursoOnline_LIS_PorCursoOnlineId";

                    cmdSQL.Parameters.Add("@CursoOnlineId", SqlDbType.Int).Value = cursoOnlineId;


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
                EvaluacionCursoOnline obj = new EvaluacionCursoOnline();
                obj.EvaluacionCursoOnlineId = (int)(dr["EvaluacionCursoOnlineId"]);
                obj.MatriculaCursoOnlineId = (int)(dr["MatriculaCursoOnlineId"]);
                obj.UnidadCursoOnlineId = (int)(dr["UnidadCursoOnlineId"]);
                obj.FechaInicio = (DateTime)(dr["FechaInicio"]);
                obj.FechaFin = (dr["FechaFin"].ToString() != String.Empty ? (DateTime?)(dr["FechaFin"]) : null); //(DateTime)(dr["FechaFin"]);
                obj.Nota = (dr["Nota"].ToString() != String.Empty ? (decimal?)(dr["Nota"]) : null);  //(decimal)(dr["Nota"]);
                obj.PorcentajeAprobacion = (decimal)(dr["PorcentajeAprobacion"]);
                obj.PuntajeTotal = (decimal)(dr["PuntajeTotal"]);
                obj.PuntajeObtenido = (dr["PuntajeObtenido"].ToString() != String.Empty ? (decimal?)(dr["PuntajeObtenido"]) : null);  //(decimal)(dr["PuntajeObtenido"]);

                retVal.Add(obj);
            }

            return retVal;
        }

        public List<EvaluacionCursoOnline> ListarEvaluacionCursoOnlinePorUsuarioId(int usuarioId)
        {
            List<EvaluacionCursoOnline> retVal = new List<EvaluacionCursoOnline>();

            DataTable dt = new DataTable();

            string cadenaConexion = Util.CadenaConexion;

            using (SqlConnection conexion = new SqlConnection(cadenaConexion))
            {
                conexion.Open();

                using (SqlCommand cmdSQL = new SqlCommand())
                {
                    cmdSQL.Connection = conexion;
                    cmdSQL.CommandType = CommandType.StoredProcedure;
                    cmdSQL.CommandText = "USP_EvaluacionCursoOnline_LIS_PorUsuarioId";

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
                EvaluacionCursoOnline obj = new EvaluacionCursoOnline();
                obj.EvaluacionCursoOnlineId = (int)(dr["EvaluacionCursoOnlineId"]);
                obj.MatriculaCursoOnlineId = (int)(dr["MatriculaCursoOnlineId"]);
                obj.UnidadCursoOnlineId = (int)(dr["UnidadCursoOnlineId"]);
                obj.FechaInicio = (DateTime)(dr["FechaInicio"]);
                obj.FechaFin = (dr["FechaFin"].ToString() != String.Empty ? (DateTime?)(dr["FechaFin"]) : null); //(DateTime)(dr["FechaFin"]);
                obj.Nota = (dr["Nota"].ToString() != String.Empty ? (decimal?)(dr["Nota"]) : null);  //(decimal)(dr["Nota"]);
                obj.PorcentajeAprobacion = (decimal)(dr["PorcentajeAprobacion"]);
                obj.PuntajeTotal = (decimal)(dr["PuntajeTotal"]);
                obj.PuntajeObtenido = (dr["PuntajeObtenido"].ToString() != String.Empty ? (decimal?)(dr["PuntajeObtenido"]) : null);  //(decimal)(dr["PuntajeObtenido"]);

                retVal.Add(obj);
            }

            return retVal;
        }

        public EvaluacionCursoOnline ObtenerEvaluacionCursoOnlinePorId(int evaluacionCursoOnlineId)
        {
            EvaluacionCursoOnline retVal = new EvaluacionCursoOnline();

            DataTable dt = new DataTable();

            string cadenaConexion = Util.CadenaConexion;

            using (SqlConnection conexion = new SqlConnection(cadenaConexion))
            {
                conexion.Open();

                using (SqlCommand cmdSQL = new SqlCommand())
                {
                    cmdSQL.Connection = conexion;
                    cmdSQL.CommandType = CommandType.StoredProcedure;
                    cmdSQL.CommandText = "USP_EvaluacionCursoOnline_SEL_PorEvaluacionCursoOnlineId";

                    cmdSQL.Parameters.Add("@EvaluacionCursoOnlineId", SqlDbType.Int).Value = evaluacionCursoOnlineId;


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
                retVal.EvaluacionCursoOnlineId = (int)(dr["EvaluacionCursoOnlineId"]);
                retVal.MatriculaCursoOnlineId = (int)(dr["MatriculaCursoOnlineId"]);
                retVal.UnidadCursoOnlineId = (int)(dr["UnidadCursoOnlineId"]);
                retVal.FechaInicio = (DateTime)(dr["FechaInicio"]);
                retVal.FechaFin = (dr["FechaFin"].ToString() != String.Empty ? (DateTime?)(dr["FechaFin"]) : null); //(DateTime)(dr["FechaFin"]);
                retVal.Nota = (dr["Nota"].ToString() != String.Empty ? (decimal?)(dr["Nota"]) : null);  //(decimal)(dr["Nota"]);
                retVal.PorcentajeAprobacion = (decimal)(dr["PorcentajeAprobacion"]);
                retVal.PuntajeTotal = (decimal)(dr["PuntajeTotal"]);
                retVal.PuntajeObtenido = (dr["PuntajeObtenido"].ToString() != String.Empty ? (decimal?)(dr["PuntajeObtenido"]) : null);  //(decimal)(dr["PuntajeObtenido"]);

            }

            return retVal;
        }

        public List<MatriculaCursoOnline> ListarMatriculaCursoOnlinePorCursoOnlineIdPorUsuarioId(int cursoOnlineId, int usuarioId)
        {
            List<MatriculaCursoOnline> retVal = new List<MatriculaCursoOnline>();

            DataTable dt = new DataTable();

            string cadenaConexion = Util.CadenaConexion;

            using (SqlConnection conexion = new SqlConnection(cadenaConexion))
            {
                conexion.Open();

                using (SqlCommand cmdSQL = new SqlCommand())
                {
                    cmdSQL.Connection = conexion;
                    cmdSQL.CommandType = CommandType.StoredProcedure;
                    cmdSQL.CommandText = "USP_MatriculaCursoOnline_LIS_PorIdPorUsuarioId";

                    cmdSQL.Parameters.Add("@CursoOnlineId", SqlDbType.Int).Value = cursoOnlineId;
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
                MatriculaCursoOnline obj = new MatriculaCursoOnline();
                obj.MatriculaCursoOnlineId = (int)(dr["MatriculaCursoOnlineId"]);
                obj.UsuarioId = (int)(dr["UsuarioId"]);
                obj.CursoOnlineId = (int)(dr["CursoOnlineId"]);
                obj.FechaMatricula = (DateTime)(dr["FechaMatricula"]);
                obj.FechaCompletado = (dr["FechaCompletado"].ToString() != String.Empty ? (DateTime?)(dr["FechaCompletado"]) : null);
                obj.FechaAprobado = (dr["FechaAprobado"].ToString() != String.Empty ? (DateTime?)(dr["FechaAprobado"]) : null);
                obj.Estado = (string)(dr["Estado"]);
                obj.PorcentajeAvance = (decimal)(dr["PorcentajeAvance"]);
                obj.Nota = (dr["Nota"].ToString() != String.Empty ? (decimal?)(dr["Nota"]) : null);
                obj.PorcentajeObtenido = (dr["PorcentajeObtenido"].ToString() != String.Empty ? (decimal?)(dr["PorcentajeObtenido"]) : null);  //(decimal)(dr["PorcentajeObtenido"]);
                obj.TotalUnidadesCursoOnline = (dr["TotalUnidadesCursoOnline"].ToString() != String.Empty ? (int?)(dr["TotalUnidadesCursoOnline"]) : null);  //(int)(dr["TotalUnidadesCursoOnline"]);
                obj.Grupo = dr["Grupo"].ToString();
                obj.Ranking = (dr["Ranking"].ToString() != String.Empty ? (int?)((decimal?)(dr["Ranking"])) : null);  //(int)(dr["Ranking"]);
                retVal.Add(obj);
            }

            return retVal;
        }

        public MatriculaCursoOnline ObtenerMatriculaCursoOnlinePorId(int matriculaCursoOnlineId)
        {
            MatriculaCursoOnline retVal = new MatriculaCursoOnline();

            DataTable dt = new DataTable();

            string cadenaConexion = Util.CadenaConexion;

            using (SqlConnection conexion = new SqlConnection(cadenaConexion))
            {
                conexion.Open();

                using (SqlCommand cmdSQL = new SqlCommand())
                {
                    cmdSQL.Connection = conexion;
                    cmdSQL.CommandType = CommandType.StoredProcedure;
                    cmdSQL.CommandText = "USP_MatriculaCursoOnline_SEL";

                    cmdSQL.Parameters.Add("@MatriculaCursoOnlineId", SqlDbType.Int).Value = matriculaCursoOnlineId;


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
                MatriculaCursoOnline obj = new MatriculaCursoOnline();
                obj.MatriculaCursoOnlineId = (int)(dr["MatriculaCursoOnlineId"]);
                obj.UsuarioId = (int)(dr["UsuarioId"]);
                obj.CursoOnlineId = (int)(dr["CursoOnlineId"]);
                obj.FechaMatricula = (DateTime)(dr["FechaMatricula"]);
                obj.FechaCompletado = (dr["FechaCompletado"].ToString() != String.Empty ? (DateTime?)(dr["FechaCompletado"]) : null);
                obj.FechaAprobado = (dr["FechaAprobado"].ToString() != String.Empty ? (DateTime?)(dr["FechaAprobado"]) : null);
                obj.Estado = (string)(dr["Estado"]);
                obj.PorcentajeAvance = (decimal)(dr["PorcentajeAvance"]);
                obj.Nota = (dr["Nota"].ToString() != String.Empty ? (decimal?)(dr["Nota"]) : null);
                obj.PorcentajeObtenido = (dr["PorcentajeObtenido"].ToString() != String.Empty ? (decimal?)(dr["PorcentajeObtenido"]) : null);
                obj.TotalUnidadesCursoOnline = (dr["TotalUnidadesCursoOnline"].ToString() != String.Empty ? (int?)(dr["TotalUnidadesCursoOnline"]) : null);
                obj.Grupo = (dr["Grupo"].ToString() != String.Empty ? (string)(dr["Grupo"]) : null);
                obj.Ranking = (dr["Ranking"].ToString() != String.Empty ? (int?)(dr["Ranking"]) : null);

                obj.CursoOnline = ObtenerCursoOnlinePorId(obj.CursoOnlineId);

                retVal = obj;
            }

            return retVal;
        }

        //public List<EvaluacionCursoOnline> ListarEvaluacionCursoOnlinePorMatriculaCursonOnlineId(int matriculaCursoOnlineId)
        //{
        //    List<EvaluacionCursoOnline> retVal = new List<EvaluacionCursoOnline>();

        //    DataTable dt = new DataTable();

        //    string cadenaConexion = Util.CadenaConexion;

        //    using (SqlConnection conexion = new SqlConnection(cadenaConexion))
        //    {
        //        conexion.Open();

        //        using (SqlCommand cmdSQL = new SqlCommand())
        //        {
        //            cmdSQL.Connection = conexion;
        //            cmdSQL.CommandType = CommandType.StoredProcedure;
        //            cmdSQL.CommandText = "USP_EvaluacionCursoOnline_LIS_PorMatriculaCursoOnlineId"; //'USP_EvaluacionCursoOnline_LIS_PorMatriculaCursoOnlineId'.

        //            cmdSQL.Parameters.Add("@MatriculaCursonOnlineId", SqlDbType.Int).Value = matriculaCursoOnlineId;


        //            using (SqlDataAdapter da = new SqlDataAdapter(cmdSQL))
        //            {
        //                da.Fill(dt);
        //            }
        //        }
        //        if (conexion.State == ConnectionState.Open)
        //        {
        //            conexion.Close();
        //        }
        //    }


        //    foreach (DataRow dr in dt.Rows)
        //    {
        //        EvaluacionCursoOnline obj = new EvaluacionCursoOnline();
        //        obj.EvaluacionCursoOnlineId = (int)(dr["EvaluacionCursoOnlineId"]);
        //        obj.MatriculaCursoOnlineId = (int)(dr["MatriculaCursoOnlineId"]);
        //        obj.UnidadCursoOnlineId = (int)(dr["UnidadCursoOnlineId"]);
        //        obj.FechaInicio = (DateTime)(dr["FechaInicio"]);
        //        obj.FechaFin = (dr["FechaFin"].ToString() != String.Empty ? (DateTime?)(dr["FechaFin"]) : null);  //(DateTime)(dr["FechaFin"]);
        //        obj.Nota = (dr["Nota"].ToString() != String.Empty ? (decimal?)(dr["Nota"]) : null);  //(decimal)(dr["Nota"]);
        //        obj.PorcentajeAprobacion = (decimal)(dr["PorcentajeAprobacion"]);
        //        obj.PuntajeTotal = (decimal)(dr["PuntajeTotal"]);
        //        obj.PuntajeObtenido = (dr["PuntajeObtenido"].ToString() != String.Empty ? (decimal?)(dr["PorcentajeObtenido"]) : null);  //(decimal)(dr["PuntajeObtenido"]);


        //        retVal.Add(obj);
        //    }

        //    return retVal;
        //}

        public CursoOnline ObtenerCursoOnlinePorId(int cursoOnlineId, int usuarioId=0)
        {
            CursoOnline retVal = new CursoOnline();

            DataTable dt = new DataTable();

            string cadenaConexion = Util.CadenaConexion;

            using (SqlConnection conexion = new SqlConnection(cadenaConexion))
            {
                conexion.Open();

                using (SqlCommand cmdSQL = new SqlCommand())
                {
                    cmdSQL.Connection = conexion;
                    cmdSQL.CommandType = CommandType.StoredProcedure;
                    cmdSQL.CommandText = "USP_CursoOnline_SEL_PorCursoOnlineId";
                    
                    cmdSQL.Parameters.Add("@CursoOnlineId", SqlDbType.Int).Value = cursoOnlineId;
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
                CursoOnline obj = new CursoOnline();
                obj.CursoOnlineId = (int)(dr["CursoOnlineId"]);
                obj.Calificacion = (int)(dr["Calificacion"]);
                obj.CantidadCalificacion = (int)(dr["CantidadCalificacion"]);
                obj.Codigo = (dr["Codigo"].ToString() != String.Empty ? (string)(dr["Codigo"]) : null);
                obj.Nombre = (dr["Nombre"].ToString() != String.Empty ? (string)(dr["Nombre"]) : null);
                obj.CategoriaCursoOnlineId = (int)(dr["CategoriaCursoOnlineId"]);
                obj.Estado = (dr["Estado"].ToString() != String.Empty ? (string)(dr["Estado"]) : null);
                obj.Descripcion = (dr["Descripcion"].ToString() != String.Empty ? (string)(dr["Descripcion"]) : null);
                obj.NumPreguntasEvaluacion = (int)(dr["NumPreguntasEvaluacion"]);
                obj.TiempoEvaluacion = (int)(dr["TiempoEvaluacion"]);
                obj.PorcentajeAprobacion = (decimal)(dr["PorcentajeAprobacion"]);
                obj.RutaImagen = (dr["RutaImagen"].ToString() != String.Empty ? (string)(dr["RutaImagen"]) : null);
                obj.TieneExamen = (dr["TieneExamen"].ToString() != String.Empty ? (string)(dr["TieneExamen"]) : null);
                obj.AutoMatriculaHabilitada = (bool)(dr["AutoMatriculaHabilitada"]);
                obj.EstadoFavorito = (bool)(dr["EstadoFavorito"]);
                
                obj.PermiteReinicioScorm = (dr["PermiteReinicioScorm"].ToString() != String.Empty ? (bool?)(dr["PermiteReinicioScorm"]) : null);       //(bool)(dr["PermiteReinicioScorm"]);
                obj.RutaBanner = (dr["RutaBanner"].ToString() != String.Empty ? (string)(dr["RutaBanner"]) : null); //(string)(dr["RutaBanner"]);
                obj.FechaCreacion = (dr["FechaCreacion"].ToString() != String.Empty ? (DateTime?)(dr["FechaCreacion"]) : null);  //(DateTime)(dr["FechaCreacion"]);
                obj.Ranking = (dr["Ranking"].ToString() != String.Empty ? (decimal?)(dr["Ranking"]) : null);//(decimal)(dr["Ranking"]);
                obj.NroRankings = (dr["NroRankings"].ToString() != String.Empty ? (int?)(dr["NroRankings"]) : null); //(int)(dr["NroRankings"]);
                obj.Orden = (dr["NroRankings"].ToString() != String.Empty ? (int?)(dr["Orden"]) : null);

                obj.UsuarioCreacion  = (dr["UsuarioCreacion"].ToString() != String.Empty ? (int?)(dr["UsuarioCreacion"]) : null);
                obj.UsuarioProfesor  = (dr["UsuarioProfesor"].ToString() != String.Empty ? (int?)(dr["UsuarioProfesor"]) : null);


                obj.DisponibilidadCurso = (dr["DisponibilidadCurso"].ToString() != String.Empty ? (int?)(dr["DisponibilidadCurso"]) : null);//(int)(dr["DisponibilidadCurso"]);


                retVal = obj;
            }

            return retVal;
        }

        public DisponibilidadCursoOnline ObtenerDisponibilidadCursoOnlinePorCursoOnlineId(int cursoOnlineId)
        {
            DisponibilidadCursoOnline retVal = new DisponibilidadCursoOnline();

            DataTable dt = new DataTable();

            string cadenaConexion = Util.CadenaConexion;

            using (SqlConnection conexion = new SqlConnection(cadenaConexion))
            {
                conexion.Open();

                using (SqlCommand cmdSQL = new SqlCommand())
                {
                    cmdSQL.Connection = conexion;
                    cmdSQL.CommandType = CommandType.StoredProcedure;
                    cmdSQL.CommandText = "USP_DisponibilidadCursoOnline_SEL_PorCursoOnlineId";
                    cmdSQL.Parameters.Add("@CursoOnlineId", SqlDbType.Int).Value = cursoOnlineId;


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
                DisponibilidadCursoOnline obj = new DisponibilidadCursoOnline();
                obj.DisponibilidadCursoOnlineId = (int)(dr["DisponibilidadCursoOnlineId"]);
                obj.CursoOnlineId = (int)(dr["CursoOnlineId"]);
                obj.FechaInicio = (DateTime)(dr["FechaInicio"]);
                obj.FechaFin = (dr["FechaFin"].ToString() != String.Empty ? (DateTime?)(dr["FechaFin"]) : null);

                retVal = obj;
            }

            return retVal;
        }

        public List<DisponibilidadCursoOnline> ListarDisponibilidadCursoOnlineUsuarioId(int usuarioId)
        {
            List<DisponibilidadCursoOnline> retVal = new List<DisponibilidadCursoOnline>();

            DataTable dt = new DataTable();

            string cadenaConexion = Util.CadenaConexion;

            using (SqlConnection conexion = new SqlConnection(cadenaConexion))
            {
                conexion.Open();

                using (SqlCommand cmdSQL = new SqlCommand())
                {
                    cmdSQL.Connection = conexion;
                    cmdSQL.CommandType = CommandType.StoredProcedure;
                    cmdSQL.CommandText = "USP_DisponibilidadCursoOnlineUsuario_LIS";

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

            List<DataRow> listDR = dt.AsEnumerable().ToList();

            retVal =
                listDR
                    .DistinctBy(
                        x =>
                            new
                            {
                                a = x["CursoOnlineId"],
                                b = x["DisponibilidadCursoOnlineId"]
                            })
                    .Select(
                        a => new DisponibilidadCursoOnline()
                        {
                            CursoOnlineId = (int)(a["CursoOnlineId"]),
                            DisponibilidadCursoOnlineId = (int)(a["DisponibilidadCursoOnlineId"]),
                            FechaInicio = (DateTime)(a["FechaInicio"]),
                            FechaFin = (a["FechaFin"].ToString() != String.Empty ? (DateTime?)(a["FechaFin"]) : null)
                        })
                        .ToList();

            foreach (DisponibilidadCursoOnline dco in retVal)
            {
                dco.CursoOnline = ObtenerCursoOnlinePorId(dco.CursoOnlineId);
            }


            return retVal;

        }

        public List<DisponibilidadCursoOnline> ListarDisponibilidadCursoOnlinePorCursoOnlineMatricula(int usuarioId)
        {
            List<DisponibilidadCursoOnline> retVal = new List<DisponibilidadCursoOnline>();

            DataTable dt = new DataTable();

            string cadenaConexion = Util.CadenaConexion;

            using (SqlConnection conexion = new SqlConnection(cadenaConexion))
            {
                conexion.Open();

                using (SqlCommand cmdSQL = new SqlCommand())
                {
                    cmdSQL.Connection = conexion;
                    cmdSQL.CommandType = CommandType.StoredProcedure;
                    cmdSQL.CommandText = "USP_DisponibilidadCursoOnline_LIS_PorCursoOnlineMatriculaCursoOnline";

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

            List<DataRow> listDR = dt.AsEnumerable().ToList();

            retVal =
                listDR
                    .DistinctBy(
                        x =>
                            new
                            {
                                a = x["CursoOnlineId"],
                                b = x["DisponibilidadCursoOnlineId"]
                            })
                    .Select(
                        a => new DisponibilidadCursoOnline()
                        {
                            CursoOnlineId = (int)(a["CursoOnlineId"]),
                            DisponibilidadCursoOnlineId = (int)(a["DisponibilidadCursoOnlineId"]),
                            FechaInicio = (DateTime)(a["FechaInicio"]),
                            FechaFin = (a["FechaFin"].ToString() != String.Empty ? (DateTime?)(a["FechaFin"]) : null)
                        })
                        .ToList();

            foreach (DisponibilidadCursoOnline dco in retVal)
            {
                dco.CursoOnline = ObtenerCursoOnlinePorId(dco.CursoOnlineId, usuarioId);
            }

            return retVal;

        }

        public List<DisponibilidadCursoOnline> ListarDisponibilidadCursoOnline()
        {
            List<DisponibilidadCursoOnline> retVal = new List<DisponibilidadCursoOnline>();

            DataTable dt = new DataTable();

            string cadenaConexion = Util.CadenaConexion;

            using (SqlConnection conexion = new SqlConnection(cadenaConexion))
            {
                conexion.Open();

                using (SqlCommand cmdSQL = new SqlCommand())
                {
                    cmdSQL.Connection = conexion;
                    cmdSQL.CommandType = CommandType.StoredProcedure;
                    cmdSQL.CommandText = "USP_DisponibilidadCursoOnline_LIS";
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
                DisponibilidadCursoOnline obj = new DisponibilidadCursoOnline();
                obj.CursoOnlineId = (int)(dr["CursoOnlineId"]);
                obj.DisponibilidadCursoOnlineId = (int)(dr["DisponibilidadCursoOnlineId"]);
                obj.FechaInicio = (DateTime)(dr["FechaInicio"]);
                obj.FechaFin = (dr["FechaFin"].ToString() != String.Empty ? (DateTime?)(dr["FechaFin"]) : null);

                obj.CursoOnline = ObtenerCursoOnlinePorId(obj.CursoOnlineId);

                retVal.Add(obj);
            }

            return retVal;

        }

        public List<CursoOnlineEncuestaResultado> ListarCursoOnlineEncuestaResultado(int cursoOnlineId)
        {
            List<CursoOnlineEncuestaResultado> retVal = new List<CursoOnlineEncuestaResultado>();

            DataTable dt = new DataTable();

            string cadenaConexion = Util.CadenaConexion;

            using (SqlConnection conexion = new SqlConnection(cadenaConexion))
            {
                conexion.Open();

                using (SqlCommand cmdSQL = new SqlCommand())
                {
                    cmdSQL.Connection = conexion;
                    cmdSQL.CommandType = CommandType.StoredProcedure;
                    cmdSQL.CommandText = "USP_CursoOnlineEncuestaResultado_SEL";
                    cmdSQL.Parameters.Add("@MatriculaCursoOnlineId", SqlDbType.Int).Value = cursoOnlineId;

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
                CursoOnlineEncuestaResultado obj = new CursoOnlineEncuestaResultado
                {
                    CursoOnlineId = int.Parse(dr["CursoOnlineId"].ToString()),
                    Calificacion = int.Parse(dr["Calificacion"].ToString()),
                    Pregunta = dr["Pregunta"].ToString(),
                    Respuesta = dr["Respuesta"].ToString(),
                    FechaRegistro = dr["FechaRegistro"].ToString()
                };

                retVal.Add(obj);
            }

            return retVal;
        }

        public List<int> ListarDisponibilidadCursoOnlineId()

        {
            List<int> retVal = new List<int>();

            DataTable dt = new DataTable();

            string cadenaConexion = Util.CadenaConexion;

            using (SqlConnection conexion = new SqlConnection(cadenaConexion))
            {
                conexion.Open();

                using (SqlCommand cmdSQL = new SqlCommand())
                {
                    cmdSQL.Connection = conexion;
                    cmdSQL.CommandType = CommandType.StoredProcedure;
                    cmdSQL.CommandText = "USP_DisponibilidadCursoOnline_LIS_CursoOnlineId";


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
                retVal.Add((int)(dr["CursoOnlineId"]));
            }


            return retVal;

        }

        public List<MatriculaCursoOnline> ListarMatriculaCursoOnline(int usuarioId)
        {
            List<MatriculaCursoOnline> retVal = new List<MatriculaCursoOnline>();

            DataTable dt = new DataTable();

            string cadenaConexion = Util.CadenaConexion;

            using (SqlConnection conexion = new SqlConnection(cadenaConexion))
            {
                conexion.Open();

                using (SqlCommand cmdSQL = new SqlCommand())
                {
                    cmdSQL.Connection = conexion;
                    cmdSQL.CommandType = CommandType.StoredProcedure;
                    cmdSQL.CommandText = "USP_MatriculaCursoOnlineUsuario_LIS";

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


            List<DataRow> listDR = dt.AsEnumerable().ToList();

            retVal =
                listDR
                    .DistinctBy(
                        x =>
                            new
                            {
                                a = x["CursoOnlineId"]
                            })
                    .Select(
                        a => new MatriculaCursoOnline()
                        {
                            UsuarioId = (int)(a["UsuarioId"]),
                            CursoOnlineId = (int)(a["CursoOnlineId"]),
                            MatriculaCursoOnlineId = (int)(a["MatriculaCursoOnlineId"]),
                            Estado = (a["Estado"].ToString() != String.Empty ? (string)(a["Estado"]) : null),
                            FechaMatricula = (DateTime)(a["FechaMatricula"]),
                            FechaCompletado = (a["FechaCompletado"].ToString() != string.Empty ? (DateTime?)(a["FechaCompletado"]) : null),
                            FechaAprobado = (a["FechaAprobado"].ToString() != string.Empty ? (DateTime?)(a["FechaAprobado"]) : null),
                            PorcentajeAvance = (decimal)(a["PorcentajeAvance"]),
                            Nota = (a["Nota"].ToString() != String.Empty ? (decimal?)(a["Nota"]) : null),
                            PorcentajeObtenido = (a["PorcentajeObtenido"].ToString() != String.Empty ? (decimal?)(a["PorcentajeObtenido"]) : null),
                            TotalUnidadesCursoOnline = (a["TotalUnidadesCursoOnline"].ToString() != String.Empty ? (int?)(a["TotalUnidadesCursoOnline"]) : null),
                            Grupo = (a["Grupo"].ToString() != String.Empty ? (string)(a["Grupo"]) : null),
                            Ranking = (a["Ranking"].ToString() != String.Empty ? (int?)(a["Ranking"]) : null)
                        })
                        .ToList();

            foreach (MatriculaCursoOnline mco in retVal)
            {
                mco.CursoOnline = ObtenerCursoOnlinePorId(mco.CursoOnlineId,usuarioId);
            }

            return retVal;

        }

        public List<MatriculaCursoOnline> ListarMatriculaCursoOnlinePorEmpresaId(int empresaId)
        {
            List<MatriculaCursoOnline> retVal = new List<MatriculaCursoOnline>();

            DataTable dt = new DataTable();

            string cadenaConexion = Util.CadenaConexion;

            using (SqlConnection conexion = new SqlConnection(cadenaConexion))
            {
                conexion.Open();

                using (SqlCommand cmdSQL = new SqlCommand())
                {
                    cmdSQL.Connection = conexion;
                    cmdSQL.CommandType = CommandType.StoredProcedure;
                    cmdSQL.CommandText = "USP_MatriculaCursoOnlineUsuario_LIS_PorEmpresaId";

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


            List<DataRow> listDR = dt.AsEnumerable().ToList();

            retVal =
                listDR
                    .DistinctBy(
                        x =>
                            new
                            {
                                a = x["CursoOnlineId"]
                            })
                    .Select(
                        a => new MatriculaCursoOnline()
                        {
                            UsuarioId = (int)(a["UsuarioId"]),
                            CursoOnlineId = (int)(a["CursoOnlineId"]),
                            MatriculaCursoOnlineId = (int)(a["MatriculaCursoOnlineId"]),
                            Estado = (a["Estado"].ToString() != String.Empty ? (string)(a["Estado"]) : null),
                            FechaMatricula = (DateTime)(a["FechaMatricula"]),
                            FechaCompletado = (a["FechaCompletado"].ToString() != string.Empty ? (DateTime?)(a["FechaCompletado"]) : null),
                            FechaAprobado = (a["FechaAprobado"].ToString() != string.Empty ? (DateTime?)(a["FechaAprobado"]) : null),
                            PorcentajeAvance = (decimal)(a["PorcentajeAvance"]),
                            Nota = (a["Nota"].ToString() != String.Empty ? (decimal?)(a["Nota"]) : null),
                            PorcentajeObtenido = (a["PorcentajeObtenido"].ToString() != String.Empty ? (decimal?)(a["PorcentajeObtenido"]) : null),
                            TotalUnidadesCursoOnline = (a["TotalUnidadesCursoOnline"].ToString() != String.Empty ? (int?)(a["TotalUnidadesCursoOnline"]) : null),
                            Grupo = (a["Grupo"].ToString() != String.Empty ? (string)(a["Grupo"]) : null),
                            Ranking = (a["Ranking"].ToString() != String.Empty ? (int?)(a["Ranking"]) : null)
                        })
                        .ToList();

            foreach (MatriculaCursoOnline mco in retVal)
            {
                mco.CursoOnline = ObtenerCursoOnlinePorId(mco.CursoOnlineId);
            }

            return retVal;

        }

        public List<CursoOnline> ListarCursosUsuarioPorNoUsuarioId(int usuarioId)
        {
            List<CursoOnline> retVal = new List<CursoOnline>();

            DataTable dt = new DataTable();

            string cadenaConexion = Util.CadenaConexion;

            using (SqlConnection conexion = new SqlConnection(cadenaConexion))
            {
                conexion.Open();

                using (SqlCommand cmdSQL = new SqlCommand())
                {
                    cmdSQL.Connection = conexion;
                    cmdSQL.CommandType = CommandType.StoredProcedure;
                    cmdSQL.CommandText = "USP_CursoOnline_LIS_PorNoUsuarioId";

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
                CursoOnline obj = new CursoOnline();
                obj.CursoOnlineId = (int)(dr["CursoOnlineId"]);
                obj.Nombre = (string)(dr["Nombre"]);
                obj.RutaImagen = (string)(dr["RutaImagen"]);
                obj.Ranking = (int)(dr["Ranking"]);
                obj.Descripcion = (string)(dr["Descripcion"]);
                obj.Estado = (string)(dr["Estado"]);
                obj.FechaCreacion = (dr["FechaCreacion"].ToString() != string.Empty ? (DateTime?)(dr["FechaCreacion"]) : null); //(DateTime)(dr["FechaCreacion"]);
                retVal.Add(obj);
            }

            return retVal;

        }

        public List<CursoOnline> ListarCursosUsuarioPorUsuarioId(int usuarioId)
        {
            List<CursoOnline> retVal = new List<CursoOnline>();

            DataTable dt = new DataTable();

            string cadenaConexion = Util.CadenaConexion;

            using (SqlConnection conexion = new SqlConnection(cadenaConexion))
            {
                conexion.Open();

                using (SqlCommand cmdSQL = new SqlCommand())
                {
                    cmdSQL.Connection = conexion;
                    cmdSQL.CommandType = CommandType.StoredProcedure;
                    cmdSQL.CommandText = "USP_CursoOnline_LIS_PorUsuarioId";

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
                CursoOnline obj = new CursoOnline();
                obj.CursoOnlineId = (int)(dr["CursoOnlineId"]);
                obj.Codigo = (string)(dr["Codigo"]);
                obj.Nombre = (string)(dr["Nombre"]);
                obj.CategoriaCursoOnlineId = (int)(dr["CategoriaCursoOnlineId"]);
                obj.Estado = (string)(dr["Estado"]);
                obj.Descripcion = (string)(dr["Descripcion"]);
                obj.NumPreguntasEvaluacion = (int)(dr["NumPreguntasEvaluacion"]);
                obj.TiempoEvaluacion = (int)(dr["TiempoEvaluacion"]);
                obj.PorcentajeAprobacion = (decimal)(dr["PorcentajeAprobacion"]);
                obj.RutaImagen = (string)(dr["RutaImagen"]);
                obj.TieneExamen = (string)(dr["TieneExamen"]);
                obj.AutoMatriculaHabilitada = (bool)(dr["AutoMatriculaHabilitada"]);
                obj.PermiteReinicioScorm = (dr["PermiteReinicioScorm"].ToString() != String.Empty ? (bool?)(dr["PermiteReinicioScorm"]) : null);       //(bool)(dr["PermiteReinicioScorm"]);
                obj.RutaBanner = (dr["RutaBanner"].ToString() != String.Empty ? (string)(dr["RutaBanner"]) : null); //(string)(dr["RutaBanner"]);
                obj.FechaCreacion = (DateTime)(dr["FechaCreacion"]);
                obj.Ranking = (dr["Ranking"].ToString() != String.Empty ? (decimal?)(dr["Ranking"]) : null);//(decimal)(dr["Ranking"]);
                obj.NroRankings = (dr["NroRankings"].ToString() != String.Empty ? (int?)(dr["NroRankings"]) : null); //(int)(dr["NroRankings"]);
                obj.Orden = (int)(dr["Orden"]);
                obj.DisponibilidadCurso = (dr["DisponibilidadCurso"].ToString() != String.Empty ? (int?)(dr["DisponibilidadCurso"]) : null);//(int)(dr["DisponibilidadCurso"]);
                retVal.Add(obj);
            }

            return retVal;

        }

        public List<CategoriaCursoOnline> ListarCategoriaCursoOnline()

        {
            List<CategoriaCursoOnline> retVal = new List<CategoriaCursoOnline>();

            DataTable dt = new DataTable();

            string cadenaConexion = Util.CadenaConexion;

            using (SqlConnection conexion = new SqlConnection(cadenaConexion))
            {
                conexion.Open();

                using (SqlCommand cmdSQL = new SqlCommand())
                {
                    cmdSQL.Connection = conexion;
                    cmdSQL.CommandType = CommandType.StoredProcedure;
                    cmdSQL.CommandText = "USP_CategoriaCursoOnline_LIS";


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
                CategoriaCursoOnline obj = new CategoriaCursoOnline();
                obj.CategoriaCursoOnlineId = (int)(dr["CategoriaCursoOnlineId"]);
                obj.Nombre = (string)(dr["Nombre"]);
                obj.Color = (dr["Color"].ToString() != String.Empty ? (string)(dr["Color"]) : null);

                retVal.Add(obj);
            }


            return retVal;

        }

        public CategoriaCursoOnline ObtenerCategoriaCursoOnline(int categoriaCursoOnlineId)
        {
            CategoriaCursoOnline retVal = new CategoriaCursoOnline();

            DataTable dt = new DataTable();

            string cadenaConexion = Util.CadenaConexion;

            using (SqlConnection conexion = new SqlConnection(cadenaConexion))
            {
                conexion.Open();

                using (SqlCommand cmdSQL = new SqlCommand())
                {
                    cmdSQL.Connection = conexion;
                    cmdSQL.CommandType = CommandType.StoredProcedure;
                    cmdSQL.CommandText = "USP_CategoriaCursoOnline_SEL";

                    cmdSQL.Parameters.Add("@CategoriaCursoOnlineId", SqlDbType.Int).Value = categoriaCursoOnlineId;

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
                retVal.CategoriaCursoOnlineId = (int)(dr["CategoriaCursoOnlineId"]);
                retVal.Nombre = (string)(dr["Nombre"]);
                retVal.Color = (dr["Color"].ToString() != String.Empty ? (string)(dr["Color"]) : null);
            }

            return retVal;
        }

        public List<CursoOnlineMaestro> ListarCursoOnlineMaestroUsuarioPorUsuarioId(int usuarioId)
        {
            List<CursoOnlineMaestro> retVal = new List<CursoOnlineMaestro>();

            DataTable dt = new DataTable();

            string cadenaConexion = Util.CadenaConexion;

            using (SqlConnection conexion = new SqlConnection(cadenaConexion))
            {
                conexion.Open();

                using (SqlCommand cmdSQL = new SqlCommand())
                {
                    cmdSQL.Connection = conexion;
                    cmdSQL.CommandType = CommandType.StoredProcedure;
                    cmdSQL.CommandText = "USP_CursoOnlineMaestro_LIS_PorUsuarioId";

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
                CursoOnlineMaestro obj = new CursoOnlineMaestro();
                obj.CursoOnlineMaestroId = (int)(dr["CursoOnlineMaestroId"]);
                obj.Codigo = (string)(dr["Codigo"]);
                obj.Nombre = (string)(dr["Nombre"]);
                obj.CategoriaCursoOnlineId = (int)(dr["CategoriaCursoOnlineId"]);
                obj.Estado = (string)(dr["Estado"]);
                obj.Descripcion = (string)(dr["Descripcion"]);
                obj.NumPreguntasEvaluacion = (int)(dr["NumPreguntasEvaluacion"]);
                obj.TiempoEvaluacion = (int)(dr["TiempoEvaluacion"]);
                obj.PorcentajeAprobacion = (decimal)(dr["PorcentajeAprobacion"]);
                obj.RutaImagen = (string)(dr["RutaImagen"]);
                obj.TieneExamen = (string)(dr["TieneExamen"]);
                obj.AutoMatriculaHabilitada = (bool)(dr["AutoMatriculaHabilitada"]);
                obj.PermiteReinicioScorm = (dr["PermiteReinicioScorm"].ToString() != String.Empty ? (bool?)(dr["PermiteReinicioScorm"]) : null);       //(bool)(dr["PermiteReinicioScorm"]);
                obj.RutaBanner = (dr["RutaBanner"].ToString() != String.Empty ? (string)(dr["RutaBanner"]) : null); //(string)(dr["RutaBanner"]);
                obj.FechaCreacion = (DateTime)(dr["FechaCreacion"]);
                obj.Ranking = (dr["Ranking"].ToString() != String.Empty ? (decimal?)(dr["Ranking"]) : null);//(decimal)(dr["Ranking"]);
                obj.NroRankings = (dr["NroRankings"].ToString() != String.Empty ? (int?)(dr["NroRankings"]) : null); //(int)(dr["NroRankings"]);
                obj.Orden = (dr["Orden"].ToString() != String.Empty ? (int?)(dr["Orden"]) : null);  //(int)(dr["Orden"]);
                obj.CantidadCursos = (dr["CantidadCursos"].ToString() != String.Empty ? (int?)(dr["CantidadCursos"]) : null);//(int)(dr["DisponibilidadCurso"]);
                retVal.Add(obj);
            }

            return retVal;

        }

        public List<CursoOnlineMaestro> ListarCursoOnlineMaestro()
        {
            List<CursoOnlineMaestro> retVal = new List<CursoOnlineMaestro>();

            DataTable dt = new DataTable();

            string cadenaConexion = Util.CadenaConexion;

            using (SqlConnection conexion = new SqlConnection(cadenaConexion))
            {
                conexion.Open();

                using (SqlCommand cmdSQL = new SqlCommand())
                {
                    cmdSQL.Connection = conexion;
                    cmdSQL.CommandType = CommandType.StoredProcedure;
                    cmdSQL.CommandText = "USP_CursoOnlineMaestro_LIS";

                    //cmdSQL.Parameters.Add("@UsuarioId", SqlDbType.Int).Value = usuarioId;


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
                CursoOnlineMaestro obj = new CursoOnlineMaestro();
                obj.CursoOnlineMaestroId = (int)(dr["CursoOnlineMaestroId"]);
                obj.CursoOnlineId = (int)(dr["CursoOnlineId"]);
                obj.Codigo = (string)(dr["Codigo"]);
                obj.Nombre = (string)(dr["Nombre"]);
                obj.CategoriaCursoOnlineId = (int)(dr["CategoriaCursoOnlineId"]);
                obj.Estado = (string)(dr["Estado"]);
                obj.Descripcion = (string)(dr["Descripcion"]);
                obj.NumPreguntasEvaluacion = (int)(dr["NumPreguntasEvaluacion"]);
                obj.TiempoEvaluacion = (int)(dr["TiempoEvaluacion"]);
                obj.PorcentajeAprobacion = (decimal)(dr["PorcentajeAprobacion"]);
                //obj.RutaImagen = (string)(dr["RutaImagen"]);
                obj.RutaImagen = (dr["RutaImagen"].ToString() != String.Empty ? (string)(dr["RutaImagen"]) : null);
                obj.TieneExamen = (string)(dr["TieneExamen"]);
                obj.AutoMatriculaHabilitada = (bool)(dr["AutoMatriculaHabilitada"]);
                obj.PermiteReinicioScorm = (dr["PermiteReinicioScorm"].ToString() != String.Empty ? (bool?)(dr["PermiteReinicioScorm"]) : null);       //(bool)(dr["PermiteReinicioScorm"]);
                obj.RutaBanner = (dr["RutaBanner"].ToString() != String.Empty ? (string)(dr["RutaBanner"]) : null); //(string)(dr["RutaBanner"]);
                obj.FechaCreacion = (DateTime)(dr["FechaCreacion"]);
                obj.Ranking = (dr["Ranking"].ToString() != String.Empty ? (decimal?)(dr["Ranking"]) : null);//(decimal)(dr["Ranking"]);
                obj.NroRankings = (dr["NroRankings"].ToString() != String.Empty ? (int?)(dr["NroRankings"]) : null); //(int)(dr["NroRankings"]);
                obj.Orden = (dr["Orden"].ToString() != String.Empty ? (int?)(dr["Orden"]) : null);  //(int)(dr["Orden"]);
                obj.CantidadCursos = (dr["CantidadCursos"].ToString() != String.Empty ? (int?)(dr["CantidadCursos"]) : null);//(int)(dr["DisponibilidadCurso"]);
                retVal.Add(obj);
            }

            return retVal;

        }

        public List<ViewCertificadoAlumno> ListarCertificadoAlumnoPorUsuarioId(int usuarioId)
        {
            List<ViewCertificadoAlumno> retVal = new List<ViewCertificadoAlumno>();

            DataTable dt = new DataTable();

            string cadenaConexion = Util.CadenaConexion;

            using (SqlConnection conexion = new SqlConnection(cadenaConexion))
            {
                conexion.Open();

                using (SqlCommand cmdSQL = new SqlCommand())
                {
                    cmdSQL.Connection = conexion;
                    cmdSQL.CommandType = CommandType.StoredProcedure;
                    cmdSQL.CommandText = "USP_ViewCertificadoAlumno_LIS_PorUsuarioId";
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
                ViewCertificadoAlumno obj = new ViewCertificadoAlumno();
                obj.Nombre = (string)(dr["Nombre"]);
                obj.UsuarioId = (int)(dr["UsuarioId"]);
                obj.CursoOnlineMaestroId = (int)(dr["CursoOnlineMaestroId"]);
                obj.PorcentajeObtenido = (decimal)(dr["PorcentajeObtenido"]);
                obj.PorcentajeAvance = (decimal)(dr["PorcentajeAvance"]);
                obj.CantidadCursos = (int)(dr["CantidadCursos"]);
                retVal.Add(obj);
            }

            return retVal;

        }

        public List<ViewCertificadoAlumno> ListarCertificadoTurismoAlumnoPorUsuarioId(int usuarioId)
        {
            List<ViewCertificadoAlumno> retVal = new List<ViewCertificadoAlumno>();

            DataTable dt = new DataTable();

            string cadenaConexion = Util.CadenaConexion;

            using (SqlConnection conexion = new SqlConnection(cadenaConexion))
            {
                conexion.Open();

                using (SqlCommand cmdSQL = new SqlCommand())
                {
                    cmdSQL.Connection = conexion;
                    cmdSQL.CommandType = CommandType.StoredProcedure;
                    cmdSQL.CommandText = "USP_ViewCertificadoTurismoAlumno_LIS_PorUsuarioId";
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
                ViewCertificadoAlumno obj = new ViewCertificadoAlumno();
                obj.Nombre = (string)(dr["Nombre"]);
                obj.UsuarioId = (int)(dr["UsuarioId"]);
                obj.CursoOnlineId = (int)(dr["CursoOnlineId"]);
                obj.PorcentajeObtenido = (decimal)(dr["PorcentajeObtenido"]);
                obj.PorcentajeAvance = (decimal)(dr["PorcentajeAvance"]);
                //obj.CantidadCursos = (int)(dr["CantidadCursos"]);
                retVal.Add(obj);
            }

            return retVal;

        }

        public List<EvaluacionCursoOnline> ListarEvaluacionCursoOnlinePorMatriculaCursoOnlineIdAgrupado(int matriculaCursoOnlineId)
        {
            List<EvaluacionCursoOnline> retVal = new List<EvaluacionCursoOnline>();

            DataTable dt = new DataTable();

            string cadenaConexion = Util.CadenaConexion;

            using (SqlConnection conexion = new SqlConnection(cadenaConexion))
            {
                conexion.Open();

                using (SqlCommand cmdSQL = new SqlCommand())
                {
                    cmdSQL.Connection = conexion;
                    cmdSQL.CommandType = CommandType.StoredProcedure;
                    cmdSQL.CommandText = "USP_EvaluacionCursoOnline_LIS_PorMatriculaCursoOnlineId";
                    cmdSQL.Parameters.Add("@MatriculaCursoOnlineId", SqlDbType.Int).Value = matriculaCursoOnlineId;
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
                EvaluacionCursoOnline obj = new EvaluacionCursoOnline();
                obj.MatriculaCursoOnlineId = (int)(dr["MatriculaCursoOnlineId"]);
                obj.UnidadCursoOnlineId = (int)(dr["UnidadCursoOnlineId"]);
                obj.Nota = (dr["Nota"].ToString() != String.Empty ? (decimal?)(dr["Nota"]) : null);
                retVal.Add(obj);
            }

            return retVal;

        }

        public List<RespuestaCursoOnline> ListarRespuestaCursoOnlinePorEvaluacionCursoOnlineId(int evaluacionCursoOnlineId)
        {
            List<RespuestaCursoOnline> retVal = new List<RespuestaCursoOnline>();

            DataTable dt = new DataTable();

            string cadenaConexion = Util.CadenaConexion;

            using (SqlConnection conexion = new SqlConnection(cadenaConexion))
            {
                conexion.Open();

                using (SqlCommand cmdSQL = new SqlCommand())
                {
                    cmdSQL.Connection = conexion;
                    cmdSQL.CommandType = CommandType.StoredProcedure;
                    cmdSQL.CommandText = "USP_RespuestaCursoOnline_LIS_PorEvaluacionCursoOnlineId";

                    cmdSQL.Parameters.Add("@EvaluacionCursoOnlineId", SqlDbType.Int).Value = evaluacionCursoOnlineId;

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
                RespuestaCursoOnline obj = new RespuestaCursoOnline();
                obj.RespuestaCursoOnlineId = (int)(dr["RespuestaCursoOnlineId"]);
                obj.PreguntaCursoOnlineId = (int)(dr["PreguntaCursoOnlineId"]);
                obj.Texto = dr["Texto"].ToString();
                obj.EsSolucion = (bool)(dr["EsSolucion"]);
                obj.OrdenSolucion = (dr["OrdenSolucion"].ToString() != String.Empty ? (int?)(dr["OrdenSolucion"]) : null);
                retVal.Add(obj);
            }

            return retVal;
        }

        public List<PreguntaCursoOnline> ListarPreguntaCursoOnlinePorCursoOnlineId(int cursoOnlineId)
        {
            List<PreguntaCursoOnline> retVal = new List<PreguntaCursoOnline>();

            DataTable dt = new DataTable();

            string cadenaConexion = Util.CadenaConexion;

            using (SqlConnection conexion = new SqlConnection(cadenaConexion))
            {
                conexion.Open();

                using (SqlCommand cmdSQL = new SqlCommand())
                {
                    cmdSQL.Connection = conexion;
                    cmdSQL.CommandType = CommandType.StoredProcedure;
                    cmdSQL.CommandText = "USP_PreguntaCursoOnline_LIS_PorCursoOnline";
                    cmdSQL.Parameters.Add("@CursoOnlineId", SqlDbType.Int).Value = cursoOnlineId;
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
                PreguntaCursoOnline obj = new PreguntaCursoOnline();

                obj.PreguntaCursoOnlineId = (int)(dr["PreguntaCursoOnlineId"]);
                obj.CursoOnlineId = (int)(dr["CursoOnlineId"]);
                obj.Tipo = dr["Tipo"].ToString();
                obj.NumRespuestas = (int)(dr["NumRespuestas"]);
                obj.Texto = (string)(dr["Texto"]);
                obj.Puntaje = (int)(dr["Puntaje"]);
                obj.Estado = (string)(dr["Estado"]);
                obj.UnidadOnlineId = (int)(dr["UnidadOnlineId"]);

                retVal.Add(obj);
            }

            return retVal;

        }

        public List<PreguntaCursoOnline> ListarPreguntaCursoOnline()
        {
            List<PreguntaCursoOnline> retVal = new List<PreguntaCursoOnline>();

            DataTable dt = new DataTable();

            string cadenaConexion = Util.CadenaConexion;

            using (SqlConnection conexion = new SqlConnection(cadenaConexion))
            {
                conexion.Open();

                using (SqlCommand cmdSQL = new SqlCommand())
                {
                    cmdSQL.Connection = conexion;
                    cmdSQL.CommandType = CommandType.StoredProcedure;
                    cmdSQL.CommandText = "USP_PreguntaCursoOnline_LIS";
                    //cmdSQL.Parameters.Add("@CursoOnlineId", SqlDbType.Int).Value = cursoOnlineId;
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
                PreguntaCursoOnline obj = new PreguntaCursoOnline();

                obj.PreguntaCursoOnlineId = (int)(dr["PreguntaCursoOnlineId"]);
                obj.CursoOnlineId = (dr["CursoOnlineId"].ToString() != String.Empty ? (int?)(dr["CursoOnlineId"]) : null);  //(int)(dr["CursoOnlineId"]);
                obj.Tipo = dr["Tipo"].ToString();
                obj.NumRespuestas = (int)(dr["NumRespuestas"]);
                obj.Texto = (string)(dr["Texto"]);
                obj.Puntaje = (int)(dr["Puntaje"]);
                obj.Estado = (string)(dr["Estado"]);
                obj.UnidadOnlineId = (int)(dr["UnidadOnlineId"]);

                retVal.Add(obj);
            }

            return retVal;

        }

        public PreguntaCursoOnline ObtenerPreguntaCursoOnline(int preguntaCursoOnlineId)
        {
            PreguntaCursoOnline retVal = new PreguntaCursoOnline();

            DataTable dt = new DataTable();

            string cadenaConexion = Util.CadenaConexion;

            using (SqlConnection conexion = new SqlConnection(cadenaConexion))
            {
                conexion.Open();

                using (SqlCommand cmdSQL = new SqlCommand())
                {
                    cmdSQL.Connection = conexion;
                    cmdSQL.CommandType = CommandType.StoredProcedure;
                    cmdSQL.CommandText = "USP_PreguntaCursoOnline_SEL";
                    cmdSQL.Parameters.Add("@PreguntaCursoOnlineId", SqlDbType.Int).Value = preguntaCursoOnlineId;
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
                PreguntaCursoOnline obj = new PreguntaCursoOnline();

                obj.PreguntaCursoOnlineId = (int)(dr["PreguntaCursoOnlineId"]);
                obj.CursoOnlineId = (dr["CursoOnlineId"].ToString() != String.Empty ? (int?)(dr["CursoOnlineId"]) : null);  //(int)(dr["CursoOnlineId"]);
                obj.Tipo = dr["Tipo"].ToString();
                obj.NumRespuestas = (int)(dr["NumRespuestas"]);
                obj.Texto = (string)(dr["Texto"]);
                obj.Puntaje = (int)(dr["Puntaje"]);
                obj.Estado = (string)(dr["Estado"]);
                obj.UnidadOnlineId = (int)(dr["UnidadOnlineId"]);

                retVal = obj;
            }

            return retVal;
        }

        public List<PreguntaCursoOnline> ListarPreguntaCursoOnlinePorUnidadOnlineId(int unidadOnlineId)
        {
            List<PreguntaCursoOnline> retVal = new List<PreguntaCursoOnline>();

            DataTable dt = new DataTable();

            string cadenaConexion = Util.CadenaConexion;

            using (SqlConnection conexion = new SqlConnection(cadenaConexion))
            {
                conexion.Open();

                using (SqlCommand cmdSQL = new SqlCommand())
                {
                    cmdSQL.Connection = conexion;
                    cmdSQL.CommandType = CommandType.StoredProcedure;
                    cmdSQL.CommandText = "USP_PreguntaCursoOnline_LIS_PorUnidadOnlineId";
                    cmdSQL.Parameters.Add("@UnidadOnlineId", SqlDbType.Int).Value = unidadOnlineId;
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

            //PreguntaCursoOnline objP = new PreguntaCursoOnline();
            RespuestaCursoOnline objR = new RespuestaCursoOnline();

            foreach (DataRow dr in dt.Rows)
            {
                PreguntaCursoOnline obj = retVal.LastOrDefault();
                objR = new RespuestaCursoOnline();
                objR.RespuestaCursoOnlineId = (int)(dr["RespuestaCursoOnlineId"]);
                objR.PreguntaCursoOnlineId = (int)(dr["PreguntaCursoOnlineId"]);
                objR.Texto = dr["TextoRCO"].ToString();
                objR.EsSolucion = (bool)(dr["EsSolucion"]);
                objR.OrdenSolucion = (dr["OrdenSolucion"].ToString() != String.Empty ? (int?)(dr["OrdenSolucion"]) : null);

                if (obj != null && obj.PreguntaCursoOnlineId == (int)(dr["PreguntaCursoOnlineId"]))
                {
                    retVal.LastOrDefault().RespuestaCursoOnline.Add(objR);
                }
                else
                {

                    obj = new PreguntaCursoOnline();
                    obj.PreguntaCursoOnlineId = (int)(dr["PreguntaCursoOnlineId"]);
                    obj.CursoOnlineId = (dr["CursoOnlineId"].ToString() != String.Empty ? (int?)(dr["CursoOnlineId"]) : null);
                    obj.Tipo = dr["Tipo"].ToString();
                    obj.NumRespuestas = (int)(dr["NumRespuestas"]);
                    obj.Texto = (string)(dr["Texto"]);
                    obj.Puntaje = (int)(dr["Puntaje"]);
                    obj.Estado = (string)(dr["Estado"]);
                    obj.UnidadOnlineId = (int)(dr["UnidadOnlineId"]);
                    obj.RespuestaCursoOnline = new List<RespuestaCursoOnline>();
                    obj.RespuestaCursoOnline.Add(objR);
                    retVal.Add(obj);
                }
            }

            return retVal;

        }

        public List<PreguntaEvaluacionCursoOnline> ListarPreguntaEvaluacionCursoOnline()
        {
            List<PreguntaEvaluacionCursoOnline> retVal = new List<PreguntaEvaluacionCursoOnline>();

            DataTable dt = new DataTable();

            string cadenaConexion = Util.CadenaConexion;

            using (SqlConnection conexion = new SqlConnection(cadenaConexion))
            {
                conexion.Open();

                using (SqlCommand cmdSQL = new SqlCommand())
                {
                    cmdSQL.Connection = conexion;
                    cmdSQL.CommandType = CommandType.StoredProcedure;
                    cmdSQL.CommandText = "USP_PreguntaEvaluacionCursoOnlinePreguntaCursoOnline_LIS";

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
                PreguntaEvaluacionCursoOnline obj = new PreguntaEvaluacionCursoOnline();

                obj.PreguntaEvaluacionCursoOnlineId = (int)(dr["PreguntaEvaluacionCursoOnlineId"]);
                obj.EvaluacionCursoOnlineId = (int)(dr["EvaluacionCursoOnlineId"]);
                obj.PreguntaCursoOnlineId = (int)(dr["PreguntaCursoOnlineId"]);
                obj.TieneRespuestaCorrecta = (dr["TieneRespuestaCorrecta"].ToString() != String.Empty ? (bool?)(dr["TieneRespuestaCorrecta"]) : null);

                retVal.Add(obj);
            }

            return retVal;

        }

        public List<PreguntaEvaluacionCursoOnline> ListarPreguntaEvaluacionCursoOnlinePorEvaluacionCursoOnlineId(int evaluacionCursoOnlineId)
        {
            List<PreguntaEvaluacionCursoOnline> retVal = new List<PreguntaEvaluacionCursoOnline>();

            DataTable dt = new DataTable();

            string cadenaConexion = Util.CadenaConexion;

            using (SqlConnection conexion = new SqlConnection(cadenaConexion))
            {
                conexion.Open();

                using (SqlCommand cmdSQL = new SqlCommand())
                {
                    cmdSQL.Connection = conexion;
                    cmdSQL.CommandType = CommandType.StoredProcedure;
                    cmdSQL.Parameters.Add("@EvaluacionCursoOnlineId", SqlDbType.Int).Value = evaluacionCursoOnlineId;
                    cmdSQL.CommandText = "USP_PreguntaEvaluacionCursoOnlinePreguntaCursoOnline_LIS_PorEvaluacionCursoOnlineId";

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

            List<PreguntaCursoOnline> lstPregs = ListarPreguntaCursoOnline();

            foreach (DataRow dr in dt.Rows)
            {
                PreguntaEvaluacionCursoOnline obj = new PreguntaEvaluacionCursoOnline();

                obj.PreguntaEvaluacionCursoOnlineId = (int)(dr["PreguntaEvaluacionCursoOnlineId"]);
                obj.EvaluacionCursoOnlineId = (int)(dr["EvaluacionCursoOnlineId"]);
                obj.PreguntaCursoOnlineId = (int)(dr["PreguntaCursoOnlineId"]);
                obj.TieneRespuestaCorrecta = (dr["TieneRespuestaCorrecta"].ToString() != String.Empty ? (bool?)(dr["TieneRespuestaCorrecta"]) : null);

                obj.PreguntaCursoOnline = lstPregs.First(x => x.PreguntaCursoOnlineId == obj.PreguntaCursoOnlineId);

                retVal.Add(obj);
            }

            return retVal;

        }

        public List<RespuestaEvaluacionCursoOnline> ListarRespuestaEvaluacionCursoOnline()
        {
            List<RespuestaEvaluacionCursoOnline> retVal = new List<RespuestaEvaluacionCursoOnline>();

            DataTable dt = new DataTable();

            string cadenaConexion = Util.CadenaConexion;

            using (SqlConnection conexion = new SqlConnection(cadenaConexion))
            {
                conexion.Open();

                using (SqlCommand cmdSQL = new SqlCommand())
                {
                    cmdSQL.Connection = conexion;
                    cmdSQL.CommandType = CommandType.StoredProcedure;
                    cmdSQL.CommandText = "USP_RespuestaEvaluacionCursoOnlineRespuestaCursoOnline_LIS";

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
                RespuestaEvaluacionCursoOnline obj = new RespuestaEvaluacionCursoOnline();

                obj.RespuestaEvaluacionCursoOnlineId = (int)(dr["RespuestaEvaluacionCursoOnlineId"]);
                obj.PreguntaEvaluacionCursoOnlineId = (int)(dr["PreguntaEvaluacionCursoOnlineId"]);
                obj.RespuestaCursoOnlineId = (int)(dr["RespuestaCursoOnlineId"]);
                obj.OrdenMostrado = (int)(dr["OrdenMostrado"]);

                retVal.Add(obj);
            }

            return retVal;

        }

        public List<RespuestaEvaluacionCursoOnline> ListarRespuestaEvaluacionCursoOnlinePorEvaluacionCursoOnlineId(int evaluacionCursoOnlineId)
        {
            List<RespuestaEvaluacionCursoOnline> retVal = new List<RespuestaEvaluacionCursoOnline>();

            DataTable dt = new DataTable();

            string cadenaConexion = Util.CadenaConexion;

            using (SqlConnection conexion = new SqlConnection(cadenaConexion))
            {
                conexion.Open();

                using (SqlCommand cmdSQL = new SqlCommand())
                {
                    cmdSQL.Connection = conexion;
                    cmdSQL.CommandType = CommandType.StoredProcedure;
                    cmdSQL.Parameters.Add("@EvaluacionCursoOnlineId", SqlDbType.Int).Value = evaluacionCursoOnlineId;

                    cmdSQL.CommandText = "USP_RespuestaEvaluacionCursoOnlineRespuestaCursoOnline_LIS_PorEvaluacionCursoOnlineId";

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
                RespuestaEvaluacionCursoOnline obj = new RespuestaEvaluacionCursoOnline();

                obj.RespuestaEvaluacionCursoOnlineId = (int)(dr["RespuestaEvaluacionCursoOnlineId"]);
                obj.PreguntaEvaluacionCursoOnlineId = (int)(dr["PreguntaEvaluacionCursoOnlineId"]);
                obj.RespuestaCursoOnlineId = (int)(dr["RespuestaCursoOnlineId"]);
                obj.OrdenMostrado = (int)(dr["OrdenMostrado"]);

                obj.RespuestaCursoOnline = ObtenerRespuestaCursoOnlinePorId(obj.RespuestaCursoOnlineId);

                retVal.Add(obj);
            }

            return retVal;

        }

        public RespuestaCursoOnline ObtenerRespuestaCursoOnlinePorId(int respuestaCursoOnlineId)
        {
            RespuestaCursoOnline retVal = new RespuestaCursoOnline();

            DataTable dt = new DataTable();

            string cadenaConexion = Util.CadenaConexion;

            using (SqlConnection conexion = new SqlConnection(cadenaConexion))
            {
                conexion.Open();

                using (SqlCommand cmdSQL = new SqlCommand())
                {
                    cmdSQL.Connection = conexion;
                    cmdSQL.CommandType = CommandType.StoredProcedure;

                    cmdSQL.Parameters.Add("@RespuestaCursoOnlineId", SqlDbType.Int).Value = respuestaCursoOnlineId;

                    cmdSQL.CommandText = "USP_RespuestaCursoOnline_SEL_PorRespuestaCursoOnlineId";

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


                retVal.RespuestaCursoOnlineId = (int)(dr["RespuestaCursoOnlineId"]);
                retVal.PreguntaCursoOnlineId = (int)(dr["PreguntaCursoOnlineId"]);
                retVal.Texto = (string)(dr["Texto"]);
                retVal.EsSolucion = (bool)(dr["EsSolucion"]);
                retVal.OrdenSolucion = (dr["OrdenSolucion"].ToString() != String.Empty ? (int?)(dr["OrdenSolucion"]) : null);



            }

            return retVal;

        }

        public bool EliminarCategoriaCursoOnline(int categoriaCursoOnlineId)
        {
            string cadenaConexion = Util.CadenaConexion;

            try
            {
                using (SqlConnection conexion = new SqlConnection(cadenaConexion))
                {
                    conexion.Open();

                    using (SqlCommand cmdSql = new SqlCommand())
                    {
                        cmdSql.Connection = conexion;
                        cmdSql.CommandType = CommandType.StoredProcedure;
                        cmdSql.CommandText = "USP_CategoriaCursoOnline_DEL";

                        cmdSql.Parameters.Add("@CategoriaCursoOnlineId", SqlDbType.Int).Value = categoriaCursoOnlineId;
                        cmdSql.ExecuteNonQuery();
                    }
                    if (conexion.State == ConnectionState.Open)
                    {
                        conexion.Close();
                    }
                }

                return true;
            }
            catch (Exception e)
            {
                return false;
            }
        }
        
        public bool DeleteCursoMaestroOnline(Int32 CursoOnlineId)
        {
            string cadenaConexion = Util.CadenaConexion;

            try
            {
                using (SqlConnection conexion = new SqlConnection(cadenaConexion))
                {
                    conexion.Open();

                    using (SqlCommand cmdSql = new SqlCommand())
                    {
                        cmdSql.Connection = conexion;
                        cmdSql.CommandType = CommandType.StoredProcedure;
                        cmdSql.CommandText = "USP_CursoOnlineMaestro_DEL";
                        cmdSql.Parameters.Add("@CursoOnlineMaestroId", SqlDbType.Int).Value = CursoOnlineId;
                        cmdSql.ExecuteNonQuery();
                    }
                    if (conexion.State == ConnectionState.Open)
                    {
                        conexion.Close();
                    }
                }

                return true;
            }
            catch (Exception e)
            {
                return false;
            }
        }

        public int ActualizarCategoriaCursoOnline(CategoriaCursoOnline categoriaCursoOnline)
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
                    cmdSQL.CommandText = "USP_CategoriaCursoOnline_UPD";

                    cmdSQL.Parameters.Add("@CategoriaCursoOnlineId", SqlDbType.Int).Value = categoriaCursoOnline.CategoriaCursoOnlineId;
                    cmdSQL.Parameters.Add("@Nombre", SqlDbType.VarChar, -1).Value = categoriaCursoOnline.Nombre;
                    cmdSQL.Parameters.Add("@Color", SqlDbType.VarChar, -1).Value = categoriaCursoOnline.Color;
                    lintResultado = cmdSQL.ExecuteNonQuery();
                }
                if (conexion.State == ConnectionState.Open)
                {
                    conexion.Close();
                }
            }

            return lintResultado;

        }

        public int InsertarCategoriaCursoOnline(CategoriaCursoOnline categoriaCursoOnline)
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
                    cmdSQL.CommandText = "USP_CategoriaCursoOnline_INS";
                    cmdSQL.Parameters.Add("@Nombre", SqlDbType.VarChar, -1).Value = categoriaCursoOnline.Nombre;
                    cmdSQL.Parameters.Add("@Color", SqlDbType.VarChar, -1).Value = categoriaCursoOnline.Color;
                    lintResultado = cmdSQL.ExecuteNonQuery();
                }
                if (conexion.State == ConnectionState.Open)
                {
                    conexion.Close();
                }
            }

            return lintResultado;

        }

        public int InsertarCursoOnline(CursoOnline cursoOnline)
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
                    cmdSQL.CommandText = "USP_CursoOnline_INS";
                    cmdSQL.Parameters.Add("@Codigo", SqlDbType.VarChar, -1).Value = cursoOnline.Codigo;
                    cmdSQL.Parameters.Add("@Nombre", SqlDbType.VarChar, -1).Value = cursoOnline.Nombre;
                    cmdSQL.Parameters.Add("@CategoriaCursoOnlineId", SqlDbType.Int).Value = cursoOnline.CategoriaCursoOnlineId;
                    cmdSQL.Parameters.Add("@Estado", SqlDbType.VarChar, -1).Value = cursoOnline.Estado;
                    cmdSQL.Parameters.Add("@Descripcion", SqlDbType.VarChar, -1).Value = cursoOnline.Descripcion;
                    cmdSQL.Parameters.Add("@NumPreguntasEvaluacion", SqlDbType.Int).Value = cursoOnline.NumPreguntasEvaluacion;
                    cmdSQL.Parameters.Add("@TiempoEvaluacion", SqlDbType.Int).Value = cursoOnline.TiempoEvaluacion;
                    cmdSQL.Parameters.Add("@PorcentajeAprobacion", SqlDbType.Decimal).Value = cursoOnline.PorcentajeAprobacion;
                    cmdSQL.Parameters.Add("@RutaImagen", SqlDbType.VarChar, -1).Value = cursoOnline.RutaImagen;
                    cmdSQL.Parameters.Add("@TieneExamen", SqlDbType.VarChar, -1).Value = cursoOnline.TieneExamen;
                    cmdSQL.Parameters.Add("@AutoMatriculaHabilitada", SqlDbType.Bit).Value = cursoOnline.AutoMatriculaHabilitada;
                    cmdSQL.Parameters.Add("@PermiteReinicioScorm", SqlDbType.Bit).Value = cursoOnline.PermiteReinicioScorm;
                    cmdSQL.Parameters.Add("@RutaBanner", SqlDbType.VarChar, -1).Value = cursoOnline.RutaBanner;
                    cmdSQL.Parameters.Add("@FechaCreacion", SqlDbType.DateTime).Value = cursoOnline.FechaCreacion;
                    cmdSQL.Parameters.Add("@Ranking", SqlDbType.Decimal).Value = cursoOnline.Ranking;
                    cmdSQL.Parameters.Add("@NroRankings", SqlDbType.Int).Value = cursoOnline.NroRankings;
                    cmdSQL.Parameters.Add("@Orden", SqlDbType.Int).Value = cursoOnline.Orden;
                    cmdSQL.Parameters.Add("@DisponibilidadCurso", SqlDbType.Int).Value = cursoOnline.DisponibilidadCurso;


                    cmdSQL.Parameters.Add("@UsuarioCreacion", SqlDbType.Int).Value = cursoOnline.UsuarioCreacion;
                    cmdSQL.Parameters.Add("@UsuarioProfesor", SqlDbType.Int).Value = cursoOnline.UsuarioProfesor;

                    lintResultado = Convert.ToInt32(cmdSQL.ExecuteScalar().ToString());

                }
                if (conexion.State == ConnectionState.Open)
                {
                    conexion.Close();
                }
            }

            return lintResultado;

        }

        public int InsertarCursoGrupoMaestro(CursoGrupo cursoGrupo)
        {
            int lintResultado = 0;

            string cadenaConexion = Util.CadenaConexion;

            using (SqlConnection conexion = new SqlConnection(cadenaConexion))
            {
                conexion.Open();

                using (SqlCommand cmdSql = new SqlCommand())
                {
                    cmdSql.Connection = conexion;
                    cmdSql.CommandType = CommandType.StoredProcedure;
                    cmdSql.CommandText = "USP_CursoGrupo_INS";
                    cmdSql.Parameters.Add("@GrupoId", SqlDbType.Int).Value = cursoGrupo.GrupoId;
                    cmdSql.Parameters.Add("@CursoOnlineMaestroId", SqlDbType.Int).Value = cursoGrupo.CursoOnlineMaestroId;
                    cmdSql.Parameters.Add("@Nombre", SqlDbType.VarChar, -1).Value = cursoGrupo.Nombre;
                    cmdSql.Parameters.Add("@Estado", SqlDbType.VarChar, -1).Value = cursoGrupo.Estado;
                    cmdSql.Parameters.Add("@CursoOnlineId", SqlDbType.Int).Value = cursoGrupo.CursoOnlineId;

                    lintResultado = Convert.ToInt32(cmdSql.ExecuteScalar().ToString());

                }
                if (conexion.State == ConnectionState.Open)
                {
                    conexion.Close();
                }
            }

            return lintResultado;

        }

        public int InsertarUsuarioGrupoMaestro(UsuarioGrupo usuarioGrupo)
        {
            int lintResultado = 0;

            string cadenaConexion = Util.CadenaConexion;

            using (SqlConnection conexion = new SqlConnection(cadenaConexion))
            {
                conexion.Open();

                using (SqlCommand cmdSql = new SqlCommand())
                {
                    cmdSql.Connection = conexion;
                    cmdSql.CommandType = CommandType.StoredProcedure;
                    cmdSql.CommandText = "USP_UsuarioGrupo_INS";
                    cmdSql.Parameters.Add("@GrupoId", SqlDbType.Int).Value = usuarioGrupo.GrupoId;
                    cmdSql.Parameters.Add("@UsuarioId", SqlDbType.Int).Value = usuarioGrupo.UsuarioId;
                    cmdSql.Parameters.Add("@Nombre", SqlDbType.VarChar, -1).Value = usuarioGrupo.Nombre;
                    cmdSql.Parameters.Add("@Estado", SqlDbType.VarChar, -1).Value = usuarioGrupo.Estado;

                    lintResultado = Convert.ToInt32(cmdSql.ExecuteScalar().ToString());

                }
                if (conexion.State == ConnectionState.Open)
                {
                    conexion.Close();
                }
            }

            return lintResultado;

        }

        public int InsertarCursoOnlineMaestro(CursoOnlineMaestro cursoOnline)
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
                    cmdSQL.CommandText = "USP_CursoOnlineMaestro_INS";
                    cmdSQL.Parameters.Add("@Codigo", SqlDbType.VarChar, -1).Value = cursoOnline.Codigo;
                    cmdSQL.Parameters.Add("@Nombre", SqlDbType.VarChar, -1).Value = cursoOnline.Nombre;
                    cmdSQL.Parameters.Add("@CategoriaCursoOnlineId", SqlDbType.Int).Value = cursoOnline.CategoriaCursoOnlineId;
                    cmdSQL.Parameters.Add("@Estado", SqlDbType.VarChar, -1).Value = cursoOnline.Estado;
                    cmdSQL.Parameters.Add("@Descripcion", SqlDbType.VarChar, -1).Value = cursoOnline.Descripcion;
                    cmdSQL.Parameters.Add("@NumPreguntasEvaluacion", SqlDbType.Int).Value = cursoOnline.NumPreguntasEvaluacion;
                    cmdSQL.Parameters.Add("@TiempoEvaluacion", SqlDbType.Int).Value = cursoOnline.TiempoEvaluacion;
                    cmdSQL.Parameters.Add("@PorcentajeAprobacion", SqlDbType.Decimal).Value = cursoOnline.PorcentajeAprobacion;
                    cmdSQL.Parameters.Add("@RutaImagen", SqlDbType.VarChar, -1).Value = cursoOnline.RutaImagen;
                    cmdSQL.Parameters.Add("@TieneExamen", SqlDbType.VarChar, -1).Value = cursoOnline.TieneExamen;
                    cmdSQL.Parameters.Add("@AutoMatriculaHabilitada", SqlDbType.Bit).Value = cursoOnline.AutoMatriculaHabilitada;
                    cmdSQL.Parameters.Add("@PermiteReinicioScorm", SqlDbType.Bit).Value = cursoOnline.PermiteReinicioScorm;
                    cmdSQL.Parameters.Add("@RutaBanner", SqlDbType.VarChar, -1).Value = cursoOnline.RutaBanner;
                    cmdSQL.Parameters.Add("@FechaCreacion", SqlDbType.DateTime).Value = cursoOnline.FechaCreacion;
                    cmdSQL.Parameters.Add("@Ranking", SqlDbType.Decimal).Value = cursoOnline.Ranking;
                    cmdSQL.Parameters.Add("@NroRankings", SqlDbType.Int).Value = cursoOnline.NroRankings;
                    cmdSQL.Parameters.Add("@Orden", SqlDbType.Int).Value = cursoOnline.Orden;


                    lintResultado = Convert.ToInt32(cmdSQL.ExecuteScalar().ToString());

                }
                if (conexion.State == ConnectionState.Open)
                {
                    conexion.Close();
                }
            }

            return lintResultado;

        }

        public int ActualizarCursoOnline(CursoOnline cursoOnline)
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
                    cmdSQL.CommandText = "USP_CursoOnline_UPD";
                    cmdSQL.Parameters.Add("@CursoOnlineId", SqlDbType.Int).Value = cursoOnline.CursoOnlineId;
                    cmdSQL.Parameters.Add("@Codigo", SqlDbType.VarChar, -1).Value = cursoOnline.Codigo;
                    cmdSQL.Parameters.Add("@Nombre", SqlDbType.VarChar, -1).Value = cursoOnline.Nombre;
                    cmdSQL.Parameters.Add("@CategoriaCursoOnlineId", SqlDbType.Int).Value = cursoOnline.CategoriaCursoOnlineId;
                    cmdSQL.Parameters.Add("@Estado", SqlDbType.VarChar, -1).Value = cursoOnline.Estado;
                    cmdSQL.Parameters.Add("@Descripcion", SqlDbType.VarChar, -1).Value = cursoOnline.Descripcion;
                    cmdSQL.Parameters.Add("@NumPreguntasEvaluacion", SqlDbType.Int).Value = cursoOnline.NumPreguntasEvaluacion;
                    cmdSQL.Parameters.Add("@TiempoEvaluacion", SqlDbType.Int).Value = cursoOnline.TiempoEvaluacion;
                    cmdSQL.Parameters.Add("@PorcentajeAprobacion", SqlDbType.Decimal).Value = cursoOnline.PorcentajeAprobacion;
                    cmdSQL.Parameters.Add("@RutaImagen", SqlDbType.VarChar, -1).Value = cursoOnline.RutaImagen;
                    cmdSQL.Parameters.Add("@TieneExamen", SqlDbType.VarChar, -1).Value = cursoOnline.TieneExamen;
                    cmdSQL.Parameters.Add("@AutoMatriculaHabilitada", SqlDbType.Bit).Value = cursoOnline.AutoMatriculaHabilitada;
                    cmdSQL.Parameters.Add("@PermiteReinicioScorm", SqlDbType.Bit).Value = cursoOnline.PermiteReinicioScorm;
                    cmdSQL.Parameters.Add("@RutaBanner", SqlDbType.VarChar, -1).Value = cursoOnline.RutaBanner;
                    cmdSQL.Parameters.Add("@FechaCreacion", SqlDbType.DateTime).Value = cursoOnline.FechaCreacion;
                    cmdSQL.Parameters.Add("@Ranking", SqlDbType.Decimal).Value = cursoOnline.Ranking;
                    cmdSQL.Parameters.Add("@NroRankings", SqlDbType.Int).Value = cursoOnline.NroRankings;
                    cmdSQL.Parameters.Add("@Orden", SqlDbType.Int).Value = cursoOnline.Orden;
                    cmdSQL.Parameters.Add("@DisponibilidadCurso", SqlDbType.Int).Value = cursoOnline.DisponibilidadCurso;

                    cmdSQL.Parameters.Add("@UsuarioCreacion", SqlDbType.Int).Value = cursoOnline.UsuarioCreacion ;
                    cmdSQL.Parameters.Add("@UsuarioProfesor", SqlDbType.Int).Value = cursoOnline.UsuarioProfesor;


                    lintResultado = cmdSQL.ExecuteNonQuery();
                }
                if (conexion.State == ConnectionState.Open)
                {
                    conexion.Close();
                }
            }

            return lintResultado;

        }

        public int ActualizarCursoOnlineMaestro(CursoOnlineMaestro cursoOnline)
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
                    cmdSQL.CommandText = "USP_CursoOnlineMaestro_UPD";
                    cmdSQL.Parameters.Add("@CursoOnlineMaestroId", SqlDbType.Int).Value = cursoOnline.CursoOnlineMaestroId;
                    cmdSQL.Parameters.Add("@Codigo", SqlDbType.VarChar, -1).Value = cursoOnline.Codigo;
                    cmdSQL.Parameters.Add("@Nombre", SqlDbType.VarChar, -1).Value = cursoOnline.Nombre;
                    cmdSQL.Parameters.Add("@CategoriaCursoOnlineId", SqlDbType.Int).Value = cursoOnline.CategoriaCursoOnlineId;
                    cmdSQL.Parameters.Add("@Estado", SqlDbType.VarChar, -1).Value = cursoOnline.Estado;
                    cmdSQL.Parameters.Add("@Descripcion", SqlDbType.VarChar, -1).Value = cursoOnline.Descripcion;
                    cmdSQL.Parameters.Add("@NumPreguntasEvaluacion", SqlDbType.Int).Value = cursoOnline.NumPreguntasEvaluacion;
                    cmdSQL.Parameters.Add("@TiempoEvaluacion", SqlDbType.Int).Value = cursoOnline.TiempoEvaluacion;
                    cmdSQL.Parameters.Add("@PorcentajeAprobacion", SqlDbType.Decimal).Value = cursoOnline.PorcentajeAprobacion;
                    cmdSQL.Parameters.Add("@RutaImagen", SqlDbType.VarChar, -1).Value = cursoOnline.RutaImagen;
                    cmdSQL.Parameters.Add("@TieneExamen", SqlDbType.VarChar, -1).Value = cursoOnline.TieneExamen;
                    cmdSQL.Parameters.Add("@AutoMatriculaHabilitada", SqlDbType.Bit).Value = cursoOnline.AutoMatriculaHabilitada;
                    cmdSQL.Parameters.Add("@PermiteReinicioScorm", SqlDbType.Bit).Value = cursoOnline.PermiteReinicioScorm;
                    cmdSQL.Parameters.Add("@RutaBanner", SqlDbType.VarChar, -1).Value = cursoOnline.RutaBanner;

                    //cmdSQL.Parameters.Add("@FechaCreacion", SqlDbType.DateTime).Value = cursoOnline.FechaCreacion;
                    //cmdSQL.Parameters.Add("@Ranking", SqlDbType.Decimal).Value = cursoOnline.Ranking;
                    //cmdSQL.Parameters.Add("@NroRankings", SqlDbType.Int).Value = cursoOnline.NroRankings;
                    //cmdSQL.Parameters.Add("@Orden", SqlDbType.Int).Value = cursoOnline.Orden;

                    //cmdSQL.Parameters.Add("@DisponibilidadCurso", SqlDbType.Int).Value = cursoOnline.DisponibilidadCurso;

                    //cmdSQL.Parameters.Add("@UsuarioCreacion", SqlDbType.Int).Value = cursoOnline.UsuarioCreacion;
                    //cmdSQL.Parameters.Add("@UsuarioProfesor", SqlDbType.Int).Value = cursoOnline.UsuarioProfesor;


                    lintResultado = cmdSQL.ExecuteNonQuery();
                }
                if (conexion.State == ConnectionState.Open)
                {
                    conexion.Close();
                }
            }

            return lintResultado;

        }

        public int ActualizarCursoGrupoMaestro(CursoGrupo cursoGrupo)
        {
            int lintResultado = 0;

            string cadenaConexion = Util.CadenaConexion;

            using (SqlConnection conexion = new SqlConnection(cadenaConexion))
            {
                conexion.Open();

                using (SqlCommand cmdSql = new SqlCommand())
                {
                    cmdSql.Connection = conexion;
                    cmdSql.CommandType = CommandType.StoredProcedure;
                    cmdSql.CommandText = "USP_CursoGrupo_UPD";
                    cmdSql.Parameters.Add("@CursoGrupoId", SqlDbType.Int).Value = cursoGrupo.CursoGrupoId;
                    cmdSql.Parameters.Add("@Nombre", SqlDbType.VarChar, -1).Value = cursoGrupo.Nombre;
                    cmdSql.Parameters.Add("@Estado", SqlDbType.VarChar, -1).Value = cursoGrupo.Estado;
                    cmdSql.Parameters.Add("@CursoOnlineId", SqlDbType.Int).Value = cursoGrupo.CursoOnlineId;

                    lintResultado = cmdSql.ExecuteNonQuery();
                }
                if (conexion.State == ConnectionState.Open)
                {
                    conexion.Close();
                }
            }

            return lintResultado;

        }

        public int ActualizarUsuarioGrupoMaestro(UsuarioGrupo usuarioGrupo)
        {
            int lintResultado = 0;

            string cadenaConexion = Util.CadenaConexion;

            using (SqlConnection conexion = new SqlConnection(cadenaConexion))
            {
                conexion.Open();

                using (SqlCommand cmdSql = new SqlCommand())
                {
                    cmdSql.Connection = conexion;
                    cmdSql.CommandType = CommandType.StoredProcedure;
                    cmdSql.CommandText = "USP_UsuarioGrupo_UPD";
                    cmdSql.Parameters.Add("@UsuarioGrupoId", SqlDbType.Int).Value = usuarioGrupo.UsuarioGrupoId;
                    cmdSql.Parameters.Add("@GrupoId", SqlDbType.Int).Value = usuarioGrupo.GrupoId;
                    cmdSql.Parameters.Add("@Nombre", SqlDbType.VarChar, -1).Value = usuarioGrupo.Nombre;
                    cmdSql.Parameters.Add("@Estado", SqlDbType.VarChar, -1).Value = usuarioGrupo.Estado;

                    lintResultado = cmdSql.ExecuteNonQuery();
                }
                if (conexion.State == ConnectionState.Open)
                {
                    conexion.Close();
                }
            }

            return lintResultado;
        }

        public int InsertarDisponibilidadCursoOnline(DisponibilidadCursoOnline disponibilidadCursoOnline)
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
                    cmdSQL.CommandText = "USP_DisponibilidadCursoOnline_INS";
                    cmdSQL.Parameters.Add("@CursoOnlineId", SqlDbType.Int).Value = disponibilidadCursoOnline.CursoOnlineId;
                    cmdSQL.Parameters.Add("@FechaInicio", SqlDbType.DateTime).Value = disponibilidadCursoOnline.FechaInicio;
                    cmdSQL.Parameters.Add("@FechaFin", SqlDbType.DateTime).Value = disponibilidadCursoOnline.FechaFin;

                    lintResultado = cmdSQL.ExecuteNonQuery();

                }
                if (conexion.State == ConnectionState.Open)
                {
                    conexion.Close();
                }
            }

            return lintResultado;

        }

        public int ActualizarCursoOnlineEncuesta(CursoOnlineEncuesta cursoOnlineEncuesta) {
            int lintResultado = 0;

            string cadenaConexion = Util.CadenaConexion;

            using (SqlConnection conexion = new SqlConnection(cadenaConexion))
            {
                conexion.Open();

                using (SqlCommand cmdSQL = new SqlCommand())
                {
                    cmdSQL.Connection = conexion;
                    cmdSQL.CommandType = CommandType.StoredProcedure;
                    cmdSQL.CommandText = "USP_CursoOnlineEncuesta_UPD";
                    cmdSQL.Parameters.Add("@CursoOnlineEncuestaId", SqlDbType.Int).Value = cursoOnlineEncuesta.CursoOnlineEncuestaId;
                    cmdSQL.Parameters.Add("@CursoOnlineId", SqlDbType.Int).Value = cursoOnlineEncuesta.CursoOnlineId;
                    cmdSQL.Parameters.Add("@Descripcion", SqlDbType.VarChar).Value = cursoOnlineEncuesta.Descripcion;
                    cmdSQL.Parameters.Add("@Nombre", SqlDbType.VarChar).Value = cursoOnlineEncuesta.Nombre;
                    cmdSQL.Parameters.Add("@Activo", SqlDbType.Bit).Value = cursoOnlineEncuesta.Activo;

                    lintResultado = cmdSQL.ExecuteNonQuery();

                }
                if (conexion.State == ConnectionState.Open)
                {
                    conexion.Close();
                }
            }

            return lintResultado;
        }

        public int InsertarCursoOnlineEncuesta(CursoOnlineEncuesta cursoOnlineEncuesta)
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
                    cmdSQL.CommandText = "USP_CursoOnlineEncuesta_INS";
                    cmdSQL.Parameters.Add("@Nombre", SqlDbType.VarChar).Value = cursoOnlineEncuesta.Nombre;
                    cmdSQL.Parameters.Add("@Descripcion", SqlDbType.VarChar).Value = cursoOnlineEncuesta.Descripcion;
                    cmdSQL.Parameters.Add("@CursoOnlineId", SqlDbType.Int).Value = cursoOnlineEncuesta.CursoOnlineId;
                    cmdSQL.Parameters.Add("@Activo", SqlDbType.Bit).Value = cursoOnlineEncuesta.Activo;

                    lintResultado =Convert.ToInt32(cmdSQL.ExecuteScalar().ToString());

                }
                if (conexion.State == ConnectionState.Open)
                {
                    conexion.Close();
                }
            }

            return lintResultado;
        }

        public int InsertarUnidadCursoOnline(UnidadCursoOnline unidadCursoOnline)
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
                    cmdSQL.CommandText = "USP_UnidadCursoOnline_INS";
                    //cmdSQL.Parameters.Add("@UnidadCursoOnlineId", SqlDbType.Int).Value = unidadCursoOnline.UnidadCursoOnlineId;
                    cmdSQL.Parameters.Add("@CursoOnlineId", SqlDbType.Int).Value = unidadCursoOnline.CursoOnlineId;
                    cmdSQL.Parameters.Add("@TipoUnidadId", SqlDbType.Int).Value = unidadCursoOnline.TipoUnidadId;
                    cmdSQL.Parameters.Add("@UnidadCursoOnlinePadreId", SqlDbType.Int).Value = unidadCursoOnline.UnidadCursoOnlinePadreId;
                    cmdSQL.Parameters.Add("@Nombre", SqlDbType.VarChar, -1).Value = unidadCursoOnline.Nombre;
                    cmdSQL.Parameters.Add("@Descripcion", SqlDbType.VarChar, -1).Value = unidadCursoOnline.Descripcion;
                    cmdSQL.Parameters.Add("@GUID", SqlDbType.VarChar, -1).Value = unidadCursoOnline.GUID;
                    cmdSQL.Parameters.Add("@RutaArchivoOriginal", SqlDbType.VarChar, -1).Value = unidadCursoOnline.RutaArchivoOriginal;
                    cmdSQL.Parameters.Add("@RutaArchivoInicio", SqlDbType.VarChar, -1).Value = unidadCursoOnline.RutaArchivoInicio;
                    cmdSQL.Parameters.Add("@RutaWeb", SqlDbType.VarChar, -1).Value = unidadCursoOnline.RutaWeb;
                    cmdSQL.Parameters.Add("@Orden", SqlDbType.Int).Value = unidadCursoOnline.Orden;
                    cmdSQL.Parameters.Add("@Estado", SqlDbType.VarChar, -1).Value = unidadCursoOnline.Estado;
                    cmdSQL.Parameters.Add("@TiempoPermanencia", SqlDbType.Int).Value = unidadCursoOnline.TiempoPermanencia;
                    cmdSQL.Parameters.Add("@AnchoContenedor", SqlDbType.Int).Value = unidadCursoOnline.AnchoContenedor;
                    cmdSQL.Parameters.Add("@AltoContenedor", SqlDbType.Int).Value = unidadCursoOnline.AltoContenedor;
                    cmdSQL.Parameters.Add("@TipoUnidad", SqlDbType.VarChar, -1).Value = unidadCursoOnline.TipoUnidad;
                    cmdSQL.Parameters.Add("@RutaArchivoAdicional", SqlDbType.VarChar, -1).Value = unidadCursoOnline.RutaArchivoAdicional;
                    cmdSQL.Parameters.Add("@ExtensionArchivoAdicional", SqlDbType.VarChar, -1).Value = unidadCursoOnline.ExtensionArchivoAdicional;
                    cmdSQL.Parameters.Add("@TipoContenidoArchivoAdicional", SqlDbType.VarChar, -1).Value = unidadCursoOnline.TipoContenidoArchivoAdicional;
                    cmdSQL.Parameters.Add("@HasTarea", SqlDbType.Bit, -1).Value = unidadCursoOnline.HasTarea;
                    cmdSQL.Parameters.Add("@TareaFechaLimite", SqlDbType.DateTime, -1).Value = unidadCursoOnline.TareaFechaLimite;
                    lintResultado = Convert.ToInt32(cmdSQL.ExecuteScalar().ToString());

                }
                if (conexion.State == ConnectionState.Open)
                {
                    conexion.Close();
                }
            }

            return lintResultado;

        }

        public int ActualizarUnidadCursoOnline(UnidadCursoOnline unidadCursoOnline)
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
                    cmdSQL.CommandText = "USP_UnidadCursoOnline_UPD";
                    cmdSQL.Parameters.Add("@UnidadCursoOnlineId", SqlDbType.Int).Value = unidadCursoOnline.UnidadCursoOnlineId;
                    cmdSQL.Parameters.Add("@CursoOnlineId", SqlDbType.Int).Value = unidadCursoOnline.CursoOnlineId;
                    cmdSQL.Parameters.Add("@TipoUnidadId", SqlDbType.Int).Value = unidadCursoOnline.TipoUnidadId;
                    cmdSQL.Parameters.Add("@UnidadCursoOnlinePadreId", SqlDbType.Int).Value = unidadCursoOnline.UnidadCursoOnlinePadreId;
                    cmdSQL.Parameters.Add("@Nombre", SqlDbType.VarChar, -1).Value = unidadCursoOnline.Nombre;
                    cmdSQL.Parameters.Add("@Descripcion", SqlDbType.VarChar, -1).Value = unidadCursoOnline.Descripcion;
                    cmdSQL.Parameters.Add("@GUID", SqlDbType.VarChar, -1).Value = unidadCursoOnline.GUID;
                    cmdSQL.Parameters.Add("@RutaArchivoOriginal", SqlDbType.VarChar, -1).Value = unidadCursoOnline.RutaArchivoOriginal;
                    cmdSQL.Parameters.Add("@RutaArchivoInicio", SqlDbType.VarChar, -1).Value = unidadCursoOnline.RutaArchivoInicio;
                    cmdSQL.Parameters.Add("@RutaWeb", SqlDbType.VarChar, -1).Value = unidadCursoOnline.RutaWeb;
                    cmdSQL.Parameters.Add("@Orden", SqlDbType.Int).Value = unidadCursoOnline.Orden;
                    cmdSQL.Parameters.Add("@Estado", SqlDbType.VarChar, -1).Value = unidadCursoOnline.Estado;
                    cmdSQL.Parameters.Add("@TiempoPermanencia", SqlDbType.Int).Value = unidadCursoOnline.TiempoPermanencia;
                    cmdSQL.Parameters.Add("@AnchoContenedor", SqlDbType.Int).Value = unidadCursoOnline.AnchoContenedor;
                    cmdSQL.Parameters.Add("@AltoContenedor", SqlDbType.Int).Value = unidadCursoOnline.AltoContenedor;
                    cmdSQL.Parameters.Add("@TipoUnidad", SqlDbType.VarChar, -1).Value = unidadCursoOnline.TipoUnidad;
                    cmdSQL.Parameters.Add("@RutaArchivoAdicional", SqlDbType.VarChar, -1).Value = unidadCursoOnline.RutaArchivoAdicional;
                    cmdSQL.Parameters.Add("@ExtensionArchivoAdicional", SqlDbType.VarChar, -1).Value = unidadCursoOnline.ExtensionArchivoAdicional;
                    cmdSQL.Parameters.Add("@TipoContenidoArchivoAdicional", SqlDbType.VarChar, -1).Value = unidadCursoOnline.TipoContenidoArchivoAdicional;
                    cmdSQL.Parameters.Add("@HasTarea", SqlDbType.Bit, -1).Value = unidadCursoOnline.HasTarea;
                    cmdSQL.Parameters.Add("@TareaFechaLimite", SqlDbType.DateTime, -1).Value = unidadCursoOnline.TareaFechaLimite;
                    lintResultado = cmdSQL.ExecuteNonQuery();
                }
                if (conexion.State == ConnectionState.Open)
                {
                    conexion.Close();
                }
            }
            return lintResultado;
        }

        public int InsertarAvanceMatriculaCursoOnline(AvanceMatriculaCursoOnline avanceMatriculaCursoOnline)
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
                    cmdSQL.CommandText = "USP_AvanceMatriculaCursoOnline_INS";
                    cmdSQL.Parameters.Add("@MatriculaCursoOnlineId", SqlDbType.Int).Value = avanceMatriculaCursoOnline.MatriculaCursoOnlineId;
                    cmdSQL.Parameters.Add("@UnidadCursoOnlineId", SqlDbType.Int).Value = avanceMatriculaCursoOnline.UnidadCursoOnlineId;
                    cmdSQL.Parameters.Add("@FechaInicio", SqlDbType.DateTime).Value = avanceMatriculaCursoOnline.FechaInicio;

                    lintResultado = Convert.ToInt32(cmdSQL.ExecuteScalar().ToString());

                }
                if (conexion.State == ConnectionState.Open)
                {
                    conexion.Close();
                }
            }

            return lintResultado;

        }

        public int ActualizarAvanceMatriculaCursoOnline(AvanceMatriculaCursoOnline avanceMatriculaCursoOnline)
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
                    cmdSQL.CommandText = "USP_AvanceMatriculaCursoOnline_UPD";
                    cmdSQL.Parameters.Add("@AvanceMatriculaCursoOnlineId", SqlDbType.Int).Value = avanceMatriculaCursoOnline.AvanceMatriculaCursoOnlineId;
                    cmdSQL.Parameters.Add("@FechaFin", SqlDbType.DateTime).Value = avanceMatriculaCursoOnline.FechaFin;

                    lintResultado = Convert.ToInt32(cmdSQL.ExecuteScalar().ToString());

                }
                if (conexion.State == ConnectionState.Open)
                {
                    conexion.Close();
                }
            }

            return lintResultado;

        }

        public int ActualizarMatriculaCursoOnline(MatriculaCursoOnline matriculaCursoOnline)
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
                    cmdSQL.CommandText = "USP_MatriculaCursoOnline_UPD";
                    cmdSQL.Parameters.Add("@MatriculaCursoOnlineId", SqlDbType.Int).Value = matriculaCursoOnline.MatriculaCursoOnlineId;
                    cmdSQL.Parameters.Add("@PorcentajeAvance", SqlDbType.Decimal).Value = matriculaCursoOnline.PorcentajeAvance;
                    cmdSQL.Parameters.Add("@Estado", SqlDbType.VarChar, -1).Value = matriculaCursoOnline.Estado;
                    cmdSQL.Parameters.Add("@FechaCompletado", SqlDbType.DateTime).Value = matriculaCursoOnline.FechaCompletado;

                    lintResultado = cmdSQL.ExecuteNonQuery();

                }
                if (conexion.State == ConnectionState.Open)
                {
                    conexion.Close();
                }
            }

            return lintResultado;

        }

        public List<ViewUsuarioMatriculaCursoOnline> ListarViewUsuarioMatriculaCursoOnline()
        {
            List<ViewUsuarioMatriculaCursoOnline> retVal = new List<ViewUsuarioMatriculaCursoOnline>();

            DataTable dt = new DataTable();

            string cadenaConexion = Util.CadenaConexion;

            using (SqlConnection conexion = new SqlConnection(cadenaConexion))
            {
                conexion.Open();

                using (SqlCommand cmdSQL = new SqlCommand())
                {
                    cmdSQL.Connection = conexion;
                    cmdSQL.CommandType = CommandType.StoredProcedure;
                    cmdSQL.CommandText = "USP_ViewUsuarioMatriculaCursoOnline_LIS";

                    //cmdSQL.Parameters.Add("@CursoOnlineId", SqlDbType.Int).Value = cursoOnlineId;

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
                ViewUsuarioMatriculaCursoOnline obj = new ViewUsuarioMatriculaCursoOnline();

                obj.UsuarioId = (int)(dr["UsuarioId"]);
                obj.NombreUsuario = dr["NombreUsuario"].ToString();
                obj.ApellidoUsuario = dr["ApellidoUsuario"].ToString();
                obj.EmailUsuario = dr["EmailUsuario"].ToString();
                obj.EmpresaId = (dr["EmpresaId"].ToString() != String.Empty ? (int?)(dr["EmpresaId"]) : null);
                obj.NombreCursoOnline = dr["NombreCursoOnline"].ToString();
                obj.PorcentajeAprobacionCursoOnline = (decimal)(dr["PorcentajeAprobacionCursoOnline"]);
                obj.CursoOnlineId = (int)(dr["CursoOnlineId"]);
                obj.MatriculaCursoOnlineId = (int)(dr["MatriculaCursoOnlineId"]);
                obj.FechaMatricula = (DateTime)(dr["FechaMatricula"]);
                obj.FechaCompletado = (dr["FechaCompletado"].ToString() != String.Empty ? (DateTime?)(dr["FechaCompletado"]) : null);
                obj.FechaAprobado = (dr["FechaAprobado"].ToString() != String.Empty ? (DateTime?)(dr["FechaAprobado"]) : null);
                obj.PorcentajeObtenido = (dr["PorcentajeObtenido"].ToString() != String.Empty ? (decimal?)(dr["PorcentajeObtenido"]) : null);

                retVal.Add(obj);
            }

            return retVal;
        }

        public List<ViewCertificadoAlumno> ListarViewCertificadoAlumno()
        {
            List<ViewCertificadoAlumno> retVal = new List<ViewCertificadoAlumno>();

            DataTable dt = new DataTable();

            string cadenaConexion = Util.CadenaConexion;

            using (SqlConnection conexion = new SqlConnection(cadenaConexion))
            {
                conexion.Open();

                using (SqlCommand cmdSQL = new SqlCommand())
                {
                    cmdSQL.Connection = conexion;
                    cmdSQL.CommandType = CommandType.StoredProcedure;
                    cmdSQL.CommandText = "USP_ViewCertificadoAlumno_LIS";

                    //cmdSQL.Parameters.Add("@CursoOnlineId", SqlDbType.Int).Value = cursoOnlineId;

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
                ViewCertificadoAlumno obj = new ViewCertificadoAlumno();

                obj.Nombre = dr["Nombre"].ToString();
                obj.UsuarioId = (int)(dr["UsuarioId"]); ;
                obj.CursoOnlineMaestroId = (int)(dr["CursoOnlineMaestroId"]); ;
                obj.PorcentajeObtenido = (dr["PorcentajeObtenido"].ToString() != String.Empty ? (decimal?)(dr["PorcentajeObtenido"]) : null);
                obj.PorcentajeAvance = (dr["PorcentajeAvance"].ToString() != String.Empty ? (decimal?)(dr["PorcentajeAvance"]) : null);
                obj.CantidadCursos = (dr["CantidadCursos"].ToString() != String.Empty ? (int?)(dr["CantidadCursos"]) : null);



                retVal.Add(obj);
            }

            return retVal;
        }

        public List<ViewCertificadoAlumno> ListarViewCertificadoTurismoAlumno()
        {
            List<ViewCertificadoAlumno> retVal = new List<ViewCertificadoAlumno>();

            DataTable dt = new DataTable();

            string cadenaConexion = Util.CadenaConexion;

            using (SqlConnection conexion = new SqlConnection(cadenaConexion))
            {
                conexion.Open();

                using (SqlCommand cmdSQL = new SqlCommand())
                {
                    cmdSQL.Connection = conexion;
                    cmdSQL.CommandType = CommandType.StoredProcedure;
                    cmdSQL.CommandText = "USP_ViewCertificadoTurismoAlumno_LIS";

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
                ViewCertificadoAlumno obj = new ViewCertificadoAlumno();

                obj.Nombre = dr["Nombre"].ToString();
                obj.UsuarioId = (int)(dr["UsuarioId"]); ;
                obj.CursoOnlineId = (int)(dr["CursoOnlineId"]); ;
                obj.PorcentajeObtenido = (dr["PorcentajeObtenido"].ToString() != String.Empty ? (decimal?)(dr["PorcentajeObtenido"]) : null);
                obj.PorcentajeAvance = (dr["PorcentajeAvance"].ToString() != String.Empty ? (decimal?)(dr["PorcentajeAvance"]) : null);

                retVal.Add(obj);
            }

            return retVal;
        }

        public List<ViewUsuarioCursoMaestroReporte> ListarViewUsuarioCursoMaestroReporte()
        {
            List<ViewUsuarioCursoMaestroReporte> retVal = new List<ViewUsuarioCursoMaestroReporte>();

            DataTable dt = new DataTable();

            string cadenaConexion = Util.CadenaConexion;

            using (SqlConnection conexion = new SqlConnection(cadenaConexion))
            {
                conexion.Open();

                using (SqlCommand cmdSQL = new SqlCommand())
                {
                    cmdSQL.Connection = conexion;
                    cmdSQL.CommandType = CommandType.StoredProcedure;
                    cmdSQL.CommandText = "USP_ViewUsuarioCursoMaestroReporte_LIS";

                    //cmdSQL.Parameters.Add("@CursoOnlineId", SqlDbType.Int).Value = cursoOnlineId;

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
                ViewUsuarioCursoMaestroReporte obj = new ViewUsuarioCursoMaestroReporte();

                obj.NombreCurso = dr["NombreCurso"].ToString();
                obj.UsuarioId = (int)(dr["UsuarioId"]);
                obj.RazonSocial = dr["RazonSocial"].ToString();
                obj.Apellido = dr["Apellido"].ToString();
                obj.Nombre = dr["Nombre"].ToString();
                obj.CursoOnlineMaestroId = (int)(dr["CursoOnlineMaestroId"]);
                obj.PorcentajeAvance = (decimal)(dr["PorcentajeAvance"]);

                retVal.Add(obj);
            }

            return retVal;
        }

        public List<ViewUsuarioCursoMaestroReporte> ListarViewUsuarioCursoTurismoReporte()
        {
            List<ViewUsuarioCursoMaestroReporte> retVal = new List<ViewUsuarioCursoMaestroReporte>();

            DataTable dt = new DataTable();

            string cadenaConexion = Util.CadenaConexion;

            using (SqlConnection conexion = new SqlConnection(cadenaConexion))
            {
                conexion.Open();

                using (SqlCommand cmdSQL = new SqlCommand())
                {
                    cmdSQL.Connection = conexion;
                    cmdSQL.CommandType = CommandType.StoredProcedure;
                    cmdSQL.CommandText = "USP_ViewUsuarioCursoTurismoReporte_LIS"; 

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
                ViewUsuarioCursoMaestroReporte obj = new ViewUsuarioCursoMaestroReporte();

                obj.NombreCurso = dr["NombreCurso"].ToString();
                obj.UsuarioId = (int)(dr["UsuarioId"]);
                obj.RazonSocial = dr["RazonSocial"].ToString();
                obj.Apellido = dr["Apellido"].ToString();
                obj.Nombre = dr["Nombre"].ToString();
                obj.CursoOnlineId = (int)(dr["CursoOnlineId"]);
                obj.PorcentajeAvance = (decimal)(dr["PorcentajeAvance"]);

                retVal.Add(obj);
            }

            return retVal;
        }

        public int InsertarLogCertificado(LogCertificado logCertificado)
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
                    cmdSQL.CommandText = "USP_LogCertificado_INS";

                    //cmdSQL.Parameters.Add("@LogCertificadoId", SqlDbType.Int).Value = logCertificado.LogCertificadoId;
                    cmdSQL.Parameters.Add("@UsuarioId", SqlDbType.Int).Value = logCertificado.UsuarioId;
                    //cmdSQL.Parameters.Add("@FechaDescarga", SqlDbType.DateTime).Value = logCertificado.FechaDescarga;
                    cmdSQL.Parameters.Add("@MatriculaCursoOnlineId", SqlDbType.Int).Value = logCertificado.MatriculaCursoOnlineId;

                    lintResultado = cmdSQL.ExecuteNonQuery();//Convert.ToInt32(cmdSQL.ExecuteScalar().ToString());

                }
                if (conexion.State == ConnectionState.Open)
                {
                    conexion.Close();
                }
            }

            return lintResultado;
        }

        public List<CursoOnlineMaestro> SPUSP_AddCursoMaestro_LIS(int? grupoId)
        {
            List<CursoOnlineMaestro> retVal = new List<CursoOnlineMaestro>();

            DataTable dt = new DataTable();

            string cadenaConexion = Util.CadenaConexion;

            using (SqlConnection conexion = new SqlConnection(cadenaConexion))
            {
                conexion.Open();

                using (SqlCommand cmdSQL = new SqlCommand())
                {
                    cmdSQL.Connection = conexion;
                    cmdSQL.CommandType = CommandType.StoredProcedure;
                    cmdSQL.CommandText = "USP_AddCursoMaestro_LIS";

                    cmdSQL.Parameters.Add("@GrupoId", SqlDbType.Int).Value = grupoId ?? 0;

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
                CursoOnlineMaestro obj = new CursoOnlineMaestro();


                obj.CursoOnlineMaestroId = (int)dr["CursoOnlineMaestroId"];
                obj.Codigo = dr["Codigo"].ToString();
                obj.Nombre = dr["Nombre"].ToString();
                obj.CategoriaCursoOnlineId = (int)dr["NombreCurso"];
                obj.Estado = dr["Estado"].ToString();
                obj.Descripcion = dr["Descripcion"].ToString();
                obj.NumPreguntasEvaluacion = (int)dr["NombreCurso"];
                obj.TiempoEvaluacion = (int)dr["NombreCurso"];
                obj.PorcentajeAprobacion = (decimal)dr["NombreCurso"];
                obj.RutaImagen = dr["RutaImagen"].ToString();
                obj.TieneExamen = dr["TieneExamen"].ToString();
                obj.AutoMatriculaHabilitada = (bool)dr["NombreCurso"];
                obj.PermiteReinicioScorm = (dr["PermiteReinicioScorm"].ToString() != String.Empty ? (bool?)(dr["PermiteReinicioScorm"]) : null);
                obj.RutaBanner = dr["RutaBanner"].ToString();
                obj.FechaCreacion = (dr["FechaCreacion"].ToString() != String.Empty ? (DateTime?)(dr["FechaCreacion"]) : null);
                obj.Ranking = (dr["Ranking"].ToString() != String.Empty ? (decimal?)(dr["Ranking"]) : null);
                obj.NroRankings = (dr["NroRankings"].ToString() != String.Empty ? (int?)(dr["NroRankings"]) : null);
                obj.Orden = (dr["Orden"].ToString() != String.Empty ? (int?)(dr["Orden"]) : null);
                obj.CantidadCursos = (dr["CantidadCursos"].ToString() != String.Empty ? (int?)(dr["CantidadCursos"]) : null);




                retVal.Add(obj);
            }

            return retVal;
        }

        public List<CursoGrupo> ListarCursoGrupoGrupo()
        {
            List<CursoGrupo> retVal = new List<CursoGrupo>();

            DataTable dt = new DataTable();

            string cadenaConexion = Util.CadenaConexion;

            using (SqlConnection conexion = new SqlConnection(cadenaConexion))
            {
                conexion.Open();

                using (SqlCommand cmdSQL = new SqlCommand())
                {
                    cmdSQL.Connection = conexion;
                    cmdSQL.CommandType = CommandType.StoredProcedure;
                    cmdSQL.CommandText = "USP_CursoGrupo_LIS";

                    //cmdSQL.Parameters.Add("@CursoOnlineId", SqlDbType.Int).Value = grupoId ?? 0;

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
                CursoGrupo obj = new CursoGrupo
                {
                    CursoGrupoId = (int)dr["CursoGrupoId"],
                    GrupoId = (dr["GrupoId"].ToString() != String.Empty ? (int?)(dr["GrupoId"]) : null),
                    CursoOnlineMaestroId = (dr["CursoOnlineMaestroId"].ToString() != String.Empty ? (int?)(dr["CursoOnlineMaestroId"]) : null),
                    Nombre = dr["Nombre"].ToString(),
                    Estado = dr["Estado"].ToString(),
                    CursoOnlineId = (dr["CursoOnlineId"].ToString() != string.Empty ? (int?)(dr["CursoOnlineId"]) : null)
                };

                Grupo addGrupo = ObtenerGrupo(obj.GrupoId);
                CursoOnlineMaestro cursoOnlineMaestro = ObtenerCursoOnlineMaestroPorId(obj.CursoOnlineMaestroId);

                obj.Grupo = addGrupo.FechaCreacion == null ? null : addGrupo;
                obj.CursoOnlineMaestro = cursoOnlineMaestro;

                retVal.Add(obj);
            }

            return retVal;
        }

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
                    cmdSQL.CommandText = "USP_ViewEnlacesInteres_LIS";

                    //cmdSQL.Parameters.Add("@CursoOnlineId", SqlDbType.Int).Value = grupoId ?? 0;

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
                EnlacesInteres obj = new EnlacesInteres();


                obj.EnlaceInteresId = (int)dr["EnlaceInteresId"];
                //obj.GrupoId = (dr["GrupoId"].ToString() != String.Empty ? (int?)(dr["GrupoId"]) : null);
                //obj.CursoOnlineMaestroId = (dr["CursoOnlineMaestroId"].ToString() != String.Empty ? (int?)(dr["CursoOnlineMaestroId"]) : null);
                obj.Titulo = dr["Titulo"].ToString();
                obj.Imagen = dr["Imagen"].ToString();

                obj.Descripcion = dr["Descripcion"].ToString();
                obj.Tipo = dr["Tipo"].ToString();
                obj.CodigoYoutube = dr["CodigoYoutube"].ToString();
                obj.Url = dr["Url"].ToString();
                obj.Estado = dr["Estado"].ToString();
                obj.Pdf = dr["Pdf"].ToString();


                //obj.Grupo = addGrupo.FechaCreacion == null ? null : addGrupo;

                retVal.Add(obj);
            }

            return retVal;
        }

        public Grupo ObtenerGrupo(int? grupoId)
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
                    cmdSQL.CommandText = "USP_Grupo_SEL";

                    cmdSQL.Parameters.Add("@GrupoId", SqlDbType.Int).Value = grupoId ?? 0;

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


                retVal.GrupoId = (int)dr["GrupoId"];
                retVal.Descripcion = dr["Descripcion"].ToString();
                retVal.Estado = dr["Estado"].ToString();

                retVal.FechaCreacion = (dr["FechaCreacion"].ToString() != String.Empty ? (DateTime?)(dr["FechaCreacion"]) : null);
                retVal.Aforo = (dr["Aforo"].ToString() != String.Empty ? (int?)(dr["Aforo"]) : null);
                retVal.TipoRegistro = dr["Estado"].ToString();


            }

            return retVal;
        }

        public int InsertarPreguntaCursoOnline(PreguntaCursoOnline preguntaCursoOnline)
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
                    cmdSQL.CommandText = "USP_PreguntaCursoOnline_INS";

                    cmdSQL.Parameters.Add("@CursoOnlineId", SqlDbType.Int).Value = preguntaCursoOnline.CursoOnlineId;
                    cmdSQL.Parameters.Add("@Tipo", SqlDbType.VarChar, -1).Value = preguntaCursoOnline.Tipo;
                    cmdSQL.Parameters.Add("@NumRespuestas", SqlDbType.Int).Value = preguntaCursoOnline.NumRespuestas;
                    cmdSQL.Parameters.Add("@Texto", SqlDbType.VarChar, -1).Value = preguntaCursoOnline.Texto;
                    cmdSQL.Parameters.Add("@Puntaje", SqlDbType.Int).Value = preguntaCursoOnline.Puntaje;
                    cmdSQL.Parameters.Add("@Estado", SqlDbType.VarChar, -1).Value = preguntaCursoOnline.Estado;
                    cmdSQL.Parameters.Add("@UnidadOnlineId", SqlDbType.Int).Value = preguntaCursoOnline.UnidadOnlineId;

                    lintResultado = Convert.ToInt32(cmdSQL.ExecuteScalar().ToString());

                }
                if (conexion.State == ConnectionState.Open)
                {
                    conexion.Close();
                }
            }

            return lintResultado;
        }

        public int ActualizarPreguntaCursoOnline(PreguntaCursoOnline preguntaCursoOnline)
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
                    cmdSQL.CommandText = "USP_PreguntaCursoOnline_UPD";

                    cmdSQL.Parameters.Add("@PreguntaCursoOnlineId", SqlDbType.Int).Value = preguntaCursoOnline.PreguntaCursoOnlineId;
                    cmdSQL.Parameters.Add("@CursoOnlineId", SqlDbType.Int).Value = preguntaCursoOnline.CursoOnlineId;
                    cmdSQL.Parameters.Add("@Tipo", SqlDbType.VarChar, -1).Value = preguntaCursoOnline.Tipo;
                    cmdSQL.Parameters.Add("@NumRespuestas", SqlDbType.Int).Value = preguntaCursoOnline.NumRespuestas;
                    cmdSQL.Parameters.Add("@Texto", SqlDbType.VarChar, -1).Value = preguntaCursoOnline.Texto;
                    cmdSQL.Parameters.Add("@Puntaje", SqlDbType.Int).Value = preguntaCursoOnline.Puntaje;
                    cmdSQL.Parameters.Add("@Estado", SqlDbType.VarChar, -1).Value = preguntaCursoOnline.Estado;
                    cmdSQL.Parameters.Add("@UnidadOnlineId", SqlDbType.Int).Value = preguntaCursoOnline.UnidadOnlineId;

                    lintResultado = cmdSQL.ExecuteNonQuery();

                }
                if (conexion.State == ConnectionState.Open)
                {
                    conexion.Close();
                }
            }

            return lintResultado;
        }

        public int ActualizarRespuestaCursoOnline(RespuestaCursoOnline respuestaCursoOnline)
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
                    cmdSQL.CommandText = "USP_RespuestaCursoOnline_UPD";

                    cmdSQL.Parameters.Add("@PreguntaCursoOnlineId", SqlDbType.Int).Value = respuestaCursoOnline.PreguntaCursoOnlineId;
                    cmdSQL.Parameters.Add("@Texto", SqlDbType.VarChar, -1).Value = respuestaCursoOnline.Texto;
                    cmdSQL.Parameters.Add("@EsSolucion", SqlDbType.Bit).Value = respuestaCursoOnline.EsSolucion;
                    cmdSQL.Parameters.Add("@OrdenSolucion", SqlDbType.Int).Value = respuestaCursoOnline.OrdenSolucion;
                    cmdSQL.Parameters.Add("@RespuestaCursoOnlineId", SqlDbType.Int).Value = respuestaCursoOnline.RespuestaCursoOnlineId;

                    lintResultado = cmdSQL.ExecuteNonQuery();

                }
                if (conexion.State == ConnectionState.Open)
                {
                    conexion.Close();
                }
            }

            return lintResultado;
        }

        public int InsertarRespuestaCursoOnline(RespuestaCursoOnline respuestaCursoOnline)
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
                    cmdSQL.CommandText = "USP_RespuestaCursoOnline_INS";

                    cmdSQL.Parameters.Add("@PreguntaCursoOnlineId", SqlDbType.Int).Value = respuestaCursoOnline.PreguntaCursoOnline.PreguntaCursoOnlineId ;
                    cmdSQL.Parameters.Add("@Texto", SqlDbType.VarChar, -1).Value = respuestaCursoOnline.Texto;
                    cmdSQL.Parameters.Add("@EsSolucion", SqlDbType.Bit).Value = respuestaCursoOnline.EsSolucion;
                    cmdSQL.Parameters.Add("@OrdenSolucion", SqlDbType.Int).Value = respuestaCursoOnline.OrdenSolucion;


                    lintResultado = Convert.ToInt32(cmdSQL.ExecuteScalar().ToString());

                }
                if (conexion.State == ConnectionState.Open)
                {
                    conexion.Close();
                }
            }

            return lintResultado;
        }

        public int InsertarEvaluacionCursoOnline(EvaluacionCursoOnline evaluacion)
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
                    cmdSQL.CommandText = "USP_EvaluacionCursoOnline_INS";


                    //cmdSQL.Parameters.Add("@EvaluacionCursoOnlineId", SqlDbType.Int).Value = evaluacion.EvaluacionCursoOnlineId;
                    cmdSQL.Parameters.Add("@MatriculaCursoOnlineId", SqlDbType.Int).Value = evaluacion.MatriculaCursoOnlineId;
                    cmdSQL.Parameters.Add("@UnidadCursoOnlineId", SqlDbType.Int).Value = evaluacion.UnidadCursoOnlineId;
                    cmdSQL.Parameters.Add("@FechaInicio", SqlDbType.DateTime).Value = evaluacion.FechaInicio;
                    cmdSQL.Parameters.Add("@FechaFin", SqlDbType.DateTime).Value = evaluacion.FechaFin;
                    cmdSQL.Parameters.Add("@Nota", SqlDbType.Decimal).Value = evaluacion.Nota;
                    cmdSQL.Parameters.Add("@PorcentajeAprobacion", SqlDbType.Decimal).Value = evaluacion.PorcentajeAprobacion;
                    cmdSQL.Parameters.Add("@PuntajeTotal", SqlDbType.Decimal).Value = evaluacion.PuntajeTotal;
                    cmdSQL.Parameters.Add("@PuntajeObtenido", SqlDbType.Decimal).Value = evaluacion.PuntajeObtenido;

                    lintResultado = Convert.ToInt32(cmdSQL.ExecuteScalar().ToString());

                }
                if (conexion.State == ConnectionState.Open)
                {
                    conexion.Close();
                }
            }

            return lintResultado;
        }

        public int ActualizarEvaluacionCursoOnline(EvaluacionCursoOnline evaluacion)
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
                    cmdSQL.CommandText = "USP_EvaluacionCursoOnline_UPD";


                    cmdSQL.Parameters.Add("@EvaluacionCursoOnlineId", SqlDbType.Int).Value = evaluacion.EvaluacionCursoOnlineId;
                    cmdSQL.Parameters.Add("@MatriculaCursoOnlineId", SqlDbType.Int).Value = evaluacion.MatriculaCursoOnlineId;
                    cmdSQL.Parameters.Add("@UnidadCursoOnlineId", SqlDbType.Int).Value = evaluacion.UnidadCursoOnlineId;
                    cmdSQL.Parameters.Add("@FechaInicio", SqlDbType.DateTime).Value = evaluacion.FechaInicio;
                    cmdSQL.Parameters.Add("@FechaFin", SqlDbType.DateTime).Value = evaluacion.FechaFin;
                    cmdSQL.Parameters.Add("@Nota", SqlDbType.Decimal).Value = evaluacion.Nota;
                    cmdSQL.Parameters.Add("@PorcentajeAprobacion", SqlDbType.Decimal).Value = evaluacion.PorcentajeAprobacion;
                    cmdSQL.Parameters.Add("@PuntajeTotal", SqlDbType.Decimal).Value = evaluacion.PuntajeTotal;
                    cmdSQL.Parameters.Add("@PuntajeObtenido", SqlDbType.Decimal).Value = evaluacion.PuntajeObtenido;


                    lintResultado = cmdSQL.ExecuteNonQuery();

                }
                if (conexion.State == ConnectionState.Open)
                {
                    conexion.Close();
                }
            }

            return lintResultado;
        }

        public int InsertarPreguntaEvaluacionCursoOnline(PreguntaEvaluacionCursoOnline pregunta)
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
                    cmdSQL.CommandText = "USP_PreguntaEvaluacionCursoOnline_INS";

                    cmdSQL.Parameters.Add("@EvaluacionCursoOnlineId", SqlDbType.Int).Value = pregunta.EvaluacionCursoOnlineId;
                    cmdSQL.Parameters.Add("@PreguntaCursoOnlineId", SqlDbType.Int).Value = pregunta.PreguntaCursoOnlineId;
                    cmdSQL.Parameters.Add("@TieneRespuestaCorrecta", SqlDbType.Bit).Value = pregunta.TieneRespuestaCorrecta;


                    lintResultado = Convert.ToInt32(cmdSQL.ExecuteScalar().ToString());

                }
                if (conexion.State == ConnectionState.Open)
                {
                    conexion.Close();
                }
            }

            return lintResultado;
        }

        public int InsertarRespuestaEvaluacionCursoOnline(RespuestaEvaluacionCursoOnline respuestaEvaluacion)
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
                    cmdSQL.CommandText = "USP_RespuestaEvaluacionCursoOnline_INS";

                    cmdSQL.Parameters.Add("@PreguntaEvaluacionCursoOnlineId", SqlDbType.Int).Value = respuestaEvaluacion.PreguntaEvaluacionCursoOnlineId;
                    cmdSQL.Parameters.Add("@RespuestaCursoOnlineId", SqlDbType.Int).Value = respuestaEvaluacion.RespuestaCursoOnlineId;
                    cmdSQL.Parameters.Add("@OrdenMostrado", SqlDbType.Int).Value = respuestaEvaluacion.OrdenMostrado;


                    lintResultado = Convert.ToInt32(cmdSQL.ExecuteScalar().ToString());

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
