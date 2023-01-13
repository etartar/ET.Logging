using ET.Logging.Serilog.ConfigurationModels;
using ET.Logging.Serilog.Messages;
using Microsoft.Extensions.Configuration;
using Serilog;
using Serilog.Sinks.MSSqlServer;

namespace ET.Logging.Serilog.Logger
{
    public class MsSqlLogger : LoggerServiceBase
    {
        public MsSqlLogger(IConfiguration configuration)
        {
            var logConfiguration = configuration.GetSection("SeriLogConfigurations:MsSqlConfiguration")
                .Get<MsSqlConfiguration>() ?? throw new Exception(SerilogMessages.NullOptionsMessage);

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
