using System;
using System.IO;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using robot.ControllerPackages;

namespace robot
{
    /// <summary>
    /// Wire up Dependency injection to start the application
    /// </summary>
    class Program
    {
        private static ServiceProvider _serviceProvider;
        public static IConfigurationRoot _configuration;

        static void Main(string[] args)
        {
            RegisterServices();
            IServiceScope scope = _serviceProvider.CreateScope();
            scope.ServiceProvider.GetRequiredService<RobotApplication>().Run(args);
            DisposeServices();
        }
        private static void RegisterServices()
        {
            var services = new ServiceCollection();
            services.AddSingleton<IControllerPackage, ControllerPackage_en_au_2d>();
            services.AddSingleton<RobotApplication>();
            // Build configuration
            var path = Directory.GetParent(AppContext.BaseDirectory).FullName;
            _configuration = new ConfigurationBuilder()
                .SetBasePath(Directory.GetParent(AppContext.BaseDirectory).FullName)
                .AddJsonFile("appsettings.json", false)
                .Build();
            services.AddSingleton<IConfigurationRoot>(_configuration);
            _serviceProvider = services.BuildServiceProvider(true);
        }
        private static void DisposeServices()
        {
            if (_serviceProvider == null)
            {
                return;
            }
            if (_serviceProvider is IDisposable)
            {
                ((IDisposable)_serviceProvider).Dispose();
            }
        }

    }
}
