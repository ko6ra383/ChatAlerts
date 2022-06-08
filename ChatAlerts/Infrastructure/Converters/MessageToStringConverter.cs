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
    public class MessageToStringConverter : IValueConverter
    {
        public object Convert(object value, Type targetType, object parameter, CultureInfo culture)
        {
            //if (!(value is IEnumerable<Message> msg)) return null;
            //$"{msg.UserID} <{msg.TimeStamp:H:m d:M:y}> {msg.MessageText}"
            return null;
        }

        public object ConvertBack(object value, Type targetType, object parameter, CultureInfo culture)
        {
            throw new NotImplementedException();
        }
    }
}
