using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
//using System.Data.Entity;
using PROMPERU.AULAVIRTUAL.WEB.Models;
using PROMPERU.AULAVIRTUAL.WEB.Helpers;

namespace PROMPERU.AULAVIRTUAL.WEB.Logic
{
    public class PermisoAccesoLogic
    {
        //public EvolEntities context = new EvolEntities();

        public enum TipoPermiso
        {
            Restringido = 1,
            Lectura = 2,
            Escritura = 3
        }

        #region Functiones comunes
        public TipoPermiso GetMaxPermiso(TipoPermiso permisoA, TipoPermiso permisoB)
        {
            return (TipoPermiso)Math.Max((Int32)permisoA, (Int32)permisoB);
        }

        public TipoPermiso GetMinPermiso(TipoPermiso permisoA, TipoPermiso permisoB)
        {
            return (TipoPermiso)Math.Min((Int32)permisoA, (Int32)permisoB);
        }

        public Boolean TienePermisoSuficiente(TipoPermiso permisoObtenido, TipoPermiso permisoNecesario)
        {
            return (Int32)permisoObtenido >= (Int32)permisoNecesario;
        }
               
        #endregion

        #region Templates
        /*
        public TipoPermiso GetPermisoExperienciaExitosa(Int32 usuarioId, Int32 experienciaExitosaId, Int32 faseId)
        {
            try
            {
                var experienciExitosa = context.ExperienciaExitosa.First(x => x.ExperienciaExitosaId == experienciaExitosaId);
                var usuario = context.Usuario.First(x => x.UsuarioId == usuarioId);

                if (experienciExitosa.EmpresaId != usuario.EmpresaId)
                    return TipoPermiso.Restringido;

                var faseActual = context.Fase.FirstOrDefault(x => x.EsActual == true);
                
                if (faseActual == null)
                    return TipoPermiso.Lectura;

                if (faseActual.FaseId == experienciExitosa.FaseId && (faseId == -1 || faseActual.FaseId == faseId))
                    return TipoPermiso.Escritura;
                else
                    return TipoPermiso.Lectura;
            }
            catch (Exception ex)
            {
            }

            return TipoPermiso.Restringido;
        }
        */
        #endregion
    }
}