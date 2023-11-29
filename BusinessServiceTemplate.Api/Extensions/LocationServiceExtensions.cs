using System.Net;

namespace BusinessServiceTemplate.Api.Extensions
{
    public static class LocationServiceExtensions
    {
        public static IServiceCollection AddApiLocationService(this IServiceCollection services, string url)
        {
            services.AddHttpClient("LocationServiceHttpClient",
            c =>
            {
                c.BaseAddress = new Uri(uriString: url);
                c.Timeout = TimeSpan.FromMinutes(1); //  Can be set to Timeout.InfiniteTimeSpan to allow the TimeoutHandler to set timeout
            })
            .ConfigurePrimaryHttpMessageHandler(() =>
            {
                return new HttpClientHandler()
                {
                    UseDefaultCredentials = true,
                    Credentials = new NetworkCredential("", ""),
                };
            });

            services.AddSingleton<ILocationService, LocationService>();

            return services;
        }
    }
}
