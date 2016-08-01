
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


        public bool EnviaEmailCaio(string enviadoPara, string corpo, string assunto)
        {

            if (string.IsNullOrEmpty(enviadoPara));
            string email = "suporte@fai.ufscar.br";
            SmtpClient client = new SmtpClient();
            client.Host = "faiadm1007.fai2008.ufscar.br";
            client.EnableSsl = false;
            client.Credentials = new NetworkCredential("suporte@fai.ufscar.br", "Sup0rte@2013");
            client.Port = 25;
            MailMessage message = new MailMessage();
            message.Sender = new MailAddress(email, "Suporte FAI UFSCar");
            message.From = new MailAddress(email, "Suporte FAI UFSCar");
            message.To.Add(new MailAddress(enviadoPara, "Coordenador"));
            message.Bcc.Add(new MailAddress("caio.cardozo@fai.ufscar.br"));
           message.Subject = assunto;
            message.Body = corpo;
            message.IsBodyHtml = true;
            message.Priority = MailPriority.High;
          try
            {
                client.Send(message);
                return true;
            }
            catch (Exception ex)
            {
                //throw new Exception(ex.Message);
                return false;
            }

        }
    }
}