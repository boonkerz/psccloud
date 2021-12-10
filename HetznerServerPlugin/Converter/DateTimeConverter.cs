using System;
using System.Globalization;
using Avalonia.Data.Converters;
using Avalonia.Media;
using Brushes = System.Drawing.Brushes;

namespace PscCloud.Plugin.HetznerServerPlugin.Converter
{
    public class DateTimeConverter : IValueConverter
    {
        public object Convert(object value, Type targetType, object parameter, CultureInfo culture)
        {
            var item = (DateTime)value;
            var brush = new SolidColorBrush();
            brush.Color = Colors.Red;

            if ((DateTime.Now - item).TotalDays > 2) return brush;

            brush.Color = Colors.Green;

            return brush;
        }

        public object ConvertBack(object value, Type targetType, object parameter, CultureInfo culture)
        {
            if (value.Equals("RUNNING")) return "green";

            return "red";
        }
    }
}
