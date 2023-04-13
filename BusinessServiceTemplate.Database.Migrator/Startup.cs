using BusinessServiceTemplate.Database.Migrator.Configs;
using Microsoft.Extensions.Configuration;

namespace BusinessServiceTemplate.Database.Migrator
{
    public class Startup
    {
        public Startup()
        {
            var builder = new ConfigurationBuilder()
                      .SetBasePath(Directory.GetCurrentDirectory())
                      .AddJsonFile("appsettings.json", optional: false);

            IConfiguration config = builder.Build();

            DbSettings = config.GetSection("DbSettings").Get<DbSettings>();
        }

        internal DbSettings DbSettings { get; private set; }
    }
}
