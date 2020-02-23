using System;
using System.Windows.Data;
using System.Globalization;

namespace Drills.TennisMatch.Desktop.Converters
{
    [ValueConversion(typeof(bool), typeof(bool))]
    public class NegationConverter : IValueConverter
    {
        public object Convert(object value, Type targetType, object _p, CultureInfo _c)
        {
            if (targetType == typeof(bool)) 
            {
                return !(bool)value;
            }
            else
            {
                throw new InvalidOperationException($"Target of type {targetType.FullName} cannot be converted to boolean");
            }
        }            

        public object ConvertBack(object _v, Type _t, object _p, CultureInfo _c) =>
            throw new NotSupportedException();
    }
}
