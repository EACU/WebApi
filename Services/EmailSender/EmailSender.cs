using AutoMapper.Configuration;
using EACA_API.Models.Email;
using MailKit.Net.Smtp;
using Microsoft.Extensions.Options;
using MimeKit;
using System.Threading.Tasks;

namespace EACA_API.Services.EmailSender
{
    public class EmailSender : IEmailSender
    {
        private readonly EmailOptions _emailOptions;

        public EmailSender(IOptions<EmailOptions> jwtOptions)
        {
            _emailOptions = jwtOptions.Value;
        }

        public async Task SendEmailAsync(string email, string subject, string message)
        {
            var emailMessage = new MimeMessage();

            emailMessage.From.Add(new MailboxAddress("Портал ЕАСИ", _emailOptions.Login));
            emailMessage.To.Add(new MailboxAddress("", email));
            emailMessage.Subject = subject;
            emailMessage.Body = new TextPart(MimeKit.Text.TextFormat.Html)
            {
                Text = message
            };

            using (var client = new SmtpClient())
            {
                await client.ConnectAsync(_emailOptions.Host, _emailOptions.Port, _emailOptions.SSL);
                await client.AuthenticateAsync(_emailOptions.Login, _emailOptions.Password);
                await client.SendAsync(emailMessage);

                await client.DisconnectAsync(true);
            }
        }
    }
}
