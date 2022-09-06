using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using PROMPERU.AULAVIRTUAL.DA;
using System.Data;
using PROMPERU.AULAVIRTUAL.BE.CURSOS;
using PROMPERU.AULAVIRTUAL.BE.USUARIO;

namespace PROMPERU.AULAVIRTUAL.BL
{
    public class CursosBL
    {
        public List<CursoOnline> ListarCursoOnline()
        {
            CursosDA SQL = new CursosDA();
            return SQL.ListarCursoOnline();

        }
        public CursoOnlineEncuesta ListarCursoOnlineEncuesta(int cursoOnlineId)
        {
            CursosDA SQL = new CursosDA();
            return SQL.ObtenerEncuesta(cursoOnlineId);

        }
        public List<DetalleCursoOnlineEncuesta> ListarCursoOnlineEncuestaDetalle(int cursoOnlineEncuestaId)
        {
            CursosDA SQL = new CursosDA();
            return SQL.ObtenerEncuestaDetalle(cursoOnlineEncuestaId);

        }
        public List<CursoOnline> AddCursoOnline(int cursoOnlineMaestro)
        {
            CursosDA SQL = new CursosDA();
            return SQL.AddCursoOnline(cursoOnlineMaestro);
        }

        public List<ViewCursoMatricula> ListaViewCursoMatricula()
        {
            CursosDA SQL = new CursosDA();
            return SQL.ListaViewCursoMatricula();
        }

        public List<CursoOnline> ListarCursoOnlineDetalleCursoOnlineMaestro(int cursoOnlineId)
        {
            CursosDA SQL = new CursosDA();
            return SQL.ListarCursoOnlineDetalleCursoOnlineMaestro(cursoOnlineId);
        }

        public List<MatriculaCursoOnline> ListarMatriculaCursoOnlinePorCursoOnlineId(int? cursoOnlineId, string iNACTIVO)
        {
            CursosDA SQL = new CursosDA();
            return SQL.ListarMatriculaCursoOnlinePorCursoOnlineId(cursoOnlineId, iNACTIVO);
        }

        public VideoUnidadCursoOnline ObtenerVideoUnidadCursoOnlinePorId(int? videoUnidadCursoOnlineId)
        {
            CursosDA SQL = new CursosDA();
            return SQL.ObtenerVideoUnidadCursoOnlinePorId(videoUnidadCursoOnlineId);
            
        }

        public List<MatriculaCursoOnline> ListarMatriculaCursoOnlinePorCursoOnlineId(string iNACTIVO)
        {
            CursosDA SQL = new CursosDA();
            return SQL.ListarMatriculaCursoOnlinePorCursoOnlineId(iNACTIVO);
        }

        public int InsertarCursoOnlineVisto(int cursoOnlineId, int usuarioId)
        {
            CursosDA SQL = new CursosDA();
            return SQL.InsertarCursoOnlineVisto(cursoOnlineId, usuarioId);
        }

        public List<CursoOnline> ListarCursoOnlineDetalleCursoOnlineMaestroPorCursoOnlineMaestroId(int cursoOnlineMaestroId)
        {
            CursosDA SQL = new CursosDA();
            return SQL.ListarCursoOnlineDetalleCursoOnlineMaestro(cursoOnlineMaestroId);

        }

        public List<CursoOnlineResponse> ListarCursoOnlineRanking()
        {
            CursosDA SQL = new CursosDA();
            return SQL.ListarCursosOnlineRankingResponse();
        }

  
        public List<TipoUnidad> ListarTipoUnidad()
        {
            CursosDA SQL = new CursosDA();
            return SQL.ListarTipoUnidad();
        }

        public List<CursoOnlineVisto> ObtenerCursosVisto(int usuarioId)
        {
            CursosDA SQL = new CursosDA();
            return SQL.ObtenerCursosVisto(usuarioId);
        }

        public CursoOnlineEncuestaRespuesta ObtenerCursoOnlineEncuestaRespuesta(int matriculaCursoOnlineId)
        {
            CursosDA SQL = new CursosDA();
            return SQL.ObtenerCursoOnlineEncuestaRespuesta(matriculaCursoOnlineId);
        }

