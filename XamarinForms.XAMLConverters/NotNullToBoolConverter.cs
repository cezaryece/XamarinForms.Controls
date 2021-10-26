using System;
using System.Globalization;
using Xamarin.Forms;

namespace XamarinForms.XAMLConverters
{
	public class NotNullToBoolConverter : IValueConverter
	{
		//parameter: string "True" or "False" indicating if value should be inverted
		public object Convert(object value, Type targetType, object parameter, CultureInfo culture)
		{
			var converted = value != null;
			if (parameter != null && bool.Parse((string)parameter)) converted = !converted;
			return converted;
		}

		public object ConvertBack(object value, Type targetType, object parameter, CultureInfo culture) { throw new NotImplementedException(); }
	}
}