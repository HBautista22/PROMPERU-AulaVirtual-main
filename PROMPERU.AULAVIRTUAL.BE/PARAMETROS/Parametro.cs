
namespace PROMPERU.AULAVIRTUAL.BE.PARAMETROS
{

using System;
    using System.Collections.Generic;
    
public partial class Parametro
{

    public int ParametroId { get; set; }

    public int ParametroGrupoId { get; set; }

    public Nullable<int> ParametroPadreId { get; set; }

    public string Codigo { get; set; }

    public string Descripcion { get; set; }

    public string DescripcionCorta { get; set; }

    public string DescripcionEN { get; set; }

    public string DescripcionCortaEN { get; set; }

    public string Valor { get; set; }

    public Nullable<int> Orden { get; set; }

    public string Propiedades { get; set; }

    public bool EsActivo { get; set; }



    public virtual ParametroGrupo ParametroGrupo { get; set; }

}

}
