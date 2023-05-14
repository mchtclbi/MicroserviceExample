using System.Text;
using RabbitMQ.Client;
using RabbitMQ.Client.Events;
using Neredekal.Application.Helper;
using Neredekal.ProductAPI.Consumer;

var factory = new ConnectionFactory { Uri = new Uri(ReadConfig.GetConnectionString("RabbitMq")) };
using var connection = factory.CreateConnection();
using var channel = connection.CreateModel();

const string queue = "product-report";

channel.QueueDeclare(queue: queue,
                     durable: false,
                     exclusive: false,
                     autoDelete: false,
                     arguments: null);

Console.WriteLine(" [*] Waiting for messages.");

var consumer = new EventingBasicConsumer(channel);
consumer.Received += (model, ea) =>
{
    var body = ea.Body.ToArray();
    var message = Encoding.UTF8.GetString(body);
    new ProductReport().SendReportProgress(message);
};
channel.BasicConsume(queue: queue,
                     autoAck: true,
                     consumer: consumer);

Console.WriteLine(" Press [enter] to exit.");
Console.ReadLine();