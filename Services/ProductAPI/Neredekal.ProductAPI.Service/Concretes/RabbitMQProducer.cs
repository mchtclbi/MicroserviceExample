using System.Text;
using RabbitMQ.Client;
using Neredekal.ProductAPI.Service.Interfaces;
using Neredekal.Application.Helper;

namespace Neredekal.ProductAPI.Service.Concretes
{
    public class RabbitMQProducer : IRabbitMQSProducer
    {
        public void SendProductMessage(string message)
        {
            var factory = new ConnectionFactory { Uri = new Uri(ReadConfig.GetConnectionString("RabbitMq")) };
            using var connection = factory.CreateConnection();
            using var channel = connection.CreateModel();

            channel.QueueDeclare(queue: "product-report",
                                 durable: false,
                                 exclusive: false,
                                 autoDelete: false,
                                 arguments: null);

            var body = Encoding.UTF8.GetBytes(message);

            channel.BasicPublish(exchange: string.Empty,
                                 routingKey: "product-report",
                                 basicProperties: null,
                                 body: body);
        }
    }
}