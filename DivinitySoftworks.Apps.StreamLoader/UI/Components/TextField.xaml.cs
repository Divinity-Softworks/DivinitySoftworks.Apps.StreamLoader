using System.Windows;
using System.Windows.Controls;
using System.Windows.Media;

namespace DivinitySoftworks.Apps.StreamLoader.UI.Components {

    public partial class TextField : TextBox {
        public TextField() {
            InitializeComponent();
        }

        public string Placeholder {
            get { return (string)GetValue(PlaceholderProperty); }
            set { SetValue(PlaceholderProperty, value); }
        }

        public static readonly DependencyProperty PlaceholderProperty = DependencyProperty.Register(nameof(Placeholder), typeof(string), typeof(TextField), new PropertyMetadata(string.Empty));

        public Brush PlaceholderBrush {
            get { return (Brush)GetValue(PlaceholderBrushProperty); }
            set { SetValue(PlaceholderBrushProperty, value); }
        }

        public static readonly DependencyProperty PlaceholderBrushProperty = DependencyProperty.Register(nameof(PlaceholderBrush), typeof(Brush), typeof(TextField), new PropertyMetadata(new SolidColorBrush(Colors.Black)));

        public Brush FocusBrush {
            get { return (Brush)GetValue(FocusBrushProperty); }
            set { SetValue(FocusBrushProperty, value); }
        }

        public static readonly DependencyProperty FocusBrushProperty = DependencyProperty.Register(nameof(FocusBrush), typeof(Brush), typeof(TextField), new PropertyMetadata(new SolidColorBrush(Colors.Black)));

        public Geometry Icon {
            get { return (Geometry)GetValue(IconProperty); }
            set { SetValue(IconProperty, value); }
        }

        public static readonly DependencyProperty IconProperty = DependencyProperty.Register(nameof(Icon), typeof(Geometry), typeof(TextField), new PropertyMetadata(null));







        
    }
}
