using DivinitySoftworks.Apps.StreamLoader.Core.Managers;
using DivinitySoftworks.Apps.StreamLoader.UI.Pages;
using DivinitySoftworks.Apps.StreamLoader.UI.ViewModels.Base;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using System.Windows.Controls;

namespace DivinitySoftworks.Apps.StreamLoader.UI.ViewModels {

    public interface IMainWindowViewModel {

        string Name { get; set; }

        Page? Page { get; set; }

        void SetPage(Type target);

        ValueTask LoadAsync();
    }

    public class MainWindowViewModel : ViewModel, IMainWindowViewModel {
        readonly IConfigurationManager _configurationManager;

        readonly Dictionary<Type, Page> _pages = new ();

        public MainWindowViewModel(IConfigurationManager configurationManager, YouTubePage youTubePage, SettingsPage settingsPage) {
            _configurationManager = configurationManager;
            _configurationManager.OnConfigurationChanged += ConfigurationManager_OnConfigurationChanged;

            _pages.Add(typeof(YouTubePage), youTubePage);
            _pages.Add(typeof(SettingsPage), settingsPage);

            Page = youTubePage;
        }


        public string _name = "Mystery Guest";
        public string Name {
            get {
                return _name;
            }
            set {
                ChangeAndNotify(ref _name, value);
            }
        }

        Page? _page;
        public Page? Page {
            get {
                return _page;
            }
            set {
                ChangeAndNotify(ref _page, value);
            }
        }

        public void SetPage(Type target) {
            if (_pages.ContainsKey(target)) {
                Page = _pages[target];
                return;
            }

            _page = _pages.First().Value;
        }

        public async ValueTask LoadAsync() {
            string? name = await _configurationManager.GetUserSettingAsync<string>(nameof(Name));
            string? folder = await _configurationManager.GetUserSettingAsync<string>("Folder");
            Name = name ?? "Mystery Guest";

            if(string.IsNullOrWhiteSpace(name) || string.IsNullOrWhiteSpace(folder))
                SetPage(typeof(SettingsPage));
        }

        private async void ConfigurationManager_OnConfigurationChanged(object? sender, ConfigurationChangedEventArgs e) {
            if (e.Key != nameof(Name)) return;
            Name = await _configurationManager.GetUserSettingAsync<string>(e.Key) ?? string.Empty;
        }
    }
}
