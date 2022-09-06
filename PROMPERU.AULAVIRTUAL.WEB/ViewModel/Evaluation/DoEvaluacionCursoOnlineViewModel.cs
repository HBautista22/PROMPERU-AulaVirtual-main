using PROMPERU.AULAVIRTUAL.WEB.Controllers;
using PROMPERU.AULAVIRTUAL.WEB.Helpers;
using PROMPERU.AULAVIRTUAL.BE.CURSOS;
using PROMPERU.AULAVIRTUAL.BL;

using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Web;
//using System.Data.Entity;

namespace PROMPERU.AULAVIRTUAL.WEB.ViewModel.Evaluation
{
    public class DoEvaluacionCursoOnlineViewModel
    {
        CursosBL cursosBL = new CursosBL();
        UsuarioBL usuarioBL = new UsuarioBL();
        EmpresaBL empresaBL = new EmpresaBL();

        public Int32 CursoOnlineId { get; set; }
        public Int32 UnidadCursoOnlineId { get; set; }
        public Int32 EvaluacionCursoOnlineId { get; set; }
        public CursoOnline CursoOnline { get; set; }
        public EvaluacionCursoOnline EvaluacionCursoOnline { get; set; }
        public List<PreguntaEvaluacionCursoOnline> LstPreguntaEvaluacionCursoOnline { get; set; } = new List<PreguntaEvaluacionCursoOnline>();
        public Dictionary<Int32, List<RespuestaEvaluacionCursoOnline>> DictRespuestasEvaluacionCursoOnline { get; set; } = new Dictionary<int, List<RespuestaEvaluacionCursoOnline>>();

        public void CargarDatos(Int32 cursoOnlineId, Int32 evaluacionCursoOnlineId)
        {
            this.CursoOnlineId = cursoOnlineId;
            this.EvaluacionCursoOnlineId = evaluacionCursoOnlineId;

            this.LstPreguntaEvaluacionCursoOnline = cursosBL.ListarPreguntaEvaluacionCursoOnlinePorEvaluacionCursoOnlineId(evaluacionCursoOnlineId);  //.Where(x => x.EvaluacionCursoOnlineId == evaluacionCursoOnlineId).ToList();
            this.DictRespuestasEvaluacionCursoOnline = cursosBL.ListarRespuestaEvaluacionCursoOnlinePorEvaluacionCursoOnlineId(evaluacionCursoOnlineId).
                GroupBy(x => x.PreguntaEvaluacionCursoOnlineId).ToDictionary(x => x.Key, x => x.ToList());
        }
    }
}