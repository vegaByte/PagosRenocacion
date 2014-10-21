using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Data;
using System.Windows.Media;

namespace PagosRenovacion.Commands.Converters
{
    public class ConverterStatus : IValueConverter
    {
        public object Convert(object value, Type targetType, object parameter, System.Globalization.CultureInfo culture)
        {
            if (value == null)
                return null;

            var status = value.ToString();
            SolidColorBrush result = new SolidColorBrush(Colors.Aqua);
            switch (status)
            {
                case "Pagado":
                    result = new SolidColorBrush(Colors.LawnGreen);
                    break;
            }

            return result;
        }
        public object ConvertBack(object value, Type targetType, object parameter, System.Globalization.CultureInfo culture)
        {
            return null;
        }
    }
}
