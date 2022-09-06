using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using PROMPERU.AULAVIRTUAL.WEB.Models;
using PROMPERU.AULAVIRTUAL.WEB.Controllers;
using PROMPERU.AULAVIRTUAL.WEB.Helpers;
using System.ComponentModel.DataAnnotations;
//using System.Data.Entity;
using System.Web.Mvc;
using PROMPERU.AULAVIRTUAL.BL;

namespace PROMPERU.AULAVIRTUAL.WEB.ViewModel.User
{
    public class EditGrupoViewModel
    {
        UsuarioBL usuarioBL = new UsuarioBL();

        public Int32? GrupoId { get; set; }

        [Display(Name = "Descripcion")]
        [Required]
        public String Descripcion { get; set; }

        //[Display(Name = "Aforo")]
        //public int? Aforo { get; set; }

        [Display(Name = "¿Esta Activo?")]
        [Required]
        public Boolean Estado { get; set; }

        public List<BaseModel> LstTipoGrupo { get; set; }

        [Display(Name = "Seleccionar Tipo Grupo")]
        public String TipoGrupo { get; set; }

        public int Aforo { get; set; }


        public EditGrupoViewModel()
        {

            LstTipoGrupo = new List<BaseModel>
            {
                new BaseModel(ConstantHelpers.ROL.ALUMNO,"ALUMNO"),
                new BaseModel(ConstantHelpers.ROL.EMPRESA,"EMPRESA"),
                new BaseModel("GNR","GENERAL"),
            };

        }

        public void CargarDatos(CargarDatosContext dataContext, Int32? GrupoId)
        {
            this.GrupoId = GrupoId;

            if (GrupoId.HasValue)
            {
                //var Grupo = dataContext.context.Grupo.Find(GrupoId);
                var Grupo = usuarioBL.ObtenerGrupoPorId(GrupoId); //dataContext.context.Grupo.Find(GrupoId);
                this.GrupoId = Grupo.GrupoId;
                this.Descripcion = Grupo.Descripcion;
                this.Estado = (Grupo.Estado == ConstantHelpers.ESTADO.ACTIVO);
                this.TipoGrupo = Grupo.TipoRegistro;

            }

            /*var LstCurso_p = dataContext.context.CursoOnlineMaestro.SqlQuery("USP_CursoMaestroXGrupo_LIS {0}", GrupoId).ToList();

            if (LstCurso_p != null)
            {

                this.CursoOnlineMaestroArr = LstCurso_p.AsEnumerable().Select(o => new CursoOnlineMaestro { CursoOnlineMaestroId = o.CursoOnlineMaestroId }).ToArray();

            }

                LstCurso = dataContext.context.CursoOnlineMaestro.ToList();*/


        }
    }
}




