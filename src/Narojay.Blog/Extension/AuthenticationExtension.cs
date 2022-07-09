using System.Text;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Authentication.JwtBearer;
using Microsoft.AspNetCore.Hosting;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.IdentityModel.Tokens;
using Narojay.Blog.Infrastruct.Jwt;
using Narojay.Blog.Infrastruct.Jwt.Extension;

namespace Narojay.Blog.Extension;

public static class AuthenticationExtension
{
    public static IServiceCollection AddCustomizedAuthentication(this IServiceCollection services,
        IConfiguration configuration, IWebHostEnvironment env)
    {
        var issuer = configuration["JwtConfig:Issuer"];

        var audience = configuration["JwtConfig:Audience"];

        var secretKey = configuration["JwtConfig:SecretKey"];

        services.AddAuthentication(options =>
        {
            options.DefaultAuthenticateScheme = JwtBearerDefaults.AuthenticationScheme;
            options.DefaultChallengeScheme = JwtBearerDefaults.AuthenticationScheme;
            options.DefaultScheme = JwtBearerDefaults.AuthenticationScheme;
        }).AddJwtBearer(option =>
        {
            option.RequireHttpsMetadata = false;
            option.TokenValidationParameters = new TokenValidationParameters
            {
                ValidateIssuer = true,
                ValidIssuer = issuer,
                ValidateAudience = true,
                ValidAudience = audience,
                ValidateIssuerSigningKey = true,
                IssuerSigningKey = new SymmetricSecurityKey(Encoding.UTF8.GetBytes(secretKey)),
                ValidateLifetime = true,
                SaveSigninToken = true
            };
            option.Events = new JwtBearerEvents
            {
                OnMessageReceived = context =>
                {
                    var accessToken = context.Request.Query["access_token"];
                    var path = context.HttpContext.Request.Path;
                    if (path.StartsWithSegments("/bloghub"))
                    {
                        context.Token = accessToken;
                    }

                    return Task.CompletedTask;
                }
            };
        });

        services.UseJwtService(x => x.ConfigOptions(new JwtOptions
        {
            Audience = audience,
            SecretKey = secretKey,
            Issuer = issuer
        }));

        return services;
    }
}