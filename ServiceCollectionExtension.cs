using Microsoft.AspNetCore.Authentication.JwtBearer;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.IdentityModel.Tokens;
using RateLimit_WebAPI.Database;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace RateLimit_WebAPI
{
    public static class ServiceCollectionExtension
    {
        public static IServiceCollection RegisterDataServices(this IServiceCollection services, IConfiguration configuration)
        {
            try
            {
                services.AddDbContext<DatabaseContext>(opts => opts.UseSqlServer(configuration["ConnectionString:MyConnection"]));
                services.AddScoped<Product, Product>();
                services.AddScoped<User, User>();
                return services;
            }
            catch (Exception ex)
            {
                throw;
            }
        }

        public static IServiceCollection JwtService(this IServiceCollection services, IConfiguration configuration)
        {
            //Adding Authentication
            services.AddAuthentication(options =>
            {
                options.DefaultAuthenticateScheme = JwtBearerDefaults.AuthenticationScheme;
                options.DefaultChallengeScheme = JwtBearerDefaults.AuthenticationScheme;
                options.DefaultScheme = JwtBearerDefaults.AuthenticationScheme;
            })
            //Addding Jwt Bearer
            .AddJwtBearer(options =>
            {
                //Suppose a token is not passed through header and passed through the url query string
                //options.Events = new JwtBearerEvents()
                //{
                //    OnMessageReceived = context =>
                //    {
                //        if (context.Request.Query.ContainsKey("access_token"))
                //        {
                //            context.Token = context.Request.Query["access_token"];
                //        }
                //        return Task.CompletedTask;
                //    }
                //};

                options.SaveToken = true;
                options.RequireHttpsMetadata = false;
                options.TokenValidationParameters = new TokenValidationParameters()
                {
                    ValidateIssuer = true,
                    ValidateAudience = true,
                    ValidateLifetime = true,
                    ValidateIssuerSigningKey = true,
                    ClockSkew = TimeSpan.Zero,
                    ValidAudience = configuration["JWT:ValidAudience"],
                    ValidIssuer = configuration["JWT:ValidIssuer"],
                    IssuerSigningKey = new SymmetricSecurityKey(Encoding.UTF8.GetBytes(configuration["JWT:Secret"]))
                };
            });

            return services;
        }
    }
}
