using System.Windows;
using System.Windows.Controls;
using System.Windows.Media;

namespace DivinitySoftworks.Apps.StreamLoader.UI.Components {

    public partial class ToggleButton : Button {
        public ToggleButton() {
            InitializeComponent();
        }

        public bool IsToggled {
            get { return (bool)GetValue(IsToggledProperty); }
            set { SetValue(IsToggledProperty, value); }
        }

        public static readonly DependencyProperty IsToggledProperty = DependencyProperty.Register(nameof(IsToggled), typeof(bool), typeof(ToggleButton), new PropertyMetadata(false));
        
        public Brush Toggled {
            get { return (Brush)GetValue(ToggledProperty); }
            set { SetValue(ToggledProperty, value); }
        }

        public static readonly DependencyProperty ToggledProperty = DependencyProperty.Register(nameof(Toggled), typeof(Brush), typeof(ToggleButton), new PropertyMetadata(new SolidColorBrush(Colors.Black)));

        public CornerRadius CornerRadius {
            get { return (CornerRadius)GetValue(CornerRadiusProperty); }
            set { SetValue(CornerRadiusProperty, value); }
        }

        public static readonly DependencyProperty CornerRadiusProperty = DependencyProperty.Register(nameof(CornerRadius), typeof(CornerRadius), typeof(ToggleButton), new PropertyMetadata(new CornerRadius(0)));

        public object Value {
            get { return GetValue(ValueProperty); }
            set { SetValue(ValueProperty, value); }
        }

        public static readonly DependencyProperty ValueProperty = DependencyProperty.Register(nameof(Value), typeof(object), typeof(ToggleButton), new PropertyMetadata(null));
    }
}
