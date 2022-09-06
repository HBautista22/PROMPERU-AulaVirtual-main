using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Linq;
using System.Reflection;
using System.Web;
using System.Web.Mvc;

namespace PROMPERU.AULAVIRTUAL.WEB.Helpers
{
    public static class ConvertHelpers
    {
        private static readonly DateTime unixEpoch = new DateTime(1970, 1, 1, 0, 0, 0, 0, DateTimeKind.Utc);

        public static string GetAppSeting(string key, string defaultValue = "")
        {
            try
            {
                return System.Configuration.ConfigurationManager.AppSettings[key] ?? defaultValue;
            }
            catch (Exception)
            {
                return String.Empty;
            }
        }

        public static String ToDurationString(this Int32 val)
        {
            var hours = val / 60;
            var minutes = val % 60;
            var text = String.Empty;
            if (hours > 0)
                text += hours.ToString() + "h ";
            text += minutes.ToString("D2") + " m";
            return text;
        }

        public static Int32 ToInteger(this object val)
        {
            return ConvertHelpers.ToInteger(val, 0);
        }

        public static Int32 ToInteger(this object val, Int32 def)
        {
            try
            {
                Int32 reval = 0;
                if (val == null)
                    return 0;
                if (Int32.TryParse(val.ToString(), out reval))
                    return reval;
            }
            catch (Exception ex)
            {
            }

            return def;
        }

        public static Decimal ToDecimal(this object val)
        {
            return ConvertHelpers.ToDecimal(val, 0);
        }

        public static decimal ToDecimal(this object val, decimal def)
        {
            try
            {
                if (val == null) return 0;

                if (decimal.TryParse(val.ToString(), out decimal result))
                    return result;
            }
            catch (Exception)
            {
                // ignored
            }

            return def;
        }

        public static long ToJsonDateTime(this DateTime val)
        {
            return ((long)(val - unixEpoch).TotalSeconds) * 1000;
        }

        public static String ToFullCallendarDate(this DateTime val)
        {
            return val.ToString("yyyy-MM-dd hh:mm:ss");
        }
        public static DateTime ToDateTime(this object val)
        {
            return ConvertHelpers.ToDateTime(val, DateTime.MinValue);
        }

        /// <summary>
        /// Convierte una fecha UTC a una fecha "Local". Se toma la hora de Perú (GMT-5)
        /// </summary>
        /// <param name="val">Fecha, en UTC</param>
        /// <returns>Fecha, en hora de Perú</returns>
        public static DateTime ToLocalDate(this DateTime val)
        {
            return TimeZoneInfo.ConvertTimeFromUtc(val, TimeZoneInfo.FindSystemTimeZoneById("SA Pacific Standard Time"));
        }

        /// <summary>
        /// Convierte una fecha UTC a una fecha "Local". Se toma la hora de Perú (GMT-5)
        /// </summary>
        /// <param name="val">Fecha, en UTC</param>
        /// <returns>Fecha, en hora de Perú</returns>
        public static DateTime? ToLocalDate(this DateTime? val)
        {
            if (val.HasValue)
                return ToLocalDate(val.Value);
            else
                return val;
        }

        /// <summary>
        /// Convierte una fecha UTC a una string de una fecha "Local" con formato. Se toma la hora de Perú (GMT-5)
        /// </summary>
        /// <param name="val">Fecha, en UTC</param>
        /// <param name="format">Formato para el string</param>
        /// <returns>Fecha, en hora de Perú</returns>
        public static String ToLocalDateString(this DateTime val, String format)
        {
            return val.ToLocalDate().ToString(format);
        }

        /// <summary>
        /// Convierte una fecha UTC a una string de una fecha "Local" con formato. Se toma la hora de Perú (GMT-5)
        /// </summary>
        /// <param name="val">Fecha, en UTC</param>
        /// <param name="format">Formato para el string</param>
        /// <returns>Fecha, en hora de Perú</returns>
        public static String ToLocalDateString(this DateTime? val, String format)
        {
            if (val.HasValue)
                return ToLocalDateString(val.Value, format);
            else
                return String.Empty;
        }

