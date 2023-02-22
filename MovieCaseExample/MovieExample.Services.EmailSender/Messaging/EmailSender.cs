using FluentEmail.Core;
using FluentEmail.Razor;
using FluentEmail.Smtp;
using System.Net.Mail;
using System.Text;

namespace MovieExample.Services.EmailSender.Messaging
{
    public class EmailSender : IEmailSender
    {
        public async Task SendEmailAsync(string senderEmail, string receiverEmail, string subject, string body)
        {
            var sender = new SmtpSender(() => new SmtpClient("localhost")
            {
                EnableSsl = false,
                DeliveryMethod = SmtpDeliveryMethod.Network,
                Port = 25
            });

            StringBuilder template = new StringBuilder();
            template.AppendLine("MovieCaseExample servis mesajı,");
            template.AppendLine($"<p>{body}</p>");
            template.AppendLine(" - MovieCaseExample");

            Email.DefaultSender = sender;
            Email.DefaultRenderer = new RazorRenderer();

            await Email
                .From(senderEmail)
                .To(receiverEmail)
                .Subject(subject)
                .Body(template.ToString())
                .SendAsync();
        }
    }
}
