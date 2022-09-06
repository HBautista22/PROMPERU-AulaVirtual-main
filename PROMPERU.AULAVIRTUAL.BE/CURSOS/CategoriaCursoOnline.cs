
namespace PROMPERU.AULAVIRTUAL.BE.CURSOS
{

using System;
    using System.Collections.Generic;
    
public partial class CategoriaCursoOnline
{

    [System.Diagnostics.CodeAnalysis.SuppressMessage("Microsoft.Usage", "CA2214:DoNotCallOverridableMethodsInConstructors")]
    public CategoriaCursoOnline()
    {

        this.CursoOnline = new HashSet<CursoOnline>();

    }


    public int CategoriaCursoOnlineId { get; set; }

    public string Nombre { get; set; }

    public string Color { get; set; }



    [System.Diagnostics.CodeAnalysis.SuppressMessage("Microsoft.Usage", "CA2227:CollectionPropertiesShouldBeReadOnly")]

    public virtual ICollection<CursoOnline> CursoOnline { get; set; }

}

}
