
namespace PROMPERU.AULAVIRTUAL.BE.CURSOS
{

using System;
    using System.Collections.Generic;
    using PROMPERU.AULAVIRTUAL.BE.USUARIO;
public partial class LogUnidad
{

    public int LogUnidadId { get; set; }

    public string Estado { get; set; }

    public System.DateTime FechaRegistro { get; set; }

    public int UnidadCursoOnlineId { get; set; }

    public int MatriculaCursoOnlineId { get; set; }

    public int UsuarioId { get; set; }



    public virtual MatriculaCursoOnline MatriculaCursoOnline { get; set; }

    public virtual UnidadCursoOnline UnidadCursoOnline { get; set; }

    public virtual Usuario Usuario { get; set; }

}

}
