using System;
using System.Globalization;
using Xamarin.Forms;

namespace XamarinForms.XAMLConverters
{
	public class DatetimeToStringConverter : BindableObject, IValueConverter
	{
		public static string DateFormat = CultureInfo.CurrentUICulture.DateTimeFormat.ShortDatePattern;

		#region IValueConverter implementation
		public object Convert(object value, Type targetType, object parameter, CultureInfo culture)
		{
			if (value == null)
				return string.Empty;

			var datetime = (DateTime)value;
			return datetime.ToString(DateFormat);
		}

		public object ConvertBack(object value, Type targetType, object parameter, CultureInfo culture) { throw new NotImplementedException(); }
		#endregion
	}
}