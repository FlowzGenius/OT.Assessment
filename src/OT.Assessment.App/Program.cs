using System.Reflection;
using FluentValidation;
using MassTransit;
using OT.Assessment.Core.Commands.ProcessWager;
using OT.Assessment.Infrastructure;
using OT.Assessment.Messaging.Models;

var builder = WebApplication.CreateBuilder(args);
builder.Services.AddControllers();

// Learn more about configuring Swagger/OpenAPI at https://aka.ms/aspnetcore/swashbuckl
builder.Services.AddEndpointsApiExplorer();
builder.Services.AddSwaggerGen();
builder.Services.AddSwaggerGen(options =>
{
    var xmlFilename = $"{Assembly.GetExecutingAssembly().GetName().Name}.xml";
    options.IncludeXmlComments(Path.Combine(AppContext.BaseDirectory, xmlFilename));
});
builder.Services.AddValidatorsFromAssemblyContaining<ProcessWagerCommandValidator>();
builder.Services.AddMediatR(cfg => cfg.RegisterServicesFromAssembly(typeof(ProcessWagerCommand).Assembly));
builder.Services.AddStorage(builder.Configuration);
builder.Services.AddSingleton(TimeProvider.System);
builder.Services.AddMassTransit(x =>
{
    var mqSetting = builder.Configuration.GetSection("RabbitMqSettings");
    x.AddRequestClient<CasinoWagerEvent>();
    x.UsingRabbitMq((context, cfg) =>
    {
        cfg.Host("localhost", "/", h =>
        {
            h.Username(mqSetting["Username"]);
            h.Password(mqSetting["Password"]);
        });
    });
});

var app = builder.Build();

// Configure the HTTP request pipeline.
if (app.Environment.IsDevelopment())
{
    app.UseSwagger();
    app.UseSwaggerUI(opts =>
    {
        opts.EnableTryItOutByDefault();
        opts.DocumentTitle = "OT Assessment App";
        opts.DisplayRequestDuration();
    });
}

app.UseHttpsRedirection();

app.UseAuthorization();

app.MapControllers();

app.Run();
