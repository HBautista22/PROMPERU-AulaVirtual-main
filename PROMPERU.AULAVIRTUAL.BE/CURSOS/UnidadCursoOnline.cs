
namespace PROMPERU.AULAVIRTUAL.BE.CURSOS
{
    using PROMPERU.AULAVIRTUAL.BE.PARAMETROS;

    using System;
    using System.Collections.Generic;

    public partial class UnidadCursoOnline
    {

        //[System.Diagnostics.CodeAnalysis.SuppressMessage("Microsoft.Usage", "CA2214:DoNotCallOverridableMethodsInConstructors")]
        public UnidadCursoOnline()
        {
            this.TareaUnidadCursoOnline = new HashSet<TareaUnidadCursoOnline>();

            this.AvanceMatriculaCursoOnline = new HashSet<AvanceMatriculaCursoOnline>();

            this.EvaluacionCursoOnline = new HashSet<EvaluacionCursoOnline>();

            this.LogUnidad = new HashSet<LogUnidad>();

            this.ParametroScorm = new HashSet<ParametroScorm>();

            this.PreguntaCursoOnline = new HashSet<PreguntaCursoOnline>();

            this.RecursoCursoOnline = new HashSet<RecursoCursoOnline>();

            this.UnidadCursoOnline1 = new HashSet<UnidadCursoOnline>();

        }


        public int UnidadCursoOnlineId { get; set; }

        public int CursoOnlineId { get; set; }

        public Nullable<int> TipoUnidadId { get; set; }

        public Nullable<int> UnidadCursoOnlinePadreId { get; set; }

        public string Nombre { get; set; }

        public string Descripcion { get; set; }

        public string GUID { get; set; }

        public string RutaArchivoOriginal { get; set; }

        public string RutaArchivoInicio { get; set; }

        public string RutaWeb { get; set; }

        public int Orden { get; set; }
        public Nullable<int> Video { get; set; }
        public Nullable<int> Vivo { get; set; }


        public string Estado { get; set; }

        public int TiempoPermanencia { get; set; }

        public Nullable<int> AnchoContenedor { get; set; }

        public Nullable<int> AltoContenedor { get; set; }

        public string TipoUnidad { get; set; }

        public string RutaArchivoAdicional { get; set; }

        public string ExtensionArchivoAdicional { get; set; }

        public string TipoContenidoArchivoAdicional { get; set; }

        public bool HasTarea { get; set; }
        public Nullable<System.DateTime> TareaFechaLimite { get; set; }

        public virtual ICollection<TareaUnidadCursoOnline> TareaUnidadCursoOnline { get; set; }

        //[System.Diagnostics.CodeAnalysis.SuppressMessage("Microsoft.Usage", "CA2227:CollectionPropertiesShouldBeReadOnly")]

        public virtual ICollection<AvanceMatriculaCursoOnline> AvanceMatriculaCursoOnline { get; set; }

        //[System.Diagnostics.CodeAnalysis.SuppressMessage("Microsoft.Usage", "CA2227:CollectionPropertiesShouldBeReadOnly")]

        public virtual ICollection<EvaluacionCursoOnline> EvaluacionCursoOnline { get; set; }

        //[System.Diagnostics.CodeAnalysis.SuppressMessage("Microsoft.Usage", "CA2227:CollectionPropertiesShouldBeReadOnly")]

        public virtual ICollection<LogUnidad> LogUnidad { get; set; }

        //[System.Diagnostics.CodeAnalysis.SuppressMessage("Microsoft.Usage", "CA2227:CollectionPropertiesShouldBeReadOnly")]

        public virtual ICollection<ParametroScorm> ParametroScorm { get; set; }

        //[System.Diagnostics.CodeAnalysis.SuppressMessage("Microsoft.Usage", "CA2227:CollectionPropertiesShouldBeReadOnly")]

        public virtual ICollection<PreguntaCursoOnline> PreguntaCursoOnline { get; set; }

        //[System.Diagnostics.CodeAnalysis.SuppressMessage("Microsoft.Usage", "CA2227:CollectionPropertiesShouldBeReadOnly")]

        public virtual ICollection<RecursoCursoOnline> RecursoCursoOnline { get; set; }

        public virtual TipoUnidad TipoUnidad1 { get; set; }

        //[System.Diagnostics.CodeAnalysis.SuppressMessage("Microsoft.Usage", "CA2227:CollectionPropertiesShouldBeReadOnly")]

        public virtual ICollection<UnidadCursoOnline> UnidadCursoOnline1 { get; set; }

        public virtual UnidadCursoOnline UnidadCursoOnline2 { get; set; }

        public virtual CursoOnline CursoOnline { get; set; }

    }

}
