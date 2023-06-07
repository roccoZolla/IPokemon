using System;
using Windows.UI.Xaml.Data;

namespace IPokemon
{
    public class PPToEnabledConverter : IValueConverter
    {
        public object Convert(object value, Type targetType, object parameter, string language)
        {
            int pp = (int)value;
            return pp > 0;
        }

        public object ConvertBack(object value, Type targetType, object parameter, string language)
        {
            throw new NotSupportedException();
        }
    }
}