        public List<CursoOnline> ListarCursosUsuarioDisponible(int usuarioId)
        {
            CursosDA SQL = new CursosDA();
            return SQL.ListarCursosUsuarioPorNoUsuarioId(usuarioId);
        }
        public List<MatriculaCursoOnline> ListarMatriculaCursoOnline(int usuarioId)
        {
            CursosDA SQL = new CursosDA();
            return SQL.ListarMatriculaCursoOnline(usuarioId);
        }

        
        public List<MatriculaCursoOnline> ListarMatriculaCursoOnlinePorEmpresaId(int empresaId)
        {
            CursosDA SQL = new CursosDA();
            return SQL.ListarMatriculaCursoOnlinePorEmpresaId(empresaId);
        }

        public List<DisponibilidadCursoOnline> ListarDisponibilidadCursoOnlinePorCursoOnlineMatricula(int usuarioId)
        {
            CursosDA SQL = new CursosDA();
            return SQL.ListarDisponibilidadCursoOnlinePorCursoOnlineMatricula(usuarioId);
        }

        public List<DisponibilidadCursoOnline> ListarDisponibilidadCursoOnline()
        {
            CursosDA SQL = new CursosDA();
            return SQL.ListarDisponibilidadCursoOnline();
        }       

        public List<DisponibilidadCursoOnline> ListarDisponibilidadCursoOnlineUsuarioId(int usuarioId)
        {
            CursosDA SQL = new CursosDA();
            return SQL.ListarDisponibilidadCursoOnlineUsuarioId(usuarioId);
        }

        public int ContarEvaluacionCursoOnlinePorUnidadCursoOnlineId(int unidadCursoOnlineId)
        {
            CursosDA SQL = new CursosDA();
            return SQL.ContarEvaluacionCursoOnlinePorUnidadCursoOnlineId(unidadCursoOnlineId);
        }

        public DisponibilidadCursoOnline ObtenerDisponibilidadCursoOnlinePorId(int cursoOnlineId)
        {
            CursosDA SQL = new CursosDA();
            return SQL.ObtenerDisponibilidadCursoOnlinePorCursoOnlineId(cursoOnlineId);
        }

        public List<CursoOnlineEncuestaResultado> ListarCursoOnlineEncuestaResultado(int cursoOnlineId)
        {
            CursosDA SQL = new CursosDA();
            return SQL.ListarCursoOnlineEncuestaResultado(cursoOnlineId);
        }

        public List<int> ListarDisponibilidadCursoOnlineId()
        {
            CursosDA SQL = new CursosDA();
            return SQL.ListarDisponibilidadCursoOnlineId();

        }

        public CursoOnline ObtenerCursoOnlinePorId(int cursoOnlineId)
        {
            CursosDA SQL = new CursosDA();
            return SQL.ObtenerCursoOnlinePorId(cursoOnlineId);
        }

        public List<MatriculaCursoOnline> ListarMatriculaCursoOnlinePorCursoOnlineIdPorUsuarioId(int cursoOnlineId, int usuarioId)
        {
            CursosDA SQL = new CursosDA();
            return SQL.ListarMatriculaCursoOnlinePorCursoOnlineIdPorUsuarioId(cursoOnlineId, usuarioId);
        }

        public List<VideoUnidadCursoOnline> ListarVideoUnidadCursoOnline(int unidadCursoOnlineId)
        {
            CursosDA SQL = new CursosDA();
            return SQL.ListarVideoUnidadCursoOnline(unidadCursoOnlineId);
        }

        public List<CategoriaCursoOnline> ListarCategoriaCursoOnline()
        {
            CursosDA SQL = new CursosDA();
            return SQL.ListarCategoriaCursoOnline();
        }

        public CategoriaCursoOnline ObtenerCategoriaCursoOnline(int categoriaCursoOnlineId)
        {
            CursosDA SQL = new CursosDA();
            return SQL.ObtenerCategoriaCursoOnline(categoriaCursoOnlineId);
        }

        public List<UnidadCursoOnline> ListarUnidadCursoOnlinePorCursoOnlineId(int cursoOnlineId)
        {
            CursosDA SQL = new CursosDA();
            return SQL.ListarUnidadCursoOnlinePorCursoOnlineId(cursoOnlineId);
        }

