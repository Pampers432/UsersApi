using Microsoft.AspNetCore.Authentication.JwtBearer;
using Microsoft.Extensions.Options;
using Microsoft.IdentityModel.Tokens;
using RegistrationApi.Contracts;
using System.Text;
using UsersApi.Contracts;

namespace UsersApi.Extensions
{
    public static class ApiExtensions
    {
        public static void AddApiAuthentication(this IServiceCollection services, IOptions<JwtOptions> jwtOptions)
        {
            services.AddAuthentication(JwtBearerDefaults.AuthenticationScheme)
                .AddJwtBearer(JwtBearerDefaults.AuthenticationScheme, options =>
                {
                    options.TokenValidationParameters = new TokenValidationParameters
                    {
                        ValidateIssuer = false,
                        ValidateAudience = false,
                        ValidateLifetime = true,
                        ValidateIssuerSigningKey = true,
                        IssuerSigningKey = new SymmetricSecurityKey(Encoding.UTF8.GetBytes(jwtOptions.Value.Key ?? throw new ArgumentNullException("JWT Key is not configured")))
                    };

                    options.Events = new JwtBearerEvents
                    {
                        OnMessageReceived = context =>
                        {
                            context.Token = context.Request.Cookies["TastyCoks"];

                            if (string.IsNullOrEmpty(context.Token))
                            {
                                var authHeader = context.Request.Headers["Authorization"].FirstOrDefault();
                                if (!string.IsNullOrEmpty(authHeader) && authHeader.StartsWith("Bearer "))
                                {
                                    context.Token = authHeader.Substring(7);
                                }
                            }

                            return Task.CompletedTask;
                        },

                        //OnAuthenticationFailed = context =>
                        //{
                        //    // Логируем ошибки аутентификации
                        //    var logger = context.HttpContext.RequestServices.GetRequiredService<ILogger<Program>>();
                        //    logger.LogError(context.Exception, "JWT Authentication failed");
                        //    return Task.CompletedTask;
                        //},

                        OnForbidden = context =>
                        {
                            // Обработка случаев когда аутентификация прошла, но нет доступа
                            context.Response.StatusCode = 403;
                            return Task.CompletedTask;
                        }
                    };
                });

            services.AddAuthorization(options =>
            {
                // Политика для администраторов
                options.AddPolicy("AdminPolicy", policy =>
                {
                    policy.RequireClaim("role", "2"); 
                });

                // Политика для подтвержденных email
                options.AddPolicy("ConfirmedEmailPolicy", policy =>
                {
                    policy.RequireClaim("email_confirmed", "true");
                });

                // Политика для активных пользователей
                options.AddPolicy("ActiveUserPolicy", policy =>
                {
                    policy.RequireClaim("is_active", "true");
                });

                // Комбинированная политика
                options.AddPolicy("VerifiedUserPolicy", policy =>
                {
                    policy.RequireAuthenticatedUser();
                    policy.RequireClaim("email_confirmed", "true");
                    policy.RequireClaim("is_active", "true");
                });
            });
        }

        public static void ConfigureJwtOptions(this IServiceCollection services, IConfiguration configuration)
        {
            services.Configure<JwtOptions>(configuration.GetSection("Jwt"));
        }
    }
}