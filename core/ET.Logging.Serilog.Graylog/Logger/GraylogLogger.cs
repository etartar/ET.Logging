using ET.Logging.Serilog.Extensions;
using ET.Logging.Serilog.Graylog.ConfigurationModels;
using Microsoft.Extensions.Configuration;
using Serilog;
using Serilog.Sinks.Graylog;
using Serilog.Sinks.Graylog.Core.Transport;

namespace ET.Logging.Serilog.Graylog.Logger
{
    public class GraylogLogger : LoggerServiceBase
    {
        public GraylogLogger(IConfiguration configuration)
        {
            var logConfiguration = configuration.GetConfig<GraylogConfiguration>("SeriLogConfigurations:GraylogConfiguration");

            GraylogSinkOptions sinkOptions = new GraylogSinkOptions
            {
                HostnameOrAddress = logConfiguration.HostnameOrAddress,
                Port = logConfiguration.Port,
                TransportType = (TransportType)logConfiguration.TransportType
            };

            Logger = new LoggerConfiguration()
                .WriteTo.Graylog(sinkOptions)
                .CreateLogger();
        }
    }
}
