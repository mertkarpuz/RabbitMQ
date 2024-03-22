using Application.Interfaces;
using Application.Models;
using Application.Services;
using Domain.Interfaces;
using EmailWorker;
using Infrastructure.Context;
using Infrastructure.Repositories;
using Microsoft.EntityFrameworkCore;

IHost host = Host.CreateDefaultBuilder(args)
        .ConfigureServices((hostContext, services) =>
        {
            IConfiguration _configuration = hostContext.Configuration;
            services.AddHostedService<Worker>();
            IServiceProvider serviceProvider = services.BuildServiceProvider();
            Configuration configuration = _configuration.Get<Configuration>();
            services.AddSingleton(configuration);
            services.AddDbContext<MsSqlDbContext>(options =>
options.UseSqlServer(configuration.ConnectionStrings.MsSqlConnection),ServiceLifetime.Singleton);
            services.AddTransient<IUserService, UserService>();
            services.AddTransient<IUserRepository, UserRepository>();

        }).ConfigureAppConfiguration((hostContext, configBuilder) =>
        {
            var env = hostContext.HostingEnvironment;
            var sharedFolder = Path.Combine(env.ContentRootPath, "..", "SolutionItems");
            configBuilder.AddJsonFile(Path.Combine(sharedFolder, "sharedsettings.json"), optional: true)
            .AddJsonFile("sharedsettings.json", optional: true);
        })
    .Build();

await host.RunAsync();