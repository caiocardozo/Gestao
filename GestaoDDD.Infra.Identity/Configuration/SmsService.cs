using Microsoft.AspNet.Identity;
using System.Configuration;
using System.Threading.Tasks;
using Twilio;

namespace GestaoDDD.Infra.Identity.Configuration
{
    public class SmsService : IIdentityMessageService
    {
        public Task SendAsync(IdentityMessage message)
        {
            if (ConfigurationManager.AppSettings["Internet"] == "true") 
            {
                const string accountSid = "ACacb7988131e2bfaf4c0d0e346979b937";
                const string authToken = "f67e112b89b69f93ff67675c653fc340";

                var client = new TwilioRestClient(accountSid, authToken);

               var returnMessage = client.SendMessage("+556291501668", message.Destination, message.Body);
            }

            return Task.FromResult(0);
        }
    }
}