        public List<TareaUnidadCursoOnline> ListarTareaUnidadCursoOnlinePorUnidadCursoOnlineId(int unidadCursoOnlineId, int alumnoId)
        {
            CursosDA sql = new CursosDA();
            return sql.ListarTareaUnidadCursoOnlinePorUnidadCursoOnlineId(unidadCursoOnlineId, alumnoId);
        }

        public int InsertarTareaUnidadCursoOnline(TareaUnidadCursoOnline tarea)
        {
            CursosDA sql = new CursosDA();
            return sql.InsertarTareaUnidadCursoOnline(tarea);
        }

        public UnidadCursoOnline ObtenerUnidadCursoOnlinePorId(int unidadCursoOnlineId)
        {
            CursosDA SQL = new CursosDA();
            return SQL.ObtenerUnidadCursoOnlinePorId(unidadCursoOnlineId);
        }



        public List<AvanceMatriculaCursoOnline> ListarAvanceMatriculaCursoOnlinePorMatriculaCursoOnlineId(int matriculaCursoOnlineId)
        {
            CursosDA SQL = new CursosDA();
            return SQL.ListarAvanceMatriculaCursoOnlinePorMatriculaCursoOnlineId(matriculaCursoOnlineId);
        }

        public List<AvanceMatriculaCursoOnline> ListarAvanceMatriculaCursoOnlinePorUnidadCursoOnlineId(int unidadCursoOnlineId)
        {
            CursosDA SQL = new CursosDA();
            return SQL.ListarAvanceMatriculaCursoOnlinePorUnidadCursoOnlineId(unidadCursoOnlineId);
        }

        public CursoOnlineMaestro ObtenerCursoOnlineMaestroPorId(int? cursoOnlineMaestroId)
        {
            CursosDA SQL = new CursosDA();
            return SQL.ObtenerCursoOnlineMaestroPorId(cursoOnlineMaestroId);
        }

        public List<EvaluacionCursoOnline> ListarEvaluacionCursoOnlinePorCursoOnlineId(int cursoOnlineId)
        {
            CursosDA SQL = new CursosDA();
            return SQL.ListarEvaluacionCursoOnlinePorCursoOnlineId(cursoOnlineId);
        }

        public List<EvaluacionCursoOnline> ListarEvaluacionCursoOnlinePorUsuarioId(int usuarioId)
        {
            CursosDA SQL = new CursosDA();
            return SQL.ListarEvaluacionCursoOnlinePorUsuarioId(usuarioId);
        }


        public EvaluacionCursoOnline ObtenerEvaluacionCursoOnlinePorId(int evaluacionCursoOnlineId)
        {
            CursosDA SQL = new CursosDA();
            return SQL.ObtenerEvaluacionCursoOnlinePorId(evaluacionCursoOnlineId);
        }

        public MatriculaCursoOnline ObtenerMatriculaCursoOnlinePorId(int matriculaCursoOnlineId)
        {
            CursosDA SQL = new CursosDA();
            return SQL.ObtenerMatriculaCursoOnlinePorId(matriculaCursoOnlineId);
        }

        

        //public List<EvaluacionCursoOnline> ListarEvaluacionCursoOnlinePorMatriculaCursonOnlineId(int matriculaCursoOnlineId)
        //{
        //    CursosDA SQL = new CursosDA();
        //    return SQL.ListarEvaluacionCursoOnlinePorMatriculaCursonOnlineId(matriculaCursoOnlineId);
        //}

        public List<CursoOnline> ListarCursosUsuarioPorUsuarioId(int usuarioId)
        {
            CursosDA SQL = new CursosDA();
            return SQL.ListarCursosUsuarioPorUsuarioId(usuarioId);

        }
        public List<CursoOnlineMaestro> ListarCursosMaestroUsuarioPorUsuarioId(int usuarioId)
        {
            CursosDA SQL = new CursosDA();
            return SQL.ListarCursoOnlineMaestroUsuarioPorUsuarioId(usuarioId);

        }

        
        public List<CursoOnlineMaestro> ListarCursoOnlineMaestro()
        {
            CursosDA SQL = new CursosDA();
            return SQL.ListarCursoOnlineMaestro();

        }

