

namespace PROMPERU.AULAVIRTUAL.BE.CURSOS
{

using System;
    using System.Collections.Generic;
    
public partial class RecursoCursoOnline
{

    public int RecursoCursoOnlineId { get; set; }

    public string Nombre { get; set; }

    public string Ruta { get; set; }

    public string Tipo { get; set; }

    public string Estado { get; set; }

    public int UnidadCursoOnlineId { get; set; }

    public string Extension { get; set; }

    public string TipoContenido { get; set; }



    public virtual UnidadCursoOnline UnidadCursoOnline { get; set; }

}

}
