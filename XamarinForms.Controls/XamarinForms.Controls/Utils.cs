using System.Diagnostics;
using Xamarin.Forms;

namespace XamarinForms.Controls
{
	public class Utils
	{
		public const double Tolerance = 0.0000001;
		public static double ScaleFactor;

		public static double GetScalledFontSize(double size) { return size * ScaleFactor; }

		public static Color Lighter(Color color) { return color.WithLuminosity(color.Luminosity + 0.2); }

		public static Color Darker(Color color) { return color.WithLuminosity(color.Luminosity - 0.2); }

		public static Color Lighten(Color color, double amount) { return color.WithLuminosity(color.Luminosity + amount); }

		public static Color Darken(Color color, double amaount) { return color.WithLuminosity(color.Luminosity - amaount); }

		public static Color Saturate(Color color, double amount) { return color.WithSaturation(color.Saturation + amount); }

		public static Color Desaturate(Color color, double amaount) { return color.WithSaturation(color.Saturation - amaount); }

		public static Color Grayscale(Color color) { return color.WithSaturation(0); }

		public static Color Complement(Color color)
		{
			var hue = color.Hue * 359.0;
			var newHue = (hue + 180) % 359.0;
			var complement = color.WithHue(newHue / 359.0);

			return complement;
		}

		public static Color InvertColor(Color color)
		{
			var r = 255 - (int)(255 * color.R);
			var g = 255 - (int)(255 * color.G);
			var b = 255 - (int)(255 * color.B);
			return Color.FromRgb(r, g, b);
		}

		public static void PrintColor(Color color, string label = null)
		{
			var r = (int)(255 * color.R);
			var g = (int)(255 * color.G);
			var b = (int)(255 * color.B);

			Debug.WriteLine("{3} := R:{0} G:{1} B:{2}", r, g, b, label);
			Debug.WriteLine(color.ToString());
		}
	}
}