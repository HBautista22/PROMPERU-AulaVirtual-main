
namespace PROMPERU.AULAVIRTUAL.BE.CURSOS
{

    using System;
    using System.Collections.Generic;

    public partial class EvaluacionCursoOnline
    {

        //[System.Diagnostics.CodeAnalysis.SuppressMessage("Microsoft.Usage", "CA2214:DoNotCallOverridableMethodsInConstructors")]
        public EvaluacionCursoOnline()
        {

            this.PreguntaEvaluacionCursoOnline = new HashSet<PreguntaEvaluacionCursoOnline>();

        }


        public int EvaluacionCursoOnlineId { get; set; }

        public int MatriculaCursoOnlineId { get; set; }

        public int UnidadCursoOnlineId { get; set; }

        public System.DateTime FechaInicio { get; set; }

        public Nullable<System.DateTime> FechaFin { get; set; }

        public Nullable<decimal> Nota { get; set; }

        public decimal PorcentajeAprobacion { get; set; }

        public decimal PuntajeTotal { get; set; }

        public Nullable<decimal> PuntajeObtenido { get; set; }



        public virtual MatriculaCursoOnline MatriculaCursoOnline { get; set; }

        public virtual UnidadCursoOnline UnidadCursoOnline { get; set; }

        //[System.Diagnostics.CodeAnalysis.SuppressMessage("Microsoft.Usage", "CA2227:CollectionPropertiesShouldBeReadOnly")]

        public virtual ICollection<PreguntaEvaluacionCursoOnline> PreguntaEvaluacionCursoOnline { get; set; }

    }

}
