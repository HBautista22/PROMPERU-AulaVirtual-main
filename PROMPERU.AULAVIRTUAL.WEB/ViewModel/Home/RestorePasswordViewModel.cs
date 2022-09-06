using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Web;

namespace PROMPERU.AULAVIRTUAL.WEB.ViewModel.Home
{
    public class RestorePasswordViewModel
    {
        [Required]
        public string Codigo { get; set; }
        [Required]
        [Display(Name = "Contraseña")]
        public string Contrasena { get; set; }
        [Required]
        [Compare("Contrasena", ErrorMessage = "Las contraseñas no coinciden")]
        [Display(Name = "Confirmar contraseña")]
        public string ConfirmarContrasena { get; set; }
        public int CambioId { get; set; }
    }
}