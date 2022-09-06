namespace PROMPERU.AULAVIRTUAL.BE.CURSOS
{

using System;
    using System.Collections.Generic;
    
public partial class RespuestaSeleccionadaEvaluacionCursoOnline
{

    public int RespuestaSeleccionadaEvaluacionCursoOnlineId { get; set; }

    public int PreguntaEvaluacionCursoOnlineId { get; set; }

    public int RespuestaCursoOnlineId { get; set; }

    public int OrdenSeleccionado { get; set; }



    public virtual PreguntaEvaluacionCursoOnline PreguntaEvaluacionCursoOnline { get; set; }

    public virtual RespuestaCursoOnline RespuestaCursoOnline { get; set; }

}

}
