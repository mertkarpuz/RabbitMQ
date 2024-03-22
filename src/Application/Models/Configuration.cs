using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Application.Models
{
    public class Configuration
    {
        public RabbitMQ RabbitMQ { get; set; }
        public ConnectionStrings ConnectionStrings { get; set; }
    }
}
