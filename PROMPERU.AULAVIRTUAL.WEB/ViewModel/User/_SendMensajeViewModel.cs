using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using PROMPERU.AULAVIRTUAL.BE.USUARIO;
using PROMPERU.AULAVIRTUAL.WEB.Controllers;
using PROMPERU.AULAVIRTUAL.WEB.Helpers;
using System.ComponentModel.DataAnnotations;
using System.IO;


namespace PROMPERU.AULAVIRTUAL.WEB.ViewModel.User
{
    public class _SendMensajeViewModel
    {
        public Int32? GrupoId { get; set; }
        public List<UsuarioGrupo> LstCursoGrupo { get; set; }
        [Display(Name = "Seleccionar Notificaciones")]
        public List<PROMPERU.AULAVIRTUAL.BE.CURSOS.Notificaciones> LstNotificaciones { get; set; }
        public String ListItems { get; set; }
        public int NotificacionId { get; set; }
        [Display(Name = "Ingresar Asunto")]
        public string Asunto { get; set; }

        public int Mensaje { get; set; }

        public void CargarDatos(CargarDatosContext dataContext, Int32? GrupoId)
        {
            try
            {
                this.GrupoId = GrupoId;

                //TODO: STORe PARA CAMBIAR DATOS EN EL MENSAJE
                //IQueryable<PROMPERU.AULAVIRTUAL.WEB.Models.Notificaciones> queryCursoOnline1 = dataContext.context.Notificaciones.Where(x=>x.Estado==ConstantHelpers.ESTADO.ACTIVO && x.TipoNotificacionId==6).AsQueryable();

                //LstNotificaciones = queryCursoOnline1.ToList();

                /*IQueryable<UsuarioGrupo> queryCursoOnline = dataContext.context.UsuarioGrupo.Include(x => x.Grupo).AsQueryable();
                queryCursoOnline = queryCursoOnline.Where(x => x.Estado != ConstantHelpers.ESTADO.ELIMINADO && x.GrupoId == GrupoId);//aqui se agrego que no muestre los eliminados
                LstCursoGrupo = queryCursoOnline.ToList();*/

            }
            catch (Exception e)
            {

                throw;
            }


        }

    }

}