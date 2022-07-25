using System;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Media;

namespace DivinitySoftworks.Apps.StreamLoader.UI.Components {
    public partial class MenuItem : Button {
        public MenuItem() {
            InitializeComponent();
        }

        public bool IsSelected {
            get { return (bool)GetValue(IsSelectedProperty); }
            set { SetValue(IsSelectedProperty, value); }
        }

        public static readonly DependencyProperty IsSelectedProperty = DependencyProperty.Register(nameof(IsSelected), typeof(bool), typeof(MenuItem), new PropertyMetadata(false));

        public Brush Selected {
            get { return (Brush)GetValue(SelectedProperty); }
            set { SetValue(SelectedProperty, value); }
        }

        public static readonly DependencyProperty SelectedProperty = DependencyProperty.Register(nameof(Selected), typeof(Brush), typeof(MenuItem), new PropertyMetadata(new SolidColorBrush(Colors.Black)));

        public Geometry Icon {
            get { return (Geometry)GetValue(IconProperty); }
            set { SetValue(IconProperty, value); }
        }

        public static readonly DependencyProperty IconProperty = DependencyProperty.Register(nameof(Icon), typeof(Geometry), typeof(MenuItem));

        public Type Target {
            get { return (Type)GetValue(TargetProperty); }
            set { SetValue(TargetProperty, value); }
        }

        public static readonly DependencyProperty TargetProperty = DependencyProperty.Register(nameof(Target), typeof(Type), typeof(MenuItem));
    }
}
