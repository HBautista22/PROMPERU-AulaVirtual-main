using System;
using System.Collections.Generic;
using System.Linq;
using System.Transactions;
using System.Web;
using PROMPERU.AULAVIRTUAL.WEB.Controllers;
using PROMPERU.AULAVIRTUAL.WEB.Helpers;
using PROMPERU.AULAVIRTUAL.BE.CURSOS;
using PROMPERU.AULAVIRTUAL.BL;


namespace PROMPERU.AULAVIRTUAL.WEB.ViewModel.Course
{
    public class _OrderUnidadViewModel
    {
        CursosBL cursosBL = new CursosBL();
        public int? UnidadId { get; set; }
        public int CursoId { get; set; }        
        public string Orden { get; set; }
        public IEnumerable<UnidadCursoOnline> LstUnidad { get; private set; }
        public UnidadCursoOnline UnidadPadre { get; private set; }
        public CursoOnline CursoOnline { get; private set; }

        internal void Fill(CargarDatosContext datosContext, int cursoId, int? unidadId)
        {
            this.UnidadId = unidadId;
            this.CursoId = cursoId;
            this.UnidadPadre = cursosBL.ObtenerUnidadCursoOnlinePorId((int)unidadId); //datosContext.context.UnidadCursoOnline.Find(unidadId);
            if (UnidadPadre == null)
            {
                CursoOnline = cursosBL.ObtenerCursoOnlinePorId(cursoId); //datosContext.context.CursoOnline.Find(cursoId);
            }
            this.LstUnidad = cursosBL.ListarUnidadCursoOnlinePorCursoOnlineId(cursoId).Where(x => x.UnidadCursoOnlinePadreId == unidadId && x.CursoOnlineId == CursoId).OrderBy(x => x.Orden).ToArray();     //datosContext.context.UnidadCursoOnline.Where(x => x.UnidadCursoOnlinePadreId == unidadId && x.CursoOnlineId == CursoId).OrderBy(x => x.Orden).ToArray();
        }

        internal void Register(CargarDatosContext dataContext, _OrderUnidadViewModel model)
        {
            try
            {
                using (var ts = new TransactionScope())
                {
                    var orden = 1;
                    foreach (var unidaId in model.Orden.Split(','))
                    {
                        var unidad = cursosBL.ObtenerUnidadCursoOnlinePorId(Convert.ToInt32(unidaId));  //dataContext.context.UnidadCursoOnline.Find(unidaId.ToInteger());
                        unidad.Orden = orden;
                        orden++;

                        //TODO : Update ORDEN unidad curso online
                        //dataContext.context.SaveChanges();
                    }
                    ts.Complete();
                }
            }
            catch (Exception)
            {
                throw;
            }
        }
    }
}