using ET.Logging.Serilog.ConfigurationModels;
using ET.Logging.Serilog.Messages;
using Microsoft.Extensions.Configuration;
using Serilog;
using Serilog.Formatting.Elasticsearch;
using Serilog.Sinks.Elasticsearch;

namespace ET.Logging.Serilog.Logger
{
    public class ElasticSearchLogger : LoggerServiceBase
    {
        public ElasticSearchLogger(IConfiguration configuration)
        {
            var logConfiguration = configuration.GetSection("SeriLogConfigurations:ElasticSearchConfiguration")
                .Get<ElasticSearchConfiguration>() ?? throw new Exception(SerilogMessages.NullOptionsMessage);

            Logger = new LoggerConfiguration()
                .WriteTo.Elasticsearch(new ElasticsearchSinkOptions(new Uri(logConfiguration.ConnectionString))
                {
                    AutoRegisterTemplate = logConfiguration.AutoRegisterTemplate,
                    AutoRegisterTemplateVersion = (AutoRegisterTemplateVersion)logConfiguration.AutoRegisterTemplateVersion,
                    CustomFormatter = new ExceptionAsObjectJsonFormatter(renderMessage: logConfiguration.RenderMessage)
                })
                .CreateLogger();
        }
    }
}
