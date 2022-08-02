using DivinitySoftworks.Apps.Core.Configuration.Managers;
using DivinitySoftworks.Apps.Core.Data;
using System.Threading.Tasks;

namespace DivinitySoftworks.Apps.StreamLoader.UI.ViewModels {

    public interface ISettingsPageViewModel {

        string Name { get; set; }

        string Folder { get; set; }

        ValueTask LoadAsync();

        ValueTask SaveAsync();
    }

    public class SettingsPageViewModel : ViewModel, ISettingsPageViewModel {
        readonly IConfigurationManager _configurationManager;

        public SettingsPageViewModel(IConfigurationManager configurationManager) {
            _configurationManager = configurationManager;
        }


        public string _name = string.Empty;
        public string Name {
            get {
                return _name;
            }
            set {
                ChangeAndNotify(ref _name, value);
            }
        }

        public string _folder = string.Empty;
        public string Folder {
            get {
                return _folder;
            }
            set {
                ChangeAndNotify(ref _folder, value);
            }
        }

        public async ValueTask LoadAsync() {
            Name = await _configurationManager.GetUserSettingAsync<string>(nameof(Name)) ?? string.Empty;
            Folder = await _configurationManager.GetUserSettingAsync<string>(nameof(Folder)) ?? string.Empty;
        }

        public async ValueTask SaveAsync() {
            await _configurationManager.SetUserSettingAsync(nameof(Name), Name);
            await _configurationManager.SetUserSettingAsync(nameof(Folder), Folder);
        }
    }
}
