using Xamarin.Forms;

namespace XamarinForms.Controls.Basic
{
	public class LabelExtended : Label
	{
		public LabelExtended()
		{
			Resources = Application.Current.Resources;
			PropertyChanged += (sender, args) =>
			{
				if (args.PropertyName != nameof(Text) || Text == null || !ToUpper) return;
				var currText = Text;
				if (currText == Text.ToUpper()) return;
				Text = Text.ToUpper();
			};
		}

		public static BindableProperty FontAssetProperty = BindableProperty.Create(nameof(FontAsset), typeof(string), typeof(LabelExtended), string.Empty);

		/// <summary>
		///     Font file name located in Asset directory
		/// </summary>
		public string FontAsset { get => (string)GetValue(FontAssetProperty); set => SetValue(FontAssetProperty, value); }

		public static BindableProperty ToUpperProperty = BindableProperty.Create(nameof(ToUpper), typeof(bool), typeof(LabelExtended), false, propertyChanged: HandleToUpperChanged);

		private static void HandleToUpperChanged(BindableObject bindable, object oldvalue, object newvalue)
		{
			var me = (LabelExtended)bindable;
			if ((bool)newvalue && me.Text != null)
				me.Text = me.Text.ToUpper();
		}

		public bool ToUpper { get => (bool)GetValue(ToUpperProperty); set => SetValue(ToUpperProperty, value); }

		public static BindableProperty FontNameProperty = BindableProperty.Create(nameof(FontName), typeof(string), typeof(LabelExtended), string.Empty);

		/// <summary>
		///     Used and requierd only by Windows/WindowsPhone apps
		/// </summary>
		public string FontName { get => (string)GetValue(FontNameProperty); set => SetValue(FontNameProperty, value); }
	}
}