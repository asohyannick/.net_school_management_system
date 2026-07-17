using learning_ms.Web.Application.Interface.ITokenService;
using Microsoft.AspNetCore.Authentication.JwtBearer;
using Microsoft.IdentityModel.Tokens;
namespace learning_ms.Web.Infrastructure.Auth.JwtServiceCollectionExtensions;
using System.Text;
public static class JwtServiceCollectionExtensions
{
    public static IServiceCollection AddJwtAuthentication(
        this IServiceCollection services, IConfiguration configuration)
    {
        var jwtSettings = configuration.GetSection(JwtSettings.JwtSettings.SectionName).Get<JwtSettings.JwtSettings>()
            ?? throw new InvalidOperationException(
                $"Missing '{JwtSettings.JwtSettings.SectionName}' configuration section.");

        services.Configure<JwtSettings.JwtSettings>(configuration.GetSection(JwtSettings.JwtSettings.SectionName));
        services.AddScoped<ITokenService, JwtTokenService.JwtTokenService>();

        services
          .AddAuthentication(options =>
          {
            options.DefaultAuthenticateScheme = JwtBearerDefaults.AuthenticationScheme;
            options.DefaultChallengeScheme = JwtBearerDefaults.AuthenticationScheme;
          })
          .AddJwtBearer(options =>
          {
            options.TokenValidationParameters = new TokenValidationParameters
            {
              ValidateIssuer = true,
              ValidIssuer = jwtSettings.Issuer,
              ValidateAudience = true,
              ValidAudience = jwtSettings.Audience,
              ValidateIssuerSigningKey = true,
              IssuerSigningKey = new SymmetricSecurityKey(Encoding.UTF8.GetBytes(jwtSettings.SecretKey)),
              ValidateLifetime = true,
              ClockSkew = TimeSpan.FromMinutes(1),
            };

            options.Events = new JwtBearerEvents
            {
              OnMessageReceived = context =>
              {
                if (context.Request.Cookies.TryGetValue("accessToken", out var token))
                {
                  context.Token = token;
                }
                return Task.CompletedTask;
              }
            };
          });

        services.AddAuthorization();

        return services;
    }
}
