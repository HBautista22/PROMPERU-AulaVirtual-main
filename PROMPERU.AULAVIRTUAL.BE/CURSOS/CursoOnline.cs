

namespace PROMPERU.AULAVIRTUAL.BE.CURSOS
{

using System;
    using System.Collections.Generic;
    
public partial class CursoOnline
{

    [System.Diagnostics.CodeAnalysis.SuppressMessage("Microsoft.Usage", "CA2214:DoNotCallOverridableMethodsInConstructors")]
    public CursoOnline()
    {

        this.DetalleCursoOnlineMaestro = new HashSet<DetalleCursoOnlineMaestro>();

        this.DisponibilidadCursoOnline = new HashSet<DisponibilidadCursoOnline>();

        this.MatriculaCursoOnline = new HashSet<MatriculaCursoOnline>();

        this.PreguntaCursoOnline = new HashSet<PreguntaCursoOnline>();

        this.UnidadCursoOnline = new HashSet<UnidadCursoOnline>();

    }

        public int Calificacion { get; set; }
        public int CantidadCalificacion { get; set; }
        public int CursoOnlineId { get; set; }

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

    public Nullable<int> DisponibilidadCurso { get; set; }

    public Nullable<int> UsuarioProfesor { get; set; }
    public Nullable<int> UsuarioCreacion { get; set; }
        
     

    public virtual CategoriaCursoOnline CategoriaCursoOnline { get; set; }

    [System.Diagnostics.CodeAnalysis.SuppressMessage("Microsoft.Usage", "CA2227:CollectionPropertiesShouldBeReadOnly")]

    public virtual ICollection<DetalleCursoOnlineMaestro> DetalleCursoOnlineMaestro { get; set; }

    [System.Diagnostics.CodeAnalysis.SuppressMessage("Microsoft.Usage", "CA2227:CollectionPropertiesShouldBeReadOnly")]

    public virtual ICollection<DisponibilidadCursoOnline> DisponibilidadCursoOnline { get; set; }

    [System.Diagnostics.CodeAnalysis.SuppressMessage("Microsoft.Usage", "CA2227:CollectionPropertiesShouldBeReadOnly")]

    public virtual ICollection<MatriculaCursoOnline> MatriculaCursoOnline { get; set; }

    [System.Diagnostics.CodeAnalysis.SuppressMessage("Microsoft.Usage", "CA2227:CollectionPropertiesShouldBeReadOnly")]

    public virtual ICollection<PreguntaCursoOnline> PreguntaCursoOnline { get; set; }

    [System.Diagnostics.CodeAnalysis.SuppressMessage("Microsoft.Usage", "CA2227:CollectionPropertiesShouldBeReadOnly")]

    public virtual ICollection<UnidadCursoOnline> UnidadCursoOnline { get; set; }
    public bool EstadoFavorito { get; set; }
    }

}
