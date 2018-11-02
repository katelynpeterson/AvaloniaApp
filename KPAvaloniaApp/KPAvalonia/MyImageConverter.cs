using Avalonia;
using Avalonia.Data.Converters;
using Avalonia.Media.Imaging;
using Avalonia.Platform;
using Avalonia.Utilities;
using System;
using System.Collections.Generic;
using System.Globalization;
using System.IO;
using System.Text;

namespace KPAvalonia
{
    class MyImageConverter : IValueConverter
    {
        public static MyImageConverter Instance = new MyImageConverter();

        public object Convert(object value, Type targetType, object parameter, CultureInfo culture)
        {
            if (value is string && targetType == typeof(IBitmap))
            {
                var uri = new Uri((string)value, UriKind.RelativeOrAbsolute);
                var scheme = uri.IsAbsoluteUri ? uri.Scheme : "file";

                switch (scheme)
                {
                    case "file":
                        return new Bitmap((string)value);

                    default:
                        var assets = AvaloniaLocator.Current.GetService<IAssetLoader>();
                        return new Bitmap(assets.Open(uri));
                }
            }

            throw new NotSupportedException();
        }

        public object ConvertBack(object value, Type targetType, object parameter, CultureInfo culture)
        {
            throw new NotImplementedException();
        }
    }
}
