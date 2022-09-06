
namespace PROMPERU.AULAVIRTUAL.BE.CMS
{

using System;
    using System.Collections.Generic;
    
public partial class CMS_PreguntaFrecuente
{

    public int Id { get; set; }

    public string Pregunta { get; set; }

    public string Respuesta { get; set; }

    public bool EsPublicado { get; set; }

    public Nullable<System.DateTime> FechaEdicion { get; set; }

    public Nullable<int> UsuarioEdicionId { get; set; }

}

}
