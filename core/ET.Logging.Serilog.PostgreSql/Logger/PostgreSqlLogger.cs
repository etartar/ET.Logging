using ET.Logging.Serilog.Extensions;
using ET.Logging.Serilog.PostgreSql.ConfigurationModels;
using Microsoft.Extensions.Configuration;
using NpgsqlTypes;
using Serilog;
using Serilog.Sinks.PostgreSQL;

namespace ET.Logging.Serilog.PostgreSql.Logger
{
    public class PostgreSqlLogger : LoggerServiceBase
    {
        public PostgreSqlLogger(IConfiguration configuration)
        {
            var logConfiguration = configuration.GetConfig<PostgreSqlConfiguration>("SeriLogConfigurations:PostgreConfiguration");

            IDictionary<string, ColumnWriterBase> columWriters = new Dictionary<string, ColumnWriterBase>
            {
                { "message", new RenderedMessageColumnWriter(NpgsqlDbType.Text) },
                { "message_template", new RenderedMessageColumnWriter(NpgsqlDbType.Text) },
                { "level", new RenderedMessageColumnWriter(NpgsqlDbType.Varchar) },
                { "raise_date", new TimestampColumnWriter(NpgsqlDbType.Timestamp) },
                { "exception", new ExceptionColumnWriter(NpgsqlDbType.Text) },
                { "properties", new LogEventSerializedColumnWriter(NpgsqlDbType.Jsonb) },
                { "props_test", new PropertiesColumnWriter(NpgsqlDbType.Jsonb) },
                { "machine_name", new SinglePropertyColumnWriter("MachineName", PropertyWriteMethod.ToString, NpgsqlDbType.Text, "1") }
            };

            Logger = new LoggerConfiguration()
                .WriteTo.PostgreSQL(
                connectionString: logConfiguration.ConnectionString,
                tableName: logConfiguration.TableName,
                columnOptions: columWriters,
                needAutoCreateTable: logConfiguration.NeedAutoCreateTable)
                .CreateLogger();
        }
    }
}
