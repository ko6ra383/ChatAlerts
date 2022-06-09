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
    public class IdToUserName : IMultiValueConverter
    {

        public object Convert(object[] values, Type targetType, object parameter, CultureInfo culture)
        {
            if (values.Length == 0) return null;
            if (!(values[0] is int ID)) return null;
            if (!(values[1] is IEnumerable<User> mass)) return null;

            string name = mass.Where(x => x.ID == ID).Select(x => x.Login).First();

            return name;
        }

        public object[] ConvertBack(object value, Type[] targetTypes, object parameter, CultureInfo culture)
        {
            throw new NotImplementedException();
        }
    }
}
