using MailKit;
using MailKit.Net.Smtp;
using MailKit.Security;
using Microsoft.AspNetCore.Identity.UI.Services;
using MimeKit;
using System.Net.Security;
using IConfiguration = Microsoft.Extensions.Configuration.IConfiguration;

namespace GiffyglyphMonsterMakerV3.Utility
{
    public class MailService : IEmailSender
    {
        private readonly IConfiguration _configuration;

        public MailService(IConfiguration configuration)
        {
            _configuration = configuration;
        }

        public async Task SendEmailAsync(string email, string subject, string htmlMessage)
        {
            var mailMessage = new MimeMessage();

            mailMessage.From.Add(new MailboxAddress("admin@Skyl3lazer.com", "admin@Skyl3lazer.com"));
            mailMessage.To.Add(new MailboxAddress(email, email));
            mailMessage.Subject = subject;
            mailMessage.Body = new TextPart("html")
            {
                Text = htmlMessage
            };

            using (var smtpClient = new MailKit.Net.Smtp.SmtpClient())
            {
                smtpClient.ServerCertificateValidationCallback = (s, c, h, e) => true;
                //smtpClient.ServerCertificateValidationCallback = (s, c, h, e) => e == SslPolicyErrors.None;
                var SmtpHost = Environment.GetEnvironmentVariable("Smtp__Host") ?? _configuration["Smtp:Host"];
                var SmtpPort = Environment.GetEnvironmentVariable("Smtp__Port") ?? _configuration["Smtp:Port"];
                var SmtpAccount = Environment.GetEnvironmentVariable("Smtp__Account") ?? _configuration["Smtp:Account"];
                var SmtpPassword = Environment.GetEnvironmentVariable("Smtp__Password") ?? _configuration["Smtp:Password"];
                await smtpClient.ConnectAsync(SmtpHost, port: int.Parse(SmtpPort), SecureSocketOptions.StartTls);
                await smtpClient.AuthenticateAsync(SmtpAccount, SmtpPassword);
                await smtpClient.SendAsync(mailMessage);
                await smtpClient.DisconnectAsync(true);
            }
        }
    }
}