        /// <summary>
        /// Convierte una fecha UTC a una string de una fecha "Local" con formato. Se toma la hora de Perú (GMT-5)
        /// </summary>
        /// <param name="val">Fecha, en UTC</param>
        /// <param name="format">Formato para el string</param>
        /// <returns>Fecha, en hora de Perú</returns>
        public static String ToLocalDateString(this DateTime val)
        {
            return ToLocalDateString(val, "dd/MM/yyyy");
        }

        /// <summary>
        /// Convierte una fecha UTC a una string de una fecha "Local" con formato. Se toma la hora de Perú (GMT-5)
        /// </summary>
        /// <param name="val">Fecha, en UTC</param>
        /// <param name="format">Formato para el string</param>
        /// <returns>Fecha, en hora de Perú</returns>
        public static String ToLocalDateString(this DateTime? val)
        {
            if (val.HasValue)
                return ToLocalDateString(val.Value, "dd/MM/yyyy");
            else
                return String.Empty;
        }

        public static DateTime ToDateTime(this object val, DateTime def)
        {
            try
            {
                DateTime reval = DateTime.MinValue;

                if (DateTime.TryParse(val.ToString(), out reval))
                    return reval;
            }
            catch (Exception ex)
            {
            }

            return def;
        }

        public static Boolean IsBetween(this DateTime val, DateTime Start, DateTime End)
        {
            return val >= Start && val <= End;
        }

        public static Boolean ToBoolean(this object val)
        {
            return ConvertHelpers.ToBoolean(val, false);
        }

        // public static DataTable ToDataTable<T>(this IList<T> data)
        // {
        //     PropertyDescriptorCollection properties =
        //         TypeDescriptor.GetProperties(typeof(T));
        //     DataTable table = new DataTable();
        //     foreach (PropertyDescriptor prop in properties)
        //         table.Columns.Add(prop.Name, Nullable.GetUnderlyingType(prop.PropertyType) ?? prop.PropertyType);
        //     foreach (T item in data)
        //     {
        //         DataRow row = table.NewRow();
        //         foreach (PropertyDescriptor prop in properties)
        //             row[prop.Name] = prop.GetValue(item) ?? DBNull.Value;
        //         table.Rows.Add(row);
        //     }
        //     return table;
        // }

        public static Boolean ToBoolean(this object val, Boolean def)
        {
            try
            {
                Boolean reval = false;

                if (Boolean.TryParse(val.ToString(), out reval))
                    return reval;
            }
            catch (Exception ex)
            {
            }

            return def;
        }

        public static string ToJson(this object val)
        {
            return JsonConvert.SerializeObject(val, Formatting.Indented, new JsonSerializerSettings() { PreserveReferencesHandling = PreserveReferencesHandling.None, ReferenceLoopHandling = ReferenceLoopHandling.Ignore, ObjectCreationHandling = ObjectCreationHandling.Reuse });
        }

        public static MvcHtmlString ToHTMLJson(this object val)
        {
            return MvcHtmlString.Create(val.ToJson());
        }

        public static String SafeTrim(this String val)
        {
            try
            {
                return val.Trim();
            }
            catch (Exception)
            {
                return String.Empty;
            }
        }

        public static String ToSafeString(this object val)
        {
            try
            {
                return val.ToString();
            }
            catch (Exception)
            {
                return String.Empty;
            }
        }

        public static IDictionary<String, Object> ToObjectDictionary(this object val)
        {
            try
            {
                return val.GetType().GetProperties(BindingFlags.Instance | BindingFlags.Public).ToDictionary(prop => prop.Name, prop => prop.GetValue(val, null));
            }
            catch (Exception)
            {
                return null;
                throw;
            }

        }

        #region Enum Convert Helper
        public static T ToEnum<T>(this String value)
        {
            return (T)Enum.Parse(typeof(T), value, true);
        }
        #endregion
    }

    public static class PeruDateTime
    {
        public static DateTime Now
        {
            get
            {
                return (TimeZoneInfo.ConvertTimeFromUtc(DateTime.UtcNow, TimeZoneInfo.FindSystemTimeZoneById("SA Pacific Standard Time")));
            }

        }
    }
}