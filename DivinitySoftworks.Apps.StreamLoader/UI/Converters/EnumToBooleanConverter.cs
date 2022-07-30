using System;
using System.Globalization;
using System.Windows;
using System.Windows.Data;
using System.Windows.Markup;

namespace DivinitySoftworks.Apps.StreamLoader.UI.Converters {
    internal class EnumToBooleanConverter : MarkupExtension, IValueConverter {

        public Enum? True { get; set; }

        public object Convert(object value, Type targetType, object parameter, CultureInfo culture) {
            if (True is null) throw new ArgumentNullException(nameof(True));

            if (value != null && value.GetType().IsEnum)
                return Equals(value, True);
            else
                return DependencyProperty.UnsetValue;
        }

        public object ConvertBack(object value, Type targetType, object parameter, CultureInfo culture) {
            if (True is null) throw new ArgumentNullException(nameof(True));

            if (value is bool && (bool)value)
                return True;
            else
                return DependencyProperty.UnsetValue;
        }

        public override object ProvideValue(IServiceProvider serviceProvider) {
            return this;
        }
    }
}
