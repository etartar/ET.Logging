namespace ET.Logging.Serilog.RabbitMQ.ConfigurationModels
{
    public class RabbitMQConfiguration
    {
        public RabbitMQConfiguration()
        {
            Hostnames = new List<string>();
        }

        public int Port { get; set; }
        public string? Exchange { get; set; }
        public string? Username { get; set; }
        public string? Password { get; set; }
        public string? ExchangeType { get; set; }
        public string? RouteKey { get; set; }
        public List<string> Hostnames { get; set; }
    }
}
