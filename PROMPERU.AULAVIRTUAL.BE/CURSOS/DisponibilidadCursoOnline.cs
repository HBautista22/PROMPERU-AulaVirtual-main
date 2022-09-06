
namespace PROMPERU.AULAVIRTUAL.BE.CURSOS
{

using System;
    using System.Collections.Generic;
    
public partial class DisponibilidadCursoOnline
{

    public int DisponibilidadCursoOnlineId { get; set; }

    public int CursoOnlineId { get; set; }

    public System.DateTime FechaInicio { get; set; }

    public Nullable<System.DateTime> FechaFin { get; set; }



    public virtual CursoOnline CursoOnline { get; set; }

}

}
