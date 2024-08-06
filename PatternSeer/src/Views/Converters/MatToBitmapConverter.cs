using System.Diagnostics;
using System.Globalization;
using Avalonia;
using Avalonia.Data.Converters;
using Avalonia.Media;
using Avalonia.Media.Imaging;
using Emgu.CV;
using Emgu.CV.CvEnum;
using Emgu.CV.Util;

namespace PatternSeer.Converters;

/// <summary>
/// <c>IValueConverter</c> that converts from <c>Emgu.CV.Mat</c> to
/// <c>Avalonia.Media.Imaging.Bitmap</c>.
/// </summary>
public class MatToBitmapConverter : IValueConverter {
    public object Convert(object? value, Type targetType, object? parameter, CultureInfo culture)
    {
        if (value is Mat mat && targetType == typeof(IImage))
        {
            using (MemoryStream imageStream = new MemoryStream())
            {
                VectorOfByte matBytes = new VectorOfByte();
                CvInvoke.Imencode(".png", mat, matBytes);
                imageStream.Write(matBytes.ToArray());
                imageStream.Position = 0;
                return new Bitmap(imageStream);
            }
        }

        Debug.WriteLine("Something went wrong converting from Mat to Bitmap.");
        return AvaloniaProperty.UnsetValue;
    }

    public object ConvertBack(object? value, Type targetType, object? parameter, CultureInfo culture)
    {
        if (value is Bitmap bitmap && targetType == typeof(Mat))
        {
            using (MemoryStream imageStream = new MemoryStream())
            {
                Mat mat = new Mat();
                bitmap.Save(imageStream);
                CvInvoke.Imdecode(imageStream.ToArray(), ImreadModes.Color, mat);
                return mat;
            }
        }

        Debug.WriteLine("Something went wrong converting back from Bitmap to Mat.");
        return AvaloniaProperty.UnsetValue;
    }
}
