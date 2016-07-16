
using System;
using System.Collections.Generic;
using System.Configuration;
using System.Net;
using System.Net.Mail;

namespace GestaoDDD.MVC.Util
{
    public class EnviaEmail
    {
        public KeyValuePair<bool, string> EnviaEmailConfirmacao(string enviadoPara, string corpo, string assunto)
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

               var smtpClient = new SmtpClient(ConfigurationManager.AppSettings["SmtpCliente"], Int32.Parse(ConfigurationManager.AppSettings["SmtpPorta"]));
               var credentials = new NetworkCredential(ConfigurationManager.AppSettings["ContaDeEmail"],
                   ConfigurationManager.AppSettings["SenhaEmail"]);
               smtpClient.UseDefaultCredentials = false;
               smtpClient.Credentials = credentials;
               smtpClient.Port = Int32.Parse(ConfigurationManager.AppSettings["SmtpPorta"]);
               smtpClient.EnableSsl = true;
               smtpClient.Send(message);

                return new KeyValuePair<bool, string>(true, "OK"); 
            }
            catch (Exception e)
            {
                return new KeyValuePair<bool, string>(false, e.Message);
            }

        }
    }
}