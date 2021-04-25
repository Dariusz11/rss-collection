using SendGrid;
using SendGrid.Helpers.Mail;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace RssCollection.Services
{
    public class Mailing : IMailing
    {
        public bool Sending(string Receiver, string Topic, string Description)
        {
            var apiKey = "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";
            var client = new SendGridClient(apiKey);
            var from = new EmailAddress("gandarianna@gmail.com", "DL");
            //var subject = "First topic";
            var to = new EmailAddress(Receiver, "DL kolejny");
            //var plainTextContent = "and easy to do anywhere, even with C#";
            //var htmlContent = "<strong>Topic</strong>";
            var msg = MailHelper.CreateSingleEmail(from, to, Topic, null, Description);
            var response = client.SendEmailAsync(msg).Result;
            return true;
        }
    }
}
