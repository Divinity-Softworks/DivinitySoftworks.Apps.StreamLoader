using System;
using System.Timers;
using System.Windows.Controls;

namespace DivinitySoftworks.Apps.StreamLoader.UI.Components {
    public partial class DateTimeLabel : Label {
        private readonly Timer _timer;

        public DateTimeLabel() {
            InitializeComponent();

            Content = DateTime.Now.ToGreeting();

            _timer = new Timer(500);
            _timer.Elapsed += Timer_Elapsed;
            _timer.Start();
        }

        private void Timer_Elapsed(object? sender, ElapsedEventArgs e) {
            if (e.SignalTime.Second != 0) return;

            Dispatcher.Invoke(() => {
                Content = DateTime.Now.ToGreeting();
            });
        }
    }
}