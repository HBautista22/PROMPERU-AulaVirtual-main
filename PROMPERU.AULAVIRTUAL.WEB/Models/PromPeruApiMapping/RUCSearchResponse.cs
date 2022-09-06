using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace PROMPERU.AULAVIRTUAL.WEB.Models.PromPeruApiMapping
{
    public class BaseResponse
    {
        public string status { get; set; }
        public string message { get; set; }
    }

    public class RUCSearchResponse : BaseResponse
    {
        public List<RUCSearchDetails> result { get; set; }
    }

    public class RUCSearchDetails
    {
        public string ciiu { get; set; }
        public string ruc { get; set; }
        public string razon { get; set; }
        public string nombrecomercial { get; set; }
        public string direccionfiscal { get; set; }
        public string codtipopersona { get; set; }
        public string desctipopersona { get; set; }
        public string codtipoempresa { get; set; }
        public string ubigeo { get; set; }
        public string fechaalta { get; set; }
    }

    public class DNISearchResponse : BaseResponse
    {
        public List<DNISearchDetails> result { get; set; }
    }

    public class DNISearchDetails
    {
        public string dni { get; set; }
        public string paterno { get; set; }
        public string materno { get; set; }
        public string nombres { get; set; }
        public string direccion { get; set; }
        public string ubigeo { get; set; }
    }
}