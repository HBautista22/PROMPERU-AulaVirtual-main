
//------------------------------------------------------------------------------
// <auto-generated>
//     Este código se generó a partir de una plantilla.
//
//     Los cambios manuales en este archivo pueden causar un comportamiento inesperado de la aplicación.
//     Los cambios manuales en este archivo se sobrescribirán si se regenera el código.
// </auto-generated>
//------------------------------------------------------------------------------


namespace PROMPERU.AULAVIRTUAL.WEB.Models
{

using System;
    using System.Collections.Generic;
    
public partial class ParametroGrupo
{

    [System.Diagnostics.CodeAnalysis.SuppressMessage("Microsoft.Usage", "CA2214:DoNotCallOverridableMethodsInConstructors")]
    public ParametroGrupo()
    {

        this.Parametro = new HashSet<Parametro>();

    }


    public int ParametroGrupoId { get; set; }

    public string Codigo { get; set; }

    public string Descripcion { get; set; }

    public string DescripcionCorta { get; set; }

    public string DescripcionEN { get; set; }

    public string DescripcionCortaEN { get; set; }



    [System.Diagnostics.CodeAnalysis.SuppressMessage("Microsoft.Usage", "CA2227:CollectionPropertiesShouldBeReadOnly")]

    public virtual ICollection<Parametro> Parametro { get; set; }

}

}
