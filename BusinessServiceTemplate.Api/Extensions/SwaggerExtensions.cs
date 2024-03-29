﻿using BusinessServiceTemplate.Api.Security;
using BusinessServiceTemplate.Core.Attributes;
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
                        AuthorizationCode = new OpenApiOAuthFlow
                        {
                            TokenUrl = new Uri($"https://{configuration["Authorization:Domain"]}/oauth/token"),
                            AuthorizationUrl = new Uri($"https://{configuration["Authorization:Domain"]}/authorize?audience={configuration["Authorization:Audience"]}"),
                            Scopes = Enum.GetValues(typeof(SecurityOperation)).Cast<SecurityOperation>().ToDictionary(x => x.ToString(), x => x.ToString())

                            // scopes added here will be used to request the matching scope defined in the authorized App which has been authorized the permissions to the api
                            //Scopes = new Dictionary<string, string>
                            //{
                            //    { $"PanelAccess",
                            //      $"PanelAccess"
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

                options.DocInclusionPredicate((docName, apiDesc) =>
                {
                    var actionMethodInfo = apiDesc.ActionDescriptor.GetType().GetProperty("MethodInfo").GetValue(apiDesc.ActionDescriptor) as MethodInfo;

                    if (actionMethodInfo != null)
                    {
                        var hideAttribute = actionMethodInfo.GetCustomAttribute<SwaggerHideInEnvironmentAttribute>();

                        if (hideAttribute != null)
                        {
                            var currentEnvironment = Environment.GetEnvironmentVariable("ASPNETCORE_ENVIRONMENT");

                            if (hideAttribute.Environments.Contains(currentEnvironment, StringComparer.OrdinalIgnoreCase))
                            {
                                return false;
                            }
                        }
                    }

                    return true;
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
