using Microsoft.AspNetCore.Authentication.JwtBearer;
using Microsoft.AspNetCore.Authorization;
using Microsoft.IdentityModel.Tokens;
using System.IdentityModel.Tokens.Jwt;
using System.Security.Claims;
using System.Security.Cryptography.X509Certificates;
using System.Text;

namespace BusinessServiceTemplate.Auth.Extensions
{
    public static class AuthorizationExtensions
    {
        //public static IServiceCollection ConfigureAuthoization(this IServiceCollection services, IConfiguration configuration)
        //{
        //    var domain = $"https://{configuration["Authorization:Domain"]}/";
        //    services.AddAuthentication(JwtBearerDefaults.AuthenticationScheme)
        //    .AddJwtBearer(options =>
        //    {
        //        options.Authority = domain;
        //        options.Audience = configuration["Authorization:Audience"];
        //        options.TokenValidationParameters = new TokenValidationParameters
        //        {
        //            NameClaimType = ClaimTypes.NameIdentifier
        //        };
        //    });

        //    services.AddAuthorization(options =>
        //    {
        //        //options.AddPolicy("fullaccess:apis", policy => policy.Requirements.Add(new
        //        //SecurityScopeRequirement("fullaccess:apis", domain)));

        //        var operations = Enum.GetValues(typeof(SecurityOperation))
        //            .Cast<SecurityOperation>()
        //            .ToList();

        //        foreach (var operation in operations)
        //        {
        //            options.AddPolicy(operation.ToString(), policy =>
        //            {
        //                policy.AddAuthenticationSchemes(JwtBearerDefaults.AuthenticationScheme);
        //                policy.AddRequirements(
        //                    new SecurityScopeRequirement(operation.ToString(), domain));
        //            });
        //        }
        //    });

        //    services.AddSingleton<IAuthorizationHandler, SecurityScopeHandler>();
        //    return services;
        //}

        public static IServiceCollection ConfigureAuthoization(this IServiceCollection services, IConfiguration configuration)
        {
            //var domain = $"https://{configuration["Authorization:Domain"]}/";

            // Normal approach for JWT token validation
            services.AddAuthentication(JwtBearerDefaults.AuthenticationScheme)
            .AddJwtBearer(options =>
            {
                //options.Authority = domain;
                //options.Audience = configuration["JwtSettings:Audience"];
                options.TokenValidationParameters = new TokenValidationParameters
                {
                    NameClaimType = ClaimTypes.NameIdentifier,
                    ValidateAudience = true,
                    ValidateIssuer = true,
                    ValidateIssuerSigningKey = true,
                    IssuerSigningKey = new SymmetricSecurityKey(Encoding.UTF8.GetBytes(configuration["JwtSettings:Secret"])),
                    ValidIssuer = configuration["JwtSettings:Issuer"],
                    ValidAudience = configuration["JwtSettings:Audience"],
                    ValidateLifetime = true
                };
            });

            //services.AddAuthorization(options =>
            //{
            //    //options.AddPolicy("fullaccess:apis", policy => policy.Requirements.Add(new
            //    //SecurityScopeRequirement("fullaccess:apis", domain)));

            //    var operations = Enum.GetValues(typeof(SecurityOperation))
            //        .Cast<SecurityOperation>()
            //        .ToList();

            //    foreach (var operation in operations)
            //    {
            //        options.AddPolicy(operation.ToString(), policy =>
            //        {
            //            policy.AddAuthenticationSchemes(JwtBearerDefaults.AuthenticationScheme);
            //            policy.AddRequirements(
            //                new SecurityScopeRequirement(operation.ToString(), domain));
            //        });
            //    }
            //});

            //services.AddSingleton<IAuthorizationHandler, SecurityScopeHandler>();



            return services;
        }
    }
}
