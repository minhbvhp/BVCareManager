using System;
using System.Collections.Generic;
using System.Globalization;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Data;

namespace BVCareManager.Converter
{
    class NumberFormatConverter : IValueConverter
    {
        public object Convert(object value, Type targetType, object parameter, CultureInfo culture)
        {
            if (value is int)
            {
                if ((int)value != 0)
                    return ((int)value).ToString("N0", culture);
                else
                    return String.Empty;
            }
            else
            {
                return String.Empty;
            }
        }

        public object ConvertBack(object value, Type targetType, object parameter, CultureInfo culture)
        {
            int ret;

            if (int.TryParse((string)value, NumberStyles.AllowThousands, culture, out ret))
            {
                return ret;
            }
            return 0;
        }
    }
}
