namespace ET.Logging.Serilog.Graylog.ConfigurationModels
{
    public class GraylogConfiguration
    {
        public string? HostnameOrAddress { get; set; }
        public int Port { get; set; }
        public int TransportType { get; set; }
    }
}
