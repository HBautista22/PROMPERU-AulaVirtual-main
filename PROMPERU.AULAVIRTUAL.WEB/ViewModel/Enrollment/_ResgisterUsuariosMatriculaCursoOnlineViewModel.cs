using PROMPERU.AULAVIRTUAL.WEB.Controllers;
using PROMPERU.AULAVIRTUAL.WEB.Helpers;
using PROMPERU.AULAVIRTUAL.BE.USUARIO;
using PROMPERU.AULAVIRTUAL.BE.CURSOS;
using PROMPERU.AULAVIRTUAL.BL;

using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
//using System.Data.Entity;
using System.Linq;
using System.Web;

namespace PROMPERU.AULAVIRTUAL.WEB.ViewModel.Enrollment
{
    

    public class _ResgisterUsuariosMatriculaCursoOnlineViewModel
    {
        CursosBL cursosBL = new CursosBL();

        public Int32 EmpresaId { get; set; }
        public CursoOnline CursoOnline { get; set; }
        public Int32 CursoOnlineId { get; set; }

        [Display(Name = "Usuarios")]
        public List<Usuario> LstUsuario { get; set; }

        public List<Int32> LstUsuarioId { get; set; }

        public _ResgisterUsuariosMatriculaCursoOnlineViewModel()
        {
            LstUsuarioId = new List<Int32>();
        }

        public void CargarDatos(CargarDatosContext dataContext, Int32 cursoOnlineId)
        {
            
            this.CursoOnline = cursosBL.ObtenerCursoOnlinePorId(cursoOnlineId);  //dataContext.context.CursoOnline.Find(cursoOnlineId);
            this.EmpresaId = dataContext.Session.GetEmpresaId();
            this.CursoOnlineId = cursoOnlineId;
            

            var fechaInicioMatriculaVigente = ConstantHelpers.GET_FECHA_INICIO_MATRICULA_VIGENTE();

            var lstUsuarioMatricula = new MatriculaCursoOnline(); //dataContext.context.MatriculaCursoOnline.Include(x => x.Usuario).Where(x => x.Usuario.EmpresaId == EmpresaId && x.CursoOnlineId == cursoOnlineId && !x.Estado.Equals(ConstantHelpers.ESTADO_MATRICULA.INACTIVO) && (x.FechaMatricula >= fechaInicioMatriculaVigente /*|| x.Estado.Equals(ConstantHelpers.ESTADO_MATRICULA.APROBADO)*/)).Select(x => x.UsuarioId).ToList();

            //if (lstUsuarioMatricula == null)
            //    lstUsuarioMatricula = new List<int>();

            LstUsuario = new List<Usuario>();// dataContext.context.Usuario.Where(x => x.EmpresaId == EmpresaId && x.Estado == ConstantHelpers.ESTADO.ACTIVO && !lstUsuarioMatricula.Contains(x.UsuarioId)).ToList();
        }

    }
}