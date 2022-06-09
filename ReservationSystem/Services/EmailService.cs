using SendGrid;
using SendGrid.Helpers.Mail;
using System;
using System.Threading.Tasks;

namespace ReservationSystem.Services
{
    public class EmailService
    {
        public EmailSenderOptions Options { get; }

        public async Task SendEmailAsync(string toEmail, string subject, string message)
        {
            if (string.IsNullOrEmpty(Options.SendGridKey))
            {
                throw new Exception("Null SendGridKey");
            }
            await Execute(Options.SendGridKey, subject, message, toEmail);
        }

        public async Task Execute(string apiKey, string subject, string message, string toEmail)
        {
            var client = new SendGridClient(apiKey);
            var msg = new SendGridMessage()
            {
                From = new EmailAddress("Joe@contoso.com", "Password Recovery"),
                Subject = subject,
                PlainTextContent = message,
                HtmlContent = message
            };
            msg.AddTo(new EmailAddress(toEmail));

            // Disable click tracking.
            // See https://sendgrid.com/docs/User_Guide/Settings/tracking.html
            msg.SetClickTracking(false, false);
            var response = await client.SendEmailAsync(msg);
        }

        static async Task OldExecute()
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