        public int ActualizarCategoriaCursoOnline(CategoriaCursoOnline categoriaCursoOnline)
        {
            CursosDA SQL = new CursosDA();
            return SQL.ActualizarCategoriaCursoOnline(categoriaCursoOnline);
        }

        public bool EliminarCategoriaCursoOnline(int categoriaCursoOnlineId)
        {
            CursosDA sql = new CursosDA();
            return sql.EliminarCategoriaCursoOnline(categoriaCursoOnlineId);
        }

        public bool DeleteCursoMaestroOnline(Int32 CursoOnlineId)
        {
            CursosDA sql = new CursosDA();
            return sql.DeleteCursoMaestroOnline(CursoOnlineId);
        }

        public int InsertarCategoriaCursoOnline(CategoriaCursoOnline categoriaCursoOnline)
        {
            CursosDA SQL = new CursosDA();
            return SQL.InsertarCategoriaCursoOnline(categoriaCursoOnline);
        }

        public int InsertarCursoOnline(CursoOnline cursoOnline)
        {
            CursosDA SQL = new CursosDA();
            return SQL.InsertarCursoOnline(cursoOnline);
        }

        public int InsertarCursoOnlineMaestro(CursoOnlineMaestro cursoOnline)
        {
            CursosDA SQL = new CursosDA();
            return SQL.InsertarCursoOnlineMaestro(cursoOnline);
        }
        public int InsertarCursoGrupoMaestro(CursoGrupo cursoGrupo)
        {
            CursosDA sql = new CursosDA();
            return sql.InsertarCursoGrupoMaestro(cursoGrupo);
        }

        public int InsertarUsuarioGrupoMaestro(UsuarioGrupo usuarioGrupo)
        {
            CursosDA sql = new CursosDA();
            return sql.InsertarUsuarioGrupoMaestro(usuarioGrupo);
        }

        public int ActualizarCursoOnline(CursoOnline cursoOnline)
        {
            CursosDA SQL = new CursosDA();
            return SQL.ActualizarCursoOnline(cursoOnline);
        }
        public int ActualizarCursoOnlineMaestro(CursoOnlineMaestro cursoOnline)
        {
            CursosDA SQL = new CursosDA();
            return SQL.ActualizarCursoOnlineMaestro(cursoOnline);
        }
        public int ActualizarCursoGrupoMaestro(CursoGrupo cursoGrupo)
        {
            CursosDA sql = new CursosDA();
            return sql.ActualizarCursoGrupoMaestro(cursoGrupo);
        }

        public int ActualizarUsuarioGrupoMaestro(UsuarioGrupo usuarioGrupo)
        {
            CursosDA sql = new CursosDA();
            return sql.ActualizarUsuarioGrupoMaestro(usuarioGrupo);
        }

        public int InsertarUnidadCursoOnline(UnidadCursoOnline unidadCursoOnline)
        {
            CursosDA SQL = new CursosDA();
            return SQL.InsertarUnidadCursoOnline(unidadCursoOnline);
        }

        public int ActualizarUnidadCursoOnline(UnidadCursoOnline unidadCursoOnline)
        {
            CursosDA SQL = new CursosDA();
            return SQL.ActualizarUnidadCursoOnline(unidadCursoOnline);
        }

        public int InsertarDisponibilidadCursoOnline(DisponibilidadCursoOnline disponibilidadCursoOnline)
        {
            CursosDA SQL = new CursosDA();
            return SQL.InsertarDisponibilidadCursoOnline(disponibilidadCursoOnline);
        }

        public int ActualizarCursoOnlineEncuesta(CursoOnlineEncuesta cursoOnlineEncuesta) {
            CursosDA SQL = new CursosDA();
            return SQL.ActualizarCursoOnlineEncuesta(cursoOnlineEncuesta);

        }
        public int InsertarCursoOnlineEncuesta(CursoOnlineEncuesta cursoOnlineEncuesta) {
            CursosDA SQL = new CursosDA();
            return SQL.InsertarCursoOnlineEncuesta(cursoOnlineEncuesta);

        }
        public List<DetalleCursoOnlineMaestro> ListarDetallCursoOnlineMaestroCursoOnline()
        {
            CursosDA SQL = new CursosDA();
            return SQL.ListarDetallCursoOnlineMaestroCursoOnline();
        }

