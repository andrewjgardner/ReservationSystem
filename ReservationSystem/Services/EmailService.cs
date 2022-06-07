using SendGrid;
using SendGrid.Helpers.Mail;
using System;
using System.Threading.Tasks;

namespace ReservationSystem.Services
{
    public class EmailService
    {
        private string _apiKey;

        public static void Main()
        {
            Execute().Wait();
        }

        static async Task Execute()
        {
            //TODO: Move API key to appsettings.json
            var apiKey = "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";
            //var apiKey = Environment.GetEnvironmentVariable("test-key");
            var client = new SendGridClient(apiKey);
            var from = new EmailAddress("conor.oneill9@studytafensw.edu.au", "Conor");
            var subject = "Sending with SendGrid is Fun";
            var to = new EmailAddress("conor.oneill9@studytafensw.edu.au", "Conor2");
            var plainTextContent = "and easy to do anywhere, even with C#";
            var htmlContent = "<strong>and easy to do anywhere, even with C#</strong>";
            var msg = MailHelper.CreateSingleEmail(from, to, subject, plainTextContent, htmlContent);
            var response = await client.SendEmailAsync(msg);
        }
    }
}