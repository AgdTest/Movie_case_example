using FluentValidation.AspNetCore;
using JwtAuthenticationManager;
using MassTransit;
using Microsoft.Extensions.Options;
using Microsoft.OpenApi.Models;
using MovieExample.Services.Movie.Consumers;
using MovieExample.Services.Movie.Filters;
using MovieExample.Services.Movie.Mapping;
using MovieExample.Services.Movie.Services;
using MovieExample.Services.Movie.Settings;
using MovieExample.Services.Movie.Validators;

var builder = WebApplication.CreateBuilder(args);

// Add services to the container.

builder.Services.AddMassTransit(x =>
{
    x.AddConsumer<CreateMovieMessageCommandConsumer>();
    x.UsingRabbitMq((context, cfg) =>
    {
        cfg.Host(builder.Configuration["RabbitMQUrl"], "/", host =>
        {
            host.Username("guest");
            host.Password("guest");
        });

        cfg.ReceiveEndpoint("create-movie-service", e =>
        {
            e.ConfigureConsumer<CreateMovieMessageCommandConsumer>(context);
        });
    });
});

builder.Services.AddMassTransitHostedService();

builder.Services.AddScoped<IMovieService, MovieService>();

builder.Services.AddAutoMapper(typeof(MapProfile));
builder.Services.AddControllers(options => options.Filters.Add(new ValidateFilterAttribute())).AddFluentValidation(x => x.RegisterValidatorsFromAssemblyContaining<ReviewDtoValidator>());

builder.Services.AddCustomJwtAuthentication();

builder.Services.Configure<DatabaseSettings>(builder.Configuration.GetSection("DatabaseSettings"));

builder.Services.AddSingleton<IDatabaseSettings>(sp =>
{
    return sp.GetRequiredService<IOptions<DatabaseSettings>>().Value;
});

builder.Services.AddSwaggerGen(c =>
{
    c.SwaggerDoc("v1", new OpenApiInfo { Title = "MovieExample.Services.Movie", Version = "v1" });
    c.AddSecurityDefinition("Bearer", new OpenApiSecurityScheme
    {
        Description = "JWT Authorization header using the Bearer scheme.",
        Name = "Authorization",
        In = ParameterLocation.Header,
        Type = SecuritySchemeType.ApiKey,
        Scheme = "Bearer"
    });
    c.AddSecurityRequirement(new OpenApiSecurityRequirement
    {
        {
            new OpenApiSecurityScheme
            {
                Reference = new OpenApiReference
                {
                    Type = ReferenceType.SecurityScheme,
                    Id = "Bearer"
                }
            },
            new List<string>()
        }
    });
});

var app = builder.Build();

// Configure the HTTP request pipeline.

if (app.Environment.IsDevelopment())
{
    app.UseSwagger();
    app.UseSwaggerUI(c => c.SwaggerEndpoint("/swagger/v1/swagger.json", "MovieExample.Services.Movie v1"));
}

app.UseRouting();

app.UseAuthentication();
app.UseAuthorization();

app.UseEndpoints(endpoints =>
{
    endpoints.MapControllers();
});

app.Run();