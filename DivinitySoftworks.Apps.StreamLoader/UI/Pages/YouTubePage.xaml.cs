using DivinitySoftworks.Apps.StreamLoader.UI.ViewModels;
using System;
using System.Windows.Controls;
using System.Windows.Input;

namespace DivinitySoftworks.Apps.StreamLoader.UI.Pages {

    public partial class YouTubePage : Page {
        public YouTubePage(IYouTubePageViewModel youTubePageViewModel) {
            InitializeComponent();

            DataContext = youTubePageViewModel;
        }

        public IYouTubePageViewModel ViewModel {
            get {
                return (IYouTubePageViewModel)DataContext;
            }
        }

        private void TextBox_KeyUp(object sender, KeyEventArgs e) {
            if (e.Key is not Key.Enter)
                return;

            if (!Uri.TryCreate(ViewModel.Url, UriKind.Absolute, out Uri? uri))
                return;

            ViewModel.AddStreamItem(uri);
            ViewModel.Url = string.Empty;
        }
    }
}

//https://www.youtube.com/watch?v=FFqgR71-kYA&t=449s