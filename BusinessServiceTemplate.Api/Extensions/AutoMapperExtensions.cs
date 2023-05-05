using BusinessServiceTemplate.Core;
using System.Reflection;
using System.Security.Cryptography.Xml;

namespace BusinessServiceTemplate.Api.Extensions
{
    public static class AutoMapperExtensions
    {
        public static IServiceCollection ConfigureAutoMapper(this IServiceCollection services)
        {
            //  Create the automapper registration in the DI.
            services.AddAutoMapper(
                new List<Assembly?>
                {
                    Assembly.GetAssembly(typeof(AssemblyMarker)),
                    Assembly.GetAssembly(typeof(Core.AssemblyMarker))
                },
                ServiceLifetime.Singleton);

            return services;
        }
    }
}
