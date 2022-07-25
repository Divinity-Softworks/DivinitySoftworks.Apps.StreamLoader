using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.IO;
using System.IO.IsolatedStorage;
using System.Text;
using System.Threading.Tasks;

namespace DivinitySoftworks.Apps.StreamLoader.Core.Managers {

    public interface IConfigurationManager {
        event EventHandler<ConfigurationChangedEventArgs>? OnConfigurationChanged;
        ValueTask<T?> GetUserSettingAsync<T>(string key);
        ValueTask SetUserSettingAsync(string key, object value);
    }

    public class ConfigurationManager : IConfigurationManager {
        protected string _directory = "Settings";

        Dictionary<string, object>? _userSettings;

        public event EventHandler<ConfigurationChangedEventArgs>? OnConfigurationChanged;

        public async ValueTask<T?> GetUserSettingAsync<T>(string key) {
            if (_userSettings is null || !_userSettings.ContainsKey(key))
                await LoadUserSettingsAsync();

            if (_userSettings is null || !_userSettings.ContainsKey(key))
                return default;

            if (_userSettings[key] is T)
                return (T)_userSettings[key];

            try {
                return (T)Convert.ChangeType(_userSettings[key], typeof(T));
            }
            catch (InvalidCastException) {
                return default;
            }
        }

        public async ValueTask SetUserSettingAsync(string key, object value) {
            if (_userSettings is null || !_userSettings.ContainsKey(key))
                await LoadUserSettingsAsync();

            if (_userSettings is null)
                _userSettings = new Dictionary<string, object>();

            if (_userSettings.ContainsKey(key)) {
                _userSettings[key] = value;
                await SaveUserSettingsAsync();

                OnConfigurationChanged?.Invoke(this, new ConfigurationChangedEventArgs(key));
                
                return;
            }

            _userSettings.Add(key, value);
            await SaveUserSettingsAsync();

            OnConfigurationChanged?.Invoke(this, new ConfigurationChangedEventArgs(key));

            return;
        }

        private async Task LoadUserSettingsAsync() {
            using IsolatedStorageFile isolatedStorageFile = IsolatedStorageFile.GetUserStoreForApplication();
            isolatedStorageFile.CreateDirectory(_directory);
            if (!isolatedStorageFile.FileExists($@"{_directory}\User.cfg")) {
                await SaveUserSettingsAsync();
                return;
            }

            using IsolatedStorageFileStream isolatedStorageFileStream = isolatedStorageFile.OpenFile($@"{_directory}\User.cfg", FileMode.OpenOrCreate);
            using StreamReader streamReader = new(isolatedStorageFileStream);
            // Using 'ReadToEnd()', since 'ReadToEndAsync()' doesn't return.
            _userSettings = JsonConvert.DeserializeObject<Dictionary<string, object>>(streamReader.ReadToEnd());
        }

        private ValueTask SaveUserSettingsAsync() {
            if (_userSettings is null)
                _userSettings = new();

            using IsolatedStorageFile isolatedStorageFile = IsolatedStorageFile.GetUserStoreForApplication();
            isolatedStorageFile.CreateDirectory(_directory);
            using IsolatedStorageFileStream isolatedStorageFileStream = isolatedStorageFile.CreateFile($@"{_directory}\User.cfg");
            return isolatedStorageFileStream.WriteAsync(new ReadOnlyMemory<byte>(Encoding.UTF8.GetBytes(JsonConvert.SerializeObject(_userSettings))));
        }
    }

    public class ConfigurationChangedEventArgs : EventArgs {
        public ConfigurationChangedEventArgs(string key) {
            Key = key;
        }

        public string Key { get; set; }
    }
}
