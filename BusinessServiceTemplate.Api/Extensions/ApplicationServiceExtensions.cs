using BusinessServiceTemplate.Core.Services;
using BusinessServiceTemplate.Core.Services.Interfaces;

namespace BusinessServiceTemplate.Api.Extensions
{
    public static class ApplicationServiceExtensions
    {
        public static IServiceCollection ConfigureApplicationServices(this IServiceCollection services)
        {
            services.AddSingleton<IImportExportService, ImportExportService>();
            return services;
        }
    }
}
