using Microsoft.AspNet.Identity;
using System.Threading.Tasks;
using Twilio;

namespace GestaoDDD.Infra.Identity.Configuration
{
    public class SmsService : IIdentityMessageService
    {
        public Task SendAsync(IdentityMessage message)
        {
            // Utilizando TWILIO como SMS Provider.
            // https://www.twilio.com/docs/quickstart/csharp/sms/sending-via-rest

            const string accountSid = "ACacb7988131e2bfaf4c0d0e346979b937";
            const string authToken = "f67e112b89b69f93ff67675c653fc340";

            var client = new TwilioRestClient(accountSid, authToken);

            client.SendMessage("6291501668", message.Destination, message.Body);

            return Task.FromResult(0);
        }
    }
}
