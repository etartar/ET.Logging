namespace ET.Logging.Serilog.ConfigurationModels
{
    public class ElasticSearchConfiguration
    {
        public string ConnectionString { get; set; }
        public bool AutoRegisterTemplate { get; set; }
        public int AutoRegisterTemplateVersion { get; set; }
        public bool RenderMessage { get; set; }
    }
}
