namespace Application.Models
{
    public class RabbitMQ
    {
        public string HostName { get; set; }
        public int Port { get; set; }
        public string UserName { get; set; }
        public string Password { get; set; }
        public string EmailQueue { get; set; }
    }
}
