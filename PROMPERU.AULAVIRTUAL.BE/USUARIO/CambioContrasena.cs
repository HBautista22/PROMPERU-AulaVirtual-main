
namespace PROMPERU.AULAVIRTUAL.BE.USUARIO
{

using System;
    using System.Collections.Generic;
    
public partial class CambioContrasena
{

    public int CambioContrasenaId { get; set; }

    public System.DateTime Fecha { get; set; }

    public string Token { get; set; }

    public int UsuarioId { get; set; }

    public Nullable<System.DateTime> FechaCambio { get; set; }



    public virtual Usuario Usuario { get; set; }

}

}
