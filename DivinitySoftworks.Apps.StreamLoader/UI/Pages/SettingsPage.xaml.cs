using DivinitySoftworks.Apps.Core.Components;
using DivinitySoftworks.Apps.StreamLoader.UI.ViewModels;
using System.Windows.Controls;

namespace DivinitySoftworks.Apps.StreamLoader.UI.Pages {

    public partial class SettingsPage : Page {
        public SettingsPage(ISettingsPageViewModel settingsPageViewModel) {
            InitializeComponent();

            DataContext = settingsPageViewModel;
        }

        public ISettingsPageViewModel ViewModel {
            get {
                return (ISettingsPageViewModel)DataContext;
            }
        }

        private async void TextField_TextChanged(object sender, TextChangedEventArgs e) {
            if (((TextField)sender).IsFocused)
                await ViewModel.SaveAsync();
        }

        private async void Page_Loaded(object sender, System.Windows.RoutedEventArgs e) {
            await ViewModel.LoadAsync();
        }
    }
}
