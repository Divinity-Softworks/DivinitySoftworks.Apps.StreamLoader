using DivinitySoftworks.Apps.StreamLoader.Data.Enums;
using System;
using System.Globalization;
using System.Windows;
using System.Windows.Data;
using System.Windows.Markup;

namespace DivinitySoftworks.Apps.StreamLoader.UI.Converters {
    internal class StateToBrushConverter : MarkupExtension, IValueConverter {
        public BrushType BrushType { get; set; } = BrushType.Regular;

        public object Convert(object value, Type targetType, object parameter, CultureInfo culture) {
            if (BrushType is BrushType.Unknown) throw new ArgumentNullException(nameof(BrushType));

            if (value is not State)
                throw new InvalidCastException(nameof(value));

            State? state = (State)value;

            return Application.Current.FindResource(GetResource(state ?? State.Initial));
        }

        public object ConvertBack(object value, Type targetType, object parameter, CultureInfo culture) {
            throw new NotImplementedException();
        }

        public override object ProvideValue(IServiceProvider serviceProvider) {
            return this;
        }

        private string GetResource(State state) {
            string resource = "Brushes.";

            switch (state) {
                case State.Error:
                    resource += "Red";
                    break;
                case State.Info:
                    resource += "Blue";
                    break;
                case State.Warning:
                    resource += "Orange";
                    break;
                case State.Success:
                    resource += "Green";
                    break;
                default:
                    resource += "Gray";
                    break;
            }

            if (BrushType == BrushType.Light)
                return $"{resource}.Light";
            if (BrushType == BrushType.Dark)
                return $"{resource}.Dark";
            return resource;
        }
    }
}
