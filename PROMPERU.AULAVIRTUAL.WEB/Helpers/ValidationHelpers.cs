using System;
using System.Collections.Generic;
using System.Globalization;
using System.Linq;
using System.Text.RegularExpressions;
using System.Web;
using System.Web.Mvc;

namespace PROMPERU.AULAVIRTUAL.WEB.Helpers
{
    public static class ValidationHelpers
    {
        public static JsonResult JsonValidation(this ModelStateDictionary state)
        {
            return new JsonResult
            {
                Data = new
                {
                    Tag = "ValidationError",
                    State = from e in state
                            where e.Value.Errors.Count > 0
                            select new
                            {
                                Name = e.Key,
                                Error = string.Concat(e.Value.Errors.Select(x => x.ErrorMessage)
                                                                    .Concat(e.Value.Errors.Where(x => x.Exception != null).Select(x => x.Exception.Message)))
                            }
                }
            };
        }

        public static bool IsValidEmail(string email)
        {
            try
            {
                var addr = new System.Net.Mail.MailAddress(email);
                return addr.Address == email;
            }
            catch
            {
                return false;
            }
        }
    }
}