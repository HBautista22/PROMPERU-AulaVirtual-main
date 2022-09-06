using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Web;

namespace PROMPERU.AULAVIRTUAL.WEB.ViewModel.Home
{
    public class LoginViewModel
    {
        [Required(ErrorMessage = "El nombre de usuario es obligatorio")]
        public String Codigo { get; set; }
        [Required(ErrorMessage = "La contraseña es obligatoria")]
        public String Contrasena { get; set; }
        public string Action { get; set; }
        public string Controller { get; set; }
        public string ReturnUrl { get; set; }
    }
}