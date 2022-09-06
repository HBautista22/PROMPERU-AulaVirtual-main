namespace PROMPERU.AULAVIRTUAL.BE.CURSOS
{

using System;
    using System.Collections.Generic;
    
public partial class TipoNotificaciones
{

    [System.Diagnostics.CodeAnalysis.SuppressMessage("Microsoft.Usage", "CA2214:DoNotCallOverridableMethodsInConstructors")]
    public TipoNotificaciones()
    {

        this.Notificaciones = new HashSet<Notificaciones>();

    }


    public int TipoNotificacionId { get; set; }

    public string Nombre { get; set; }

    public string Codigo { get; set; }

    public string Estado { get; set; }



    [System.Diagnostics.CodeAnalysis.SuppressMessage("Microsoft.Usage", "CA2227:CollectionPropertiesShouldBeReadOnly")]

    public virtual ICollection<Notificaciones> Notificaciones { get; set; }

}

}
