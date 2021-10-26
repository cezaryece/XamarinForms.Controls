using System;
using Xamarin.Forms;
using Xamarin.Forms.Xaml;

namespace XamarinForms.Controls.Clases
{
	[ContentProperty("Text")]
	public class TextToUpperExtension : IMarkupExtension
	{
		public string Source { get; set; }

		public object ProvideValue(IServiceProvider serviceProvider) { return Source?.ToUpper(); }
	}
}