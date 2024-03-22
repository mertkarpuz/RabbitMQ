using Application.Models;
using RabbitMQ.Client.Events;
using RabbitMQ.Client;
using Application.Interfaces;
using System.Text.Json;
using System.Text;
using Domain;

namespace EmailWorker
{
    public class Worker : BackgroundService
    {
        private readonly IUserService userService;
        private readonly ConnectionFactory connectionFactory;
        private readonly IModel channel;
        private readonly EventingBasicConsumer consumer;
        private readonly Configuration configuration;
        public Worker(Configuration configuration, IUserService userService)
        {
            this.configuration = configuration;
            this.userService = userService;
            this.connectionFactory = new ConnectionFactory
            {
                HostName = configuration.RabbitMQ.HostName,
                Port = configuration.RabbitMQ.Port,
                UserName = configuration.RabbitMQ.UserName,
                Password = configuration.RabbitMQ.Password,
            };
            var connection = connectionFactory.CreateConnection();
            channel = connection.CreateModel();
            consumer = new(channel);
        }

        protected override Task ExecuteAsync(CancellationToken stoppingToken)
        {
            consumer.Received += Consumer_Received;
            channel.BasicConsume(queue: configuration.RabbitMQ.EmailQueue , autoAck: false, consumer: consumer);
            return Task.CompletedTask;
        }



        private void Consumer_Received(object sender, BasicDeliverEventArgs ea)
        {
            try
            {
                Console.WriteLine("Time: " + DateTime.Now);
                string message = Encoding.UTF8.GetString(ea.Body.ToArray());
                User user = JsonSerializer.Deserialize<User>(message);

                userService.AddUser(user);

                Console.WriteLine("Record sent, Info : " + user.Email);

                Thread.Sleep(TimeSpan.FromSeconds(20));
            }
            catch (Exception ex)
            {
                Console.WriteLine(ex.ToString());
            }
            finally
            {
                channel.BasicAck(ea.DeliveryTag, false);
            }
        }
    }
}
