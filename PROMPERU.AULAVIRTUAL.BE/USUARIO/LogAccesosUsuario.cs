
namespace PROMPERU.AULAVIRTUAL.BE.USUARIO
{

using System;
    using System.Collections.Generic;
    
public partial class LogAccesosUsuario
{

    public int LogAccesoUsuarioId { get; set; }

    public System.DateTime FechaAcceso { get; set; }

    public string DatosNavegador { get; set; }

    public string UserAgent { get; set; }

    public string TipoAcceso { get; set; }

    public int UsuarioId { get; set; }

    public string IPAcceso { get; set; }



    public virtual Usuario Usuario { get; set; }

}

}
