using System;
using System.Globalization;
using Xamarin.Forms;

namespace XamarinForms.XAMLConverters
{
	public class InverseBooleanConverter : IValueConverter
	{
		//parameter: second condition to check
		public object Convert(object value, Type targetType, object parameter, CultureInfo culture) { return parameter == null ? !(bool)value : !((bool)value && (bool)parameter); }

		public object ConvertBack(object value, Type targetType, object parameter, CultureInfo culture) { throw new NotImplementedException(); }
	}
}