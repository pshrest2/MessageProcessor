using System;
namespace RSMessageProcessor.RabbitMQ.Dtos
{
	public class RabbitConfig
	{
		public RabbitConfig()
		{
		}
        public string UserName { get; set; }

        public string Password { get; set; }

        public string HostName { get; set; }

        public int Port { get; set; } = 5672;

        public string VHost { get; set; } = "/";

        public string Uri { get; set; }
    }
}

