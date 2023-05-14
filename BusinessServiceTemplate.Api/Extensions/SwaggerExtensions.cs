using BusinessServiceTemplate.Api.Security;
using Microsoft.OpenApi.Models;
using System.Reflection;

namespace BusinessServiceTemplate.Api.Extensions
{
    public static class SwaggerExtensions
    {
        public static IServiceCollection ConfigureSwaggerUi(this IServiceCollection services, IConfiguration configuration)
        {
            services.AddSwaggerGen(options =>
            {
                options.SwaggerDoc("v1", new Microsoft.OpenApi.Models.OpenApiInfo
                {
                    Title = "API Documentation",
                    Version = "v1.0",
                    Description = ""
                });
                options.ResolveConflictingActions(x => x.First());
                options.AddSecurityDefinition("oauth2", new OpenApiSecurityScheme
                {
                    Type = SecuritySchemeType.OAuth2,
                    In = ParameterLocation.Header,
                    Name = "Authorisation",
                    Scheme = "bearer",
                    Flows = new OpenApiOAuthFlows
                    {
                        Implicit = new OpenApiOAuthFlow
                        {
                            TokenUrl = new Uri($"https://{configuration["Authorization:Domain"]}/oauth/token"),
                            AuthorizationUrl = new Uri($"https://{configuration["Authorization:Domain"]}/authorize?audience={configuration["Authorization:Audience"]}"),
                            Scopes = Enum.GetValues(typeof(SecurityOperation)).Cast<SecurityOperation>().ToDictionary(x => x.ToString(), x => x.ToString())
                            //Scopes = new Dictionary<string, string>
                            //{
                            //    { "FullAccess",
                            //      "FullAccess"
                            //    },
                            //}
                        }
                    }
                });
                options.AddSecurityRequirement(new OpenApiSecurityRequirement
                {
                    {
                        new OpenApiSecurityScheme
                        {
                            Reference = new OpenApiReference { Type = ReferenceType.SecurityScheme, Id = "oauth2" }
                        },
                        new string[]{ }
                        //new[] { "openid", "PanelAccess" }
                    }
                });

                //  XML Documentation
                var xmlFile = $"{Assembly.GetExecutingAssembly().GetName().Name}.xml";
                var xmlPath = Path.Combine(AppContext.BaseDirectory, xmlFile);

                options.IncludeXmlComments(xmlPath);

            });

            return services;
        }
    }
}
