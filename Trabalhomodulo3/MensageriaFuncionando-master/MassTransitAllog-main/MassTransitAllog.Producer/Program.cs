// dotnet tool install --global dotnet-ef
// dotnet ef migrations add InitialMigration
// dotnet ef database update

using MassTransit;
using MassTransitAllog.Producer.Context;
using Microsoft.EntityFrameworkCore;
using StackExchange.Redis;
using System.Security.Authentication;

var builder = WebApplication.CreateBuilder(args);

builder.Services.AddControllers();
builder.Services.AddEndpointsApiExplorer();
builder.Services.AddSwaggerGen();

builder.Services.AddDbContext<ClienteContext>(options => {
    options
        .UseNpgsql("Host=localhost;Database=trabalho;Username=postgres;Password=5678");
}
);

builder.Services.AddSingleton<IConnectionMultiplexer>(
    ConnectionMultiplexer.Connect(
        "redis-15714.c308.sa-east-1-1.ec2.cloud.redislabs.com:15714,password=LD0DLreYHYpmroobQ2FPTgdjfioonEPb"
    )
);
builder.Services.AddHttpClient();

// CloudMQP connection
builder.Services.AddMassTransit(busConfigurator =>
{
    busConfigurator.SetKebabCaseEndpointNameFormatter();
    busConfigurator.UsingRabbitMq((context, busFactoryConfigurator) =>
    {
        busFactoryConfigurator.Host("jackal-01.rmq.cloudamqp.com", 5671, "wtiodvyx", h =>
        {
            h.Username("wtiodvyx");
            h.Password("LS2TsMZKGUqjar0plsGDSqXtor4d4s6W");

            h.UseSsl(s =>
            {
                s.Protocol = SslProtocols.Tls12;
            });
        });
    });
});

var app = builder.Build();

if (app.Environment.IsDevelopment())
{
    app.UseSwagger();
    app.UseSwaggerUI();
}

app.UseHttpsRedirection();

app.UseAuthorization();

app.MapControllers();

app.Run();
