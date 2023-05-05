namespace BusinessServiceTemplate.Database.Migrator.Settings
{
    internal class DbSettings
    {
        public required string Database { get; set; }
        public required string Host { get; set; }
        public required string Username { get; set; }
        public required string Password { get; set; }
    }
}
