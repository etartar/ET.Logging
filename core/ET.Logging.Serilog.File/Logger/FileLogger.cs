using ET.Logging.Serilog.Extensions;
using ET.Logging.Serilog.File.ConfigurationModels;
using Microsoft.Extensions.Configuration;
using Serilog;

namespace ET.Logging.Serilog.File.Logger
{
    public class FileLogger : LoggerServiceBase
    {
        public FileLogger(IConfiguration configuration)
        {
            var logConfiguration = configuration.GetConfig<FileLogConfiguration>("SeriLogConfigurations:FileLogConfiguration");

            string logFilePath = string.Format("{0}{1}", Directory.GetCurrentDirectory() + logConfiguration.FolderPath, ".txt");

            Logger = new LoggerConfiguration()
                    .WriteTo.File(
                        logFilePath,
                        rollingInterval: RollingInterval.Day,
                        retainedFileCountLimit: null,
                        fileSizeLimitBytes: 5000000,
                        outputTemplate: "{Timestamp:yyyy-MM-dd HH:mm:ss.fff zzz} [{Level}] {Message}{NewLine}{Exception}")
                    .CreateLogger();
        }
    }
}
