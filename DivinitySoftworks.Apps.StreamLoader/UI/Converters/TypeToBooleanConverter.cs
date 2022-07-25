using System;
using System.Globalization;
using System.Windows.Data;
using System.Windows.Markup;

namespace DivinitySoftworks.Apps.StreamLoader.UI.Converters {
    internal class TypeToBooleanConverter : MarkupExtension, IValueConverter {

        public Type? Type { get; set; }

        public object Convert(object value, Type targetType, object parameter, CultureInfo culture) {
            if (Type is null) throw new ArgumentNullException(nameof(Type));
            return value.GetType() == Type;
        }

        public object ConvertBack(object value, Type targetType, object parameter, CultureInfo culture) {
            throw new NotImplementedException();
        }

        public override object ProvideValue(IServiceProvider serviceProvider) {
            return this;
        }
    }
}
