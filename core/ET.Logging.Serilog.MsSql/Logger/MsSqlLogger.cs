using ET.Logging.Serilog.Extensions;
using ET.Logging.Serilog.MsSql.ConfigurationModels;
using Microsoft.Extensions.Configuration;
using Serilog;
using Serilog.Sinks.MSSqlServer;

namespace ET.Logging.Serilog.MsSql.Logger
{
    public class MsSqlLogger : LoggerServiceBase
    {
        public MsSqlLogger(IConfiguration configuration)
        {
            var logConfiguration = configuration.GetConfig<MsSqlConfiguration>("SeriLogConfigurations:MsSqlConfiguration");

            var sinkOptions = new MSSqlServerSinkOptions
            {
                TableName = logConfiguration.TableName,
                AutoCreateSqlTable = logConfiguration.AutoCreateSqlTable
            };

            ColumnOptions columnOptions = new();

            Logger = new LoggerConfiguration()
                .WriteTo.MSSqlServer(
                connectionString: logConfiguration.ConnectionString,
                sinkOptions: sinkOptions,
                columnOptions: columnOptions)
                .CreateLogger();
        }
    }
}
