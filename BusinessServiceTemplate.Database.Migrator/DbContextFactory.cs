using BusinessServiceTemplate.Database.Migrator.Settings;
using Microsoft.EntityFrameworkCore.Design;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using BusinessServiceTemplate.DataAccess.Data.Contexts;

namespace BusinessServiceTemplate.Database.Migrator
{
    internal class DbContextFactory : IDesignTimeDbContextFactory<TestSelectionRepositoryContext>
    {
        public TestSelectionRepositoryContext CreateDbContext(string[] args)
        {
            //var testSelectionDbBuilder = new DbContextOptionsBuilder<TestSelectionRepositoryContext>()
            //        .UseNpgsql($"Host=localhost;Database=postgres;Username=postgres;Password=password",
            //                b => b.MigrationsAssembly("spacom.service.testselection.dataaccess"));

            var builder = new ConfigurationBuilder()
                      .SetBasePath(Directory.GetCurrentDirectory())
                      .AddJsonFile("appsettings.json", optional: false);

            IConfiguration config = builder.Build();

            var dbConfig = config.GetSection("DbSettings").Get<DbSettings>();

            var testSelectionDbBuilder = new DbContextOptionsBuilder<TestSelectionRepositoryContext>()
                    .UseNpgsql($"Host={dbConfig.Host};Database={dbConfig.Database};Username={dbConfig.Username};Password={dbConfig.Password}",
                            b => b.MigrationsAssembly("BusinessServiceTemplate.Database.Migrator"));

            return new TestSelectionRepositoryContext(testSelectionDbBuilder.Options);
        }
    }
}
