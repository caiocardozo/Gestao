using Microsoft.AspNet.Identity;
using RestSharp.Extensions.MonoHttp;
using System;
using System.Configuration;
using System.Net;
using System.Net.Mail;
using System.Net.Mime;
using System.Threading.Tasks;

namespace GestaoDDD.Infra.Identity.Configuration
{
    public class EmailService : IIdentityMessageService
    {
        public Task SendAsync(IdentityMessage message)
        {
            // Habilitar o envio de e-mail
            if (false)
            {
                var text = HttpUtility.HtmlEncode(message.Body);

                var msg = new MailMessage { From = new MailAddress("admin@portal.com.br", "Equipe Agiliza") };

                msg.To.Add(new MailAddress(message.Destination));
                msg.Subject = message.Subject;
                msg.AlternateViews.Add(AlternateView.CreateAlternateViewFromString(text, null, MediaTypeNames.Text.Plain));
                msg.AlternateViews.Add(AlternateView.CreateAlternateViewFromString(text, null, MediaTypeNames.Text.Html));

                var smtpClient = new SmtpClient("smtp.provedor.com", Convert.ToInt32(587));
                var credentials = new NetworkCredential(ConfigurationManager.AppSettings["ContaDeEmail"],
                    ConfigurationManager.AppSettings["SenhaEmail"]);
                smtpClient.Credentials = credentials;
                smtpClient.EnableSsl = true;
                smtpClient.Send(msg);
            }

            return Task.FromResult(0);
        }
    }
}
