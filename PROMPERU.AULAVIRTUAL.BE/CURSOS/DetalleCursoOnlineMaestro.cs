
namespace PROMPERU.AULAVIRTUAL.BE.CURSOS
{

using System;
    using System.Collections.Generic;
    
public partial class DetalleCursoOnlineMaestro
{

    public int DetalleCursoOnlineId { get; set; }

    public int CursoOnlineMaestroId { get; set; }

    public int CursoOnlineId { get; set; }

    public Nullable<int> Orden { get; set; }

    public Nullable<System.DateTime> FechaCreacion { get; set; }

    public string Estado { get; set; }

    public string Nombre { get; set; }



    public virtual CursoOnlineMaestro CursoOnlineMaestro { get; set; }

    public virtual CursoOnline CursoOnline { get; set; }

}

}
