using Foolproof;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Web;
using PROMPERU.AULAVIRTUAL.BE.PARAMETROS;
using PROMPERU.AULAVIRTUAL.DA;




namespace PROMPERU.AULAVIRTUAL.WEB.ViewModel.Asesor
{
    public class RegisterAsesorViewModel
    {
        public int UsuarioId { get; set; }

        [Required(ErrorMessage = "El Email es obligatorio")]
        [EmailAddress]
        public string Email { get; set; }
        [Required(ErrorMessage = "La confirmación del Email es obligatorio")]
        [EmailAddress]
        [Compare("Email", ErrorMessage = "Los correos no coinciden")]
        [Display(Name = "Confirmar correo")]
        public string ConfirmarEmail { get; set; }
        [Required(ErrorMessage = "El Documento es obligatorio")]
        //[StringLength(8, MinimumLength = 8, ErrorMessage = "DNI solo puede tener 8 números")]
        //[RegularExpression("^[0-9]+$")]
        public string DNI { get; set; }
        [Required(ErrorMessage = "El campo Nombres es obligatorio")]
        public string Nombres { get; set; }
        [Required(ErrorMessage = "El campo Apellidos es obligatorio")]
        public string Apellidos { get; set; }

        [Required(ErrorMessage = "El campo Password es obligatorio")]
        public string Password { get; set; }

        public string Nacionalidad { get; set; }

        [Display(Name = "Nacionalidad")]
        [Required(ErrorMessage = "El campo Nacionalidad es obligatorio")]
        public int NacionalidadId2 { get; set; }

        public List<Parametro> Nacionalidades { get; set; }

        public int Conversion { get; set; }

        [RegularExpression("^[0-9]+$")]
        [Display(Name = "Télefono")]
        public string Telefono { get; set; }
        public string Ubigeo { get; set; }
        public string Direccion { get; set; }
        public string Genero { get; set; }
        [Required(ErrorMessage = "El campo Tiene test es obligatorio")]
        [Display(Name = "Tiene test del exportador")]
        public bool TieneTest { get; set; }


        public List<RutexSector> LstSector { get; set; }
        public int[] Sector { get; set; }

        //[Required(ErrorMessage = "Debe indicar que ha leído los términos y condiciones")]
        //[Display(Name = "He leido y acepto las politicas de privacidad, el tratamiento de datos y los términos & condiciones.")]
        //[RequiredIf("AceptaTerminosCondiciones","true",ErrorMessage = "Debe indicar que ha leído los términos y condiciones")]
        //[Range(typeof(bool), "true", "true", ErrorMessage = "Debe indicar que ha leído los términos y condiciones")]
        public bool AceptaTerminosCondiciones { get; set; }

        //[Required(ErrorMessage = "Debe indicar que ha leído los términos y condiciones")]
        //[RegularExpression("^[1-9]+$", ErrorMessage = "Debe indicar que ha leído los términos y condiciones")]
        public int AceptaTerminosCondiciones2 { get; set; }


        [RequiredIf("TipoRegistro", "0", ErrorMessage = "El campo CIIU es obligatorio")]
        [Display(Name = "Código CIIU")]
        public string CIIU { get; set; }

        [Required(ErrorMessage = "El campo Tipo de Registro es obligatorio")]
        public string TipoRegistro { get; set; }

        [RequiredIf("TipoRegistro", "0", ErrorMessage = "El campo RUC es obligatorio")]
        [StringLength(11, MinimumLength = 11, ErrorMessage = "RUC solo puede tener 11 números")]
        [RegularExpression("^[0-9]+$")]
        public string RUC { get; set; }
        [RequiredIf("TipoRegistro", "0", ErrorMessage = "El campo Razón Social es obligatorio")]
        [Display(Name = "Razón Social")]
        public string RazonSocial { get; set; }
        public string[] Productos { get; set; }
        [RequiredIf("TipoRegistro", "0", ErrorMessage = "El campo Email Empresa es obligatorio")]
        [EmailAddress]
        [Display(Name = "Email Empresa")]
        public string EmailEmpresa { get; set; }
        public List<AsesorBaseModel> LstGeneros { get; set; }
        public string Cargo { get; set; }

        //EvolEntities _context = new EvolEntities();

        public RegisterAsesorViewModel()
        {
            ParametrosDA parametrosDA = new ParametrosDA(); 
            
            LstGeneros = new List<AsesorBaseModel>
            {
                new AsesorBaseModel("MAL","Hombre"),
                new AsesorBaseModel("FEM","Mujer"),
                new AsesorBaseModel("OTH","Prefiero no decirlo")
            };

            Nacionalidades = parametrosDA.ListarParametro();
            LstSector = parametrosDA.ListarRutexSector();
        }
    }

    public class AsesorBaseModel
    {
        public string Codigo { get; set; }
        public string Nombre { get; set; }
        public AsesorBaseModel()
        {
        }
        public AsesorBaseModel(string codigo, string nombre)
        {
            Codigo = codigo;
            Nombre = nombre;
        }
    }
}