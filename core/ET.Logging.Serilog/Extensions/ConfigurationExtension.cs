using ET.Logging.Serilog.Messages;
using Microsoft.Extensions.Configuration;

namespace ET.Logging.Serilog.Extensions
{
    public static class ConfigurationExtension
    {
        public static T GetConfig<T>(this IConfiguration configuration, string sectionName)
        {
            return configuration.GetSection(sectionName).Get<T>() ?? throw new Exception(SerilogMessages.NullOptionsMessage);
        }
    }
}
