using System.ComponentModel.DataAnnotations;

namespace PROMPERU.AULAVIRTUAL.WEB.ViewModel.Home
{
    public class ForgotPasswordViewModel
    {
        [EmailAddress]
        [Required]
        public string Codigo { get; set; }
    }
}