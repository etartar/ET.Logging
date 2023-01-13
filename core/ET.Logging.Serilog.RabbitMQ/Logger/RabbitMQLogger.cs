using ET.Logging.Serilog.Extensions;
using ET.Logging.Serilog.RabbitMQ.ConfigurationModels;
using Microsoft.Extensions.Configuration;
using Serilog;
using Serilog.Formatting.Json;
using Serilog.Sinks.RabbitMQ;
using Serilog.Sinks.RabbitMQ.Sinks.RabbitMQ;

namespace ET.Logging.Serilog.RabbitMQ.Logger
{
    public class RabbitMQLogger : LoggerServiceBase
    {
        public RabbitMQLogger(IConfiguration configuration)
        {
            var logConfiguration = configuration.GetConfig<RabbitMQConfiguration>("SeriLogConfigurations:RabbitMQConfiguration");

            RabbitMQClientConfiguration rabbitMQClientConfiguration = new RabbitMQClientConfiguration
            {
                Port = logConfiguration.Port,
                DeliveryMode = RabbitMQDeliveryMode.Durable,
                Exchange = logConfiguration.Exchange,
                Username = logConfiguration.Username,
                Password = logConfiguration.Password,
                ExchangeType = logConfiguration.ExchangeType,
                RouteKey = logConfiguration.RouteKey,
            };

            logConfiguration.Hostnames.ForEach(x => rabbitMQClientConfiguration.Hostnames.Add(x));

            Logger = new LoggerConfiguration()
                .WriteTo.RabbitMQ((clientConfiguration, sinkConfiguration) =>
                {
                    clientConfiguration.From(rabbitMQClientConfiguration);
                    sinkConfiguration.TextFormatter = new JsonFormatter();
                })
                .CreateLogger();
        }
    }
}
