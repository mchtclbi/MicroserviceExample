namespace Neredekal.ProductAPI.Service.Interfaces
{
    public interface IRabbitMQSProducer
    {
        public void SendProductMessage(string message);
    }
}