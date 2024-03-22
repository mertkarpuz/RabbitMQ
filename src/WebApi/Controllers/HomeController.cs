using Application.Interfaces;
using Application.Models;
using Domain;
using Microsoft.AspNetCore.Mvc;
using System.Text.Json;

namespace WebApi.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class HomeController(IRabbitMQService rabbitMQService,Configuration configuration) : ControllerBase
    {
        private readonly IRabbitMQService rabbitMQService = rabbitMQService;
        private readonly Configuration configuration = configuration;

        [HttpPost]
        public IActionResult Test()
        {
            for (int i = 0; i < 100; i++)
            {
                User user = new()
                {
                    Email = "mert.karpuz@groupm.com"
                };

                rabbitMQService.Publish(configuration.RabbitMQ.EmailQueue,
                    JsonSerializer.Serialize(user));
            }
            
            return Ok("Başarılı");
        }
    }
}
