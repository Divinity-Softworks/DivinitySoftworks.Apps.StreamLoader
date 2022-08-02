using DivinitySoftworks.Apps.StreamLoader.Core.Configuration;
using DivinitySoftworks.Apps.StreamLoader.Services;
using DivinitySoftworks.Apps.StreamLoader.UI.Pages;
using DivinitySoftworks.Apps.StreamLoader.UI.ViewModels;
using DivinitySoftworks.Apps.StreamLoader.UI.Windows;
using MediatR;
using Microsoft.Extensions.DependencyInjection;

namespace DivinitySoftworks.Apps.StreamLoader {

    /// <summary>
    /// Divinity Softworks Stream Loader Application.
    /// </summary>
    public class Application : Apps.Core.App<IAppSettings, AppSettings> {
    }

    /// <inheritdoc/>
    public partial class App : Application {

        /// <inheritdoc/>
        protected override void OnStartup(System.Windows.StartupEventArgs e) {
            base.OnStartup(e);

            ServiceProvider.GetRequiredService<MainWindow>().Show();
        }

        /// <inheritdoc/>
        override protected void ConfigureServices(IServiceCollection services) {
            base.ConfigureServices(services);
            services.AddSingleton<ILogService, LogService>();
            services.AddSingleton<IStreamService, StreamService>();

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
