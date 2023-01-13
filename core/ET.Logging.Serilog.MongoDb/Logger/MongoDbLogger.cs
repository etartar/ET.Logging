using ET.Logging.Serilog.MongoDb.ConfigurationModels;
using ET.Logging.Serilog.Extensions;
using Microsoft.Extensions.Configuration;
using MongoDB.Driver;
using Serilog;

namespace ET.Logging.Serilog.MongoDb.Logger
{
    public class MongoDbLogger : LoggerServiceBase
    {
        public MongoDbLogger(IConfiguration configuration)
        {
            var logConfiguration = configuration.GetConfig<MongoDbConfiguration>("SeriLogConfigurations:MongoDbConfiguration");

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
