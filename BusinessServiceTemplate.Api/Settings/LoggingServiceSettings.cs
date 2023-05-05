namespace BusinessServiceTemplate.Api.Settings
{
    public class LoggingServiceSettings
    {
        public required string BaseAddress { get; set; }
        public string? InstanceId { get; set; }
        public string? AppName { get; set; }
        public string? HostName { get; set; }
        public bool RemoteLogging { get; set; }
    }
}
