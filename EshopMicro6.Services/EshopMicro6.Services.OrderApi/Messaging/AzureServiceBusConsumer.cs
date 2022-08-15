using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Azure.Messaging.ServiceBus;
using EshopMicro6.Services.OrderApi.Entities;
using EshopMicro6.Services.OrderApi.Interfaces;
using EshopMicro6.Services.OrderApi.Messages;
using EshopMicro6.Services.OrderApi.Repositories;
using Microsoft.Extensions.Configuration;
using Newtonsoft.Json;

namespace EshopMicro6.Services.OrderApi.Messaging
{
    public class AzureServiceBusConsumer : IAzureServiceBusConsumer
    {
        private readonly string serviceBusConnectionString;
        private readonly string subscriptionCheckout;
        private readonly string checkoutMessageTopic;
        private readonly OrderRepository _orderRepository;
        private readonly IConfiguration _configuration;

        private ServiceBusProcessor checkoutProcessor;

        public AzureServiceBusConsumer(OrderRepository orderRepository, IConfiguration configuration)
        {
            _orderRepository = orderRepository;
            _configuration = configuration;

            serviceBusConnectionString = _configuration.GetValue<string>("ServiceBusConnectionString");
            subscriptionCheckout = _configuration.GetValue<string>("SubscriptionCheckout");
            checkoutMessageTopic = _configuration.GetValue<string>("CheckoutMessageTopic");

            var client = new ServiceBusClient(serviceBusConnectionString);

            checkoutProcessor = client.CreateProcessor(checkoutMessageTopic, subscriptionCheckout);
        }

        public async Task Start() 
        {
            checkoutProcessor.ProcessMessageAsync += OnCheckoutMessageReceived;
            checkoutProcessor.ProcessErrorAsync += ErrorHandler;
            await checkoutProcessor.StartProcessingAsync();
        }

        public async Task Stop() 
        {
            await checkoutProcessor.StopProcessingAsync();
            await checkoutProcessor.DisposeAsync();
        }

        private Task ErrorHandler(ProcessErrorEventArgs args)
        {
            Console.WriteLine(args.Exception.ToString());
            return Task.CompletedTask;
        }

        private async Task OnCheckoutMessageReceived(ProcessMessageEventArgs args)
        {
            var message = args.Message;

            var body = Encoding.UTF8.GetString(message.Body);

            CheckoutHeaderDTO checkoutHeaderDTO = JsonConvert.DeserializeObject<CheckoutHeaderDTO>(body);

            OrderHeader orderHeader = new()
            {
                UserID = checkoutHeaderDTO.UserID,
                FirstName = checkoutHeaderDTO.FirstName,
                LastName = checkoutHeaderDTO.LastName,
                OrderDetails = new List<OrderDetails>(),
                CardNumber = checkoutHeaderDTO.CardNumber,
                CouponCode = checkoutHeaderDTO.CouponCode,
                CVV = checkoutHeaderDTO.CVV,
                DiscountTotal = checkoutHeaderDTO.DiscountTotal,
                Email = checkoutHeaderDTO.Email,
                ExpiryMonthYear = checkoutHeaderDTO.ExpiryMonthYear,
                OrderTime = DateTime.Now,
                OrderTotal = checkoutHeaderDTO.OrderTotal,
                PaymentStatus = false,
                Phone = checkoutHeaderDTO.Phone,
                PickupDateTime = checkoutHeaderDTO.PickupDateTime
            };

            foreach(var detailList in checkoutHeaderDTO.cartDetails)
            {
                OrderDetails orderDetails = new()
                {
                    ProductId = detailList.ProductId,
                    ProductName = detailList.Product.Name,
                    Price = detailList.Product.Price,
                    Count = detailList.Count
                };
                orderHeader.CartTotalItems += detailList.Count;
                orderHeader.OrderDetails.Add(orderDetails);
            }

            await _orderRepository.AddOrder(orderHeader);
        }
    }
}