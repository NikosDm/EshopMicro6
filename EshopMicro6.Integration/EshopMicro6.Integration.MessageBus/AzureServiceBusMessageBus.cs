using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Azure.Messaging.ServiceBus;
using Newtonsoft.Json;

namespace EshopMicro6.Integration.MessageBus
{
    public class AzureServiceBusMessageBus : IMessageBus
    {
        private readonly string connectionString = "Endpoint=sb://eshomicro6.servicebus.windows.net/;SharedAccessKeyName=RootManageSharedAccessKey;SharedAccessKey=nnOC+4nEdmwPOWgZos3Up9Th9Lk/K/qJgjTKHdMBy2o=";

        public async Task PublishMessage(BaseMessage baseMessage, string TopicName)
        {
            await using var client = new ServiceBusClient(connectionString);

            ServiceBusSender sender = client.CreateSender(TopicName);

            var JsonMessage = JsonConvert.SerializeObject(baseMessage);
            ServiceBusMessage finalMessage = new ServiceBusMessage(Encoding.UTF8.GetBytes(JsonMessage)) {
                CorrelationId = Guid.NewGuid().ToString()
            };

            await sender.SendMessageAsync(finalMessage);

            await client.DisposeAsync();
        }
    }
}