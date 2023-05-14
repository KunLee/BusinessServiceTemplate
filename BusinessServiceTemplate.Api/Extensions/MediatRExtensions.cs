namespace BusinessServiceTemplate.Api.Extensions
{
    public static class MediatRExtensions
    {
        public static IServiceCollection ConfigureMediatR(this IServiceCollection services)
        {
            //  Create the MediatR registration in the DI.
            services.AddMediatR(cfg => { 
                cfg.RegisterServicesFromAssembly(typeof(AssemblyMarker).Assembly);
                cfg.RegisterServicesFromAssembly(typeof(Core.AssemblyMarker).Assembly);
            });

            return services;
        }
    }
}
