using System;
using System.Collections.Generic;
using System.Globalization;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Data;

namespace onTime
{
    public class SecondsToHourConverter : IValueConverter
    {
        public object Convert(object value, Type targetType, object parameter, CultureInfo culture)
        {
            if (value is int)
            {
                return TimeSpan.FromSeconds((int)value).Hours.ToString("D2");
            }
            return value;
        }

        public object ConvertBack(object value, Type targetType, object parameter, CultureInfo culture)
        {
            return value;
        }
    }

    public class SecondsToMinuteConverter : IValueConverter
    {
        public object Convert(object value, Type targetType, object parameter, CultureInfo culture)
        {
            if (value is int)
            {
                return TimeSpan.FromSeconds((int)value).Minutes.ToString("D2");
            }
            return value;
        }

        public object ConvertBack(object value, Type targetType, object parameter, CultureInfo culture)
        {
            return value;
        }
    }

    public class DepartureGroup : List<Departure>
    {
        public string Hour { get; set; }

        public DepartureGroup(string hour, IEnumerable<Departure> items) : base(items)
        {
            Hour = hour;
        }
    }
}
