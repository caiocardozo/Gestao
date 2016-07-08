
using System;
using System.Configuration;
using System.Net;
using System.Net.Mail;

namespace GestaoDDD.MVC.Util
{
    public class EnviaEmail
    {
        public bool EnviaEmailConfirmacao(string enviadoPara, string corpo, string assunto)
        {
            
            try
           {
               MailMessage message = new MailMessage();
               //seta as configurações do email
               message.Sender = new MailAddress(ConfigurationManager.AppSettings["ContaDeEmail"], "Equipe Agiliza");
               message.From = new MailAddress(ConfigurationManager.AppSettings["ContaDeEmail"], "Equipe Agiliza");
               message.To.Add(new MailAddress(enviadoPara, "Confirmar Email"));
               message.Subject = assunto;
               message.Body = corpo;
               message.IsBodyHtml = true;
               message.Priority = MailPriority.Normal;

               var smtpClient = new SmtpClient("smtp.gmail.com", Convert.ToInt32(587));
               var credentials = new NetworkCredential(ConfigurationManager.AppSettings["ContaDeEmail"],
                   ConfigurationManager.AppSettings["SenhaEmail"]);
               smtpClient.UseDefaultCredentials = false;
               smtpClient.Credentials = credentials;
               smtpClient.EnableSsl = true;
               smtpClient.Send(message);

                return true;
            }
            catch (Exception)
            {
                return false;
            }

        }
    }
}