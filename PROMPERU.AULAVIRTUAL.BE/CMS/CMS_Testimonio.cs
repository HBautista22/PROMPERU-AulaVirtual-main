
namespace PROMPERU.AULAVIRTUAL.BE.CMS
{

using System;
    using System.Collections.Generic;
    
public partial class CMS_Testimonio
{

    public int Id { get; set; }

    public string Titulo { get; set; }

    public string Descripcion { get; set; }

    public string Imagen { get; set; }

    public string Nombre { get; set; }

    public string Empresa { get; set; }

    public bool EsPublicado { get; set; }

    public Nullable<System.DateTime> FechaEdicion { get; set; }

    public Nullable<int> UsuarioEdicionId { get; set; }

}

}
