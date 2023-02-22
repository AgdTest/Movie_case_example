using MassTransit;
using MovieExample.Services.EmailSender.Consumers;
using MovieExample.Services.EmailSender.Messaging;

var builder = WebApplication.CreateBuilder(args);

// Add services to the container.

builder.Services.AddMassTransit(x =>
{
    x.AddConsumer<EmailMessageCommandConsumer>();
    x.UsingRabbitMq((context, cfg) =>
    {
        cfg.Host(builder.Configuration["RabbitMQUrl"], "/", host =>
        {
            host.Username("guest");
            host.Password("guest");
        });

        cfg.ReceiveEndpoint("send-email-service", e =>
        {
            e.ConfigureConsumer<EmailMessageCommandConsumer>(context);
        });
    });
});

builder.Services.AddMassTransitHostedService();

builder.Services.AddScoped<IEmailSender, EmailSender>();

var app = builder.Build();

// Configure the HTTP request pipeline.

app.Run();