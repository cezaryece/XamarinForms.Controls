using Xamarin.Forms;
using XamarinForms.Controls.Clases;

namespace XamarinForms.Controls.Basic
{
	public class CheckBoxWithTag : CheckboxExtended
	{
		public void SetCheckBox(CheckBoxItem item)
		{
			Tag = item.TagObject;
			DefaultText = item.KeyString;
			Checked = item.IsChecked;
		}

		public static BindableProperty TagProperty = BindableProperty.Create(nameof(Tag), typeof(object), typeof(CheckBoxWithTag));
		public object Tag { get => GetValue(TagProperty); set => SetValue(TagProperty, value); }
	}
}