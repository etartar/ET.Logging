using ET.Logging.Serilog.ConfigurationModels;
using ET.Logging.Serilog.Messages;
using Microsoft.Extensions.Configuration;
using MongoDB.Driver;
using Serilog;

namespace ET.Logging.Serilog.Logger
{
    public class MongoDbLogger : LoggerServiceBase
    {
        public MongoDbLogger(IConfiguration configuration)
        {
            var logConfiguration = configuration.GetSection("SeriLogConfigurations:MongoDbConfiguration")
                .Get<MongoDbConfiguration>() ?? throw new Exception(SerilogMessages.NullOptionsMessage);

            Logger = new LoggerConfiguration()
                .WriteTo.MongoDBBson(cfg =>
                {
                    var client = new MongoClient(logConfiguration.ConnectionString);
                    var mongoDbInstance = client.GetDatabase(logConfiguration.Collection);
                    cfg.SetMongoDatabase(mongoDbInstance);
                })
                .CreateLogger();
        }
    }
}
