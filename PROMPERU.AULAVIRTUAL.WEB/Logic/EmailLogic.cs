using SendGrid;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Net.Mail;
using System.Web;
using System.Web.Mvc;
using System.Configuration;
using PROMPERU.AULAVIRTUAL.WEB.Helpers;
using PROMPERU.AULAVIRTUAL.BL;
using PROMPERU.AULAVIRTUAL.WEB.Models;
//using System.Data.Entity;
using PROMPERU.AULAVIRTUAL.Helpers;

namespace PROMPERU.AULAVIRTUAL.WEB.Logic
{
    public class EmailLogic
    {
        UsuarioBL usuarioBL = new UsuarioBL();
        CursosBL cursosBL = new CursosBL();
        private String emailFrom;
        private String emailFromName;
        private String emailUserName;
        private String emailPassword;
        private NetworkCredential credentials;
        //private EvolEntities context;

        public EmailLogic()//EvolEntities context)
        {
            this.emailFrom = ConfigurationManager.AppSettings["SendGird.From"].ToString();
            this.emailFromName = ConfigurationManager.AppSettings["SendGird.FromName"].ToString();
            this.emailUserName = ConfigurationManager.AppSettings["SendGrid.UserName"].ToString();
            this.emailPassword = ConfigurationManager.AppSettings["SendGrid.Password"].ToString();
            this.credentials = new NetworkCredential(emailUserName, emailPassword);
            //this.context = context;
        }

        public void SendMailOnEnrollment(String asunto, String mensaje, Int32 usuarioId, Int32 CursoOnlineId)
        {
            try
            {
                var mailReceptores = usuarioBL.ObtenerUsuarioPorId(usuarioId).Email;     //context.Usuario.Find(usuarioId).Email;

                if (!ValidationHelpers.IsValidEmail(mailReceptores))
                    return;

                var templateReder = new TemplateRender();

                var CursoOnline = cursosBL.ObtenerCursoOnlinePorId(CursoOnlineId);    //context.CursoOnline.Find(CursoOnlineId);

                var myMessage = new SendGridMessage();

                myMessage.AddTo(mailReceptores);

                myMessage.From = new MailAddress(emailFrom, emailFromName);
                myMessage.Subject = asunto + " " + CursoOnline.Nombre;
                myMessage.Html = templateReder.Render("MailNotificacion", new { Descripcion = mensaje + CursoOnline.Nombre, Mensaje = mensaje + CursoOnline.Nombre });


                var transporWeb = new Web(credentials);
                if (transporWeb != null)
                    transporWeb.DeliverAsync(myMessage);

            }
            catch (Exception e)
            {
                Util.SaveErrorLog(e);
                throw;
            }
        }

        public void SendMailOnEnrollmentCargaUsuario(String asunto, String mensaje, Int32 usuarioId, Int32 CursoOnlineId)
        {
            try
            {
                var usuario = usuarioBL.ObtenerUsuarioPorId(usuarioId);
                var mailReceptores = usuario.Email;
                var templateReder = new TemplateRender();

                var CursoOnline = cursosBL.ObtenerCursoOnlinePorId(CursoOnlineId);  //context.CursoOnline.FirstOrDefault(x => x.CursoOnlineId == CursoOnlineId);

                var myMessage = new SendGridMessage();

                myMessage.AddTo(mailReceptores);

                myMessage.From = new MailAddress(emailFrom, emailFromName);
                myMessage.Subject = asunto;
                myMessage.Html = templateReder.Render("MailNotificacionCargaUsuario", new { Descripcion = mensaje, Mensaje = mensaje, Cuenta = usuario.Codigo, Password = usuario.Password, Curso = CursoOnline.Nombre });


                var transporWeb = new Web(credentials);
                if (transporWeb != null)
                    transporWeb.DeliverAsync(myMessage);

            }
            catch (Exception e)
            {
                Util.SaveErrorLog(e);
                throw;
            }
        }

        public void SendMailOnEvaluationSucces(String asunto, String mensaje, Int32 usuarioId, Int32 CursoOnlineId)
        {
            try
            {
                var usuario = usuarioBL.ObtenerUsuarioPorId(usuarioId);
                //var Supervisores = context.Usuario.Where(x => x.EmpresaId == Usuario.EmpresaId && x.Rol == ConstantHelpers.ROL.SUPERVISOR).ToList();

                var mailReceptores = usuario.Email;
                var templateReder = new TemplateRender();

                var CursoOnline = cursosBL.ObtenerCursoOnlinePorId(CursoOnlineId);  //context.CursoOnline.FirstOrDefault(x => x.CursoOnlineId == CursoOnlineId);

                var myMessage = new SendGridMessage();

                myMessage.AddTo(mailReceptores);

                //Supervisores.ForEach(x => myMessage.AddTo(x.Email)); 

                myMessage.From = new MailAddress(emailFrom, emailFromName);
                myMessage.Subject = asunto + CursoOnline.Nombre;
                myMessage.Html = templateReder.Render("MailNotificacion", new { Descripcion = mensaje, Mensaje = mensaje });


                var transporWeb = new Web(credentials);
                if (transporWeb != null)
                    transporWeb.DeliverAsync(myMessage);

            }
            catch (Exception ex)
            {

                throw;
            }
        }

        public void SendMailContact(string mensaje, string correo, string nombre)
        {
            var myMessage = new SendGridMessage();
            myMessage.AddTo("daxxad@gmail.com");
            myMessage.From = new MailAddress(emailFrom, emailFromName);
            myMessage.Subject = "Formulario de contacto";
            myMessage.Html = mensaje + " " + Environment.NewLine + Environment.NewLine + nombre + "(" + correo + ")";

            var transporWeb = new Web(credentials);
            if (transporWeb != null)
                transporWeb.DeliverAsync(myMessage);
        }

        public void SendMailGroup(int mensaje, string asunto, int? GrupoId)
        {
            //TODO, revisar el store

            //var Grupo = context.Usuario.SqlQuery("USP_SendMensajeGrupo_LIS {0}", GrupoId).ToList();
            //var Notificaciones = context.Notificaciones.FirstOrDefault(x => x.NotificacionId == mensaje);

            //var templateReder = new TemplateRender();

            //var myMessage = new SendGridMessage();
            //myMessage.AddTo("rguillermo@promperuext.pe");

            //foreach (var item in Grupo)
            //{
            //    myMessage.AddBcc(item.Email);
            //}

            //myMessage.From = new MailAddress(emailFrom, emailFromName);
            //myMessage.Subject = asunto;
            //myMessage.Html = templateReder.Render("MailNotificacion", new { Descripcion = Notificaciones.Contenido, Mensaje = Notificaciones.Contenido });


            //var transporWeb = new Web(credentials);
            //if (transporWeb != null)
            //    transporWeb.DeliverAsync(myMessage);
        }

    }
}