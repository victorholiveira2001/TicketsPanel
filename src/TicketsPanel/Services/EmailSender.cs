using Microsoft.AspNetCore.Identity.UI.Services;
using System.Net;
using System.Net.Mail;

namespace TicketsPanel.Services
{
    public class EmailSender (EmailSettings settings, IConfiguration configuration): IEmailSender
    {
        private readonly EmailSettings _settings = settings;
        private readonly IConfiguration _configuration = configuration;
        public Task SendEmailAsync(string email, string subject, string htmlMessage)
        {
            string host = _configuration.GetValue<string>("Smtp:Server");
            int port = _configuration.GetValue<int>("Smtp:Port");
            string fromAddress = _configuration.GetValue<string>("Smtp:FromAddress");
            string userName = _configuration.GetValue<string>("Smtp:UserName");
            string password = _configuration.GetValue<string>("Smtp:Password");

            var smtp = new SmtpClient(host, port)
            {
                Credentials = new NetworkCredential(userName, password),
                EnableSsl = true
            };

            var mailMessage = new MailMessage()
            {
                From = new MailAddress(fromAddress, "TicketsPanel"),
                Subject = subject,
                Body = htmlMessage,
                IsBodyHtml = true
            };

            mailMessage.To.Add(email);
            smtp.Send(mailMessage);

            return Task.CompletedTask;

        }
    }
}
