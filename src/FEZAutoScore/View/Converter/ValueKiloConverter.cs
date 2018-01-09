using System;
using System.Globalization;
using System.Windows.Data;

namespace FEZAutoScore.View.Converter
{
    public class ValueKiloConverter : IValueConverter
    {
        public object Convert(object value, Type targetType, object parameter, CultureInfo culture)
        {
            double ret = 0.0;

            try
            {
                var val = double.Parse(value.ToString());
                ret = val / 1000;
            }
            catch
            { }

            return ret;
        }

        public object ConvertBack(object value, Type targetType, object parameter, CultureInfo culture)
        {
            double ret = 0.0;

            try
            {
                var val = (double)value;
                ret = val * 1000;
            }
            catch
            { }

            return ret;
        }
    }
}
