using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
namespace PROMPERU.AULAVIRTUAL.BE.CURSOS
{
    public partial class CursoOnlineFavorito
    {

  
        public int CursoOnlineFavoritoId { get; set; }
        public int UsuarioId { get; set; }

        

        public int CursoOnlineId { get; set; }




        public bool EstadoFavorito { get; set; }
        public CursoOnline CursoOnline { get; set; }
    }
}
