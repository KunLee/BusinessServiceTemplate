using BusinessServiceTemplate.Auth.Services;
using BusinessServiceTemplate.Auth.Services.Interfaces;

namespace BusinessServiceTemplate.Auth.Extensions
{
    public static class TokenServiceExtensions
    {
        public static IServiceCollection ConfigureTokenServices(this IServiceCollection services)
        {
            services.AddSingleton<ITokenService, TokenService>();
            return services;
        }
    }
}
