using System;
using System.Globalization;
using System.Windows.Data;
using System.Windows.Markup;
using System.Windows.Media;

namespace DivinitySoftworks.Apps.StreamLoader.UI.Converters {
    internal class BooleanToBrushConverter : MarkupExtension, IValueConverter {

        public Brush True { get; set; } = Brushes.Black;

        public Brush False { get; set; } = Brushes.White;

        public object Convert(object value, Type targetType, object parameter, CultureInfo culture) {
            if (value is not bool)
                return False;

            return ((bool) value) ? True : False;
        }

        public object ConvertBack(object value, Type targetType, object parameter, CultureInfo culture) {
            throw new NotImplementedException();
        }

        public override object ProvideValue(IServiceProvider serviceProvider) {
            return this;
        }
    }
}
