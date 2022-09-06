using System;

namespace PROMPERU.AULAVIRTUAL.BE.CURSOS
{
    public class TareaUnidadCursoOnline
    {
        public int TareaUnidadCursoOnlineId { get; set; }
        public int UnidadCursoOnlineId { get; set; }
        public int AlumnoId { get; set; }
        public string RutaTarea { get; set; }
        public int? Nota { get; set; }
        public string ComentarioProfesor { get; set; }
        public string Estado { get; set; }
        public DateTime? FechaAprobacion { get; set; }
        public DateTime FechaEnvio { get; set; }
        public string RutaTareaCorregido { get; set; }

        public virtual UnidadCursoOnline UnidadCursoOnline { get; set; }
    }
}
