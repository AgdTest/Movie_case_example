using Hangfire;
using MassTransit;
using MovieExample.Jobs;
using MovieExample.Jobs.Jobs;

var builder = WebApplication.CreateBuilder(args);

// Add services to the container.

builder.ConfigureHangfireService();

builder.Services.AddMassTransit(x =>
{
    x.UsingRabbitMq((context, cfg) =>
    {
        cfg.Host(builder.Configuration["RabbitMQUrl"], "/", host =>
        {
            host.Username("guest");
            host.Password("guest");
        });
    });
});

builder.Services.AddMassTransitHostedService();

builder.Services.AddTransient<IMovieCollectorJob, MovieCollectorJob>();

var app = builder.Build();

// Configure the HTTP request pipeline.

app.UseHangfireDashboard();

// Manually start the job at the start.
BackgroundJob.Enqueue<MovieCollectorJob>(x => x.CollectMovies());

app.ConfigureJobs();

app.Run();