
using System;
using System.Collections.Generic;
using System.Configuration;
using System.Net;
using System.Net.Mail;
using System.Text;

namespace GestaoDDD.MVC.Util
{
    public class EnviaEmail
    {
        public KeyValuePair<bool, string> EnviaEmailConfirmacao(string enviadoPara, string corpo, string assunto)
        {
            
            try
            {

                var message = new MailMessage();
                //seta as configurações do email
                //message.Sender = new MailAddress(ConfigurationManager.AppSettings["ContaDeEmail"], "Equipe Agiliza");
                message.Sender = new MailAddress("agilizaorcamentos@gmail.com", "Equipe Agiliza");
                message.From = new MailAddress("agilizaorcamentos@gmail.com", "Equipe Agiliza");
                message.To.Add(new MailAddress(enviadoPara, "Confirmar Email"));
                message.BodyEncoding = Encoding.UTF8;
                message.BodyEncoding = Encoding.GetEncoding("ISO-8859-1");
                message.Subject = assunto;
                message.Body = corpo;
                message.IsBodyHtml = true;
                message.Priority = MailPriority.High;

                var smtpClient = new SmtpClient("smtp.gmail.com", 587);
                var credentials = new NetworkCredential("agilizaorcamentos@gmail.com", "agiliza@2016_");
                smtpClient.DeliveryMethod = SmtpDeliveryMethod.Network;
                smtpClient.UseDefaultCredentials = false;
                smtpClient.Credentials = credentials;
                smtpClient.EnableSsl = true;
                smtpClient.Send(message);

                return new KeyValuePair<bool, string>(true, "OK"); 
            }
            catch (Exception e)
            {
                return new KeyValuePair<bool, string>(false, e.Message + e.InnerException);
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
