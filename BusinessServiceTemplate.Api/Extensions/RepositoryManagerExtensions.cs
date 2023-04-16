using BusinessServiceTemplate.DataAccess;
using BusinessServiceTemplate.DataAccess.Data.Contexts;
using Microsoft.EntityFrameworkCore;

namespace BusinessServiceTemplate.Api.Extensions
{
    public static class RepositoryManagerExtensions
    {
        public static IServiceCollection ConfigureRepositoryManager(this IServiceCollection services, IConfiguration configuration)
        {
            //  Create the repository manager registration in the DI.


            var test = configuration.GetValue<string>("Database:ConnectionString");

            services.AddDbContext<TestSelectionRepositoryContext>(options =>
                    options.UseNpgsql(configuration.GetValue<string>("Database:ConnectionString")));

            services.AddScoped<ITestSelectionRepositoryManager, TestSelectionRepositoryManager>();

            return services;
        }
    }
}
