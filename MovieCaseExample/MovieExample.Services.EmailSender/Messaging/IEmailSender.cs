namespace MovieExample.Services.EmailSender.Messaging
{
    public interface IEmailSender
    {
        Task SendEmailAsync(string senderEmail, string receiverEmail, string subject, string body);
    }
}
