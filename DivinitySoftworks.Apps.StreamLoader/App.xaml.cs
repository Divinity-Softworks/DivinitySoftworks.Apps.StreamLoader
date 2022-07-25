using DivinitySoftworks.Apps.StreamLoader.Core.Configuration;
using DivinitySoftworks.Apps.StreamLoader.Core.Managers;
using DivinitySoftworks.Apps.StreamLoader.Services;
using DivinitySoftworks.Apps.StreamLoader.UI.Pages;
using DivinitySoftworks.Apps.StreamLoader.UI.ViewModels;
using DivinitySoftworks.Apps.StreamLoader.UI.Windows;
using MediatR;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using System;
using System.IO;
using System.IO.IsolatedStorage;
using System.Windows;

namespace DivinitySoftworks.Apps.StreamLoader {
    
    public partial class App : Application {

        IServiceProvider? _serviceProvider = null;
        public IServiceProvider ServiceProvider {
            get {
                if (_serviceProvider is null) throw new NullReferenceException(nameof(ServiceProvider));
                return _serviceProvider;
            }
            private set {
                _serviceProvider = value;
            }
        }

        IConfiguration? _configuration = null;
        public IConfiguration Configuration {
            get {
                if (_configuration is null) throw new NullReferenceException(nameof(Configuration));
                return _configuration;
            }
            private set {
                _configuration = value;
            }
        }

        protected override void OnStartup(StartupEventArgs e) {
            base.OnStartup(e);

            Configuration = new ConfigurationBuilder()
                .SetBasePath(Directory.GetCurrentDirectory())
                .AddJsonFile("AppSettings.json", false, true)
                .Build();

            ServiceCollection serviceCollection = new();
            ConfigureServices(serviceCollection);

            ServiceProvider = serviceCollection.BuildServiceProvider();
            ServiceProvider.GetRequiredService<MainWindow>().Show();

            
        }


        private void ConfigureServices(IServiceCollection services) {
            services.AddSingleton(typeof(IAppSettings), Configuration.GetSection(nameof(AppSettings)).Get<AppSettings>());
            services.AddSingleton<ILogService, LogService>();
            services.AddSingleton<IStreamService, StreamService>();
            services.AddSingleton<IConfigurationManager, Core.Managers.ConfigurationManager>();

            services.AddMediatR(typeof(App));

            services.AddTransient<IMainWindowViewModel, MainWindowViewModel>();
            services.AddTransient<IYouTubePageViewModel, YouTubePageViewModel>();
            services.AddTransient<ISettingsPageViewModel, SettingsPageViewModel>();

            services.AddTransient(typeof(MainWindow));
            services.AddTransient(typeof(YouTubePage));
            services.AddTransient(typeof(SettingsPage));
        }
    }
}