        public List<DetalleCursoOnlineMaestro> ListarDetalleCursoOnlineMaestroPorCursoOnlineId(int cursoOnlineId)
        {
            CursosDA SQL = new CursosDA();
            return SQL.ListarDetalleCursoOnlineMaestroPorCursoOnlineId(cursoOnlineId);
        }

        public List<ViewCertificadoAlumno> ListarCertificadoAlumnoPorUsuarioId(int usuarioId)
        {
            CursosDA SQL = new CursosDA();
            return SQL.ListarCertificadoAlumnoPorUsuarioId(usuarioId);
        }

        public List<ViewCertificadoAlumno> ListarCertificadoTurismoAlumnoPorUsuarioId(int usuarioId)
        {
            CursosDA SQL = new CursosDA();
            return SQL.ListarCertificadoTurismoAlumnoPorUsuarioId(usuarioId);
        }

        public List<EnlacesInteres> ListaEnlancesInteres()
        {
            CursosDA SQL = new CursosDA();
            return SQL.ListarEnlacesInteres();
        }
        public List<EvaluacionCursoOnline> ListarEvaluacionCursoOnlinePorMatriculaCursoOnlineIdAgrupado(int matriculaCursoOnlineId)
        {
            CursosDA SQL = new CursosDA();
            return SQL.ListarEvaluacionCursoOnlinePorMatriculaCursoOnlineIdAgrupado(matriculaCursoOnlineId);
        }

        public List<UnidadCursoOnline> ListarUnidadCursoOnlinePorUnidad(int UnidadCursoOnlineId)
        {
            CursosDA SQL = new CursosDA();
            return SQL.ListarUnidadCursoOnlinePorUnidad(UnidadCursoOnlineId);
        }

        public List<RespuestaCursoOnline> ListarRespuestaCursoOnlinePorEvaluacionCursoOnlineId(int evaluacionCursoOnlineId)
        {
            CursosDA SQL = new CursosDA();
            return SQL.ListarRespuestaCursoOnlinePorEvaluacionCursoOnlineId(evaluacionCursoOnlineId);
        }

        public List<PreguntaCursoOnline> ListarPreguntaCursoOnlinePorCursoOnlineId(int cursoOnlineId)
        {
            CursosDA SQL = new CursosDA();
            return SQL.ListarPreguntaCursoOnlinePorCursoOnlineId(cursoOnlineId);
        }

        public int InsertarRespuestaSeleccionadaEvaluacionCursoOnline(RespuestaSeleccionadaEvaluacionCursoOnline respuestaSeleccionadaEvaluacionCursoOnline)
        {
            CursosDA SQL = new CursosDA();
            return SQL.InsertarRespuestaSeleccionadaEvaluacionCursoOnline(respuestaSeleccionadaEvaluacionCursoOnline);
        }


        public int EliminarRespuestaSeleccionadaEvaluacionCursoOnlinePorEvaluacionCursoOnlineId(int evaluacionCursoOnlineId)
        {
            CursosDA SQL = new CursosDA();
            return SQL.EliminarRespuestaSeleccionadaEvaluacionCursoOnlinePorEvaluacionCursoOnlineId(evaluacionCursoOnlineId);
        }

        public List<PreguntaCursoOnline> ListarPreguntaCursoOnline()
        {
            CursosDA SQL = new CursosDA();
            return SQL.ListarPreguntaCursoOnline();
        }

        public PreguntaCursoOnline ObtenerPreguntaCursoOnline(int preguntaCursoOnlineId)
        {
            CursosDA SQL = new CursosDA();
            return SQL.ObtenerPreguntaCursoOnline(preguntaCursoOnlineId);

        }

        public List<PreguntaCursoOnline> ListarPreguntaCursoOnlinePorUnidadCursoOnlineId(int unidadCursoOnlineId)
        {
            CursosDA SQL = new CursosDA();
            return SQL.ListarPreguntaCursoOnlinePorUnidadOnlineId(unidadCursoOnlineId);
        }

