using System;
using System.Linq;
using PROMPERU.AULAVIRTUAL.WEB.Controllers;
using PROMPERU.AULAVIRTUAL.BE.CURSOS;
using PROMPERU.AULAVIRTUAL.BL;
using PROMPERU.AULAVIRTUAL.Helpers;

namespace PROMPERU.AULAVIRTUAL.WEB.ViewModel.User
{
    // ReSharper disable once InconsistentNaming
    public class _AddUsuarioViewModel
    {
        public GrupoBL GrupoBl = new GrupoBL();

        public int? GrupoId { get; set; }
        public int? Estado { get; set; }

        public void CargarDatos(CargarDatosContext dataContext, int? grupoId)
        {
            try
            {
                GrupoId = grupoId;
                Grupo grupos = GrupoBl
                    .ListarGrupo().FirstOrDefault(x => x.GrupoId == grupoId);
                if (grupos != null) Estado = grupos.Aforo;
            }
            catch (Exception e)
            {
                Util.SaveErrorLog(e);
            }
        }

    }

}