using System.Text;
using Asp.Versioning;
using Microsoft.AspNetCore.Authentication.JwtBearer;
using Microsoft.AspNetCore.Identity;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.IdentityModel.Tokens;
using Swashbuckle.AspNetCore.Filters;
using Microsoft.OpenApi.Models;
using Tech.DAL;
using Tech.Domain.Entity;
using Tech.Domain.Settings;

namespace Tech.Api
{
    public static class Startup
    {
        public static void AddAuthenticationAndAuthorization(this IServiceCollection services, WebApplicationBuilder builder)
        {
            var options = builder.Configuration.GetSection(JwtSettings.DefaultSection).Get<JwtSettings>();
            var jwtKey = options.JwtKey;
            var issuer = options.Issuer;
            var audience = options.Audience;
            
            services.AddAuthorization();
            
            services.AddIdentity<User, IdentityRole<long>>(identityOptions =>
                {
                    identityOptions.Password.RequiredLength = 5;
    
                })
                .AddEntityFrameworkStores<ApplicationDbContext>()
                .AddDefaultTokenProviders();
            
            services.AddAuthentication(authOptions =>
            {
                authOptions.DefaultAuthenticateScheme = JwtBearerDefaults.AuthenticationScheme;
                authOptions.DefaultChallengeScheme = JwtBearerDefaults.AuthenticationScheme;
            }).AddJwtBearer(jwtOptions =>
            {
                jwtOptions.TokenValidationParameters = new TokenValidationParameters()
                {
                    ValidateActor = true,
                    ValidateIssuer = true,
                    ValidateAudience = true,
                    RequireExpirationTime = true,
                    ValidateIssuerSigningKey = true,
                    ValidIssuer = issuer,
                    ValidAudience = audience,
                    IssuerSigningKey = new SymmetricSecurityKey(Encoding.UTF8.GetBytes(jwtKey))
                };
            });
        }

        public static void AddSwagger(this IServiceCollection services)
        {
            services.AddApiVersioning()
                .AddApiExplorer(versionApiOptions =>
                {
                    versionApiOptions.DefaultApiVersion = new ApiVersion(1, 0);
                    versionApiOptions.GroupNameFormat = "'v'VVV";
                    versionApiOptions.SubstituteApiVersionInUrl = true;
                    versionApiOptions.AssumeDefaultVersionWhenUnspecified = true;
                });

            services.AddEndpointsApiExplorer();
            
            services.AddSwaggerGen(swaggerOptions =>
            {
                swaggerOptions.SwaggerDoc("v1", new OpenApiInfo()
                {
                    Version = "v1",
                    Title = "Tech v1",
                    Description = "This is v1",
                });
            });
        }
    }
}
