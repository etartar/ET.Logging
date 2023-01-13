using ET.Logging.Core.Abstract;
using log4net;
using log4net.Config;
using log4net.Repository;
using log4net.Repository.Hierarchy;
using System.Reflection;
using System.Text.Json;
using System.Xml;

namespace ET.Logging.Log4Net.Logger
{
    public class Log4NetLogger : ICustomLogger
    {
        internal readonly ILog _logger;
        internal readonly ILoggerRepository _loggerRepository;

        public Log4NetLogger()
        {
            _logger = LogManager.GetLogger(typeof(Log4NetLogger));

            string logFilePath = string.Format("{0}/{1}", Directory.GetCurrentDirectory(), "log4net.config");

            XmlDocument log4netConfig = new XmlDocument();
            log4netConfig.Load(File.OpenRead(logFilePath));
            ILoggerRepository loggerRepository = LogManager.CreateRepository(Assembly.GetEntryAssembly(), typeof(Hierarchy));
            XmlConfigurator.Configure(loggerRepository, log4netConfig["log4net"]);

            _loggerRepository = loggerRepository;
        }

        public void Debug(object message) => _logger.Debug(JsonSerializer.Serialize(message));

        public void Debug(object message, Exception exception) => _logger.Debug(JsonSerializer.Serialize(message), exception);

        public void Error(object message) => _logger.Error(JsonSerializer.Serialize(message));

        public void Error(object message, Exception exception) => _logger.Error(JsonSerializer.Serialize(message), exception);

        public void Fatal(object message) => _logger.Fatal(JsonSerializer.Serialize(message));

        public void Fatal(object message, Exception exception) => _logger.Fatal(JsonSerializer.Serialize(message), exception);

        public void Info(object message) => _logger.Info(JsonSerializer.Serialize(message));

        public void Info(object message, Exception exception) => _logger.Info(JsonSerializer.Serialize(message), exception);

        public void Warn(object message) => _logger.Warn(JsonSerializer.Serialize(message));

        public void Warn(object message, Exception exception) => _logger.Warn(JsonSerializer.Serialize(message), exception);
    }
}
