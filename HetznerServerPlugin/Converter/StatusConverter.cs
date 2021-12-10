using System;
using System.Globalization;
using Avalonia.Data.Converters;
using Avalonia.Media;
using Brushes = System.Drawing.Brushes;

namespace PscCloud.Plugin.HetznerServerPlugin.Converter
{
    public class StatusConverter : IValueConverter
    {
        public object Convert(object value, Type targetType, object parameter, CultureInfo culture)
        {
            var brush = new SolidColorBrush();
            brush.Color = Colors.Green;

            if (value.Equals("running")) return brush;

            brush.Color = Colors.Red;

            return brush;
        }

        public object ConvertBack(object value, Type targetType, object parameter, CultureInfo culture)
        {
            if (value.Equals("RUNNING")) return "green";

            return "red";
        }
    }
}
