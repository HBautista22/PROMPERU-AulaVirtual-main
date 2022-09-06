using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using PROMPERU.AULAVIRTUAL.BE.CURSOS;
using PROMPERU.AULAVIRTUAL.BE.USUARIO;
using PROMPERU.AULAVIRTUAL.DA;

namespace PROMPERU.AULAVIRTUAL.BL
{
    public class GrupoBL
    {
        public List<Grupo> ListarGrupo()
        {
            GrupoDA SQL = new GrupoDA();
            return SQL.ListarGrupo();
        }

        public Grupo ObtenerGrupoPorId(int? GrupoId)
        {
            GrupoDA SQL = new GrupoDA();
            return SQL.ObtenerGrupoPorId(GrupoId);
        }

        public List<Grupo> ListarAddGrupoLISPorUsuarioId(int usuarioId)
        {
            GrupoDA SQL = new GrupoDA();
            return SQL.ListarAddGrupoLISPorUsuarioId(usuarioId);
        }

        public List<Grupo> ListarAddGrupoSELPorUsuarioId(int usuarioId)
        {
            GrupoDA SQL = new GrupoDA();
            return SQL.ListarAddGrupoSELPorUsuarioId(usuarioId);
        }

        public void ActualizarGrupo(Grupo grupo)
        {
            GrupoDA SQL = new GrupoDA();
            SQL.ActualizarGrupo(grupo);
        }

        public void InsertarGrupo(Grupo grupo)
        {
            GrupoDA SQL = new GrupoDA();
            SQL.InsertarGrupo(grupo);
        }
    }
}
