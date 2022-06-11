using ChatAlerts.Models;
using System;
using System.Collections.Generic;
using System.Globalization;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Data;

namespace ChatAlerts.Infrastructure.Converters
{
    public class TupleConverter : IMultiValueConverter
    {
        public object Convert(object[] values, Type targetType, object parameter, CultureInfo culture)
        {
            if(values.Length == 0) return null;
            if (values[0] is string val0)
                if (val0.Length < 2) return null;
            if (values[1] is string val1)
                if (val1.Length < 2) return null;
            return new Tuple<string, string>((string)values[0], (string)values[1]);
        }

        public object[] ConvertBack(object value, Type[] targetTypes, object parameter, CultureInfo culture)
        {
            throw new NotImplementedException();
        }
    }
}
