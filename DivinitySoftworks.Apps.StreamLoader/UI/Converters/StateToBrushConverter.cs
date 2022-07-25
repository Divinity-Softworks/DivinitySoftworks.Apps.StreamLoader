using DivinitySoftworks.Apps.StreamLoader.Data.Enums;
using System;
using System.Globalization;
using System.Windows.Data;
using System.Windows.Markup;

namespace DivinitySoftworks.Apps.StreamLoader.UI.Converters {
    internal class StateToBrushConverter : MarkupExtension, IValueConverter {
        public BrushType BrushType { get; set; } = BrushType.Unknown;

        public object Convert(object value, Type targetType, object parameter, CultureInfo culture) {
            if (BrushType is BrushType.Unknown) throw new ArgumentNullException(nameof(BrushType));
            
            if(value is not State)
                throw new InvalidCastException(nameof(value));

            State? state = (State)value;

            switch (state) {
                default:
                    break;
            }

        }

        public object ConvertBack(object value, Type targetType, object parameter, CultureInfo culture) {
            throw new NotImplementedException();
        }

        public override object ProvideValue(IServiceProvider serviceProvider) {
            return this;
        }
    }
}
