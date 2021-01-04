using AvalonDock;
using System;
using System.Globalization;
using System.Windows.Data;

namespace Converters
{
    public class DocumentClosingEventsConverter : IValueConverter
    {
        public object Convert(object value, Type targetType, object parameter, CultureInfo culture)
        {
            return value is DocumentClosedEventArgs args ? args.Document.Content : null;
        }

        public object ConvertBack(object value, Type targetType, object parameter, CultureInfo culture)
        {
            throw new NotImplementedException();
        }
    }
}
