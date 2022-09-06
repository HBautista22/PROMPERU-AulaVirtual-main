using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Web;

namespace PROMPERU.AULAVIRTUAL.WEB.ViewModel.Home
{
    public class VerifyEmailViewModel
    {
        public string EmailToken { get; set; }
        public int CambioId { get; set; }
    }
}