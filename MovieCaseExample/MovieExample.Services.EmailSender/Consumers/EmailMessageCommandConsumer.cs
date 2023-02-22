using MassTransit;
using MovieExample.Services.EmailSender.Messaging;
using MovieExample.Shared.Messages;

namespace MovieExample.Services.EmailSender.Consumers
{
    public class EmailMessageCommandConsumer : IConsumer<EmailMessageCommand>
    {
        private readonly IEmailSender _emailSender;

        public EmailMessageCommandConsumer(IEmailSender emailSender)
        {
            _emailSender = emailSender;
        }

        public async Task Consume(ConsumeContext<EmailMessageCommand> context)
        {
            await _emailSender.SendEmailAsync(context.Message.SenderEmail, 
                context.Message.ReceiverEmail, 
                context.Message.Subject,
                context.Message.Body);
        }
    }
}
