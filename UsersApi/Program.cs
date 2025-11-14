
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Options;
using RegistrationApi.Contracts;
using RegistrationApi.Services;
using System;
using UsersApi.Abstraction;
using UsersApi.Contracts;
using UsersApi.Data;
using UsersApi.Extensions;
using UsersApi.Repositories;
using UsersApi.Services;

namespace UsersApi
{
    public class Program
    {
        public static void Main(string[] args)
        {
            var builder = WebApplication.CreateBuilder(args);

            builder.Services.AddDbContext<UsersApiDbContext>(options =>
                options.UseSqlServer(builder.Configuration.GetConnectionString("DefaultConnection")));

            builder.Services.AddScoped<IUserService, UserService>();
            builder.Services.AddScoped<IUserRepository, UserRepository>();
            builder.Services.AddScoped<IPasswordHasherService, PasswordHasherService>();

            builder.Services.Configure<JwtOptions>(builder.Configuration.GetSection("Jwt"));
            var jwtOptions = builder.Configuration.GetSection("Jwt").Get<JwtOptions>();
            builder.Services.AddScoped<IJwtService, JwtService>();            
            builder.Services.AddApiAuthentication(Options.Create(jwtOptions));

            builder.Services.AddControllers();
            builder.Services.AddEndpointsApiExplorer();
            builder.Services.AddSwaggerGen();

            var app = builder.Build();

            if (app.Environment.IsDevelopment())
            {
                app.UseSwagger();
                app.UseSwaggerUI();
            }

            app.UseCookiePolicy(new CookiePolicyOptions
            {
                MinimumSameSitePolicy = SameSiteMode.Strict,
                HttpOnly = Microsoft.AspNetCore.CookiePolicy.HttpOnlyPolicy.Always,
                Secure = Microsoft.AspNetCore.Http.CookieSecurePolicy.Always
            });

            app.UseAuthentication();
            app.UseAuthorization();

            app.UseHttpsRedirection();

            app.MapControllers();

            app.Run();


        }
    }
}
