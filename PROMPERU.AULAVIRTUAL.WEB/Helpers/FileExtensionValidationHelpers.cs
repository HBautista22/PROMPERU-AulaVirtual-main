using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.IO;
using System.Linq;
using System.Web;

namespace PROMPERU.AULAVIRTUAL.WEB.Helpers
{
    public class FileExtensionsValidationHelpers : ValidationAttribute
    {
        public string Extension { get; set; }
        protected override ValidationResult IsValid(object value, ValidationContext validationContext)
        {
            if (value != null)
            {
                var extensiones = Extension.Split(',');

                HttpPostedFileWrapper file = (HttpPostedFileWrapper)value;
                string extentionArchivo = Path.GetExtension(file.FileName);

                if (!Extension.Contains(extentionArchivo))
                {
                    return new ValidationResult("Solo se permiten extension(es) " + Extension);
                }
            }
            return ValidationResult.Success;
        }
    }
}