        public List<PreguntaCursoOnline> ListarPreguntaCursoOnlinePorUnidadOnlineId(int unidadOnlineId)
        {
            CursosDA SQL = new CursosDA();
            return SQL.ListarPreguntaCursoOnlinePorUnidadOnlineId(unidadOnlineId);
        }

        public List<PreguntaEvaluacionCursoOnline> ListarPreguntaEvaluacionCursoOnline()
        {
            CursosDA SQL = new CursosDA();
            return SQL.ListarPreguntaEvaluacionCursoOnline();
        }

        public List<PreguntaEvaluacionCursoOnline> ListarPreguntaEvaluacionCursoOnlinePorEvaluacionCursoOnlineId(int evaluacionCursoOnlineId)
        {
            CursosDA SQL = new CursosDA();
            return SQL.ListarPreguntaEvaluacionCursoOnlinePorEvaluacionCursoOnlineId(evaluacionCursoOnlineId);
        }

        public List<RespuestaEvaluacionCursoOnline> ListarRespuestaEvaluacionCursoOnline()
        {
            CursosDA SQL = new CursosDA();
            return SQL.ListarRespuestaEvaluacionCursoOnline();
        }

        public List<RespuestaEvaluacionCursoOnline> ListarRespuestaEvaluacionCursoOnlinePorEvaluacionCursoOnlineId(int evaluacionCursoOnlineId)
        {
            CursosDA SQL = new CursosDA();
            return SQL.ListarRespuestaEvaluacionCursoOnlinePorEvaluacionCursoOnlineId(evaluacionCursoOnlineId);
        }


        public int InsertarMatricualCursoOnline(MatriculaCursoOnline matricula)
        {
            CursosDA SQL = new CursosDA();
            return SQL.InsertarMatricualCursoOnline(matricula);
        }

        public int InsertarAvanceMatriculaCursoOnline(AvanceMatriculaCursoOnline avanceMatriculaCursoOnline)
        {
            CursosDA SQL = new CursosDA();
            return SQL.InsertarAvanceMatriculaCursoOnline(avanceMatriculaCursoOnline);
        }

        public int ActualizarAvanceMatriculaCursoOnline(AvanceMatriculaCursoOnline avanceMatriculaCursoOnline)
        {
            CursosDA SQL = new CursosDA();
            return SQL.ActualizarAvanceMatriculaCursoOnline(avanceMatriculaCursoOnline);
        }

        public int ActualizarMatriculaCursoOnline(MatriculaCursoOnline matriculaCursoOnline)
        {
            CursosDA SQL = new CursosDA();
            return SQL.ActualizarMatriculaCursoOnline(matriculaCursoOnline);
        }

        public List<ViewUsuarioMatriculaCursoOnline> ListarViewUsuarioMatriculaCursoOnline()
        {
            CursosDA SQL = new CursosDA();
            return SQL.ListarViewUsuarioMatriculaCursoOnline();
        }

        public List<ViewCertificadoAlumno> ListarViewCertificadoAlumno()
        {
            CursosDA SQL = new CursosDA();
            return SQL.ListarViewCertificadoAlumno();
        }

        public List<ViewCertificadoAlumno> ListarViewCertificadoTurismoAlumno()
        {
            CursosDA SQL = new CursosDA();
            return SQL.ListarViewCertificadoTurismoAlumno();
        }


        public List<ViewUsuarioCursoMaestroReporte> ListarViewUsuarioCursoMaestroReporte()
        {
            CursosDA SQL = new CursosDA();
            return SQL.ListarViewUsuarioCursoMaestroReporte();
        }

        public List<ViewUsuarioCursoMaestroReporte> ListarViewUsuarioCursoTurismoReporte()
        {
            CursosDA SQL = new CursosDA();
            return SQL.ListarViewUsuarioCursoTurismoReporte();
        }

        public int InsertarLogCertificado(LogCertificado logCertificado)
        {
            CursosDA SQL = new CursosDA();
            return SQL.InsertarLogCertificado(logCertificado);
        }

        public List<CursoOnlineMaestro> SPUSP_AddCursoMaestro_LIS(int? grupoId)
        {
            CursosDA SQL = new CursosDA();
            return SQL.SPUSP_AddCursoMaestro_LIS(grupoId);
        }

