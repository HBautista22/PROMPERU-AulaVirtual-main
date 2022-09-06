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
using PROMPERU.AULAVIRTUAL.BL;

//using PagedList;

namespace PROMPERU.AULAVIRTUAL.WEB.ViewModel.User
{
    public class _AddCursoMaestroOnlineViewModel
    {
        public Int32? GrupoId { get; set; }
        public List<CursoGrupo> LstCursoGrupo { get; set; }
        public List<CursoOnlineMaestro> LstCursoOnlineMaestro { get; set; }
        public String ListItems { get; set; }

        public void CargarDatos(CargarDatosContext dataContext, Int32? GrupoId)
        {
            CursosBL cursosBL = new CursosBL();

            try
            {
                this.GrupoId = GrupoId;

                //IQueryable<CursoOnlineMaestro> queryCursoOnline1 = dataContext.context.CursoOnlineMaestro.SqlQuery("USP_AddCursoMaestro_LIS {0}", GrupoId).AsQueryable();
                IQueryable<CursoOnlineMaestro> queryCursoOnline1 = cursosBL.SPUSP_AddCursoMaestro_LIS(GrupoId).AsQueryable();

                LstCursoOnlineMaestro = queryCursoOnline1.ToList();

                //IQueryable<CursoGrupo> queryCursoOnline = dataContext.context.CursoGrupo.Include(x => x.Grupo).AsQueryable();
                IQueryable<CursoGrupo> queryCursoOnline = cursosBL.ListarCursoGrupoGrupo().AsQueryable();
                queryCursoOnline = queryCursoOnline.Where(x => x.Estado != ConstantHelpers.ESTADO.ELIMINADO && x.GrupoId == GrupoId);//aqui se agrego que no muestre los eliminados
                LstCursoGrupo = queryCursoOnline.ToList();

            }
            catch (Exception e)
            {

                throw;
            }


        }

    }

}