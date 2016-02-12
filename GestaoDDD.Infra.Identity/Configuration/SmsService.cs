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
                const string accountSid = "AC118c7ef4f8d25ea1cf3144b5d5b55900";
                const string authToken = "9e938a4dc40b851ecff0b1c807636aa3";

                var client = new TwilioRestClient(accountSid, authToken);

                var returnMessage = client.SendMessage("+15005550000", message.Destination, message.Body);
            }

            return Task.FromResult(0);
        }
    }
}
