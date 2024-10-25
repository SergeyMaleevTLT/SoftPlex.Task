using System.Text;
using Microsoft.AspNetCore.Authentication.JwtBearer;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.IdentityModel.Tokens;
using SoftPlex.Task.Core.Infrastructure.Auth.Impl;


namespace SoftPlex.Task.Core.Infrastructure.Auth;

public static class AuthJwtExtensions
{
    /// <summary>
    /// Инициализирует Jwt аутентификкацию
    /// </summary>
    public static void AddAuthJwt(this IServiceCollection services, JwtSettings settings)
    {
        
        services.AddSingleton(settings);
            
        //Аутентификация пользователя схемой JWT
        services.AddAuthentication(x =>
            {
                x.DefaultAuthenticateScheme = JwtBearerDefaults.AuthenticationScheme;
                x.DefaultScheme = JwtBearerDefaults.AuthenticationScheme;
                x.DefaultChallengeScheme = JwtBearerDefaults.AuthenticationScheme;
            }) //Использовал только секретный ключ, остальное можно добавлять по необходимости
            .AddJwtBearer(option =>
            {
                option.TokenValidationParameters = new TokenValidationParameters
                {
                    ValidateIssuer = false,
                    ValidateAudience = false,
                    ValidateIssuerSigningKey = true,
                    IssuerSigningKey = new SymmetricSecurityKey(Encoding.UTF8.GetBytes(settings.Secret))
                };
            });

        services.AddSingleton<IPasswordHashService, PasswordHashService>();
        services.AddScoped<IAuthService, AuthJwtService>();
    }
}