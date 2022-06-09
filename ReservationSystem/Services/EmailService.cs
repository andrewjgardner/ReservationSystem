using Microsoft.AspNetCore.Identity.UI.Services;
using Microsoft.Extensions.Options;
using SendGrid;
using SendGrid.Helpers.Mail;
using System;
using System.Threading.Tasks;

namespace ReservationSystem.Services
{
    public class EmailService : IEmailSender
    {
        public SendGridOptions Options { get; set; }


        public EmailService(IOptions<SendGridOptions> optionsAccessor)
        {
            Options = optionsAccessor.Value;
        }

        public async Task SendEmailAsync(string toEmail, string subject, string message)
        {
            //if (string.IsNullOrEmpty(Options.SendGridKey))
            //{
            //    throw new Exception("Null SendGridKey");
            //}
            //await ExecuteWithSendGrid(Options.SendGridKey, subject, message, toEmail);


        }

        public async Task ExecuteWithSendGrid(string apiKey, string subject, string message, string toEmail)
        {
            var client = new SendGridClient(apiKey);
            var msg = new SendGridMessage()
            {
                From = new EmailAddress("conor.oneill9@studytafensw.edu.au", "Password Recovery"),
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

        public async Task ExecuteWithOutlook(string subject, string message)
        {

        }
    }
}