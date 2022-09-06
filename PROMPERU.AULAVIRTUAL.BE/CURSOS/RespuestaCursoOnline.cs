
namespace PROMPERU.AULAVIRTUAL.BE.CURSOS
{

using System;
    using System.Collections.Generic;
    
public partial class RespuestaCursoOnline
{

    [System.Diagnostics.CodeAnalysis.SuppressMessage("Microsoft.Usage", "CA2214:DoNotCallOverridableMethodsInConstructors")]
    public RespuestaCursoOnline()
    {

        this.RespuestaEvaluacionCursoOnline = new HashSet<RespuestaEvaluacionCursoOnline>();

        this.RespuestaSeleccionadaEvaluacionCursoOnline = new HashSet<RespuestaSeleccionadaEvaluacionCursoOnline>();

    }


    public int RespuestaCursoOnlineId { get; set; }

    public int PreguntaCursoOnlineId { get; set; }

    public string Texto { get; set; }

    public bool EsSolucion { get; set; }

    public Nullable<int> OrdenSolucion { get; set; }



    public virtual PreguntaCursoOnline PreguntaCursoOnline { get; set; }

    [System.Diagnostics.CodeAnalysis.SuppressMessage("Microsoft.Usage", "CA2227:CollectionPropertiesShouldBeReadOnly")]

    public virtual ICollection<RespuestaEvaluacionCursoOnline> RespuestaEvaluacionCursoOnline { get; set; }

    [System.Diagnostics.CodeAnalysis.SuppressMessage("Microsoft.Usage", "CA2227:CollectionPropertiesShouldBeReadOnly")]

    public virtual ICollection<RespuestaSeleccionadaEvaluacionCursoOnline> RespuestaSeleccionadaEvaluacionCursoOnline { get; set; }

}

}
