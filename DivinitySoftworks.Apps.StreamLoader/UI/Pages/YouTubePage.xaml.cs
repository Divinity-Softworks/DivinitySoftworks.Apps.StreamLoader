using DivinitySoftworks.Apps.Core.Components;
using DivinitySoftworks.Apps.StreamLoader.Data.Enums;
using DivinitySoftworks.Apps.StreamLoader.Services;
using DivinitySoftworks.Apps.StreamLoader.UI.Components;
using DivinitySoftworks.Apps.StreamLoader.UI.ViewModels;
using System;
using System.Windows.Controls;
using System.Windows.Input;

namespace DivinitySoftworks.Apps.StreamLoader.UI.Pages {

    public partial class YouTubePage : Page {
        readonly ILogService _logService;

        public YouTubePage(IYouTubePageViewModel youTubePageViewModel, ILogService logService) {
            InitializeComponent();

            DataContext = youTubePageViewModel;

            _logService = logService;
        }

        public IYouTubePageViewModel ViewModel {
            get {
                return (IYouTubePageViewModel)DataContext;
            }
        }

        private async void TextBox_KeyUp(object sender, KeyEventArgs e) {
            if (e.Key is not Key.Enter)
                return;

            if (!Uri.TryCreate(ViewModel.Url, UriKind.Absolute, out Uri? uri)) {
                _logService.LogError("Invalid URL", $"The value [{ViewModel.Url}] is not a valid URL.");
                return;
            }

            await ViewModel.AddStreamItemAsync(uri, ViewModel.StreamType);
            ViewModel.Url = string.Empty;
        }

        private void ToggleButton_Click(object sender, System.Windows.RoutedEventArgs e) {
            if (sender is not ToggleButton) return;

            ViewModel.StreamType = (StreamType)((ToggleButton)sender).Value;
        }
    }
}