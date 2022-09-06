namespace PROMPERU.AULAVIRTUAL.BE.CURSOS
{
    using PROMPERU.AULAVIRTUAL.BE.PARAMETROS;
    using PROMPERU.AULAVIRTUAL.BE.USUARIO;
    using System;
    using System.Collections.Generic;

    public partial class MatriculaCursoOnline
    {

        //[System.Diagnostics.CodeAnalysis.SuppressMessage("Microsoft.Usage", "CA2214:DoNotCallOverridableMethodsInConstructors")]
        public MatriculaCursoOnline()
        {

            this.AvanceMatriculaCursoOnline = new HashSet<AvanceMatriculaCursoOnline>();

            this.EvaluacionCursoOnline = new HashSet<EvaluacionCursoOnline>();

            this.LogUnidad = new HashSet<LogUnidad>();

            this.ParametroScorm = new HashSet<ParametroScorm>();

        }


        public int MatriculaCursoOnlineId { get; set; }

        public int UsuarioId { get; set; }

        public int CursoOnlineId { get; set; }

        public System.DateTime FechaMatricula { get; set; }

        public Nullable<System.DateTime> FechaCompletado { get; set; }

        public Nullable<System.DateTime> FechaAprobado { get; set; }

        public string Estado { get; set; }

        public decimal PorcentajeAvance { get; set; }

        public Nullable<decimal> Nota { get; set; }

        public Nullable<decimal> PorcentajeObtenido { get; set; }

        public Nullable<int> TotalUnidadesCursoOnline { get; set; }

        public string Grupo { get; set; }

        public Nullable<int> Ranking { get; set; }

        

        //[System.Diagnostics.CodeAnalysis.SuppressMessage("Microsoft.Usage", "CA2227:CollectionPropertiesShouldBeReadOnly")]

        public virtual ICollection<AvanceMatriculaCursoOnline> AvanceMatriculaCursoOnline { get; set; }

        //[System.Diagnostics.CodeAnalysis.SuppressMessage("Microsoft.Usage", "CA2227:CollectionPropertiesShouldBeReadOnly")]

        public virtual ICollection<EvaluacionCursoOnline> EvaluacionCursoOnline { get; set; }

        //[System.Diagnostics.CodeAnalysis.SuppressMessage("Microsoft.Usage", "CA2227:CollectionPropertiesShouldBeReadOnly")]

        public virtual ICollection<LogUnidad> LogUnidad { get; set; }

        //[System.Diagnostics.CodeAnalysis.SuppressMessage("Microsoft.Usage", "CA2227:CollectionPropertiesShouldBeReadOnly")]

        public virtual ICollection<ParametroScorm> ParametroScorm { get; set; }

        public virtual CursoOnline CursoOnline { get; set; }

        public virtual Usuario Usuario { get; set; }

    }

}
