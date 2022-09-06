using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace PROMPERU.AULAVIRTUAL.WEB.Helpers
{
    public class Error
    {
        public Error(string key, string message)
        {
            Key = key;
            Message = message;
        }

        public string Key { get; set; }
        public string Message { get; set; }
    }

    public static class ModelStateExtensions
    {

        public static string AllErrors(this ModelStateDictionary modelState)
        {
            var result = new List<Error>();
            var erroneousFields = modelState.Where(ms => ms.Value.Errors.Any())
                                            .Select(x => new { x.Key, x.Value.Errors });

            foreach (var erroneousField in erroneousFields)
            {
                var fieldKey = erroneousField.Key;
                var fieldErrors = erroneousField.Errors
                                   .Select(error => new Error(fieldKey, error.ErrorMessage));
                result.AddRange(fieldErrors);
            }

            string errors = Environment.NewLine;

            foreach (var item in result)
            {
                errors += $"{item.Key}: {item.Message}{Environment.NewLine}";
            }

            return errors;
        }
    }
}