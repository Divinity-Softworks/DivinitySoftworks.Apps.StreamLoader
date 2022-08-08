using DivinitySoftworks.Apps.Core.Components.Base;
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
            BorderThickness = WindowState == WindowState.Maximized
                ? new Thickness(0, 0, 0, SystemParameters.PrimaryScreenHeight - SystemParameters.FullPrimaryScreenHeight - SystemParameters.WindowCaptionHeight)
                : new Thickness(0, 0, 0, 0);
        }

        private async void Window_Loaded(object sender, RoutedEventArgs e) {
            await ViewModel.LoadAsync();
        }

        private void OnMinimize(object sender, System.Windows.Input.MouseButtonEventArgs e) {
            WindowState = WindowState.Minimized;

        }
        private void OnMaximize(object sender, System.Windows.Input.MouseButtonEventArgs e) {
            WindowState = WindowState == WindowState.Maximized ? WindowState.Normal : WindowState.Maximized;

        }
        private void OnClose(object sender, System.Windows.Input.MouseButtonEventArgs e) {
            Close();
        }

        private void Window_StateChanged_1(object sender, EventArgs e) {

        }
    }
}
