using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using PROMPERU.AULAVIRTUAL.WEB.Models;
using PROMPERU.AULAVIRTUAL.WEB.Controllers;
using PROMPERU.AULAVIRTUAL.WEB.Helpers;
using System.ComponentModel.DataAnnotations;
//using System.Data.Entity;
using System.IO;
using PROMPERU.AULAVIRTUAL.BE.CURSOS;
using PROMPERU.AULAVIRTUAL.BL;

//using PagedList;

namespace PROMPERU.AULAVIRTUAL.WEB.ViewModel.Course
{
    public class _AddCursoOnlineViewModel
    {
        CursosBL cursosBL = new CursosBL();
        public Int32? CursoOnlineId { get; set; }
        public List<DetalleCursoOnlineMaestro> LstDetalleOnline { get; set; }
        public List<CursoOnline> LstCursoOnline { get; set; }
        public String ListItems { get; set; }

        public void CargarDatos(CargarDatosContext dataContext, Int32? CursoOnlineId)
        {
        
                try
            {
                this.CursoOnlineId = CursoOnlineId;


                IQueryable<CursoOnline> queryCursoOnline1 =  cursosBL.AddCursoOnline((int)CursoOnlineId).AsQueryable(); //dataContext.context.CursoOnline.SqlQuery("USP_AddCursoOnline_LIS {0}", CursoOnlineId).AsQueryable();

                LstCursoOnline = queryCursoOnline1.ToList();

                IQueryable<DetalleCursoOnlineMaestro> queryCursoOnline =   cursosBL.ListarDetallCursoOnlineMaestroCursoOnline().AsQueryable();
                queryCursoOnline = queryCursoOnline.OrderBy(x => x.Orden).Where(x => x.Estado != ConstantHelpers.ESTADO.ELIMINADO && x.CursoOnlineMaestroId == CursoOnlineId);//aqui se agrego que no muestre los eliminados
                LstDetalleOnline = queryCursoOnline.ToList();

            }
            catch (Exception e)
            {

                throw;
            }


        }
            

    }

}