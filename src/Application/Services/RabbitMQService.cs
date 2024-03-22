using Application.Interfaces;
using Application.Models;
using RabbitMQ.Client;
using System.Text;

namespace Application.Services
{
    public class RabbitMQService : IRabbitMQService
    {
        private readonly ConnectionFactory connectionFactory;
        private readonly IModel channel;

        public RabbitMQService(Configuration configuration)
        {
            this.connectionFactory = new ConnectionFactory
            {
                HostName = configuration.RabbitMQ.HostName,
                Port = configuration.RabbitMQ.Port,
                UserName = configuration.RabbitMQ.UserName,
                Password = configuration.RabbitMQ.Password,
            };
            var connection = connectionFactory.CreateConnection();
            channel = connection.CreateModel();
        }
        public void Publish(string queueName, string data)
        {
            channel.QueueDeclare(queueName, false, false, false);
            var body = Encoding.UTF8.GetBytes(data);
            channel.BasicPublish(exchange: "", routingKey: queueName, basicProperties: null, body: body);
        }

    }
}
