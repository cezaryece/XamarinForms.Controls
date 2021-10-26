using System;
using System.Globalization;
using Xamarin.Forms;

namespace XamarinForms.XAMLConverters
{
	public class BoolToDoubleConverter : IValueConverter
	{
		//parameter: int returned if value is true, if parameter is negative input value is inverted

		#region Implementation of IValueConverter
		public object Convert(object value, Type targetType, object parameter, CultureInfo culture)
		{
			double convertedParam;
			var str = (string)parameter;
			if (!double.TryParse(str, out convertedParam)) return 0;
			if (Math.Abs(convertedParam) < 0.0000001) return 0.0;
			if ((bool)value) return convertedParam >= 0 ? convertedParam : 0;

			//else
			return convertedParam >= 0 ? 0 : Math.Abs(convertedParam);
		}

		public object ConvertBack(object value, Type targetType, object parameter, CultureInfo culture) { throw new NotImplementedException(); }
		#endregion
	}
}