// See https://aka.ms/new-console-template for more information
using BusinessServiceTemplate.DataAccess.Data.Contexts;
using BusinessServiceTemplate.Database.Migrator;
using Microsoft.EntityFrameworkCore;

var startup = new Startup();

var host = startup.DbSettings.Host;
var username = startup.DbSettings.Username;
var password = startup.DbSettings.Password;
var database = startup.DbSettings.Database;

var testSelectionDb = new DbContextOptionsBuilder<TestSelectionRepositoryContext>()
    .UseNpgsql($"Host={host};Database={database};Username={username};Password={password}",
            b => b.MigrationsAssembly("spacom.service.testselection.dataaccess"));

var options = testSelectionDb.Options;

using (var testSelectionDbContext = new TestSelectionRepositoryContext(options))
{
    // Create design-time services
}

Console.ReadLine();