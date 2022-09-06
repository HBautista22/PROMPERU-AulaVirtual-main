using System;
using System.Collections.Generic;
using System.Configuration;
using System.IO;
using System.Net.Mail;
using System.Linq;
using System.Net;
using System.Text;
using System.Web;
using PROMPERU.AULAVIRTUAL.WEB.Helpers;

namespace PROMPERU.AULAVIRTUAL.WEB.Logic
{
    public interface IBaseMail
    {
        void SendMultipleEmail(List<Tuple<string, string>> emails, string subject, string body);
        void SendSingleEmail(string email, string name, string subject, string body);
        void SendMultipleEmail(List<string> emails, string subject, string body);
    }

    public class MailSMTP : IBaseMail, IDisposable
    {
        public static string NewLine = "<br/>";
        private string _host { get; set; }
        private Int32 _port { get; set; }
        private string _emailFromPwd { get; set; }
        private SmtpClient _smtpClient { get; set; }
        private string _emailFrom { get; set; }
        private string _emailFromName { get; set; }

        /// <summary>
        /// "SMTP_FROM", "SMTP_FROM_PWD", "SMTP_FROM_NAME", "SMTP_HOST", "SMTP_PORT"
        /// </summary>
        public MailSMTP()
        {
            this._emailFrom = System.Configuration.ConfigurationManager.AppSettings.Get("SMTP_FROM");
            this._emailFromPwd = System.Configuration.ConfigurationManager.AppSettings.Get("SMTP_FROM_PWD");
            this._emailFromName = System.Configuration.ConfigurationManager.AppSettings.Get("SMTP_FROM_NAME");
            this._host = System.Configuration.ConfigurationManager.AppSettings.Get("SMTP_HOST");
            this._port = System.Configuration.ConfigurationManager.AppSettings.Get("SMTP_PORT").ToInteger(25);

            this._smtpClient = new SmtpClient(this._host);
            this._smtpClient.UseDefaultCredentials = false;
            this._smtpClient.Credentials = new NetworkCredential(this._emailFrom, this._emailFromPwd);
            this._smtpClient.Port = this._port;
            this._smtpClient.DeliveryMethod = SmtpDeliveryMethod.Network;
            this._smtpClient.EnableSsl = true;
            this._smtpClient.Timeout = 10000;
        }

        public virtual void SendMultipleEmail(List<Tuple<string, string>> emails, string subject, string body)
        {
            try
            {
                var mailMessage = new MailMessage();
                mailMessage.From = new MailAddress(this._emailFrom);
                foreach (var email in emails)
                    mailMessage.To.Add(email.Item1);

                mailMessage.Subject = subject;
                mailMessage.Body = body;
                mailMessage.IsBodyHtml = true;

                mailMessage.BodyEncoding = UTF8Encoding.UTF8;
                mailMessage.DeliveryNotificationOptions = DeliveryNotificationOptions.OnFailure;

                this._smtpClient.Send(mailMessage);
            }
            catch (Exception ex)
            {
                //throw ex;
            }
        }

        public virtual void SendSingleEmail(string email, string name, string subject, string body)
        {
            var emails = new List<Tuple<string, string>>();
            emails.Add(new Tuple<string, string>(email, name));
            this.SendMultipleEmail(emails, subject, body);
        }

        public void SendSingleEmail(string email, string subject, string body) => SendSingleEmail(email, string.Empty, subject, body);

        public void SendMultipleEmail(List<string> emails, string subject, string body) => this.SendMultipleEmail(emails.Select(x => new Tuple<string, string>(x, string.Empty)).ToList(), subject, body);

        public void Dispose()
        {
            GC.SuppressFinalize(this);
        }


        public string PopulateEmailBody(String direccion,String contenido, String usuario, String titulo)
        {
            string body = string.Empty;
            using (StreamReader reader = new StreamReader(direccion))
            {
                body = reader.ReadToEnd();
            }
            body = body.Replace("{CONTENIDO}", contenido);
            body = body.Replace("{NOMBREUSUARIO}", usuario);
            body = body.Replace("{TITULO}", titulo);
            return body;
        }

        public void SendCalendar(string email, string name, string subject, string body, DateTime start, DateTime end, string Title)
        {
            MailMessage msg = new MailMessage();
            msg.From = new MailAddress(this._emailFrom);
            msg.To.Add(new MailAddress(email, name));
            
            msg.Subject = subject;
            msg.Body = body;
            msg.IsBodyHtml = true;
            
            msg.BodyEncoding = UTF8Encoding.UTF8;
            msg.DeliveryNotificationOptions = DeliveryNotificationOptions.OnFailure;


            StringBuilder str = new StringBuilder();
            str.AppendLine("BEGIN:VCALENDAR");
            str.AppendLine("PRODID:-//"+Title);
            str.AppendLine("VERSION:2.0");
            str.AppendLine("METHOD:REQUEST");
            str.AppendLine("BEGIN:VEVENT");
            str.AppendLine(string.Format("DTSTART:{0:yyyyMMddTHHmmssZ}", start.ToUniversalTime()));
            str.AppendLine(string.Format("DTSTAMP:{0:yyyyMMddTHHmmssZ}", DateTime.UtcNow));
            str.AppendLine(string.Format("DTEND:{0:yyyyMMddTHHmmssZ}", end.ToUniversalTime()));
            str.AppendLine("LOCATION: " + "AULA VIRTUAL PROMPERU");
            str.AppendLine(string.Format("UID:{0}", Guid.NewGuid()));
            str.AppendLine(string.Format("DESCRIPTION:{0}", msg.Body));
            str.AppendLine(string.Format("X-ALT-DESC;FMTTYPE=text/html:{0}", msg.Body));
            str.AppendLine(string.Format("SUMMARY:{0}", Title));
            str.AppendLine(string.Format("ORGANIZER:MAILTO:{0}", msg.From.Address));

            str.AppendLine(string.Format("ATTENDEE;CN=\"{0}\";RSVP=TRUE:mailto:{1}", msg.To[0].DisplayName, msg.To[0].Address));

            str.AppendLine("BEGIN:VALARM");
            str.AppendLine("TRIGGER:-PT15M");
            str.AppendLine("ACTION:DISPLAY");
            str.AppendLine("DESCRIPTION:Reminder");
            str.AppendLine("END:VALARM");
            str.AppendLine("END:VEVENT");
            str.AppendLine("END:VCALENDAR");

            byte[] byteArray = Encoding.ASCII.GetBytes(str.ToString());
            MemoryStream stream = new MemoryStream(byteArray);

            Attachment attach = new Attachment(stream, "calendar.ics");

            msg.Attachments.Add(attach);

            System.Net.Mime.ContentType contype = new System.Net.Mime.ContentType("text/calendar");
            contype.Parameters.Add("method", "REQUEST");
            //  contype.Parameters.Add("name", "Meeting.ics");
            AlternateView avCal = AlternateView.CreateAlternateViewFromString(str.ToString(), contype);
            msg.AlternateViews.Add(avCal);

            //Now sending a mail with attachment ICS file.                     

            this._smtpClient.Send(msg);
        }
    }
}