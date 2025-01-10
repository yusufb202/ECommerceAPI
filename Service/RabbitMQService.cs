using Core.RabbitMQ;
using Microsoft.Extensions.Options;
using RabbitMQ.Client;
using System.Text;


namespace Service
{
    public class RabbitMQService : IRabbitMQService
    {
        private readonly RabbitMQSettings _rabbitMQSettings;

        public RabbitMQService(IOptions<RabbitMQSettings> rabbitMQSettings)
        {
            _rabbitMQSettings = rabbitMQSettings.Value;
        }

        public void PublishMessage(string queueName, string message)
        {
            var factory = new ConnectionFactory
            {
                HostName = _rabbitMQSettings.Host,
                UserName = _rabbitMQSettings.UserName,
                Password = _rabbitMQSettings.Password,
                VirtualHost = _rabbitMQSettings.VirtualHost,
                Port = _rabbitMQSettings.Port,
            };

            using var connection = factory.CreateConnection();
            using var channel = connection.CreateModel();

            channel.QueueDeclare(queue: queueName,
                                 durable: true,
                                 exclusive: false,
                                 autoDelete: false,
                                 arguments: null);

            var body = Encoding.UTF8.GetBytes(message);

            channel.BasicPublish(exchange: "",
                                 routingKey: queueName,
                                 basicProperties: null,
                                 body: body);

            Console.WriteLine($"[x] Sent: {message}");
        }
    }

}
