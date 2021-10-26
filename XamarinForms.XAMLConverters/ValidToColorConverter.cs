using System;
using System.Globalization;
using Xamarin.Forms;

namespace XamarinForms.XAMLConverters
{
	public class ValidToColorConverter : IValueConverter
	{
		//parameter: string "True" or "False" indicating if value should be inverted
		public object Convert(object value, Type targetType, object parameter, CultureInfo culture)
		{
			var converted = (bool)value;
			if (parameter != null && bool.Parse((string)parameter)) converted = !converted;
			return converted ? Color.Lime : Color.Red;
		}

		public object ConvertBack(object value, Type targetType, object parameter, CultureInfo culture) { throw new NotImplementedException(); }
	}
}