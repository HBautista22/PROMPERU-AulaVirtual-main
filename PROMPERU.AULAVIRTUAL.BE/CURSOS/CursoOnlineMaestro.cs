
namespace PROMPERU.AULAVIRTUAL.BE.CURSOS
{

using System;
    using System.Collections.Generic;
    
public partial class CursoOnlineMaestro
{

    [System.Diagnostics.CodeAnalysis.SuppressMessage("Microsoft.Usage", "CA2214:DoNotCallOverridableMethodsInConstructors")]
    public CursoOnlineMaestro()
    {

        this.DetalleCursoOnlineMaestro = new HashSet<DetalleCursoOnlineMaestro>();

        this.CursoGrupo = new HashSet<CursoGrupo>();

    }


    public int CursoOnlineMaestroId { get; set; }

    public int? CursoOnlineId { get; set; }

    public string Codigo { get; set; }

    public string Nombre { get; set; }

    public int CategoriaCursoOnlineId { get; set; }

    public string Estado { get; set; }

    public string Descripcion { get; set; }

    public int NumPreguntasEvaluacion { get; set; }

    public int TiempoEvaluacion { get; set; }

    public decimal PorcentajeAprobacion { get; set; }

    public string RutaImagen { get; set; }

    public string TieneExamen { get; set; }

    public bool AutoMatriculaHabilitada { get; set; }

    public Nullable<bool> PermiteReinicioScorm { get; set; }

    public string RutaBanner { get; set; }

    public Nullable<System.DateTime> FechaCreacion { get; set; }

    public Nullable<decimal> Ranking { get; set; }

    public Nullable<int> NroRankings { get; set; }

    public Nullable<int> Orden { get; set; }

    public Nullable<int> CantidadCursos { get; set; }



    [System.Diagnostics.CodeAnalysis.SuppressMessage("Microsoft.Usage", "CA2227:CollectionPropertiesShouldBeReadOnly")]

    public virtual ICollection<DetalleCursoOnlineMaestro> DetalleCursoOnlineMaestro { get; set; }

    [System.Diagnostics.CodeAnalysis.SuppressMessage("Microsoft.Usage", "CA2227:CollectionPropertiesShouldBeReadOnly")]

    public virtual ICollection<CursoGrupo> CursoGrupo { get; set; }

}

}
