using EshopMicro6.Integration.MessageBus;
using EshopMicro6.Payments.PaymentProcessor;
using EshopMicro6.Services.PaymentApi.Extensions;
using EshopMicro6.Services.PaymentApi.Interfaces;
using EshopMicro6.Services.PaymentApi.Messaging;

var builder = WebApplication.CreateBuilder(args);

// Add services to the container.
builder.Services.AddSingleton<IProcessPayment, ProcessPayment>();

builder.Services.AddSingleton<IAzureServiceBusConsumer, AzureServiceBusConsumer>();
builder.Services.AddSingleton<IMessageBus, AzureServiceBusMessageBus>();

builder.Services.AddControllers();
// Learn more about configuring Swagger/OpenAPI at https://aka.ms/aspnetcore/swashbuckle
builder.Services.AddEndpointsApiExplorer();
builder.Services.AddSwaggerGen();

var app = builder.Build();

// Configure the HTTP request pipeline.
if (app.Environment.IsDevelopment())
{
    app.UseSwagger();
    app.UseSwaggerUI();
}

app.UseHttpsRedirection();

app.UseAuthorization();

app.MapControllers();

app.UseAzureServiceBusConsumer();

app.Run();
