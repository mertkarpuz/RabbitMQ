namespace Application.Interfaces
{
    public interface IRabbitMQService
    {
        void Publish(string queueName, string data);
    }
}
