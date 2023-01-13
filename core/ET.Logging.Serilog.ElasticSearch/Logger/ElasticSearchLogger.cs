using ET.Logging.Serilog.ElasticSearch.ConfigurationModels;
using ET.Logging.Serilog.Extensions;
using Microsoft.Extensions.Configuration;
using Serilog;
using Serilog.Formatting.Elasticsearch;
using Serilog.Sinks.Elasticsearch;

namespace ET.Logging.Serilog.ElasticSearch.Logger
{
    public class ElasticSearchLogger : LoggerServiceBase
    {
        public ElasticSearchLogger(IConfiguration configuration)
        {
            var logConfiguration = configuration.GetConfig<ElasticSearchConfiguration>("SeriLogConfigurations:ElasticSearchConfiguration");

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
