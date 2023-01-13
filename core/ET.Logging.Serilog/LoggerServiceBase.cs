using ET.Logging.Core.Abstract;
using Serilog;
using System.Text.Json;

namespace ET.Logging.Serilog
{
    public abstract class LoggerServiceBase : ICustomLogger
    {
        protected ILogger Logger { get; set; }

        public void Debug(object message) => Logger.Debug(JsonSerializer.Serialize(message));

        public void Debug(object message, Exception exception) => Logger.Debug(JsonSerializer.Serialize(message), exception);

        public void Error(object message) => Logger.Error(JsonSerializer.Serialize(message));

        public void Error(object message, Exception exception) => Logger.Error(JsonSerializer.Serialize(message), exception);

        public void Fatal(object message) => Logger.Fatal(JsonSerializer.Serialize(message));

        public void Fatal(object message, Exception exception) => Logger.Fatal(JsonSerializer.Serialize(message), exception);

        public void Info(object message) => Logger.Information(JsonSerializer.Serialize(message));

        public void Info(object message, Exception exception) => Logger.Information(JsonSerializer.Serialize(message), exception);

        public void Warn(object message) => Logger.Warning(JsonSerializer.Serialize(message));

        public void Warn(object message, Exception exception) => Logger.Warning(JsonSerializer.Serialize(message), exception);
    }
}
