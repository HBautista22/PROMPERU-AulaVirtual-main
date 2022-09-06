using PROMPERU.AULAVIRTUAL.WEB.Controllers;
using PROMPERU.AULAVIRTUAL.WEB.Models;
using System;
using System.Collections.Generic;
using System.Linq;
//using System.Data.Entity;
using PROMPERU.AULAVIRTUAL.WEB.Helpers;
using PROMPERU.AULAVIRTUAL.BE.CURSOS;
using PROMPERU.AULAVIRTUAL.BL;

namespace PROMPERU.AULAVIRTUAL.WEB.ViewModel.Report
{
    public class SendRecordatorioViewModel
    {
        public Int32 CursoOnlineId { get; set; }
        public List<MatriculaCursoOnline> LstMatriculaCursoOnlinePendienteAprobacion { get; set; }

        CursosBL cursosBL = new CursosBL();
        public void CargarDatos(CargarDatosContext dataContext, Int32 cursoOnlineId)
        {
            CursoOnlineId = cursoOnlineId;
            var empresaId = dataContext.Session.GetEmpresaId();
            var fechaInicioMatriculaVigente = ConstantHelpers.GET_FECHA_INICIO_MATRICULA_VIGENTE();

            //LstMatriculaCursoOnlinePendienteAprobacion = dataContext.context.MatriculaCursoOnline.Where(x => x.CursoOnlineId == CursoOnlineId && x.Usuario.EmpresaId == empresaId && x.Estado != ConstantHelpers.ESTADO_MATRICULA.APROBADO && x.Estado != ConstantHelpers.ESTADO_MATRICULA.INACTIVO && x.FechaMatricula >= fechaInicioMatriculaVigente).ToList();
            LstMatriculaCursoOnlinePendienteAprobacion = cursosBL.ListarMatriculaCursoOnlinePorEmpresaId(empresaId).Where(x => x.CursoOnlineId == CursoOnlineId && x.Estado != ConstantHelpers.ESTADO_MATRICULA.APROBADO && x.Estado != ConstantHelpers.ESTADO_MATRICULA.INACTIVO && x.FechaMatricula >= fechaInicioMatriculaVigente).ToList();
        }
    }
}