
namespace PROMPERU.AULAVIRTUAL.BE.CMS
{

using System;
    using System.Collections.Generic;
    
public partial class CMS_Label
{

    public int CMS_LabelId { get; set; }

    public string Codigo { get; set; }

    public string Texto { get; set; }

    public bool EsRaw { get; set; }

    public string TipoComponente { get; set; }

    public Nullable<System.DateTime> FechaModificacion { get; set; }

}

}
