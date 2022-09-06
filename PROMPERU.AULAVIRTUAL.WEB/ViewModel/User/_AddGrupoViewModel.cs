using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using PROMPERU.AULAVIRTUAL.BE.CURSOS;
using PROMPERU.AULAVIRTUAL.WEB.Controllers;
using PROMPERU.AULAVIRTUAL.WEB.Helpers;
using System.ComponentModel.DataAnnotations;
//using System.Data.Entity;
using System.IO;

//using PagedList;

namespace PROMPERU.AULAVIRTUAL.WEB.ViewModel.User
{
    public class _AddGrupoViewModel
    {
        public Int32? UsuarioId { get; set; }
        public string Usuario_Id { get; set; }
        
        public List<Grupo> LstCursoGrupo { get; set; }
        public List<Grupo> LstCursoOnlineMaestro { get; set; }
        public String ListItems { get; set; }
        public int MarcarTodo { get; set; }


        public void CargarDatos(CargarDatosContext dataContext, Int32? UsuarioId, String MarcaAlgunos)
        {
            //try
            //{
            //    //this.UsuarioId = UsuarioId;

            //    //IQueryable<Grupo> queryCursoOnline1 = dataContext.context.Grupo.SqlQuery("USP_AddGrupo_LIS {0}", UsuarioId).AsQueryable();

            //    //LstCursoOnlineMaestro = queryCursoOnline1.ToList();

            //    //IQueryable<Grupo> queryCursoOnline = dataContext.context.Grupo.SqlQuery("USP_AddGrupo_SEL {0}", UsuarioId).AsQueryable();
            //    ////queryCursoOnline = queryCursoOnline.Where(x => x.Estado != ConstantHelpers.ESTADO.ELIMINADO && x.UsuarioId == UsuarioId);//aqui se agrego que no muestre los eliminados
            //    //LstCursoGrupo = queryCursoOnline.ToList();

            //}
            //catch (Exception e)
            //{

            //    throw;
            //}


        }

    }

}