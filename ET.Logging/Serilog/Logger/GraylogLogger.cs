using ET.Logging.Serilog.ConfigurationModels;
using ET.Logging.Serilog.Messages;
using Microsoft.Extensions.Configuration;
using Serilog;
using Serilog.Sinks.Graylog;
using Serilog.Sinks.Graylog.Core.Transport;

namespace ET.Logging.Serilog.Logger
{
    public class GraylogLogger : LoggerServiceBase
    {
        public GraylogLogger(IConfiguration configuration)
        {
            var logConfiguration = configuration.GetSection("SeriLogConfigurations:GraylogConfiguration")
                .Get<GraylogConfiguration>() ?? throw new Exception(SerilogMessages.NullOptionsMessage);

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
