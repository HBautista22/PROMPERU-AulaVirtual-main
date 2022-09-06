
namespace PROMPERU.AULAVIRTUAL.BE.CMS
{

using System;
    using System.Collections.Generic;
    
public partial class CMS_Agenda
{

    public int Id { get; set; }

    public string Nombre { get; set; }

    public Nullable<System.DateTime> FechaInicio { get; set; }

    public Nullable<System.DateTime> FechaFin { get; set; }

    public string Imagen { get; set; }

    public string Sumilla { get; set; }

    public string Archivo { get; set; }

    public string RutaInscripcion { get; set; }

    public bool EsPublicado { get; set; }

    public Nullable<System.DateTime> FechaEdicion { get; set; }

    public Nullable<int> UsuarioEdicionId { get; set; }

}

}
