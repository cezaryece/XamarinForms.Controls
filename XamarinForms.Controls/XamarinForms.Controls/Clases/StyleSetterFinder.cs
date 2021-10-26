using System;
using System.Diagnostics;
using System.Linq;
using Xamarin.Forms;

namespace XamarinForms.Controls.Clases
{
	/// <summary>
	///     Finds setter in style and in styles it is based on
	/// </summary>
	public class StyleSetterFinder<T>
	{
		public static T Get(string propertyName, Style style)
		{
			try
			{
				var setter = style.Setters.FirstOrDefault(s => s.Property.PropertyName == propertyName);
				if (setter == null && style.BasedOn != null)
					return Get(propertyName, style.BasedOn);
				return (T)setter?.Value;
			}
			catch (Exception e)
			{
				Debug.WriteLine(e);
			}

			return default;
		}
	}
}