        public List<CursoGrupo> ListarCursoGrupoGrupo()
        {
            CursosDA SQL = new CursosDA();
            return SQL.ListarCursoGrupoGrupo();
        }

        public int InsertarPreguntaCursoOnline(PreguntaCursoOnline preguntaCursoOnline)
        {
            CursosDA SQL = new CursosDA();
            return SQL.InsertarPreguntaCursoOnline(preguntaCursoOnline);
        }

        public int ActualizarPreguntaCursoOnline(PreguntaCursoOnline preguntaCursoOnline)
        {
            CursosDA SQL = new CursosDA();
            return SQL.ActualizarPreguntaCursoOnline(preguntaCursoOnline);
        }


        public int ActualizarRespuestaCursoOnline(RespuestaCursoOnline preguntaCursoOnline)
        {
            CursosDA SQL = new CursosDA();
            return SQL.ActualizarRespuestaCursoOnline(preguntaCursoOnline);
        }

        public int InsertarRespuestaCursoOnline(RespuestaCursoOnline respuestaCursoOnline)
        {
            CursosDA SQL = new CursosDA();
            return SQL.InsertarRespuestaCursoOnline(respuestaCursoOnline);
        }

        public int InsertarEvaluacionCursoOnline(EvaluacionCursoOnline evaluacion)
        {
            CursosDA SQL = new CursosDA();
            return SQL.InsertarEvaluacionCursoOnline(evaluacion);
        }

        public int ActualizarEvaluacionCursoOnline(EvaluacionCursoOnline evaluacion)
        {
            CursosDA SQL = new CursosDA();
            return SQL.ActualizarEvaluacionCursoOnline(evaluacion);
        }

        public int InsertarPreguntaEvaluacionCursoOnline(PreguntaEvaluacionCursoOnline preguntaEvaluacion)
        {
            CursosDA SQL = new CursosDA();
            return SQL.InsertarPreguntaEvaluacionCursoOnline(preguntaEvaluacion);
        }

        public int InsertarRespuestaEvaluacionCursoOnline(RespuestaEvaluacionCursoOnline respuestaEvaluacion)
        {
            CursosDA SQL = new CursosDA();
            return SQL.InsertarRespuestaEvaluacionCursoOnline(respuestaEvaluacion);
        }

        public int ActualizarCursoOnlineEncuestaDetalle(DetalleCursoOnlineEncuesta detalleCursoOnlineEncuesta)
        {
            CursosDA SQL = new CursosDA();
            return SQL.ActualizarCursoOnlineEncuestaDetalle(detalleCursoOnlineEncuesta);
        }

        public int InsertarCursoOnlineEncuestaDetalle(DetalleCursoOnlineEncuesta detalleCursoOnlineEncuesta)
        {
            CursosDA SQL = new CursosDA();
            return SQL.InsertarCursoOnlineEncuestaDetalle(detalleCursoOnlineEncuesta);
        }

        public int InsertarVideoUnidadCursoOnline(VideoUnidadCursoOnline videoUnidadCursoOnline)
        {
            CursosDA SQL = new CursosDA();
            return SQL.InsertarVideoUnidadCursoOnline(videoUnidadCursoOnline);
        }

        public int ActualizarVideoUnidadCursoOnline(VideoUnidadCursoOnline videoUnidadCursoOnline)
        {
            CursosDA SQL = new CursosDA();
            return SQL.ActualizarVideoUnidadCursoOnline(videoUnidadCursoOnline);
        }

        public int InsertarCursoOnlineEncuestaRespuesta(CursoOnlineEncuestaRespuesta cursoOnlineEncuestaRespuesta)
        {
            CursosDA SQL = new CursosDA();
            return SQL.InsertarCursoOnlineEncuestaRespuesta(cursoOnlineEncuestaRespuesta);
        }

        public int InsertarCursoOnlineEncuestaRespuestaDetalle(CursoOnlineEncuestaRespuestaDetalle oCursoOnlineEncuestaRespuestaDetalle)
        {
            CursosDA SQL = new CursosDA();
            return SQL.InsertarCursoOnlineEncuestaRespuestaDetalle(oCursoOnlineEncuestaRespuestaDetalle);
        }
    }
}
