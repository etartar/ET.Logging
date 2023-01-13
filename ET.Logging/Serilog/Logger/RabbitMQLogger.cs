using ET.Logging.Serilog.ConfigurationModels;
using ET.Logging.Serilog.Messages;
using Microsoft.Extensions.Configuration;
using Serilog;
using Serilog.Formatting.Json;
using Serilog.Sinks.RabbitMQ;
using Serilog.Sinks.RabbitMQ.Sinks.RabbitMQ;

namespace ET.Logging.Serilog.Logger
{
    public class RabbitMQLogger : LoggerServiceBase
    {
        public RabbitMQLogger(IConfiguration configuration)
        {
            var logConfiguration = configuration.GetSection("SeriLogConfigurations:RabbitMQConfiguration")
                .Get<RabbitMQConfiguration>() ?? throw new Exception(SerilogMessages.NullOptionsMessage);

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
