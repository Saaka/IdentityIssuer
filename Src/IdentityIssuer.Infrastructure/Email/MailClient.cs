using System.Threading.Tasks;
using IdentityIssuer.Application.Models;
using MailKit.Net.Smtp;
using MimeKit;

namespace IdentityIssuer.Infrastructure.Email
{
    public interface IMailClient
    {
        Task Send(MimeMessage message, EmailSettings emailSettings);
    }
    public class MailClient : IMailClient
    {
        public async Task Send(MimeMessage message, EmailSettings emailSettings)
        {
            using (var client = new SmtpClient())
            {
                await client.ConnectAsync(emailSettings.SmtpHost, emailSettings.SmtpPort);
                await client.AuthenticateAsync(emailSettings.UserName, emailSettings.Password);
                await client.SendAsync(message);
                await client.DisconnectAsync(true);
            }
        }
    }
}