using Application.Interfaces;
using Application.Models;
using Application.Services;
using Domain.Interfaces;
using Infrastructure.Context;
using Infrastructure.Repositories;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;

namespace Infrastructure.Ioc
{
    public static class DependencyInjection
    {
        public static void RegisterServices(this IServiceCollection services, IConfiguration _configuration)
        {
            Configuration configuration = _configuration.Get<Configuration>();
            services.AddSingleton(configuration);

            services.AddDbContext<MsSqlDbContext>(options =>
options.UseSqlServer(configuration.ConnectionStrings.MsSqlConnection),ServiceLifetime.Transient);


            services.AddScoped<IRabbitMQService, RabbitMQService>();
            services.AddScoped<IUserRepository, UserRepository>();
            services.AddScoped<IUserService, UserService>();


        }
    }
}
