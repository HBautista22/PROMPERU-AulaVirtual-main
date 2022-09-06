using PROMPERU.AULAVIRTUAL.WEB.Controllers;
using PROMPERU.AULAVIRTUAL.BE.CURSOS;
using PROMPERU.AULAVIRTUAL.BE.USUARIO;
using PROMPERU.AULAVIRTUAL.BE.PARAMETROS;
using System;
using System.Collections.Generic;
using System.Linq;
//using System.Data.Entity;
using System.ComponentModel.DataAnnotations;
using System.Web.Mvc;
using PROMPERU.AULAVIRTUAL.WEB.Helpers;
using PROMPERU.AULAVIRTUAL.BL;
using PROMPERU.AULAVIRTUAL.DA;

namespace PROMPERU.AULAVIRTUAL.WEB.ViewModel.User
{
    public class ViewPerfilViewModel
    {
        public Int32 UsuarioId { get; set; }
        public List<MatriculaCursoOnline> LstMatriculaCursoOnline { get; set; }
        public List<EvaluacionCursoOnline> LstEvaluacionCursoOnline { get; set; }
        public Usuario Usuario { get; set; }
        [Display(Name ="Nombre del curso")]
        public String NombreCurso { get; set; }
        public String UrlReferrer { get; set; }

        public List<SelectListItem> LstRol { get; set; }
        public List<Parametro> LstSexo { get; set; }
        public List<Parametro> LstNacionalidad { get; set; }

        public List<Departamento> LstDepartamento { get; set; }
        public List<Provincia> LstProvincia { get; set; }
        public List<Distrito> LstDistrito { get; set; }

        public Int32? DepartamentoId { get; set; }
        public Int32? ProvinciaId { get; set; }

        public List<RutexSector> LstSector { get; set; }
        public int[] Sector { get; set; }

        ParametroBL parametroBL = new ParametroBL();
        CursosBL cursosBL = new CursosBL();
        UsuarioBL usuarioBL = new UsuarioBL();
        JsonBL jsonBL = new JsonBL();
        EmpresaBL empresaBL = new EmpresaBL();
        ParametrosDA parametrosDA = new ParametrosDA();
        //ParametroBL parametroBL = new ParametroBL();
        public ViewPerfilViewModel()
        {
            LstRol = new List<SelectListItem>();
            LstDepartamento = new List<Departamento>();
            LstProvincia= new List<Provincia>();
            LstDistrito = new List<Distrito>();
        }

        public void CargarDatos(CargarDatosContext dataContext, Int32 usuarioId)
        {
            
            LstSector = parametrosDA.ListarRutexSector();

            this.UsuarioId = usuarioId;

            this.LstRol.Add(new SelectListItem { Text = "Supervisor", Value = ConstantHelpers.ROL.SUPERVISOR });
            this.LstRol.Add(new SelectListItem { Text = "Alumno", Value = ConstantHelpers.ROL.ALUMNO });
            List<Parametro> lstParametros = parametroBL.ListarParametro();

            this.LstSexo = lstParametros.Where(x => x.ParametroGrupoId == 5 && x.EsActivo).OrderBy(x => x.Orden).ToList();

            this.LstNacionalidad = lstParametros.Where(x => x.ParametroGrupoId == 4 && x.EsActivo).OrderBy(x => x.Orden).ToList();

            //List<MatriculaCursoOnline> queryMatriculaCursoOnline = dataContext.context.MatriculaCursoOnline.Include(x => x.Usuario).Include(x => x.CursoOnline).Where(x => x.UsuarioId == UsuarioId).AsQueryable();
            List<MatriculaCursoOnline> queryMatriculaCursoOnline = cursosBL.ListarMatriculaCursoOnline(UsuarioId);  
            if (!String.IsNullOrEmpty(this.NombreCurso))
            {
                queryMatriculaCursoOnline = queryMatriculaCursoOnline.Where(x => x.CursoOnline.Nombre.Contains(this.NombreCurso)).ToList();
            }


            this.LstMatriculaCursoOnline = queryMatriculaCursoOnline.ToList();

            //var queryEvaluacionCursoOnline = dataContext.context.EvaluacionCursoOnline.Include(x => x.MatriculaCursoOnline).Include(x => x.MatriculaCursoOnline.CursoOnline).Where(x => x.MatriculaCursoOnline.UsuarioId == UsuarioId).AsQueryable();



            var queryEvaluacionCursoOnline = cursosBL.ListarEvaluacionCursoOnlinePorUsuarioId(UsuarioId);   //new List<EvaluacionCursoOnline>(); 


            if (!String.IsNullOrEmpty(this.NombreCurso))
            {
                queryEvaluacionCursoOnline = queryEvaluacionCursoOnline.Where(x => x.MatriculaCursoOnline.CursoOnline.Nombre.Contains(this.NombreCurso)).ToList();
            }

            this.LstEvaluacionCursoOnline = queryEvaluacionCursoOnline.ToList();

            foreach (EvaluacionCursoOnline eva in this.LstEvaluacionCursoOnline)
            {
                eva.MatriculaCursoOnline = cursosBL.ObtenerMatriculaCursoOnlinePorId(eva.MatriculaCursoOnlineId);
            }

            //this.Usuario = dataContext.context.Usuario.Find(UsuarioId);
            this.Usuario = usuarioBL.ObtenerUsuarioPorId(UsuarioId); 

            this.Usuario.Empresa = empresaBL.ObtenerEmpresaPorEmpresaId(this.Usuario.EmpresaId??0).FirstOrDefault(x => x.EmpresaId == (this.Usuario.EmpresaId??0));

            //this.LstDepartamento = dataContext.context.Departamento.Where(x => x.EstadoId == 3).ToList();
            List<Distrito> lstDistrito = parametroBL.ListarDistrito();
            List<Provincia> lstProvincia = parametroBL.ListarProvincia();
            List<Departamento> lstDepartamento = parametroBL.ListarDepartamento();

            if (!(this.Usuario.DistritoId.HasValue && this.Usuario.DistritoId > 0))
            {
                this.Usuario.DistritoId = 1248;
            }



                if (this.Usuario.DistritoId.HasValue && this.Usuario.DistritoId > 0)
            {
                this.Usuario.Distrito = lstDistrito.FirstOrDefault(x => x.DistritoId == this.Usuario.DistritoId);

                this.ProvinciaId = this.Usuario.Distrito.ProvinciaId;
                this.DepartamentoId = this.Usuario.Distrito.DepartamentoId;

                this.Usuario.Distrito.Provincia = lstProvincia.FirstOrDefault(x => x.ProvinciaId == this.ProvinciaId);
                this.Usuario.Distrito.Departamento = lstDepartamento.FirstOrDefault(x => x.DepartamentoId == this.DepartamentoId);
                

                this.LstDistrito = lstDistrito.Where(x => x.ProvinciaId == this.ProvinciaId && x.EstadoId == 3).ToList();
                this.LstProvincia = lstProvincia.Where(x => x.DepartamentoId == this.DepartamentoId && x.EstadoId == 3).ToList();
                this.LstDepartamento = lstDepartamento;
            }
            
           
        }
    }
}
