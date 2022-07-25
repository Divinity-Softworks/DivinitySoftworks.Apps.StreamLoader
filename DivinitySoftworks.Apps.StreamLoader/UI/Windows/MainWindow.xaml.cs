using DivinitySoftworks.Apps.StreamLoader.UI.Components;
using DivinitySoftworks.Apps.StreamLoader.UI.ViewModels;
using System;
using System.Windows;

namespace DivinitySoftworks.Apps.StreamLoader.UI.Windows {

    public partial class MainWindow : Window {
        public MainWindow(IMainWindowViewModel mainWindowViewModel) {
            InitializeComponent();

            DataContext = mainWindowViewModel;
        }

        public IMainWindowViewModel ViewModel {
            get {
                return (IMainWindowViewModel)DataContext;
            }
        }

        private void MenuItem_Click(object sender, RoutedEventArgs e) {
            ViewModel.SetPage(((MenuItem)sender).Target);
        }

        private void Window_StateChanged(object sender, EventArgs e) {
        }

        private async void Window_Loaded(object sender, RoutedEventArgs e) {
            await ViewModel.LoadAsync();
        }
    }
}
