
namespace PROMPERU.AULAVIRTUAL.BE.CURSOS
{

using System;
    using System.Collections.Generic;
    
public partial class CursoGrupo
{

    public int CursoGrupoId { get; set; }

    public Nullable<int> GrupoId { get; set; }

    public Nullable<int> CursoOnlineMaestroId { get; set; }

    public string Estado { get; set; }

    public string Nombre { get; set; }

    public int? CursoOnlineId { get; set; }



    public virtual CursoOnlineMaestro CursoOnlineMaestro { get; set; }

    public virtual Grupo Grupo { get; set; }

}

}
