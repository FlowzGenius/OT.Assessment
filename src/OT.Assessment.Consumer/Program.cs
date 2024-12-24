using FluentValidation;
using MassTransit;
using Microsoft.Extensions.Configuration;
using OT.Assessment.Consumer;
using OT.Assessment.Core.Commands.ProcessWager;
using OT.Assessment.Infrastructure;

var host = Host.CreateDefaultBuilder(args)
    .ConfigureAppConfiguration(config =>
    {
        config.SetBasePath(Directory.GetCurrentDirectory())
            .AddJsonFile("appsettings.json")
            .Build();
    })
    .ConfigureServices((context, services) =>
    {
        //configure services
        var mqSetting = context.Configuration.GetSection("RabbitMqSettings");
        services.AddMassTransit(busConfigurator =>
        {
            busConfigurator.AddConsumer<WagerEventConsumer>();
            busConfigurator.UsingRabbitMq((context, cfg) =>
            {
                cfg.Host("localhost", "/", h =>
                {
                    h.Username(mqSetting["Username"]);
                    h.Password(mqSetting["Password"]);
                });
                cfg.ConfigureEndpoints(context);
            });
        });
        services.AddSingleton(TimeProvider.System);
        services.AddValidatorsFromAssemblyContaining<ProcessWagerCommandValidator>();
        services.AddMediatR(cfg => cfg.RegisterServicesFromAssembly(typeof(ProcessWagerCommand).Assembly));
        services.AddStorage(context.Configuration);
    })
    .Build();

var logger = host.Services.GetRequiredService<ILogger<Program>>();
logger.LogInformation("Application started {time:yyyy-MM-dd HH:mm:ss}", DateTime.Now);

await host.RunAsync();

logger.LogInformation("Application ended {time:yyyy-MM-dd HH:mm:ss}", DateTime.Now);